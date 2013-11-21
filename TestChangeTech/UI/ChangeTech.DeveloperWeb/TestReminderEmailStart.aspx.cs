using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class TestReminderEmailStart : PageBase<SimpleProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //This function is no use now. For that the function begins to use cloud queue.
            //Resolve<IProgramUserService>().SendEmailToUser(Convert.ToDateTime("2011-5-2 18:31:57.890"));
        }
    }
}
