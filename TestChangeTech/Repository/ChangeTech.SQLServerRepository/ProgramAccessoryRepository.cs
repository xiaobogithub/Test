using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class ProgramAccessoryRepository : RepositoryBase, IProgramAccessoryRepository
    {
        //public AccessoryTemplate GetAccessoryPagesByProgram(Guid programGuid, string type)
        //{
        //    return GetEntities<AccessoryTemplate>(a => a.Program.ProgramGUID == programGuid && a.Type == type).FirstOrDefault();
        //}

        public void Insert(AccessoryTemplate accessoryPage)
        {
            InsertEntity(accessoryPage);
        }

        public void Update(AccessoryTemplate accessoryPage)
        {
            UpdateEntity(accessoryPage);
        }

        public void Delete(Guid accessoryGuid)
        {
            DeleteEntity<AccessoryTemplate>(a => a.AccessoryTemplateGUID == accessoryGuid, Guid.Empty);
        }

        public AccessoryTemplate GetAccessory(Guid accessoryGuid)
        {
            return GetEntities<AccessoryTemplate>(a => a.AccessoryTemplateGUID == accessoryGuid).FirstOrDefault();
        }

        public AccessoryTemplate GetAccessory(Guid programGuid, string type)
        {
            return GetEntities<AccessoryTemplate>(a => a.Program.ProgramGUID == programGuid && a.Type == type).FirstOrDefault();
        }

        public void DeleteAccessoryOfProgram(Guid programGuid)
        {
            DeleteEntity<AccessoryTemplate>(a => a.Program.ProgramGUID == programGuid, Guid.Empty);
        }

        public IQueryable<AccessoryTemplate> GetAccessoryList(Guid programGuid)
        {
            return GetEntities<AccessoryTemplate>(a => a.Program.ProgramGUID == programGuid);
        }
    }
}
