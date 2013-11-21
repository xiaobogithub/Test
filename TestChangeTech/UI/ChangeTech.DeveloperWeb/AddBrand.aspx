<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBrand.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddBrand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="header">
  <h1>Add Brand overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div class="box-main">
    <table>
    <tr>
      <td style="width:25%">&nbsp;</td>
      <td style="width:40%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:BrandName %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBrandName" runat="server" CssClass="textfield-largetext"></asp:TextBox><span class="requiredMarked">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Required" ControlToValidate="txtBrandName"></asp:RequiredFieldValidator>
            </td>
            <td >&nbsp;</td>
        </tr>

        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ resources:BrandURL %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBrandURL" runat="server" CssClass="textfield-largetext"></asp:TextBox><span class="requiredMarked">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Required" ControlToValidate="txtBrandURL"></asp:RequiredFieldValidator>
            </td>
            <td >&nbsp;</td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td><p class="name"><asp:Literal ID="Literal7" Text="<%$ Resources:LogoPicture%>" runat="server"></asp:Literal></p></td>
            <td>
                <asp:FileUpload ID="fileUpload" runat="server" />
                <div class="flashlogo"><img width="189" height="59" alt="Temp logo" src="../gfx/logo-program.png"/></div>
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td>&nbsp;</td>
            <td align="right">
                <asp:Button ID="btnAdd" runat="server" Text="<%$ resources:Share,Add %>" OnClick="btnAdd_Click" CssClass="button-update"/>
                    <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
  </div> 
</asp:Content>
