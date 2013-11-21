using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Web.UI.WebControls;
using System.Web;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListPredictorCategory : PageBase<PredictorCategorysModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindPredictoryCategory();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddPredictorCategory.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string predictorCategoryGuid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditPredictorCategory.aspx?Category={0}", predictorCategoryGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Guid predictorCategoryGuid = new Guid(((Button)sender).CommandArgument);
                if (Resolve<IPredictorCategoryService>().CanDeletePredictorCategory(predictorCategoryGuid))
                {
                    Resolve<IPredictorCategoryService>().DeletePredictorCategory(predictorCategoryGuid);
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('The category is referenced by predictor!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        private void BindPredictoryCategory()
        {
            Model = new PredictorCategorysModel(Resolve<IPredictorCategoryService>().GetAllPredictorCategory());
            int programSecuirty = ContextService.CurrentAccount.Security;
            // program security
            if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ApplicationCreate, (PermissionEnum)programSecuirty))
            {
                Button addNewPredictorCategory = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAdd");
                addNewPredictorCategory.Enabled = true;
            }
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "CategoryName", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)Resources.Share.Name, (string)Resources.Share.Description};
            string[] sortExpression = { "CategoryName", "CategoryDescription" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            rpPredictorCategory.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<PredictorCategoryModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            rpPredictorCategory.DataBind(); 
        }
               
        protected void rpPredictorCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
