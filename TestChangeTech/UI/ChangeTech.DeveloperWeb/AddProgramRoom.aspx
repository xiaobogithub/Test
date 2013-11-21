<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProgramRoom.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddProgramRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add program room overview</h1>
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
        <td><p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Program %>"></asp:Literal></p></td>
        <td>
            <asp:Literal ID="ltProgram" runat="server" Text=""></asp:Literal>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal></p></td>
        <td>
            <asp:TextBox ID="txtName" runat="server"  CssClass="textfield-largetext"></asp:TextBox><span style="color: Red">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtName" ErrorMessage="Required"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal></p></td>
        <td>
             <asp:TextBox ID="txtDescription" runat="server" TextMode ="MultiLine" CssClass="textfield-largetext"></asp:TextBox>
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
        <td  align="right">
        <div class="buttons">
            <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:Share,Add %>" CssClass="button-update" OnClick="btnAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>" CausesValidation="false" OnClick="btnCancel_Click"/>
            </div>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
