using System;

using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Collections.Generic;
using ChangeTech.Contracts;
using System.Web.UI.WebControls;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageUserMenu : PageBase<UserMenuModelCollection>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    BindMenuList();
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            string menuItemGUID = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("~/EditUserMenu.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_MENUITEM_GUID, menuItemGUID,Constants.QUERYSTR_PROGRAM_GUID,Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }

        protected void menuRepeater_ItemDataBind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                UserMenuModel menuModel = e.Item.DataItem as UserMenuModel;
                Button availableButton = e.Item.FindControl("availableButton") as Button;
                if (menuModel.Available)
                {
                    availableButton.Text = "Unable";
                }
                else
                {
                    availableButton.Text = "Enable";
                }
            }
        }

        protected void FiliterButton_Click(object sender, EventArgs e)
        {
            BindMenuList();
        }

        private void BindMenuList()
        {
            try
            {
                ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                programLabel.Text = programModel.ProgramName;

                List<UserMenuModel> userMenuList = Resolve<IUserMenuService>().GetUserMenusOfProram(programModel.Guid);
                menuRepeater.DataSource = userMenuList;
                menuRepeater.DataBind();
                addUesrMenuLinkButton.Visible = true;
                if (userMenuList.Count > 0)
                {
                    addUesrMenuLinkButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                msgLbl.Text = "Fail to get menus, internal error occurs, please report to Change Tech develop team.";
            }
        }

        //private void BindLanguage()
        //{
        //    try
        //    {
        //        ddlLanguage.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
        //        ddlLanguage.DataTextField = "Name";
        //        ddlLanguage.DataValueField = "LanguageGUID";
        //        ddlLanguage.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        msgLbl.Text = "Fail to get languages, internal error occurs, please report to Change Tech develop team.";
        //    }
        //}

        //private void BindProgram()
        //{
        //    try
        //    {
        //        ddlProgram.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
        //        ddlProgram.DataTextField = "ProgramName";
        //        ddlProgram.DataValueField = "ProgramGuid";
        //        ddlProgram.DataBind();

        //        BindLanguage();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        msgLbl.Text = "Fail to get programs, internal error occurs, please report to Change Tech develop team.";
        //    }
        //}

        //private void BindLanguage()
        //{
        //    string programGUID = ddlProgram.SelectedValue;
        //    ddlLanguage.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programGUID));
        //    ddlLanguage.DataBind();

        //    BindMenuList();
        //}

        //protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindLanguage();
        //}

        //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindMenuList();
        //}

        protected void addUesrMenuLinkButton_Click(object sender, EventArgs e)
        {
            //ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(ddlProgram.SelectedValue),
            //   new Guid(ddlLanguage.SelectedValue));
            Resolve<IUserMenuService>().AddUserMenuForProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            BindMenuList();
        }

        protected void availableButton_Click(object sender, EventArgs e)
        {
            Button availableButton = sender as Button;
            Guid itemGUID = new Guid(availableButton.CommandArgument);
            if (availableButton.Text.Equals("Enable"))
            {
                Resolve<IUserMenuService>().EnableUserMenu(itemGUID);
            }
            else
            {
                Resolve<IUserMenuService>().UnableUserMenu(itemGUID);
            }

            BindMenuList();
        }
    }
}
