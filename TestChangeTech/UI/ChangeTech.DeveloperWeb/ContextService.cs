using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public class ContextService
    {
        public static UserModel CurrentAccount
        {
            get
            {
                if (HttpContext.Current.Session["CurrentAccount"] == null)
                {
                    IContainerContext context = ContainerManager.GetContainer("container");
                    HttpContext.Current.Session["CurrentAccount"]=new object();
                    //HttpContext.Current.Session["CurrentAccount"] = context.Resolve<IUserService>().GetAdminUserByUserName(HttpContext.Current.User.Identity.Name);//.GetUserByUserName(HttpContext.Current.User.Identity.Name); //
                    HttpContext.Current.Session["CurrentAccount"] = context.Resolve<IUserService>().GetUserByUserName(HttpContext.Current.User.Identity.Name);
                }
                return HttpContext.Current.Session["CurrentAccount"] as UserModel;
            }
            set
            {
                HttpContext.Current.Session["CurrentAccount"] = value;
            }
        }

        public static void ClearAccount()
        {
            //HttpContext.Current.Session["CurrentUser"] = null; 
            HttpContext.Current.Session["CurrentAccount"] = null;
            HttpContext.Current.Session.Abandon();
        }

        public static string LFAccount
        {
            get
            {
                if (HttpContext.Current.Session["LFAccount"] == null)
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

        public static void ClearLFAccount()
        {
            HttpContext.Current.Session["LFAccount"] = null;
            HttpContext.Current.Session.Abandon();
        }
    }
}
