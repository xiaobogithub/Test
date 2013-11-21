using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.Models;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class HPOrderEmailService : ServiceBase, IHPOrderEmailService
    {
        public static readonly string EMAILTEMPLATETYEP_HP_ORDER = "HP Order Email";
        public static readonly string HPORDEREMAIL_TO_ADDRESS = "HPOrderEmailToAddress";
        public static readonly string HPORDEREMAIL_SUBJECT = "HPOrderEmailSubject";
        public void InsertHPOrderEmail(Models.HPOrderModel orderModel)
        {
            Guid HPORDEREMAILTEMPLATEGUID = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeGuidByName(EMAILTEMPLATETYEP_HP_ORDER);
            List<HPOrderEmailIntervalTime> intervalTimes = Resolve<IHPOrderEmailRepository>().GetIntervalTimesByEmailTemplateTypeGuid(HPORDEREMAILTEMPLATEGUID).ToList();
            foreach (var intervalTimeEntity in intervalTimes)
            {
                HPOrderEmail hpOrderEmailEntity = new HPOrderEmail()
                {
                    HPOrderEmailGUID = Guid.NewGuid(),
                    HPContactEmail = orderModel.SOSIContactEmail,
                    ProgramGUID = orderModel.ProgramGUID,
                    HPOrderGUID = orderModel.HPOrderGUID,
                    LogType = (int)LogTypeEnum.SendEmailToCustomer,
                    HPEmailSubject = Resolve<ISystemSettingRepository>().GetSettingValue(HPORDEREMAIL_SUBJECT),
                    EmailTemplateTypeGUID = HPORDEREMAILTEMPLATEGUID
                };
                //hpOrderEmailEntity.HPEmailBody = GetHPOrderEmailBodyContent(orderModel);
                hpOrderEmailEntity.HPEmailSendDate = orderModel.StartDate.Date.AddDays(intervalTimeEntity.IntervalTime.Value);

                Resolve<IHPOrderEmailRepository>().Insert(hpOrderEmailEntity);
            }
            //insert last email according hp-order's stopdate,send email to 'support@changetech.no' 
            HPOrderEmail hpOrderLastEmailEntity = new HPOrderEmail()
            {
                HPOrderEmailGUID = Guid.NewGuid(),
                HPContactEmail = Resolve<ISystemSettingRepository>().GetSettingValue(HPORDEREMAIL_TO_ADDRESS),
                ProgramGUID = orderModel.ProgramGUID,
                HPOrderGUID = orderModel.HPOrderGUID,
                LogType = (int)LogTypeEnum.SendEmailToCustomer,
                HPEmailSubject = Resolve<ISystemSettingRepository>().GetSettingValue("HPOrderEmailSubject"),
                EmailTemplateTypeGUID = HPORDEREMAILTEMPLATEGUID
            };
            hpOrderLastEmailEntity.HPEmailSendDate = orderModel.StopDate.Date;

            Resolve<IHPOrderEmailRepository>().Insert(hpOrderLastEmailEntity);
        }

        public void Update(Models.HPOrderEmailModel orderEmail)
        {
            HPOrderEmail orderEmailEntity = Resolve<IHPOrderEmailRepository>().GetOrderEmailByHPOrderEmailGuid(orderEmail.HPOrderEmailGUID);
            if (orderEmailEntity != null)
            {
                orderEmailEntity.HPEmailBody = orderEmail.HPEmailBody;
                orderEmailEntity.IsSend = orderEmail.IsSend;
                Resolve<IHPOrderEmailRepository>().Update(orderEmailEntity);
            }

        }

        public void Delete(Guid orderEmailGuid)
        {
            throw new NotImplementedException();
        }

        public List<Models.HPOrderEmailModel> GetOrderEmailsByHPOrderGuid(Guid orderGuid)
        {
            throw new NotImplementedException();
        }

        public List<Models.HPOrderEmailModel> GetOrderEmailsByCurrentDate()
        {
            IQueryable<HPOrderEmail> hpOrderEmailList = Resolve<IHPOrderEmailRepository>().GetOrderEmails();

            List<HPOrderEmailModel> hpOrderEmailModels = new List<HPOrderEmailModel>();
            foreach (var hpOrderEmailEntity in hpOrderEmailList)
            {
                if (hpOrderEmailEntity.HPEmailSendDate.Value.Date == DateTime.UtcNow.Date)
                {
                    HPOrderEmailModel orderEmailModel = new HPOrderEmailModel
                    {
                        HPContactEmail = hpOrderEmailEntity.HPContactEmail,
                        HPEmailBody = hpOrderEmailEntity.HPEmailBody,
                        HPEmailSubject = hpOrderEmailEntity.HPEmailSubject,
                        HPOrderEmailGUID = hpOrderEmailEntity.HPOrderEmailGUID,
                        HPOrderGUID = hpOrderEmailEntity.HPOrderGUID,
                        ProgramGUID = hpOrderEmailEntity.ProgramGUID.Value,
                        LogType = hpOrderEmailEntity.LogType.Value,
                        HPEmailSendDate = hpOrderEmailEntity.HPEmailSendDate.Value
                    };
                    hpOrderEmailModels.Add(orderEmailModel);
                }
            }

            return hpOrderEmailModels;
        }

        public string GetHPOrderEmailBodyContent(HPOrderModel orderModel)
        {
            //<html><body style="font-size: 12px; font-family:Arial"><br/>Customer name: {CustomerName}<br />Contact person name: {ContactPersonName}<br />Contact person number: {ContactPersonNumber}<br />Contact person phone: {ContactPersonPhone}<br />Contact person email: {ContactPersonEmail}<br />Period: {StartDate} - {StopDate}<br />Completed Health Profiles: {NumberOfCompleted}<br />Number of users: {NumberOfUsers}</body></html>
            string body = string.Empty;
            int numberOfCompleted = Resolve<IHPOrderLicenceRepository>().GetUsedOrderLicencesByHPOrderGuid(orderModel.HPOrderGUID).Count();
            Guid HPORDEREMAILTEMPLATEGUID = Resolve<IEmailTemplateTypeRepository>().GetEmailTemplateTypeGuidByName(EMAILTEMPLATETYEP_HP_ORDER);
            if (HPORDEREMAILTEMPLATEGUID != Guid.Empty && HPORDEREMAILTEMPLATEGUID != null)
            {
                EmailTemplateTypeContent emailTemplateContentEntity = Resolve<IEmailTemplateRepository>().GetEmailTemplateTypeContent(HPORDEREMAILTEMPLATEGUID);
                if (emailTemplateContentEntity != null)
                {
                    body = emailTemplateContentEntity.HtmlContent.ToString();
                    body = body.Replace("{CustomerName}", orderModel.CustomerName)
                        .Replace("{ContactPersonName}", orderModel.ContactPersonName)
                        .Replace("{ContactPersonNumber}", orderModel.ContactPersonNumber)
                        .Replace("{ContactPersonPhone}", orderModel.ContactPersonPhone)
                        .Replace("{ContactPersonEmail}", orderModel.ContactPersonEmail)
                        .Replace("{StartDate}", orderModel.StartDate.ToShortDateString())
                        .Replace("{StopDate}", orderModel.StopDate.ToShortDateString())
                        .Replace("{NumberOfCompleted}", numberOfCompleted.ToString())
                        .Replace("{NumberOfUsers}", orderModel.NumberOfEmployees.ToString());
                }
            }

            return body;
        }

    }
}