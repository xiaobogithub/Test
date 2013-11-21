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
    public partial class AddProgramRoom : PageBase<ProgramRoomModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
                {
                    try
                    {
                        FormatPage();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID].ToString());
                    ////cancelHyperLink.NavigateUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID].ToString());
                }
                else
                {
                    Response.Redirect("InvalidUrl.aspx");
                }
            }
        }

        private void FormatPage()
        {
            ltProgram.Text = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID])).ProgramName;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Model = new ProgramRoomModel
                {
                    Description = txtDescription.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Program = new ProgramModel { Guid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID].ToString()) },
                };
                if (Resolve<IProgramRoomService>().Insert(Model))
                {
                    Response.Redirect(string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID].ToString()));
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Change another name of program room!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string goBackUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID].ToString());
            Response.Redirect(goBackUrl);
        }
    }
}
