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
    public partial class CheckProgram : PageBase<ModelBase>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]))
            {
                if (!IsPostBack)
                {
                    InitialPage();
                    ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
                }
            }
            else
            {
                Response.Redirect("InvalidUrl.aspx");
            }
        }

        private void InitialPage()
        {
            SimpleProgramModel program = Resolve<IProgramService>().GetSimpleProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            programLabel.Text = program.ProgramName;
        }

        protected void checkPagevariableButton_Click(object sender, EventArgs e)
        {
            tipLabel.Text = "The page variables bellow is not used in program";
            List<string> variableNameList = Resolve<IPageVariableService>().CheckPageVariableServiceConditionOfProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            resultListBox.DataSource = variableNameList;
            resultListBox.DataBind();
        }

        protected void checkExpressionButton_Click(object sender, EventArgs e)
        {
            tipLabel.Text = "Information of invalidate expressions ";
            List<string> expression = Resolve<IProgramService>().CheckExpressionForProgram(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            resultListBox.DataSource = expression;
            resultListBox.DataBind();
        }

        protected void checkSettingButton_Click(object sender, EventArgs e)
        {
            tipLabel.Text = "Information of program setting";
            resultListBox.DataSource = Resolve<IProgramService>().CheckProgramSetting(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            resultListBox.DataBind();
        }
    }
}
