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
    public partial class ManageSM : PageBase<MessageModel>
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ProgramGUID))
            {
                if(!IsPostBack)
                {
                    InitialPage();
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPage()
        {
            Model = Resolve<IShortMessageService>().GetMessageByProgramAndType(new Guid(ProgramGUID), messageTypeLabel.Text);
            if(Model.MessageGUID != Guid.Empty)
            {
                textTextBox.Text = Model.Text;
                okButton.Text = "Update";
            }
            else
            {
                okButton.Text = "Add";
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            if(okButton.Text == "Add")
            {
                Resolve<IShortMessageService>().AddMessage(new MessageModel
                {
                    Type = messageTypeLabel.Text,
                    Text = textTextBox.Text,
                    ProgramGUID = new Guid(ProgramGUID)
                });
            }
            else
            {
                Resolve<IShortMessageService>().UpdateMessage(new MessageModel
                {
                    Text = textTextBox.Text,
                    Type = messageTypeLabel.Text,
                    ProgramGUID = new Guid(ProgramGUID)
                });
            }
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        }
    }
}
