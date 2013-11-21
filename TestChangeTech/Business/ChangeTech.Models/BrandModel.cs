using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class BrandModel
    {
        public Guid BrandGUID { get; set; }
        public string BrandName { get; set; }
        public ResourceModel BrandLogo { get; set; }
        public string BrandURL { get; set; }

    }
}
