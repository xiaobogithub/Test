using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class HPOrderLicenceRepository : RepositoryBase, IHPOrderLicenceRepository
    {
        public void Insert(HPOrderLicence hpOrderLicenceEntity)
        {
            InsertEntity(hpOrderLicenceEntity);
        }

        public void Update(HPOrderLicence hpOrderLicenceEntity)
        {
            UpdateEntity(hpOrderLicenceEntity);
        }

        public void Delete(Guid hpOrderLicenceGuid)
        {
            DeleteEntity<HPOrderLicence>(ol => ol.HPOrderLicenceGUID == hpOrderLicenceGuid, new Guid());
        }

        public void DeleteEntityByProgramUserGuid(Guid programUserGuid)
        {
            DeleteEntity<HPOrderLicence>(ol => ol.ProgramUserGUID == programUserGuid, new Guid());
        }

        public HPOrderLicence GetOrderLicenceByOrderLicenceGuid(Guid hpOrderLicenceGuid)
        {
            return GetEntities<HPOrderLicence>().Where(ol => ol.HPOrderLicenceGUID == hpOrderLicenceGuid && (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false)).FirstOrDefault();
        }

        public HPOrderLicence GetOrderLicenceByProgramUserGuid(Guid programUserGuid)
        {
            return GetEntities<HPOrderLicence>().Where(ol => ol.ProgramUserGUID == programUserGuid && (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false)).FirstOrDefault();
        }

        public IQueryable<HPOrderLicence> GetUsedOrderLicencesByHPOrderGuid(Guid hpOrderGuid)
        {
            return GetEntities<HPOrderLicence>().Where(ol => ol.HPOrderGUID == hpOrderGuid && (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false));
        }
    }
}
