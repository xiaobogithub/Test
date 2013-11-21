using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class HPOrderService : ServiceBase, IHPOrderService
    {
        #region Public Event
        public static readonly string MD5_KEY = "psycholo";
        public static readonly string EMAILTEMPLATETYEP_HP_ORDER = "HP Order Email";
        public static readonly string HPORDEREMAIL_TO_ADDRESS = "HPOrderEmailToAddress";
        public void InsertHPOrder(HPOrderModel order)
        {
            string orderCode = GenerateHPOrderCode();
            HPOrder hpOrderEntity = new HPOrder
            {
                HPOrderGUID = order.HPOrderGUID,
                HPProgramGUID = order.ProgramGUID,
                HPOrderStatusID = order.OrderStatus,
                CustomerName = order.CustomerName,
                ContactPersonEmail = order.ContactPersonEmail,
                ContactPersonName = order.ContactPersonName,
                ContactPersonNumber = order.ContactPersonNumber,
                SOSIContactEmail = order.SOSIContactEmail,
                Code = orderCode,
                ContactPersonPhone = order.ContactPersonPhone,
                NumberOfEmployees = order.NumberOfEmployees,
                LocationID = order.LocationID,
                IndustryID = order.IndustryID,
                StartDate = order.StartDate,
                StopDate = order.StopDate,
                UpdatedBy = order.UpdatedBy
            };
            hpOrderEntity.LinkToStartPageURL = GeneralLinkToStartPageURL(orderCode, order.ProgramGUID);
            hpOrderEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(order.ProgramGUID);

            Resolve<IHPOrderRepository>().Insert(hpOrderEntity);
        }

        public void UpdateHPOrder(HPOrderModel order)
        {
            HPOrder hpOrderEntity = Resolve<IHPOrderRepository>().GetHPOrderByOrderGuid(order.HPOrderGUID);

            if (hpOrderEntity.HPProgramGUID != order.ProgramGUID)
            {
                hpOrderEntity.HPProgramGUID = order.ProgramGUID;
                hpOrderEntity.Program = Resolve<IProgramRepository>().GetProgramByGuid(order.ProgramGUID);
            }
            if (hpOrderEntity.StartDate != order.StartDate || hpOrderEntity.StopDate != order.StopDate)
            {
                List<HPOrderEmail> orderEmails = Resolve<IHPOrderEmailRepository>().GetAllOrderEmailsByHPOrderGuid(order.HPOrderGUID).ToList();
                foreach (HPOrderEmail orderEmail in orderEmails)
                {
                    Resolve<IHPOrderEmailRepository>().Delete(orderEmail.HPOrderEmailGUID);
                }
                Resolve<IHPOrderEmailService>().InsertHPOrderEmail(order);
                hpOrderEntity.StartDate = order.StartDate;
                hpOrderEntity.StopDate = order.StopDate;
            }
            if (hpOrderEntity.SOSIContactEmail != order.SOSIContactEmail)
            {
                //update HPOrderEmail send date
                List<HPOrderEmail> orderEmailList = Resolve<IHPOrderEmailRepository>().GetAllOrderEmailsByHPOrderGuidAndHPContactEmail(order.HPOrderGUID, hpOrderEntity.SOSIContactEmail).ToList();
                foreach (HPOrderEmail orderEmail in orderEmailList)
                {
                    orderEmail.HPContactEmail = order.SOSIContactEmail;
                    Resolve<IHPOrderEmailRepository>().Update(orderEmail);
                }
                hpOrderEntity.SOSIContactEmail = order.SOSIContactEmail;
            }
            if (hpOrderEntity.HPOrderStatusID != order.OrderStatus)
            {
                hpOrderEntity.HPOrderStatusID = order.OrderStatus;
            }
            if (hpOrderEntity.CustomerName != order.CustomerName)
            {
                hpOrderEntity.CustomerName = order.CustomerName;
            }
            if (hpOrderEntity.ContactPersonEmail != order.ContactPersonEmail)
            {
                hpOrderEntity.ContactPersonEmail = order.ContactPersonEmail;
            }
            if (hpOrderEntity.ContactPersonName != order.ContactPersonName)
            {
                hpOrderEntity.ContactPersonName = order.ContactPersonName;
            }
            if (hpOrderEntity.ContactPersonNumber != order.ContactPersonNumber)
            {
                hpOrderEntity.ContactPersonNumber = order.ContactPersonNumber;
            }
            if (hpOrderEntity.ContactPersonPhone != order.ContactPersonPhone)
            {
                hpOrderEntity.ContactPersonPhone = order.ContactPersonPhone;
            }
            if (hpOrderEntity.NumberOfEmployees != order.NumberOfEmployees)
            {
                hpOrderEntity.NumberOfEmployees = order.NumberOfEmployees;
            }
            if (hpOrderEntity.LinkToStartPageURL != order.LinkToStartPageURL)
            {
                hpOrderEntity.LinkToStartPageURL = order.LinkToStartPageURL;
            }
            if (hpOrderEntity.LocationID != order.LocationID)
            {
                hpOrderEntity.LocationID = order.LocationID;
            }
            if (hpOrderEntity.IndustryID != order.IndustryID)
            {
                hpOrderEntity.IndustryID = order.IndustryID;
            }
            if (hpOrderEntity.UpdatedBy != order.UpdatedBy)
            {
                hpOrderEntity.UpdatedBy = order.UpdatedBy;
            }

            Resolve<IHPOrderRepository>().Update(hpOrderEntity);
        }


        public List<HPOrderModel> GetHPOrders()
        {
            List<HPOrder> hpOrders = Resolve<IHPOrderRepository>().GetOrders().OrderBy(hp => hp.Created).ToList();
            List<HPOrderModel> hpOrdersModel = new List<HPOrderModel>();
            foreach (var order in hpOrders)
            {
                HPOrderModel hpOrderModel = new HPOrderModel
                {
                    HPOrderGUID = order.HPOrderGUID,
                    ProgramGUID = order.HPProgramGUID,
                    OrderStatus = order.HPOrderStatusID,
                    CustomerName = order.CustomerName,
                    ContactPersonEmail = order.ContactPersonEmail,
                    ContactPersonName = order.ContactPersonName,
                    ContactPersonNumber = order.ContactPersonNumber,
                    ContactPersonPhone = order.ContactPersonPhone,
                    SOSIContactEmail = order.SOSIContactEmail,
                    NumberOfEmployees = order.NumberOfEmployees.Value,
                    LinkToStartPageURL = order.LinkToStartPageURL,
                    LocationID = order.LocationID.Value,
                    IndustryID = order.IndustryID.Value,
                    Created = order.Created.Value,
                    StartDate = order.StartDate.Value,
                    StopDate = order.StopDate.Value,
                    Code = order.Code,
                    UpdatedBy = order.UpdatedBy.Value
                };
                hpOrderModel.UsedLicence = Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(hpOrderModel.HPOrderGUID).Count();
                hpOrdersModel.Add(hpOrderModel);
            }

            return hpOrdersModel;
        }

        public HPOrderModel GetHPOrderModelByHPOrderGuid(Guid hpOrderGuid)
        {
            HPOrder hpOrder = Resolve<IHPOrderRepository>().GetHPOrderByOrderGuid(hpOrderGuid);
            HPOrderModel hpOrderModel = null;
            if (hpOrder != null)
            {
                hpOrderModel = new HPOrderModel
                {
                    HPOrderGUID = hpOrder.HPOrderGUID,
                    ProgramGUID = hpOrder.HPProgramGUID,
                    OrderStatus = hpOrder.HPOrderStatusID,
                    CustomerName = hpOrder.CustomerName,
                    ContactPersonEmail = hpOrder.ContactPersonEmail,
                    SOSIContactEmail = hpOrder.SOSIContactEmail,
                    ContactPersonName = hpOrder.ContactPersonName,
                    ContactPersonNumber = hpOrder.ContactPersonNumber,
                    ContactPersonPhone = hpOrder.ContactPersonPhone,
                    NumberOfEmployees = hpOrder.NumberOfEmployees.Value,
                    LinkToStartPageURL = hpOrder.LinkToStartPageURL,
                    LocationID = hpOrder.LocationID.Value,
                    IndustryID = hpOrder.IndustryID.Value,
                    Created = hpOrder.Created.Value,
                    StartDate = hpOrder.StartDate.Value,
                    StopDate = hpOrder.StopDate.Value,
                    Code = hpOrder.Code,
                    UpdatedBy = hpOrder.UpdatedBy.Value
                };
                hpOrderModel.UsedLicence = Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(hpOrderModel.HPOrderGUID).Count();
            }

            return hpOrderModel;
        }

        public HPOrderModel GetHPOrderModelByCode(string code)
        {
            HPOrder hpOrder = Resolve<IHPOrderRepository>().GetHPOrderByOrderCode(code);
            HPOrderModel hpOrderModel = null;
            if (hpOrder != null)
            {
                hpOrderModel = new HPOrderModel
                {
                    HPOrderGUID = hpOrder.HPOrderGUID,
                    ProgramGUID = hpOrder.HPProgramGUID,
                    OrderStatus = hpOrder.HPOrderStatusID,
                    CustomerName = hpOrder.CustomerName,
                    ContactPersonEmail = hpOrder.ContactPersonEmail,
                    ContactPersonName = hpOrder.ContactPersonName,
                    ContactPersonNumber = hpOrder.ContactPersonNumber,
                    ContactPersonPhone = hpOrder.ContactPersonPhone,
                    SOSIContactEmail = hpOrder.SOSIContactEmail,
                    NumberOfEmployees = hpOrder.NumberOfEmployees.Value,
                    LinkToStartPageURL = hpOrder.LinkToStartPageURL,
                    LocationID = hpOrder.LocationID.Value,
                    IndustryID = hpOrder.IndustryID.Value,
                    Created = hpOrder.Created.Value,
                    StartDate = hpOrder.StartDate.Value,
                    StopDate = hpOrder.StopDate.Value,
                    Code = hpOrder.Code,
                    UpdatedBy = hpOrder.UpdatedBy.Value
                };
                hpOrderModel.UsedLicence = Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(hpOrderModel.HPOrderGUID).Count();
            }

            return hpOrderModel;
        }

        public List<HPOrderParamModel> GetHPOrderParamsByType(string type)
        {
            List<HPOrderParam> hpOrderParamEntityList = Resolve<IHPOrderRepository>().GetHPOrderParamsByType(type).ToList();
            List<HPOrderParamModel> hpOrderParamModelList = new List<HPOrderParamModel>();
            HPOrderParamModel hpOrderParamModel = null;
            foreach (var hpOrderParamEntity in hpOrderParamEntityList)
            {
                hpOrderParamModel = new HPOrderParamModel
                {
                    HPOrderParamID = hpOrderParamEntity.HPOrderParamID,
                    HPOrderParamName = hpOrderParamEntity.HPOrderParamName,
                    HPOrderParamType = hpOrderParamEntity.HPOrderParamType
                };
                hpOrderParamModelList.Add(hpOrderParamModel);
            }

            return hpOrderParamModelList;
        }

        public HPOrderParamModel GetHPOrderParamByParamName(string paramName)
        {
            HPOrderParam hpOrderParamEntity = Resolve<IHPOrderRepository>().GetHPOrderParamByParamName(paramName);
            HPOrderParamModel hpOrderParamModel = null;
            if (hpOrderParamEntity != null)
            {
                hpOrderParamModel = new HPOrderParamModel
                {
                    HPOrderParamID = hpOrderParamEntity.HPOrderParamID,
                    HPOrderParamName = hpOrderParamEntity.HPOrderParamName,
                    HPOrderParamType = hpOrderParamEntity.HPOrderParamType
                };
            }

            return hpOrderParamModel;
        }

        public string GetHPProgramLink(HPOrderModel orderModel)
        {
            string programLink = string.Empty;
            string serverPath = Resolve<IEmailService>().GetServerPath(orderModel.ProgramGUID);
            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(orderModel.ProgramGUID);

            string securityStr = StringUtility.MD5Encrypt(string.Format("{0};{1};{2};{3}", "", "", UserTaskTypeEnum.RegisteOfHPOrderSystem, orderModel.HPOrderGUID), MD5_KEY);
            if (programModel != null && !string.IsNullOrEmpty(programModel.Code))
            {
                programLink = string.Format("{0}ChangeTech.html?P={1}&Mode=Trial&HpSecurity={2}", serverPath, programModel.Code, securityStr);
            }
            return programLink;
        }

        #endregion

        #region Private Event
        private string GenerateHPOrderCode()
        {
            string code = string.Empty;
            do
            {
                code = Ethos.Utility.StringUtility.GenerateCheckLetterCode(3, false);
            }
            while (IsHPOrderCodeExisted(code));

            return code;
        }

        private bool IsHPOrderCodeExisted(string code)
        {
            bool flag = false;
            if (Resolve<IHPOrderRepository>().GetHPOrderByOrderCode(code) != null)
            {
                flag = true;
            }
            return flag;
        }

        private string GeneralLinkToStartPageURL(string code, Guid programGuid)
        {
            //http://localhost:41265/HealthProfileSystem/StartHealthProfile.aspx?code=BJV2Z
            string linkToStartPageUrl = string.Empty;
            string serverPath = Resolve<IEmailService>().GetServerPath(programGuid);
            linkToStartPageUrl = serverPath + "HealthProfileSystem/halsoprofil-setup-independent.aspx?code=" + code;// string.Format("{0}HealthProfileSystem/StartHealthProfile.aspx?code={2}", serverPath, code);

            return linkToStartPageUrl;
        }
        #endregion
    }
}