using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using Ethos.Utility;
using System.Globalization;

namespace ChangeTech.Services
{
    public class OrderLicenceService : ServiceBase, IOrderLicenceService
    {
        private const string OPENLICENCEGUID = "A0F8FD76-8DA3-4592-83FB-339D4BF5419F";
        private const string SIMPLELICENCEGUID = "C1A3BC0F-07C1-42E2-86CC-635186DE8837";
        public void InsertOrderLicence(OrderLicenceModel orderLicenceModel)
        {
            try
            {
                OrderLicence orderLicenceEntity = new OrderLicence()
                {
                    OrderLicenceGUID = orderLicenceModel.OrderLicenceGUID,
                    OrderContentGUID = orderLicenceModel.OrderContentGUID.Value,
                    ProgramUserGUID = orderLicenceModel.ProgramUserGUID.Value,
                    //PromotionCode = orderLicenceModel.PromotionCode,
                    UpdatedBy = orderLicenceModel.UpdatedBy,
                    Updated = DateTime.UtcNow,
                    IsDeleted = null
                };
                Resolve<IOrderLicenceRepository>().Insert(orderLicenceEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}, OrderLicenceGUID {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, orderLicenceModel.OrderContentGUID));
                throw ex;
            }
        }

        public void UpdateOrderLicence(OrderLicenceModel orderLicenceModel)
        {
            OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetOrderLicenceByOrderLicenceGuid(orderLicenceModel.OrderLicenceGUID);
            if (!orderLicenceEntity.OrderContentReference.IsLoaded) orderLicenceEntity.OrderContentReference.Load();
            orderLicenceEntity.OrderContentGUID =orderLicenceModel.OrderContentGUID.HasValue? orderLicenceModel.OrderContentGUID.Value :Guid.Empty;
            orderLicenceEntity.ProgramUserGUID = orderLicenceModel.OrderContentGUID.HasValue ? orderLicenceModel.OrderContentGUID.Value : Guid.Empty;
           // if(!string.IsNullOrEmpty( orderLicenceModel.PromotionCode)) orderLicenceEntity.PromotionCode=orderLicenceModel.PromotionCode;
            if(orderLicenceModel.UpdatedBy!=Guid.Empty) orderLicenceEntity.UpdatedBy=orderLicenceModel.UpdatedBy;
            try
            {
                Resolve<IOrderLicenceRepository>().Update(orderLicenceEntity);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}, OrderLicenceGUID {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, orderLicenceModel.OrderContentGUID));
                throw ex;
            }
        }

        public int GetUsedOrderLicencesByResellerGuidAndOrderContentGuid(Guid resellerGuid, Guid orderContentGuid)
        {
            return Resolve<IOrderLicenceRepository>().GetUsedOrderLicencesByResellerGuidAndOrderContentGuid(resellerGuid, orderContentGuid).Count();
        }

        public OrderLicenceModel GetOrderLicenceByOrderContentGuid(Guid orderContentGuid)
        {
            OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetOrderLicenceByOrderContentGuid(orderContentGuid);
            OrderLicenceModel orderLicenceModel = null;
            if (orderLicenceEntity!=null)
            {
                orderLicenceModel = new OrderLicenceModel();
                orderLicenceModel.OrderLicenceGUID = orderLicenceEntity.OrderLicenceGUID;
                orderLicenceModel.OrderContentGUID = orderLicenceEntity.OrderContentGUID.HasValue ? orderLicenceEntity.OrderContentGUID.Value : Guid.Empty;
                orderLicenceModel.ProgramUserGUID = orderLicenceEntity.ProgramUserGUID.HasValue ? orderLicenceEntity.ProgramUserGUID.Value : Guid.Empty;
                //orderLicenceModel.PromotionCode = string.IsNullOrEmpty(orderLicenceEntity.PromotionCode) ? null : orderLicenceEntity.PromotionCode;
                 orderLicenceModel.UpdatedBy = orderLicenceEntity.UpdatedBy.Value;
            }

            return orderLicenceModel;
        }

        public OrderLicenceModel GetOrderLicenceByProgramUserGuid(Guid programUserGuid)
        {
            OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetOrderLicenceByProgramUserGuid(programUserGuid);
            OrderLicenceModel orderLicenceModel = null;
            if (orderLicenceEntity != null)
            {
                orderLicenceModel = new OrderLicenceModel();
                orderLicenceModel.OrderLicenceGUID = orderLicenceEntity.OrderLicenceGUID;
                orderLicenceModel.OrderContentGUID = orderLicenceEntity.OrderContentGUID.HasValue ? orderLicenceEntity.OrderContentGUID.Value : Guid.Empty;
                orderLicenceModel.ProgramUserGUID = orderLicenceEntity.ProgramUserGUID.HasValue ? orderLicenceEntity.ProgramUserGUID.Value : Guid.Empty;
                //orderLicenceModel.PromotionCode = string.IsNullOrEmpty(orderLicenceEntity.PromotionCode) ? null : orderLicenceEntity.PromotionCode;
                 orderLicenceModel.UpdatedBy = orderLicenceEntity.UpdatedBy.Value;
            }

            return orderLicenceModel;
        }

        public OrderLicenceModel GetOrderLicenceByOrderContentGuidAndProgramUserGuid(Guid orderContentGuid, Guid puGuid)
        {
            OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetOrderLicenceByOrderContentGuidAndProgramUserGuid(orderContentGuid, puGuid);
            OrderLicenceModel orderLicenceModel = null;
            if (orderLicenceEntity!=null)
            {
                orderLicenceModel = new OrderLicenceModel();
                orderLicenceModel.OrderLicenceGUID = orderLicenceEntity.OrderLicenceGUID;
                orderLicenceModel.OrderContentGUID = orderLicenceEntity.OrderContentGUID.HasValue ? orderLicenceEntity.OrderContentGUID.Value : Guid.Empty;
                orderLicenceModel.ProgramUserGUID = orderLicenceEntity.ProgramUserGUID.HasValue ? orderLicenceEntity.ProgramUserGUID.Value : Guid.Empty;
                //orderLicenceModel.PromotionCode = string.IsNullOrEmpty(orderLicenceEntity.PromotionCode) ? null : orderLicenceEntity.PromotionCode;
                if (orderLicenceEntity.UpdatedBy != Guid.Empty) orderLicenceModel.UpdatedBy = orderLicenceEntity.UpdatedBy.Value;
            }

            return orderLicenceModel;
        }

        public List<OrderLicenceModel> GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid)
        {
            List<OrderLicenceModel> orderLicences = new List<OrderLicenceModel>();
            List<OrderLicence> orderLicenceList = Resolve<IOrderLicenceRepository>().GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(orderContentGuid, programGuid).ToList();
            if (orderLicenceList.Count >0)
            {
                foreach (OrderLicence orderLicence in orderLicenceList)
                {
                    OrderLicenceModel orderLicenceModel = new OrderLicenceModel();
                    orderLicenceModel.OrderLicenceGUID = orderLicence.OrderLicenceGUID;
                    orderLicenceModel.ProgramUserGUID = orderLicence.ProgramUserGUID.HasValue ? orderLicence.ProgramUserGUID.Value : Guid.Empty;
                    orderLicenceModel.OrderContentGUID = orderLicence.OrderContentGUID.HasValue ? orderLicence.OrderContentGUID.Value : Guid.Empty;
                    //orderLicenceModel.PromotionCode = !string.IsNullOrEmpty(orderLicence.PromotionCode) ? orderLicence.PromotionCode : string.Empty;
                    orderLicenceModel.UpdatedBy = orderLicence.UpdatedBy.HasValue ? orderLicence.UpdatedBy.Value : Guid.Empty;
                    OrderLicenceModel olModel = orderLicences.Where(ol => ol.ProgramUserGUID == orderLicenceModel.ProgramUserGUID).FirstOrDefault();
                    if (olModel == null)
                    {
                        orderLicences.Add(orderLicenceModel);
                    }
                }
            }

            return orderLicences;
        }
        
        public OrderLicenceModel GetLastRegistedUserByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid)
        {
            OrderLicenceModel orderLicenceModel = new OrderLicenceModel();
            OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetLastRegistedUserByOrderContentGuidAndProgramGuid(orderContentGuid, programGuid);
            if (orderLicenceEntity != null)
            {
                orderLicenceModel.OrderLicenceGUID = orderLicenceEntity.OrderLicenceGUID;
                orderLicenceModel.OrderContentGUID = orderLicenceEntity.OrderContentGUID.HasValue ? orderLicenceEntity.OrderContentGUID.Value : Guid.Empty;
                orderLicenceModel.ProgramUserGUID = orderLicenceEntity.ProgramUserGUID.HasValue ? orderLicenceEntity.ProgramUserGUID.Value : Guid.Empty;
                //orderLicenceModel.PromotionCode = !string.IsNullOrEmpty(orderLicenceEntity.PromotionCode) ? orderLicenceEntity.PromotionCode : string.Empty;
                orderLicenceModel.UpdatedBy = orderLicenceEntity.UpdatedBy.HasValue ? orderLicenceEntity.UpdatedBy.Value : Guid.Empty;
                if (!orderLicenceEntity.ProgramUserReference.IsLoaded) orderLicenceEntity.ProgramUserReference.Load();
                orderLicenceModel.LastRegisted = orderLicenceEntity.ProgramUser.StartDate.HasValue ? orderLicenceEntity.ProgramUser.StartDate.Value.ToString("dd.MM.yyyy") : string.Empty;
            }
            return orderLicenceModel;
        }

        public ValidateOrderLicenceResponseModel ValidateOrderLicence(Guid orderContentGuid, Guid programGuid)
        {
            ValidateOrderLicenceResponseModel responseModel = new ValidateOrderLicenceResponseModel();
            try
            {
                OrderContentModel orderContentModel = Resolve<IOrderContentService>().GetOrderContentByOrderContentGuid(orderContentGuid);
                OrderModel orderModel = Resolve<IOrderService>().GetOrderByOrderGuid(orderContentModel.OrderGUID);
                int usedProgramLicences = (Resolve<IOrderLicenceService>().GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(orderContentModel.OrderContentGUID, orderContentModel.ProgramGUID)).Count;
                if (programGuid == orderContentModel.ProgramGUID)
                {
                    if (orderModel.ExpiredDate.Value > DateTime.UtcNow.Date || orderModel.OrderStatusID == (int)OrderStatusEnum.Expired)
                    {
                        if (orderModel.OrderStatusID == (int)OrderStatusEnum.Active)
                        {
                            OrderLicenceTypeModel orderTypeModel = Resolve<IOrderService>().GetOrderLicenceTypeByTypeGuid(orderModel.LicenceTypeGUID);
                            if (orderTypeModel.OrderLicenceTypeGUID.ToString().ToUpper() == OPENLICENCEGUID)
                            {
                                responseModel.Result = ResultEnum.Success;
                            }
                            else
                            {
                                if (usedProgramLicences + 1 <= orderContentModel.ProgramLicences)
                                {
                                    responseModel.Result = ResultEnum.Success;
                                }
                                else
                                {
                                    // has't ProgramLicence 
                                    responseModel.Result = ResultEnum.Error;
                                    responseModel.LoginFailedType = LoginFailedTypeEnum.NoOrderLicence;
                                }
                            }
                        }
                        else
                        {
                            // Order Cancelled 
                            responseModel.Result = ResultEnum.Error;
                            responseModel.LoginFailedType = LoginFailedTypeEnum.OrderHasCancelled;
                        }
                    }
                    else
                    {
                        //Order Expired
                        responseModel.Result = ResultEnum.Error;
                        responseModel.LoginFailedType = LoginFailedTypeEnum.OrderHasExpired;
                    }
                }
                else
                {
                    //Program don't in current Order .
                    responseModel.Result = ResultEnum.Error;
                    responseModel.LoginFailedType = LoginFailedTypeEnum.NoOrderLicence;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}", System.Reflection.MethodBase.GetCurrentMethod().Name));
                throw ex;
            }

            return responseModel;
        }
    }
}
