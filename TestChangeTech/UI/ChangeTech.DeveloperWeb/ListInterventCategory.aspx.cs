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
    public partial class ListInterventCategory : PageBase<IntervnetCategoryListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindInterventCategory();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void BindInterventCategory()
        {
            ddlPredictor.DataSource = Resolve<IPredictorService>().GetPredictorDropDownList();
            ddlPredictor.DataTextField = "TextField";
            ddlPredictor.DataValueField = "DataField";
            ddlPredictor.DataBind();

            if (Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID] != null)
            {
                ddlPredictor.SelectedValue = Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID];
                Model = new IntervnetCategoryListModel(Resolve<IInterventCategoryService>().GetInterventCategoryModelsByPredictorGuid(new Guid(Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID])));
            }
            else
            {
                Model = new IntervnetCategoryListModel(Resolve<IInterventCategoryService>().GetAllInterventCategory());
            }

            int programSecuirty = ContextService.CurrentAccount.Security;
            // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
            {
                Button addNewInterventCategory = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAdd");
                addNewInterventCategory.Enabled = true;
            }

            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "Name", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description, (string)Resources.Share.Predictor };
            string[] sortExpression = { "Name", "Description", "PredictorName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpIntervent.DataSource = PagingSortingService.GetCurrentPage<InterventCategoryModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpIntervent.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID] != null)
            {
                Response.Redirect(string.Format("~/AddInterventCategory.aspx?{0}={1}",
                    Constants.QUERYSTR_PREDICTOR_GUID, Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID]));
            }
            else
            {
                Response.Redirect("~/AddInterventCategory.aspx");
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string interventCategoryGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("~/EditInterventCategory.aspx?{0}={1}",
                Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid interventCategoryGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IInterventCategoryService>().CanDeleteInterventCategory(interventCategoryGuid))
                {
                    Resolve<IInterventCategoryService>().DeleteInterventCategory(interventCategoryGuid);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The intervent category is in using!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void ddlPredictor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPredictor.SelectedValue))
            {
                Response.Redirect(string.Format("ListInterventCategory.aspx?{0}={1}",
                    Constants.QUERYSTR_PREDICTOR_GUID, ddlPredictor.SelectedValue));
            }
            else
            {
                Response.Redirect("ListInterventCategory.aspx");
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
            }
        }
    }
}
