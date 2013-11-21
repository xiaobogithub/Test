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
    public partial class ManageOrderEmailTemplate : PageBase<OrderEmailTemplateModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindLanguageDropDownList();
                    InitialOrderEmailTemplate();
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Constants.URL_DEVELOPER_HOME;
        }

        private void InitialOrderEmailTemplate()
        {
            string languageGuid = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]))
            {
                languageGuid = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
            }
            else
            {
                languageGuid = ddlLanguage.SelectedValue;
            }

            ManageOrderEmailTemplateByLanguage();
        }

        private void BindLanguageDropDownList()
        {
            ddlLanguage.DataSource = Resolve<ILanguageService>().GetAllProgramLanguageModel();
            ddlLanguage.DataBind();
            if (Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID] != null)
            {
                ddlLanguage.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OrderEmailTemplateModel oetModel = new OrderEmailTemplateModel
                {
                    Name = txtName.Text.Trim(),
                    Body = txtBody.Text.Trim(),
                    Subject = txtSubject.Text.Trim(),
                    EmailTemplateTypeGUID = new Guid(Resolve<ISystemSettingService>().GetSettingValue("OrderEmailTemplateGUID")), //Resolve<IEmailTemplateTypeService>().GetEmailTemplateTypeByTypeID(Convert.ToInt32(EmailTemplateTypeEnum.OrderEmail)).EmailTemplateTypeGuid,
                    LanguageGUID = new Guid(ddlLanguage.SelectedValue),
                    LastUpdated = DateTime.UtcNow,
                    LastUpdatedBy = ContextService.CurrentAccount.UserGuid,
                    OrderEmailTemplateGUID = new Guid(ViewState["OrderEmailTemplateGuid"].ToString())
                };
                    
                if (oetModel.OrderEmailTemplateGUID == Guid.Empty)
                {
                    oetModel.OrderEmailTemplateGUID = Guid.NewGuid();
                    Resolve<IOrderEmailTemplateService>().InsertOrderEmailTemplateModel(oetModel);
                }
                else
                {
                    Resolve<IOrderEmailTemplateService>().UpdateOrderEmailTemplateModel(oetModel);
                }
                Response.Redirect("ManageOrderEmailTemplate.aspx");
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ManageOrderEmailTemplateByLanguage();
            Response.Redirect(string.Format("ManageOrderEmailTemplate.aspx?{0}={1}", Constants.QUERYSTR_LANGUAGE_GUID, ddlLanguage.SelectedValue));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_DEVELOPER_HOME);
        }

        protected void ManageOrderEmailTemplateByLanguage()
        {
            Guid emailTemplateTypeGuid = new Guid(Resolve<ISystemSettingService>().GetSettingValue("OrderEmailTemplateGUID"));
            EmailTemplateTypeModel ettModel = Resolve<IEmailTemplateTypeService>().GetEmailTemplateTypeByEmailTemplateTypeGuid(emailTemplateTypeGuid);
            OrderEmailTemplateModel orderEmailTemplateModel = Resolve<IOrderEmailTemplateService>().GetOrderEmailTemplateByEmailTemplateTypeGuidAndLanguageGuid(ettModel.EmailTemplateTypeGuid, new Guid(ddlLanguage.SelectedValue));
            if (orderEmailTemplateModel != null)
            {
                txtName.Text = orderEmailTemplateModel.Name;
                txtSubject.Text = orderEmailTemplateModel.Subject;
                txtBody.Text = orderEmailTemplateModel.Body;
                ViewState["OrderEmailTemplateGuid"] = orderEmailTemplateModel.OrderEmailTemplateGUID;
            }
            else
            {
                txtName.Text = string.Empty;
                txtSubject.Text = string.Empty;
                txtBody.Text = string.Empty;
                ViewState["OrderEmailTemplateGuid"] = Guid.Empty;
            }
        }
    }
}