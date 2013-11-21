using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class CompanyUserInfoPage : PageBase<ActivityLogModels>
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROFRAM_USER_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        //CompanyRightModel companyRightModel = Resolve<ICompanyService>().GetCompanyRightModelByGuid(new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));
                        BindSessionDDL(new Guid(ProgramGuid));
                        BindUserInfo(new Guid(Request.QueryString[Constants.QUERYSTR_PROFRAM_USER_GUID]));
                        InitializeFilterSetting();
                        BindActivityLogList(new Guid(ProgramGuid));
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = BackUrl();
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }



        private string BackUrl()
        {
            string backURL = string.Empty;
            if (string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]))
            {
                backURL = string.Format("UsersNotInCompany.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]);
            }
            else
            {
                backURL = string.Format("CompanyUsers.aspx?{0}={1}&{2}={3}&{4}={5}",
                         Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID],
                         Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                         Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]);
            }

            return backURL;
        }

        private void BindUserInfo(Guid programUserGuid)
        {
            CompanyUserInfoModel companyUserInfoModel = Resolve<IProgramUserService>().GetCompanyUserInfo(programUserGuid);
            emailTextBox.Text = companyUserInfoModel.Email;
            StatusDropdownList.SelectedValue = companyUserInfoModel.Status;
            firstNameTextBox.Text = companyUserInfoModel.FirstName;
            lastNameTextBox.Text = companyUserInfoModel.LastName;
            if (companyUserInfoModel.Gender.Trim().Equals("Female"))
            {
                genderDropDownList.SelectedIndex = 1;
            }
            phoneTextBox.Text = companyUserInfoModel.MobilePhone;
            registerDateLbl.Text = companyUserInfoModel.RegisterDate;
            currentDayLbl.Text = companyUserInfoModel.CurrentDay;
            pinCodeTextBox.Text = companyUserInfoModel.PinCode;
            StatusDropdownList.SelectedValue = companyUserInfoModel.Status;
        }

        private void InitializeFilterSetting()
        {
            if (Request.QueryString[Constants.QUERYSTR_ACTIVITY_TYPE] != null)
            {
                ddlActivityType.SelectedValue = Request.QueryString[Constants.QUERYSTR_ACTIVITY_TYPE];
            }
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_ENDTIME]))
            {
                txtEnd.Text = Request.QueryString[Constants.QUERYSTR_ENDTIME];
            }
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_BEGINTIME]))
            {
                txtBegin.Text = Request.QueryString[Constants.QUERYSTR_BEGINTIME];
            }
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_USERSTATUS]))
            {
                StatusDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_USERSTATUS];
            }
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]))
            {
                ddlSession.SelectedValue = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];
            }
        }

        private void BindActivityLogList(Guid programGuid)
        {
            DateTime Begin = DateTime.MinValue;
            DateTime End = DateTime.MinValue;
            if (txtBegin.Text.Trim() != "")
            {
                Begin = Convert.ToDateTime(txtBegin.Text + " 00:00:01");
            }
            if (txtEnd.Text.Trim() != "")
            {
                End = Convert.ToDateTime(txtEnd.Text + " 23:59:59");
            }

            // for paging and sorting            
            int logItemsCount = Resolve<IActivityLogService>().GetItemsCount(emailTextBox.Text.Trim(), programGuid
                , new Guid(ddlSession.SelectedValue), Begin, End, Convert.ToInt32(ddlActivityType.SelectedValue), StatusDropdownList.Text);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(logItemsCount) / PageSize), "UserName", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            Model = Resolve<IActivityLogService>().GetItems(emailTextBox.Text.Trim(), programGuid
                , new Guid(ddlSession.SelectedValue), Begin, End, Convert.ToInt32(ddlActivityType.SelectedValue), CurrentPageNumber, PageSize, StatusDropdownList.Text);

            rptLog.DataSource = Model;
            rptLog.DataBind();
        }

        private void BindSessionDDL(Guid programGuid)
        {

            List<SimpleSessionModel> listSession = Resolve<ISessionService>().GetSimpleSessionsByProgramGuid(programGuid);
            listSession.Insert(0, new SimpleSessionModel { ID = Guid.Empty, Name = "All" });
            ddlSession.DataSource = listSession;
            ddlSession.DataBind();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            CompanyUserInfoModel companyUserInfo = new CompanyUserInfoModel();
            companyUserInfo.Email = emailTextBox.Text.Trim();
            companyUserInfo.FirstName = firstNameTextBox.Text.Trim();
            companyUserInfo.LastName = lastNameTextBox.Text.Trim();
            companyUserInfo.Gender = genderDropDownList.SelectedValue;
            companyUserInfo.MobilePhone = phoneTextBox.Text.Trim();
            companyUserInfo.PinCode = pinCodeTextBox.Text.Trim();
            companyUserInfo.ProgramUserGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROFRAM_USER_GUID]);
            companyUserInfo.Status = StatusDropdownList.SelectedValue;
            Resolve<IProgramUserService>().UpdateCompanyUserInfo(companyUserInfo);
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        private string InitialURL()
        {
            string urlStr = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]))
            {
                urlStr = string.Format("CompanyUserInfoPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID],
                     Constants.QUERYSTR_PROFRAM_USER_GUID, Request.QueryString[Constants.QUERYSTR_PROFRAM_USER_GUID], Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                     Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]);
            }
            else
            {
                urlStr = string.Format("CompanyUserInfoPage.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PROFRAM_USER_GUID, Request.QueryString[Constants.QUERYSTR_PROFRAM_USER_GUID], Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]);
            }

            if (ddlSession.SelectedValue != Guid.Empty.ToString())
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_SESSION_GUID, ddlSession.SelectedValue);
            }
            if (ddlActivityType.SelectedValue != "0")
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_ACTIVITY_TYPE, ddlActivityType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtBegin.Text))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_BEGINTIME, txtBegin.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtEnd.Text))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_ENDTIME, txtEnd.Text.Trim());
            }
            if (!string.IsNullOrEmpty(StatusDropdownList.Text))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_USERSTATUS, StatusDropdownList.Text);
            }

            return urlStr;
        }

        private string MakeUpStr(string urlStr, string queryStr, string value)
        {
            return string.Format(urlStr + "&{0}={1}", queryStr, value);
        }
    }
}
