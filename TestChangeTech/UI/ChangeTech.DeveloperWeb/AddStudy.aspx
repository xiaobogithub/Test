<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddStudy.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddStudy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add study overview</h1>
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
        <td><p class="name">Name : </p></td>
        <td>
         <asp:TextBox ID="nameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="nameTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Description : </p></td>
        <td>
            <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine"  CssClass="textfield-largetext" Rows="5"></asp:TextBox>
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
             <asp:Button ID="addButton" runat="server" Text="Add" OnClick="addButton_Click"  CssClass="button-update"/>
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
