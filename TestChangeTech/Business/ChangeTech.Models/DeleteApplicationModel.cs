using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class DeleteApplicationModel
    {
        public Guid ApplicationGUID { get; set; }
        public Guid ProgramGUID { get;set;}
        public Guid AssigneeGUID { get; set; }
        public string AssigneeEmail { get; set; }
        public Guid ApplicantGUID { get; set; }
        public string ApplicantEmail { get; set; }
        public string ProgramName { get; set; }
        public string Status { get; set; }
    }
}
