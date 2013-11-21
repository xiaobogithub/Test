<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditUserGroup.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditUserGroup" %>

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
                <asp:Button ID="updateButton" runat="server" Text="Update" 
                    onclick="updateButton_Click" />
                <asp:HyperLink ID="cancelHyperLink" runat="server">Cancel</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
