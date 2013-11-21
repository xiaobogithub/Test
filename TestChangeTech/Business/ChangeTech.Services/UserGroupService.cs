using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;
using ChangeTech.Models;

namespace ChangeTech.Services
{
    public class UserGroupService : ServiceBase, IUserGroupService
    {
        #region IUserGroupService Members

        public List<UserGroupModel> GetUserGroupList(Guid programGuid)
        {
            List<UserGroupModel> models = new List<UserGroupModel>();
            List<UserGroup> usergrouplist = Resolve<IUserGroupRepository>().GetUserGroupByProgramGUID(programGuid).ToList();
            foreach (UserGroup group in usergrouplist)
            {
                UserGroupModel model = new UserGroupModel();
                model.GroupGUID = group.GroupGUID;
                model.Name = group.Name;
                model.Description = group.Description;
                models.Add(model);
            }
            return models;
        }

        public void UpdateUserGroup(UserGroupModel model)
        {
            UserGroup usergroupEntity = Resolve<IUserGroupRepository>().GetUserGroup(model.GroupGUID);
            usergroupEntity.Name = model.Name;
            usergroupEntity.Description = model.Description;
            Resolve<IUserGroupRepository>().Update(usergroupEntity);
        }

        public UserGroupModel GetUserGroupModel(Guid groupGuid)
        {
            UserGroup usergroupentity = Resolve<IUserGroupRepository>().GetUserGroup(groupGuid);
            UserGroupModel model = new UserGroupModel
            {
                GroupGUID = usergroupentity.GroupGUID,
                Name = usergroupentity.Name,
                Description = usergroupentity.Description
            };

            return model;
        }

        public void DeleteUserGroup(Guid groupGuid)
        {
            Resolve<IUserGroupRepository>().Delete(groupGuid);
        }

        public void AddUserGroup(UserGroupModel model)
        {
            UserGroup group = new UserGroup
            {
                Description = model.Description,
                Name = model.Name,
                Program = Resolve<IProgramRepository>().GetProgramByGuid(model.ProgramGUID),
                GroupGUID = Guid.NewGuid()
            };

            Resolve<IUserGroupRepository>().Add(group);
        }

        #endregion
    }
}
