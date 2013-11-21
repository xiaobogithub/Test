using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class GraphContentRepository : RepositoryBase, IGraphContentRepository
    {
        public void Insert(GraphContent graphContent)
        {
            InsertEntity(graphContent);           
        }

        public GraphContent Get(Guid graphGUID)
        {
            return GetEntities<GraphContent>(g => g.Graph.GraphGUID == graphGUID).FirstOrDefault();
        }

        public void Update(GraphContent graphContent)
        {
            UpdateEntity(graphContent);
        }

        public void Delete(Guid graphGUID)
        {
            DeleteEntities<GraphContent>(g => g.GraphGUID == graphGUID, Guid.Empty);
        }

        public IQueryable<GraphContent> GetGraphsByProgram(Guid programguid, int startDay, int endDay)
        {
            return GetEntities<GraphContent>(g => g.Graph.Page.PageSequence.SessionContent.Where(s=>
                s.Session.Program.ProgramGUID == programguid &&
                (s.Session.Day.Value >= startDay && s.Session.Day.Value <= endDay) &&
                !(g.IsDeleted.HasValue && g.IsDeleted.Value) &&
                !(g.Graph.Page.IsDeleted.HasValue && g.Graph.Page.IsDeleted.Value) &&
                !(g.Graph.Page.PageSequence.IsDeleted.HasValue && g.Graph.Page.PageSequence.IsDeleted.Value)).Count() > 0 &&
                !(g.IsDeleted.HasValue && g.IsDeleted.Value) &&
                !(g.Graph.Page.IsDeleted.HasValue && g.Graph.Page.IsDeleted.Value) &&
                !(g.Graph.Page.PageSequence.IsDeleted.HasValue && g.Graph.Page.PageSequence.IsDeleted.Value));
        }
    }
}
