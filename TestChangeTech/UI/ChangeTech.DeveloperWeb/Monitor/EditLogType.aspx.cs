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
    public partial class EditLogType : PageBase<ActivityLogTypeModel>
    {
        private string LogTypeID { get { return Request.QueryString[Constants.QUERYSTR_LOG_TYPE_ID]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialPage();
            }
        }

        private void InitialPage()
        {
            Model = Resolve<IActivityLogService>().GetLogType(Convert.ToInt32(LogTypeID));
            ltlLogTypeName.Text = Model.Name;
            InitialDropDown();
            logPriorityDropDownList.SelectedValue = Model.LogPriority.ID.ToString();
        }

        private void InitialDropDown()
        {
            logPriorityDropDownList.Items.Clear();
            ActivityLogPriorityModels models = Resolve<IActivityLogService>().GetLogPriorities();
            foreach (ActivityLogPriorityModel item in models)
            {
                logPriorityDropDownList.Items.Add(new ListItem(item.Name.ToString(), item.ID.ToString()));
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            ActivityLogTypeModel model = new ActivityLogTypeModel();
            model.ID = Convert.ToInt32(LogTypeID);
            model.Name = ltlLogTypeName.Text;
            model.LogPriority = Resolve<IActivityLogService>().GetLogPriority(Convert.ToInt32(logPriorityDropDownList.SelectedItem.Value));
            Resolve<IActivityLogService>().UpdateLogType(model);
            Response.Redirect(string.Format("EditLogType.aspx?{0}={1}", Constants.QUERYSTR_LOG_TYPE_ID, LogTypeID));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListLogType.aspx");
        }
    }
}