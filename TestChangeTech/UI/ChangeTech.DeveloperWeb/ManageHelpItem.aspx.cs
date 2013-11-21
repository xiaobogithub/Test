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
    public partial class ManageHelpItem : PageBase<HelpItemListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindHelpItem();
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
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

        //private void InitialPage()
        //{
        //    BindProgram();
        //    //BindLanguage();
        //    //BindHelpItem();
        //}

        private void BindHelpItem()
        {
            ProgramModel programmodel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programLabel.Text = programmodel.ProgramName;
            Model = new HelpItemListModel();
            //Model.HelpItemList = new List<HelpItemModel>();
            Model.HelpItemList = Resolve<IHelpItemService>().GetHelpItemModelList(programmodel.Guid);
            HelpItemRepeater.DataSource = Model.HelpItemList;
            HelpItemRepeater.DataBind();
        }

        //private void BindLanguage()
        //{
        //    //LanguageDropDownList.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
        //    //LanguageDropDownList.DataTextField = "Name";
        //    //LanguageDropDownList.DataValueField = "LanguageGUID";
        //    //LanguageDropDownList.DataBind();
        //    //if (!string.IsNullOrEmpty(Constants.QUERYSTR_LANGUAGE_GUID))
        //    //{
        //    //    LanguageDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
        //    //}
        //    if (ProgramDropDownList.SelectedItem != null)
        //    {
        //        string programGUID = ProgramDropDownList.SelectedValue;
        //        LanguageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programGUID));
        //        LanguageDropDownList.DataTextField = "Name";
        //        LanguageDropDownList.DataValueField = "LanguageGUID";
        //        LanguageDropDownList.DataBind();
        //    }

        //    BindHelpItem();
        //}

        //private void BindProgram()
        //{
        //    ProgramDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //    ProgramDropDownList.DataTextField = "ProgramName";
        //    ProgramDropDownList.DataValueField = "ProgramGuid";
        //    ProgramDropDownList.DataBind();
        //    if (!string.IsNullOrEmpty(Constants.QUERYSTR_PROGRAM_GUID))
        //    {
        //        ProgramDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
        //    }

        //    BindLanguage();
        //}

        //protected void LanguageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindHelpItem();
        //    //Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, LanguageDropDownList.SelectedValue));      
        //}

        protected void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                Resolve<IHelpItemService>().MakeHelpItemOrderUp(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(((ImageButton)sender).CommandArgument));
                Response.Redirect(Request.Url.ToStringWithoutPort());
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                Resolve<IHelpItemService>().MakeHelpItemOrderDown(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(((ImageButton)sender).CommandArgument));
                Response.Redirect(Request.Url.ToStringWithoutPort());
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddHelpItem.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string itemguid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditHelpItem.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_HELPITEM_GUID, itemguid, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Guid itemGuid = new Guid(((Button)sender).CommandArgument);
                Resolve<IHelpItemService>().Delete(itemGuid);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }

            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        //protected void ProgramDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Response.Redirect(string.Format("ManageHelpItem.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, ProgramDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, LanguageDropDownList.SelectedValue));
        //    BindLanguage();
        //}

        protected void HelpItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                if (e.Item.ItemIndex == 0)
                {
                    Button upButton = (Button)e.Item.FindControl("ButtonUp");
                    upButton.Visible = false;
                }
                if (e.Item.ItemIndex == Model.HelpItemList.Count - 1)
                {
                    Button DownImageButton = (Button)e.Item.FindControl("ButtonDown");
                    DownImageButton.Visible = false;
                }
            }
        }
    }
}
