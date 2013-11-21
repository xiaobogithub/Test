<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.6.2.ct.timezone.js" ></script>
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

        $(document).ready(function () {
            var programGuid = GetArgsFromHref(window.location.href, "ProgramGUID");
            //alert(programGuid);
            $('#pTimeZone').timezone({
                programGuid: programGuid
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="alertmessage hidden">There is an error somewhere</div>
    <div class="confirmationmessage hidden">The program is updated!</div>
    <div class="header">
        <h1>Manage program overview</h1>
        <div class="clear"></div>
    </div>
    <div class="box-main">
    <table>
    <tr>
      <td style="width:20%;">&nbsp;</td>
      <td style="width:55%;">&nbsp;</td>
      <td style="width:25%">&nbsp;</td>
    </tr>
        <%--<tr>
            <td colspan="3" align="center">
                <b>Program URLs</b>
            </td>
    </tr>--%>
    <tr>
      <td><p class="name">Screen URL :</p></td>
       <td>
                <table style="padding:10px">
                <tr><td style="width:140px"><label class="checkbox-default">(Default by choice):</label></td><td><asp:HyperLink ID="screenDefaultHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px"><label class="checkbox-default">(Flash Version):</label></td><td><asp:HyperLink ID="screenFlashHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px;padding-bottom:0px;"><label class="checkbox-default">(HTML5 Version):</label></td><td style="padding-bottom:0px;"><asp:HyperLink ID="screenHTML5HyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
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
      <td><p class="name">Login URL :</p></td>
       <td>
                <table>
                <tr><td style="width:140px;"><label class="checkbox-default">(Default by choice):</label></td><td><asp:HyperLink ID="loginDefaultHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px;"><label class="checkbox-default">(Flash Version):</label></td><td><asp:HyperLink ID="loginFlashHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px;padding-bottom:0px;"><label class="checkbox-default">(HTML5 Version):</label></td><td style="padding-bottom:0px;"><asp:HyperLink ID="loginHTML5HyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
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
      <td><p class="name">Test URL :</p></td>
       <td >
                <table>
                <tr><td style="width:140px;"><label class="checkbox-default">(Default by choice):</label></td><td><asp:HyperLink ID="testDefaultHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px;"><label class="checkbox-default">(Flash Version):</label></td><td><asp:HyperLink ID="testFlashHyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td style="width:140px;padding-bottom:0px;"><label class="checkbox-default">(HTML5 Version):</label></td><td style="padding-bottom:0px;"><asp:HyperLink ID="testHTML5HyperLink" runat="server" Font-Size="Small" Target="_blank"></asp:HyperLink></td></tr>
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
      <td><p class="name">CTPP URL :</p></td>
       <td>
                <table>
                    <tr><td style="width:140px;padding-bottom:0px;"><label class="checkbox-default">CTPP URL:</label></td><td style="padding-bottom:0px;"><asp:HyperLink ID="CTPPHyperLink" Font-Size="Small" runat="server" Target="_blank"></asp:HyperLink></td></tr>
                </table>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
        <%--<tr>
            <td colspan="3" align="center">
                <b>Program Configuration</b>
            </td>
    </tr>--%>
   
    <tr>
      <td><p class="name">Support email address :</p></td>
      <td><asp:TextBox ID="supportEmailTextBox" runat="server" CssClass="textfield-extention" ></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Support email name :</p></td>
      <td><asp:TextBox ID="supportNameTextBox" runat="server" CssClass="textfield-extention" ></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
   <tr>
      <td><p class="name"><asp:Literal ID="Literal7" Text="<%$ Resources:LogoPicture%>" runat="server"></asp:Literal></p></td>
      <td><label for="fileUpload"></label>
            <asp:FileUpload ID="fileUpload" runat="server" />
            <div class="flashlogo">
                <asp:Image ID="programLogo" runat="server" Width="189px" Height="59px"/>
            </div>
        </td>
      <td><p class="guide">This is the logo picture that is used in the Flash-version of this program</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name"> Pincode enable :</p></td>
      <td><asp:CheckBox ID="pinCodeCheckBox" runat="server" />
        <label for="pinCodeCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">Pincode is generated when register and send by SMS. If pincode is enable, user need to enter pincode when login.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Chargeable :</p></td>
      <td><asp:CheckBox ID="paymentCheckBox" runat="server" />
        <label for="paymentCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If the program is chargeable, after register, it will redirect user to payment page where user can pay for subscription.</p></td>
    </tr>
     <tr>
      <td><p class="name">Price :</p></td>
      <td><asp:TextBox ID="priceTextBox" runat="server" CssClass="textfield-extention"></asp:TextBox>
                &nbsp;<label class="checkbox-default">NOK</label>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Privacy interval</p></td>
      <td> <asp:TextBox ID="weeksTextBox" runat="server" CssClass="textfield-extention" ></asp:TextBox>
                &nbsp;<label class="checkbox-default">Weeks</label>
        </td>
      <td><p class="guide">How many weeks later after user complete the program, we disconnect their data from them.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Privacy protected :</p></td>
      <td><asp:CheckBox ID="cutConnectCheckBox" runat="server" />
        <label for="cutConnectCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide"> If the program need to protect users' privacy, we will disconnect their data from them after they complete the program in specific interval.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Two parts program :</p></td>
      <td><asp:CheckBox ID="isContainTwoPartsCheckBox" runat="server" />
        <label for="isContainTwoPartsCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If program with has two parts, user can send SMS to stop the first part and then start the second part.</p></td>
    </tr>
     <tr>
      <td><p class="name">Second part start from :</p></td>
      <td><asp:DropDownList ID="switchDayDropDownList" CssClass="listmenu-default" runat="server" Width="168px">
                </asp:DropDownList>
        </td>
      <td><p class="guide">Which session the second part will start from.</p></td>
    </tr>
    <tr>
      <td><p class="name">Short name</p></td>
      <td><asp:TextBox ID="shortNameTextBox" runat="server" 
              CssClass="textfield-extention" ></asp:TextBox></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Https enable :</p></td>
      <td><asp:CheckBox ID="supportHttpsCheckBox" runat="server" />
        <label for="supportHttpsCheckBox" class="checkbox-default">Enable</label></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Gender sensitive :</p></td>
      <td><asp:CheckBox ID="genderCheckBox" runat="server" />
        <label for="genderCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If need statistic both for Male and Female separately, please check this.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Require serial number :</p></td>
      <td><asp:CheckBox ID="needSerialNumberCheckBox" runat="server" />
        <label for="needSerialNumberCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If serial number is required in this program, user need to type in a serial number when register.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">No Catching Up Enable :</p></td>
      <td><asp:CheckBox ID="noCatchingUpCheckBox" runat="server" />
        <label for="noCatchingUpCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide"> If no catching up is enable in specific programs , user will not need to take days you missed, but can go directly to todays session.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Order Program Enable :</p></td>
      <td><asp:CheckBox ID="orderProgramCheckBox" runat="server" />
        <label for="noCatchingUpCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If it is true and  program belonged Publish , indicate this program should be included in the order system.</p></td>
    </tr>
    <tr>
      <td><p class="name">Order Program Text :</p></td>
      <td>
          <asp:TextBox ID="txtOrderProgramText" runat="server" 
              CssClass="textfield-extention" TextMode="MultiLine"></asp:TextBox>
        </td>
      <td><p class="guide">It is used for email in the order system</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
     <!--HP Order Program Enable-->
        <tr>
            <td><p class="name"> HP Order Program Enable:</p></td>
            <td><asp:CheckBox ID="hpOrderProgramCheckBox" runat="server" /><label for="hpOrderProgramCheckBox" class="checkbox-default">Enable</label>
            <td><p class="guide">If it is true and program belonged Publish , indicate this program should be included in the Health profile order system.</p></td>
        </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name"> CTPP Enable :</p></td>
      <td><asp:CheckBox ID="cttpEnableCheckBox" runat="server" />
        <label for="cttpEnableCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If CTPP is enable, user can retake sessions on CTPP page. To enable CTPP, the program must has configured CTPP settings and belong to a brand.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
     <tr>
      <td><p class="name">CTPP Start-Button Invisible :</p></td>
      <td><asp:CheckBox ID="startButtonInvisibleCheckBox" runat="server" />
        <label for="startButtonInvisibleCheckBox" class="checkbox-default">Invisible</label></td>
      <td><p class="guide"> If it checked,the StartButton will invisible on CTPP page.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
     <tr>
      <td><p class="name"> Hide help menu and day number :</p></td>
      <td><asp:CheckBox ID="dayAndSetMenuInvisibleCheckBox" runat="server" />
        <label for="startButtonInvisibleCheckBox" class="checkbox-default">Invisible</label></td>
      <td><p class="guide"> If it checked,the Day and SetMenu will invisible on HTML5/Flash page of this program.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
     <tr>
      <td><p class="name">HTML5 preview enable :</p></td>
      <td><asp:CheckBox ID="html5EnableCheckBox" runat="server" />
        <label for="html5EnableCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide"> If HTML5 preview is enable, when developer click preview button, both flash and HTML5 version will be previewed.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Use responsive design in HTML5 :</p></td>
      <td><asp:CheckBox ID="html5NewUIEnableCheckBox" runat="server" />
        <label for="html5NewUIEnableCheckBox" class="checkbox-default">Enable</label></td>
      <td><p class="guide">If program use responsive design in HTML5, then program will go into the new ui url.</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
      <td><p class="name">Time-Zone Enable :</p></td>
      <td><asp:CheckBox ID="supportTimeZoneCheckBox"  runat="server" />
        <label for="startButtonInvisibleCheckBox" class="checkbox-default">Enable</label><br />
        <p id="pTimeZone"></p>
        </td>
      <td><p class="guide">If time-zone enabled , this program should support time zones</p></td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
   
  
    <tr>
      <td><p class="name">Flash or HTML5 :</p></td>
      <td colspan="2">
                <table>
                    <tr><td><asp:RadioButton ID="ChooseFlash" GroupName="FlashOrHTML5" Checked="true" runat="server" /></td><td><label class="checkbox-default">Uses Flash on all browsers.</label></td></tr>
                    <tr><td><asp:RadioButton ID="ChooseDetect" GroupName="FlashOrHTML5" runat="server" /></td><td><label class="checkbox-default">Uses HTML5 if browser supports it, if not Flash will be used.</label></td></tr>
                    <tr><td style="padding-bottom:0px;"><asp:RadioButton ID="ChooseHTML5" GroupName="FlashOrHTML5" runat="server" /></td><td style="padding-bottom:0px;"><label class="checkbox-default">Uses HTML5 on compatible browsers, message is shown to user if browsers does not support HTML5.</label></td></tr>
                </table>
        </td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td align="right">
                <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="button-update" OnClick="saveButton_Click" />
                <asp:Button ID="cancelButton" runat="server" CssClass="button-delete" Text="Cancel" OnClick="cancelButton_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
</asp:Content>
