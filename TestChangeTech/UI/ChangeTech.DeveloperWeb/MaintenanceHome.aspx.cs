using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class MaintenanceHome : PageBase<CompanyRightModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
               // ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = Constants.URL_MAINTENANCE_HOME;
            }
        }

        private void InitialPage()
        {
            InitialProgramDropDownList();
            BindListView();
        }

        private void BindListView()
        {
            if(programDropDownList.Items.Count > 0 && languageDropDownList.Items.Count > 0)
            {
                ProgramModel program = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(programDropDownList.SelectedValue), new Guid(languageDropDownList.SelectedValue));
                //ViewState[Constants.QUERYSTR_PROGRAM_GUID] = program.Guid;
                if(program != null)
                {
                    int numberOfProgram = Resolve<ICompanyService>().GetCompanyCountByProgram(program.Guid);
                    InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfProgram) / PageSize), "Name", "asc");

                    PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

                    companyRepeater.DataSource = Resolve<ICompanyService>().GetCompanyRightListByProgram(program.Guid, CurrentPageNumber, PageSize);
                    companyRepeater.DataBind();
                }
            }
        }

        private void InitialProgramDropDownList()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramUserHasPermission(ContextService.CurrentAccount.UserGuid);
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
            if(!string.IsNullOrEmpty(programDropDownList.SelectedValue))
            {
                programDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }

            BindLanguageDropDown();
        }

        private void BindLanguageDropDown()
        {
            if(!string.IsNullOrEmpty(programDropDownList.SelectedValue))
            {
                languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
                languageDropDownList.DataTextField = "Name";
                languageDropDownList.DataValueField = "LanguageGUID";
                languageDropDownList.DataBind();
                if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]))
                {
                    languageDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
                }
            }
        }

        protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindLanguageDropDown();
            //BindListView();
            Response.Redirect(string.Format("MaintenanceHome.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue));
        }

        protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MaintenanceHome.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddCompay3rdParty.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string rightGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditCompany3rdParty.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_COMPANY_RIGHT_GUID, rightGuid, Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string rightGuid = ((Button)sender).CommandArgument;
            Resolve<ICompanyService>().DeleteCompanyRight(new Guid(rightGuid));

            BindListView();
        }

        protected void showButton_Click(object sernder, EventArgs e)
        {
            if (string.IsNullOrEmpty(programDropDownList.SelectedValue) || string.IsNullOrEmpty(languageDropDownList.SelectedValue))
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Warning", "<script type='text/javascript'>alert('The current user has not permission!');</script>");
            }
            else
            {
                Response.Redirect(string.Format("EditNullCompany3rdParty.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
            }
        }

        protected void btnUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(programDropDownList.SelectedValue) || string.IsNullOrEmpty(languageDropDownList.SelectedValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Warning", "<script type='text/javascript'>alert('The current user has not permission!');</script>");
            }
            else
            {
                Response.Redirect(string.Format("UsersByEmailOrMobile.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue, Constants.QUERYSTR_COMPANY_PAGE, CurrentPageNumber, Constants.QUERYSTR_USER_EMAIL, txtEmail.Text, Constants.QUERYSTR_USER_MOBILE, txtMobile.Text));
            }
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
    }
}
