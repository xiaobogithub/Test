using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ScreenResultTemplatePageLineRepository : RepositoryBase, IScreenResultTemplatePageLineRepository
    {
        public void AddPageLine(ScreenResultTemplatePageLine pageLineEntity)
        {
            InsertEntity(pageLineEntity);
        }

        public void UpdatePageLine(ScreenResultTemplatePageLine pageLineEntity)
        {
            UpdateEntity(pageLineEntity);
        }

        public void DeletePageLine(Guid pageLineGuid)
        {
            DeleteEntity<ScreenResultTemplatePageLine>(pl => pl.PageLineGUID == pageLineGuid, new Guid());
        }

        public ScreenResultTemplatePageLine GetPageLine(Guid pageLineGuid)
        {
            return GetEntities<ScreenResultTemplatePageLine>().Where(pl => pl.PageLineGUID == pageLineGuid && (!pl.IsDeleted.HasValue || pl.IsDeleted.HasValue && pl.IsDeleted.Value == false)).FirstOrDefault();
        }

        public IQueryable<ScreenResultTemplatePageLine> GetPageLinesByPageGuid(Guid pageGuid)
        {
            return GetEntities<ScreenResultTemplatePageLine>().Where(pl => pl.PageGUID == pageGuid && (!pl.IsDeleted.HasValue || pl.IsDeleted.HasValue && pl.IsDeleted.Value == false)).OrderBy(pl => pl.Order);
        }

        public ScreenResultTemplatePageLine GetPageLineByPageGuidAndPageLineOrder(Guid pageGuid, int orderNum)
        {
            return GetEntities<ScreenResultTemplatePageLine>().Where(pl => pl.PageGUID == pageGuid && pl.Order == orderNum && (!pl.IsDeleted.HasValue || pl.IsDeleted.HasValue && pl.IsDeleted.Value == false)).FirstOrDefault();
        }
    }
}
