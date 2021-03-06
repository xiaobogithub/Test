﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddNewPageSequence.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddNewPageSequence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add new pagesequence overview</h1>
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
        <td><p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Share,Name %>"></asp:Literal></p></td>
        <td>
             <asp:TextBox ID="txtName" runat="server"  CssClass="textfield-largetext"></asp:TextBox><span style="color: Red"></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="name"> <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:Share,Description %>"></asp:Literal></p></td>
        <td>
                <asp:TextBox ID="txtDes" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="7"></asp:TextBox>
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
            <asp:Button ID="btnAdd" runat="server" Text="<%$ resources:Share,Add %>" CssClass="button-update"  onclick="btnAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="button-delete" Text="<%$ resources:Share,Cancel %>" OnClick="btnCancel_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
