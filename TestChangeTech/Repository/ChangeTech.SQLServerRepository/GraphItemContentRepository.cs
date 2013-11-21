using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class GraphItemContentRepository : RepositoryBase, IGraphItemContentRepository
    {
        public void Insert(GraphItemContent itemcontent)
        {
            InsertEntity(itemcontent);
        }

        public GraphItemContent Get(Guid graphItemGUID)
        {
            return GetEntities<GraphItemContent>(g => g.GraphItemGUID == graphItemGUID).FirstOrDefault();
        }

        public void Delete(Guid graphItemGUID)
        {
            DeleteEntities<GraphItemContent>(g => g.GraphItemGUID == graphItemGUID, Guid.Empty);
        }

        public void DeleteByGraphGUID(Guid graphGUID)
        {
            DeleteEntities<GraphItemContent>(g => g.GraphItem.Graph.GraphGUID == graphGUID, Guid.Empty);
        }

        public void Update(GraphItemContent itemcontent)
        {
            UpdateEntity(itemcontent);
        }

        public IQueryable<GraphItemContent> GetByProgram(Guid programguid, int startDay, int endDay)
        {
            return GetEntities<GraphItemContent>(g => g.GraphItem.Graph.Page.PageSequence.SessionContent.Where(s=>
                s.Session.Program.ProgramGUID == programguid &&
                (s.Session.Day.Value >= startDay && s.Session.Day.Value <= endDay)&&
                !(s.IsDeleted.HasValue && s.IsDeleted.Value) &&
                !(s.Session.IsDeleted.HasValue && s.Session.IsDeleted.Value) &&
                !(s.Session.Program.IsDeleted.HasValue && s.Session.Program.IsDeleted.Value)).Count() > 0 &&
                ! (g.IsDeleted.HasValue && g.IsDeleted.Value) &&
                ! (g.GraphItem.IsDeleted.HasValue && g.GraphItem.IsDeleted.Value) &&
                ! (g.GraphItem.Graph.IsDeleted.HasValue && g.GraphItem.Graph.IsDeleted.Value) &&
                ! (g.GraphItem.Graph.Page.IsDeleted.HasValue && g.GraphItem.Graph.Page.IsDeleted.Value) &&
                ! (g.GraphItem.Graph.Page.PageSequence.IsDeleted.HasValue && g.GraphItem.Graph.Page.PageSequence.IsDeleted.Value));
        }
    }
}
