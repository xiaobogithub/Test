using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.Collections.Generic;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddTranslationJob : PageBase<TranslationJobModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindDefaultLanguageProgram();
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ManageTranslationJob.aspx";
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }

        #region Button Events
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
            ddlFromLanguage.DataSource = languages;
            ddlFromLanguage.DataBind();
            ddlToLanguage.DataSource = languages;
            ddlToLanguage.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int defaultTranslated;
            switch (this.ddlDefaultTranslatedField.SelectedValue)
            {
                case "1":
                    defaultTranslated = (int)DefaultContentInTranslatedFieldEnum.OriginalText;
                    break;
                case "2":
                    defaultTranslated = (int)DefaultContentInTranslatedFieldEnum.GoogleTranslation;
                    break;
                case "3":
                    defaultTranslated = (int)DefaultContentInTranslatedFieldEnum.Nothing;
                    break;
                default:
                    defaultTranslated = (int)DefaultContentInTranslatedFieldEnum.OriginalText;
                    break;
            }
            TranslationJobModel translationJobModel = new TranslationJobModel
            {
                Program = new ProgramBaseModel
                {
                    ProgramGuid = new Guid(ddlProgram.SelectedValue)
                },
                FromLanguage = new LanguageBaseModel
                {
                    LanguageGUID = new Guid(ddlFromLanguage.SelectedValue)
                },
                ToLanguage = new LanguageBaseModel
                {
                    LanguageGUID = new Guid(ddlToLanguage.SelectedValue)
                },
                DefaultTranslatedContent = defaultTranslated
            };
            translationJobModel.TranslationJobGUID = Resolve<ITranslationJobService>().AddTranslationJob(translationJobModel);

            Response.Redirect(string.Format("EditTranslationJob.aspx?{0}={1}", Constants.QUERYSTR_TRANSLATION_JOB_GUID, translationJobModel.TranslationJobGUID.ToString()), false);
            return;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageTranslationJob.aspx");
        }

        #endregion

        #region Private Methods
        private void BindDefaultLanguageProgram()
        {
            ProgramsModel programsModel = Resolve<IProgramService>().GetProgramsModel();
            ddlProgram.DataSource = programsModel;
            ddlProgram.DataBind();
        }
        #endregion
    }
}