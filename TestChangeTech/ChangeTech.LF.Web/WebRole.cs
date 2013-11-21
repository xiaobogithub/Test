using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure;

namespace ChangeTech.LF.Web
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            return base.OnStart();
        }
    }
}