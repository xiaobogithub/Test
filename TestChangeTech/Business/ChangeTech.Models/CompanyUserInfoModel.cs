using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class CompanyUserInfoModel
    {
        public Guid ProgramUserGUID { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Status { get; set; }
        public string RegisterDate { get; set; }
        public string CurrentDay { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PinCode { get; set; }
    }
}
