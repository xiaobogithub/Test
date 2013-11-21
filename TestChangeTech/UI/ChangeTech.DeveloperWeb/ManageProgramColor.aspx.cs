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
    public partial class ManageProgramColor : PageBase<EditProgramModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindColor();
                ((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]);
            }
        }

        private void BindColor()
        {
            // main color
            ProgramColorModel colModel = Resolve<IProgramService>().GetProgramColor(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            lineColorText.Text = colModel.GeneralColor;
            shadowTextBox.Text = colModel.CoverShadowColor;
            coverShadowVisibleCheckBox.Checked = colModel.IsShadowVisible;
            // set primary button color
            PrimaryButtonColorModel colorModel = Resolve<IProgramService>().GetPrimaryButtonColor(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
            normalFromTextBox.Text = colorModel.normalFrom;
            normalToTextBox.Text = colorModel.normalTo;
            overFromTextBox.Text = colorModel.overFrom;
            overToTextBox.Text = colorModel.overTo;
            DownFromTextBox.Text = colorModel.downFrom;
            DownToTextBox.Text = colorModel.downTo;
            disableFromTextBox.Text = colorModel.disableFrom;
            disableToTextBox.Text = colorModel.disableTo;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Resolve<IProgramService>().UpdateProgramMainColor(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), lineColorText.Text, shadowTextBox.Text, coverShadowVisibleCheckBox.Checked);
            Resolve<IProgramService>().UpdateProgarmPrimaryButtonAndTopLineColor(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]), normalFromTextBox.Text, normalToTextBox.Text,
                   overFromTextBox.Text, overToTextBox.Text, DownFromTextBox.Text, DownToTextBox.Text, disableFromTextBox.Text, disableToTextBox.Text, lineColorText.Text);
            Response.Redirect(string.Format("EditProgram.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, Request.QueryString[Constants.QUERYSTR_PROGRAM_GUID]));
        }
    }
}
