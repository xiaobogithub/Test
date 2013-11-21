using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using Ethos.Utility;
using ChangeTech.Contracts;
using System.Globalization;
using ChangeTech.DeveloperWeb.HealthProfileSystem.common;

namespace ChangeTech.DeveloperWeb.HealthProfileSystem
{
    public partial class AddNewOrder : PageBase<HPOrderModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitPage();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }

        private void InitPage()
        {
            BindLocationDDL();
            BindIndustryDDL();
            BindLanguageAndProgram();
        }

        private void BindLocationDDL()
        {
            ddlLocation.DataSource = Resolve<IHPOrderService>().GetHPOrderParamsByType(HpConstants.STR_LOCATION);
            ddlLocation.DataValueField = "HPOrderParamID";
            ddlLocation.DataTextField = "HPOrderParamName";
            ddlLocation.DataBind();
        }

        private void BindIndustryDDL()
        {
            ddlIndustry.DataSource = Resolve<IHPOrderService>().GetHPOrderParamsByType(HpConstants.STR_INDUSTRY);
            ddlIndustry.DataValueField = "HPOrderParamID";
            ddlIndustry.DataTextField = "HPOrderParamName";
            ddlIndustry.DataBind();
            //ddlLanguage.Items.Insert(0, new ListItem("-Please select language-", Guid.Empty.ToString()));
        }

        private void BindLanguageAndProgram()
        {
            ddlLanguage.DataSource = Resolve<ILanguageService>().GetLanguagesModel();
            ddlLanguage.DataValueField = "LanguageGUID";
            ddlLanguage.DataTextField = "Name";
            ddlLanguage.DataBind();
            BindProgramDDL(new Guid(ddlLanguage.SelectedValue));
        }

        private void BindProgramDDL(Guid languageGuid)
        {
            ddlHPProgram.DataSource = Resolve<IProgramService>().GetHPOrderProgramsByLanguageGuid(languageGuid);
            ddlHPProgram.DataValueField = "ProgramGuid";
            ddlHPProgram.DataTextField = "ProgramName";
            ddlHPProgram.DataBind();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate;
                DateTime stopDate;
                int numOfEmployees = 0;
                HPOrderModel orderModel = new HPOrderModel();
                DateTime.TryParseExact(this.txtStartDate.Text.Trim(), HpConstants.DATE_FORMAT, null, DateTimeStyles.None, out startDate);
                DateTime.TryParseExact(this.txtStopDate.Text.Trim(), HpConstants.DATE_FORMAT, null, DateTimeStyles.None, out stopDate);
                orderModel.HPOrderGUID = Guid.NewGuid();
                orderModel.ProgramGUID = new Guid(ddlHPProgram.SelectedValue);
                orderModel.OrderStatus = Convert.ToInt32(OrderStatusEnum.Active);
                orderModel.CustomerName = string.IsNullOrEmpty(txtCustomerName.Text) ? string.Empty : txtCustomerName.Text.Trim();
                orderModel.ContactPersonName = string.IsNullOrEmpty(txtContactPersonName.Text) ? string.Empty : txtContactPersonName.Text.Trim();
                orderModel.ContactPersonNumber = string.IsNullOrEmpty(txtContactPersonNum.Text) ? string.Empty : txtContactPersonNum.Text.Trim();
                orderModel.ContactPersonPhone = string.IsNullOrEmpty(txtContactPersonPhone.Text) ? string.Empty : txtContactPersonPhone.Text.Trim();
                orderModel.ContactPersonEmail = string.IsNullOrEmpty(txtContactPersonEmail.Text) ? string.Empty : txtContactPersonEmail.Text.Trim();
                orderModel.SOSIContactEmail = string.IsNullOrEmpty(txtSOSIContactEmail.Text) ? string.Empty : txtSOSIContactEmail.Text.Trim();
                orderModel.StartDate = startDate;
                orderModel.StopDate = stopDate;
                orderModel.LocationID = Resolve<IHPOrderService>().GetHPOrderParamByParamName(ddlLocation.SelectedItem.Text).HPOrderParamID;
                orderModel.IndustryID = Resolve<IHPOrderService>().GetHPOrderParamByParamName(ddlIndustry.SelectedItem.Text).HPOrderParamID;
                orderModel.UpdatedBy = Resolve<IUserService>().GetCurrentUser().UserGuid;
                if (int.TryParse(txtUserCount.Text.Trim(), out numOfEmployees))
                {
                    orderModel.NumberOfEmployees = numOfEmployees;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Warning", "<script type='text/javascript'>alert('Please enter the correct NumOfEmployees !');</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Warning", "<script type='text/javascript'>alert('123');</script>");
                    return;
                }
                // add log about new order.
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.AddOrder,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    Message = "New HP OrderGUID : " + orderModel.HPOrderGUID,
                    ProgramGuid = orderModel.ProgramGUID,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = ContextService.CurrentAccount.UserGuid,
                    From = string.Empty
                };
                Resolve<IActivityLogService>().Insert(model);
                //insert new HpOrder to HPOrder table.
                Resolve<IHPOrderService>().InsertHPOrder(orderModel);
                //insert need send email to HPEmailTemplate table.
                Resolve<IHPOrderEmailService>().InsertHPOrderEmail(orderModel);
                // set Redirect funtion's second argument is false in Response.Redirect funtion of try catch .
                Response.Redirect(Constants.URL_HPSYSTEM_MAINPAGE, false);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_HPSYSTEM_MAINPAGE);
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProgramDDL(new Guid(ddlLanguage.SelectedValue));
        }
    }
}