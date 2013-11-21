using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IHPOrderLicenceRepository
    {
        void Insert(HPOrderLicence hpOrderLicenceEntity);
        void Update(HPOrderLicence hpOrderLicenceEntity);
        void Delete(Guid hpOrderLicenceGuid);
        void DeleteEntityByProgramUserGuid(Guid programUserGuid);
        HPOrderLicence GetOrderLicenceByOrderLicenceGuid(Guid hpOrderLicenceGuid);
        HPOrderLicence GetOrderLicenceByProgramUserGuid(Guid programUserGuid);
        IQueryable<HPOrderLicence> GetUsedOrderLicencesByHPOrderGuid(Guid hpOrderGuid);
    }
}
