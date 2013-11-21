<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditStudyContent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditStudyContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add study content overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>

<div style="height:580px;">
<div class="box-main">
  <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
     <tr>
        <td><p class="name">URL : </p></td>
        <td>
         <asp:TextBox ID="urlTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="urlTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
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
             <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click"  CssClass="button-update"/>
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" CausesValidation="False" CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
  </div>
</div>
</asp:Content>
