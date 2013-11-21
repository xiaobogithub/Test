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
    public partial class EditProgramRoom : PageBase<ProgramRoomModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[Constants.QUERYSTR_PROGRAM_ROOM_GUID]))
                {
                    try
                    {
                        FormatPage();
                    }
                    catch (Exception ex)
                    {
                        LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        throw ex;
                    }
                    //((HyperLink)Master.FindControl("menuLoginView").FindControl("hpBackLink")).NavigateUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ViewState["ProgramGuid"].ToString());
                    ////cancelHyperLink.NavigateUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ViewState["ProgramGuid"].ToString());
                }
                else
                {
                    Response.Redirect("InvalidUrl.aspx");
                }
            }
        }

        private void FormatPage()
        {
            Model = Resolve<IProgramRoomService>().GetRoom(new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_ROOM_GUID]));
            ViewState["ProgramGuid"] = Model.Program.Guid;
            ltProgram.Text = Model.Program.ProgramName;
            txtDescription.Text = Model.Description;
            txtName.Text = Model.Name;
            roomColorText.Text = Model.TopBarColor;
            shadowTextBox.Text = Model.CoverShadowColor;
            coverShadowVisibleCheckBox.Checked = Model.IsCoverShadowVisible;
            if (!string.IsNullOrEmpty(Model.ButtonDisable))
            {
                string[] disableColor = Model.ButtonDisable.Split(',');
                disableFromTextBox.Text = disableColor[0].Substring(2);
                disableToTextBox.Text = disableColor[1].Substring(2);
            }
            if (!string.IsNullOrEmpty(Model.ButtonDown))
            {
                string[] downColor = Model.ButtonDown.Split(',');
                DownFromTextBox.Text = downColor[0].Substring(2);
                DownToTextBox.Text = downColor[1].Substring(2);
            }
            if (!string.IsNullOrEmpty(Model.ButtonNormal))
            {
                string[] normalColor = Model.ButtonNormal.Split(',');
                normalFromTextBox.Text = normalColor[0].Substring(2);
                normalToTextBox.Text = normalColor[1].Substring(2);
            }
            if (!string.IsNullOrEmpty(Model.ButtonOver))
            {
                string[] overColor = Model.ButtonDown.Split(',');
                overFromTextBox.Text = overColor[0].Substring(2);
                overToTextBox.Text = overColor[1].Substring(2);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Model = new ProgramRoomModel
                {
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    TopBarColor = roomColorText.Text,
                    Program = new ProgramModel { Guid = new Guid(ViewState["ProgramGuid"].ToString()), },
                    ProgramRoomGuid = new Guid(Request.QueryString[Constants.QUERYSTR_PROGRAM_ROOM_GUID].ToString()),
                    ButtonDisable = GetFromToColor(disableFromTextBox.Text, disableToTextBox.Text),
                    ButtonDown = GetFromToColor(DownFromTextBox.Text, DownToTextBox.Text),
                    ButtonOver = GetFromToColor(overFromTextBox.Text, overToTextBox.Text),
                    ButtonNormal = GetFromToColor(normalFromTextBox.Text, normalToTextBox.Text),
                    CoverShadowColor = string.IsNullOrEmpty(shadowTextBox.Text)? string.Empty: "0x" + shadowTextBox.Text,
                    IsCoverShadowVisible = coverShadowVisibleCheckBox.Checked
                };
                Resolve<IProgramRoomService>().Update(Model);
            }
            catch (Exception ex)
            {
                LogUtility.LogUtilityIntance.LogException(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw ex;
            }
            Response.Redirect(string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ViewState["ProgramGuid"]));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string goBackUrl = string.Format("~/ListProgramRoom.aspx?{0}={1}", Constants.QUERYSTR_PROGRAM_GUID, ViewState["ProgramGuid"].ToString());
            Response.Redirect(goBackUrl);
        }

        private string GetFromToColor(string fromColor, string toColor)
        {
            string formatColor = string.Empty;
            if (!string.IsNullOrEmpty(fromColor) &&
                !string.IsNullOrEmpty(toColor))
            {
                formatColor = string.Format("0x{0},0x{1}", fromColor, toColor);
            }
            return formatColor;
        }
    }
}
