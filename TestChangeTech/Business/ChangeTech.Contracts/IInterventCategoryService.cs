using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IInterventCategoryService
    {
        List<InterventCategoryModel> GetInterventCategoryModelsByPredictorGuid(Guid guid);
        List<InterventCategoryModel> GetAllInterventCategory();
        List<DropDownListItemModel> GetInterventCategoryDropDownList();
        void InsertInterventCategory(InterventCategoryModel interventCategory);
        InterventCategoryModel GetInterventCategoryModel(Guid categoryGuid);
        void UpdateInterventCategory(InterventCategoryModel interventcategory);
        void DeleteInterventCategory(Guid interventCategoryGuid);
        bool CanDeleteInterventCategory(Guid interventCategoryGuid);
    }
}
