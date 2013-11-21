using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class GraphRepository : RepositoryBase, IGraphRepository
    {
        public void Instert(Graph graph)
        {
            InsertEntity(graph);
        }

        public Graph GetGraphByPageGuid(Guid pageGuid)
        {
            return GetEntities<Graph>(g => g.Page.PageGUID == pageGuid && (g.IsDeleted.HasValue ? g.IsDeleted == false : true)).FirstOrDefault();
        }      

        public void Update(Graph graph)
        {
            UpdateEntity(graph);
        }

        public Graph Get(Guid graphGuid)
        {
            return GetEntities<Graph>(g => g.GraphGUID == graphGuid && (g.IsDeleted.HasValue ? g.IsDeleted == false : true)).FirstOrDefault();
        }
    }
}
