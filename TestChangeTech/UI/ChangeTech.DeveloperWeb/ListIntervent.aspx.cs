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
    public partial class ListIntervent : PageBase<InterventListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindIntervent();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
               // ((HyperLink)Master.FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void BindIntervent()
        {
            ddlInterventCategory.DataSource = Resolve<IInterventCategoryService>().GetInterventCategoryDropDownList();
            ddlInterventCategory.DataTextField = "TextField";
            ddlInterventCategory.DataValueField = "DataField";
            ddlInterventCategory.DataBind();

            if (Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID] == null)
            {
                Model = new InterventListModel(Resolve<IInterventService>().GetAllIntervent());
            }
            else
            {
                ddlInterventCategory.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID];
                Model = new InterventListModel(Resolve<IInterventService>().GetInterventsOfCategory(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID])));
            }
            int programSecuirty = ContextService.CurrentAccount.Security;
                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
                {
                    Button addNewIntervent = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAdd");
                    addNewIntervent.Enabled = true;
                }

            // for paging and soring
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "InterventName", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description, (string)Resources.Share.InterventCategory };
            string[] sortExpression = { "InterventName", "Description", "InterventCategoryName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            // bind SpecificIntervent to repeater
            rpIntervent.DataSource = PagingSortingService.GetCurrentPage<InterventModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpIntervent.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID] != null)
            {
                Response.Redirect(string.Format("~/AddIntervent.aspx?{0}={1}",
                    Constants.QUERYSTR_INTERVENTCATEGORY_GUID, Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]));
            }
            else
            {
                Response.Redirect("~/AddIntervent.aspx");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string interventGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("~/EditIntervent.aspx?{0}={1}", 
                Constants.QUERYSTR_INTERVENT_GUID,interventGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid specificInterventGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IInterventService>().CanDeleteIntervent(specificInterventGuid))
                {
                    Resolve<IInterventService>().DeleteIntervent(specificInterventGuid);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('This intervent is in using, you cannot delete it.');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void ddlIntervent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlInterventCategory.SelectedValue))
            {
                Response.Redirect(string.Format("ListIntervent.aspx?{0}={1}", 
                    Constants.QUERYSTR_INTERVENTCATEGORY_GUID, ddlInterventCategory.SelectedValue));
            }
            else
            {
                Response.Redirect("ListIntervent.aspx");
            }
        }

        protected void rpIntervent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int programSecuirty = ContextService.CurrentAccount.Security;
            if (e.Item.ItemType == ListItemType.Header)
            {
                ////// program security
                ////if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
                ////{
                ////    ((Button)e.Item.FindControl("btnAdd")).Enabled = true;
                ////}
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
                if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationEdit, (PermissionEnum)programSecuirty))
                {
                    ((Button)e.Item.FindControl("mergeButton")).Enabled = true;
                }
            }
        }

        protected void mergeButton_Click(object sender, EventArgs e)
        {
            string interventGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("MergeIntervent.aspx?{0}={1}", Constants.QUERYSTR_INTERVENT_GUID, interventGuid));
        }
    }
}
