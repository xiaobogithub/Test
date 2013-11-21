using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Models;

namespace ChangeTech.Contracts
{
    public interface IUserService
    {
        UserModel GetAdminUserByUserName(string userName);
        UserModel GetUserByUserName(string userName, Guid programGuid);
        UserModel GetUserModelByUserGUID(Guid userGUID);
        UserModel Logon(string userName, string password, Guid programGuid);
        UserModel GetUserByUserGuid(Guid userGuid);
        Guid NewUser(UserModel newUserModel);
        Guid NewApplicationUser(UserModel newUserModel);
        void RegisterUser(UserModel user, Guid languageGuid, Guid programGuid, string clientIP);
        bool IsValidUserName(string userNameGuid, Guid programGuid);
        bool IsValidUser(Guid userGuid, string password);
        bool IsValidUser(string userName, string password, Guid programGuid);
        bool IsValidUserRetrievePassword(string userName, Guid programGuid);
        bool IsValidUserRetrievePassword(string userName, string mobilePhone, Guid programGuid);
        bool HasPermission(PermissionEnum programSecqurity, PermissionEnum permission, PermissionEnum applicationSecurity);
        ApplicationUserSecurityListModel GetApplicationSecurityListModel();
        ApplicationUserSecurityModel GetUserSecurityModel(string userName);
        UsersModel GetUsersNotInProgram(Guid programGuid);
        //TODO:Use a program user model as parameters
        List<UserModel> GetUsersInProgram(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail);
        //TODO:Use a program user model as parameters
        List<UserModel> GetUsersInProgram(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail, int currentPage, int pageSize);
        //TODO:Use a program user model as parameters
        int GetUsrsCout(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail);
        UsersModel GetAllApplicationUser();
        UsersModel GetQueryApplicationUser(string query);
        UsersModel GetCommonUsers();
        void UpdateUserSecurity(string userName, PermissionEnum security, Guid programGuid);
        void UpdateUserSecurity(Guid userGuid, PermissionEnum security);
        void UpdateUserWithoutPassword(Guid userGuid, string userName, string mobilePhone, string gender);
        void UpdatePassword(Guid userGuid, string password);
        void DeleteUser(Guid userGuid);
        void UpdateUserInfo(UserModel userInfo);
        string EndUserLogin(string userName, string password, string programGuid);
        string EndUserLoginForCTPP(string userName, string password, string programGuid);
        //string EndUserRegister(string userName, string password, string userGuid, string programGuid, string LanguageGuid);
        UserModel GetCurrentUser();
        /// <summary>
        /// Update the profile of user
        /// </summary>
        /// <param name="userInfo">The information of user</param>
        /// <returns>There are three flag maybe return: 0: fail, 1: successful, 2: change other email</returns>
        int UpdateUserProfile(UserModel userInfo);
        //TODO:Use a program user model as parameters
        void UpdateUserType(string userName, UserTypeEnum userType, Guid programGuid);
        List<UserModel> GetSuperAdminList();
        void SetLastSelectResource(Guid categoryGuid, Guid resourceGuid, string resourceType);
        UsersModel GetUserByUserType(UserTypeEnum userType);
        UserPermissionListModel GetUserPermissionListModel(Guid userGuid);
        UsersModel GetAdminUsers();
        bool ValidatePinCode(Guid userGuid, string pinCode);
        int ShouldDoDay(Guid programGuid, Guid userGuid);
        string GetPinCodeByUserGuid(Guid userGuid);
        string GetUserSecrity(Guid userGuid);
        void GetUserStatistics(Guid programGUID);
        string GetUserStatus(string userName, Guid programGUID);
        List<UserTypeModel> GetAllUserTypes();
        UserModel GetUserByUserName(string userName);
    }
}
