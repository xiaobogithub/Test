using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class CompanyModel
    {
        public Guid CompanyGUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string InternalContact { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string PostalAddress { get; set; }
    }

    public class CompanyRightModel
    {
        public Guid CompanyRightGUID { get; set; }
        public Guid CompanyGUID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string ComayDescription { get; set; }// notes
        public Guid ProgramGUID { get; set; }
        public string ProgramName { get; set; }
        public string Language { get; set; }
        public DateTime OverDueTime { get; set; }
        public DateTime StartTime { get; set; }
        public string ContactPerson { get; set; }
        public string InternalContact { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string PostalAddress { get; set; }
        public string ProgramCode { get; set; }
        public bool IsSupportHttps { get; set; }
        //public string Notes { get; set; }
    }
}
