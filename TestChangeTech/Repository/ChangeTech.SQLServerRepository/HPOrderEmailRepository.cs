using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class HPOrderEmailRepository : RepositoryBase, IHPOrderEmailRepository
    {
        public void Insert(HPOrderEmail orderEmail)
        {
            InsertEntity(orderEmail);
        }

        public void Update(HPOrderEmail orderEmail)
        {
            UpdateEntity(orderEmail);
        }

        public void Delete(Guid orderEmailGuid)
        {
            DeleteEntity<HPOrderEmail>(om => om.HPOrderEmailGUID == orderEmailGuid, new Guid());
        }

        public HPOrderEmail GetOrderEmailByHPOrderEmailGuid(Guid orderEmailGuid)
        {
            return GetEntities<HPOrderEmail>().Where(om => om.HPOrderEmailGUID == orderEmailGuid && (!om.IsSend.HasValue || (om.IsSend.HasValue && om.IsSend.Value != true))).FirstOrDefault();
        }

        public HPOrderEmail GetOrderEmailByHPOrderGuidAndHPContactEmail(Guid orderGuid, string contactEmail)
        {
            return GetEntities<HPOrderEmail>().Where(om => om.HPOrderGUID == orderGuid && om.HPContactEmail == contactEmail).FirstOrDefault();
        }

        public IQueryable<HPOrderEmail> GetAllOrderEmailsByHPOrderGuidAndHPContactEmail(Guid orderGuid, string contactEmail)
        {
            return GetEntities<HPOrderEmail>().Where(om => om.HPOrderGUID == orderGuid && om.HPContactEmail == contactEmail).OrderBy(om => om.HPEmailSendDate);//&& (!om.IsSend.HasValue || (om.IsSend.HasValue && om.IsSend.Value != true))
        }

        public IQueryable<HPOrderEmail> GetAllOrderEmailsByHPOrderGuid(Guid orderGuid)
        {
            return GetEntities<HPOrderEmail>().Where(om => om.HPOrderGUID == orderGuid).OrderBy(om => om.HPEmailSendDate);//&& (!om.IsSend.HasValue || (om.IsSend.HasValue && om.IsSend.Value != true))
        }

        public IQueryable<HPOrderEmail> GetNotSendOrderEmailsByHPOrderGuid(Guid orderGuid)
        {
            return GetEntities<HPOrderEmail>().Where(om => om.HPOrderGUID == orderGuid && (!om.IsSend.HasValue || (om.IsSend.HasValue && om.IsSend.Value != true)));
        }

        public IQueryable<HPOrderEmail> GetOrderEmails()
        {
            return GetEntities<HPOrderEmail>().Where(om => (!om.IsSend.HasValue || (om.IsSend.HasValue && om.IsSend.Value != true)));
        }

        public IQueryable<HPOrderEmailIntervalTime> GetIntervalTimesByEmailTemplateTypeGuid(Guid emailTemplateTypeGUID)
        {
            return GetEntities<HPOrderEmailIntervalTime>().Where(t => t.EmailTemplateTypeGUID == emailTemplateTypeGUID);
        }
    }
}