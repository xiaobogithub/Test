using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.Collections.Generic;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageApplicationSecurity : PageBase<UsersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindSecurityList();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ManageApplicationSecurity.aspx";
            }
        }

        private void BindSecurityList()
        {
            Model = Resolve<IUserService>().GetAdminUsers();

            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "UserName", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)base.GetLocalResourceObject("Email") };
            string[] sortExpression = { "UserName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            applicationSecurityRepeater.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<UserModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            applicationSecurityRepeater.DataBind();

            List<UserTypeModel> userTypes = Resolve<IUserService>().GetAllUserTypes();
            ddlUserType.DataSource = userTypes;
            ddlUserType.DataBind();
        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedUserGUID = ((Button)sender).CommandArgument;

                UserModel userModel = Resolve<IUserService>().GetUserByUserGuid(new Guid(selectedUserGUID));
                emailTxtBox.Text = userModel.UserName;
                firstNameTxtBox.Text = userModel.FirstName;
                lastNameTxtBox.Text = userModel.LastName;
                genderDDL.SelectedValue = userModel.Gender.ToString().Trim();
                mobilePhoneTxtBox.Text = userModel.PhoneNumber;
                ddlUserType.SelectedValue = ((int)userModel.UserType).ToString();
                cboIsSMSLogin.Checked = userModel.IsSMSLogin;
                saveBtn.CommandArgument = userModel.UserGuid.ToString();

                UserPermissionListModel permissionListModel = Resolve<IUserService>().GetUserPermissionListModel(new Guid(selectedUserGUID));
                userHasPermissionGridView.DataSource = permissionListModel.ProgramUserHasPermission;
                userHasPermissionGridView.DataBind();

                userHasNotPermissionGridView.DataSource = permissionListModel.ProgramUserHasNotPermission;
                userHasNotPermissionGridView.DataBind();

                applicationSecurityView.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedUserGUID = ((Button)sender).CommandArgument;
                Resolve<IUserService>().DeleteUser(new Guid(selectedUserGUID));
                BindSecurityList();
                applicationSecurityView.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedUserGUID = ((Button)sender).CommandArgument;
                UserModel userModel = Resolve<IUserService>().GetUserByUserGuid(new Guid(selectedUserGUID));
                userModel.UserName = emailTxtBox.Text;
                userModel.FirstName = firstNameTxtBox.Text;
                userModel.LastName = lastNameTxtBox.Text;
                userModel.Gender = genderDDL.SelectedValue.ToLower().Equals(GenderEnum.Male.ToString().ToLower()) ? GenderEnum.Male : GenderEnum.Female;//genderDDL.SelectedValue;
                userModel.PhoneNumber = mobilePhoneTxtBox.Text;
                userModel.UserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), ddlUserType.SelectedValue);
                userModel.IsSMSLogin = cboIsSMSLogin.Checked;
                
                Resolve<IUserService>().UpdateUserInfo(userModel);
                BindSecurityList();
                applicationSecurityView.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void addAdminLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewUserForApplication.aspx");
        }

        protected void applicationSecurityRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                Label lab = e.Item.FindControl("lbAccount") as Label;
                if (lab != null)
                {
                    if (lab.Text == Context.User.Identity.Name)
                    {
                        ((Button)e.Item.FindControl("editBtn")).Enabled = false;
                        ((Button)e.Item.FindControl("deleteBtn")).Enabled = false;
                    }
                }

                DropDownList ddl = e.Item.FindControl("ddlUserType") as DropDownList;
                if (ddl != null)
                {
                    List<UserTypeModel> userTypes = Resolve<IUserService>().GetAllUserTypes();
                    ddl.DataSource = userTypes;
                    ddl.DataBind();
                    ddl.SelectedValue = ((int)((UserModel)e.Item.DataItem).UserType).ToString();
                }
            }
        }

        protected void userHasPermissionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            userHasPermissionGridView.PageIndex = e.NewPageIndex;
            string selectedUserGUID = saveBtn.CommandArgument;
            BindUserPermission(selectedUserGUID);
        }

        protected void userHasNotPermissionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            userHasNotPermissionGridView.PageIndex = e.NewPageIndex;
            string selectedUserGUID = saveBtn.CommandArgument;
            BindUserPermission(selectedUserGUID);
        }

        private void BindUserPermission(string userGUID)
        {
            UserPermissionListModel permissionListModel = Resolve<IUserService>().GetUserPermissionListModel(new Guid(userGUID));
            userHasPermissionGridView.DataSource = permissionListModel.ProgramUserHasPermission;
            userHasPermissionGridView.DataBind();

            userHasNotPermissionGridView.DataSource = permissionListModel.ProgramUserHasNotPermission;
            userHasNotPermissionGridView.DataBind();
        }

        protected void userHasPermissionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string selectedUserGUID = saveBtn.CommandArgument;
            string programGUID = e.CommandArgument.ToString();
            Resolve<IProgramUserService>().DeleteProgramUser(new Guid(programGUID), new Guid(selectedUserGUID));
            BindUserPermission(selectedUserGUID);
        }

        //protected void userHasNotPermissionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    string selectedUserGUID = saveBtn.CommandArgument;
        //    string programGUID = e.CommandArgument.ToString();
        //    Resolve<IProgramUserService>().AddProgramUser(new Guid(programGUID), new Guid(selectedUserGUID));
        //    BindUserPermission(selectedUserGUID);
        //}

        protected void addProgramBtn_Click(object sender, EventArgs e)
        {
            string selectedUserGUID = saveBtn.CommandArgument;
            string programGUID = ((Button)sender).CommandArgument.ToString();
            Resolve<IProgramUserService>().AddProgramUser(new Guid(programGUID), new Guid(selectedUserGUID));
            BindUserPermission(selectedUserGUID);
        }
    }
}
