<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CopyFromAnotherDay.aspx.cs" Inherits="ChangeTech.DeveloperWeb.CopyFromAnotherDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Copy from another day</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
<tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Program</p></td>
        <td>
              <asp:DropDownList ID="programDropDownList" runat="server" CssClass="listmenu-large" AutoPostBack="true" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Language</p></td>
        <td>
             <asp:DropDownList ID="languageDropDownList" runat="server" CssClass="listmenu-default" AutoPostBack="true" OnSelectedIndexChanged="languageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name">Please select session you want</p></td>
        <td>
               <asp:DropDownList ID="sessionDropDownList" CssClass="listmenu-default" runat="server">
                </asp:DropDownList>
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
            <asp:Button ID="okButton" runat="server" Text="OK" CssClass="button-update"   OnClick="okButton_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" CssClass="button-delete"   OnClick="cancelButton_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
