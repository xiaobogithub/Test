using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class NewUserForApplication : PageBase<RegisterModel>
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
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ManageApplicationSecurity.aspx";
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
            // gender dropdownlist
            RegisterModel rm = new RegisterModel();
            rm.GenderList = new SortedList<int, string>();
            rm.GenderList.Add((int)GenderEnum.Male, GetLocalResourceObject("Male").ToString());
            rm.GenderList.Add((int)GenderEnum.Female, GetLocalResourceObject("Female").ToString());

            genderDropdownList.DataSource = rm.GenderList;
            genderDropdownList.DataTextField = "Value";
            genderDropdownList.DataValueField = "Key";
            genderDropdownList.DataBind();

            List<UserTypeModel> userTypes = Resolve<IUserService>().GetAllUserTypes();
            ddlUserType.DataSource = userTypes;
            ddlUserType.DataBind();
            
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
                    //bool isSMSLogin = IsSMSLoginCheckBox.Checked;
                    UserModel userModel = new UserModel();
                    userModel.UserName = emailTextBox.Text.Trim();
                    userModel.PassWord = passwordTextBox.Text.Trim();
                    userModel.PhoneNumber = mobilePhoneTextBox.Text.Trim();
                    userModel.Gender = (GenderEnum)genderDropdownList.SelectedIndex;
                    userModel.FirstName = firstNameTextBox.Text.Trim();
                    userModel.LastName = lastNameTextBox.Text.Trim();
                    userModel.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue);
                    userModel.IsSMSLogin = IsSMSLoginCheckBox.Checked;

                    Guid newUserGuid = Resolve<IUserService>().NewApplicationUser(userModel);
                    if (newUserGuid != Guid.Empty)
                    {
                        if (IsSendMailCheckBox.Checked)
                        {
                            Resolve<IEmailService>().SendLoginInfoToNewAdmin(newUserGuid);
                        }
                        Response.Redirect(string.Format("ManageApplicationSecurity.aspx"));
                    }
                    else
                    {
                        msgLbl.Text = GetLocalResourceObject("AccountExist").ToString();
                    }                    
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageApplicationSecurity.aspx");
        }
    }
}
