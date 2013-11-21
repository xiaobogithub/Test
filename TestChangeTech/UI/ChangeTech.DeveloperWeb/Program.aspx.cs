using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class Program : PageBase<SimpleProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
                {
                    List<SimpleProgramModel> programs = Resolve<IProgramService>().GetAllSimpleProgramModel();
                    string[] uslstring = Request.Url.ToStringWithoutPort().Split('?');
                    string URL = string.Empty;
                    if (programs != null)
                    {
                        foreach (SimpleProgramModel spm in programs)
                        {
                            if (spm.ProgramName.Equals(Request.QueryString["Name"]))
                            {
                                string webRoot = uslstring[0].Replace("Program.aspx", "ChangeTech.html");
                                URL = string.Format("{0}?" + Constants.QUERYSTR_PROGRAM_GUID + "={1}&Mode=Trial", webRoot, spm.ProgramGuid);
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(URL))
                    {
                        Response.Redirect(URL);
                    }
                    else
                    {
                        Context.Response.Write("Invalidate URL");
                    }
                }
            }        
        }
    }
}
