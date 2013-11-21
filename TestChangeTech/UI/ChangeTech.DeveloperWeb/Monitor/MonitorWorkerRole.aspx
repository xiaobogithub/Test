<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorWorkerRole.aspx.cs"
    Inherits="ChangeTech.DeveloperWeb.Monitor.MonitorWorkerRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblMessage" Width="100%"></asp:Label>
    </div>
    <div>
        <asp:Button ID="btnEmailDetails" runat="server" 
            Text="View Today's Reminder Email Details" onclick="btnEmailDetails_Click" />
    </div>
    <table style="width: 100%; line-height: 25px">
        <tr>
            <td>
                Reminder email total count
            </td>
            <td>
                <asp:Label ID="lblTotalCount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Reminder email success count
            </td>
            <td>
                <asp:Label ID="lblSuccessCount" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Reminder email failure
            </td>
            <td>
                <asp:DataList ID="dlFailureEmails" runat="server">
                    <ItemTemplate>
                        <ul>
                            <%# ((string)Container.DataItem) %>
                        </ul>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
