using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPredictorCategoryRepository
    {
        List<PredictorCategory> GetAllPredictorCategory();
        void InsertPredictorCategory(PredictorCategory predictorCategory);
        void DeletePredictorCategory(Guid predictorCategoryGuid);
        PredictorCategory GetPredictorCategoryByCategoryGuid(Guid predictorCategoryGuid);
        void UpdatePredictorCategory(PredictorCategory predicCategory);
    }
}
