using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IUserGroupService
    {
        List<UserGroupModel> GetUserGroupList(Guid programGuid);
        void UpdateUserGroup(UserGroupModel model);
        UserGroupModel GetUserGroupModel(Guid groupGuid);
        void DeleteUserGroup(Guid groupGuid);
        void AddUserGroup(UserGroupModel model);
    }
}
