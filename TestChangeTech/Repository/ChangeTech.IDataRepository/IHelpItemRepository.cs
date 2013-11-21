using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IHelpItemRepository
    {
        void Insert(HelpItem item);
        void Update(HelpItem item);
        void Delete(Guid itemGuid);
        void DeleteHelpItemOfProgram(Guid programGuid);
        HelpItem GetItem(Guid itemGuid);
        IQueryable<HelpItem> GetItemByProgram(Guid programGuid);
    }
}
