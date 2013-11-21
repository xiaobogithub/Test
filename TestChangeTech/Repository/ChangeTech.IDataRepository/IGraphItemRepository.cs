using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IGraphItemRepository
    {
        IQueryable<GraphItem> GetGraphItemByGraph(Guid graphGuid);
        GraphItem get(Guid itemGuid);
        void Update(GraphItem item);
        void Insert(GraphItem item);
        void Delete(Guid itemGuid);
        void DeleteByGraphGuid(Guid graphGuid);
    }
}
