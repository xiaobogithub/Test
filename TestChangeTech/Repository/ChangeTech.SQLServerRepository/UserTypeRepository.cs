using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Linq;
using System;

namespace ChangeTech.SQLServerRepository
{
    public class UserTypeRepository : RepositoryBase, IUserTypeRepository
    {
        public IQueryable<UserType> GetAllUserTypes()
        {
            return GetEntities<UserType>(ut => (!ut.IsDeleted.HasValue || (ut.IsDeleted.HasValue && ut.IsDeleted != true)) && ut.Visible == true);
        }
    }
}
