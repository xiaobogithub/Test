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
using System.Web.UI.HtmlControls;
using System.Collections;
using ChangeTech.Services;
using System.Text;
using System.Globalization;

namespace ChangeTech.DeveloperWeb.OrderSystem
{
    public partial class AddNewOrder : PageBase<OrderModel>
    {
        private const int GUID_LENGTH = 36;
        private const string DATE_FORMAT = "dd.MM.yyyy";
        private const string CHECKBOX_STATUS = "on";
        private const string OPENLICENCEGUID = "A0F8FD76-8DA3-4592-83FB-339D4BF5419F";
        private const string SIMPLELICENCEGUID = "C1A3BC0F-07C1-42E2-86CC-635186DE8837";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitPage();
                    this.txtExpiredDate.Text = DateTime.UtcNow.AddYears(1).ToString(DATE_FORMAT);
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }

                //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Constants.URL_ORDERSYSTEM_LIST;
            }
        }

        private void InitPage()
        {
            BindLanguageDDL();
            BindLicenceTypeDDL();
        }

        private void BindLicenceTypeDDL()
        {
            ddlLicenceType.DataSource = Resolve<IOrderService>().GetAllOrderLicenceTypes();
            ddlLicenceType.DataValueField = "OrderLicenceTypeGUID";
            ddlLicenceType.DataTextField = "TypeName";
            ddlLicenceType.DataBind();
        }

        private void BindLanguageDDL()
        {
            ddlLanguage.DataSource = Resolve<ILanguageService>().GetAllProgramLanguageModel();
            ddlLanguage.DataValueField = "LanguageGUID";
            ddlLanguage.DataTextField="Name";
            ddlLanguage.DataBind();
            ddlLanguage.Items.Insert(0, new ListItem("-Please select language-", Guid.Empty.ToString()));
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            //Insert a order into Order table.
            try
            {
                DateTime expiredDate;
                DateTime.TryParseExact(this.txtExpiredDate.Text.Trim(), DATE_FORMAT, null, DateTimeStyles.None, out expiredDate);
                Guid orderGuid = Guid.NewGuid();
                OrderModel orderModel = new OrderModel()
                {
                    OrderGUID = orderGuid,
                    CustomerName = this.txtCustomerName.Text.Trim(),
                    CustomerEmail = this.txtCustomerEmail.Text.Trim(),
                    LanguageGUID = new Guid(this.ddlLanguage.SelectedValue),
                    LicenceTypeGUID = new Guid(this.ddlLicenceType.SelectedValue),
                    //NumberOfEmployees = !string.IsNullOrEmpty(txtNumOfEmployees.Text.Trim()) ? Convert.ToInt32(txtNumOfEmployees.Text.Trim()) : null,
                    OrderStatusID = Convert.ToInt32(OrderStatusEnum.Active),
                    ExpiredDate = expiredDate,
                    IsPromotion = this.chkPromotion.Checked,
                    UpdatedBy = ContextService.CurrentAccount.UserGuid,
                    Created = DateTime.UtcNow
                };
                SetNumberOfEmployeesOfOrder(orderModel);
                Dictionary<Guid, int> licenceDic = GetProgramAndLicenceInOrder();
                orderModel.orderContents = GetOrderContentsByOrder(licenceDic, orderModel);
                
                // add log about new order.
                InsertLogModel model = new InsertLogModel
                {
                    ActivityLogType = (int)LogTypeEnum.AddOrder,
                    Browser = HttpContext.Current.Request.UserAgent,
                    IP = HttpContext.Current.Request.UserHostAddress,
                    Message = "New OrderGUID : "+orderModel.OrderGUID,
                    ProgramGuid = Guid.Empty,
                    PageGuid = Guid.Empty,
                    PageSequenceGuid = Guid.Empty,
                    SessionGuid = Guid.Empty,
                    UserGuid = ContextService.CurrentAccount.UserGuid,
                    From = string.Empty
                };
                Resolve<IActivityLogService>().Insert(model);

                //insert data into Order and OrderContent table.
                Resolve<IOrderService>().InsertOrder(orderModel);
                //Send email to customer.
                Resolve<IEmailService>().SendEmailToCustomer(orderModel);
                // set Redirect funtion's second argument is false in Response.Redirect funtion of try catch .
                Response.Redirect(Constants.URL_ORDERSYSTEM_HOME, false);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        //Cancel Create new order
        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_ORDERSYSTEM_LIST);
        }

        //OrderLicenceType Changed Event.
        protected void ddlLicenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlLicenceType.SelectedValue) && ddlLicenceType.SelectedValue.ToUpper() == OPENLICENCEGUID)
            {
                txtNumOfEmployees.Enabled = true;
                chkPromotion.Enabled = false;
                if (txtNumOfEmployees.Text.Length<=0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Warning","<script type='text/javascript'>alert('');</script>");
                }
            }
            else
            {
                txtNumOfEmployees.Enabled = false;
                chkPromotion.Enabled = true;
            }
        }

        private void SetNumberOfEmployeesOfOrder(OrderModel orderModel)
        {
            int numOfEmployees = 0;
            switch (ddlLicenceType.SelectedValue.ToUpper())
            {
                case OPENLICENCEGUID:
                    if (!string.IsNullOrEmpty(txtNumOfEmployees.Text.Trim()) && int.TryParse(txtNumOfEmployees.Text.Trim(), out numOfEmployees))
                    {
                        orderModel.NumberOfEmployees = numOfEmployees;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Warning", "<script type='text/javascript'>alert('Please enter the correct NumOfEmployees !');</script>");
                        return;
                    }
                    break;
                case SIMPLELICENCEGUID:
                    if (!string.IsNullOrEmpty(txtNumOfEmployees.Text.Trim()))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Warning", "<script type='text/javascript'>alert('NumOfEmployees should be Empty !');</script>");
                        return;
                    }
                    else
                    {
                        orderModel.NumberOfEmployees = null;
                    }
                    break;
            }
        }

        private List<OrderContentModel> GetOrderContentsByOrder(Dictionary<Guid, int> licenceDic, OrderModel orderModel)
        {
            List<OrderContentModel> orderContents = new List<OrderContentModel>();
            if (licenceDic.Count > 0)
            {
                OrderContentModel orderContentModel = null;
                foreach (var item in licenceDic)
                {
                    orderContentModel = new OrderContentModel()
                    {
                        OrderContentGUID = Guid.NewGuid(),
                        OrderGUID = orderModel.OrderGUID,
                        ProgramGUID = item.Key,
                        ProgramLicences = item.Value,
                        UpdatedBy = ContextService.CurrentAccount.UserGuid
                    };
                    orderContents.Add(orderContentModel);
                }
            }
            return orderContents;
        }
            
        //Get programGUID and licence's number.
        private Dictionary<Guid, int> GetProgramAndLicenceInOrder()
        {
            int allLicences = 0;
            int licencesByProgram = 0;
            Guid programGuid = Guid.Empty;

            //Get programGUID and licence's number.
            Dictionary<Guid, int> licenceDic = new Dictionary<Guid, int>();
            foreach (string key in Request.Form.Keys)
            {
                if (key.Length == GUID_LENGTH && !string.IsNullOrEmpty(Request.Params[key]))
                {
                    if (Guid.TryParse(key, out programGuid))
                    {
                        if (int.TryParse(Request.Params[key], out licencesByProgram)) //Input TextBox
                        {
                            licenceDic.Add(programGuid, licencesByProgram);
                            allLicences += licencesByProgram;
                        }
                        else //Input CheckBox
                        {
                            if (Request.Params[key] == CHECKBOX_STATUS)
                            {
                                //string openLicenceProgram = "OpenLicence";
                                licenceDic.Add(programGuid, 0);
                            }
                        }
                    }
                }
            }
            return licenceDic;
        }

        //If current order's promotion checked , and update OrderLicence table data. (Insert the specified number of data  according program and licences )
        private void InsertProgramLicenceByPromotion(OrderModel orderModel, List<OrderContentModel> orderContents)
        {
            if (orderModel.IsPromotion.Value)
            {
                OrderLicenceModel orderLicenceModel = null;
                foreach (var orderContentModel in orderContents)
                {
                    for (int i = 0; i < orderContentModel.ProgramLicences; i++)
                    {
                        //string promotionCode = ServiceUtility.GetPromotionCode();
                        orderLicenceModel = new OrderLicenceModel()
                        {
                            OrderLicenceGUID = Guid.NewGuid(),
                            OrderContentGUID = orderContentModel.OrderContentGUID,
                            ProgramUserGUID = null,
                            //PromotionCode = promotionCode,
                            UpdatedBy = ContextService.CurrentAccount.UserGuid
                        };
                        //insert orderLicence
                        Resolve<IOrderLicenceService>().InsertOrderLicence(orderLicenceModel);
                    }
                }
            }
        }

    }
}