using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class UserGroupModel
    {
        public Guid GroupGUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProgramGUID { get; set; }
    }
}
