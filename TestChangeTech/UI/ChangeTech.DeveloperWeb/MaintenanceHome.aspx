<%@ Page Title="" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="MaintenanceHome.aspx.cs" Inherits="ChangeTech.DeveloperWeb.MaintenanceHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
  <h1>Maintenance home overview</h1>
  <div class="headermenu">
        <asp:Button ID="addButton" runat="server" CssClass="button-add" OnClick="addButton_Click" Text="Add new" />
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table >
    <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:50%;">&nbsp;</td>
      <td style="width:20%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Program</p>
            </td>
            <td>
                <asp:DropDownList ID="programDropDownList" runat="server" CssClass="listmenu-large" AutoPostBack="true" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td >&nbsp;</td>
            </tr>
            <tr>
            <td>
                <p class="name">Language</p>
            </td>
            <td>
                <asp:DropDownList ID="languageDropDownList" runat="server" CssClass="listmenu-default" AutoPostBack="true" OnSelectedIndexChanged="languageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
    <asp:Repeater ID="companyRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table >
                <tr>
                    <th>
                        Company name
                    </th>
                    <th>
                        Start date
                    </th>
                    <th>
                        Overdue date
                    </th>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                   <p class="name"> <%#Eval("CompanyName")%></p>
                </td>
                <td>
                    <p class="description"><%#Convert.ToDateTime(Eval("StartTime")).ToString("yyyy-MM-dd")%></p>
                </td>
                <td>
                   <p class="description"> <%#Convert.ToDateTime(Eval("OverDueTime")).ToString("yyyy-MM-dd")%></p>
                </td>
                <td align="right" style="width:100px;">
                    <asp:Button ID="editButton" runat="server" Text="Edit" OnClick="editButton_Click" CssClass="button-open"
                        CommandArgument='<%#Eval("CompanyRightGUID") %>' />
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" CssClass="button-delete" OnClientClick="return confirm('Are you sure?')"
                        OnClick="deleteButton_Click" CommandArgument='<%#Eval("CompanyRightGUID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
            <div class="pagenav">
            <%# PagingString %>
            <div class="clear"></div>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <div class="box-main">
     <table >
    <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:50%;">&nbsp;</td>
      <td style="width:20%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">User Email</p>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
            <td >&nbsp;</td>
            </tr>
            <tr>
            <td>
                <p class="name">Mobile Number</p>
            </td>
            <td>
                <asp:TextBox ID="txtMobile" CssClass="textfield-largetext" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnUser" runat="server" Text="Show User" CssClass="button-update"  onclick="btnUser_Click" />
            </td>
            <td >&nbsp;</td>
        </tr>
    </table>
    </div>
    <br />
    <div class="box-main">
    <table>
         <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:50%;">&nbsp;</td>
      <td style="width:20%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Users not registered on company</p>
            </td>
            <td align="right">
                <asp:Button ID="showButton" runat="server" CssClass="button-update" OnClick="showButton_Click" Text="Show" />
            </td>
            <td >&nbsp;</td>
        </tr>
        <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    </table>
    </div>
</asp:Content>
