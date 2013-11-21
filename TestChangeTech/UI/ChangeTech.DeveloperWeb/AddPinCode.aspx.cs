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
    public partial class AddPinCode : PageBase<AccessoryPageModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
        }

        private void InitialPage()
        {
            Model = Resolve<IProgramAccessoryService>().GetProgramAccessoryByProgarm(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), "PinCode");
            if(Model.AccessoryTemplateGUID != Guid.Empty)
            {
                titleTextBox.Text = Model.Heading;
                textTextBox.Text = Model.Text;
                pinTextBox.Text = Model.UserNameText;
                primaryButtonTextBox.Text = Model.PrimaryButtonText;
                okButton.Text = "Update";
                okButton.CommandArgument = Model.AccessoryTemplateGUID.ToString();
            }
            else
            {
                okButton.Text = "Add";
            }
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            Button okbutton = sender as Button;

            if(!string.IsNullOrEmpty(okbutton.CommandArgument))
            {
                AccessoryPageModel pagemodel = new AccessoryPageModel
                {
                    AccessoryTemplateGUID = new Guid(okbutton.CommandArgument),
                    Heading = titleTextBox.Text,
                    Text = textTextBox.Text,
                    PasswordText = pinCodeReminderTextBox.Text,
                    Type = "PinCode",
                    UserNameText = pinTextBox.Text,
                    PrimaryButtonText = primaryButtonTextBox.Text,
                    ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])
                };
                Resolve<IProgramAccessoryService>().UpdateAccessory(pagemodel);
            }
            else
            {
                AccessoryPageModel pagemodel = new AccessoryPageModel
                {
                    //AccessoryTemplateGUID = Guid.NewGuid(),
                    Heading = titleTextBox.Text,
                    Text = textTextBox.Text,
                    Type = "PinCode",
                    UserNameText = pinTextBox.Text,
                    PrimaryButtonText = primaryButtonTextBox.Text,
                    ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])
                };

                Resolve<IProgramAccessoryService>().AddAccessroy(pagemodel);
            }

            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
