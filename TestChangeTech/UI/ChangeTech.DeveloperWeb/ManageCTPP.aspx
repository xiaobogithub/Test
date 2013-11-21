<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageCTPP.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageCTPP" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript">
        function GetArgsFromHref(sHref, sArgName) {
            var args = sHref.split("?");
            var retval = "";
            if (args[0] == sHref) {
                return retval;
            }
            var str = args[1];
            args = str.split("&");
            for (var i = 0; i < args.length; i++) {
                str = args[i];
                var arg = str.split("=");
                if (arg.length <= 1) continue;
                if (arg[0] == sArgName) retval = arg[1];
            }
            return retval.replace('%7b', '{').replace('%7d', '}');
        }

        function setPresenter() {
            var programGuid = GetArgsFromHref(window.location.href, "ProgramGUID");
            var imageManagerURL = '~/ImageManager.aspx';
            imageManagerURL += '?' + 'ProgramGUID' + '=' + programGuid;
            imageManagerURL += '&' + 'PresenterMode' + '=' + 'CTPPPresenterImage';
            $('#imagemanager').attr('src', imageManagerURL);
            $('#presenterimage').dialog({
                width: 900,
                height: 700,
                modal: true,
                close: function (event, ui) {
                    if (window.location.hash.length > 1) {
                        document.location = window.location.hash.substring(1);
                    }
                }
            });
        }

        $(function () {
            var chkSpecificReportAndHelp = $("#<%=chbSpecificReportAndHelp.ClientID %>");
            var txtHelpButtonHeading = $("#<%=txtHelpButtonHeading.ClientID %>");
            var txtHelpButtonActual = $("#<%=txtHelpButtonActual.ClientID %>");
            var txtReportButtonHeading = $("#<%=txtReportButtonHeading.ClientID %>");
            var txtReportButtonActual = $("#<%=txtReportButtonActual.ClientID %>");
            var txtReportButtonComplete = $("#<%=txtReportButtonComplete.ClientID %>");
            var txtReportButtonUntaken = $("#<%=txtReportButtonUntaken.ClientID %>");
            chkSpecificReportAndHelp.change(function () {
                if (chkSpecificReportAndHelp.attr('checked') == 'checked') {
                    txtHelpButtonHeading.attr("disabled", false);
                    txtHelpButtonActual.attr("disabled", false);
                    txtReportButtonHeading.attr("disabled", false);
                    txtReportButtonActual.attr("disabled", false);
                    txtReportButtonComplete.attr("disabled", false);
                    txtReportButtonUntaken.attr("disabled", false);
                }
                else {
                    txtHelpButtonHeading.attr("disabled", true);
                    txtHelpButtonActual.attr("disabled", true);
                    txtReportButtonHeading.attr("disabled", true);
                    txtReportButtonActual.attr("disabled", true);
                    txtReportButtonComplete.attr("disabled", true);
                    txtReportButtonUntaken.attr("disabled", true);
                }
            });
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alertmessage hidden">There is an error somewhere</div>
    <div class="confirmationmessage hidden">The program is updated!</div>
    <div class="header">
        <h1>CTPP overview</h1>
        <div class="clear"></div>
    </div>
    <div class="box-main">
        <Ajax:ToolkitScriptManager runat="server" ID="toolkitScriptManage1">
        </Ajax:ToolkitScriptManager>
        <b>
            <asp:Label ID="programLabel" Visible="false" runat="server" Text=""></asp:Label>
        </b>

    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
  <%--  <tr>
      <td><p class="name">Program name</p></td>
      <td><asp:TextBox ID="txtProgramName" runat="server" CssClass="textfield-largetext"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>--%>
    <tr>
      <td><p class="name">Program URL</p></td>
      <td>
            <asp:TextBox ID="txtProgramURL" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:Label runat="server" ID="lblErrorProgramUrl" ForeColor="Red" Visible="false"></asp:Label>
      </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program subheading</p></td>
      <td><asp:TextBox ID="txtProgramSubheading" runat="server"  CssClass="textfield-largetext"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program description title</p></td>
      <td><asp:TextBox ID="txtDescriptionTitle" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program description</p></td>
      <td><asp:TextBox ID="txtDescription" runat="server" Rows="7" CssClass="textfield-default" TextMode="MultiLine"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program description title for Mobile</p></td>
      <td><asp:TextBox ID="txtDescriptionTitleForMobile" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program description for Mobile</p></td>
      <td><asp:TextBox ID="txtDescriptionForMobile" runat="server" Rows="7" CssClass="textfield-default" TextMode="MultiLine"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr style="display: none;">
      <td ><p class="name"> Twitter button</p></td>
      <td><asp:CheckBox ID="chbTwitter" runat="server" />
        <label for="chbInactive" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If checked, the twitter button enabled.</p></td>
    </tr>
    <tr style="display: none;">
      <td><p class="name"> Google+ button</p></td>
      <td><asp:CheckBox ID="chbGooglePlus" runat="server" />
        <label for="chbInactive" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If checked, the Google button enabled.</p></td>
    </tr>
    <tr>
      <td><p class="name">Program Inactive </p></td>
      <td><asp:CheckBox ID="chbInactive" runat="server" />
        <label for="chbInactive" class="checkbox-default">Inactive</label></td>
      <td><p class="guide">If checked, no login, no price button, and no download resources in the session list.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Retake day enable </p></td>
      <td><asp:CheckBox ID="chbRetake" runat="server" />
        <label for="chbRetake" class="checkbox-default">Enabled</label></td>
      <td><p class="guide"> If checked, the retake button is displayed at the finished days rows.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Other-Programs invisible </p></td>
      <td><asp:CheckBox ID="chbOtherProgramsInvisible" runat="server" />
        <label for="chbOtherProgramsInvisible" class="checkbox-default">Invisible</label></td>
      <td><p class="guide"> If checked, the Other-Programs list invisible on CTPP page.</p></td>
    </tr>
     <tr>
      <td><p class="name">Program Label </p></td>
      <td><label for="fileUpload3"></label>
        <asp:FileUpload ID="fileUpload3" runat="server" />
        <div class="flashlogo"> <asp:Image ID="CTPPLogoField" runat="server" Height="60px" /></div></td>
      <td><p class="guide">This is the logo of the program that is shown on the OTHER programs CTPP to make it easy for the user to move between different programs in the same brand.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <%--<tr>
      <td><p class="name">Start-Button invisible </p></td>
      <td><asp:CheckBox ID="chbStartButtonInvisible" runat="server" />
        <label for="chbStartButtonInvisible" class="checkbox-default">Invisible</label></td>
      <td><p class="guide"> If it checked,the StartButton will invisible on CTPP page.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>--%>
     <tr>
      <td><p class="name">Colors of the ribbon </p></td>
      <td>
        <table id="setColorTable" width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
          <td>Darkest color</td>
          <td>Lightest color</td>
          <td>Subheading color</td>
        </tr>
        <tr>
          <td><asp:TextBox ID="txtDarkColor" runat="server" CssClass="textfield-small"></asp:TextBox></td>
          <td><asp:TextBox ID="txtLightColor" runat="server" CssClass="textfield-small"></asp:TextBox></td>
          <td><asp:TextBox ID="txtSubheadColor" runat="server" CssClass="textfield-small"></asp:TextBox></td>
        </tr>
        </table>
    </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">SubPrice link </p></td>
      <td><asp:TextBox ID="txtSubPriceLink" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">ForSide link </p></td>
      <td><asp:TextBox ID="txtForSideLink" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Video link </p></td>
      <td><asp:TextBox ID="txtVideoLink" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Image for video</p></td>
        <td><label for="fileUpload4"></label>
            <asp:FileUpload ID="fileUpload4" runat="server" />
            <div class="flashlogo">
                <asp:Image ID="imgForVideo" runat="server"/>
            </div>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 1 header</p></td>
      <td><asp:TextBox ID="txtFact1Header" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 1 content</p></td>
      <td><asp:TextBox ID="txtFact1Content" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 2 header</p></td>
      <td><asp:TextBox ID="txtFact2Header" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 2 content</p></td>
      <td><asp:TextBox ID="txtFact2Content" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 3 header</p></td>
      <td><asp:TextBox ID="txtFact3Header" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 3 content</p></td>
      <td><asp:TextBox ID="txtFact3Content" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 4 header</p></td>
      <td><asp:TextBox ID="txtFact4Header" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Fact 4 content</p></td>
      <td> <asp:TextBox ID="txtFact4Content" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Facebook button</p></td>
      <td><asp:CheckBox ID="chbFacebook" runat="server" />
        <label for="chbFacebook" class="checkbox-default">Enabled</label></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Facebook link</p></td>
      <td><asp:TextBox ID="txtFacebookLink" runat="server"  CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Link technology </p></td>
      <td><asp:TextBox ID="txtLinkTechnology" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
     <tr>
      <td><p class="name">Promotion field 1 </p></td>
      <td><label for="fileUpload1"></label>
        <asp:FileUpload ID="fileUpload1" runat="server" />
        <div class="flashlogo"><asp:Image ID="promotionField1" runat="server" Height="60px" /></div></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Promotion link 1 </p></td>
      <td><asp:TextBox ID="txtPromotionLink1" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Promotion field 2</p></td>
      <td><label for="fileUpload2"></label>
        <asp:FileUpload ID="fileUpload2" runat="server" />
        <div class="flashlogo"><asp:Image ID="promotionField2" runat="server" Height="60px" /></div></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Promotion link 2 </p></td>
      <td><asp:TextBox ID="txtPromotionLink2" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
   
    <tr>
        <td><p class="name">Brand</p></td>
        <td>
            <asp:DropDownList ID="ddlBrand" runat="server" DataTextField="BrandName" DataValueField="BrandGUID" CssClass="listmenu-large">
            </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name"> Specific report and help Buttons enable</p></td>
      <td><asp:CheckBox ID="chbSpecificReportAndHelp" runat="server" />
        <label for="chbSpecificReportAndHelp" class="checkbox-default">Enabled</label></td>
      <td><p class="guide">If checked, the reportandhelp buttons's text are specific content.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Report Button Bind to Relapse </p></td>
      <td>
        <asp:Button ID="btnSetReportRelapse" runat="server" Text="Select Relapse" CssClass="button-add"
                                    OnClick="btnSetReportRelapse_Click" OnClientClick="return confirm('System will save all the other fields before this operation')" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnUnBindReportRelapse" runat="server" Text="Delete" CssClass="button-delete" OnClick="btnUnBindReportRelapse_Click" />
                    <p class="relapsename"><asp:Label ID="lblReportRelapse" Width="300px" runat="server" Text=""></asp:Label></p>
                </ContentTemplate>
        </asp:UpdatePanel>
        <p class="name"></p>
        </td>
      <td><p class="guide">If there is a relapse bind to CTPP, the name will be displayed instead of the select button.</p></td>
    </tr>
    <tr>
      <td><p class="name">Report Button heading</p></td>
      <td><asp:TextBox ID="txtReportButtonHeading" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report Button actual</p></td>
      <td><asp:TextBox ID="txtReportButtonActual" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report Button complete</p></td>
      <td><asp:TextBox ID="txtReportButtonComplete" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report Button untaken</p></td>
      <td><asp:TextBox ID="txtReportButtonUntaken" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report Button Available Time </p></td>
      <td><p class="timetext">HH: </p>
      <asp:TextBox ID="txtReportButtonAvailableTime" CssClass="textfield-timeset" runat="server" MaxLength="2"></asp:TextBox>
      <asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" MaximumValue="23"
                        MinimumValue="0" ErrorMessage="The range must be 0-23" SetFocusOnError="True"
                        Type="Integer" ControlToValidate="txtReportButtonAvailableTime"></asp:RangeValidator>
       </td>
      <td><p class="guide">The available time must be an integer and the range is 0-23</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Help Button Bind to Relapse </p></td>
      <td>
        <asp:Button ID="btnSetHelpRelapse" runat="server" Text="Select Relapse" CssClass="button-add"
                                    OnClick="btnSetHelpRelapse_Click" OnClientClick="return confirm('System will save all the other fields before this operation')" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnUnBindHelpRelapse" runat="server" Text="Delete" CssClass="button-delete"  OnClick="btnUnBindHelpRelapse_Click" />
                <p class="relapsename"><asp:Label ID="lblHelpRelapse" Width="300px" runat="server" Text=""></asp:Label></p>
            </ContentTemplate>
        </asp:UpdatePanel>
      </td>
      <td>&nbsp;</td>
    </tr>
     <tr>
      <td><p class="name">Help Button heading</p></td>
      <td><asp:TextBox ID="txtHelpButtonHeading" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Help Button actual</p></td>
      <td><asp:TextBox ID="txtHelpButtonActual" runat="server" CssClass="textfield-default"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Report RemindSMS1 text</p></td>
      <td> <asp:TextBox ID="txtRemindSMS1Text" CssClass="textfield-default" runat="server" MaxLength="1000"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report RemindSMS1 time </p></td>
      <td><p class="timetext">HH: </p>
      <asp:TextBox ID="txtRemindSMS1TimeHour" CssClass="textfield-timeset" runat="server" MaxLength="2"></asp:TextBox>
        <p class="timetext">MM: </p>
        <asp:TextBox ID="txtRemindSMS1TimeMinute" CssClass="textfield-timeset"  runat="server" MaxLength="2"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" Display="Dynamic" MaximumValue="23"
                        MinimumValue="0" ErrorMessage="The range of Hour must be 0-23" SetFocusOnError="True"
                        Type="Integer" ControlToValidate="txtRemindSMS1TimeHour"></asp:RangeValidator>
                    <asp:RangeValidator ID="RangeValidator3" runat="server" Display="Dynamic" MaximumValue="59"
                        MinimumValue="0" ErrorMessage="The range of Minute must be 0-59" SetFocusOnError="True"
                        Type="Integer" ControlToValidate="txtRemindSMS1TimeMinute"></asp:RangeValidator>
        </td>
      <td><p class="guide"> (HH is 1-23 and MM is 0-59)</p></td>
    </tr>
    <tr>
      <td><p class="name">Report RemindSMS2 text</p></td>
      <td> <asp:TextBox ID="txtRemindSMS2Text" CssClass="textfield-default" runat="server" MaxLength="1000"></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Report RemindSMS2 time </p></td>
      <td><p class="timetext">HH: </p>
        <asp:TextBox ID="txtRemindSMS2TimeHour" CssClass="textfield-timeset" runat="server" MaxLength="2"></asp:TextBox>
        <p class="timetext">MM: </p>
        <asp:TextBox ID="txtRemindSMS2TimeMinute" CssClass="textfield-timeset" runat="server" MaxLength="2"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator4" runat="server" Display="Dynamic" MaximumValue="23"
                        MinimumValue="0" ErrorMessage="The range of Hour must be 0-23" SetFocusOnError="True"
                        Type="Integer" ControlToValidate="txtRemindSMS2TimeHour"></asp:RangeValidator>
                    <asp:RangeValidator ID="RangeValidator5" runat="server" Display="Dynamic" MaximumValue="59"
                        MinimumValue="0" ErrorMessage="The range of Minute must be 0-59" SetFocusOnError="True"
                        Type="Integer" ControlToValidate="txtRemindSMS2TimeMinute"></asp:RangeValidator></td>
      <td><p class="guide"> (HH is 1-23 and MM is 0-59)</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Program presenter</p></td>
      <td><label for="setpresenter"></label>
       <input type="button" onclick="setPresenter()" id="setpresenter" name="setpresenter" value="Set Presenter" class="button-update" style="float:none; margin-top:20px" />
        <div class="flashlogo"> <asp:Image ID="CTPPPresenterField" runat="server" Height="60px" /></div></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Bookmark Image For Mobile </p></td>
      <td> 
        <div style="margin-top:45px">
        <label for="fulMobileBookmark"></label>
         <asp:FileUpload ID="fulMobileBookmark" runat="server" />
        <div class="flashlogo"> <asp:Image ID="imgMobileBookmark" runat="server" Height="60px" /></div>
        </div>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td></td>
      <td align="right">
       <asp:Button ID="okButton" runat="server" CssClass="button-update" Text="Update" OnClick="okButton_Click" />
       <asp:Button ID="cancelButton" runat="server" CssClass="button-delete"  Text="Cancel" OnClick="cancelButton_Click" />
      </td>
      <td>&nbsp;</td>
    </tr>
  </table>
</div>
<p>&nbsp;</p>
        <asp:HiddenField ID="PresenterImageHiddenControl" runat="server" />
    <div id="presenterimage" style="display: none" title="Presenter Image">
        <iframe id="imagemanager" frameborder="0" height="100%" width="100%" src=""></iframe>
    </div>
</asp:Content>
