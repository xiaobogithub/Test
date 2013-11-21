using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Web.UI.WebControls;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListLogType : PageBase<ActivityLogTypeModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLogType();
            }
        }

        private void BindLogType()
        {
            rpLogType.DataSource = Resolve<IActivityLogService>().GetLogTypes();
            rpLogType.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditLogType.aspx?{0}={1}", Constants.QUERYSTR_LOG_TYPE_ID, ((Button)sender).CommandArgument));
        }
    }
}