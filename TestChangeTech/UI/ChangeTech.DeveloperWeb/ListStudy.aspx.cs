using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListStudy : PageBase<StudyModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudyList();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "Home.aspx";
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudy.aspx");
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditStudy.aspx?{0}={1}", Constants.QUERYSTR_STUDY_GUID, ((Button)sender).CommandArgument));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Guid studyGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<IStudyService>().DeleteStudy(studyGuid);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        private void BindStudyList()
        {
            int numberOfStudy = Resolve<IStudyService>().GetNumberOfStudy();
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfStudy) / PageSize), "Name", "asc");

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            studyRepeater.DataSource = Resolve<IStudyService>().GetStudyList(CurrentPageNumber, PageSize);
            studyRepeater.DataBind();
        }
    }
}
