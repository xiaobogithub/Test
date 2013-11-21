using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IOrderContentRepository
    {
        void Insert(OrderContent orderContentEntity);
        void Delete(Guid orderContentGuid);
        void Update(OrderContent orderContentEntity);
        OrderContent GetOrderContentByOrderGuidAndProgramGuid(Guid orderGuid, Guid programGuid);
        OrderContent GetOrderContentByOrderContentGuid(Guid orderContentGuid);
        IQueryable<OrderContent> GetOrderContentsByOrderGuid(Guid orderGuid);
    }
}
