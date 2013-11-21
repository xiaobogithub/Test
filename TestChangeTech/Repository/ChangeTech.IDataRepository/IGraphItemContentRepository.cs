using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IGraphItemContentRepository
    {
        void Insert(GraphItemContent itemcontent);
        GraphItemContent Get(Guid graphItemGUID);
        void Delete(Guid graphItemGUID);
        void DeleteByGraphGUID(Guid graphGUID);
        void Update(GraphItemContent itemcontent);
        IQueryable<GraphItemContent> GetByProgram(Guid programguid, int startDay, int endDay);
    }
}
