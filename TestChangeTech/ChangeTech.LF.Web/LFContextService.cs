using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ethos.DependencyInjection;

namespace ChangeTech.LF.Web
{
    public class LFContextService
    {
        public static string LFAccount
        {
            get
            {
                if (HttpContext.Current.Session["LFAccount"] ==null)
                {
                    HttpContext.Current.Session["LFAccount"] = string.Empty;
                }

                return HttpContext.Current.Session["LFAccount"].ToString();
            }
            set
            {
                HttpContext.Current.Session["LFAccount"] = value;
            }
        }

        public static string GoWebEncryptUrl
        {
            get
            {
                if (HttpContext.Current.Session["GoWebEncryptUrl"] == null)
                {
                    HttpContext.Current.Session["GoWebEncryptUrl"] = string.Empty;
                }

                return HttpContext.Current.Session["GoWebEncryptUrl"].ToString();
            }
            set
            {
                HttpContext.Current.Session["GoWebEncryptUrl"] = value;
            }
        }

        public static void ClearLFAccount()
        {
            HttpContext.Current.Session["LFAccount"] = null;
            HttpContext.Current.Session.Contents.Remove("LFAccount");
        }

        public static void ClearGoWebEncryptUrl()
        {
            HttpContext.Current.Session["GoWebEncryptUrl"] = null;
            HttpContext.Current.Session.Contents.Remove("GoWebEncryptUrl");
        }
    }
}