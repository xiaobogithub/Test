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
    public partial class TranslatorHome : PageBase<TranslationJobModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTranslationJobList();
            }
        }

        protected void openButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageTranslationJobContent.aspx?{0}={1}", Constants.QUERYSTR_TRANSLATION_JOB_GUID, ((Button)sender).CommandArgument));
        }

        private void BindTranslationJobList()
        {
            translationJobRepeater.DataSource = Resolve<ITranslationJobService>().GetTranslationJobList(Resolve<IUserService>().GetCurrentUser().UserGuid);
            translationJobRepeater.DataBind();
        }
    }
}