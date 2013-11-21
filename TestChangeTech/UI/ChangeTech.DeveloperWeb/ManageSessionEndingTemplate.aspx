<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageSessionEndingTemplate.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageSessionEndingTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="header">
  <h1>Manage session ending overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<p>&nbsp;</p>
    <b><asp:Label ID="programLabel" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label></b>
 <p>&nbsp;</p>
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:25%">&nbsp;</td>
      <td style="width:40%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Title : </p>
            </td>
            <td>
                <asp:TextBox ID="titleTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="titleTextBox"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Text : </p>
            </td>
            <td>
                 <asp:TextBox ID="textTextBox" runat="server" TextMode="MultiLine" Rows="10" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="textTextBox"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">PrimaryButton name : </p>
            </td>
            <td>
                 <asp:TextBox ID="primaryButtonTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="primaryButtonTextBox"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td>&nbsp;</td>
            <td align="right">
            <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click"  CssClass="button-update"/>
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
</div>
</asp:Content>
