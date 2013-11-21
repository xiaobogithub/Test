using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class GraphItemRepository : RepositoryBase, IGraphItemRepository
    {
        public IQueryable<GraphItem> GetGraphItemByGraph(Guid graphGuid)
        {
            return GetEntities<GraphItem>(g => g.Graph.GraphGUID == graphGuid && (g.IsDeleted.HasValue ? g.IsDeleted == false : true));
        }

        public GraphItem get(Guid itemGuid)
        {
            return GetEntities<GraphItem>(g => g.GraphItemGUID == itemGuid && (g.IsDeleted.HasValue ? g.IsDeleted == false : true)).FirstOrDefault();
        }

        public void Update(GraphItem item)
        {
            UpdateEntity(item);
        }

        public void Insert(GraphItem item)
        {
            InsertEntity(item);
        }

        public void Delete(Guid itemGuid)
        {
            DeleteEntity<GraphItem>(g => g.GraphItemGUID == itemGuid, Guid.Empty);
        }

        public void DeleteByGraphGuid(Guid graphGuid)
        {
            DeleteEntities<GraphItem>(g => g.Graph.GraphGUID == graphGuid, Guid.Empty);
        }
    }
}
