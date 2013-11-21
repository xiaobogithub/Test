<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ManageTipMeassage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageTipMeassage"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage tip message overview</h1>
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
        <td><p class="name">Name:</p></td>
        <td>
             <asp:DropDownList ID="ddlTipMessage" runat="server" AutoPostBack="True" CssClass="listmenu-large" onselectedindexchanged="ddlTipMessage_SelectedIndexChanged">
                </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Title:</p></td>
        <td>
             <asp:TextBox ID="txtTitle" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Message:</p></td>
        <td>
             <asp:TextBox ID="txtMessage" runat="server"  CssClass="textfield-largetext" Rows="5" TextMode="MultiLine"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Go Back Button :</p></td>
        <td>
             <asp:TextBox ID="txtBackButton" runat="server" CssClass="textfield-small"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
             <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="button-update" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
        <td><p class="name">Notes:</p></td>
        <td>
             <asp:TextBox ID="txtExplanation" runat="server" CssClass="textfield-largetext"  Rows="5" TextMode="MultiLine" Enabled ="false" Wrap="true"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr style="height:10px;">
    </tr>
    </table>  
    </div>
    </div>
</asp:Content>
