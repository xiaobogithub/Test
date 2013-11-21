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
    public partial class EditIntervent : PageBase<InterventModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindIntervent();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListIntervent.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }


        private void BindIntervent()
        {
            ddlInterventCategory.DataSource = Resolve<IInterventCategoryService>().GetAllInterventCategory();
            ddlInterventCategory.DataTextField = "Name";
            ddlInterventCategory.DataValueField = "CategoryGUID";
            ddlInterventCategory.DataBind();

            Model = Resolve<IInterventService>().GetIntervent(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]));
            txtInterventDescription.Text = Model.Description;
            txtInterventName.Text = Model.InterventName;
            ddlInterventCategory.SelectedValue = Model.InterventCategoryGUID.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                InterventModel interventModel = new InterventModel();
                interventModel.InterventName = txtInterventName.Text;
                interventModel.Description = txtInterventDescription.Text;
                interventModel.InterventGUID = new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]);
                interventModel.InterventCategoryGUID = new Guid(ddlInterventCategory.SelectedValue);

                Resolve<IInterventService>().UpdateIntervent(interventModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  

            Response.Redirect("~/ListIntervent.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListIntervent.aspx");
        }
    }
}
