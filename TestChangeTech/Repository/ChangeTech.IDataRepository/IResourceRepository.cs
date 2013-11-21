using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IResourceRepository
    {
        IQueryable<Resource> GetResourcesOfCategory(string categroyName, string type);
        IQueryable<Resource> GetResourcesOfCategory(Guid categoryGUID, string type);
        Resource GetResource(Guid resourceGUID);
        Resource GetResource(string resourceNameOnServer);
        void AddResource(Resource newResource);
        void RemoveResource(Guid resourceGUID);
        void UpdateResource(Resource updateResource);
        //IQueryable<Resource> GetResourcesBySessionGUID(Guid sessionGUID);
    }
}
