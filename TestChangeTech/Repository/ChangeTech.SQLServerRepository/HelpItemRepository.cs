using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;

namespace ChangeTech.SQLServerRepository
{
    public class HelpItemRepository : RepositoryBase, IHelpItemRepository
    {
        public void Insert(HelpItem item)
        {
            InsertEntity(item);
        }

        public void Update(HelpItem item)
        {
            UpdateEntity(item);
        }

        public void Delete(Guid itemGuid)
        {
            DeleteEntities<HelpItem>(h => h.HelpItemGUID == itemGuid, Guid.Empty);
        }

        public HelpItem GetItem(Guid itemGuid)
        {
            return GetEntities<HelpItem>(h => h.HelpItemGUID == itemGuid).FirstOrDefault();
        }

        public IQueryable<HelpItem> GetItemByProgram(Guid programGuid)
        {
            return GetEntities<HelpItem>(h => h.Program.ProgramGUID == programGuid).OrderBy(h=>h.Order);
        }

        public void DeleteHelpItemOfProgram(Guid programGuid)
        {
            DeleteEntities<HelpItem>(h=>h.Program.ProgramGUID == programGuid, Guid.Empty);
        }
    }
}
