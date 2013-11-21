using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ethos.Utility;
using Ethos.DependencyInjection;
using ChangeTech.Models;
using ChangeTech.Contracts;

namespace ChangeTech.DeveloperWeb
{
    public partial class ManageRelapsePageSequence : PageBase<ProgramModel>
    {
        private const string CTPPPRESENTERMODE = "CTPPPresenterImage";
        const string PREVIEW = "Preview";
        const string USE_FOR_CTPP = "Use For CTPP";
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private Guid CTPPGUID
        {
            get
            {
                Guid _ctppGuid = Guid.Empty;
                Guid _programGuid = Guid.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_CTPP_GUID]))
                {
                    Guid.TryParse(Request.QueryString[Constants.QUERYSTR_CTPP_GUID].ToString(), out _ctppGuid);
                }
                else if (Guid.TryParse(ProgramGUID, out _programGuid))
                {
                    CTPPModel ctppModel = Resolve<ICTPPService>().GetCTPP(_programGuid);
                    if (ctppModel != null) _ctppGuid = ctppModel.CTPPGUID;
                }

                return _ctppGuid;
            }
        }

        private OriginalPageEnum OriginalPage
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]))
                    return OriginalPageEnum.CTPP;
                else
                    return OriginalPageEnum.Program;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                try
                {
                    if (!IsPostBack)
                    {
                        BindRelapse();

                        Model = Resolve<IProgramService>().GetProgramByGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));

                        //This page may come from ManageCTPP page via help button for relapse or report button for relapse
                        if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]))
                        {
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
                                Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                                Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
                                Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]);
                        }
                        else
                        {
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}",
                                Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                                Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                                Request.QueryString[Constants.QUERYSTR_SESSION_PAGE]);
                        }

                        ((Literal)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("ltlEditSession")).Text = "Subroutine";
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).Text = Model.ProgramName;
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpEditProgramLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}",
                            Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                            Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                            Request.QueryString[Constants.QUERYSTR_SESSION_PAGE]);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("SiteMapPath").FindControl("hpProgramLink")).NavigateUrl = string.Format("ListProgram.aspx?Page={0}",
                            Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID);
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        #region
        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}",
                Constants.QUERYSTR_EDITMODE, Constants.SELF,
                Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
        }

        protected void delRelapseButton_Click(object sender, EventArgs e)
        {
            string relapseguid = ((Button)sender).CommandArgument;
            Resolve<IRelapseService>().DeleteRelapse(new Guid(relapseguid));
            Response.Redirect(Request.Url.ToString());
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            string pagesequenceguid = ((Button)sender).CommandArgument;
            Response.Redirect(string.Format("EditPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}",
                Constants.QUERYSTR_EDITMODE, Constants.SELF,
                Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pagesequenceguid,
                Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE],
                Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE],
                Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
        }

        protected void relapseRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.DataItem != null)
            //{
            //    Button preview = (Button)e.Item.FindControl("preRelapseButton");
            //    string url = string.Format("ChangeTech.html?Mode=Preview&Object=Relapse&{0}={1}&{2}={3}&{4}={5}",
            //        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
            //        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, preview.CommandArgument,
            //        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
            //    preview.Attributes.Add("OnClick", "return openPage('" + url + "');");


            //    Button useRelapseForCTPP = (Button)e.Item.FindControl("useRelapseForCTPP");
            //    if (useRelapseForCTPP != null)
            //    {
            //        switch (OriginalPage)
            //        {
            //            case OriginalPageEnum.CTPP:
            //                useRelapseForCTPP.Visible = true;
            //                break;
            //            case OriginalPageEnum.Program:
            //                useRelapseForCTPP.Visible = false;
            //                break;
            //        }
            //    }
            //}
        }

        protected void moreOptionsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList moreOptionsDDL = (DropDownList)sender;
            string selectOption = moreOptionsDDL.SelectedValue;
            string pageSequenceGuidStr = moreOptionsDDL.DataValueField;
            string relapseGuidStr = moreOptionsDDL.DataTextField;
            switch (selectOption)
            {
                case PREVIEW://preview
                    string url = string.Format("ChangeTech.html?Mode=Preview&Object=Relapse&{0}={1}&{2}={3}&{4}={5}",
                        Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                        Constants.QUERYSTR_PAGE_SEQUENCE_GUID, pageSequenceGuidStr,
                        Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid);
                    Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "openUrl", "<script>window.open('" + url + "');</script>");
                    break;
                case USE_FOR_CTPP://make copy
                    switch (OriginalPage)
                    {
                        case OriginalPageEnum.CTPP:
                            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]))
                            {
                                int type = int.Parse(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);
                                CTPPRelapseEnum relapseType = (CTPPRelapseEnum)type;
                                Guid relapseGuid = Guid.Empty;
                                Guid.TryParse(relapseGuidStr, out relapseGuid);
                                if (relapseGuid != Guid.Empty && CTPPGUID != Guid.Empty)
                                {
                                    if (Resolve<ICTPPService>().BindCTPPRelapse(CTPPGUID, relapseGuid, relapseType))
                                    {
                                        //Use relapse for CTPP success. Turn to manage ctpp page.
                                        Response.Redirect(string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
                                            Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
                                            Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
                                            Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not Use the relapse for CTPP. Please try again or contact the develper for reason.');", true);
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not find the CTPPGUID or RelapseGUID. Please leave this page and come back again and then retry.');", true);
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not find the QUERYSTR_CTPP_TYPE. Please leave this page and come back again and then retry.');", true);
                            }
                            break;
                        case OriginalPageEnum.Program:
                            break;
                    }
                    break;
            }
        }

        //protected void useRelapseForCTPP_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]))
        //    {
        //        int type = int.Parse(Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);
        //        CTPPRelapseEnum relapseType = (CTPPRelapseEnum)type;

        //        Guid relapseGuid = Guid.Empty;
        //        Guid.TryParse(((Button)sender).CommandArgument, out relapseGuid);
        //        if (relapseGuid != Guid.Empty && CTPPGUID != Guid.Empty)
        //        {
        //            if (Resolve<ICTPPService>().BindCTPPRelapse(CTPPGUID, relapseGuid, relapseType))
        //            {
        //                //Use relapse for CTPP success. Turn to manage ctpp page.
        //                Response.Redirect(string.Format("ManageCTPP.aspx?{0}={1}&{2}={3}&{4}={5}",
        //                    Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID,
        //                    Constants.QUERYSTR_PRESENTERIMAGE_MODE, CTPPPRESENTERMODE,
        //                    Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE]));
        //            }
        //            else
        //            {
        //                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not Use the relapse for CTPP. Please try again or contact the develper for reason.');", true);
        //            }
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not find the CTPPGUID or RelapseGUID. Please leave this page and come back again and then retry.');", true);
        //        }
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Can not find the QUERYSTR_CTPP_TYPE. Please leave this page and come back again and then retry.');", true);
        //    }
        //}
        #endregion

        #region private methods
        private void BindRelapse()
        {
            relapseRepeater.DataSource = Resolve<IRelapseService>().GetRelapseModelList(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            relapseRepeater.DataBind();
        }

        private Guid GetSessionGUID()
        {
            Guid sessionguid = Guid.Empty;

            if (ViewState["Session"] != null)
            {
                sessionguid = new Guid(ViewState["Session"].ToString());
            }
            else
            {
                sessionguid = Resolve<ISessionService>().GetFirstSessionGUID(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                ViewState["Session"] = sessionguid;
            }
            return sessionguid;
        }
        #endregion
    }
}
