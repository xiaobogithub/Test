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
    public partial class InsertProgramToStudy : PageBase<SimpleProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindProgram();
                ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID,Request.QueryString[Constants.QUERYSTR_STUDY_GUID]);
            }
        }

        private void BindProgram()
        {
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_NAME]))
            {
                programNameTextBox.Text = Request.QueryString[Constants.QUERYSTR_PROGRAM_NAME];
            }

            int countOfProgram = Resolve<IProgramService>().GetAllNumberOfProgram(programNameTextBox.Text.Trim());
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(countOfProgram) / 15), "Name", "asc");
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            programRepeater.DataSource = Resolve<IProgramService>().GetSimplePrograms(CurrentPageNumber, 15, programNameTextBox.Text.Trim());
            programRepeater.DataBind();
        }

        protected void selectButton_Click(object sender, EventArgs e)
        {
            Guid programGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<IStudyContentService>().AddNewStudyContent(new InsertStudyContentModel {StudyGUID = new Guid(Request.QueryString[Constants.QUERYSTR_STUDY_GUID]) });
            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, Request.QueryString[Constants.QUERYSTR_STUDY_GUID]));
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("InsertProgramToStudy.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_STUDY_GUID, Request.QueryString[Constants.QUERYSTR_STUDY_GUID], Constants.QUERYSTR_PROGRAM_NAME, programNameTextBox.Text));
        }
    }
}
