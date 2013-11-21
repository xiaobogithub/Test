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
    public partial class AddStudyContent : PageBase<InsertStudyContentModel>
    {
        private string StudyGuid { get { return Request.QueryString[Constants.QUERYSTR_STUDY_GUID]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid);
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            InsertStudyContentModel scModel = new InsertStudyContentModel
            {
                StudyGUID = new Guid(StudyGuid),
                RouteURL = urlTextBox.Text.Trim()
            };

            Resolve<IStudyContentService>().AddNewStudyContent(scModel);
            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, StudyGuid));
        }
    }
}
