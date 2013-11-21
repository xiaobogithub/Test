using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.IDataRepository
{
    public interface IUserMenuRepository
    {
        IQueryable<UserMenu> GetUserMenuOfProgram(Guid programGUID);
        UserMenu GetUserMenu(Guid menuItemGUID);
        void UpdateUserMenu(UserMenu userMenu);
        IQueryable<UserMenuTemplate> GetMenuTempates();
        void DeleteUserMenu(Guid programGUID);
    }
}
