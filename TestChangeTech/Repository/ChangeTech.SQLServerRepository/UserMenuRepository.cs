using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;
using ChangeTech.IDataRepository;

namespace ChangeTech.SQLServerRepository
{
    public class UserMenuRepository : RepositoryBase, IUserMenuRepository
    {
        public IQueryable<UserMenu> GetUserMenuOfProgram(Guid programGUID)
        {
            return GetEntities<UserMenu>(um=>um.Program.ProgramGUID == programGUID);
        }

        public UserMenu GetUserMenu(Guid menuItemGUID)
        {
            return GetEntities<UserMenu>(um=>um.MenuItemGUID == menuItemGUID).FirstOrDefault();
        }

        public void UpdateUserMenu(UserMenu userMenu)
        {
            UpdateEntity(userMenu);
        }

        public IQueryable<UserMenuTemplate> GetMenuTempates()
        {
            return GetEntities<UserMenuTemplate>();
        }

        public void DeleteUserMenu(Guid programGUID)
        {
            DeleteEntities<UserMenu>(u=>u.Program.ProgramGUID == programGUID, Guid.Empty);
        }
    }
}
