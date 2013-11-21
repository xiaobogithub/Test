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
    public partial class LogReport : PageBase<UserModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProgram();
                BindUser();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = "~/Report.aspx";
            }
        }

        protected void exportButton_Click(object sender, EventArgs e)
        {
            string emaillist = GetEmailList();
            string fieds = GetField();
            string days = GetDays();
            string valideString = ValidatePage(emaillist, fieds);
            if (string.IsNullOrEmpty(valideString))
            {
                GridView tempGradview = new GridView();
                tempGradview.DataSource = Resolve<IReportService>().GetActivityLogReport(emaillist, fieds, days, programDropDownList.SelectedValue);
                tempGradview.DataBind();
                Ethos.Utility.ExportExcel.ExportExcelFromGridView(tempGradview, "ActivityLog");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+valideString+"');", true);
            }
        }       

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
        }

        private string ValidatePage(string emaillist, string fieds)
        {
            string resultString = string.Empty;
            if (string.IsNullOrEmpty(emaillist))
            {
                resultString += "Please check one user at least.";
            }
            if (string.IsNullOrEmpty(fieds))
            {
                resultString += "Please check one field at least.";
            }
            if (Convert.ToInt32(fromDropDownList.Text) > Convert.ToInt32(toDropDownList.Text))
            {
                resultString += "From day should less than to day.";
            }
            return resultString;
        }

        private string GetDays()
        {
            string result = string.Empty;
            int from = Convert.ToInt32(fromDropDownList.Text);
            int to = Convert.ToInt32(toDropDownList.Text);
            for (int i = from; i < to; i++)
            {
                result += i.ToString() + ",";
            }
            result += to.ToString();

            return result;
        }

        private string GetEmailList()
        {
            string emailStr = string.Empty;
            foreach (RepeaterItem item in this.userRepeater.Items)
            {
                CheckBox cb = item.FindControl("userCheckBox") as CheckBox;
                if (cb.Checked)
                {
                    if (!string.IsNullOrEmpty(emailStr))
                    {
                        emailStr += ",";
                    }
                    emailStr += "'";
                    emailStr += ((Label)item.FindControl("emailLabel")).Text;
                    emailStr += "'";
                }
            }
            return emailStr;
        }

        private string GetField()
        {
            string fieldStr = string.Empty;
            if (emailCheckBox.Checked)
            {
                fieldStr += ",[Email]";
            }
            if (userStatusCheckBox.Checked)
            {
                fieldStr += ",[Status]";
            }
            if (userTypeCheckBox.Checked)
            {
                fieldStr += ",[UserType]";
            }
            if (loginLogCheckBox.Checked)
            {
                fieldStr += ",[LoginTime]";
            }
            if (startDayLogCheckBox.Checked)
            {
                fieldStr += ",[StartTime]";
            }
            if (endDayLogCheckBox.Checked)
            {
                fieldStr += ",[EndTime]";
            }

            return fieldStr;
        }

        private void BindUser()
        {
            int usersCount = Resolve<IUserService>().GetUsrsCout(new Guid(programDropDownList.SelectedValue), UserTypeEnum.All, string.Empty, string.Empty);
            InitialPageInfo((int)Math.Ceiling(Convert.ToDouble(usersCount) / PageSize), "UserName", "asc");
            string url = Request.Url.ToStringWithoutPort();
            PagingString = Ethos.Utility.PagingSortingService.InitialPagingString(Request.Url.ToStringWithoutPort(), PageNumber, CurrentPageNumber, Resources.Share.Previous, Resources.Share.Next);
            userRepeater.DataSource = Resolve<IUserService>().GetUsersInProgram(new Guid(programDropDownList.SelectedValue), UserTypeEnum.All, "", string.Empty, CurrentPageNumber, PageSize);
            userRepeater.DataBind();
        }

        private void BindProgram()
        {
            programDropDownList.DataSource = Resolve<IProgramService>().GetAllSimpleProgramModel();
            programDropDownList.DataTextField = "ProgramName";
            programDropDownList.DataValueField = "ProgramGuid";
            programDropDownList.DataBind();
            if (Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID] != null)
            {
                programDropDownList.SelectedValue = Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID];
            }

            int sessionCount = Resolve<ISessionService>().GetLastSessionDay(new Guid(programDropDownList.SelectedValue));
            for (int i = 0; i <= sessionCount; i++)
            {
                fromDropDownList.Items.Add(i.ToString());
                toDropDownList.Items.Add(i.ToString());
            }
        }

        protected void programDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("LogReport.aspx?{0}={1}",Constants.QUERYSTR_PROGRAM_GUID,programDropDownList.SelectedValue));
        }
    }
}
