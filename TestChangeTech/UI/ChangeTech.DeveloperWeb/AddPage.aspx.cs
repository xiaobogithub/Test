using System;
using System.Web.UI.WebControls;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using System.Web.Configuration;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddPage : PageBase<ModelBase>
    {
        public string VersionNumberWithoutDot
        {
            get
            {
                string version = System.Configuration.ConfigurationManager.AppSettings["ProjectVersionWithoutDot"];
                return version;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID]))
            {
                if (!IsPostBack)
                {
                    //AddPageSilverlight.Source += "?DateTime=" + DateTime.UtcNow.Millisecond.ToString();
                    string azureAccountName = Resolve<ISystemSettingService>().GetSettingValue("AzureStorageAccountName");
                    AddPageSilverlight.InitParameters = "Mode=New,Azure=" + azureAccountName;
                    AddPageSilverlight.Source = string.Format("~/ClientBin/ChangeTech.Silverlight.DesignPage.xap?Version={0}", VersionNumberWithoutDot);

                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = GetBackURL();
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private bool IsEditPageSequenceSelf()
        {
            bool flug = false;
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_EDITMODE]) && Request.QueryString[Constants.QUERYSTR_EDITMODE].Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private string GetBackURL()
        {
            string url = string.Empty;
            if (IsEditPageSequenceSelf())
            {
                //url = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&Page={14}&{15}={16}", 
                //        Constants.QUERYSTR_EDITMODE, Constants.SELF,
                //        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                //        Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE],
                //        Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE],
                //        Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);
                url = Request.Url.AbsoluteUri.Replace("AddPage.aspx", "EditPageSequence.aspx");
            }
            else
            {
                //url = string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&Page={10}",
                //        Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                //        Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                //        Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE],
                //        Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE],
                //        Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]);
                url = Request.Url.AbsoluteUri.Replace("AddPage.aspx", "EditPageSequence.aspx");
            }

            return url;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("EditPageSequence.aspx?SessionContent={0}&{1}={2}&{3}={4}&{5}={6}&{7}={8}&{9}={10}&Page={11}&{12}={13}", 
                Request.QueryString["SessionContent"],
                Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID],
                Constants.QUERYSTR_PAGE_SEQUENCE_GUID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_GUID],
                Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE],
                Constants.QUERYSTR_PAGE_SEQUENCE_PAGE, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE],
                Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE],
                Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //AddPageModel pageModel = new AddPageModel();
            //pageModel.ID = Guid.NewGuid();
            //pageModel.Name = txtName.Text;
            //pageModel.BodyText = txtBodyText.Text;
            //pageModel.BodyTitle = txtBodyTitle.Text;
            //pageModel.ButtonPrimaryCaption = txtButtionPrimaryCaption.Text;
            //pageModel.ButtonSecondaryCaption = txtButtonSecondaryCaption.Text;
            //pageModel.ButtonPrimaryAction = Convert.ToInt32(ddlButtonPrimaryAction.SelectedValue);
            //pageModel.ButtonSecondaryAction = Convert.ToInt32(ddlButtonSecondaryAction.SelectedValue);
            //pageModel.SessionContentGuid = new Guid(Request.QueryString["SessionContent"]);

            //Resolve<IPageService>().InsertPage(pageModel);

            //Response.Redirect(string.Format("EditPageSequence.aspx?SessionContent={0}", Request.QueryString["SessionContent"]));
        }
    }
}
