using System;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageTranslationJobElement : PageBase<TranslationJobElementModel>
    {
        private string TranslationJobGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_TRANSLATION_JOB_GUID];
            }
        }

        private string TranslationJobContentGuid
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_TRANSLATION_JOB_CONTENT_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_TRANSLATION_JOB_CONTENT_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        InitialBackURL();
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

        private void InitialBackURL()
        {
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageTranslationJobContent.aspx?{0}={1}", Constants.QUERYSTR_TRANSLATION_JOB_GUID, TranslationJobGUID);
        }
    }
}