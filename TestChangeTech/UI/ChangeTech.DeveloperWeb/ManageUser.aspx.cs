using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageUser : PageBase<UsersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindUserList();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Default.aspx";
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void userLinkButton_Click(object sender, EventArgs e)
        {
            string userGuid = ((LinkButton)sender).CommandArgument;
            Response.Redirect(string.Format("UpdateUser.aspx?{0}={1}", Constants.QUERYSTR_USER_GUID, userGuid));
        }

        protected void deleteButton_click(object sender, EventArgs e)
        {
            try
            {
                string userGuid = ((Button)sender).CommandArgument;
                SortedList<Guid, int> usersPermission = Resolve<IProgramService>().GetProgramPermissionByUserGuid(new Guid(userGuid));
                if (usersPermission.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('The user is still active in some program!');", true);
                }
                else
                {
                    Resolve<IUserService>().DeleteUser(new Guid(userGuid));
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void addLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewUserForApplication.aspx");
        }

        private void BindUserList()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_QUERY]))
                {
                    queryTextBox.Text = Request.QueryString[Constants.QUERYSTR_QUERY];
                }

                Model = Resolve<IUserService>().GetQueryApplicationUser(queryTextBox.Text.Trim());

                // for paging and sorting
                InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "UserName", "asc");
                string url = Request.Url.ToStringWithoutPort();
                string[] header = { (string)base.GetLocalResourceObject("Email"), (string)base.GetLocalResourceObject("FirstName")
                                      , (string)base.GetLocalResourceObject("LastName"), (string)base.GetLocalResourceObject("PhoneNumber")
                                      , (string)base.GetLocalResourceObject("Gender"), (string)base.GetLocalResourceObject("UserType") };
                string[] sortExpression = { "UserName", "FirstName", "LastName", "PhoneNumber", "Gender", "UserType" };
                PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
                HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

                usersRepeater.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<UserModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
                usersRepeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(queryTextBox.Text))
            {
                Response.Redirect(string.Format("ManageUser.aspx?{0}={1}", Constants.QUERYSTR_QUERY, queryTextBox.Text.Trim()));
            }
            else
            {
                Response.Redirect("ManageUser.aspx");
            }
        }
    }
}
