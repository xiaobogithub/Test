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
    public partial class AddLanguage : PageBase<ManageLanguageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ManageLanguage.aspx";
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Resolve<ILanguageService>().AddLanguage(languageNameTextBox.Text);
            Response.Redirect("ManageLanguage.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageLanguage.aspx");
        }
    }
}
