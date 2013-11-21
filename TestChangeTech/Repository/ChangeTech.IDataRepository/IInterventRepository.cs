using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IInterventRepository
    {
        List<Intervent> GetAllIntervents();
        Intervent GetIntervent(Guid interventGuid);
        List<Intervent> GetInterventsOfCategory(Guid categoryGuid);
        void DeteteIntervent(Guid intervnetGuid);
        void UpdateIntervent(Intervent intervent);
        void InsertIntervent(Intervent intervent);
    }
}
