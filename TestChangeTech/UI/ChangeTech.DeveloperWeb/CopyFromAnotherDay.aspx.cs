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
    public partial class CopyFromAnotherDay : PageBase<ProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]);
            }
        }

        private void InitialPage()
        {
            Bindprogram();
            Bindlanguage();
            BindSessionList();
        }

        private void Bindlanguage()
        {
            if(!string.IsNullOrEmpty(programDropDownList.SelectedValue))
            {
                languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
                languageDropDownList.DataTextField = "Name";
                languageDropDownList.DataValueField = "LanguageGUID";
                languageDropDownList.DataBind();
            }
        }

        private void Bindprogram()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
        }

        private void BindSessionList()
        {
            if(!string.IsNullOrEmpty(programDropDownList.SelectedValue) && !string.IsNullOrEmpty(languageDropDownList.SelectedValue))
            {
                sessionDropDownList.DataSource = Resolve<ISessionService>().GetSimpleSessionsByProgramGuid(Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(programDropDownList.SelectedValue), new Guid(languageDropDownList.SelectedValue)).Guid);
                sessionDropDownList.DataTextField = "NameWithDayNum";
                sessionDropDownList.DataValueField = "ID";
                sessionDropDownList.DataBind();
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            Resolve<ISessionService>().CopyFromAnotherDay(new Guid(sessionDropDownList.SelectedValue), new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]));
            Response.Redirect(string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]));
        }

        protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindlanguage();
            BindSessionList();
        }

        protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSessionList();
        }
    }
}
