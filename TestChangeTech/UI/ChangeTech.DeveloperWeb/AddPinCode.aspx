<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddPinCode.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddPinCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add pincode overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
  <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:45%;">&nbsp;</td>
      <td style="width:25%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Title:</p></td>
        <td>
             <asp:TextBox ID="titleTextBox" runat="server"  CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Text:</p></td>
        <td>
             <asp:TextBox ID="textTextBox" runat="server" TextMode="MultiLine" Rows="10" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">TextBox Text:</p></td>
        <td>
            <asp:TextBox ID="pinTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Primary button text:</p></td>
        <td>
            <asp:TextBox ID="primaryButtonTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">PinCode reminder text:</p></td>
        <td>
            <asp:TextBox ID="pinCodeReminderTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
     <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
              <asp:Button ID="okButton" runat="server" Text="OK" onclick="okButton_Click" CssClass="button-update"/>
                &nbsp;&nbsp;
                <asp:Button ID="cancelButton" runat="server" Text="Cancel"  onclick="cancelButton_Click" CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
