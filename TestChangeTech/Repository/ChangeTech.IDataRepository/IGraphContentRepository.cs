using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IGraphContentRepository
    {
        void Insert(GraphContent graphContent);
        GraphContent Get(Guid graphGUID);
        void Update(GraphContent graphContent);
        void Delete(Guid graphGUID);
        IQueryable<GraphContent> GetGraphsByProgram(Guid programguid, int startDay, int endDay);
    }
}
