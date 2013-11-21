using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ChangeTech.Contracts;
using Ethos.Utility;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ActivityLogList : PageBase<ActivityLogModels>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListProgram.aspx";
                try
                {
                    InitialPage();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, string.Format("Page Name:{0}", "AcitivityLogList"));
                    throw ex;
                }
            }
        }

        //protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect(string.Format("ActivityLogList.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ddlProgram.SelectedValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, string.Format("Page Name:{0}", "AcitivityLogList"));
        //        throw ex;
        //    }
        //}

        //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(InitialURL());
        //}

        //protected void UserTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(InitialURL());
        //}

        //protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(InitialURL());
        //}

        private void InitialPage()
        {
            FormatDDL();
            if (Request.QueryString[Constants.QUERYSTR_USER_EMAIL] != null)
            {
                UserTextBox.Text = Request.QueryString[Constants.QUERYSTR_USER_EMAIL];
            }
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
            FormatRepeater();
        }

        private void FormatDDL()
        {
            BindProgram();
            BindLanguage();
            FormatSessionDDL();
        }

        private void BindProgram()
        {
            List<SimpleProgramModel> programs = Resolve<IProgramService>().GetSimpleProgramsModel();
            programs.Insert(0, new SimpleProgramModel { ProgramGuid = Guid.Empty, ProgramName = "All" });
            ddlProgram.DataSource = programs;
            ddlProgram.DataBind();
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                ddlProgram.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private void BindLanguage()
        {
            if (ddlProgram.SelectedValue != Guid.Empty.ToString())
            {
                ddlLanguage.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
                ddlLanguage.DataTextField = "Name";
                ddlLanguage.DataValueField = "LanguageGUID";
                ddlLanguage.DataBind();
                if (Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID] != null)
                {
                    ddlLanguage.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
                }
            }
            else
            {
                ddlLanguage.Items.Clear();
            }
        }

        private void FormatSessionDDL()
        {
            if (ddlProgram.SelectedValue != Guid.Empty.ToString())
            {
                ProgramModel program = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ddlProgram.SelectedValue), new Guid(ddlLanguage.SelectedValue));
                List<SimpleSessionModel> listSession = Resolve<ISessionService>().GetSimpleSessionsByProgramGuid(program.Guid);
                listSession.Insert(0, new SimpleSessionModel { ID = Guid.Empty, Name = "All" });
                ddlSession.DataSource = listSession;
                ddlSession.DataBind();
                if (Request.QueryString[Constants.QUERYSTR_SESSION_GUID] != null)
                {
                    ddlSession.SelectedValue = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];
                }
            }
            else
            {
                ddlSession.Items.Clear();
            }

            if (Request.QueryString[Constants.QUERYSTR_USERSTATUS] != null)
            {
                ddlUserStatus.SelectedValue = Request.QueryString[Constants.QUERYSTR_USERSTATUS];
            }
        }

        private void FormatRepeater()
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
            Guid programGuid = Guid.Empty;
            Guid sessionGuid = Guid.Empty;
            if (ddlProgram.SelectedValue != Guid.Empty.ToString())
            {
                ProgramModel program = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ddlProgram.SelectedValue), new Guid(ddlLanguage.SelectedValue));
                programGuid = program.Guid;

                sessionGuid = new Guid(ddlSession.SelectedValue);
            }

            int logItemsCount = Resolve<IActivityLogService>().GetItemsCount(UserTextBox.Text.Trim(), programGuid
                , sessionGuid, Begin, End, Convert.ToInt32(ddlActivityType.SelectedValue), ddlUserStatus.Text);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(logItemsCount) / PageSize), "UserName", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            Model = Resolve<IActivityLogService>().GetItems(UserTextBox.Text.Trim(), programGuid
                , sessionGuid, Begin, End, Convert.ToInt32(ddlActivityType.SelectedValue), CurrentPageNumber, PageSize, ddlUserStatus.SelectedValue);

            rptLog.DataSource = Model;
            rptLog.DataBind();
        }

        private string InitialURL()
        {
            string urlStr = string.Format("ActivityLogList.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ddlProgram.SelectedValue);
            if (ddlSession.Items.Count > 0 && ddlSession.SelectedValue != Guid.Empty.ToString())
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_SESSION_GUID, ddlSession.SelectedValue);
            }
            if (!string.IsNullOrEmpty(UserTextBox.Text))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_USER_EMAIL, UserTextBox.Text.Trim());
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
            if (ddlLanguage.Items.Count > 0 && !string.IsNullOrEmpty(ddlLanguage.SelectedValue))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_LANGUAGE_GUID, ddlLanguage.SelectedValue);
            }
            if (!string.IsNullOrEmpty(ddlUserStatus.SelectedValue))
            {
                urlStr = MakeUpStr(urlStr, Constants.QUERYSTR_USERSTATUS, ddlUserStatus.SelectedValue);
            }

            return urlStr;
        }

        private string MakeUpStr(string urlStr, string queryStr, string value)
        {
            return string.Format(urlStr + "&{0}={1}", queryStr, value);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ActivityLogList.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ddlProgram.SelectedValue));
            //BindLanguage();
            //FormatSessionDDL();
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatSessionDDL();
        }

        //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect(InitialURL());
        //}

        protected void ddlUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void UserTextBox_TextChanged(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void txtBegin_TextChanged(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void txtEnd_TextChanged(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(InitialURL());
        }
    }
}