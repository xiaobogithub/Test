<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddCompany.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>Add company overview</h1>
        <div class="headermenu"></div>
        <div class="clear"></div>
    </div>
    <div style="height:580px;">
    <div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Name :</p></td>
      <td>
                <asp:TextBox ID="nameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nameTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Description :</p></td>
      <td>
                <asp:TextBox ID="desTextBox" runat="server" CssClass="textfield-largetext" TextMode="MultiLine" Rows="5"></asp:TextBox>
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
                <asp:Button ID="addButton" runat="server" Text="Add" CssClass="button-update" onclick="addButton_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="editButton" runat="server" Text="Cancel" CssClass="button-delete" CausesValidation="False" onclick="editButton_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
