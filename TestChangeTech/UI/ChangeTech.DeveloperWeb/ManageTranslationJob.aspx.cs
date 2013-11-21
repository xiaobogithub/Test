using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageTranslationJob : PageBase<TranslationJobModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTranslationJobList();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "Home.aspx";
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTranslationJob.aspx");
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditTranslationJob.aspx?{0}={1}", Constants.QUERYSTR_TRANSLATION_JOB_GUID, ((Button)sender).CommandArgument));
        }

        protected void statsButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect(string.Format("ManageTranslationJobContent.aspx?{0}={1}", Constants.QUERYSTR_TRANSLATION_JOB_GUID, ((Button)sender).CommandArgument));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            Guid translationJobGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<ITranslationJobService>().DeleteTranslationJob(translationJobGuid);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            Guid translationJobGuid = new Guid(((Button)sender).CommandArgument);
            Resolve<ITranslationJobService>().UpdateTranslationJobElementsToDefaultContent(translationJobGuid);

            Response.Redirect(Request.Url.PathAndQuery);
        }

        private void BindTranslationJobList()
        {
            translationJobRepeater.DataSource = Resolve<ITranslationJobService>().GetTranslationJobList();
            translationJobRepeater.DataBind();
        }
    }
}