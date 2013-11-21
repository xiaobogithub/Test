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
    public partial class AddCompany : PageBase<CompanyModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageUserCompany.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            CompanyModel companymodel = new CompanyModel
            {
                CompanyGUID = Guid.NewGuid(),
                Description = desTextBox.Text,
                Name = nameTextBox.Text
            };
            Resolve<ICompanyService>().AddCompany(companymodel);
            BackToManageCompanyPage();
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            BackToManageCompanyPage();
        }

        private void BackToManageCompanyPage()
        {
            Response.Redirect(string.Format("ManageUserCompany.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
