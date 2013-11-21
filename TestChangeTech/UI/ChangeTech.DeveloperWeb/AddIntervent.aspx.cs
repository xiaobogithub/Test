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
    public partial class AddIntervent : PageBase<InterventModel>
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
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListIntervent.aspx";
            }
        }

        private void InitialPage()
        {
            ddlInterventCategory.DataSource = Resolve<IInterventCategoryService>().GetAllInterventCategory();
            ddlInterventCategory.DataTextField = "Name";
            ddlInterventCategory.DataValueField = "CategoryGUID";
            ddlInterventCategory.DataBind();
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]))
            {
                ddlInterventCategory.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID];
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                InterventModel interventModel = new InterventModel();
                interventModel.InterventName = txtInterventName.Text;
                interventModel.Description = txtInterventDescription.Text;
                interventModel.InterventCategoryGUID = new Guid(ddlInterventCategory.SelectedValue);

                Resolve<IInterventService>().InsertIntervent(interventModel);
                Response.Redirect("~/ListIntervent.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }   
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListIntervent.aspx");
        }
    }
}
