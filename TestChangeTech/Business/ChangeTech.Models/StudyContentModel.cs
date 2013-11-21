using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class StudyContentModel
    {
        public Guid StudContentGUID { get; set; }
        public string RouteURL { get; set; }
    }

    public class InsertStudyContentModel
    {
        public Guid StudyGUID { get; set; }
        public string RouteURL { get; set; }
    }
}
