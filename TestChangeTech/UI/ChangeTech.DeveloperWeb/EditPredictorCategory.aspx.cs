using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditPredictorCategory : PageBase<PredictorCategoryModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Category"]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindPredictorCategory();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListPredictorCategory.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindPredictorCategory()
        {
            Model = Resolve<IPredictorCategoryService>().GetPredictorCategoryByCategoryGuid(new Guid(Request.QueryString["Category"]));
            txtPredictorCategoryDescription.Text = Model.CategoryDescription;
            txtPredictorCategoryName.Text = Model.CategoryName;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                PredictorCategoryModel predictorCategoryModel = new PredictorCategoryModel();
                predictorCategoryModel.CategoryName = txtPredictorCategoryName.Text;
                predictorCategoryModel.CategoryDescription = txtPredictorCategoryDescription.Text;
                predictorCategoryModel.CategoryID = new Guid(Request.QueryString["Category"]);
                Resolve<IPredictorCategoryService>().UpdatePredictorCategory(predictorCategoryModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
            Response.Redirect("~/ListPredictorCategory.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListPredictorCategory.aspx");
        }
    }
}
