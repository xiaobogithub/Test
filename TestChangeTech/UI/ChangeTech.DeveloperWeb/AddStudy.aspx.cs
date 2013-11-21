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
    public partial class AddStudy : PageBase<StudyModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ListStudy.aspx";
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListStudy.aspx");
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            StudyModel studyModel = new StudyModel
            {
                Name = nameTextBox.Text,
                Description = descriptionTextBox.Text
            };

            Resolve<IStudyService>().AddNewStudy(studyModel);
            Response.Redirect("ListStudy.aspx");
        }
    }
}
