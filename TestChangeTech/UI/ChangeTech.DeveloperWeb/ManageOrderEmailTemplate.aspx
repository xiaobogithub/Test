<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
 CodeBehind="ManageOrderEmailTemplate.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageOrderEmailTemplate" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="height:580px;">
 <div class="header">
  <h1>Order Email Template </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
   <div class="box-main">
    <table>
    <tr>
      <td width="35%">&nbsp;</td>
      <td width="30%">&nbsp;</td>
      <td width="35%">&nbsp;</td>
    </tr>
     <tr>
            <td><p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:OrderEmailTemplate %>"></asp:Literal></p></td>

            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="Name" DataValueField="LanguageGUID"
                    OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged"  AutoPostBack="true" CssClass="listmenu-default">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Name %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal4" runat="server" Text="<%$ resources:Subject %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Body %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine"  rows="15" CssClass="textfield-default"></asp:TextBox>
            </td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td></td>
            <td>
            <asp:Button ID="btnCancel" runat="server" Text="<%$ resources:Share,Cancel  %>" 
                    onclick="btnCancel_Click" CssClass="button-delete"/>
                <asp:Button ID="btnAdd" runat="server" Text="<%$ resources:Save %>" OnClick="btnAdd_Click" CssClass="button-update floatRight" />
                
                <%--<asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ resources:Share,Cancel %>"
                    NavigateUrl="Home.aspx"></asp:HyperLink>--%>
            </td>
        </tr>
    </table>
  </div>
  </div>
</asp:Content>
