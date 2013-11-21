using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChangeTech.DeveloperWeb
{
    public partial class PageVariableList : System.Web.UI.Page
    {
        public string VersionNumberWithoutDot
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return version;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManagePageVariable.InitParameters = "Mode=PageVariable";
                ManagePageVariable.Source = string.Format("~/ClientBin/ChangeTech.Silverlight.DesignPage.xap?Version={0}", VersionNumberWithoutDot);
                //ManagePageVariable.
                //ManagePageVariable.Attributes.Add("UserGuid", ContextService.CurrentAccount.UserGuid.ToString());
            }
        }
    }
}
