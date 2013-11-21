<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditBrand.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditBrand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit Brand overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" Text="<%$ Resources:Share, Name%>" runat="server"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBrandName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" Text="<%$ Resources:Share, URL%>" runat="server"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBrandURL" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal7" Text="<%$ Resources:LogoPicture%>" runat="server"></asp:Literal></p>
            </td>
            <td>
                <asp:FileUpload ID="fileUpload" runat="server" />
                 <div class="flashlogo"><asp:Image ID="brandLogo" runat="server" Height="60px" /></div>
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
                <asp:Button ID="btnUpdateBrand" Text="<%$ Resources:btnUpdateBrand %>" runat="server" OnClick="btnUpdateBrand_Click"  CssClass="button-update"/>                
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
            <asp:Label ID="warnLbl" runat="server" Text="<%$ Resources:NotEditable %>"
                    Visible="false" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</div>
</div>
</asp:Content>
