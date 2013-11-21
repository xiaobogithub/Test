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
    public class InterventCategoryService : ServiceBase, IInterventCategoryService
    {
        #region IInterventService Members

        public List<InterventCategoryModel> GetInterventCategoryModelsByPredictorGuid(Guid guid)
        {
            List<InterventCategory> interventCategroyEntities = Resolve<IInterventCategoryRepository>().GetInterventCategoryByPredictorGuid(guid);
            List<InterventCategoryModel> listInterventModel = new List<InterventCategoryModel>();

            foreach (InterventCategory interventCategory in interventCategroyEntities)
            {
                if (!interventCategory.PredictorReference.IsLoaded)
                {
                    interventCategory.PredictorReference.Load();
                }

                InterventCategoryModel interventCategoryModel = new InterventCategoryModel();
                interventCategoryModel.Description = interventCategory.Description;
                interventCategoryModel.Name = interventCategory.Name;
                interventCategoryModel.CategoryGUID = interventCategory.InterventCategoryGUID;
                interventCategoryModel.PredictorID = interventCategory.Predictor.PredictorGUID;
                interventCategoryModel.PredictorName = interventCategory.Predictor.Name;


                listInterventModel.Add(interventCategoryModel);
            }

            return listInterventModel;
        }

        public List<InterventCategoryModel> GetAllInterventCategory()
        {
            List<InterventCategoryModel> interventCategoryList = new List<InterventCategoryModel>();
            List<InterventCategory> allInterventCategoryEntity = Resolve<IInterventCategoryRepository>().GetAllInterventCategory();

            foreach (InterventCategory interventCategory in allInterventCategoryEntity)
            {
                InterventCategoryModel intervnetCategoryModel = new InterventCategoryModel();
                if (!interventCategory.PredictorReference.IsLoaded)
                {
                    interventCategory.PredictorReference.Load();
                }
                intervnetCategoryModel.Description = interventCategory.Description;
                intervnetCategoryModel.Name = interventCategory.Name;
                intervnetCategoryModel.CategoryGUID = interventCategory.InterventCategoryGUID;
                intervnetCategoryModel.PredictorID = interventCategory.Predictor.PredictorGUID;
                intervnetCategoryModel.PredictorName = interventCategory.Predictor.Name;

                interventCategoryList.Add(intervnetCategoryModel);
            }

            return interventCategoryList;
        }

        public List<DropDownListItemModel> GetInterventCategoryDropDownList()
        {
            List<DropDownListItemModel> interventCategoryDropdown = new List<DropDownListItemModel>();
            interventCategoryDropdown.Add(new DropDownListItemModel("All", ""));
            List<InterventCategory> allInterventCategoryEntity = Resolve<IInterventCategoryRepository>().GetAllInterventCategory();
            foreach (InterventCategory interventCategoryEntity in allInterventCategoryEntity)
            {
                interventCategoryDropdown.Add(new DropDownListItemModel(interventCategoryEntity.Name, interventCategoryEntity.InterventCategoryGUID.ToString()));
            }
            return interventCategoryDropdown;
        }

        public InterventCategoryModel GetInterventCategoryModel(Guid categoryGuid)
        {
            InterventCategoryModel interventCategoryModel = new InterventCategoryModel();
            InterventCategory interventCategory = Resolve<IInterventCategoryRepository>().GetInterventCategory(categoryGuid);
            if (!interventCategory.PredictorReference.IsLoaded)
            {
                interventCategory.PredictorReference.Load();
            }
            interventCategoryModel.Name = interventCategory.Name;
            interventCategoryModel.Description = interventCategory.Description;
            interventCategoryModel.CategoryGUID = interventCategory.InterventCategoryGUID;
            interventCategoryModel.PredictorID = interventCategory.Predictor.PredictorGUID;
            interventCategoryModel.PredictorName = interventCategory.Predictor.Name;

            return interventCategoryModel;
        }

        public void InsertInterventCategory(InterventCategoryModel interventCatgeoryModel)
        {
            InterventCategory interventCategory = new InterventCategory();
            interventCategory.Name = interventCatgeoryModel.Name;
            interventCategory.Description = interventCatgeoryModel.Description;
            interventCategory.InterventCategoryGUID = Guid.NewGuid();
            interventCategory.Predictor = Resolve<IPredictorRepository>().GetPredictorByPredictorGuid(interventCatgeoryModel.PredictorID);
            interventCategory.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IInterventCategoryRepository>().InsertInterventCategory(interventCategory);
        }

        public void UpdateInterventCategory(InterventCategoryModel interventCategoryModel)
        {
            InterventCategory interventCategoryEntity = Resolve<IInterventCategoryRepository>().GetInterventCategory(interventCategoryModel.CategoryGUID);
            interventCategoryEntity.Name = interventCategoryModel.Name;
            interventCategoryEntity.Description = interventCategoryModel.Description;
            interventCategoryEntity.Predictor = Resolve<IPredictorRepository>().GetPredictorByPredictorGuid(interventCategoryModel.PredictorID);
            interventCategoryEntity.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IInterventCategoryRepository>().UpdateInterventCategory(interventCategoryEntity);
        }

        public void DeleteInterventCategory(Guid interventCategoryGuid)
        {
            Resolve<IInterventCategoryRepository>().DeleteInterventCategory(interventCategoryGuid);
        }

        public bool CanDeleteInterventCategory(Guid interventCategoryGuid)
        {
            bool flug = true;
            if (Resolve<IInterventRepository>().GetInterventsOfCategory(interventCategoryGuid).Count > 0)
            {
                flug = false;
            }

            return flug;
        }

        #endregion
    }
}
