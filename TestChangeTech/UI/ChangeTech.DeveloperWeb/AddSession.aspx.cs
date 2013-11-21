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

namespace ChangeTech.DeveloperWeb
{
    public partial class AddSession : PageBase<SimpleProgramModel>
    {
        private string ProgramGUID
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }
        }

        private string ProgramPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_PROGRAM_PAGE];
            }
        }

        private string SessionPage
        {
            get
            {
                return Request.QueryString[Constants.QUERYSTR_SESSION_PAGE];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    try
                    {
                        Model = Resolve<IProgramService>().GetSimpleProgram(new Guid(ProgramGUID));
                        ////hpCancel.NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage);
                        ////hlCancelAddMore.NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage);
                        ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage);
                        FormatDDL();
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

        private void FormatDDL()
        {
            //int days = Resolve<ISessionService>().GetNumberOfSession(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            int days = Resolve<ISessionService>().GetNumberOfNormalSessions(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            this.ddlDay.Items.Clear();
            for (int i = -5; i <= days; i++)
            {
                if (i < 0)
                {
                    Guid sessionGuid = Resolve<ISessionService>().GetSessionGuidByProgarmAndDay(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), i);

                    if (sessionGuid == Guid.Empty)
                    {
                        ddlDay.Items.Add(i.ToString());
                    }
                }
                else
                {
                    ddlDay.Items.Add(i.ToString());
                }
                
                if (i < days & i >= 0)
                {
                    startDayDropDownList.Items.Add(i.ToString());
                }
            }           
            ddlDay.SelectedValue = days.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                int sessionDay = Convert.ToInt32(ddlDay.SelectedValue);
                string sessionName = txtSessionName.Text;
                string sessionDes = txtSessionDescription.Text;

                SessionModel session = new SessionModel();
                session.Day = sessionDay;
                session.Description = sessionDes;
                session.Name = sessionName;
                try
                {
                    Resolve<ISessionService>().AddSessionForProgram(session, new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
                }
                catch (Exception ex)
                {
                    LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                    throw ex;
                }
                Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage));
            }
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (ValidateAddMoreDay())
                {
                    int startDay = Convert.ToInt32(startDayDropDownList.SelectedValue);
                    int days = Convert.ToInt32(daysTextBox.Text.Trim());
                    try
                    {
                        Resolve<ISessionService>().AddMoreThanOneSessionForProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), days, startDay);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    Response.Redirect(string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage));
                }
            }
        }

        protected void btnCancelOneSession_Click(object sender, EventArgs e)
        {
            string cancelBackUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage);
            Response.Redirect(cancelBackUrl);
        }

        protected void btnCancelMoreSession_Click(object sender, EventArgs e)
        {
            string cancelBackUrl = string.Format("EditProgram.aspx?{0}={1}&{2}={3}&Page={4}", Constants.QUERYSTR_PROGRAM_GUID, ProgramGUID, Constants.QUERYSTR_PROGRAM_PAGE, ProgramPage, SessionPage);
            Response.Redirect(cancelBackUrl);
        }

        
        private bool ValidateAddMoreDay()
        {
            int sessions = 0;
            return Int32.TryParse(daysTextBox.Text, out sessions);
        }
    }
}
