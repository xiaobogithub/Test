using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddNewPageSequence : PageBase<PageSequenceModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindSpecificIntervent();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void BindSpecificIntervent()
        {
            ////hplCancel.NavigateUrl = GetBackUrl();
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = GetBackUrl();
            txtName.Text = Resolve<IInterventService>().GetIntervent(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID])).InterventName;
            //if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]))
            //{
            //    ddlSpecialIntervent.DataSource = Resolve<ISpecificInterventService>().GetSpecificInterventByIntervnet(new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]));
            //}
            //else
            //{
            //    ddlSpecialIntervent.DataSource = Resolve<ISpecificInterventService>().GetAllSpecificIntervents();
            //}
            //ddlSpecialIntervent.DataTextField = "Name";
            //ddlSpecialIntervent.DataValueField = "ID";
            //ddlSpecialIntervent.DataBind();
            //ddlSpecialIntervent.SelectedValue = Request.QueryString[Constants.QUERYSTR_SPECIFICINTERVENT_GUID];
        }

        private string GetBackUrl()
        {
            return Request.Url.ToStringWithoutPort().Replace("AddNewPageSequence.aspx", "AddPageSequence.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Resolve<IPageSequenceService>().AddPageSequence(txtName.Text.Trim(), txtDes.Text.Trim(), new Guid(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]));

                Response.Redirect(GetBackUrl());
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string getBackUrl = GetBackUrl();
            Response.Redirect(getBackUrl);
        }
    }
}
