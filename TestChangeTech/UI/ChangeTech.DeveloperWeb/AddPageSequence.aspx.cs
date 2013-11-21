using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChangeTech.Models;
using ChangeTech.Contracts;
using Ethos.DependencyInjection;
using Ethos.Utility;

namespace ChangeTech.DeveloperWeb
{
    public partial class AddPageSequence : PageBase<EditSessionModel>
    {
        public const string CHANGETECHPAGEFLASH = "ChangeTech.html";
        public const string PAGEREVIEWPRESENTERMODE = "PagePresenterImage";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]))
            //{
            try
            {
                //Model = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]));
                if (!IsPostBack)
                {
                    BindDropDownList();
                    if (IsEditPagesequenceOnly())
                    {
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}",
                            Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID],
                            Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PREVIOUSPAGE]))
                        {
                            switch (Request.QueryString[Constants.QUERYSTR_PREVIOUSPAGE])
                            {
                                case "EditSession":
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]);
                                    break;
                                case "PageReview":
                                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PRESENTERIMAGE_MODE, "PAGEREVIEWPRESENTERMODE");
                                    break;
                            }
                        }
                        else
                        {
                            ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            //}
            //else
            //{
            //    Response.Redirect("InvalidUrl.aspx");
            //}
        }

        #region

        protected void predictorDropDownListChanged(object sender, EventArgs e)
        {
            if (IsEditPagesequenceOnly())
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&type={12}&{13}={14}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_EDITMODE, Constants.SELF, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
            else
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&type={8}&{9}={10}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
        }

        protected void interventCategoryDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsEditPagesequenceOnly())
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&type={14}&{15}={16}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_EDITMODE, Constants.SELF, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
            else
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&type={10}&{11}={12}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
        }

        protected void intervnetDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsEditPagesequenceOnly())
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&{14}={15}&type={16}&{17}={18}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_EDITMODE, Constants.SELF, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENT_GUID, interventDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
            else
            {
                Response.Redirect(string.Format("AddPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&type={12}&{13}={14}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENT_GUID, interventDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
            }
        }

        protected void ddlProgramRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void newPageSequenceButton_Click(object sender, EventArgs e)
        {
            if (interventDropdownList.SelectedItem != null)
            {
                if (IsEditPagesequenceOnly())
                {
                    Response.Redirect(string.Format("AddNewPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}&{14}={15}&type={16}&{17}={18}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_EDITMODE, Constants.SELF, Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENT_GUID, interventDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
                }
                else
                {
                    Response.Redirect(string.Format("AddNewPageSequence.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&type={12}&{13}={14}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID], Constants.QUERYSTR_PREDICTOR_GUID, predictorDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENTCATEGORY_GUID, interventCategoryDropdownList.SelectedValue, Constants.QUERYSTR_INTERVENT_GUID, interventDropdownList.SelectedValue, Request.QueryString["type"], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "tip", "alert('Please select a specific intervent');", true);
            }
        }

        protected void UseButton_Click(object sender, EventArgs e)
        {
            try
            {
                Guid RoomGuid = Guid.Empty;
                if (ddlProgramRoom.SelectedValue != "")
                {
                    RoomGuid = new Guid(ddlProgramRoom.SelectedValue);
                }
                string pageSeqID = ((Button)sender).CommandArgument;
                if (IsEditPagesequenceOnly())
                {
                    Resolve<IRelapseService>().AddRelapseForProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), new Guid(pageSeqID), RoomGuid);
                    Response.Redirect(string.Format("ManageRelapsePageSequence.aspx?{0}={1}&{2}={3}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID], Constants.QUERYSTR_CTPP_TYPE, Request.QueryString[Constants.QUERYSTR_CTPP_TYPE]));
                }
                else
                {
                    string SessionID = Request.QueryString[Constants.QUERYSTR_SESSION_GUID];

                    DropDownList ddl = pageSequenceRepeater.Controls[0].Controls[0].FindControl("ddlOrder") as DropDownList;


                    Resolve<IPageSequenceService>().UseExistedPageSequence(new Guid(SessionID), new Guid(pageSeqID), RoomGuid, int.Parse(ddl.SelectedValue));

                    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    {
                        switch (Request.QueryString["type"])
                        {
                            case "EditSession":
                                Response.Redirect(string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, SessionID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]));
                                break;
                            case "PageReview":
                                Response.Redirect(string.Format("PageReview.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, SessionID, Constants.QUERYSTR_PRESENTERIMAGE_MODE, PAGEREVIEWPRESENTERMODE));
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect(string.Format("EditSession.aspx?{0}={1}&{2}={3}&{4}={5}&Page={6}", Constants.QUERYSTR_PROGRAM_PAGE, Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE], Constants.QUERYSTR_SESSION_PAGE, Request.QueryString[Constants.QUERYSTR_SESSION_PAGE], Constants.QUERYSTR_SESSION_GUID, SessionID, Request.QueryString[Constants.QUERYSTR_PAGE_SEQUENCE_PAGE]),false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        //protected void EditButton_Click(object sender, EventArgs e)
        //{
        //    string PageSequenceID = ((Button)sender).CommandArgument;
        //    Response.Redirect(string.Format("EditPageSequence.aspx?{4}={5}&{0}={1}&{2}={3}", Constants.QUERYSTR_PAGE_SEQUENCE_GUID,PageSequenceID,"Option","Edit",Constants.QUERYSTR_SESSION_GUID,Request.QueryString[Constants.QUERYSTR_SESSION_GUID]));
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(string.Format("EditSession.aspx?{0}={1}", Constants.QUERYSTR_SESSION_GUID, Request.QueryString[Constants.QUERYSTR_SESSION_GUID]));
        //}

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string PageSequenceID = (sender as Button).CommandArgument;
                if (Resolve<IPageSequenceService>().DeletePageSequence(new Guid(PageSequenceID)))
                {
                    Response.Redirect(Request.Url.ToStringWithoutPort());
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "del", "alert('The page sequence is used by some programe!');", true);
                }
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
        }

        protected void pageSequenceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                try
                {
                    // for popup page
                    Button preview = (Button)e.Item.FindControl("PreviewButton");
                    Guid sessionGuid = Guid.Empty;
                    if (IsEditPagesequenceOnly())
                    {
                        List<SimpleSessionModel> sessions = Resolve<ISessionService>().GetSimpleSessionsByProgramGuid(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                        foreach (SimpleSessionModel session in sessions)
                        {
                            sessionGuid = session.ID;
                            break;
                        }
                    }
                    else
                    {
                        sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                    }

                    string url = string.Format("{6}?Mode=Preview&Object=TempPageSequence&{0}={1}&{2}={3}&{4}={5}"
                           , Constants.QUERYSTR_SESSION_GUID, sessionGuid
                           , Constants.QUERYSTR_PAGE_SEQUENCE_GUID, preview.CommandArgument
                           , Constants.QUERYSTR_USER_GUID, ContextService.CurrentAccount.UserGuid, CHANGETECHPAGEFLASH);
                    preview.Attributes.Add("OnClick", "return openPage('" + url + "');");
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
            }
        }
        #endregion

        #region Private Methods

        private bool IsEditPagesequenceOnly()
        {
            bool flug = false;
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_EDITMODE]) && Request.QueryString[Constants.QUERYSTR_EDITMODE].Equals(Constants.SELF))
            {
                flug = true;
            }

            return flug;
        }

        private void BindProgramRoom()
        {
            Guid programguid = Guid.Empty;
            if (IsEditPagesequenceOnly())
            {
                programguid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
            else
            {
                programguid = Resolve<ISessionService>().GetSessionBySessonGuid(new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID])).ProgramGuid;
            }
            ddlProgramRoom.DataSource =
                Resolve<IProgramRoomService>().GetRoomByProgram(programguid);
            ddlProgramRoom.DataBind();
            ddlProgramRoom.Items.Insert(0, new ListItem("", ""));
        }

        private void BindDropDownList()
        {
            BindProgramRoom();

            BindPredictorList();
            if (!string.IsNullOrEmpty(predictorDropdownList.SelectedValue))
            {
                BindInterventCategoryList(predictorDropdownList.SelectedValue);
            }
            if (!string.IsNullOrEmpty(interventCategoryDropdownList.SelectedValue))
            {
                BindInterventList(interventCategoryDropdownList.SelectedValue);
            }
        }

        private void BindPredictorList()
        {
            predictorDropdownList.DataSource = Resolve<IPredictorService>().GetAllPredictors();
            predictorDropdownList.DataValueField = "PredictorID";
            predictorDropdownList.DataTextField = "Name";
            predictorDropdownList.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID]))
            {
                predictorDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PREDICTOR_GUID];
            }
        }

        private void BindInterventCategoryList(string guid)
        {
            interventCategoryDropdownList.DataSource = Resolve<IInterventCategoryService>().GetInterventCategoryModelsByPredictorGuid(new Guid(guid));
            interventCategoryDropdownList.DataValueField = "CategoryGUID";
            interventCategoryDropdownList.DataTextField = "Name";
            interventCategoryDropdownList.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID]))
            {
                interventCategoryDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENTCATEGORY_GUID];
            }
        }

        private void BindInterventList(string categoryGuid)
        {
            interventDropdownList.DataSource = Resolve<IInterventService>().GetInterventsOfCategory(new Guid(categoryGuid));
            interventDropdownList.DataValueField = "InterventGUID";
            interventDropdownList.DataTextField = "InterventName";
            interventDropdownList.DataBind();
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID]))
            {
                interventDropdownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_INTERVENT_GUID];
            }
            BindPageSequenceRepeater();
        }

        private void BindPageSequenceRepeater()
        {
            if (!string.IsNullOrEmpty(interventDropdownList.SelectedValue))
            {
                List<PageSequenceModel> pageSequenceList = Resolve<IPageSequenceService>()
                    .GetPageSequenceByInterventGuid(new Guid(interventDropdownList.SelectedValue));

                // for paging and soring
                InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(pageSequenceList.Count) / PageSize), "Name", "asc");
                string url = Request.Url.ToStringWithoutPort();
                string[] header = { (string)Resources.Share.Name, (string)Resources.Share.CountOfPages, (string)Resources.Share.UsedInPrograms };
                string[] sortExpression = { "Name", "CountOfPages", "UsedInProgram" };
                PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
                HeaderString = Ethos.Utility.PagingSortingService.InitialSortingString(Request.Url.ToStringWithoutPort(), FittingSortExpression(sortExpression), header);

                pageSequenceRepeater.DataSource = PagingSortingService.GetCurrentPage<PageSequenceModel>(pageSequenceList, CurrentPageNumber, PageNumber, PageSize, SortExpression + " " + SortOrder);
                pageSequenceRepeater.DataBind();

                BindOrderDll();
            }
            else
            {
                pageSequenceRepeater.DataSource = null;
                pageSequenceRepeater.DataBind();
            }
        }

        private void BindOrderDll()
        {
            DropDownList ddl = pageSequenceRepeater.Controls[0].Controls[0].FindControl("ddlOrder") as DropDownList;

            if (IsEditPagesequenceOnly())
            {
                Literal literal = pageSequenceRepeater.Controls[0].Controls[0].FindControl("Literal4") as Literal;
                literal.Visible = false;
                ddl.Visible = false;
            }
            else
            {
                if (ddl != null)
                {
                    Guid sessionGuid = new Guid(Request.QueryString[Constants.QUERYSTR_SESSION_GUID]);
                    int pageSequenceNumber = Resolve<IPageSequenceService>().GetPageSequenceBySessionGuid(sessionGuid).Count;
                    for (int i = 1; i <= pageSequenceNumber + 1; i++)
                    {
                        ddl.Items.Add(i.ToString());
                    }
                    ddl.SelectedIndex = ddl.Items.Count - 1;
                }
            }
        }
        #endregion
    }
}
