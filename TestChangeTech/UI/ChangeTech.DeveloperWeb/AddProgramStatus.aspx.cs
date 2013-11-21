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
    public partial class AddProgramStatus : PageBase<ProgramStatusModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ListProgramStatus.aspx";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ProgramStatusModel statusModel = new ProgramStatusModel();
            statusModel.Name = txtProgramStatusName.Text;
            statusModel.Description = txtProgramStatusDescription.Text;
            try
            {
                Resolve<IProgramStatusService>().InstertProgramStatus(statusModel);
                Response.Redirect("~/ListProgramStatus.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }
    }
}
