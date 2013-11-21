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
    public partial class ListProgramStatus : PageBase<ProgramStatusListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindProgramStatus();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
           // ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = "~/ListProgram.aspx";
        }

        private void BindProgramStatus()
        {
            Model = new ProgramStatusListModel(Resolve<IProgramStatusService>().GetAllProgramStatus());

            // for paging and soring
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "Name", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description };
            string[] sortExpression = { "Name", "Description" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            // bind SpecificIntervent to repeater
            rpProgramStatus.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<ProgramStatusModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpProgramStatus.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddProgramStatus.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string StatusGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("~/EditProgramStatus.aspx?Status={0}", StatusGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid statusGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IProgramStatusService>().CanDeleteProgramStatus(statusGuid))
                {
                    Resolve<IProgramStatusService>().DeleteProgramStatus(statusGuid);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The status is referenced by program!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void rpProgramStatus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = ContextService.CurrentAccount.Security;
            if (e.Item.ItemType == ListItemType.Header)
            {
                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("btnAdd")).Enabled = true;
                }
            }
            if (e.Item.DataItem != null)
            {
                // program security

                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationDelete, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("btnDelete")).Enabled = true;
                }
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationEdit, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                }
            }
        }
    }
}
