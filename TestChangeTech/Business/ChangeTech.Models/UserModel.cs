using System;
using System.Collections;
using System.Collections.Generic;

namespace ChangeTech.Models
{
    public class UserModel
    {
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int Security { get; set; }
        public UserTypeEnum UserType { get; set; }
        public bool IsPaid { get; set; }
        public string PinCode { get; set; }
        public bool IsSMSLogin { get; set; }
        public Guid ProgramGuid { get; set; }
        public Guid UserGroupGuid { get; set; }
        public SortedList<Guid, int> ProgramSecuirty { get; set; }
    }

    public class UsersModel : List<UserModel>
    {
        public UsersModel()
        {
        }
    }

    public class UserPermissionItemModel
    {
        public string ProgramName { get; set; }
        public string LanguageName { get; set; }
        public Guid ProgramGUID { get; set; }
    }

    public class UserPermissionListModel
    {
        public List<UserPermissionItemModel> ProgramUserHasPermission { get; set; }
        public List<UserPermissionItemModel> ProgramUserHasNotPermission { get; set; }

        public UserPermissionListModel()
        {
            ProgramUserHasPermission = new List<UserPermissionItemModel>();
            ProgramUserHasNotPermission = new List<UserPermissionItemModel>();
        }
    }

    public class MissedClassUsersModel
    {
        public int TotalCount { get; set; }
        public List<UserModel> MissedClassUsers { get; set; }
        public MissedClassUsersModel()
        {
            MissedClassUsers = new List<UserModel>();
        }
    }

    public class UserTypeModel
    {
        public int UserTypeID { get; set; }
        public string Name { get; set; }
        public string DisplayText { get; set; }
    }

    /// <summary>
    /// When sendAsync emails, need an object for userToken
    /// </summary>
    public class UserTokenEmailSenderModel
    {
        public Guid ProgramGuid { get; set; }
        public Guid UserGuid { get; set; }
        public string Email { get; set; }
        public int FailCount { get; set; }
        public LogTypeEnum logType { get; set; }
    }


    public class UserTaskModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public UserTaskTypeEnum TaskType { get; set; }
        public string TaskContent { get; set; }
    }

    public class RegisterProgramUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid UserGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public string PhoneNumber { get; set; }
        public string SerialNumber { get; set; }
        public decimal? UserTimeZone { get; set; }
    }

    public class LFAuthenticationLogModel
    {
        public string Browser { get; set; }
        public string IP { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class LFSupportEmailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailTo { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
    }
}
