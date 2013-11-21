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
    public partial class AddPredictor : PageBase<PredictorModel>
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
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListPredictor.aspx";
            }
        }

        private void InitialPage()
        {
            ddlPredictorCategory.DataSource = Resolve<IPredictorCategoryService>().GetAllPredictorCategory();
            ddlPredictorCategory.DataTextField = "CategoryName";
            ddlPredictorCategory.DataValueField = "CategoryID";
            ddlPredictorCategory.DataBind();
            if (Request.QueryString["Category"] != null)
            {
                ddlPredictorCategory.SelectedValue = Request.QueryString["Category"];
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PredictorModel predictorModel = new PredictorModel();
                predictorModel.CatagoryID = new Guid(ddlPredictorCategory.SelectedValue);
                predictorModel.Name = txtPredictorName.Text;
                predictorModel.Description = txtPredictorDescription.Text;

                Resolve<IPredictorService>().InsertPredictor(predictorModel);
                Response.Redirect("~/ListPredictor.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListPredictor.aspx");
        }
    }
}
