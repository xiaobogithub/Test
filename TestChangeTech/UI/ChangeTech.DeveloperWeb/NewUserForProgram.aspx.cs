using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using System.Web.UI.WebControls;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class NewUser : PageBase<RegisterModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.QUERYSTR_PROGRAM_GUID))
                {
                    ClientScript.RegisterStartupScript(GetType(), "js" + emailTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + emailTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                    ClientScript.RegisterStartupScript(GetType(), "js" + passwordTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + passwordTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                    ClientScript.RegisterStartupScript(GetType(), "js" + confirmPasswordTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + confirmPasswordTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                    ClientScript.RegisterStartupScript(GetType(), "js" + mobilePhoneTextBox.ID, "ValidatorHookupEvent(document.getElementById(\"" + mobilePhoneTextBox.ClientID + "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);

                    if (!IsPostBack)
                    {
                        BindModel();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageProgramSecurity.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                    }
                }
                else
                {
                    Response.Redirect("InvalidUrl.aspx");
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        private void BindModel()
        {
            // mail time dropdownlist
            mailTimeDropDownList.Items.Clear();
            for (int i = 1; i < 25; i++)
            {
                ListItem item = new ListItem(i + ":00", i.ToString());
                mailTimeDropDownList.Items.Add(item);
            }

            // gender dropdownlist
            RegisterModel rm = new RegisterModel();
            rm.GenderList = new SortedList<int, string>();
            rm.GenderList.Add((int)GenderEnum.Male, GetLocalResourceObject("Male").ToString());
            rm.GenderList.Add((int)GenderEnum.Female, GetLocalResourceObject("Female").ToString());

            genderDropdownList.DataSource = rm.GenderList;
            genderDropdownList.DataTextField = "Value";
            genderDropdownList.DataValueField = "Key";
            genderDropdownList.DataBind();

            //LanguageDropdownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            //LanguageDropdownList.DataBind();
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    //string userName = emailTextBox.Text.Trim();
                    //string password = passwordTextBox.Text.Trim();
                    //Gender gender = (Gender)genderDropdownList.SelectedIndex;
                    //string mobilePhone = mobilePhoneTextBox.Text.Trim();
                    //string firstName = firstNameTextBox.Text.Trim();
                    //string lastName = lastNameTextBox.Text.Trim();
                    //int userType = Convert.ToInt32(ddlUserType.SelectedValue);
                    //Guid programGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);

                    UserModel uModel = new UserModel();
                    uModel.UserName = emailTextBox.Text.Trim();
                    uModel.PassWord = passwordTextBox.Text.Trim();
                    uModel.Gender = (GenderEnum)genderDropdownList.SelectedIndex;
                    uModel.PhoneNumber = mobilePhoneTextBox.Text.Trim();
                    uModel.FirstName = firstNameTextBox.Text.Trim();
                    uModel.LastName = lastNameTextBox.Text.Trim();
                    uModel.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue);
                    uModel.ProgramGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);

                    Guid newUserGuid = Resolve<IUserService>().NewUser(uModel);
                    int mailTime = Convert.ToInt32(mailTimeDropDownList.SelectedValue);

                    Resolve<IProgramService>().JoinProgram(uModel.ProgramGuid, newUserGuid, Guid.Empty, mailTime, Request.UserHostAddress, ProgramUserStatusEnum.Registered,"");

                    if (IsSendMailCheckBox.Checked)
                    {
                        Resolve<IEmailService>().SendRegisterEmail(newUserGuid, uModel.ProgramGuid);
                    }
                    //feedbackPanel.Visible = true;
                    Response.Redirect(string.Format("ManageProgramSecurity.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void IsUserNameUnique(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = Resolve<IUserService>().IsValidUserName(emailTextBox.Text.Trim(), new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageProgramSecurity.aspx");
        }
    }
}
