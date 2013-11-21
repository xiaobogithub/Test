using ChangeTech.Entities;
using System;
using System.Linq;

namespace ChangeTech.IDataRepository
{
    public interface IUserTypeRepository
    {
        IQueryable<UserType> GetAllUserTypes();
    }
}
