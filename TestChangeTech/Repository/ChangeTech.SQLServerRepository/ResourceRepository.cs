using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ResourceRepository: RepositoryBase,IResourceRepository
    {
        #region IResourceRepository Members

        public IQueryable<Resource> GetResourcesOfCategory(string categroyName, string type)
        {
            return GetEntities<Resource>(r => r.Name.Equals(categroyName) && r.Type.Equals(type) &&
                 (r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true));
        }

        public IQueryable<Resource> GetResourcesOfCategory(Guid categoryGUID, string type)
        {
            if (categoryGUID != Guid.Empty)
            {
                return GetEntities<Resource>(r => r.ResourceCategory.ResourceCategoryGUID.Equals(categoryGUID) && r.Type.Equals(type) &&
                    (r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true)).OrderBy(r => r.LastUpdated);
            }
            else
            {
                return GetEntities<Resource>(r => r.Type.Equals(type) &&
                    (r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true)).OrderBy(r => r.LastUpdated);
            }
        }

        public Resource GetResource(Guid resourceGUID)
        {
            return GetEntities<Resource>(r => r.ResourceGUID == resourceGUID &&
                 (r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public Resource GetResource(string resourceNameOnServer)
        {
            return GetEntities<Resource>(r => r.NameOnServer == resourceNameOnServer &&
                 (r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true)).FirstOrDefault();
        }

        public void AddResource(Resource newResource)
        {
            InsertEntity(newResource);
        }

        public void RemoveResource(Guid resourceGUID)
        {
            DeleteEntity<Resource>(r=>r.ResourceGUID == resourceGUID,Guid.Empty);
        }

        public void UpdateResource(Resource updateResource)
        {
            UpdateEntity(updateResource);
        }

        //public IQueryable<Resource> GetResourcesBySessionGUID(Guid sessionGUID) {
        //    // not finished yet.
        //    return GetEntities<Resource>(r => r.IsDeleted.HasValue ? r.IsDeleted.Value == false : true
        //        && r.PageMedia.Join(GetEntities<Page>(
        //            p => p.PageSequence.SessionContent.Join(GetEntities<Session>(
        //                s => s.SessionGUID == sessionGUID),
        //                sc => sc.Session.SessionGUID,
        //                s => s.SessionGUID,
        //                (sc, c) => sc).Count() > 0),
        //            pm => pm.PageGUID,
        //            p => p.PageGUID,
        //            (pm, p) => pm).Count() > 0);
        //}
        #endregion
    }
}
