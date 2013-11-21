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
    public partial class ManageUserCompany : PageBase<CompanyRightModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindCompanyRight();
                        BindNotJoinCompanyList();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void addCompanyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddCompany.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            string companyguid = ((Button)sender).CommandArgument;
            CompanyRightModel rightmodel = new CompanyRightModel
            {
                ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                CompanyRightGUID = Guid.NewGuid(),
                CompanyGUID = new Guid(companyguid),
                OverDueTime = DateTime.UtcNow.AddMonths(1),
                StartTime = DateTime.UtcNow
            };
            Resolve<ICompanyService>().AddCompanyRight(rightmodel);

            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string companyrightguid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditCompanyRight.aspx?{0}={1}", Constants.QUERYSTR_COMPANY_RIGHT_GUID, companyrightguid));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string companyrightguid = ((Button)sender).CommandArgument;
            Resolve<ICompanyService>().DeleteCompanyRight(new Guid(companyrightguid));
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        private void BindCompanyRight()
        {
            joinedCompanyRepeater.DataSource = Resolve<ICompanyService>().GetCompanyRightListByProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            joinedCompanyRepeater.DataBind();
        }

        private void BindNotJoinCompanyList()
        {
            NotJoinRepeater.DataSource = Resolve<ICompanyService>().GetCompanyListNotJoinProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            NotJoinRepeater.DataBind();
        }
    }
}
