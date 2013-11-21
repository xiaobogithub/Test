using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class Control
    {

    }

    public class NewPageControl
    {
        public string Name { get; set; }
        public string ParentControlGUID;
    }

    public class NewPageControlProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid PageControlGUID { get; set; }
    }
}
