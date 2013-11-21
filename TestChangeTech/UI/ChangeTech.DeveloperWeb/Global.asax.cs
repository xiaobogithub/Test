using System;
using Ethos.DependencyInjection;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

namespace ChangeTech.DeveloperWeb
{
    public class Global : System.Web.HttpApplication
    {
        private IContainerContext container = ContainerManager.GetContainer("container");
        private static readonly string ObjectContextItemName = "ObjectContext";

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest()
        {
            if (HttpContext.Current.Items[ObjectContextItemName] != null)
            {
                IDisposable disposable = HttpContext.Current.Items[ObjectContextItemName] as IDisposable;
                disposable.Dispose();
                HttpContext.Current.Items[ObjectContextItemName] = null;
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Extract the forms authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                // There is no authentication cookie.
                return;
            }

            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                // Log exception details (omitted for simplicity)
                return;
            }

            if (null == authTicket)
            {
                // Cookie failed to decrypt.
                return;
            }

            // When the ticket was created, the UserData property was assigned a
            // pipe delimited string of role names.
            string role = authTicket.UserData;

            // Create an Identity object
            FormsIdentity id = new FormsIdentity(authTicket);

            // This principal will flow throughout the request.
            GenericPrincipal principal = new GenericPrincipal(id, new string[] { role });
            // Attach the new principal object to the current HttpContext object
            Context.User = principal;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Response.Redirect("ErrorPage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {           
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}