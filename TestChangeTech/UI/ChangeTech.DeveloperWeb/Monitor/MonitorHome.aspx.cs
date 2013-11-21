﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb
{
    public partial class MonitorHome : System.Web.UI.Page
    {
        public string VersionNumber
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
                return version;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}