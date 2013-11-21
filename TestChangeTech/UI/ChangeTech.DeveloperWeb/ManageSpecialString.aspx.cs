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
    public partial class ManageSpecialString : PageBase<SpecialStringModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindLanguage();
                BindSpecialString();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void BindLanguage()
        {
            LanguageDropDownList.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
            LanguageDropDownList.DataTextField = "Name";
            LanguageDropDownList.DataValueField = "LanguageGUID";
            LanguageDropDownList.DataBind();
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]))
            {
                LanguageDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
            }
        }

        private void BindSpecialString()
        {
            if(!string.IsNullOrEmpty(LanguageDropDownList.SelectedValue))
            {
                specialStringRepeater.DataSource = Resolve<ISpecialStringService>().GetSpecialStringByLanguage(new Guid(LanguageDropDownList.SelectedValue));
                specialStringRepeater.DataBind();
            }
        }

        protected void LanguageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSpecialString();
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string stringName = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditSpecialString.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_LANGUAGE_GUID, LanguageDropDownList.SelectedValue, Constants.QUERYSTR_SPECIALSTRING_NAME, stringName));
        }

        protected void newButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(newSpecailStringNameTextBox.Text))
                {
                    if (!Resolve<ISpecialStringService>().ExistSpecialString(newSpecailStringNameTextBox.Text))
                    {
                        Resolve<ISpecialStringService>().AddSpecialString(newSpecailStringNameTextBox.Text);
                        Response.Redirect(Request.Url.ToString());
                    }
                    else
                    {
                        msgLbl.Text = "Please change to use another name, this name has been used.";
                    }
                }
                else
                {
                    msgLbl.Text = "Please input new special tring name.";
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
