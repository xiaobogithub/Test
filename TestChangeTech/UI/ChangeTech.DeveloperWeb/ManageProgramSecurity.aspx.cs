using System;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;
using System.Web.UI.WebControls;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageProgramSecurity : PageBase<ProgramSecurityModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    BindProgramDropDown();
                    BindSecurityList();
                }
                catch(Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "ListProgram.aspx";
            }
        }

        private void BindProgramDropDown()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
            if(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                programDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }

            BindLanguageDropDown();
        }

        private void BindLanguageDropDown()
        {
            languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
            languageDropDownList.DataTextField = "Name";
            languageDropDownList.DataValueField = "LanguageGUID";
            languageDropDownList.DataBind();
            if (Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID] != null)
            {
                languageDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
            }
        }

        private void BindSecurityList()
        {
            Guid programGuid = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(programDropDownList.SelectedValue), new Guid(languageDropDownList.SelectedValue)).Guid;
            // for paging and soring
            int numberOfUsers = Resolve<IProgramService>().GetNumberOfUser(programGuid, emailTextBox.Text);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberOfUsers) / PageSize), "Account", "asc");
            string url = Request.Url.ToStringWithoutPort();
            //string[] header = { (string)base.GetLocalResourceObject("Account") };
            //string[] sortExpression = { "Account" };
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            //HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);
            ProgramSecurityModel programUser = Resolve<IProgramService>().GetProgramSecuirtyModel(programGuid, emailTextBox.Text, CurrentPageNumber, PageSize);
            securityListRepeater.DataSource = programUser;
            securityListRepeater.DataBind();
        }

        protected void RegisterLinkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("NewUserForProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue));
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        //protected void AddOldUserLinkButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Response.Redirect(string.Format("AddExistUserForProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue));
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //}

        protected void editBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedProgramUser = ((Button)sender).CommandArgument;
                UserSecurityModel us = Resolve<IProgramService>().GetProgramUserSecureityModel(new Guid(selectedProgramUser));
                programUserGuidHf.Value = us.ProgramUserGuid.ToString();
                StatusDropdownList.Text = us.Status;
                programSecurityMultiView.ActiveViewIndex = 1;

                userEmailTextBox.Text = us.Account;
                firstNameTextBox.Text = us.FirstName;
                lastNameTextBox.Text = us.LastName;
                mobileTextBox.Text = us.Mobile;
                pinCodeTxtBox.Text = us.Pincode;
                serialNumberTextBox.Text = us.SerialNumber;
                if (us.Gender.Trim().Equals("Female"))
                {
                    genderDropDownList.SelectedIndex = 1;
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void deleteButton_click(object sender, EventArgs e)
        {
            try
            {
                string programuserGuid = ((Button)sender).CommandArgument;
                Resolve<IProgramService>().DeleteProgramUser(new Guid(programuserGuid));
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(Request.Url.ToStringWithoutPort());
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Resolve<IProgramService>().UpdateProgramSecurity(new Guid(programUserGuidHf.Value), userSecurity, mailTime, StatusDropdownList.Text, userEmailTextBox.Text,firstNameTextBox.Text,lastNameTextBox.Text,mobileTextBox.Text,genderDropDownList.SelectedValue);
                EditProgramUserModel editProgramUserModel = new EditProgramUserModel();
                editProgramUserModel.ProgramUsreGUID = new Guid(programUserGuidHf.Value);
                editProgramUserModel.Email = userEmailTextBox.Text.Trim();
                editProgramUserModel.FirstName = firstNameTextBox.Text.Trim();
                editProgramUserModel.Gender = genderDropDownList.SelectedValue;
                editProgramUserModel.LastName = lastNameTextBox.Text.Trim();
                editProgramUserModel.MobilePhone = mobileTextBox.Text.Trim();
                editProgramUserModel.Pincode = pinCodeTxtBox.Text.Trim();
                editProgramUserModel.SerialNumber = serialNumberTextBox.Text.Trim();
                editProgramUserModel.Status = StatusDropdownList.Text;
                Resolve<IProgramUserService>().UpdateProgramUser(editProgramUserModel);
                BindSecurityList();
                programSecurityMultiView.ActiveViewIndex = 0;
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
        protected void securityListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType != ListItemType.Header)
            {
                Label lab = e.Item.FindControl("lbAccount") as Label;
                if(lab != null)
                {
                    if(lab.Text == Context.User.Identity.Name)
                    {
                        ((Button)e.Item.FindControl("btnEdit")).Enabled = false;
                        ((Button)e.Item.FindControl("deleteButton")).Enabled = false;
                    }
                }
            }
        }

        protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageProgramSecurity.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            BindSecurityList();
        }

        protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManageProgramSecurity.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }
    }
}
