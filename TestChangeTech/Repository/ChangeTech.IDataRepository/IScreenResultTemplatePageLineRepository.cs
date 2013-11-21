using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IScreenResultTemplatePageLineRepository
    {
        void AddPageLine(ScreenResultTemplatePageLine pageLineEntity);
        void UpdatePageLine(ScreenResultTemplatePageLine pageLineEntity);
        void DeletePageLine(Guid pageLineGuid);
        ScreenResultTemplatePageLine GetPageLine(Guid pageLineGuid);
        IQueryable<ScreenResultTemplatePageLine> GetPageLinesByPageGuid(Guid pageGuid);
        ScreenResultTemplatePageLine GetPageLineByPageGuidAndPageLineOrder(Guid pageGuid,int orderNum);
    }
}
