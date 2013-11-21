<%@ Page Title="Change Tech Developer" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"  runat="server">
<title>Developer • Log in</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<link href="css/reset.css" rel="stylesheet" type="text/css" media="all" />
<link href="css/general.css" rel="stylesheet" type="text/css" media="all"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="Scripts/animatedcollapse.js">

    /***********************************************
    * Animated Collapsible DIV v2.4- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
    * This notice MUST stay intact for legal use
    * Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and 100s more
    ***********************************************/
</script>
<script type="text/javascript">
    animatedcollapse.addDiv('passwordreminder', 'fade=1')
    animatedcollapse.ontoggle = function ($, divobj, state) { //fires each time a DIV is expanded/contracted
        //$: Access to jQuery
        //divobj: DOM reference to DIV being expanded/ collapsed. Use "divobj.id" to get its ID
        //state: "block" or "none", depending on state
    }
    animatedcollapse.init()

    $(function () {
//        $("#ChangeTechLogin_UserName").css("background-color", "#FFF");
//        $("#ChangeTechLogin_Password").css("background-color", "#FFF");
        var ChangeTechLoginButton = $("#ChangeTechLogin_LoginButton");
        var ChangeTechPwdRecoveryLink = $("#ChangeTechLogin_PasswordRecoveryLink")
        $("label[for='ChangeTechLogin_RememberMe']").addClass("checkbox-default");
        $("td[class='chkpading']").parent().after('<tr><td><p class="divider"/></td><td><p class="divider" /></td></tr>');
        ChangeTechPwdRecoveryLink.parent().attr("colSpan", 1);
        var forgotPwdLink = ChangeTechPwdRecoveryLink.parent().parent().html();
        ChangeTechPwdRecoveryLink.parent().parent().remove();
        ChangeTechLoginButton.parent().css("padding-top", "10px").css("padding-right", "22px").before(forgotPwdLink); //css("padding-top", "10px")
        $("#ChangeTechLogin_PasswordRecoveryLink").parent().css("padding-top", "18px");
    });
    
</script>
</head>
<body>
<form id="Form1" runat="server" autocomplete="off">

<div id="errorMessageDiv" class="alertmessage " runat="server" visible="false">You input the wrong SMS-Code,please enter again. </div>
<div class="confirmationmessage hidden" id="confirmMessageDiv"  runat="server">The program is updated!</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="box-small">
  <div class="flipnav">
    <div class="logo"><a href="#"><img src="gfx/logo-developer.png" width="230" height="30" alt="Changetech Developer" /></a>
      <div class="version"><%= VersionNumber%></div>
    </div>
    <div class="clear"></div>
  </div>
  <table id="loginTable">
    <tr>
        <td>
             <%--Send SMS Div--%>
                    <div id="sendSMS" runat="server" visible="false" >
                                <p  class="name">
                                    <label id="lblSMScode" runat="server">
                                        Your login needs SMS authentication. Please enter the code that you are now receiving on your mobile phone:
                                    </label>
                                </p>
                                <br />
                                <p class="name">
                                    <label id="lblSMSText" runat="server" style="padding-left:5px">SMS-code :</label>
                                    <input id="txtSMSCode" type="text" class="textfield-small" style="margin-left:50px" runat="server" />
                                </p>
                                <br />
                                <p class="divider">&nbsp;</p>
                                <p style="padding-left:280px">
                                    <asp:Button ID="btnValidateSMS"  runat="server"  CssClass="button-update" Text="OK"  onclick="btnValidateSMS_Click"/>
                                </p>
                                <p> 
                                    <label id="lblCodeErrorMessage" runat="server" visible="false" style="padding-left:5px; color:Red;">You input the wrong SMS-Code,please enter again.</label>
                                </p>
                    </div>
                <div id="login">
                    <asp:Login ID="ChangeTechLogin" runat="server"
                        OnAuthenticate="ChangeTechLogin_Authenticate"
                        BorderStyle="None" 
                        BorderWidth="0px" 
                        CssClass="nestedTable"
                        LabelStyle-CssClass="name" 
                        TextBoxStyle-CssClass="textfield-login" 
                        LoginButtonStyle-CssClass="button-update" 
                        HyperLinkStyle-CssClass="paddedlink"
                        LabelStyle-HorizontalAlign="Left"
                        CheckBoxStyle-CssClass="chkpading"
                        TitleText="  "
                        TitleTextStyle-Height="15px"
                        FailureText="<%$ Resources: LoginFail%>"
                        LoginButtonText="<%$ Resources: Login%>" 
                        PasswordLabelText="<%$ Resources: Password%>"
                        RememberMeText="<%$ Resources: RemeberMe%>"
                        UserNameLabelText="<%$ Resources: UserName%>"
                        PasswordRecoveryText="<%$ Resources: ForgetPassword %>" 
                        PasswordRecoveryUrl="javascript:animatedcollapse.toggle('passwordreminder')">
                    </asp:Login>
                </div>
        </td>
    </tr>
    <tr>
        <td>
            
        </td>
    </tr>
    <tr>
      <td colspan="3"><table id="passwordreminder" style="display:none;">
          <tr>
            <td width="67%" style="padding:0px 0px 10px 0px;"><p class="divider">&nbsp;</p></td>
            <td width="23%" style="padding:0px 0px 10px 0px;"><p class="divider">&nbsp;</p></td>
            <td width="10%" style="padding:0px 0px 10px 0px;"><p class="divider">&nbsp;</p></td>
          </tr>
          <tr>
            <td colspan="3" class="heading" style="padding-bottom:20px;padding-top:20px">Retrieve your password by filling out the following:</td>
          </tr>
          <tr>
            <td><p class="name">Your E-mail:</p></td>
            <td>
                <asp:TextBox ID="txtUserEmail" runat="server" CssClass="textfield-login"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td><p class="name">Your Mobile:</p></td>
            <td>
                <asp:TextBox ID="txtMobilePhone" runat="server"  CssClass="textfield-login"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td style="padding:0px 0px 10px 0px;"><p class="divider">&nbsp;</p></td>
            <td style="padding:0px 0px 10px 0px;"><p class="divider">&nbsp;</p></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
                <td width="67%" ></td>
                <td width="23%" style="text-align:right; padding-top:10px;">
                <asp:Button ID="btnRetrieve" runat="server" CssClass="button-update"  
                        Text="Retrieve" onclick="btnRetrieve_Click" />
                <td width="10%"></td>
                <asp:Label ID="LoginErrorLabel" CssClass="alertmessage" runat="server" Visible="false" Text="An error occured. Please try again later." Width="380px"></asp:Label>
                <asp:Label ID="PwdSendLabel" CssClass="confirmationmessage" runat="server" Visible="false" Text="Password is sent to your E-mail address." Width="380px"></asp:Label>
                <div class="alertmessage hidden">An error occured. Please try again later</div>
                <div class="confirmationmessage hidden">Password is sent to your E-mail address</div>
          </tr>
        </table>
        </td>
    </tr>
  </table>
</div>
<div id="footer">DEVELOPER &copy; Changetech 2012</div>
</form>
</body>
</html>