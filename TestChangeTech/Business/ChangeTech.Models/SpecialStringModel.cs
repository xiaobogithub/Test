using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeTech.Models
{
    public class SpecialStringModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid LanguageGuid { get; set; }
        public string LanguageName { get; set; }
    }
}
