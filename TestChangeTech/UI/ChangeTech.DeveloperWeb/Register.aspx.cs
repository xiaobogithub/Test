using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using System.Web.UI.WebControls;

namespace ChangeTech.DeveloperWeb
{
    public partial class Register : PageBase<RegisterModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "js" + emailTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + emailTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
            ClientScript.RegisterStartupScript(GetType(), "js" + passwordTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + passwordTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
            ClientScript.RegisterStartupScript(GetType(), "js" + confirmPasswordTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + confirmPasswordTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
            ClientScript.RegisterStartupScript(GetType(), "js" + mobilePhoneTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + mobilePhoneTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);

            if (!IsPostBack)
            {
                BindModel();
                ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = "ListProgram.aspx";
            }
        }

        private void BindModel()
        {
            RegisterModel rm = new RegisterModel();
            rm.GenderList = new SortedList<int, string>();
            rm.GenderList.Add((int)Gender.Male, GetLocalResourceObject("Male").ToString());
            rm.GenderList.Add((int)Gender.Female, GetLocalResourceObject("Female").ToString());

            genderDropdownList.DataSource = rm.GenderList;
            genderDropdownList.DataTextField = "Value";
            genderDropdownList.DataValueField = "Key";
            genderDropdownList.DataBind();
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string userName = emailTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();
                Gender gender = (Gender)genderDropdownList.SelectedIndex;
                string mobilePhone = mobilePhoneTextBox.Text.Trim();
                string firstName = firstNameTextBox.Text.Trim();
                string lastName = lastNameTextBox.Text.Trim();
                Guid newUserGuid = Resolve<IUserService>().Register(userName, password, mobilePhone, gender, firstName, lastName);
                Guid ProgramGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);

                Resolve<IProgramService>().JoinProgram(ProgramGuid, newUserGuid);

                if (IsSendMailCheckBox.Checked)
                {
                    Resolve<IEmailService>().SendRegisterEmail(userName, password);
                }
                //feedbackPanel.Visible = true;
                Response.Redirect("ListProgram.aspx");
            }
        }

        protected void IsUserNameUnique(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Resolve<IUserService>().IsValidUserName(emailTextBox.Text.Trim());
        }
    }
}
