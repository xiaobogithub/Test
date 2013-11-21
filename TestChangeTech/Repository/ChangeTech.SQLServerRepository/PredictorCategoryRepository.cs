using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class PredictorCategoryRepository : RepositoryBase, IPredictorCategoryRepository
    {
        #region IPredictorCategoryRepository Members

        public List<PredictorCategory> GetAllPredictorCategory()
        {
            return GetEntities<PredictorCategory>().OrderBy(p => p.Name).ToList<PredictorCategory>();
        }

        public PredictorCategory GetPredictorCategoryByCategoryGuid(Guid predictorCategoryGuid)
        {
            return GetEntities<PredictorCategory>(p => p.PredictorCategoryGUID == predictorCategoryGuid && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void InsertPredictorCategory(PredictorCategory predictorCategory)
        {
            InsertEntity(predictorCategory);
        }

        public void DeletePredictorCategory(Guid predictorCategoryGuid)
        {
            DeleteEntity<PredictorCategory>(p => p.PredictorCategoryGUID == predictorCategoryGuid, new Guid());
        }

        public void UpdatePredictorCategory(PredictorCategory predicCategory)
        {
            UpdateEntity(predicCategory);
        }

        #endregion
    }
}
