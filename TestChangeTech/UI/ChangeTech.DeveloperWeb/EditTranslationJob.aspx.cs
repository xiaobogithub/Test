using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using System.Collections.Generic;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditTranslationJob : PageBase<TranslationJobModel>
    {
        private Guid TranslationJobGUID
        {
            get
            {
                return new Guid(Request.QueryString[Constants.QUERYSTR_TRANSLATION_JOB_GUID]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_TRANSLATION_JOB_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindDefaultLanguageProgram();
                        BindTranslationJobModel();
                        BindTranslators();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/ManageTranslationJob.aspx";
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

        #region Button Events
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
            ddlFromLanguage.DataSource = languages;
            ddlFromLanguage.DataBind();
            ddlToLanguage.DataSource = languages;
            ddlToLanguage.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            TranslationJobModel translationJobModel = new TranslationJobModel
            {
                TranslationJobGUID = TranslationJobGUID,
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
                }
            };
            Resolve<ITranslationJobService>().UpdateTranslationJob(translationJobModel);
        }
        #endregion

        #region Private Methods
        private void BindDefaultLanguageProgram()
        {
            ProgramsModel programsModel = Resolve<IProgramService>().GetProgramsModel();
            ddlProgram.DataSource = programsModel;
            ddlProgram.DataBind();
        }

        private void BindTranslationJobModel()
        {
            Model = Resolve<ITranslationJobService>().GetTranslationJobByGUID(TranslationJobGUID);
            ddlProgram.SelectedValue = Model.Program.ProgramGuid.ToString();

            LanguagesModel languages = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(ddlProgram.SelectedValue));
            ddlFromLanguage.DataSource = languages;
            ddlFromLanguage.DataBind();
            ddlFromLanguage.SelectedValue = Model.FromLanguage.LanguageGUID.ToString();

            ddlToLanguage.DataSource = languages;
            ddlToLanguage.DataBind();
            ddlToLanguage.SelectedValue = Model.ToLanguage.LanguageGUID.ToString();
        }

        private void BindTranslators()
        {
            List<TranslationJobTranslatorModel> translatorPermissionModel = Resolve<ITranslationJobService>().GetTranslators(TranslationJobGUID, true);
            if (translatorPermissionModel != null && translatorPermissionModel.Count > 0)
            {
                this.translatorHasPermissionGridView.DataSource = translatorPermissionModel;
            }
            else
            {
                this.translatorHasPermissionGridView.DataSource = null;
            }
            this.translatorHasPermissionGridView.DataBind();


            List<TranslationJobTranslatorModel> translatorNotPermissionModel = Resolve<ITranslationJobService>().GetTranslators(TranslationJobGUID, false);
            if (translatorNotPermissionModel != null && translatorNotPermissionModel.Count > 0)
            {
                this.translatorHasNotPermissionGridView.DataSource = translatorNotPermissionModel;
            }
            else
            {
                this.translatorHasNotPermissionGridView.DataSource = null;
            }
            this.translatorHasNotPermissionGridView.DataBind();
            
        }

        #endregion

        protected void translatorHasPermissionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                string TranslationJobTranslatorGUID = e.CommandArgument.ToString();

                Resolve<ITranslationJobService>().DeleteTranslationJobTranslator(new Guid(TranslationJobTranslatorGUID));
                BindTranslators();
            }
        }

        protected void translatorHasNotPermissionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            translatorHasNotPermissionGridView.PageIndex = e.NewPageIndex;
            BindTranslators();
        }
        

        protected void translatorHasNotPermissionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "addCommand")
            {
                TranslationJobTranslatorModel translatorModel=new TranslationJobTranslatorModel();
                string translatorGuid = e.CommandArgument.ToString();
                translatorModel.TranslatorGUID=new Guid(translatorGuid);
                translatorModel.TranslationJobGUID=TranslationJobGUID;
                Resolve<ITranslationJobService>().AddTranslationJobTranslator(translatorModel);

                BindTranslators();
            }
        }
    }
}