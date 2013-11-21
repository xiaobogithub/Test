using System;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Models;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.Services
{
    public class PredictorCategoryService : ServiceBase, IPredictorCategoryService
    {
        #region IPredictorCategoryService Members

        public List<PredictorCategoryModel> GetAllPredictorCategory()
        {
            List<PredictorCategoryModel> listPredictorCategoryModel = new List<PredictorCategoryModel>();
            List<PredictorCategory>  listPredictorCategory =  Resolve<IPredictorCategoryRepository>().GetAllPredictorCategory();
            foreach (PredictorCategory predictorCategory in listPredictorCategory)
            {
                PredictorCategoryModel predictorCategoryModel = new PredictorCategoryModel();
                predictorCategoryModel.CategoryDescription = predictorCategory.Description;
                predictorCategoryModel.CategoryName = predictorCategory.Name;
                predictorCategoryModel.CategoryID = predictorCategory.PredictorCategoryGUID;

                listPredictorCategoryModel.Add(predictorCategoryModel);
            }
            return listPredictorCategoryModel;
        }

        public void InsertPredictorCategory(PredictorCategoryModel predicCategoryModel)
        {
            PredictorCategory predictorCategory = new PredictorCategory();
            predictorCategory.PredictorCategoryGUID = Guid.NewGuid();
            predictorCategory.Name = predicCategoryModel.CategoryName;
            predictorCategory.Description = predicCategoryModel.CategoryDescription;
            predictorCategory.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPredictorCategoryRepository>().InsertPredictorCategory(predictorCategory);
        }
        
        public void DeletePredictorCategory(Guid predictorCategoryGuid)
        {
            Resolve<IPredictorCategoryRepository>().DeletePredictorCategory(predictorCategoryGuid);
        }

        public PredictorCategoryModel GetPredictorCategoryByCategoryGuid(Guid categoryGuid)
        {
            PredictorCategoryModel predictorCategoryModel = new PredictorCategoryModel();
            PredictorCategory predictorCategory = Resolve<IPredictorCategoryRepository>().GetPredictorCategoryByCategoryGuid(categoryGuid);
            predictorCategoryModel.CategoryName = predictorCategory.Name;
            predictorCategoryModel.CategoryID = predictorCategory.PredictorCategoryGUID;
            predictorCategoryModel.CategoryDescription = predictorCategory.Description;

            return predictorCategoryModel;
        }

        public void UpdatePredictorCategory(PredictorCategoryModel predicCategoryModel)
        {
            PredictorCategory predictorCategory = Resolve<IPredictorCategoryRepository>().GetPredictorCategoryByCategoryGuid(predicCategoryModel.CategoryID);
            predictorCategory.Description = predicCategoryModel.CategoryDescription;
            predictorCategory.Name = predicCategoryModel.CategoryName;
            predictorCategory.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IPredictorCategoryRepository>().UpdatePredictorCategory(predictorCategory);
        }

        public bool CanDeletePredictorCategory(Guid predictorCategoryGuid)
        {
            bool flug = true;
            if (Resolve<IPredictorRepository>().GetPredictorByPredictorCategoryGuid(predictorCategoryGuid).Count > 0)
            {
                flug = false;
            }
            return flug;
        }

        public List<DropDownListItemModel> GetPredictorCategoryDropdownList()
        {
            List<PredictorCategory> list = Resolve<IPredictorCategoryRepository>().GetAllPredictorCategory();
            List<DropDownListItemModel> ddlList = new List<DropDownListItemModel>();
            ddlList.Add(new DropDownListItemModel("All", string.Empty));
            foreach(PredictorCategory category in list)
            {
                ddlList.Add(new DropDownListItemModel(category.Name, category.PredictorCategoryGUID.ToString()));
            }
            return ddlList;
        }

        #endregion
    }
}
