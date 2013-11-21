using System;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Web.UI.WebControls;
using System.Web;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddPredictorCategory : PageBase<PredictorCategoryModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListPredictorCategory.aspx";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PredictorCategoryModel predictorCategory = new PredictorCategoryModel();
                predictorCategory.CategoryDescription = txtPredictorCategoryDescription.Text;
                predictorCategory.CategoryName = txtPredictorCategoryName.Text;

                Resolve<IPredictorCategoryService>().InsertPredictorCategory(predictorCategory);

                Response.Redirect("ListPredictorCategory.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListPredictorCategory.aspx");
        }
    }
}
