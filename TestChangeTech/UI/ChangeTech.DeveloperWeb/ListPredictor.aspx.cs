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
    public partial class ListPredictor : PageBase<PredictorsModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindPredictor();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void BindPredictor()
        {
            ddlPredictorCategory.DataSource = Resolve<IPredictorCategoryService>().GetPredictorCategoryDropdownList();
            ddlPredictorCategory.DataTextField = "TextField";
            ddlPredictorCategory.DataValueField = "DataField";
            ddlPredictorCategory.DataBind();

            if (Request.QueryString["Category"] == null)
            {
                Model = new PredictorsModel(Resolve<IPredictorService>().GetAllPredictors());
            }
            else
            {
                ddlPredictorCategory.SelectedValue = Request.QueryString["Category"];
                Model = new PredictorsModel(Resolve<IPredictorService>().GetPredictorsByPredictorCategory(new Guid(Request.QueryString["Category"])));
            }

            int programSecuirty = ContextService.CurrentAccount.Security;
           // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
            {
                Button addNewPredictor = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAdd");
                addNewPredictor.Enabled = true;
            }

            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "Name", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description, (string)Resources.Share.PredictorCategory };
            string[] sortExpression = { "Name", "Description", "CategoryName" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpPredictor.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<PredictorModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpPredictor.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPredictorCategory.SelectedValue))
            {
                Response.Redirect("~/AddPredictor.aspx");
            }
            else
            {
                Response.Redirect(string.Format("~/AddPredictor.aspx?Category={0}", ddlPredictorCategory.SelectedValue));
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string predicotrGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("~/EditPredictor.aspx?Predictor={0}", predicotrGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid predictorGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IPredictorService>().CanDeletePredictor(predictorGuid))
                {
                    Resolve<IPredictorService>().DeletePredictor(predictorGuid);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The predictor is referenced by intervent!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void ddlPredictorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPredictorCategory.SelectedValue))
            {
                Response.Redirect("ListPredictor.aspx");
            }
            else
            {
                Response.Redirect(string.Format("ListPredictor.aspx?Category={0}", ddlPredictorCategory.SelectedValue));
            }
        }

        protected void rpPredictor_ItemDataBound1(object sender, RepeaterItemEventArgs e)
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
