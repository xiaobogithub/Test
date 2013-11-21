using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChangeTech.DeveloperWeb
{
    public partial class ImageManager : System.Web.UI.Page
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
            Silverlight1.Source = string.Format("~/ClientBin/ChangeTech.Silverlight.DesignPage.xap?Version={0}", VersionNumberWithoutDot);

            if (Request.QueryString[Constants.QUERYSTR_PRESENTERIMAGE_MODE] != null)
            {
                switch (Request.QueryString[Constants.QUERYSTR_PRESENTERIMAGE_MODE].ToString().ToLower())
                {
                    case "ctpppresenterimage":
                        Silverlight1.InitParameters = "Mode=CTPPPresenterImage,Azure=changetechstorage";
                        break;
                    case "pagepresenterimage":
                        Silverlight1.InitParameters = "Mode=PagePresenterImage,Azure=changetechstorage";
                        break;

                }
            }
        }
    }
}