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
    public partial class AddHelpItem : PageBase<HelpItemModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        InitialPageInfo();
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
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
        }

        private void InitialPageInfo()
        {
            BindOrderList();
            //LanguageModel language = Resolve<ILanguageService>().GetLanguageMode(new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]));
            //LanguageTextBox.Text = language.Name;
        }

        private void BindOrderList()
        {
            OrderDropDownList.Items.Clear();
            int count = Resolve<IHelpItemService>().GetHelpItemCount(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            for (int i = 1; i <= count + 1; i++)
            {
                OrderDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            OrderDropDownList.SelectedIndex = count;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                HelpItemModel itemModel = new HelpItemModel
                {
                    Answer = AnswerTextBox.Text,
                    Question = QuestionTextBox.Text,
                    //LanguageGUID = new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]),
                    ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                    Order = Convert.ToInt32(OrderDropDownList.SelectedValue)
                };

                Resolve<IHelpItemService>().Insert(itemModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            Response.Redirect(Request.Url.ToStringWithoutPort().Replace("AddHelpItem.aspx","ManageHelpItem.aspx"));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.ToStringWithoutPort().Replace("AddHelpItem.aspx", "ManageHelpItem.aspx"));
        }
    }
}
