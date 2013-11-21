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
    public partial class EditProgramStatus : PageBase<ProgramStatusModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindProgramStatus();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListProgramStatus.aspx";
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ProgramStatusModel statusModel = new ProgramStatusModel();
                statusModel.ID = new Guid(Request.QueryString["Status"]);
                statusModel.Name = txtProgramStatusName.Text;
                statusModel.Description = txtProgramStatusDescription.Text;
                Resolve<IProgramStatusService>().UpdateProgramStatus(statusModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
            Response.Redirect("~/ListProgramStatus.aspx");
        }

        private void BindProgramStatus()
        {
            Model = Resolve<IProgramStatusService>().GetProgramStatusByStatusGuid(new Guid(Request.QueryString["Status"]));
            txtProgramStatusName.Text = Model.Name;
            txtProgramStatusDescription.Text = Model.Description; 
        }
    }
}
