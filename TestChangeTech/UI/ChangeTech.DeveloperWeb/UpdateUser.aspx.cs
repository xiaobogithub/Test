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
    public partial class UpdateUser : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_USER_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindUser();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ManageUser.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void okButton_click(object sender, EventArgs e)
        {
            try
            {
                UserModel updateuser = new UserModel();
                updateuser.UserName = emailTextBox.Text;
                updateuser.FirstName = firstNameTextBox.Text;
                updateuser.LastName = lastNameTextBox.Text;
                updateuser.PhoneNumber = cellPhoneTextBox.Text;
                updateuser.Gender = genderDropDownList.SelectedValue.ToLower().Equals(GenderEnum.Male.ToString().ToLower()) ? GenderEnum.Male : GenderEnum.Female;//genderDropDownList.SelectedValue;
                updateuser.UserGuid = new Guid(Request.QueryString[Constants.QUERYSTR_USER_GUID]);
                updateuser.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue);
                Resolve<IUserService>().UpdateUserInfo(updateuser);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect("ManageUser.aspx");
        }

        private void BindUser()
        {
            UserModel uim = Resolve<IUserService>().GetUserByUserGuid(new Guid(Request.QueryString[Constants.QUERYSTR_USER_GUID]));
            emailTextBox.Text = uim.UserName;
            cellPhoneTextBox.Text = uim.PhoneNumber.Trim();
            firstNameTextBox.Text = uim.FirstName;
            lastNameTextBox.Text = uim.LastName;
            ddlUserType.SelectedValue = (uim.UserType).ToString();
            genderDropDownList.SelectedValue = uim.Gender.ToString().Trim();            
        }
    }
}
