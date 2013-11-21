using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPredictorRepository
    {
        List<Predictor> GetAllPredictors();
        List<Predictor> GetPredictorByPredictorCategoryGuid(Guid predictorCategoryGuid);
        Predictor GetPredictorByPredictorGuid(Guid predictorGuid);
        void DeletePredictor(Guid predictorGuid);
        void InstertPredictor(Predictor predictor);
        void UpdatePredictor(Predictor predictor);
    }
}
