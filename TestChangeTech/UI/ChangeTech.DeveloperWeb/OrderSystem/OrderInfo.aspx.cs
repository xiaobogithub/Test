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
    public partial class OrderInfo : PageBase<OrderContentModel>
    {
        private string OrderGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_ORDERGUID];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindOrderInfoList();
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = Constants.URL_ORDERSYSTEM_LIST;
            }
        }

        protected void BindOrderInfoList()
        {
            Guid orderGuid = new Guid(OrderGUID);
            if (orderGuid != Guid.Empty)
            {
                List<OrderContentModel> orderContents = Resolve<IOrderContentService>().GetOrderContentsByOrderGuid(orderGuid).OrderByDescending(oc => oc.LastRegisted).ToList();//.OrderBy(oc => oc.ProgramName).ToList();
                rpOrderInfo.DataSource = orderContents;
                rpOrderInfo.DataBind();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constants.URL_ORDERSYSTEM_LIST);
        }
    }
}