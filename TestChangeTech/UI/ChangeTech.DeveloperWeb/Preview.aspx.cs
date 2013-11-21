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

namespace ChangeTech.DeveloperWeb
{
    public partial class Preview : PageBase<EditProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindPageContentModel();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        protected void repeaterSession_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                // for popup new preview page
                Button preview = (Button)e.Item.FindControl("btnPreview");
                string url = string.Format("ChangeTech.html?Mode=Preview&Object=Session&{0}={1}&{2}={3}", 
                    Constants.QUERYSTR_SESSION_GUID, preview.CommandArgument,
                    Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                preview.Attributes.Add("OnClick", "return openPage('" + url + "');");
            }
        }

        private void BindPageContentModel()
        {
            Guid programGUID = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            int sessionNumber = Resolve<ISessionService>().GetNumberOfSession(programGUID);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(sessionNumber) / PageSize), "Day", "asc");
            Model = Resolve<IProgramService>().GetEditProgramModelByGuid(programGUID, CurrentPageNumber, PageSize);

            lblTitle.Text = Model.NameInDeveloper;
            PagingString = PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            repeaterSession.DataSource = Model.Sessions;
            repeaterSession.DataBind();
        }
    }
}
