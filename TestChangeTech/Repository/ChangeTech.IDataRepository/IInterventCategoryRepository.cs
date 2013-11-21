using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IInterventCategoryRepository
    {
        List<InterventCategory> GetInterventCategoryByPredictorGuid(Guid guid);
        List<InterventCategory> GetAllInterventCategory();
        void UpdateInterventCategory(InterventCategory interventCategory);
        void DeleteInterventCategory(Guid categoryGuid);
        void InsertInterventCategory(InterventCategory interventCategory);
        InterventCategory GetInterventCategory(Guid categoryGuid);
    }
}
