using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageCopyTipMessage : PageBase<TipMessageModel>
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

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
            }
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        }

        protected void InitialPage()
        {
            BindLanguageAndProgram();
        }

        private void BindLanguageAndProgram()
        {
            List<ManageLanguageModel> languagesModel = Resolve<ILanguageService>().GetAllProgramLanguageModel();
            ddlLanguage.DataSource = languagesModel;
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataValueField = "LanguageGUID";
            ddlLanguage.DataBind();
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
            {
                BindProgramsByLanguage(new Guid(ddlLanguage.SelectedValue));
            }
        }

        private void BindProgramsByLanguage(Guid languageGuid)
        {
            if (languageGuid != Guid.Empty)
            {
                List<SimpleProgramModel> programsModel = Resolve<IProgramService>().GetSimpleProgramsByLanguageGuid(languageGuid);
                ddlProgram.DataSource = programsModel;
                ddlProgram.DataTextField = "ProgramName";
                ddlProgram.DataValueField = "ProgramGuid";
                ddlProgram.DataBind();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "warning","<script type='text/javascript'>alert('Language cann't empty !');</script>");
            }
        }
        //LanguageDDL
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProgramsByLanguage(new Guid(ddlLanguage.SelectedValue));
        }

        //ProgramDDL
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlProgram.SelectedValue))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "warning", "<script type='text/javascript'>alert('Program cann't empty !');</script>");
            }
        }

        //Copy Tip Message
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            //ProgramGuid and LanguageGuid.
            if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue) && !string.IsNullOrEmpty(ddlProgram.SelectedValue))
            {
                Guid copyLanguageGuid = new Guid(ddlLanguage.SelectedValue);
                Guid copyProgramGuid = new Guid(ddlProgram.SelectedValue);
                //ProgramModel copyProgramModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(copyProgramGuid, copyLanguageGuid);
                if (Resolve<ITipMessageService>().CopyTipMessageFromProgram(new Guid(ProgramGUID), copyProgramGuid))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Copy tipmessage succeed!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Copy tipmessage failed!');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Program or language cann't empty !');", true);
            }
        }

    }
}