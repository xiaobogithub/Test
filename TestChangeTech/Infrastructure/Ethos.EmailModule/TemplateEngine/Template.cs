using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.EmailModule.TemplateEngine
{
    public class Template
    {
        public Dictionary<string, string> Parameters { get; set; }
        public string TemplateText { get; set; }

        public Template()
        {
            Parameters = new Dictionary<string, string>();
        }
    }
}
