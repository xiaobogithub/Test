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
    public partial class CompanyUsers : PageBase<CompanyUserInfoModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]))
            {
                if (!IsPostBack)
                {
                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditCompany3rdParty.aspx?{0}={1}&{2}={3}&{4}={5}",
                    //    Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID],
                    //    Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                    //    Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]);
                    BindProgramUserRepeater(new Guid(Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID]));
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindProgramUserRepeater(Guid companyRightGuid)
        {
            int countOfCompanyUser = Resolve<IProgramUserService>().GetCountOfCompanyUser(companyRightGuid);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(countOfCompanyUser) / PageSize), "Day", "asc");
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            List<CompanyUserInfoModel> companyUserList = Resolve<IProgramUserService>().GetCompanyUserList(companyRightGuid, CurrentPageNumber, PageSize);
            CompanyUserRepeater.DataSource = companyUserList;
            CompanyUserRepeater.DataBind();
        }

        protected void CompanyUserRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                
            }
        }

        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                string programuserGuid = ((Button)sender).CommandArgument;
                Resolve<IProgramService>().DeleteProgramUser(new Guid(programuserGuid));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void EditUser_Click(object sender, EventArgs e)
        {
            try
            {
                string programuserGuid = ((Button)sender).CommandArgument;
                Response.Redirect(string.Format("CompanyUserInfoPage.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", 
                    Constants.QUERYSTR_COMPANY_RIGHT_GUID, Request.QueryString[Constants.QUERYSTR_COMPANY_RIGHT_GUID],
                    Constants.QUERYSTR_PROFRAM_USER_GUID, programuserGuid,
                    Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                    Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }           
        }
    }
}
