using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;

namespace ChangeTech.DeveloperWeb
{
    public partial class MergeIntervent : PageBase<EditSessionModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID] != null)
            {
                if (!IsPostBack)
                {
                    BindDropDownList();
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListIntervent.aspx";
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void predictorDropDownListChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MergeIntervent.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_INTERVENT_GUID, Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue));
        }

        protected void interventCategoryDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MergeIntervent.aspx?{0}={1}&{2}={3}&{4}={5}",Constants.QUERYSTR_INTERVENT_GUID,Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue));
        }

        private void BindPredictorList()
        {
            predictorDropdownList.DataSource = Resolve<IPredictorService>().GetAllPredictors();
            predictorDropdownList.DataValueField = "PredictorID";
            predictorDropdownList.DataTextField = "Name";
            predictorDropdownList.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID]))
            {
                predictorDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID];
            }
        }

        private void BindDropDownList()
        {
            interventLabel.Text = Resolve<IInterventService>().GetInterventName(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]));
            BindPredictorList();
            if (!string.IsNullOrEmpty(predictorDropdownList.SelectedValue))
            {
                BindInterventCategoryList(predictorDropdownList.SelectedValue);
            }
            if (!string.IsNullOrEmpty(interventCategoryDropdownList.SelectedValue))
            {
                BindInterventList(interventCategoryDropdownList.SelectedValue);
            }
        }

        private void BindInterventCategoryList(string guid)
        {
            interventCategoryDropdownList.DataSource = Resolve<IInterventCategoryService>().GetInterventCategoryModelsByPredictorGuid(new Guid(guid));
            interventCategoryDropdownList.DataValueField = "CategoryGUID";
            interventCategoryDropdownList.DataTextField = "Name";
            interventCategoryDropdownList.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]))
            {
                interventCategoryDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID];
            }
        }

        private void BindInterventList(string categoryGuid)
        {
            interventDropdownList.DataSource = Resolve<IInterventService>().GetInterventsOfCategory(new Guid(categoryGuid));
            interventDropdownList.DataValueField = "InterventGUID";
            interventDropdownList.DataTextField = "InterventName";
            interventDropdownList.DataBind();
        }

        protected void mergeButton_Click(object sender, EventArgs e)
        {
            if (interventDropdownList.SelectedValue != null)
            {
                Resolve<IInterventService>().MergeIntervent(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]), new Guid(interventDropdownList.SelectedValue));
                ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('merge successful');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('please select an intervent');", true);
            }
        }
    }
}
