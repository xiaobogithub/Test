using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IUserGroupRepository
    {
        IQueryable<UserGroup> GetUserGroupByProgramGUID(Guid programGuid);
        void Add(UserGroup group);
        void Delete(Guid groupGuid);
        UserGroup GetUserGroup(Guid groupGuid);
        void Update(UserGroup group);
    }
}
