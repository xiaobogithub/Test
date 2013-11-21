using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using System.Linq;
using System;

namespace ChangeTech.SQLServerRepository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        #region IUserRepository Members

        public User GetUserByEmail(string userEmail)
        {
            return GetEntities<User>(u=>u.Email==userEmail&&(!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true)).FirstOrDefault();
        }

        public User GetUserByEmail(string userEmail, Guid programGuid)
        {
            return GetEntities<User>(u => u.Email == userEmail && u.ProgramGUID == programGuid && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true)).FirstOrDefault();
        }

        public User GetUserByEmailAndProgramNotSA(string userEmail, Guid programGuid)
        {
            return GetEntities<User>(u => u.Email == userEmail && u.ProgramGUID == programGuid && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true) && u.UserType != 2).FirstOrDefault();
        }

        public User GetUserByGuid(Guid userGuid)
        {
            return GetEntities<User>(u => u.UserGUID == userGuid && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true)).FirstOrDefault();
        }

        public Guid Register(User user)
        {
            InsertEntity(user);
            return user.UserGUID;
        }

        public bool IsEmailUnique(string userEmail, Guid programGuid)
        {
            bool isUnique = true;
            if(GetEntities<User>(u => u.Email == userEmail && u.ProgramGUID == programGuid && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true)).FirstOrDefault() != null)
            {
                isUnique = false;
            }
            return isUnique;
        }

        public void UpdateUser(User user)
        {
            UpdateEntity(user);
        }

        public IQueryable<User> GetAllUsers()
        {
            return GetEntities<User>(u => !u.Email.Contains("ChangeTechTemp") && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public IQueryable<User> GetAdminUsers()
        {
            return GetEntities<User>(u => u.UserType.Value != 1 && u.UserType.Value != 3 && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public void DeleteUser(Guid userGuid)
        {
            DeleteEntity<User>(u => u.UserGUID == userGuid, Guid.Empty);
        }

        public IQueryable<User> GetUsersBySecurity(int security)
        {
            return GetEntities<User>(u => (u.Security & security) != 0 && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public IQueryable<User> QueryUsers(string keyword)
        {
            return GetEntities<User>(u => (u.Email.Contains(keyword) || u.MobilePhone.Contains(keyword) || u.FirstName.Contains(keyword) || u.LastName.Contains(keyword)) && !u.Email.Contains("ChangeTechTemp") && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public IQueryable<User> GetUserByUserType(int userTypeID)
        {
            return GetEntities<User>().Where(u => u.UserType.Value == userTypeID && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public User GetUserByEmailAndType(string email, int userType)
        {
            return GetEntities<User>().Where(u => u.Email.Equals(email) && u.UserType.Value == userType && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true)).FirstOrDefault();
        }

        public IQueryable<User> GetUserListByEmail(string email)
        {
            return GetEntities<User>().Where(u => u.Email.Equals(email) && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }

        public IQueryable<User> GetUserListByMobileAndProgram(Guid programGuid, string mobile)
        {
            return GetEntities<User>().Where(u => u.MobilePhone.Equals(mobile) && u.ProgramGUID.HasValue && u.ProgramGUID.Value == programGuid && (!u.IsDeleted.HasValue || u.IsDeleted.HasValue && u.IsDeleted != true));
        }
        #endregion
    }
}
