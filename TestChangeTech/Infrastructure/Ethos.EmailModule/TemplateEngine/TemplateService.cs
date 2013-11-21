using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.EmailModule.TemplateEngine
{
    public class TemplateService
    {
        public static string Build(string originalText, Dictionary<string, string> paramters)
        {
            foreach (KeyValuePair<string, string> paremater in paramters)
            {
                originalText = originalText.Replace("$" + paremater.Key + "$", paremater.Value);
            }
            return originalText;
        }
    }
}
