using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IPageResourceRepository
    {
        //used for page download resource as follows.
        void AddPageResource(PageResource newPageDownloadEntity);
        void DeletePageResource(Guid PageGuid);
        List<PageResource> GetPageResourcesBySessionGuid(Guid sessionGuid);
        PageResource GetPageResource(Guid pageGuid, Guid pageSequenceGuid);
    }
}
