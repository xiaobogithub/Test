using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IProgramAccessoryRepository
    {
        //AccessoryTemplate GetAccessoryPagesByProgram(Guid programGuid, string type);
        void Insert(AccessoryTemplate accessoryPage);
        void Update(AccessoryTemplate accessoryPage);
        void Delete(Guid accessoryGuid);
        AccessoryTemplate GetAccessory(Guid accessoryGuid);
        AccessoryTemplate GetAccessory(Guid programGuid, string type);
        void DeleteAccessoryOfProgram(Guid programGuid);
        IQueryable<AccessoryTemplate> GetAccessoryList(Guid programGuid);
    }
}
