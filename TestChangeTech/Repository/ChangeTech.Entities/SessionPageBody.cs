using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Entities
{
    public class SessionPageBody
    {
        public Guid SessionGUID { get; set; }
        public string PageBody { get; set; }
    }

    public class SessionPageMediaResource
    {
        public Guid SessionGUID { get; set; }
        public Guid MediaGUID { get; set; }
        public string Name { get; set; }
        public string NameOnServer { get; set; }
        public string Type { get; set; }
    }
}
