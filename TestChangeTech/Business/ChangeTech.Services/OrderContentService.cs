using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class OrderContentService : ServiceBase, IOrderContentService
    {
        private const string ORDERTYPE_OPENLICENCE_NAME = "Open licence";
        public void InsertOrderContentList(List<OrderContentModel> orderContents)
        {
            try
            {
                if (orderContents.Count > 0)
                {
                    foreach (OrderContentModel orderContentModel in orderContents)
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
                        Resolve<IOrderContentRepository>().Insert(orderContentEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, string.Format("Method Name {0}", System.Reflection.MethodBase.GetCurrentMethod().Name));
                throw ex;
            }
        }

        public int GetLicenceCountByOrderGuid(Guid orderGuid)
        {
            int licenceCount = 0;
            List<OrderContent> orderContentList = Resolve<IOrderContentRepository>().GetOrderContentsByOrderGuid(orderGuid).ToList();
            if (orderContentList.Count > 0)
            {
                foreach (OrderContent orderContent in orderContentList)
                {
                    licenceCount += orderContent.Licences.HasValue ? orderContent.Licences.Value : 0;
                }
            }
            return licenceCount;
        }

        public OrderContentModel GetOrderContentByOrderGuidAndProgramGuid(Guid orderGuid,Guid programGuid)
        {
            OrderContentModel orderContentModel = null;
            OrderContent orderContentEntity = Resolve<IOrderContentRepository>().GetOrderContentByOrderGuidAndProgramGuid(orderGuid, programGuid);
            if (orderContentEntity!=null)
            {
                orderContentModel = new OrderContentModel
                {
                    OrderContentGUID = orderContentEntity.OrderContentGUID,
                    OrderGUID = orderContentEntity.OrderGUID,
                    ProgramGUID = orderContentEntity.ProgramGUID,
                    ProgramLicences = orderContentEntity.Licences.Value,
                    UpdatedBy = orderContentEntity.UpdatedBy.Value
                };
            }
            return orderContentModel;
        }

        public List<OrderContentModel> GetOrderContentsByOrderGuid(Guid orderGuid)
        {
            List<OrderContentModel> orderContents = new List<OrderContentModel>();
            List<OrderContent> orderContentList = Resolve<IOrderContentRepository>().GetOrderContentsByOrderGuid(orderGuid).ToList();
            if (orderContentList.Count>0)
            {
                foreach (OrderContent orderContent in orderContentList)
                {
                    OrderContentModel orderContentModel = new OrderContentModel();
                    orderContentModel.OrderContentGUID = orderContent.OrderContentGUID != Guid.Empty ? orderContent.OrderContentGUID : Guid.Empty;
                    orderContentModel.OrderGUID = orderContent.OrderGUID != Guid.Empty ? orderContent.OrderGUID : Guid.Empty;
                    orderContentModel.ProgramGUID = orderContent.ProgramGUID != Guid.Empty ? orderContent.ProgramGUID : Guid.Empty;
                    orderContentModel.UpdatedBy = orderContent.UpdatedBy != Guid.Empty ? orderContent.UpdatedBy.Value : Guid.Empty;
                    orderContentModel.ProgramLicences = orderContent.Licences.HasValue ? orderContent.Licences.Value : 0;
                    if (orderContentModel.ProgramLicences == 0)
                    {
                        orderContentModel.Licences = ORDERTYPE_OPENLICENCE_NAME;
                    }
                    else
                    {
                        orderContentModel.Licences = orderContentModel.ProgramLicences.ToString() ;
                    }
                    List<OrderLicenceModel>orderLicences = Resolve<IOrderLicenceService>().GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(orderContent.OrderContentGUID, orderContent.ProgramGUID);
                    orderContentModel.UsedLicences=orderLicences.Count();

                    ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(orderContent.ProgramGUID);
                    orderContentModel.ProgramName = programModel.ProgramName;

                    OrderLicenceModel orderLicenceModel = Resolve<IOrderLicenceService>().GetLastRegistedUserByOrderContentGuidAndProgramGuid(orderContent.OrderContentGUID, orderContent.ProgramGUID);
                    if (orderLicenceModel != null)
                    {
                        orderContentModel.LastRegisted = orderLicenceModel.LastRegisted;
                    }
                    
                    orderContents.Add(orderContentModel);
                }
            }

            return orderContents;
        }

        public OrderContentModel GetOrderContentByOrderContentGuid(Guid orderContentGuid)
        {
            OrderContent orderContentEntity = Resolve<IOrderContentRepository>().GetOrderContentByOrderContentGuid(orderContentGuid);
            OrderContentModel orderContentModel = new OrderContentModel();
            if (orderContentEntity != null)
            {
                orderContentModel.OrderContentGUID = orderContentEntity.OrderContentGUID;
                orderContentModel.OrderGUID = orderContentEntity.OrderGUID;
                orderContentModel.ProgramGUID = orderContentEntity.ProgramGUID;
                orderContentModel.ProgramLicences = orderContentEntity.Licences.Value;
                if (!orderContentEntity.ProgramReference.IsLoaded) orderContentEntity.ProgramReference.Load();
                orderContentModel.ProgramName = orderContentEntity.Program.Name;
                orderContentModel.UpdatedBy = orderContentEntity.UpdatedBy.Value;
                OrderLicence orderLicenceEntity = Resolve<IOrderLicenceRepository>().GetLastRegistedUserByOrderContentGuidAndProgramGuid(orderContentGuid, orderContentEntity.ProgramGUID);
                if (orderLicenceEntity != null)
                {
                    if (!orderLicenceEntity.ProgramUserReference.IsLoaded) orderLicenceEntity.ProgramUserReference.Load();
                    orderContentModel.LastRegisted = orderLicenceEntity.ProgramUser.StartDate.HasValue ? orderLicenceEntity.ProgramUser.StartDate.Value.ToString("dd.MM.yyyy") : string.Empty;
                }
                orderContentModel.UsedLicences = Resolve<IOrderLicenceService>().GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(orderContentGuid, orderContentEntity.ProgramGUID).Count();
            }

            return orderContentModel;
        }
    }
}
