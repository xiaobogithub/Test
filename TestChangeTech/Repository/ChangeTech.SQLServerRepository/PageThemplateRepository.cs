using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class PageThemplateRepository: RepositoryBase, IPageThemplateRepository
    {
        #region IPageThemeRepository Members

        public IQueryable<PageTemplate> GetAllPageTemplate()
        {
            return GetEntities<PageTemplate>();
        }

        public PageTemplate Get(Guid pageTemplate)
        {
            return GetEntities<PageTemplate>(p => p.PageTemplateGUID == pageTemplate && 
                (p.IsDeleted.HasValue? p.IsDeleted.Value == false: true)).FirstOrDefault();
        }
        #endregion
    }
}
