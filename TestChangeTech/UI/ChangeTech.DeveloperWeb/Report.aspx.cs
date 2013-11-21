using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Text;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class Report : PageBase<ReportModel>
    {
        public Guid ProgramGUID { get { return new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]); } }
        public Guid UserGroupGUID { get { return new Guid(UserGroupDropDownList.SelectedValue); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if(!IsPostBack)
                {
                    try
                    {
                        BindProgramDropDownList();
                        BindReportGridView();
                    }
                    catch(Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindProgramDropDownList()
        {
            List<CompanyRightModel> gruops = Resolve<ICompanyService>().GetCompanyRightListByProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            gruops.Insert(0, new CompanyRightModel { CompanyName = "All", CompanyGUID = Guid.Empty });
            UserGroupDropDownList.DataSource = gruops;
            UserGroupDropDownList.DataTextField = "CompanyName";
            UserGroupDropDownList.DataValueField = "CompanyGUID";
            UserGroupDropDownList.DataBind();
        }

        private void BindReportGridView()
        {
            ReportGridView.DataSource = Resolve<IReportService>().GetReportItems(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(UserGroupDropDownList.SelectedValue), "", true);
            ReportGridView.DataBind();

            if(Resolve<IProgramService>().IsProgramSeparateGender(ProgramGUID))
            {
                separatePanel.Visible = true;
                maleGridView.DataSource = Resolve<IReportService>().GetReportItems(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(UserGroupDropDownList.SelectedValue), GenderEnum.Male.ToString(), true);
                maleGridView.DataBind();
                femaleGridView.DataSource = Resolve<IReportService>().GetReportItems(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(UserGroupDropDownList.SelectedValue), GenderEnum.Female.ToString(), true);
                femaleGridView.DataBind();
            }
        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            Ethos.Utility.ExportExcel.ExportExcelFromGridView(ReportGridView, "Report");
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }

        protected void UserGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindReportGridView();
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
