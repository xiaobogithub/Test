using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class EditEmailTemplate : PageBase<EmailTemplateModel>
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProgramGUID))
            {
                if (!IsPostBack)
                {
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
                    try
                    {
                        FormatControl();
                        FormatInformation();
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

        private void FormatControl()
        {
            //ddlLanguage.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
            //ddlLanguage.DataBind();

            //ddlProgram.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
            //ddlProgram.DataBind();
            //BindProgramLanguage();
            EmailTemplateTypesModel emailTemplateTypes = Resolve<IEmailTemplateTypeService>().GetAll();
            ddlEmailTemplateType.DataSource = emailTemplateTypes.EmailTemplateTypeList;

            if (emailTemplateTypes.EmailTemplateTypeList.Count > 0 && emailTemplateTypes.LastSelectedEmailTemplateTypeGuid != Guid.Empty)
            {
                foreach (EmailTemplateTypeModel model in emailTemplateTypes.EmailTemplateTypeList)
                {
                    if (model.EmailTemplateTypeGuid == emailTemplateTypes.LastSelectedEmailTemplateTypeGuid)
                    {
                        ddlEmailTemplateType.SelectedIndex = emailTemplateTypes.EmailTemplateTypeList.IndexOf(model);
                        break;
                    }
                }
            }
            ddlEmailTemplateType.DataBind();
        }


        private void FormatInformation()
        {
            EmailTemplateTypeModel model = Resolve<IEmailTemplateTypeService>().GetItem(new Guid(ddlEmailTemplateType.SelectedValue));
            labEmailTemlateTypeDescription.Text = model.Description;

            ProgramModel programModel = Resolve<IProgramService>().GetProgramByGUID(new Guid(ProgramGUID));
            programLabel.Text = programModel.ProgramName;
            Model = Resolve<IEmailTemplateService>().GetByProgramEmailTemplageType(programModel.Guid, new Guid(ddlEmailTemplateType.SelectedValue));
            if (Model != null)
            {
                txtName.Text = Model.Name;
                txtSubject.Text = Model.Subject;
                txtBody.Text = Model.Body;
                if (ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.InviteFriend.ToString() && ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.Welcome.ToString())
                {
                    linkTextPanel.Visible = true;
                    linkTextTextBox.Text = Model.LinkText;
                }
                else
                {
                    linkTextPanel.Visible = false;
                }
                ViewState["EmailTemplateGuid"] = Model.EmailTemplateGuid;
            }
            else
            {
                txtName.Text = string.Empty;
                txtSubject.Text = string.Empty;
                txtBody.Text = string.Empty;
                if (ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.InviteFriend.ToString() && ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.Welcome.ToString())
                {
                    linkTextPanel.Visible = true;
                    linkTextTextBox.Text = string.Empty;
                }
                else
                {
                    linkTextPanel.Visible = false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Guid emailTemplateTypeGuid = new Guid(ddlEmailTemplateType.SelectedValue);
                EmailTemplateModel emailTemplateModel = Resolve<IEmailTemplateService>().GetByProgramEmailTemplageType(new Guid(ProgramGUID), emailTemplateTypeGuid);
                if (emailTemplateModel == null)
                {
                    EmailTemplateModel etModel = new EmailTemplateModel();
                    etModel.EmailTemplateGuid = Guid.NewGuid();
                    etModel.Name = txtName.Text;
                    etModel.Body = txtBody.Text;
                    etModel.Subject = txtSubject.Text;
                    etModel.Program = new ProgramModel
                    {
                        Guid = new Guid(ProgramGUID),
                    };
                    etModel.Type = new EmailTemplateTypeModel
                   {
                       EmailTemplateTypeGuid = emailTemplateTypeGuid
                   };
                    if (ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.InviteFriend.ToString() && ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.Welcome.ToString())
                    {
                        etModel.LinkText = linkTextTextBox.Text;
                    }

                    Resolve<IEmailTemplateService>().Insert(etModel);
                }
                else
                {
                    emailTemplateModel.Name = txtName.Text;
                    emailTemplateModel.Body = txtBody.Text;
                    emailTemplateModel.Subject = txtSubject.Text;
                    emailTemplateModel.Program = new ProgramModel
                    {
                        Guid = new Guid(ProgramGUID),
                    };
                    emailTemplateModel.Type = new EmailTemplateTypeModel
                    {
                        EmailTemplateTypeGuid = emailTemplateTypeGuid
                    };
                    if (ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.InviteFriend.ToString() && ddlEmailTemplateType.SelectedItem.Text != EmailTypeEnum.Welcome.ToString())
                    {
                        emailTemplateModel.LinkText = linkTextTextBox.Text;
                    }

                    Resolve<IEmailTemplateService>().Update(emailTemplateModel);
                }
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            string goBackUrl = string.Format(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
            Response.Redirect(goBackUrl);
        }

        //protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        BindProgramLanguage();
        //        FormatInformation();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //}

        //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FormatInformation();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        throw ex;
        //    }
        //}

        protected void ddlEmailTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FormatInformation();
            }
            catch(Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }
    }
}
