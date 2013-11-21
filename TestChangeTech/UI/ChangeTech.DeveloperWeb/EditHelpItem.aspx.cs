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
    public partial class EditHelpItem : PageBase<HelpItemModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_HELPITEM_GUID]))
            {
                if (!IsPostBack)
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
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
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPage()
        {
            BindOrderList();
            HelpItemModel itemModel = Resolve<IHelpItemService>().GetHelpItemModel(new Guid(Request.QueryString[Constants.QUERYSTR_HELPITEM_GUID]));
            if (itemModel != null)
            {
                QuestionTextBox.Text = itemModel.Question;
                AnswerTextBox.Text = itemModel.Answer;
                //LanguageModel language = Resolve<ILanguageService>().GetLanguageMode(itemModel.LanguageGUID);
                //if (language != null)
                //{
                //    LanguageTextBox.Text = language.Name;
                //}
                OrderDropDownList.SelectedValue = itemModel.Order.ToString();
            }
        }

        private void BindOrderList()
        {
            OrderDropDownList.Items.Clear();
            int count = Resolve<IHelpItemService>().GetHelpItemCount(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            for (int i = 1; i <= count; i++)
            {
                OrderDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                HelpItemModel itemModel = new HelpItemModel
                {
                    HelpItemGUID = new Guid(Request.QueryString[Constants.QUERYSTR_HELPITEM_GUID]),
                    Answer = AnswerTextBox.Text,
                    Question = QuestionTextBox.Text,
                    //LanguageGUID = new Guid(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]),
                    ProgramGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]),
                    Order = Convert.ToInt32(OrderDropDownList.SelectedValue)
                };
                Resolve<IHelpItemService>().Update(itemModel);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
