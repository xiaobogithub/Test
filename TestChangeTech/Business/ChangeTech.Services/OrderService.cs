using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Globalization;

namespace ChangeTech.Services
{
    public class OrderService :ServiceBase, IOrderService
    {
        private const string ORDERTYPE_OPENLICENCE_NAME = "Open licence";
        public void InsertOrder(OrderModel orderModel)
        {
            Order orderEntity = new Order();
            orderEntity.OrderGUID = orderModel.OrderGUID;
            orderEntity.CustomerName = orderModel.CustomerName;
            orderEntity.CustomerEmail = orderModel.CustomerEmail;
            orderEntity.LanguageGUID = orderModel.LanguageGUID;
            orderEntity.OrderStatusID = orderModel.OrderStatusID;
            orderEntity.ExpiredDate = orderModel.ExpiredDate;
            orderEntity.IsPromotion = orderModel.IsPromotion;
            orderEntity.UpdatedBy = orderModel.UpdatedBy;
            orderEntity.Created = orderModel.Created;
            orderEntity.IsDeleted = null;
            orderEntity.OrderLicenceTypeGUID = orderModel.LicenceTypeGUID;
            orderEntity.NumberOfEmployees = orderModel.NumberOfEmployees;

            foreach (OrderContentModel orderContentModel in orderModel.orderContents)
            {
                OrderContent orderContentEntity = new OrderContent()
                {
                    OrderContentGUID = orderContentModel.OrderContentGUID,
                    OrderGUID = orderContentModel.OrderGUID,
                    ProgramGUID = orderContentModel.ProgramGUID,
                    UpdatedBy = orderContentModel.UpdatedBy,
                    Licences = orderContentModel.ProgramLicences,
                    IsDeleted = null
                };
                orderEntity.OrderContent.Add(orderContentEntity);
            }
            
            Resolve<IOrderRepository>().Insert(orderEntity);
        }

        public void UpdateOrder(OrderModel orderModel)
        {
            Order orderEntity = Resolve<IOrderRepository>().GetOrderByOrderGuid(orderModel.OrderGUID);
            if (orderEntity!=null)
            {
                orderEntity.OrderGUID = orderModel.OrderGUID;
                orderEntity.LanguageGUID = orderModel.LanguageGUID;
                orderEntity.OrderStatusID = orderModel.OrderStatusID.Value;
                orderEntity.IsPromotion = orderModel.IsPromotion.Value;
                orderEntity.ExpiredDate = orderModel.ExpiredDate.Value;
                orderEntity.UpdatedBy = orderModel.UpdatedBy;
                orderEntity.CustomerName = orderModel.CustomerName;
                orderEntity.CustomerEmail = orderModel.CustomerEmail;
                orderEntity.Created = orderModel.Created;
            }
            Resolve<IOrderRepository>().Update(orderEntity);
        }

        public OrderModel GetOrderByOrderGuid(Guid orderGuid)
        {
            Order orderEntity = Resolve<IOrderRepository>().GetOrderByOrderGuid(orderGuid);
            OrderModel orderModel = null;
            if (orderEntity != null)
            {
                orderModel = new OrderModel();
                orderModel.OrderGUID = orderEntity.OrderGUID;
                orderModel.CustomerName = orderEntity.CustomerName;
                orderModel.CustomerEmail = orderEntity.CustomerEmail;
                orderModel.Created = orderEntity.Created.Value; //orderEntity.Created.Value.ToString("dd.MM.yyyy");
                orderModel.ExpiredDate = orderEntity.ExpiredDate.Value;
                orderModel.IsPromotion = orderEntity.IsPromotion.Value;
                orderModel.UpdatedBy = orderEntity.UpdatedBy.Value;
                orderModel.OrderStatusID = orderEntity.OrderStatusID.Value;
                orderModel.NumberOfEmployees = orderEntity.NumberOfEmployees;
                if (!orderEntity.OrderLicenceTypeReference.IsLoaded) orderEntity.OrderLicenceTypeReference.Load();
                orderModel.LicenceTypeGUID = orderEntity.OrderLicenceTypeGUID;
                if (!orderEntity.LanguageReference.IsLoaded) orderEntity.LanguageReference.Load();
                orderModel.LanguageGUID = orderEntity.Language.LanguageGUID;
            }
            return orderModel;
        }

