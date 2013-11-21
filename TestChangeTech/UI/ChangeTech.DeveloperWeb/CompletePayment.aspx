<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompletePayment.aspx.cs"
    Inherits="ChangeTech.DeveloperWeb.CompletePayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache,must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <title>Untitled Page</title>
    <link href="Themes/payment.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
    function closewindow() {
        window.close();
    }
    function popup() {
        alert("Please contact with IT");
    }

</script>

<body>
    <form id="form1" runat="server">
    <!--{{{start:header-->
    <div id="header">
        &nbsp;<asp:Image ID="LogoImage" runat="server" Height="60" /><h1>
            ChangeTech payment</h1>
    </div>
    <!--}}}end:header-->
    <!--{{{start:nav-->
    <div id="nav">
    </div>
    <!--}}}end:nav-->
    <!--{{{start:main-->
    <div id="main">
        <div class="public_box login">
            <div id="Div_Errtip" runat="server">
                <asp:Label ID="lb_Message" runat="server"></asp:Label></div>
            <div class="conner_lt">
            </div>
            <div class="conner_rt">
            </div>
            <div class="conner_lb">
            </div>
            <div class="conner_rb">
            </div>
            <h4>
                Message</h4>
            <div class="pb_inner">
                <table>
                    <tr style="height: 400;">
                        <td>
                            <asp:Label ID="resultLabel" runat="server" Text=""></asp:Label><br />
                            <asp:HyperLink ID="loginHyperLink" runat="server" Visible="false"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <!--}}}end:main-->
    <div id="footer">
        copyrights &copy; 2010 changetech
    </div>
    </form>
</body>
</html>
