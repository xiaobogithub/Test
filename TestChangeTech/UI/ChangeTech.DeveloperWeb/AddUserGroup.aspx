<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddUserGroup.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddUserGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                Name
            </td>
            <td>
                <asp:TextBox ID="groupNameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Description
            </td>
            <td>
                <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine" Height="100px"
                    Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="addButton" runat="server" Text="Add" OnClick="addButton_Click" />
                <asp:HyperLink ID="cancelHyperLink" runat="server">Cancel</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
