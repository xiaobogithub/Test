using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class OrderLicenceRepository : RepositoryBase, IOrderLicenceRepository
    {
        public  void Insert(OrderLicence orderLicenceEntity)
        {
            InsertEntity(orderLicenceEntity);
        }
        public void Update(OrderLicence orderLicenceEntity)
        {
            UpdateEntity(orderLicenceEntity);
        }
        public void Delete(Guid orderLicenceGuid)
        {
            DeleteEntity<OrderLicence>(ol => ol.OrderLicenceGUID == orderLicenceGuid, new Guid());
        }
        public void DeleteEntityByProgramUserGuid(Guid programUserGuid)
        {
            DeleteEntity<OrderLicence>(ol => ol.ProgramUserGUID == programUserGuid, new Guid());
        }
        public OrderLicence GetOrderLicenceByOrderLicenceGuid(Guid orderLicenceGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.OrderLicenceGUID == orderLicenceGuid).FirstOrDefault();
        }

        public OrderLicence GetOrderLicenceByOrderContentGuid(Guid orderContentGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.OrderContentGUID == orderContentGuid && (!ol.ProgramUserGUID.HasValue||ol.ProgramUserGUID.Value==Guid.Empty)).FirstOrDefault();
        }

        public OrderLicence GetOrderLicenceByProgramUserGuid(Guid programUserGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.ProgramUserGUID == programUserGuid).FirstOrDefault();
        }

        public OrderLicence GetOrderLicenceByOrderContentGuidAndProgramUserGuid(Guid orderContentGuid, Guid puGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.OrderContentGUID == orderContentGuid && ol.ProgramUserGUID == puGuid).FirstOrDefault();
        }

        public IQueryable<OrderLicence> GetUsedOrderLicencesByResellerGuidAndOrderContentGuid(Guid resellerGuid, Guid orderContentGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.UpdatedBy == resellerGuid && ol.OrderContentGUID == orderContentGuid && (ol.ProgramUserGUID.HasValue && ol.ProgramUserGUID != Guid.Empty));
        }
        public IQueryable<OrderLicence> GetUsedOrderLicencesByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => ((!ol.IsDeleted.HasValue) || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.OrderContentGUID == orderContentGuid && ol.ProgramUserGUID.HasValue && ol.ProgramUser.Program.ProgramGUID == programGuid);
        }
        public OrderLicence GetLastRegistedUserByOrderContentGuidAndProgramGuid(Guid orderContentGuid, Guid programGuid)
        {
            return GetEntities<OrderLicence>().Where(ol => (!ol.IsDeleted.HasValue || ol.IsDeleted.HasValue && ol.IsDeleted.Value == false) && ol.OrderContentGUID == orderContentGuid && ol.ProgramUserGUID.HasValue && ol.ProgramUser.Program.ProgramGUID == programGuid).OrderByDescending(ol=>ol.ProgramUser.StartDate).FirstOrDefault();
        }
    }
}
