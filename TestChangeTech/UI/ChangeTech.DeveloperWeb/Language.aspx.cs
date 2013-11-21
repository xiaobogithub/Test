using System;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class Language : PageBase<LanguagesModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindLanguages();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Language.aspx";
            }
        }

        private void BindLanguages()
        {
            Model = Resolve<ILanguageService>().GetLanguagesModel();

            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(Model.Count) / PageSize), "Name", "asc");

            string url = Request.Url.ToStringWithoutPort();
            string[] header = { (string)base.GetLocalResourceObject("LanguageName") };
            string[] sortExpression = { "Name" };

            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

            languagesRepeater.DataSource = Ethos.Utility.PagingSortingService.GetCurrentPage<LanguageModel>(Model, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
            languagesRepeater.DataBind();
        }

        protected void addLanguageButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string newLanguageName = languageNameTextBox.Text.Trim();

                    Resolve<ILanguageService>().AddLanguage(newLanguageName);

                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                string languageGUID = ((Button)sender).CommandArgument;
                LanguageModel languageModel = Resolve<ILanguageService>().GetLanguageMode(new Guid(languageGUID));
                newLanguageNameTextBox.Text = languageModel.Name;
                saveButton.CommandArgument = languageModel.LanguageGUID.ToString();

                languageMultiView.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void IsLanguageUnique(object source, ServerValidateEventArgs args)
        {
            try
            {
                string newLanguageName = languageNameTextBox.Text.Trim();
                args.IsValid = Resolve<ILanguageService>().IsValidLanguageName(newLanguageName);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string languageGUID = ((Button)sender).CommandArgument;
                Resolve<ILanguageService>().DeleteLanguage(new Guid(languageGUID));
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
            //BindLanguages();
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string languageGUID = ((Button)sender).CommandArgument;
                Resolve<ILanguageService>().UpdateLanguage(new Guid(languageGUID), newLanguageNameTextBox.Text.Trim());
                //BindLanguages();
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }  
            languageMultiView.ActiveViewIndex = 0;
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }
    }
}
