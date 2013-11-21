<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddSession.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddSession" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add  one session overview</h1>
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
        <td><p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ resources:Share,Day %>"></asp:Literal>:</p></td>
        <td>
            <asp:DropDownList ID="ddlDay" runat="server" CssClass="listmenu-default" ></asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Share,Name %>"></asp:Literal>:</p></td>
        <td>
           <asp:TextBox ID="txtSessionName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Share,Description %>"></asp:Literal>:</p></td>
        <td>
                <asp:TextBox ID="txtSessionDescription" runat="server" TextMode="MultiLine" Rows="10" CssClass="textfield-largetext"></asp:TextBox>
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
                <asp:Button ID="btnAddOneSession" runat="server" CssClass="button-update"  Text="<%$ resources:Share,Add %>" OnClick="btnAdd_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelOneSession" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>"  OnClick="btnCancelOneSession_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
  </table>
    </div>

  <div class="header">
  <h1>Add more sessions overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>

<div class="box-main">
    <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:43%;">&nbsp;</td>
      <td style="width:32%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"> Which day you want to add after:</p></td>
        <td>
           <asp:DropDownList ID="startDayDropDownList" runat="server" CssClass="listmenu-default" ></asp:DropDownList>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">How many sessions you want to add:</p></td>
        <td>
            <asp:TextBox ID="daysTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr align="center">
        <td></td>
        <td align="right">
                <asp:Button ID="btnAddMoreSessions" runat="server" Text="Add" CssClass="button-update" OnClick="Button_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelMoreSession" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>" OnClick="btnCancelMoreSession_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
  </table>
    </div>
<%--end tag--%>
</div>
</asp:Content>
