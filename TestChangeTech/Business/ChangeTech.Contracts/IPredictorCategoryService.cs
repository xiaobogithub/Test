using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IPredictorCategoryService
    {
        List<PredictorCategoryModel> GetAllPredictorCategory();
        void InsertPredictorCategory(PredictorCategoryModel predicCategoryModel);
        void DeletePredictorCategory(Guid predictorCategoryGuid);
        PredictorCategoryModel GetPredictorCategoryByCategoryGuid(Guid predictorCategoryGuid);
        void UpdatePredictorCategory(PredictorCategoryModel predictorCategoryModel);
        bool CanDeletePredictorCategory(Guid predictorCategoryGuid);
        List<DropDownListItemModel> GetPredictorCategoryDropdownList();
    }
}
