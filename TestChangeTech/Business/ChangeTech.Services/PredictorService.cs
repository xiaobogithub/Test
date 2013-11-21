using System.Linq;
using System.Collections.Generic;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using Ethos.DependencyInjection;
using ChangeTech.Entities;
using ChangeTech.Models;
using System;

namespace ChangeTech.Services
{
    public class PredictorService : ServiceBase, IPredictorService
    {
        #region IPredictorService Members

        public List<PredictorModel> GetAllPredictors()
        {
            List<Predictor> listPredictor = Resolve<IPredictorRepository>().GetAllPredictors();
            List<PredictorModel> listPredictorModel = new List<PredictorModel>();
            foreach (Predictor predictor in listPredictor)
            {
                if (!predictor.PredictorCategoryReference.IsLoaded)
                {
                    predictor.PredictorCategoryReference.Load();
                }
                PredictorModel prdictorModel = new PredictorModel();
                prdictorModel.PredictorID = predictor.PredictorGUID;
                prdictorModel.Name = predictor.Name;
                prdictorModel.Description = predictor.Description;
                prdictorModel.CatagoryID = predictor.PredictorCategory.PredictorCategoryGUID;
                prdictorModel.CategoryName = predictor.PredictorCategory.Name;

                listPredictorModel.Add(prdictorModel);
            }

            return listPredictorModel;
        }

        public bool CanDeletePredictor(Guid predictorGuid)
        {
            bool flug = true;
            if (Resolve<IInterventCategoryRepository>().GetInterventCategoryByPredictorGuid(predictorGuid).Count > 0)
            {
                flug = false;
            }

            return flug;
        }

        public void DeletePredictor(Guid predictorGuid)
        {
            Resolve<IPredictorRepository>().DeletePredictor(predictorGuid);
        }

        public void InsertPredictor(PredictorModel predictorModel)
        {
            Predictor predictor = new Predictor();
            predictor.PredictorGUID = Guid.NewGuid();
            predictor.Name = predictorModel.Name;
            predictor.Description = predictorModel.Description;
            predictor.PredictorCategory = Resolve<IPredictorCategoryRepository>().GetPredictorCategoryByCategoryGuid(predictorModel.CatagoryID);
            predictor.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPredictorRepository>().InstertPredictor(predictor);
        }

        public PredictorModel GetPredictorByPredictorGuid(Guid predictorGuid)
        {
            PredictorModel predictorModel = new PredictorModel();
            Predictor predictor = Resolve<IPredictorRepository>().GetPredictorByPredictorGuid(predictorGuid);
            if (!predictor.PredictorCategoryReference.IsLoaded)
            {
                predictor.PredictorCategoryReference.Load();
            }
            predictorModel.CatagoryID = predictor.PredictorCategory.PredictorCategoryGUID;
            predictorModel.CategoryName = predictor.PredictorCategory.Name;
            predictorModel.Name = predictor.Name;
            predictorModel.Description = predictor.Description;
            predictorModel.PredictorID = predictor.PredictorGUID;

            return predictorModel;
        }

        public void UpdatePredictor(PredictorModel predictorModel)
        {
            Predictor predictor = Resolve<IPredictorRepository>().GetPredictorByPredictorGuid(predictorModel.PredictorID);
            predictor.Name = predictorModel.Name;
            predictor.Description = predictorModel.Description;
            predictor.PredictorCategory = Resolve<IPredictorCategoryRepository>().GetPredictorCategoryByCategoryGuid(predictorModel.CatagoryID);
            predictor.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPredictorRepository>().UpdatePredictor(predictor);
        }

        public List<PredictorModel> GetPredictorsByPredictorCategory(Guid categoryGuid)
        {
            List<Predictor> listPredictor = Resolve<IPredictorRepository>().GetPredictorByPredictorCategoryGuid(categoryGuid);
            List<PredictorModel> listPredictorModel = new List<PredictorModel>();
            foreach (Predictor predictor in listPredictor)
            {
                if (!predictor.PredictorCategoryReference.IsLoaded)
                {
                    predictor.PredictorCategoryReference.Load();
                }
                PredictorModel prdictorModel = new PredictorModel();
                prdictorModel.PredictorID = predictor.PredictorGUID;
                prdictorModel.Name = predictor.Name;
                prdictorModel.Description = predictor.Description;
                prdictorModel.CatagoryID = predictor.PredictorCategory.PredictorCategoryGUID;
                prdictorModel.CategoryName = predictor.PredictorCategory.Name;

                listPredictorModel.Add(prdictorModel);
            }

            return listPredictorModel; 
        }

        public List<DropDownListItemModel> GetPredictorDropDownList()
        {
            List<DropDownListItemModel> dropDownList = new List<DropDownListItemModel>();
            dropDownList.Add(new DropDownListItemModel("All", ""));
            List<Predictor> predictorList = Resolve<IPredictorRepository>().GetAllPredictors();
            foreach (Predictor pre in predictorList)
            {
                dropDownList.Add(new DropDownListItemModel(pre.Name, pre.PredictorGUID.ToString()));
            }
            return dropDownList;
        }

        #endregion
    }
}
