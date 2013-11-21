using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb.OrderSystem
{
    public partial class ListOrder : PageBase<OrderModel>
    {
        public const string DATE_FORMAT = "dd.MM.yyyy";
        public string VersionNumber
        {
            get
            {
                return  System.Configuration.ConfigurationManager.AppSettings["ProjectVersion"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindOrderList();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Constants.URL_ORDERSYSTEM_LIST;
            }
        }

        private void BindOrderList()
        {
            UserModel userModel = Resolve<IUserService>().GetCurrentUser();
            List<OrderModel> orderList = Resolve<IOrderService>().GetOrdersByResellerGuid(userModel.UserGuid);
            rpOrderList.DataSource = orderList;
            rpOrderList.DataBind();
        }

        //Add New Order 
        protected void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_ORDERSYSTEM_ADDNEWORDER);
        }

        //Navigate to MoreInfo Page
        protected void btnMoreInfo_Click(object sender, EventArgs e)
        {
            Button moreInfoButton = sender as Button;
            Guid orderGuid = new Guid(moreInfoButton.CommandArgument);
            Response.Redirect(string.Format("~/OrderSystem/OrderInfo.aspx?{0}={1}", Constants.QUERYSTR_ORDERGUID, orderGuid));
        }

        //Cancel Current order
        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            //When click cancel button,reminde user whether cancel message.
            //Cancel Button's Text changed to Cancelled
            Button cancelButton = sender as Button;
            if (cancelButton != null)
            {
                OrderModel orderModel = Resolve<IOrderService>().GetOrderByOrderGuid(new Guid(cancelButton.CommandArgument));
                if (orderModel != null)
                {
                    orderModel.OrderStatusID = (int)OrderStatusEnum.Cancel;
                    // add log about cancel order.
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.CancelOrder,
                        Browser = HttpContext.Current.Request.UserAgent,
                        IP = HttpContext.Current.Request.UserHostAddress,
                        Message = "Cancle Order : " + orderModel.OrderGUID,
                        ProgramGuid = Guid.Empty,
                        PageGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = ContextService.CurrentAccount.UserGuid,
                        From = string.Empty
                    };
                    Resolve<IActivityLogService>().Insert(model);
                    Resolve<IOrderService>().UpdateOrder(orderModel);
                }
            }
            BindOrderList();
        }

        protected void rpOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem!=null)
            {
                Button cancelButton = e.Item.FindControl("btnCancelOrder") as Button;
                cancelButton.Attributes.Add("onClick ", "return   confirm( 'Do you really want to cancel the order?'); ");

                OrderModel orderModel = Resolve<IOrderService>().GetOrderByOrderGuid(new Guid(cancelButton.CommandArgument));
                if (orderModel.ExpiredDate.Value.Date < DateTime.UtcNow.Date)
                {
                    if (orderModel.OrderStatusID != (int)OrderStatusEnum.Expired)
                    {
                        orderModel.OrderStatusID = (int)OrderStatusEnum.Expired;
                        // add log about expired order.
                        InsertLogModel model = new InsertLogModel
                        {
                            ActivityLogType = (int)LogTypeEnum.ExpiredOrder,
                            Browser = HttpContext.Current.Request.UserAgent,
                            IP = HttpContext.Current.Request.UserHostAddress,
                            Message = "Expired Order : " + orderModel.OrderGUID,
                            ProgramGuid = Guid.Empty,
                            PageGuid = Guid.Empty,
                            PageSequenceGuid = Guid.Empty,
                            SessionGuid = Guid.Empty,
                            UserGuid = ContextService.CurrentAccount.UserGuid,
                            From = string.Empty
                        };
                        Resolve<IActivityLogService>().Insert(model);
                        Resolve<IOrderService>().UpdateOrder(orderModel);
                    }
                }

                if (orderModel!=null)
                {
                    switch (orderModel.OrderStatusID)
                    {
                        case (int)OrderStatusEnum.Active:
                            break;
                        case (int)OrderStatusEnum.Cancel:
                            Button cancelledButton = e.Item.FindControl("btnCancelledOrder") as Button;
                            cancelButton.Visible = false;
                            cancelledButton.Visible = true;
                            cancelledButton.Enabled = false;
                            break;
                        case (int)OrderStatusEnum.Expired:
                            Button expiredButton = e.Item.FindControl("btnExpiredOrder") as Button;
                            cancelButton.Visible = false;
                            expiredButton.Visible = true;
                            expiredButton.Enabled = false;
                            break;
                    }
                }
            }
        }

        protected void rpOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Button cancelButton = e.Item.FindControl("btnCancelOrder") as Button;
        }
    }
}