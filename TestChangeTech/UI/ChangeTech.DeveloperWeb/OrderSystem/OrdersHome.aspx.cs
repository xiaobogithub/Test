using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using ChangeTech.Models;
using Ethos.DependencyInjection;

namespace ChangeTech.DeveloperWeb.OrderSystem
{
    public partial class OrdersHome : PageBase<OrderModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Response.Redirect(Constants.URL_ORDERSYSTEM_LIST,false);
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }
    }
}