using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.Services
{
    public class ResourceCategoryService : ServiceBase, IResourceCategoryService
    {
        public ResourceCategoriesModel GetAllCategory()
        {
            ResourceCategoriesModel categoriesModel = new ResourceCategoriesModel();
            categoriesModel.Categories = new List<ResourceCategoryModel>();

            List<ResourceCategory> resourceCateories = Resolve<IResourceCategoryRepository>().GetAllCategory().OrderByDescending(r => r.LastAccessed).ToList();
            foreach (ResourceCategory cate in resourceCateories)
            {
                ResourceCategoryModel categoryModel = new ResourceCategoryModel();
                categoryModel.CategoryGuid = cate.ResourceCategoryGUID;
                categoryModel.CategoryName = cate.Name;
                categoriesModel.Categories.Add(categoryModel);
            }

            categoriesModel.LastSelectedResourceCategory = GetCurrentUserLastSelectCategory();
            categoriesModel.LastSelectedResourceType = GetCurrentUserLastSelectResourceType();
            //Guid lastselectcategory = GetCurrentUserLastSelectCategory();
            //if (lastselectcategory != Guid.Empty)
            //{
            //    ResourceCategory selecedCategory = Resolve<IResourceCategoryRepository>().GetResourceCategory(lastselectcategory);
            //    categoriesModel.LastSelectedResourceCategory = new ResourceCategoryModel
            //    {
            //        CategoryGuid = selecedCategory.ResourceCategoryGUID,
            //        CategoryName = selecedCategory.Name
            //    };
            //}

            return categoriesModel;
        }

        private Guid GetCurrentUserLastSelectCategory()
        {
            Guid lastselectcategory = Guid.Empty;
            User currentUser = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            if (currentUser != null && currentUser.LastSelectedResourceCategory != null)
            {
                lastselectcategory = currentUser.LastSelectedResourceCategory.Value;
            }
            return lastselectcategory;
        }

        private string GetCurrentUserLastSelectResourceType()
        {
            string lastSelectResourceType = string.Empty;
            User currentUser = Resolve<IUserRepository>().GetUserByGuid(Resolve<IUserService>().GetCurrentUser().UserGuid);
            if (currentUser != null && currentUser.LastSelectedResourceType != null)
            {
                lastSelectResourceType = currentUser.LastSelectedResourceType;
            }
            return lastSelectResourceType;
        }

        public void InsertResourceCategory(Guid categoryGuid, string name, string des)
        {
            ResourceCategory category = new ResourceCategory();
            category.Name = name;
            category.ResourceCategoryGUID = categoryGuid;
            category.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IResourceCategoryRepository>().InsertResourceCategory(category);
        }

        public void UpdateResourceCategory(Guid categoryGuid, string name, string des)
        {
            ResourceCategory category = Resolve<IResourceCategoryRepository>().GetResourceCategory(categoryGuid);
            category.Name = name;
            category.LastUpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
            Resolve<IResourceCategoryRepository>().UpdateResourceCategory(category);
        }

        public void DeleteResourceCategory(Guid categoryGuid)
        {
            Resolve<IResourceCategoryRepository>().DeleteResourceCategory(categoryGuid);
        }
    }
}
