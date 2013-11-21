using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class OrderContentRepository : RepositoryBase, IOrderContentRepository
    {
        public void Insert(OrderContent orderContentEntity)
        {
            InsertEntity(orderContentEntity);
        }

        public void Delete(Guid orderContentGuid)
        {
            DeleteEntity<OrderContent>(oc => oc.OrderContentGUID == orderContentGuid, new Guid());
        }

        public void Update(OrderContent orderContentEntity)
        {
            UpdateEntity(orderContentEntity);
        }

        public OrderContent GetOrderContentByOrderGuidAndProgramGuid(Guid orderGuid, Guid programGuid)
        {
            return GetEntities<OrderContent>().Where(oc => (!oc.IsDeleted.HasValue || oc.IsDeleted.HasValue && oc.IsDeleted.Value == false) && oc.OrderGUID == orderGuid && oc.ProgramGUID == programGuid).FirstOrDefault();
        }

        public OrderContent GetOrderContentByOrderContentGuid(Guid orderContentGuid)
        {
            return GetEntities<OrderContent>().Where(oc => (!oc.IsDeleted.HasValue || oc.IsDeleted.HasValue && oc.IsDeleted.Value == false) && oc.OrderContentGUID == orderContentGuid).FirstOrDefault();
        }

        public IQueryable<OrderContent> GetOrderContentsByOrderGuid(Guid orderGuid)
        {
            return GetEntities<OrderContent>().Where(oc => (!oc.IsDeleted.HasValue || oc.IsDeleted.HasValue && oc.IsDeleted.Value == false) && oc.OrderGUID == orderGuid).OrderBy(oc => oc.Program.Name);
        }
    }
}
