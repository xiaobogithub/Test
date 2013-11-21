using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class ApplicationUserSecurityModel
    {
        public Guid UserGuid { get; set; }
        public int ApplicationSecurity { get; set; }
        public string Account { get; set; }
        public bool SuperAdmin { get; set; }
        public bool None { get; set; }
        public bool Admin { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

        public bool IsPermission(PermissionEnum permission)
        {
            return ((PermissionEnum)ApplicationSecurity & permission) == permission;
        }
    }

    public class ApplicationUserSecurityListModel : List<ApplicationUserSecurityModel>
    {
        public ApplicationUserSecurityListModel()
        { 
        }

        public ApplicationUserSecurityListModel(List<ApplicationUserSecurityModel> usersSecurity)
        {
            AddRange(usersSecurity);
        }
    }
}
