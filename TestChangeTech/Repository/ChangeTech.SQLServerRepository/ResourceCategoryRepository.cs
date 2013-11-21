using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ResourceCategoryRepository: RepositoryBase, IResourceCategoryRepository
    {
        #region IResourceCategoryRepository Members

        public ResourceCategory GetResourceCategory(Guid categoryGUID)
        {
            return GetEntities<ResourceCategory>(rc => rc.ResourceCategoryGUID == categoryGUID).FirstOrDefault();
        }

        public ResourceCategory GetResourceCategory(string categoryName)
        {
            return GetEntities<ResourceCategory>(rc => rc.Name.Equals(categoryName)).FirstOrDefault();
        }

        public IQueryable<ResourceCategory> GetAllCategory()
        {
            return GetEntities<ResourceCategory>();
        }

        public void InsertResourceCategory(ResourceCategory category)
        {
            InsertEntity(category);
        }

        public void DeleteResourceCategory(Guid categoryGuid)
        {
            DeleteEntity<ResourceCategory>(r => r.ResourceCategoryGUID == categoryGuid, new Guid());
        }

        public void UpdateResourceCategory(ResourceCategory category)
        {
            UpdateEntity(category);
        }

        #endregion
    }
}
