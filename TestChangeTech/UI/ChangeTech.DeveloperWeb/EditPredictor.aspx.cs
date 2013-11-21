using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditPredictor : PageBase<PredictorModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Predictor"]))
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
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListPredictor.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindPredictor()
        {
            ddlPredictorCategory.DataSource = Resolve<IPredictorCategoryService>().GetAllPredictorCategory();
            ddlPredictorCategory.DataTextField = "CategoryName";
            ddlPredictorCategory.DataValueField = "CategoryID";
            ddlPredictorCategory.DataBind();
            Model = Resolve<IPredictorService>().GetPredictorByPredictorGuid(new Guid(Request.QueryString["Predictor"]));
            txtPredictorDescription.Text = Model.Description;
            txtPredictorName.Text = Model.Name;
            ddlPredictorCategory.SelectedValue = Model.CatagoryID.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                PredictorModel predictorModel = new PredictorModel();
                predictorModel.Name = txtPredictorName.Text;
                predictorModel.Description = txtPredictorDescription.Text;
                predictorModel.CatagoryID = new Guid(ddlPredictorCategory.SelectedValue);
                predictorModel.CategoryName = ddlPredictorCategory.Text;
                predictorModel.PredictorID = new Guid(Request.QueryString["Predictor"]);

                Resolve<IPredictorService>().UpdatePredictor(predictorModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
            Response.Redirect("~/ListPredictor.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListPredictor.aspx");
        }
    }
}
