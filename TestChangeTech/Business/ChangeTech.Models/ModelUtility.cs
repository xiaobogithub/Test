using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangeTech.Entities;

namespace ChangeTech.Models
{
    public class ModelUtility
    {
        public static ApplicationUserSecurityModel ParaseApplicationUserSecurityModel(User user)
        {
            if(user != null)
            {
                ApplicationUserSecurityModel au = new ApplicationUserSecurityModel();
                au.UserGuid = user.UserGUID;
                au.Account = user.Email;
                au.ApplicationSecurity = user.Security;
                au.SuperAdmin = au.IsPermission(PermissionEnum.ApplicationSuperAdmin);
                au.None = au.IsPermission(PermissionEnum.ApplicationNone);
                au.Admin = au.IsPermission(PermissionEnum.ApplicationAdmin);
                au.Delete = au.IsPermission(PermissionEnum.ApplicationDelete);
                au.Edit = au.IsPermission(PermissionEnum.ApplicationEdit);
                au.Create = au.IsPermission(PermissionEnum.ApplicationCreate);
                return au;
            }
            else
            {
                return null;
            }
        }

        public static UserSecurityModel ParaseProgramSecurityEntity(ProgramUser ps)
        {
            if(ps != null)
            {
                UserSecurityModel us = new UserSecurityModel();
                if(!ps.UserReference.IsLoaded)
                {
                    ps.UserReference.Load();
                }
                us.Account = ps.User.Email;
                us.ApplicationSecurity = ps.Security;
                if(!ps.ProgramReference.IsLoaded)
                {
                    ps.ProgramReference.Load();
                }
                us.ProgramGuid = ps.Program.ProgramGUID;
                us.ProgramUser = ps.Security;
                if(!ps.UserReference.IsLoaded)
                {
                    ps.UserReference.Load();
                }
                us.UserGuid = ps.User.UserGUID;
                us.ProgramUserGuid = ps.ProgramUserGUID;
                us.EmailTime = ps.MailTime == null ? 0 : Convert.ToInt32(ps.MailTime);
                us.Status = ps.Status;
                us.FirstName = ps.User.FirstName;
                us.LastName = ps.User.LastName;
                us.Mobile = ps.User.MobilePhone;
                us.Gender = ps.User.Gender;
                us.CurrentDay = ps.Day.HasValue ? ps.Day.Value.ToString() : "";
                us.LastLogonDateStr = ps.LastFinishDate.HasValue ? ps.LastFinishDate.Value.ToString("yyyy-MM-dd HH:mm") : "";
                us.LastSendEmailDateStr = ps.LastSendEmailTime.HasValue ? ps.LastSendEmailTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                us.Pincode = ps.User.PinCode;
                us.SerialNumber = ps.User.SerialNumber;
                us.RegisterDateStr = ps.StartDate.HasValue ? ps.StartDate.Value.ToString("yyyy-MM-dd") : "";
                us.SwithMessageDateStr = ps.SwitchMessageTime.HasValue ? ps.SwitchMessageTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                return us;
            }
            else
            {
                return null;
            }
        }
    }
}
