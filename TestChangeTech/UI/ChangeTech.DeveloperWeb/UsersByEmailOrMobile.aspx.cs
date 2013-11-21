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
    public partial class UsersByEmailOrMobile : PageBase<CompanyUserInfoModel>
    {
        private string ProgramGuid
        {
            get
            {
                if (ViewState[Constants.QUERYSTR_PROGRAM_GUID] == null)
                {
                    ProgramModel currentProgramModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
                    ViewState[Constants.QUERYSTR_PROGRAM_GUID] = currentProgramModel.Guid;
                }
                return ViewState[Constants.QUERYSTR_PROGRAM_GUID].ToString();
            }
        }

        private string ProgramName
        {
            get {
                ProgramModel currentProgramModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
                return currentProgramModel.ProgramName;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlProgram.Text = ProgramName;
                txtEmail.Text = Request.QueryString[Constants.QUERYSTR_USER_EMAIL];
                txtMobile.Text = Request.QueryString[Constants.QUERYSTR_USER_MOBILE];
                BindUserList();
                //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID], Request.QueryString[Constants.QUERYSTR_COMPANY_PAGE]);
            }
        }

        private void BindUserList()
        {
            int countOfCompanyUser = Resolve<IProgramUserService>().GetCountOfUserByEmailOrMobile(new Guid(ProgramGuid), txtEmail.Text, txtMobile.Text);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(countOfCompanyUser) / PageSize), "Day", "asc");
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            CompanyUserRepeater.DataSource = Resolve<IProgramUserService>().GetUsersByEmailOrMobile(new Guid(ProgramGuid), txtEmail.Text, txtMobile.Text, CurrentPageNumber, PageSize);
            CompanyUserRepeater.DataBind();
        }

        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                string programuserGuid = ((Button)sender).CommandArgument;
                Resolve<IProgramService>().DeleteProgramUser(new Guid(programuserGuid));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void EditUser_Click(object sender, EventArgs e)
        {
            try
            {
                string programuserGuid = ((Button)sender).CommandArgument;
                Response.Redirect(string.Format("CompanyUserInfoPage.aspx?{0}={1}&{2}={3}&{4}={5}",
                    Constants.QUERYSTR_PROFRAM_USER_GUID, programuserGuid, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            //can't use BindUserList as the when click other pages, the url contents the wrong email and mobile. need to recreate the url.
            //BindUserList();
            Response.Redirect(string.Format("UsersByEmailOrMobile.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID], Constants.QUERYSTR_COMPANY_PAGE, Request.QueryString[Constants.QUERYSTR_COMPANY_PAGE], Constants.QUERYSTR_USER_EMAIL, txtEmail.Text, Constants.QUERYSTR_USER_MOBILE, txtMobile.Text));

        }
    }
}