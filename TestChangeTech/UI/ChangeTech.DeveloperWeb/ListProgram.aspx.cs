using System;

using ChangeTech.Contracts;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using System.Web.UI.WebControls;
using System.Web;
using System.Configuration;
using System.Resources;
using System.Reflection;
using Ethos.Utility;
using System.Collections.Generic;

namespace ChangeTech.DeveloperWeb
{
    public partial class ProgramManagement : PageBase<ProgramsModel>
    {
        private static int SORT_INDEX = 0;
        private static string SORT_VALUE = "Name";
        private const string ASSIGN_MANAGER = "Assign managers";
        private const string COPY_PROGRAM = "Copy program";
        private const string PROGRAM_NAME = "Name";
        private const string PROGRAM_DATE = "Date";
        private const string PROGRAM_USERS = "Users";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindProgram();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        #region Private Methods
        private void BindProgram()
        {
            if (Resolve<IUserService>().HasPermission((PermissionEnum)ContextService.CurrentAccount.Security, PermissionEnum.ApplicationCreate, (PermissionEnum)ContextService.CurrentAccount.Security))
            {
                Button addNewProgram = (Button)Master.FindControl("ContentPlaceHolder1").FindControl("btnAddNewProgram");
                addNewProgram.Enabled = true;
            }
            int numberOfProgram = Resolve<IProgramService>().GetNumberOfProgram();
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfProgram) / 15), "Name", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            List<ProgramModel> programs = new List<ProgramModel>();
            programs = Resolve<IProgramService>().GetSortProgramsModel(CurrentPageNumber, 15, SORT_VALUE);
            rpProgram.DataSource = programs;
            rpProgram.DataBind();
        }
        #endregion

        #region Control events
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            string programGuid = editButton.CommandArgument;
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, CurrentPageNumber, Constants.QUERYSTR_PROGRAM_GUID, programGuid));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button deleteButton = sender as Button;
                string programGuid = deleteButton.CommandArgument;
                if (Resolve<IProgramService>().CanDeleteProgram(new Guid(programGuid)))
                {
                    Resolve<IProgramService>().DeleteProgram(new Guid(programGuid));
                    //BindProgram();   
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "tip", "alert('You cannot delete this program, because there are users still using it.');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void btnAddNewProgram_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_PAGE, CurrentPageNumber));
        }

        protected void MoreOptionsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList moreOptionsDDL = (DropDownList)sender;
            string selectOption = moreOptionsDDL.SelectedValue;
            string programGuid = moreOptionsDDL.DataValueField;
            switch (selectOption)
            {
                case ASSIGN_MANAGER:
                    Response.Redirect(string.Format("AssignProjectManager.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, CurrentPageNumber, Constants.QUERYSTR_PROGRAM_GUID, programGuid));
                    break;
                case COPY_PROGRAM:
                    try
                    {
                        if (!string.IsNullOrEmpty(programGuid))
                        {
                            Resolve<IProgramService>().CopyProgram(new Guid(programGuid), ContextService.CurrentAccount.UserGuid);
                        }
                        //BindProgram();
                        ContextService.CurrentAccount.ProgramSecuirty = Resolve<IProgramService>().GetProgramPermissionByUserGuid(ContextService.CurrentAccount.UserGuid);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                    break;
                default:
                    break;
            }
        }

        protected void ProgramSortDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ProgramModel> programs = new List<ProgramModel>();
            DropDownList programSortDDL = (DropDownList)sender;

            #region PagingString
            int numberOfProgram = Resolve<IProgramService>().GetNumberOfProgram();
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfProgram) / 15), "Name", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            #endregion

            SORT_INDEX = programSortDDL.SelectedIndex;
            SORT_VALUE = programSortDDL.SelectedValue;
            switch (programSortDDL.SelectedValue)
            {
                case PROGRAM_NAME:
                    programs = Resolve<IProgramService>().GetSortProgramsModel(CurrentPageNumber, 15, PROGRAM_NAME);
                    rpProgram.DataSource = programs;
                    rpProgram.DataBind();
                    break;
                case PROGRAM_DATE:
                    programs = Resolve<IProgramService>().GetSortProgramsModel(CurrentPageNumber, 15, PROGRAM_DATE);
                    rpProgram.DataSource = programs;
                    rpProgram.DataBind();
                    break;
                case PROGRAM_USERS:
                    programs = Resolve<IProgramService>().GetSortProgramsModel(CurrentPageNumber, 15, PROGRAM_USERS);
                    rpProgram.DataSource = programs;
                    rpProgram.DataBind();
                    break;
            }
        }

        protected void programManagerLinkButton_Click(object sender, EventArgs e)
        {
            string programGuid = ((LinkButton)sender).CommandArgument;
            Response.Redirect(string.Format("AssignProjectManager.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_PAGE, CurrentPageNumber, Constants.QUERYSTR_PROGRAM_GUID, programGuid));
        }
        
        protected void rpProgram_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (Resolve<IUserService>().HasPermission((Permission)ContextService.CurrentAccount.Security, Permission.ApplicationSuperAdmin, (Permission)ContextService.CurrentAccount.Security))
            //{
            //    if (e.Item.ItemType == ListItemType.Header)
            //    {
            //        ((Button)e.Item.FindControl("btnAddNewProgram")).Enabled = true;
            //    }
            //    if (e.Item.DataItem != null)
            //    {
            //        ((Button)e.Item.FindControl("btnMakeCopy")).Enabled = true;
            //        ((Button)e.Item.FindControl("btnSecurity")).Enabled = true;
            //        ((Button)e.Item.FindControl("btnDelete")).Enabled = true;
            //        ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
            //        //*******************************************************
            //        //Update by: Li Shulei
            //        //Issue: 522
            //        //Update For: Customer can preview the program
            //        //*******************************************************
            //        ((Button)e.Item.FindControl("btnPreview")).Enabled = true;
            //    }
            //}
            //else
            //{
            if (e.Item.ItemType == ListItemType.Header)
            {
                //// applictaion security
                //if(Resolve<IUserService>().HasPermission((PermissionEnum)ContextService.CurrentAccount.Security, PermissionEnum.ApplicationCreate, (PermissionEnum)ContextService.CurrentAccount.Security))
                //{
                //    ((Button)e.Item.FindControl("btnAddNewProgram")).Enabled = true;
                //}
                List<ProgramModel> programs = new List<ProgramModel>();
                DropDownList programSortDDL = (DropDownList)e.Item.FindControl("ProgramSortDDL");
                programSortDDL.SelectedIndex = SORT_INDEX;
            }
            if (e.Item.DataItem != null)
            {
                // applictaion security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)ContextService.CurrentAccount.Security, PermissionEnum.ApplicationCreate, (PermissionEnum)ContextService.CurrentAccount.Security))
                {
                    //((Button)e.Item.FindControl("btnMakeCopy")).Enabled = true;
                    ((DropDownList)e.Item.FindControl("MoreOptionsDDL")).Enabled = true;
                }

                // program security
                if (Resolve<IUserService>().HasPermission((PermissionEnum)ContextService.CurrentAccount.Security, PermissionEnum.ApplicationAdmin, (PermissionEnum)ContextService.CurrentAccount.Security))
                {
                    //((Button)e.Item.FindControl("btnSecurity")).Enabled = true;
                    //((Button)e.Item.FindControl("btnDelete")).Enabled = true;
                    ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                }
                if (ContextService.CurrentAccount.ProgramSecuirty.ContainsKey(((ProgramModel)e.Item.DataItem).Guid))
                {
                    int programSecuirty = ContextService.CurrentAccount.ProgramSecuirty[((ProgramModel)e.Item.DataItem).Guid];
                    //*******************************************************
                    //Update by: Li Shulei
                    //Issue: 522
                    //Update For: Customer can preview the program
                    //*******************************************************
                    //if (Resolve<IUserService>().HasPermission((Permission)programSecuirty, Permission.ProgramView, (Permission)ContextService.CurrentAccount.Security))
                    //{
                    //    ((Button)e.Item.FindControl("btnPreview")).Enabled = true;
                    //}
                    //if (Resolve<IUserService>().HasPermission((Permission)programSecuirty, Permission.ProgramAdmin, (Permission)ContextService.CurrentAccount.Security))
                    //{
                    //    ((Button)e.Item.FindControl("btnSecurity")).Enabled = true;
                    //}
                    //if (Resolve<IUserService>().HasPermission((Permission)programSecuirty, Permission.ProgramDelete, (Permission)ContextService.CurrentAccount.Security))
                    //{
                    //    ((Button)e.Item.FindControl("btnDelete")).Enabled = true;
                    //}
                    if (Resolve<IUserService>().HasPermission((PermissionEnum)programSecuirty, PermissionEnum.ProgramEdit, (PermissionEnum)ContextService.CurrentAccount.Security))
                    {
                        ((Button)e.Item.FindControl("btnEdit")).Enabled = true;
                    }
                }
            }
            //}
        }
        #endregion

        #region so far not use
        //protected void btnMakeCopy_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Button copyButton = sender as Button;
        //        string programGuid = copyButton.CommandArgument;
        //        if (!string.IsNullOrEmpty(programGuid))
        //        {
        //            Resolve<IProgramService>().CopyProgram(new Guid(programGuid), ContextService.CurrentAccount.UserGuid);
        //        }
        //        //BindProgram();
        //        ContextService.CurrentAccount.ProgramSecuirty = Resolve<IProgramService>().GetProgramPermissionByUserGuid(ContextService.CurrentAccount.UserGuid);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //    Response.Redirect(Request.Url.ToStringWithoutPort());
        //}

        //protected void btnSecurity_Click(object sender, EventArgs e)
        //{
        //    string programGuid = ((Button)sender).CommandArgument;
        //    Response.Redirect(string.Format("ManageProgramSecurity.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, programGuid));
        //}

        //protected void btnPreview_Click(object sender, EventArgs e)
        //{
        //    string programGuid = ((Button)sender).CommandArgument;
        //    Response.Redirect(string.Format("Preview.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, programGuid));
        //}

        //private ProgramsModel InitialProgramURL(ProgramsModel programsModel)
        //{
        //    foreach (ProgramModel model in programsModel)
        //    {
        //        model.ScreenURL = string.Format(Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, model.Guid);
        //        model.LoginURL = string.Format(Resolve<ISystemSettingService>().GetSettingValue("WebServerPath") + "ChangeTech.html?Mode=Live&{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, model.Guid);
        //        //model.ScreenURL = string.Format(ConfigurationManager.AppSettings["WebServerPath"] + "ChangeTech.html?Mode=Trial&{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, model.Guid);
        //        //model.LoginURL = string.Format(ConfigurationManager.AppSettings["WebServerPath"] + "ChangeTech.html?Mode=Live&{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, model.Guid);
        //    }

        //    return programsModel;
        //} 
        #endregion
    }
}
