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
    public partial class EditInterventCategory : PageBase<InterventCategoryModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]))
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
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListInterventCategory.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }


        private void BindInterventCategory()
        {
            ddlPredictor.DataSource = Resolve<IPredictorService>().GetAllPredictors();
            ddlPredictor.DataTextField = "Name";
            ddlPredictor.DataValueField = "PredictorID";
            ddlPredictor.DataBind();

            Model = Resolve<IInterventCategoryService>().GetInterventCategoryModel(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]));
            txtInterventCategoryDescription.Text = Model.Description;
            txtInterventCategoryName.Text = Model.Name;
            ddlPredictor.SelectedValue = Model.PredictorID.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                InterventCategoryModel interventCategoryModel = new InterventCategoryModel();
                interventCategoryModel.Name = txtInterventCategoryName.Text;
                interventCategoryModel.Description = txtInterventCategoryName.Text;
                interventCategoryModel.CategoryGUID = new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]);
                interventCategoryModel.PredictorID = new Guid(ddlPredictor.SelectedValue);

                Resolve<IInterventCategoryService>().UpdateInterventCategory(interventCategoryModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  

            Response.Redirect("~/ListInterventCategory.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInterventCategory.aspx");
        }
    }
}
