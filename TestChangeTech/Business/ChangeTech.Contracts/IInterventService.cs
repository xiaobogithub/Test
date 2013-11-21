using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IInterventService
    {
        List<InterventModel> GetAllIntervent();
        List<InterventModel> GetInterventsOfCategory(Guid categoryGuid);
        InterventModel GetIntervent(Guid interventGuid);
        void DeleteIntervent(Guid interventGuid);
        void InsertIntervent(InterventModel interventModel);
        bool CanDeleteIntervent(Guid interventGuid);
        void UpdateIntervent(InterventModel interventModel);
        void MergeIntervent(Guid toInterventGUID, Guid fromInterventGUID);
        string GetInterventName(Guid interventGuid);
    }
}
