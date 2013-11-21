using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IResourceCategoryService
    {
        ResourceCategoriesModel GetAllCategory();
        void InsertResourceCategory(Guid category,string name, string des);
        void UpdateResourceCategory(Guid categoryGuid, string name, string des);
        void DeleteResourceCategory(Guid categoryGuid);
    }
}
