using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ListSMS : PageBase<ShortMessageQueueModel>
    {
        private Guid ProgramGUID { get { return new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitialPage();
                BindSMS();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
            }
        }

        private void InitialPage()
        {
            if(!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_USER_EMAIL]))
            {
                emailTextBox.Text = Request.QueryString[Constants.QUERYSTR_USER_EMAIL];
            }
        }

        private void BindSMS()
        {
            int numberSM = Resolve<IShortMessageService>().GetSMQListCount(ProgramGUID, emailTextBox.Text);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(numberSM) / PageSize), "ID", "asc");
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.PathAndQuery, PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);

            smRepeater.DataSource = Resolve<IShortMessageService>().GetSMQList(ProgramGUID, CurrentPageNumber, PageSize, emailTextBox.Text);
            smRepeater.DataBind();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ListSMS.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_USER_EMAIL, emailTextBox.Text, Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID));
        }
    }
}
