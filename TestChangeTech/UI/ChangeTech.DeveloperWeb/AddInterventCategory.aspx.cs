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
    public partial class AddInterventCategory : PageBase<InterventCategoryModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitialPage();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListInterventCategory.aspx";
            }
        }

        private void InitialPage()
        {
            ddlPredictor.DataSource = Resolve<IPredictorService>().GetAllPredictors();
            ddlPredictor.DataTextField = "Name";
            ddlPredictor.DataValueField = "PredictorID";
            ddlPredictor.DataBind();
            if (Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID] != null)
            {
                ddlPredictor.SelectedValue = Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID];
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                InterventCategoryModel interventCategoryModel = new InterventCategoryModel();
                interventCategoryModel.Name = txtInterventCategoryName.Text;
                interventCategoryModel.Description = txtInterventCategoryDescription.Text;
                interventCategoryModel.PredictorID = new Guid(ddlPredictor.SelectedValue);

                Resolve<IInterventCategoryService>().InsertInterventCategory(interventCategoryModel);
                Response.Redirect("~/ListInterventCategory.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInterventCategory.aspx");
        }
    }
}
