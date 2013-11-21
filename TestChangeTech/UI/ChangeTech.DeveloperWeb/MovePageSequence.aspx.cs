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
    public partial class MovePageSequence : PageBase<EditSessionModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "Home.aspx";
            }
        }

        protected void predictorDropDownListChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MovePageSequence.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue));
        }

        protected void interventCategoryDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MovePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}", Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue));
        }

        protected void intervnetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MovePageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENT_GUID, interventDropdownList.SelectedValue));
        }

        private void BindDropDownList()
        {
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

        private void BindInterventList(string categoryGuid)
        {
            interventDropdownList.DataSource = Resolve<IInterventService>().GetInterventsOfCategory(new Guid(categoryGuid));
            interventDropdownList.DataValueField = "InterventGUID";
            interventDropdownList.DataTextField = "InterventName";
            interventDropdownList.DataBind();
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]))
            {
                interventDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID];
            }
            BindPageSequenceRepeater();
        }

        private void BindPageSequenceRepeater()
        {
            if (!string.IsNullOrEmpty(interventDropdownList.SelectedValue))
            {
                List<PageSequenceModel> pageSequenceList = Resolve<IPageSequenceService>()
                    .GetPageSequenceByInterventGuid(new Guid(interventDropdownList.SelectedValue));
                pageSequenceGridView.DataSource = pageSequenceList;
                pageSequenceGridView.DataBind();    
            }
            else
            {
                pageSequenceGridView.DataSource = null;
                pageSequenceGridView.DataBind();
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

        private void BindPredictorList()
        {
            predictorDropdownList.DataSource = Resolve<IPredictorService>().GetAllPredictors();
            predictorDropdownList.DataValueField = "PredictorID";
            predictorDropdownList.DataTextField = "Name";
            predictorDropdownList.DataBind();

            targetInterventDropDownList.DataSource = Resolve<IInterventService>().GetAllIntervent();
            targetInterventDropDownList.DataTextField = "InterventName";
            targetInterventDropDownList.DataValueField = "InterventGUID";
            targetInterventDropDownList.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID]))
            {
                predictorDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID];
            }
        }

        protected void moveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pageSequenceList.Value))
            {
                string[] pageSequences = pageSequenceList.Value.Split(',');
                Resolve<IPageSequenceService>().MovePageSequenceToAnotherIntervent(pageSequences, new Guid(targetInterventDropDownList.SelectedValue));
                BindPageSequenceRepeater();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "tip", "alert('Please checked one page sequence at least.');", true);
            }
        }
    }
}