        public List<OrderModel> GetOrdersByResellerGuid(Guid resellerGuid)
        {
            int allLicenceCount = 0;
            int usedLicenceCount = 0;
            List<OrderModel>ordersModelList = new List<OrderModel>();
            List<Order> orders = Resolve<IOrderRepository>().GetOrdersByResellerGuid(resellerGuid).ToList();
            if (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    usedLicenceCount = 0;
                    OrderModel orderModel = new OrderModel();
                    orderModel.CustomerName = order.CustomerName;
                    orderModel.CustomerEmail = order.CustomerEmail;
                    orderModel.Created = order.Created.Value; //order.Created.Value.ToString("dd.MM.yyyy");
                    orderModel.OrderGUID = order.OrderGUID;
                    orderModel.LanguageGUID = order.LanguageGUID.HasValue ? order.LanguageGUID.Value : Guid.Empty;
                    orderModel.UpdatedBy = order.UpdatedBy.HasValue ? order.UpdatedBy.Value : Guid.Empty;
                    orderModel.IsPromotion = order.IsPromotion.HasValue ? order.IsPromotion.Value : false;
                    orderModel.NumberOfEmployees = order.NumberOfEmployees;
                    if (!order.OrderLicenceTypeReference.IsLoaded) order.OrderLicenceTypeReference.Load();
                    orderModel.LicenceTypeGUID = order.OrderLicenceTypeGUID;

                    allLicenceCount = Resolve<IOrderContentService>().GetLicenceCountByOrderGuid(order.OrderGUID);
                    
                    if (!order.OrderLicenceTypeReference.IsLoaded)
                    {
                        order.OrderLicenceTypeReference.Load();
                    }
                    if (order.OrderLicenceType.Name == ORDERTYPE_OPENLICENCE_NAME)
                    {
                        orderModel.OrderLicences = ORDERTYPE_OPENLICENCE_NAME;
                    }
                    else
                    {
                        orderModel.OrderLicences = allLicenceCount.ToString();
                    }

                    List<OrderContentModel> orderContents = Resolve<IOrderContentService>().GetOrderContentsByOrderGuid(order.OrderGUID);
                    if (orderContents.Count > 0)
                    {
                        foreach (OrderContentModel orderContent in orderContents)
                        {
                            usedLicenceCount += orderContent.UsedLicences;
                        }
                    }
                    orderModel.UsedLicences = usedLicenceCount;
                    orderModel.OrderStatusID = order.OrderStatusID;
                    //orderModel.OrderStatus = order.OrderStatusID.Value == 1 ? "Active" : "Cancelled";
                    ordersModelList.Add(orderModel);
                }
            }

            return ordersModelList;
        }

        //OrderLicenceType Service
        public OrderLicenceTypeModel GetOrderLicenceTypeByTypeGuid(Guid orderTypeGuid)
        {
            OrderLicenceType orderTypeEntity = Resolve<IOrderRepository>().GetOrderTypeByTypeGuid(orderTypeGuid);
            OrderLicenceTypeModel orderTypeModel = null;
            if (orderTypeEntity!=null)
            {
                orderTypeModel = new OrderLicenceTypeModel 
                {
                    OrderLicenceTypeGUID = orderTypeEntity.OrderLicenceTypeGUID,
                    TypeName = orderTypeEntity.Name,
                    TypeDescription = orderTypeEntity.Description
                };
            }
            return orderTypeModel;
        }

        public List<OrderLicenceTypeModel> GetAllOrderLicenceTypes()
        {
            OrderLicenceTypeModel orderTypeModel = null;
            List<OrderLicenceTypeModel> orderTypeModels = new List<OrderLicenceTypeModel>();
            List<OrderLicenceType> orderTypeEntitys = Resolve<IOrderRepository>().GetAllOrderTypes().OrderBy(ot=>ot.Order).ToList();
            foreach (OrderLicenceType orderTypeEntity in orderTypeEntitys)
            {
                orderTypeModel = new OrderLicenceTypeModel
                {
                    OrderLicenceTypeGUID = orderTypeEntity.OrderLicenceTypeGUID,
                    TypeName = orderTypeEntity.Name,
                    TypeDescription = orderTypeEntity.Description
                };
                orderTypeModels.Add(orderTypeModel);
            }

            return orderTypeModels;
        }
        
    }
}
