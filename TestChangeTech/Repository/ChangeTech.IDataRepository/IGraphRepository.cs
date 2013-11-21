using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IGraphRepository
    {
        void Instert(Graph graph);
        Graph GetGraphByPageGuid(Guid pageGuid);
        void Update(Graph graph);
        Graph Get(Guid graphGuid);       
    }
}
