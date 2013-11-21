using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IHPOrderEmailRepository
    {
        void Insert(HPOrderEmail orderEmail);
        void Update(HPOrderEmail orderEmail);
        void Delete(Guid orderEmailGuid);
        HPOrderEmail GetOrderEmailByHPOrderEmailGuid(Guid orderEmailGuid);
        HPOrderEmail GetOrderEmailByHPOrderGuidAndHPContactEmail(Guid orderGuid, string contactEmail);
        IQueryable<HPOrderEmail> GetAllOrderEmailsByHPOrderGuidAndHPContactEmail(Guid orderGuid, string contactEmail);
        IQueryable<HPOrderEmail> GetAllOrderEmailsByHPOrderGuid(Guid orderGuid);
        IQueryable<HPOrderEmail> GetNotSendOrderEmailsByHPOrderGuid(Guid orderGuid);
        IQueryable<HPOrderEmail> GetOrderEmails();
        IQueryable<HPOrderEmailIntervalTime> GetIntervalTimesByEmailTemplateTypeGuid(Guid emailTemplateTypeGUID);
    }
}