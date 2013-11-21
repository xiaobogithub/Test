using System;
using System.Web;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;

using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using ChangeTech.IDataRepository;
using ChangeTech.Entities;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public static readonly string MD5_KEY = "psycholo";
        public static readonly DateTime defautTime = new DateTime(2010, 12, 18);
        private bool _isWebApplication;
        public UserService()
            : this(true)
        {
        }

        public UserService(bool isWebApplication)
        {
            _isWebApplication = isWebApplication;
        }

        #region IUserService Members

        public UserModel GetAdminUserByUserName(string userName)
        {
            UserModel um = null;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, Guid.Empty);
            //User user = Resolve<IUserRepository>().GetUserByEmail(userName);
            if(user != null)
            {
                um = new UserModel();
                um.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;
                um.UserName = user.Email;
                um.PhoneNumber = user.MobilePhone;
                um.Security = user.Security;
                um.UserGuid = user.UserGUID;
                um.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.Value.ToString());
                um.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                um.ProgramSecuirty = new SortedList<Guid, int>();
                List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(user.UserGUID).ToList();
                foreach(ProgramUser pu in psList)
                {
                    if (!pu.ProgramReference.IsLoaded)
                    {
                        pu.ProgramReference.Load();
                    }
                    um.ProgramSecuirty.Add(pu.Program.ProgramGUID, pu.Security);
                }
            }
            return um;
        }

        public UserModel Logon(string userName, string password, Guid programGuid)
        {
            UserModel um = null;
            try
            {
                User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
                if(user != null && user.Password.Equals(password))
                {
                    um = new UserModel();
                    um.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;
                    um.UserName = user.Email;
                    um.PhoneNumber = user.MobilePhone;
                    um.Security = user.Security;
                    um.UserGuid = user.UserGUID;
                    um.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.Value.ToString());
                    um.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                    um.ProgramSecuirty = new SortedList<Guid, int>();
                    List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(user.UserGUID).ToList();
                    foreach(ProgramUser ps in psList)
                    {
                        if(!ps.ProgramReference.IsLoaded)
                        {
                            ps.ProgramReference.Load();
                        }
                        um.ProgramSecuirty.Add(ps.Program.ProgramGUID, ps.Security);
                    }

                    // update last logon field
                    user.LastLogon = DateTime.UtcNow;
                    Resolve<IUserRepository>().UpdateUser(user);

                    LogUtility.LogUtilityIntance.LogMessage(string.Format("Login::{0}", user.Email));
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            return um;
        }

        //public Guid NewUser(string userName, string password, string mobilePhone, Gender gender, string firstName, string lastName, int userType, Guid programGuid)
        public Guid NewUser(UserModel newUserModel)
        {
            User newuser = new User();
            newuser.UserGUID = Guid.NewGuid();
            newuser.Email = newUserModel.UserName;
            newuser.Password = newUserModel.PassWord; //password;
            newuser.MobilePhone = newUserModel.PhoneNumber; //mobilePhone;
            newuser.Gender = newUserModel.Gender.ToString().Trim(); // gender.ToString();
            newuser.FirstName = newUserModel.FirstName; //firstName;
            newuser.LastName = newUserModel.LastName; //lastName;
            newuser.Security = 0; //No permission of application, must apply to join one program
            newuser.LastLogon = DateTime.UtcNow;
            newuser.UserType = (int)newUserModel.UserType;// userType;
            newuser.ProgramGUID = newUserModel.ProgramGuid;// programGuid;
            return Resolve<IUserRepository>().Register(newuser);
        }

        //public Guid NewApplicationUser(string userName, string password, string mobilePhone, Gender gender, string firstName, string lastName, int userType, bool isSMSLogin)
        //the validate of this func is not the same with the func NewUser.  TODO: change the validate into the same.
        public Guid NewApplicationUser(UserModel newUserModel)
        {
            User checkUser = Resolve<IUserRepository>().GetUserByEmailAndType(newUserModel.UserName, (int)newUserModel.UserType);
            if(checkUser == null)
            {
                User newuser = new User();
                newuser.UserGUID = Guid.NewGuid();
                newuser.Email = newUserModel.UserName;// userName;
                newuser.Password = newUserModel.PassWord;// password;
                newuser.MobilePhone = newUserModel.PhoneNumber; //mobilePhone;
                newuser.Gender = newUserModel.Gender.ToString().Trim();// gender.ToString();
                newuser.FirstName = newUserModel.FirstName;// firstName;
                newuser.LastName = newUserModel.LastName;// lastName;
                newuser.Security = (int)PermissionEnum.ApplicationSuperAdmin;
                newuser.LastLogon = DateTime.UtcNow;
                newuser.UserType = (int)newUserModel.UserType;// userType;
                newuser.ProgramGUID = Guid.Empty;
                newuser.IsSMSLogin = newUserModel.IsSMSLogin;// isSMSLogin;
                return Resolve<IUserRepository>().Register(newuser);
            }
            else
            {
                return Guid.Empty;
            }
        }

        public void RegisterUser(UserModel user, Guid languageGuid, Guid programGuid, string clientIP)
        {
            Program programentity = Resolve<IProgramRepository>().GetProgramByGuid(programGuid);
            DateTime setCurrentByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, user.UserGuid, DateTime.UtcNow);
            User newuser = new User();
            newuser.UserGUID = user.UserGuid;
            newuser.Email = user.UserName;
            newuser.Password = user.PassWord;
            newuser.MobilePhone = user.PhoneNumber;
            newuser.Gender = user.Gender.ToString().Trim();
            newuser.FirstName = user.FirstName;
            newuser.LastName = user.LastName;
            newuser.Security = 0; //No permission of application, must apply to join one program
            newuser.LastLogon = DateTime.UtcNow;
            newuser.UserType = (int)user.UserType;
            newuser.ProgramGUID = programGuid;

            // if program need pincode, create a pincode for user when registering
            if(programentity.IsNeedPinCode.HasValue && programentity.IsNeedPinCode.Value == true)
            {
                newuser.PinCode = ServiceUtility.GetPinCode();
            }
            Resolve<IUserRepository>().Register(newuser);
        }

        public bool IsValidUserName(string userName, Guid programGuid)
        {
            return Resolve<IUserRepository>().IsEmailUnique(userName, programGuid);
        }

        public bool IsValidUser(Guid userGuid, string password)
        {
            bool flag = false;
            User user = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            if(user != null && user.Password.Equals(password))
            {
                flag = true;
            }

            return flag;
        }

        public bool IsValidUser(string userName, string password, Guid programGuid)
        {
            bool flag = false;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            if(user != null && user.Password.Equals(password))
            {
                flag = true;
            }

            return flag;
        }

        public bool IsValidUserRetrievePassword(string userName, string mobilePhone, Guid programGuid)
        {
            bool flag = false;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            if(user != null && user.MobilePhone.Trim().Equals(mobilePhone))
            {
                flag = true;
            }

            return flag;
        }

        public bool IsValidUserRetrievePassword(string userName, Guid programGuid)
        {
            bool flag = false;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            if (user != null)
            {
                flag = true;
            }

            return flag;
        }

        public bool HasPermission(PermissionEnum programsecqurity, PermissionEnum permission, PermissionEnum applicationsecurity)
        {
            bool flag = false;
            if((programsecqurity & permission) == permission)
            {
                flag = true;
            }
            // if user is super admin, return ture
            if((applicationsecurity & PermissionEnum.ApplicationSuperAdmin) == PermissionEnum.ApplicationSuperAdmin)
            {
                flag = true;
            }
            return flag;
        }

        public UserModel GetUserByUserName(string userName, Guid programGuid)
        {
            UserModel um = null;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            if(user != null)
            {
                um = new UserModel();
                um.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;
                um.UserName = user.Email;
                um.PhoneNumber = user.MobilePhone;
                um.Security = user.Security;
                um.UserGuid = user.UserGUID;
                um.PassWord = user.Password;
                um.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.ToString());
                um.IsPaid = user.IsPaid.HasValue ? user.IsPaid.Value : false;
                um.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                um.ProgramSecuirty = new SortedList<Guid, int>();
                List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(user.UserGUID).ToList();
                foreach(ProgramUser ps in psList)
                {
                    if(!ps.ProgramReference.IsLoaded)
                    {
                        ps.ProgramReference.Load();
                    }
                    um.ProgramSecuirty.Add(ps.Program.ProgramGUID, ps.Security);
                }
            }
            return um;
        }

        public UserModel GetUserByUserName(string userName)
        {
            UserModel um = null;
            User user = Resolve<IUserRepository>().GetUserByEmail(userName);
            if (user != null)
            {
                um = new UserModel();
                um.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;
                um.UserName = user.Email;
                um.PhoneNumber = user.MobilePhone;
                um.Security = user.Security;
                um.UserGuid = user.UserGUID;
                um.PassWord = user.Password;
                um.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.ToString());
                um.IsPaid = user.IsPaid.HasValue ? user.IsPaid.Value : false;
                um.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                um.ProgramSecuirty = new SortedList<Guid, int>();
                List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(user.UserGUID).ToList();
                foreach (ProgramUser ps in psList)
                {
                    if (!ps.ProgramReference.IsLoaded)
                    {
                        ps.ProgramReference.Load();
                    }
                    um.ProgramSecuirty.Add(ps.Program.ProgramGUID, ps.Security);
                }
            }
            return um;
        }

        public UserModel GetUserModelByUserGUID(Guid userGUID)
        {
            UserModel um = null;
            User user = Resolve<IUserRepository>().GetUserByGuid(userGUID);
            if(user != null)
            {
                um = new UserModel();
                um.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;
                um.UserName = user.Email;
                um.PhoneNumber = user.MobilePhone;
                um.Security = user.Security;
                um.UserGuid = user.UserGUID;
                um.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.ToString());
                um.PassWord = user.Password;
                um.IsPaid = user.IsPaid.HasValue ? user.IsPaid.Value : false;
                um.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;

                um.ProgramSecuirty = new SortedList<Guid, int>();
                List<ProgramUser> psList = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(user.UserGUID).ToList();
                foreach(ProgramUser ps in psList)
                {
                    if(!ps.ProgramReference.IsLoaded)
                    {
                        ps.ProgramReference.Load();
                    }
                    um.ProgramSecuirty.Add(ps.Program.ProgramGUID, ps.Security);
                }
            }
            return um;
        }

        public ApplicationUserSecurityListModel GetApplicationSecurityListModel()
        {
            ApplicationUserSecurityListModel auList = new ApplicationUserSecurityListModel();

            IQueryable<User> allUser = Resolve<IUserRepository>().GetAdminUsers();
            foreach(User user in allUser)
            {
                auList.Add(ModelUtility.ParaseApplicationUserSecurityModel(user));
            }
            return auList;
        }

        public ApplicationUserSecurityModel GetUserSecurityModel(string userName)
        {
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, Guid.Empty);

            return ModelUtility.ParaseApplicationUserSecurityModel(user);
        }

        public void UpdateUserSecurity(string userName, PermissionEnum security, Guid programGuid)
        {
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            user.Security = (int)security;
            Resolve<IUserRepository>().UpdateUser(user);
        }

        public void UpdateUserSecurity(Guid userGuid, PermissionEnum security)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            user.Security = (int)security;
            Resolve<IUserRepository>().UpdateUser(user);
        }

        public void UpdateUserWithoutPassword(Guid userGuid, string userName, string mobilePhone, string gender)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            user.Email = userName;
            user.MobilePhone = mobilePhone;
            user.Gender = gender;
            Resolve<IUserRepository>().UpdateUser(user);
        }

        public void UpdatePassword(Guid userGuid, string password)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            user.Password = password;

            Resolve<IUserRepository>().UpdateUser(user);
        }

        public UsersModel GetUsersNotInProgram(Guid programGuid)
        {
            UsersModel addUsers = new UsersModel();

            List<ProgramUser> programUserList = Resolve<IProgramUserRepository>().GetProgramUserListByProgramGuid(programGuid).ToList();
            Dictionary<string, Guid> programUserDic = new Dictionary<string, Guid>();
            foreach(ProgramUser pu in programUserList)
            {
                if(!pu.UserReference.IsLoaded)
                {
                    pu.UserReference.Load();
                }

                programUserDic.Add(pu.User.Email, pu.User.UserGUID);
            }

            IQueryable<User> userlist = Resolve<IUserRepository>().GetAllUsers();

            foreach(User user in userlist)
            {
                if(!programUserDic.ContainsKey(user.Email))
                {
                    UserModel addUserModel = new UserModel();
                    addUserModel.FirstName = user.FirstName;
                    addUserModel.LastName = user.LastName;
                    addUserModel.UserName = user.Email;
                    addUserModel.UserGuid = user.UserGUID;
                    addUsers.Add(addUserModel);
                }
            }
            return addUsers;
        }

        public int GetUsrsCout(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail)
        {
            IQueryable<ProgramUser> programUserList = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(programGuid);

            if(UserStatus != "")
            {
                programUserList = programUserList.Where<ProgramUser>(l => l.Status == UserStatus);
            }
            if (UserType != UserTypeEnum.All)
            {
                programUserList = programUserList.Where<ProgramUser>(l => l.User.UserType.HasValue ? l.User.UserType.Value == (int)UserType : false);
            }
            if(!string.IsNullOrEmpty(userEmail))
            {
                programUserList = programUserList.Where<ProgramUser>(l => l.User.Email.Contains(userEmail));
            }

            return programUserList.Count();
        }

        public List<UserModel> GetUsersInProgram(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail, int currentPage, int pageSize)
        {
            List<UserModel> users = new List<UserModel>();
            IQueryable<ProgramUser> programUserQueryableList = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(programGuid);

            if(UserStatus != "")
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.Status == UserStatus);
            }
            if (UserType != UserTypeEnum.All)
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.User.UserType.HasValue ? l.User.UserType.Value == (int)UserType : false);
            }
            if(!string.IsNullOrEmpty(userEmail))
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.User.Email.Contains(userEmail));
            }
            programUserQueryableList = programUserQueryableList.OrderBy(p => p.User.Email).Skip((currentPage - 1) * pageSize).Take(pageSize);

            List<ProgramUser> programUserList = programUserQueryableList.ToList();
            foreach(ProgramUser user in programUserList)
            {
                if(!user.UserReference.IsLoaded)
                {
                    user.UserReference.Load();
                }

                UserModel model = new UserModel
                {
                    UserName = user.User.Email,
                    UserGuid = user.User.UserGUID,
                };
                users.Add(model);
            }
            return users;
        }

        public List<UserModel> GetUsersInProgram(Guid programGuid, UserTypeEnum UserType, string UserStatus, string userEmail)
        {
            List<UserModel> users = new List<UserModel>();

            IQueryable<ProgramUser> programUserQueryableList = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuid(programGuid);

            if(UserStatus != "")
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.Status == UserStatus);
            }
            if (UserType != UserTypeEnum.All)
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.User.UserType.HasValue ? l.User.UserType.Value == (int)UserType : false);
            }
            if(!string.IsNullOrEmpty(userEmail))
            {
                programUserQueryableList = programUserQueryableList.Where<ProgramUser>(l => l.User.Email.Contains(userEmail));
            }

            List<ProgramUser> programUserList = programUserQueryableList.ToList();

            foreach(ProgramUser user in programUserList)
            {
                if(!user.UserReference.IsLoaded)
                {
                    user.UserReference.Load();
                }
                if(!user.User.Email.StartsWith("ChangeTechTemp"))
                {
                    UserModel model = new UserModel
                    {
                        UserName = user.User.Email,
                        UserGuid = user.User.UserGUID,
                    };
                    users.Add(model);
                }
            }
            return users.OrderBy(u => u.UserName).ToList<UserModel>();
        }

        public UsersModel GetAllApplicationUser()
        {
            UsersModel addUsers = new UsersModel();
            IQueryable<User> userList = Resolve<IUserRepository>().GetAllUsers();
            foreach(User user in userList)
            {
                UserModel uim = new UserModel();
                uim.FirstName = user.FirstName;
                uim.LastName = user.LastName;
                uim.PhoneNumber = user.MobilePhone;
                uim.UserName = user.Email;
                uim.UserGuid = user.UserGUID;
                uim.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female; //user.Gender;
                uim.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.Value.ToString());
                uim.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                addUsers.Add(uim);
            }

            return addUsers;
        }

        public UsersModel GetQueryApplicationUser(string query)
        {
            UsersModel addUsers = new UsersModel();
            IQueryable<User> userList = null;
            if(string.IsNullOrEmpty(query))
            {
                userList = Resolve<IUserRepository>().GetAllUsers();
            }
            else
            {
                userList = Resolve<IUserRepository>().QueryUsers(query);
            }

            foreach(User user in userList)
            {
                UserModel uim = new UserModel();
                uim.FirstName = user.FirstName;
                uim.LastName = user.LastName;
                uim.PhoneNumber = user.MobilePhone;
                uim.UserName = user.Email;
                uim.UserGuid = user.UserGUID;
                uim.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;//user.Gender;
                uim.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.Value.ToString());
                uim.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                addUsers.Add(uim);
            }

            return addUsers;
        }

        public UserModel GetUserByUserGuid(Guid userGuid)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            UserModel uim = new UserModel();
            if(user != null)
            {
                uim.FirstName = user.FirstName;
                uim.LastName = user.LastName;
                uim.PhoneNumber = user.MobilePhone;
                uim.UserGuid = user.UserGUID;
                uim.UserName = user.Email;
                uim.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;// user.Gender;
                uim.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), user.UserType.Value.ToString());
                uim.IsSMSLogin = user.IsSMSLogin.HasValue ? user.IsSMSLogin.Value : false;
                uim.PinCode = user.PinCode;
            }
            return uim;
        }

        public void UpdateUserInfo(UserModel userInfo)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(userInfo.UserGuid);
            user.Email = userInfo.UserName;
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.MobilePhone = userInfo.PhoneNumber;
            user.Gender = userInfo.Gender.ToString().Trim();
            user.UserType = (int)userInfo.UserType;
            user.IsSMSLogin = userInfo.IsSMSLogin;
            switch(userInfo.UserType)
            {
                case ChangeTech.Models.UserTypeEnum.Administrator:
                    user.Security = (int)PermissionEnum.ApplicationSuperAdmin;
                    break;
                case ChangeTech.Models.UserTypeEnum.ProgramAdministrator:
                    user.Security = (int)(PermissionEnum.ApplicationAdmin | PermissionEnum.ApplicationCreate | PermissionEnum.ApplicationDelete | PermissionEnum.ApplicationEdit);
                    break;
                case ChangeTech.Models.UserTypeEnum.ProgramEditor:
                    user.Security = (int)(PermissionEnum.ApplicationAdmin);
                    break;
                case ChangeTech.Models.UserTypeEnum.Customer:
                    user.Security = (int)(PermissionEnum.ApplicationNone);
                    break;
            }
            Resolve<IUserRepository>().UpdateUser(user);
            user = Resolve<IUserRepository>().GetUserByGuid(userInfo.UserGuid);
            if(!user.ProgramUser.IsLoaded)
            {
                user.ProgramUser.Load();
            }
            foreach(ProgramUser pu in user.ProgramUser)
            {
                switch(userInfo.UserType)
                {
                    case ChangeTech.Models.UserTypeEnum.Administrator:
                    case ChangeTech.Models.UserTypeEnum.ProgramAdministrator:
                        pu.Security = (int)(PermissionEnum.ProgramAdmin | PermissionEnum.ProgramCreate | PermissionEnum.ProgramDelete | PermissionEnum.ProgramEdit | PermissionEnum.ProgramView);
                        break;
                    case ChangeTech.Models.UserTypeEnum.ProgramEditor:
                    case ChangeTech.Models.UserTypeEnum.Customer:
                        pu.Security = (int)(PermissionEnum.ProgramView);
                        break;
                }
                Resolve<IProgramUserRepository>().Update(pu);
            }
        }

        public void DeleteUser(Guid userGuid)
        {
            Resolve<IUserRepository>().DeleteUser(userGuid);
        }

        /// <summary>
        /// This is used for flash end user. CTPP need use EndUserLoginForCTPP.
        /// This function does not let enduser to login when LoginFailedType.HaveDoneAllClassed
        /// EndUserLoginForCTPP need let enduser to login when have done all class.
        /// If this function need be modified, consider whether the EndUserLoginForCTPP need be done together
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="programGuid"></param>
        /// <returns>
        /// 0: Invalid user or other reason
        /// 1: Valid user
        /// </returns>
        /// TODO: need to return a model which contains the login status and login user model.
        public string EndUserLogin(string userName, string password, string programGuid)
        {
            string result = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";";
            //End user should not be used by super admin.
            User user = Resolve<IUserRepository>().GetUserByEmailAndProgramNotSA(userName, new Guid(programGuid));
            if (user == null)
            {
                result += LoginFailedTypeEnum.UserNotExisted;
            }
            else
            {
                if (user.Password == password)
                {
                    ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(new Guid(programGuid), user.UserGUID);

                    if (programUser != null)
                    {
                        ProgramUserStatusEnum puStatus;
                        Enum.TryParse(programUser.Status, true, out puStatus);
                        //set user 's currenttime according TimeZone.
                        DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), user.UserGUID, DateTime.UtcNow);
                        switch (puStatus)
                        {
                            case ProgramUserStatusEnum.Paused:
                                if (programUser.LastPauseDate.HasValue && programUser.LastPauseDate.Value.AddDays(programUser.LastPauseDay.Value).Date <= setCurrentTimeByTimeZone.Date)
                                {
                                    programUser.Status = ProgramUserStatusEnum.Active.ToString();
                                    Resolve<IProgramUserRepository>().Update(programUser);
                                    result = ProgramUserActiveForEndUserLogin(result, programUser, false);
                                }
                                else
                                {
                                    result += LoginFailedTypeEnum.ProgramIsPaused;
                                }
                                break;
                            case ProgramUserStatusEnum.Completed:
                            case ProgramUserStatusEnum.Terminated:
                                // TODO: For terminated, need one more tip message
                                result += LoginFailedTypeEnum.HaveDoneAllClassed;
                                break;
                            case ProgramUserStatusEnum.Screening:
                                result += LoginFailedTypeEnum.UserNotExisted; // TODO: Need one more tip message
                                break;
                            default://Active
                                result = ProgramUserActiveForEndUserLogin(result, programUser, false);
                                break;
                        }
                    }
                    else
                    {
                        result += LoginFailedTypeEnum.NeedToJoinProgram;
                    }
                }
                else
                {
                    result += LoginFailedTypeEnum.PasswordWrong;
                }
            }
            return result;
        }

        /// <summary>
        /// This function is used for EndUserLogin function. When active or (Pause but it's time to update it to active),two case need the same codes. So write it into a function
        /// </summary>
        /// <param name="result"></param>
        /// <param name="programUser"></param>
        /// <param name="isForCTPP"></param>
        /// <returns></returns>
        private string ProgramUserActiveForEndUserLogin(string result, ProgramUser programUser,bool isForCTPP)
        {
            if (!programUser.ProgramReference.IsLoaded) programUser.ProgramReference.Load();
            if (!programUser.UserReference.IsLoaded) programUser.UserReference.Load();

            Program program = Resolve<IProgramRepository>().GetProgramByGuid(programUser.Program.ProgramGUID);
            bool isProgramNeedPay = program.IsWithPay.HasValue ? program.IsWithPay.Value : false;
            bool isProgramNoCatchUp = program.IsNoCatchUp.HasValue ? program.IsNoCatchUp.Value : false;
            bool isUserPaid = programUser.User.IsPaid.HasValue ? programUser.User.IsPaid.Value : false;

            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programUser.Program.ProgramGUID, programUser.User.UserGUID, DateTime.UtcNow);
            //Get Should do day at this time
            int currentDay = Resolve<IProgramUserService>().GetShouldDoDay(programUser.Program.ProgramGUID, programUser.User.UserGUID, setCurrentTimeByTimeZone);
            if (programUser.Day != null)
            {
                if (isForCTPP)//For CTPP, IF HaveDoneAllClassed, can login, but from flash, can not.
                {
                    //result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + programUser.User.UserGUID + ";" + programUser.Program.ProgramGUID + ";" + ((int)programUser.Day + 1);
                    result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + programUser.User.UserGUID + ";" + programUser.Program.ProgramGUID + ";" + currentDay;
                }
                else
                {
                    //Session currentSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programUser.Program.ProgramGUID, (int)programUser.Day + 1);
                    //if (currentSession != null)
                    //{
                    //    result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + programUser.User.UserGUID + ";" + programUser.Program.ProgramGUID + ";" + ((int)programUser.Day + 1);
                    //}
                    Session currentSession = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programUser.Program.ProgramGUID, currentDay);
                    if (currentSession != null)
                    {
                        result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + programUser.User.UserGUID + ";" + programUser.Program.ProgramGUID + ";" + currentDay;
                    }
                    // Should not go here
                    else
                    {
                        result += LoginFailedTypeEnum.HaveDoneAllClassed;
                        programUser.Status = ProgramUserStatusEnum.Completed.ToString();
                        Resolve<IProgramUserRepository>().Update(programUser);
                    }
                }
            }
            else//when program.Day==null,get first day from SessionList 
            {
                if (!programUser.Program.Session.IsLoaded)
                {
                    programUser.Program.Session.Load();
                }
                Session currentSession = programUser.Program.Session.OrderBy(d => d.Day).First();
                if (currentSession != null)
                {
                    result = Convert.ToInt32(ReturnMessageCodeEnum.SuccessCode).ToString() + ";" + programUser.User.UserGUID + ";" + programUser.Program.ProgramGUID + ";" + currentSession.Day;
                    if (programUser.Status.Equals(ProgramUserStatusEnum.Paused.ToString()))
                    {
                        programUser.Status = ProgramUserStatusEnum.Active.ToString();
                        Resolve<IProgramUserRepository>().Update(programUser);
                    }
                }
                else
                {
                    result += LoginFailedTypeEnum.NoClassYet;
                }
            }
            return result;
        }

        public string EndUserLoginForCTPP(string userName, string password, string programGuid)
        {
            string result = Convert.ToInt32(ReturnMessageCodeEnum.ErrorCode).ToString()+";";
            //End user should not be used by super admin.
            User user = Resolve<IUserRepository>().GetUserByEmailAndProgramNotSA(userName, new Guid(programGuid));
            if (user == null)
            {
                result += LoginFailedTypeEnum.UserNotExisted;
            }
            else
            {
                if (user.Password == password)
                {
                    ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(new Guid(programGuid), user.UserGUID);
                    if (programUser != null)
                    {
                        ProgramUserStatusEnum puStatus;
                        Enum.TryParse(programUser.Status, true, out puStatus);
                        //set user 's currenttime according TimeZone.
                        DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(new Guid(programGuid), user.UserGUID, DateTime.UtcNow);
                        switch (puStatus)
                        {
                            case ProgramUserStatusEnum.Paused:
                                if (programUser.LastPauseDate.HasValue && programUser.LastPauseDate.Value.AddDays(programUser.LastPauseDay.Value).Date <= setCurrentTimeByTimeZone.Date)
                                {
                                    programUser.Status = ProgramUserStatusEnum.Active.ToString();
                                    Resolve<IProgramUserRepository>().Update(programUser);
                                    result = ProgramUserActiveForEndUserLogin(result, programUser, true);
                                }
                                else
                                {
                                    result += LoginFailedTypeEnum.ProgramIsPaused;
                                }
                                break;
                            case ProgramUserStatusEnum.Screening:
                                result += LoginFailedTypeEnum.UserNotExisted; // TODO: Need one more tip message
                                break;
                            case ProgramUserStatusEnum.Completed:
                            case ProgramUserStatusEnum.Terminated:
                                // TODO: For terminated, need one more tip message
                                //result += LoginFailedType.HaveDoneAllClassed;//CTPP need login when have done all the classes.
                                //break;
                            default://Active
                                result = ProgramUserActiveForEndUserLogin(result, programUser, true);
                                break;
                        }
                    }
                    else
                    {
                        result += LoginFailedTypeEnum.NeedToJoinProgram;
                    }
                }
                else
                {
                    result += LoginFailedTypeEnum.PasswordWrong;
                }
            }
            return result;
        }

        public UsersModel GetCommonUsers()
        {
            UsersModel usm = new UsersModel();
            IQueryable<User> allUser = Resolve<IUserRepository>().GetAllUsers();
            foreach(User user in allUser)
            {
                if(user.Security == 0)
                {
                    UserModel uim = new UserModel();
                    uim.FirstName = user.FirstName;
                    uim.LastName = user.LastName;
                    uim.PhoneNumber = user.MobilePhone;
                    uim.UserGuid = user.UserGUID;
                    uim.UserName = user.Email;
                    uim.Gender = user.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;//user.Gender;
                    usm.Add(uim);
                }
            }

            return usm;
        }

        public UserModel GetCurrentUser()
        {
            if(_isWebApplication)
            {
                return GetAdminUserByUserName(HttpContext.Current.User.Identity.Name);
            }
            else
            {
                //TODO: Should write real logic when it is client application
                return new UserModel();
            }
        }

        /// <summary>
        /// Update the profile of user
        /// </summary>
        /// <param name="userInfo">The information of user</param>
        /// <returns>There are three flag maybe return: 0: fail, 1: successful, 2: change other email</returns>
        public int UpdateUserProfile(UserModel userInfo)
        {
            int flag = 0;
            //ToDo: Get user's original information
            User user = Resolve<IUserRepository>().GetUserByGuid(userInfo.UserGuid);

            //ToDo: Check the email
            //1. If user don't want to change email, it's OK
            //2. If user didn't change their email, it's OK
            //3. If user changed their email, and in there are no same email in user table, it's OK
            //4. If have same email in user table before the information updata, set flag is 2
            if(string.IsNullOrEmpty(userInfo.UserName) || user.Email == userInfo.UserName || Resolve<IUserRepository>().GetUserByEmail(userInfo.UserName, user.ProgramGUID.Value) == null)
            {
                //ToDo: Update the information of user
                if(!string.IsNullOrEmpty(userInfo.UserName))
                {
                    user.Email = userInfo.UserName;
                }
                if(!string.IsNullOrEmpty(userInfo.FirstName))
                {
                    user.FirstName = userInfo.FirstName;
                }
                if(!string.IsNullOrEmpty(userInfo.LastName))
                {
                    user.LastName = userInfo.LastName;
                }
                if(!string.IsNullOrEmpty(userInfo.PhoneNumber))
                {
                    user.MobilePhone = userInfo.PhoneNumber;
                }
                if (!string.IsNullOrEmpty(userInfo.Gender.ToString().Trim()))
                {
                    user.Gender = userInfo.Gender.ToString().Trim();
                }
                if(userInfo.UserType != 0)
                {
                    user.UserType = (int)userInfo.UserType;
                }
                Resolve<IUserRepository>().UpdateUser(user);
                flag = 1;
            }
            else
            {
                flag = 2;
            }
            return flag;
        }

        public void UpdateUserType(string userName, UserTypeEnum userType, Guid programGuid)
        {
            User userEntity = Resolve<IUserRepository>().GetUserByEmail(userName, programGuid);
            userEntity.UserType = (int)userType;
            Resolve<IUserRepository>().UpdateUser(userEntity);
        }

        public List<UserModel> GetSuperAdminList()
        {
            List<UserModel> userModelList = new List<UserModel>();
            IQueryable<User> userList = Resolve<IUserRepository>().GetUserByUserType((int)ChangeTech.Models.UserTypeEnum.Administrator);
            foreach(User user in userList)
            {
                UserModel usermodel = new UserModel
                {
                    UserGuid = user.UserGUID,
                    UserName = user.Email
                };
                userModelList.Add(usermodel);
            }

            return userModelList;
        }

        public void SetLastSelectResource(Guid categoryGuid, Guid resourceGuid, string resourceType)
        {
            User user = Resolve<IUserRepository>().GetUserByGuid(GetCurrentUser().UserGuid);
            user.LastSelectedResource = resourceGuid;
            user.LastSelectedResourceCategory = categoryGuid;
            user.LastSelectedResourceType = resourceType;
            Resolve<IUserRepository>().UpdateUser(user);
        }

        public UsersModel GetUserByUserType(ChangeTech.Models.UserTypeEnum userType)
        {
            UsersModel um = new UsersModel();
            IQueryable<User> userEntities = Resolve<IUserRepository>().GetUserByUserType((int)userType);
            foreach(User userEntity in userEntities)
            {
                UserModel uim = new UserModel();
                uim.FirstName = userEntity.FirstName;
                uim.LastName = userEntity.LastName;
                uim.PhoneNumber = userEntity.MobilePhone;
                uim.UserGuid = userEntity.UserGUID;
                uim.UserName = userEntity.Email;
                uim.Gender = userEntity.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;//userEntity.Gender;
                uim.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), (userEntity.UserType.HasValue ? userEntity.UserType.Value : 1).ToString());
                um.Add(uim);
            }
            return um;
        }

        public UsersModel GetAdminUsers()
        {
            UsersModel um = new UsersModel();
            IQueryable<User> userEntities = Resolve<IUserRepository>().GetAdminUsers();
            foreach(User userEntity in userEntities)
            {
                UserModel uim = new UserModel();
                uim.FirstName = userEntity.FirstName;
                uim.LastName = userEntity.LastName;
                uim.PhoneNumber = userEntity.MobilePhone;
                uim.UserGuid = userEntity.UserGUID;
                uim.UserName = userEntity.Email;
                uim.Gender = userEntity.Gender.Trim().Equals(GenderEnum.Male.ToString().Trim()) ? GenderEnum.Male : GenderEnum.Female;//userEntity.Gender;
                uim.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), (userEntity.UserType.HasValue ? userEntity.UserType.Value : 1).ToString());
                uim.IsSMSLogin = userEntity.IsSMSLogin.HasValue ? userEntity.IsSMSLogin.Value : false;
                um.Add(uim);
            }
            return um;
        }

        public UserPermissionListModel GetUserPermissionListModel(Guid userGuid)
        {
            UserPermissionListModel userPermissionList = new UserPermissionListModel();
            List<Program> programs = Resolve<IProgramRepository>().GetAllPrograms().ToList();
            List<ProgramUser> programUsers = Resolve<IProgramUserRepository>().GetProgramUserListByUserGuid(userGuid).ToList();

            foreach (ProgramUser programUser in programUsers)
            {
                if (!programUser.ProgramReference.IsLoaded)
                {
                    programUser.ProgramReference.Load();
                }
            }

            foreach(Program program in programs)
            {
                if(!program.LanguageReference.IsLoaded)
                {
                    program.LanguageReference.Load();
                }
                UserPermissionItemModel permissionItemModel = new UserPermissionItemModel();
                permissionItemModel.LanguageName = program.Language.Name;
                permissionItemModel.ProgramGUID = program.ProgramGUID;
                permissionItemModel.ProgramName = program.Name;

                
                if(programUsers.Where(p => p.Program.ProgramGUID == program.ProgramGUID).Count() > 0)
                {
                    userPermissionList.ProgramUserHasPermission.Add(permissionItemModel);
                }
                else
                {
                    userPermissionList.ProgramUserHasNotPermission.Add(permissionItemModel);
                }
            }
            return userPermissionList;
        }

        public bool ValidatePinCode(Guid userGuid, string pinCode)
        {
            bool flug = false;
            User userentity = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            if(userentity != null && userentity.PinCode.Equals(pinCode))
            {
                flug = true;
            }

            return flug;
        }

        public int ShouldDoDay(Guid programGuid, Guid userGuid)
        {
            int day = 1;
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGuid, userGuid, DateTime.UtcNow);
            int isThereClassReturnCode = Resolve<IProgramUserService>().IsThereClassToday(userGuid, programGuid, setCurrentTimeByTimeZone);
            if(isThereClassReturnCode == 5)
            {
                day = Resolve<IProgramUserService>().GetOutlineDay(setCurrentTimeByTimeZone);
            }
            else
            {
                ProgramUser programuserentity = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGuid, userGuid);
                day = (programuserentity.Day.HasValue ? programuserentity.Day.Value : 0) + 1;
            }

            return day;
        }


        #region Todo : Get Should do Session
        //public int ShouldDoSession(Guid programGuid, Guid userGuid)
        //{
        //    //default should do Day 1.
        //    int currentDay = int.MinValue;
        //    int maxDay = int.MinValue;

        //    //Get ReturnCode
        //    int isThereSessionReturnCode = Resolve<IProgramUserService>().IsThereClassToday(userGuid, programGuid, DateTime.UtcNow);

        //    //Get currentProgram lastSession
        //    Session lastSessionModel = Resolve<ISessionRepository>().GetLastSessionOfProgram(programGuid);

        //    if (lastSessionModel != null)
        //    {
        //        maxDay = lastSessionModel.Day.HasValue ? lastSessionModel.Day.Value : maxDay;
        //    }

        //    if (isThereSessionReturnCode == 5)
        //    {
        //        currentDay = Resolve<IProgramUserService>().GetOutlineDay(DateTime.UtcNow);
        //    }
        //    else
        //    {
        //        ProgramUser programUserEntity = Resolve<IProgramUserRepository>().GetProgramSecurityByProgramGuidAndUserGuid(programGuid, userGuid);
        //        if (programUserEntity != null )
        //        {
        //            if (Resolve<IProgramService>().GetProgramByGUID(programGuid).IsNoCatchUp == false)// Catch Up Day
        //            {
        //                currentDay = (programUserEntity.Day.HasValue ? programUserEntity.Day.Value : 0) + 1;
        //            }
        //            else //No Catch Up Day
        //            {
        //                if (!programUserEntity.LastFinishDate.HasValue)
        //                {
        //                    currentDay = (programUserEntity.Day.HasValue ? programUserEntity.Day.Value : 0) + 1;
        //                }
        //                else
        //                {
        //                    DateTime lastFinishDate = programUserEntity.LastFinishDate.Value;
        //                    int days = (DateTime.UtcNow - lastFinishDate).Days;
        //                    if (programUserEntity.Day.Value + days <= maxDay)
        //                    {
        //                        currentDay = programUserEntity.Day.Value + days;
        //                    }
        //                    else
        //                    {
        //                        currentDay = maxDay;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return currentDay;
        //}
        #endregion

        public string GetPinCodeByUserGuid(Guid userGuid)
        {
            string pinCode = string.Empty;
            User userentity = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            if(userentity != null)
            {
                pinCode = userentity.PinCode;
            }

            return pinCode;
        }

        public string GetUserSecrity(Guid userGuid)
        {
            User userentity = Resolve<IUserRepository>().GetUserByGuid(userGuid);
            return StringUtility.MD5Encrypt(string.Format("{0};{1}", userentity.Email, userentity.Password), MD5_KEY);
        }

        public void GetUserStatistics(Guid programGUID)
        {
            SortedList<DateTime, int> allUserPerDay = new SortedList<DateTime, int>();
            SortedList<DateTime, int> registerUserPerDay = new SortedList<DateTime, int>();
            SortedList<string, int> exitPagePerDay = new SortedList<string, int>();
            List<ProgramUser> programUsers = Resolve<IProgramUserRepository>().GetProgramUser(programGUID).ToList();

            Session sessionEntity = Resolve<ISessionRepository>().GetSessionByProgramGuidAndDay(programGUID, 0);
            if(!sessionEntity.SessionContent.IsLoaded)
            {
                sessionEntity.SessionContent.Load();
            }

            foreach(SessionContent sessionContent in sessionEntity.SessionContent)
            {
                if(!(sessionContent.IsDeleted.HasValue && sessionContent.IsDeleted.Value))
                {
                    string str = "";
                    if(!sessionContent.PageSequenceReference.IsLoaded)
                    {
                        sessionContent.PageSequenceReference.Load();
                    }
                    PageSequence pageSequence = sessionContent.PageSequence;
                    if(!(pageSequence.IsDeleted.HasValue && pageSequence.IsDeleted.Value))
                    {
                        if(!pageSequence.Page.IsLoaded)
                        {
                            pageSequence.Page.Load();
                        }
                        foreach(Page page in pageSequence.Page)
                        {
                            if(!(page.IsDeleted.HasValue && page.IsDeleted.Value))
                            {
                                str = sessionEntity.Day.ToString() + "." + sessionContent.PageSequenceOrderNo.ToString() + "." + page.PageOrderNo.ToString();
                                if(!exitPagePerDay.Keys.Contains(str))
                                {
                                    exitPagePerDay.Add(str, 0);
                                }
                            }
                        }
                    }
                }
            }

            foreach(ProgramUser programUser in programUsers)
            {
                DateTime dateTime = new DateTime(programUser.StartDate.Value.Year, programUser.StartDate.Value.Month, programUser.StartDate.Value.Day);
                if (programUser.StartDate.Value > defautTime)
                {
                    if(allUserPerDay.Keys.Contains(dateTime))
                    {
                        allUserPerDay[dateTime]++;
                    }
                    else
                    {
                        allUserPerDay[dateTime] = 1;
                    }
                    if(!programUser.Status.Equals("Screening"))
                    {
                        if(registerUserPerDay.Keys.Contains(dateTime))
                        {
                            registerUserPerDay[dateTime]++;
                        }
                        else
                        {
                            registerUserPerDay[dateTime] = 1;
                        }
                    }
                    if(!programUser.UserReference.IsLoaded)
                    {
                        programUser.UserReference.Load();
                    }
                    ActivityLog activityLog = Resolve<IActivityLogRepository>().GetLastActivity(programUser.User.UserGUID, 7);
                    if(activityLog != null)
                    {
                        Session aSessionEntity = Resolve<ISessionRepository>().GetSessionBySessionGuid(activityLog.SessionGuid.Value);
                        //PageSequence aPageSequenceEntity = Resolve<IPageSequenceRepository>().GetPageSequenceByGuid(activityLog.PageSequenceGuid.Value);
                        SessionContent aSessionContent = Resolve<ISessionContentRepository>().GetSessionContentBySessionGuidPageSequenceGuid(activityLog.SessionGuid.Value, activityLog.PageSequenceGuid.Value);
                        Page aPageEntity = Resolve<IPageRepository>().GetPageByPageGuid(activityLog.PageGuid.Value);
                        if(aSessionEntity != null && aSessionContent != null && aPageEntity != null)
                        {
                            string str1 = string.Format("{0}.{1}.{2}", aSessionEntity.Day, aSessionContent.PageSequenceOrderNo, aPageEntity.PageOrderNo);
                            if(!exitPagePerDay.Keys.Contains(str1))
                            {
                                System.Diagnostics.Trace.WriteLine(str1);
                            }
                            else
                            {
                                exitPagePerDay[str1]++;
                            }
                        }
                    }
                }
            }

            System.Diagnostics.Trace.WriteLine("=====================================");
            foreach(DateTime dd in allUserPerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", dd.ToString()));
            }

            System.Diagnostics.Trace.WriteLine("=====================================");

            foreach(DateTime dd in allUserPerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", allUserPerDay[dd]));
            }

            System.Diagnostics.Trace.WriteLine("=====================================");

            foreach(DateTime dd in registerUserPerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", dd.ToString()));
            }

            System.Diagnostics.Trace.WriteLine("=====================================");

            foreach(DateTime dd in registerUserPerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", registerUserPerDay[dd]));
            }

            System.Diagnostics.Trace.WriteLine("=====================================");

            foreach(string pageNO in exitPagePerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", pageNO));
            }

            System.Diagnostics.Trace.WriteLine("=====================================");

            foreach(string pageNO in exitPagePerDay.Keys)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}", exitPagePerDay[pageNO]));
            }
        }

        public string GetUserStatus(string userName, Guid programGUID)
        {
            User user = Resolve<IUserRepository>().GetUserByEmail(userName, programGUID);
            ProgramUser programUser = Resolve<IProgramUserRepository>().GetProgramUserByProgramGuidAndUserGuid(programGUID, user.UserGUID);
            //set current time according TimeZone.
            DateTime setCurrentTimeByTimeZone = Resolve<IProgramUserService>().GetCurrentTimeByTimeZone(programGUID, user.UserGUID, DateTime.UtcNow);
            if (programUser.Status.Equals(ProgramUserStatusEnum.Paused.ToString()) &&
                //programUser.LastPauseDate.HasValue && programUser.LastPauseDate.Value.AddDays(programUser.LastPauseDay.Value).Date <= DateTime.UtcNow.Date)
                programUser.LastPauseDate.HasValue && programUser.LastPauseDate.Value.AddDays(programUser.LastPauseDay.Value).Date <= setCurrentTimeByTimeZone.Date)
            {
                programUser.Status = ProgramUserStatusEnum.Active.ToString();
                Resolve<IProgramUserRepository>().Update(programUser);
            }
            return programUser.Status;
        }

        public List<UserTypeModel> GetAllUserTypes()
        {
            List<UserTypeModel> userTypesModel = new List<UserTypeModel>();
            IQueryable<ChangeTech.Entities.UserType> allUserTypes = Resolve<IUserTypeRepository>().GetAllUserTypes();
            foreach (ChangeTech.Entities.UserType userType in allUserTypes)
            {
                userTypesModel.Add(new UserTypeModel
                {
                    UserTypeID = userType.UserTypeID,
                    Name = userType.Name,
                    DisplayText = userType.DisplayText
                });

            }
            return userTypesModel;
        }
        #endregion
    }
}
