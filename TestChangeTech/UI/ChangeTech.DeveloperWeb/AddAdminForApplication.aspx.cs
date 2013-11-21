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
    public partial class AddAdminForApplication : PageBase<UsersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindUserList();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ManageApplicationSecurity.aspx";
            }
        }

        protected void btnAddUser_click(object sender, EventArgs e)
        {
            try
            {
                string userGuid = ((Button)sender).CommandArgument;
                string programGuid = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
                Resolve<IUserService>().UpdateUserSecurity(new Guid(userGuid), PermissionEnum.ApplicationEdit);
                Response.Redirect("ManageApplicationSecurity.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }        
        }

        private void BindUserList()
        {
            Model = Resolve<IUserService>().GetCommonUsers();

            // for paging and sorting
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "UserName", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)base.GetLocalResourceObject("Email"), (string)base.GetLocalResourceObject("FirstName"), (string)base.GetLocalResourceObject("LastName") };
            string[] sortExpression = { "UserName", "FirstName", "LastName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpUserList.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<UserModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpUserList.DataBind();
        }
    }
}
