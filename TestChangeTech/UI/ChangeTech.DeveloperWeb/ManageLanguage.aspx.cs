using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageLanguage : PageBase<ManageLanguageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProgramLanguage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Default.aspx";
            }
        }

        private void BindProgramLanguage()
        {
            rpLanguage.DataSource = Resolve<ILanguageService>().GetAllProgramLanguageModel();
            rpLanguage.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddLanguage.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        { 
        }
    }
}
