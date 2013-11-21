using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class PageResourceRepository : RepositoryBase, IPageResourceRepository 
    {
        /// <summary>
        /// used for page download resource table.
        /// </summary>
        /// <param name="newPageDownloadEntity">Entity of PageDownloadResource Datatable</param>
        public void AddPageResource(PageResource newPageDownloadEntity)
        {
            InsertEntity(newPageDownloadEntity);
        }

        /// <summary>
        /// used to delete pagedownloadresource datatable of the pageguid
        /// </summary>
        /// <param name="PageGuid">PageGuid to delete</param>
        public void DeletePageResource(Guid PageGuid)
        {
            DeleteEntity<PageResource>(p => p.PageGUID == PageGuid, Guid.Empty);
        }

        public PageResource GetPageResource(Guid pageGuid, Guid pageSequenceGuid)
        {
            return GetEntities<PageResource>().Where(pr => pr.PageGUID == pageGuid && pr.PageSequenceGUID == pageSequenceGuid).FirstOrDefault();
        }

        /// <summary>
        /// used for pagedownload resource table to get all resources of the certain session.
        /// </summary>
        /// <param name="sessionGuid">sessionGuid</param>
        /// <returns>list of resource</returns>
        public List<PageResource> GetPageResourcesBySessionGuid(Guid sessionGuid)
        {
            return GetEntities<PageResource>(p => p.SessionGUID == sessionGuid).ToList<PageResource>();
        }

    }
}
