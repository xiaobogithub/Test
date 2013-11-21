using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class UserGroupRepository : RepositoryBase, IUserGroupRepository
    {

        #region IUserGroupRepository Members

        public IQueryable<UserGroup> GetUserGroupByProgramGUID(Guid programGuid)
        {
            return GetEntities<UserGroup>(ug => ug.Program.ProgramGUID == programGuid);
        }

        public void Add(UserGroup group)
        {
            InsertEntity(group);
        }

        public void Delete(Guid groupGuid)
        {
            DeleteEntity<UserGroup>(u => u.GroupGUID == groupGuid, Guid.Empty);
        }

        public UserGroup GetUserGroup(Guid groupGuid)
        {
            return GetEntities<UserGroup>(u => u.GroupGUID == groupGuid).FirstOrDefault();
        }

        public void Update(UserGroup group)
        {
            UpdateEntity(group);
        }

        #endregion
    }
}
