using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ethos.Utility
{
    public static class URLExtension
    {
        public static string ToStringWithoutPort(this Uri uri)
        {
            string url = uri.ToString();
            string speStr = ":2";
            if (url.Contains(speStr))
            {
                string str1 = url.Substring(0, url.IndexOf(speStr));
                string str2 = url.Substring(url.IndexOf(speStr) + 6);
                url = str1 + str2;
            }
            return url;
        }
    }
}
