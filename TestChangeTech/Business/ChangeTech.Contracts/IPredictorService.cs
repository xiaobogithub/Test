using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IPredictorService
    {
        List<PredictorModel> GetAllPredictors();
        List<PredictorModel> GetPredictorsByPredictorCategory(Guid categoryGuid);
        PredictorModel GetPredictorByPredictorGuid(Guid predictorGuid);
        bool CanDeletePredictor(Guid predictorGuid);
        void DeletePredictor(Guid predictorGuid);
        void InsertPredictor(PredictorModel predictorModel);
        void UpdatePredictor(PredictorModel predictorModel);
        List<DropDownListItemModel> GetPredictorDropDownList();
    }
}
