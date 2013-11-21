using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditSpecialString : PageBase<SpecialStringModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_SPECIALSTRING_NAME]) && !string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]))
            {
                if(!IsPostBack)
                {
                    InitialPage();
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPage()
        {
            Model = Resolve<ISpecialStringService>().GetSpecialStringBy(Request.QueryString[Constants.QUERYSTR_SPECIALSTRING_NAME], new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
            languageLabel.Text = Model.LanguageName;
            valueTextBox.Text = Model.Value;
            stringNameLabel.Text = Model.Name;
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            SpecialStringModel spModel = new SpecialStringModel
            {
                LanguageGuid = new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]),
                Name = stringNameLabel.Text,
                Value = valueTextBox.Text
            };

            Resolve<ISpecialStringService>().UpdateSpecialString(spModel);
            Response.Redirect(string.Format("ManageSpecialString.aspx?{0}={1}", Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageSpecialString.aspx?{0}={1}", Constants.QUERYSTR_LANGUAGE_GUID, Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
        }
    }
}
