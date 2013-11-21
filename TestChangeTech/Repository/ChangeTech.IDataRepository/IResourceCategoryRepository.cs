using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
namespace ChangeTech.IDataRepository
{
    public interface IResourceCategoryRepository
    {
        ResourceCategory GetResourceCategory(Guid categoryGUID);
        ResourceCategory GetResourceCategory(string categoryName);
        IQueryable<ResourceCategory> GetAllCategory();
        void InsertResourceCategory(ResourceCategory category);
        void DeleteResourceCategory(Guid categoryGuid);
        void UpdateResourceCategory(ResourceCategory category);
    }
}
