<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportUserVariable.aspx.cs"
    Inherits="ChangeTech.DeveloperWeb.ExportUserVariable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export user variable</title>
    <link href="Themes/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="text-align: center; vertical-align: middle">
        <div id="header">
            Export user variable
        </div>
        <div id="centerPart">
            <asp:UpdatePanel ID="updatePanel" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="updateTimer" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <table width="100%" style="margin:0px; padding:0px">
                        <tr style="text-align:left">
                            <td style="width:30%">
                                <b>Status:</b>
                            </td>
                            <td style="width:70%">
                                <asp:Label ID="msgLbl" runat="server" Text="Exporting data, please wait for seconds......"></asp:Label>
                            </td>
                        </tr>
                        <tr style="text-align:left">
                            <td><b>Time used:</b></td>
                            <td><asp:Label ID="timeLbl" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HyperLink ID="downloadLnk" runat="server" Text="[Download]" Font-Bold="true"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="startDateTimeHF" runat="server" />
    <asp:HiddenField ID="fileNameHF" runat="server" />
    <asp:Timer ID="updateTimer" runat="server" OnTick="updateTimer_Tick" 
        Interval="15000">
    </asp:Timer>
    </form>
</body>
</html>
