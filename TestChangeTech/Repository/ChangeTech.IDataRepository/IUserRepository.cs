using ChangeTech.Entities;
using System;
using System.Linq;

namespace ChangeTech.IDataRepository
{
    public interface IUserRepository
    {
        void UpdateUser(User user);
        User GetUserByEmail(string userEmail, Guid programGuid);
        User GetUserByEmailAndProgramNotSA(string userEmail, Guid programGuid);
        User GetUserByGuid(Guid userGuid);
        Guid Register(User user);
        bool IsEmailUnique(string userEmail, Guid programGuid);
        IQueryable<User> GetAllUsers();
        IQueryable<User> QueryUsers(string keyword);
        IQueryable<User> GetAdminUsers();
        void DeleteUser(Guid userGuid);
        IQueryable<User> GetUsersBySecurity(int security);
        IQueryable<User> GetUserByUserType(int userTypeID);
        User GetUserByEmailAndType(string email, int userType);
        IQueryable<User> GetUserListByEmail(string email);
        IQueryable<User> GetUserListByMobileAndProgram(Guid programGuid, string mobile);
        User GetUserByEmail(string userEmail);
    }
}
