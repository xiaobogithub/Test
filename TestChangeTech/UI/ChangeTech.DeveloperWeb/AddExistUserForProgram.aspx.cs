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
    public partial class AddOldUserForProgram : PageBase<UsersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
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
                    ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = string.Format("ManageProgramSecurity.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void btnAddUser_click(object sender, EventArgs e)
        {
            try 
            {
                string userGuid = ((Button)sender).CommandArgument;
                string programGuid = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
                ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(programGuid));
                Resolve<IProgramService>().JoinProgram(new Guid(programGuid), new Guid(userGuid),Guid.Empty, 3,Request.UserHostAddress, ProgramUserStatus.Registered,"");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }              
            Response.Redirect(string.Format("ManageProgramSecurity.aspx?{0}={1}",Constants.QUERYSTR_PROGRAM_GUID,Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        private void BindUserList()
        {
            Model = Resolve<IUserService>().GetUsersNotInProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));

            // for paging and sorting
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "UserName", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)base.GetLocalResourceObject("Email"), (string)base.GetLocalResourceObject("FirstName"), (string)base.GetLocalResourceObject("LastName") };
            string[] sortExpression = { "UserName", "FirstName", "LastName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpUserList.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<UserInfoModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpUserList.DataBind();
        }
    }
}
