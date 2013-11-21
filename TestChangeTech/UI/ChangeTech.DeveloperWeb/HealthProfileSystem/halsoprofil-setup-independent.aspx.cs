using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using Ethos.Utility;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb.HealthProfileSystem
{
    public partial class halsoprofil_setup_independent : PageBase<HPOrderModel>
    {
        public string HPOrderCode
        {
            get { return Request.QueryString[Constants.QUERYSTR_HPORDER_CODE]; }
        }
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
            HPOrderModel orderModel = Resolve<IHPOrderService>().GetHPOrderModelByCode(HPOrderCode);
            ValidateOrderLicenceResponseModel responseModel = Resolve<IHPOrderLicenceService>().ValidateHPOrderLicence(orderModel.HPOrderGUID, orderModel.ProgramGUID);
            switch (responseModel.Result)
            {
                case ResultEnum.Success:
                    offlineDiv.Visible = false;
                    onlineDiv.Visible = true;
                    lblCustomerName.Text = orderModel.CustomerName;
                    lblCustomerContactPersonName.Text = orderModel.ContactPersonName;
                    lblNumberOfUsers.Text = orderModel.NumberOfEmployees.ToString();
                    break;
                case ResultEnum.Error:
                    Response.Redirect(Constants.URL_HPSYSTEM_STARTPAGEOFFLINE,false);
                    break;
            }
        }

        protected void actionLink_Click(object sender, EventArgs e)
        {
            HPOrderModel orderModel = Resolve<IHPOrderService>().GetHPOrderModelByCode(HPOrderCode);
            if (orderModel != null)
            {
                string startProgramUrl = Resolve<IHPOrderService>().GetHPProgramLink(orderModel);
                Response.Redirect(startProgramUrl);
            }
        }
    }
}