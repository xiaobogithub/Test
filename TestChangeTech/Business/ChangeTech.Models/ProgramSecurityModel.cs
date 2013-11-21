using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class UserSecurityModel
    {
        public Guid UserGuid { get; set; }
        public Guid ProgramGuid { get; set; }
        public string Account { get; set; }
        public int ApplicationSecurity { get; set; }
        public int ProgramUser { get; set; }
        public Guid ProgramUserGuid { get; set; }
        public int EmailTime { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string RegisterDateStr { get; set; }
        public string LastLogonDateStr { get; set; }
        public string LastSendEmailDateStr { get; set; }
        public string SwithMessageDateStr { get; set; }
        public string CurrentDay { get; set; }
        public string Pincode { get; set; }
        public string SerialNumber { get; set; }

        public bool ProgramAdmin
        {
            get
            {
                return IsProgramPermission(PermissionEnum.ProgramAdmin);
            }
        }

        public bool ProgramCreate
        {
            get
            {
                return IsProgramPermission(PermissionEnum.ProgramCreate);
            }
        }

        public bool ProgramDelete
        {
            get
            {
                return IsProgramPermission(PermissionEnum.ProgramDelete);
            }
        }

        public bool ProgramEdit
        {
            get
            {
                return IsProgramPermission(PermissionEnum.ProgramEdit);
            }
        }

        public bool ProgramView
        {
            get
            {
                return IsProgramPermission(PermissionEnum.ProgramView);
            }
        }

        public bool IsApplicationPermission(PermissionEnum permission)
        {
            return ((PermissionEnum)ApplicationSecurity & permission) == permission;
        }

        public bool IsProgramPermission(PermissionEnum permission)
        {
            return ((PermissionEnum)ProgramUser & permission) == permission;
        }
    }

    public class EditProgramUserModel
    {
        public Guid ProgramUsreGUID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pincode { get; set; }
        public string MobilePhone { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }
    }

    public class ProgramUserModel
    {
        public Guid UserGUID { get; set; }
        public Guid ProgramGUID { get; set; }
        public Guid ProgramUserGUID { get; set; }
        public decimal? UserTimeZone { get; set; }
        public bool? IsSMSToEmail { get; set; }
    }

    public class SetUserTimeModel
    {
        public Guid ProgramGuid { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime UtcTime { get; set; }
    }

    public class ProgramSecurityModel : List<UserSecurityModel>
    {
        public string ProgramName { get; set; }

        public ProgramSecurityModel()
        {
        }

        public ProgramSecurityModel(List<UserSecurityModel> us)
        {
            AddRange(us);
        }
    }
}
