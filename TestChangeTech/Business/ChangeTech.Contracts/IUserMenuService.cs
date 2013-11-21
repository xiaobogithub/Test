using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;
using ChangeTech.Entities;

namespace ChangeTech.Contracts
{
    public interface IUserMenuService
    {
        List<UserMenuModel> GetUserMenusOfProram(Guid programGuid);
        UserMenuModel GetUserMenu(Guid menuItemGUID);
        void UpdateUserMenu(Guid menuItemGUID, string text, string formTitle, string formText, string formBackButtonText, string formSubmitButtonText);
        void CreateUserMenuForProgramWhoDonotHave();
        Program addUserMenuFormProgarm(Program programentity);
        void AddUserMenuForProgram(Guid programGuid);
        void EnableUserMenu(Guid menuItemGUID);
        void UnableUserMenu(Guid menuItemGUID);
        UserMenu CloneUserMenu(UserMenu userMenu);
        UserMenu SetDefaultGuidForUserMenu(UserMenu needSetDefaultEntity, CloneProgramParameterModel cloneParameterModel);
    }
}
