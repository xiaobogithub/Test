<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UpdateUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Update user overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
    <div class="box-main">
    <table>
        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Email %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="emailTextBox" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CellPhone %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="cellPhoneTextBox" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:FirstName %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="firstNameTextBox" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:LastName %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="lastNameTextBox" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:UserType %>"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="listmenu-default">
                    <asp:ListItem Text="User" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Tester" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Customer" Value="4"></asp:ListItem>
                    <asp:ListItem Text="ProjectManager" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Gender %>"></asp:Literal>
            </td>
            <td>
                <asp:DropDownList ID="genderDropDownList" runat="server" CssClass="listmenu-default">
                <asp:ListItem Text = "Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text = "Female" Value="Female"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="okButton" runat="server" Text="<%$Resources:Share,Update %>" CssClass="button-update" OnClick = "okButton_click" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
