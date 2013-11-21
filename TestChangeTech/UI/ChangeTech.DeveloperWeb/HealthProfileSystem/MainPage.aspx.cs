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

namespace ChangeTech.DeveloperWeb.HealthProfileSystem
{
    public partial class MainPage : PageBase<HPOrderModel>
    {
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
               //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Constants.URL_HPSYSTEM_MAINPAGE;
            }
        }

        private void BindOrderList()
        {
            List<HPOrderModel> orderList = Resolve<IHPOrderService>().GetHPOrders();
            rpHPOrderList.DataSource = orderList;
            rpHPOrderList.DataBind();
        }

        protected void rpHPOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rpHPOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            {
                if (e.Item.DataItem != null)
                {
                    Button cancelButton = e.Item.FindControl("btnCancelOrder") as Button;
                    cancelButton.Attributes.Add("onClick ", "return   confirm( 'Do you really want to cancel the order?'); ");

                    HPOrderModel orderModel = Resolve<IHPOrderService>().GetHPOrderModelByHPOrderGuid(new Guid(cancelButton.CommandArgument));
                    if (DateTime.UtcNow.Date > orderModel.StartDate.Date && DateTime.UtcNow.Date > orderModel.StopDate.Date)
                    {
                        if (orderModel.OrderStatus != (int)OrderStatusEnum.Expired)
                        {
                            orderModel.OrderStatus = (int)OrderStatusEnum.Expired;
                            // add log about expired order.
                            InsertLogModel model = new InsertLogModel
                            {
                                ActivityLogType = (int)LogTypeEnum.ExpiredOrder,
                                Browser = HttpContext.Current.Request.UserAgent,
                                IP = HttpContext.Current.Request.UserHostAddress,
                                Message = "Expired HP Order : " + orderModel.HPOrderGUID,
                                ProgramGuid = orderModel.ProgramGUID,
                                PageGuid = Guid.Empty,
                                PageSequenceGuid = Guid.Empty,
                                SessionGuid = Guid.Empty,
                                UserGuid = ContextService.CurrentAccount.UserGuid,
                                From = string.Empty
                            };
                            Resolve<IActivityLogService>().Insert(model);
                            Resolve<IHPOrderService>().UpdateHPOrder(orderModel);
                        }
                    }

                    if (orderModel != null)
                    {
                        switch (orderModel.OrderStatus)
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
        }

        //Add New Order 
        protected void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_HPSYSTEM_ADDNEWORDER);
        }

        //Edit Order 
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            string hpOrderGuid = editButton.CommandArgument;
            Response.Redirect(string.Format("~/HealthProfileSystem/EditOrder.aspx?{0}={1}", Constants.QUERYSTR_HPORDERGUID, hpOrderGuid));
        }

        //Cancel Current order
        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            //When click cancel button,reminde user whether cancel message.
            //Cancel Button's Text changed to Cancelled
            Button cancelButton = sender as Button;
            if (cancelButton != null)
            {
                HPOrderModel orderModel = Resolve<IHPOrderService>().GetHPOrderModelByHPOrderGuid(new Guid(cancelButton.CommandArgument));
                if (orderModel != null)
                {
                    orderModel.OrderStatus = (int)OrderStatusEnum.Cancel;
                    // add log about cancel order.
                    InsertLogModel model = new InsertLogModel
                    {
                        ActivityLogType = (int)LogTypeEnum.CancelOrder,
                        Browser = HttpContext.Current.Request.UserAgent,
                        IP = HttpContext.Current.Request.UserHostAddress,
                        Message = "Cancle Health Profile Order : " + orderModel.HPOrderGUID,
                        ProgramGuid = orderModel.ProgramGUID,
                        PageGuid = Guid.Empty,
                        PageSequenceGuid = Guid.Empty,
                        SessionGuid = Guid.Empty,
                        UserGuid = ContextService.CurrentAccount.UserGuid,
                        From = string.Empty
                    };
                    Resolve<IActivityLogService>().Insert(model);
                    Resolve<IHPOrderService>().UpdateHPOrder(orderModel);
                }
            }
            BindOrderList();
        }

    }
}