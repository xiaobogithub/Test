using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using Ethos.DependencyInjection;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManagePaymentOrder : PageBase<ManageOrderModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindProgram();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Home.aspx";
            }
        }

        private void BindProgram()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetSimpleProgramsModel();
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                programDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }

            BindLanguage();
        }

        private void BindLanguage()
        {
            languageDropDownList.DataSource = Resolve<IProgramLanguageService>().GetLanguagesSupportByProgram(new Guid(programDropDownList.SelectedValue));
            languageDropDownList.DataTextField = "Name";
            languageDropDownList.DataValueField = "LanguageGUID";
            languageDropDownList.DataBind();

            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID]))
            {
                languageDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_LANGUAGE_GUID];
            }

            BindPaymentOrder();
        }

        private void BindPaymentOrder()
        {
            if(!string.IsNullOrEmpty(languageDropDownList.SelectedValue) && !string.IsNullOrEmpty(programDropDownList.SelectedValue))
            {
                ProgramModel programModel = Resolve<IProgramService>().GetProgramByProgramGUIDAndLanguageGUID(new Guid(programDropDownList.SelectedValue), new Guid(languageDropDownList.SelectedValue));
                int countNumber = Resolve<IPaymentService>().GetNumberOfPayOrder(programModel.Guid);
                InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(countNumber) / 15), "PayTime", "asc");
                PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
                orderRepeater.DataSource = Resolve<IPaymentService>().GetOrderList(programModel.Guid, CurrentPageNumber, 15);
                orderRepeater.DataBind();
            }
        }

        protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManagePaymentOrder.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }

        protected void languageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ManagePaymentOrder.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, programDropDownList.SelectedValue, Constants.QUERYSTR_LANGUAGE_GUID, languageDropDownList.SelectedValue));
        }
    }
}
