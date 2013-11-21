using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class PredictorRepository : RepositoryBase, IPredictorRepository
    {
        #region IPredictorRepository Members

        public List<Predictor> GetAllPredictors()
        {
            return GetEntities<Predictor>().OrderBy(p => p.Name).ToList<Predictor>();
        }

        public List<Predictor> GetPredictorByPredictorCategoryGuid(Guid predictorCategoryGuid)
        {
            return GetEntities<Predictor>(p => p.PredictorCategory.PredictorCategoryGUID == predictorCategoryGuid && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).OrderBy(p => p.Name).ToList<Predictor>();
        }

        public Predictor GetPredictorByPredictorGuid(Guid PredictorGuid)
        {
            return GetEntities<Predictor>(p => p.PredictorGUID == PredictorGuid &&
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        }

        public void DeletePredictor(Guid predictorGuid)
        {
            DeleteEntity<Predictor>(p => p.PredictorGUID == predictorGuid, new Guid());
        }

        public void InstertPredictor(Predictor predictor)
        {
            InsertEntity(predictor);
        }

        public void UpdatePredictor(Predictor predictor)
        {
            UpdateEntity(predictor);
        }

        #endregion
    }
}
