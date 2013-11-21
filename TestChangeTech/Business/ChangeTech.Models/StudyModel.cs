using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class StudyModel
    {
        public Guid StudyGUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
    }
}
