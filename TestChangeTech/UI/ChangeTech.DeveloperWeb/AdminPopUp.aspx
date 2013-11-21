<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPopUp.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AdminPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    E-mail
                </td>
                <td>
                    <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="SearchButton" runat="server" Text="Search" 
                        onclick="SearchButton_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="adminGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="12">
         <Columns>
            <asp:BoundField HeaderText="E-mail" DataField="UserName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="selectLinkButton" runat="server" OnClick="selectLinkButton_Click"
                        CommandArgument='<%#Eval("UserName") %>'>Select</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </div>
    <script language="javascript">
        function returnValue() {
            window.returnValue = 'true';
            window.closed();
        }
    </script>
    </form>
</body>
</html>
