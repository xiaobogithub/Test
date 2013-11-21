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
    public partial class EditStudyContent : PageBase<StudyContentModel>
    {
        private string StudyGuid { get { return Request.QueryString[Constants.QUERYSTR_STUDY_GUID]; } }
        private string StudyContentGuid { get { return Request.QueryString[Constants.QUERYSTR_STUDY_CONTENT_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid);
            }
        }

        private void InitialPage()
        {
            Model = Resolve<IStudyContentService>().GetStudyContentByGuid(new Guid(StudyContentGuid));
            urlTextBox.Text = Model.RouteURL;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            StudyContentModel scModel = new StudyContentModel
            {
                StudContentGUID = new Guid(StudyContentGuid),
                RouteURL = urlTextBox.Text.Trim()
            };
            Resolve<IStudyContentService>().UpdateStudyContent(scModel);

            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid));
        }
    }
}
