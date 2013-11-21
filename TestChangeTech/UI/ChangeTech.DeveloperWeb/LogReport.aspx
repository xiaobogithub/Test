<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LogReport.aspx.cs" Inherits="ChangeTech.DeveloperWeb.LogReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Log Report overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table>
    <tr>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:60%;">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Program:</p>
            </td>
            <td>
                <asp:DropDownList ID="programDropDownList" CssClass="listmenu-large" runat="server" AutoPostBack="true" OnSelectedIndexChanged="programDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
        <tr>
            <td>
                <p class="name">Please check users</p>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:Repeater ID="userRepeater" runat="server">
                    <HeaderTemplate>
                    <div class="list">
                        <table>
                            <tr>
                                <th style="width:10%;">
                                </th>
                                <th>
                                    <p class="name">E-mail</p>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox ID="userCheckBox" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="emailLabel" runat="server" CssClass="name" Text='<%#Eval ("UserName")%>'></asp:Label>
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
            </td>
            <td style="vertical-align: top; padding-left:10px;">
            <div class="box-main" style="width:300px">
                <table style="width:200px">
                    <tr>
                        <td>
                            <p class="name">Please check fields</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="emailCheckBox" runat="server" Text="" /><label for="emailCheckBox" class="checkbox-default">Email accounts</label><br />
                            <asp:CheckBox ID="userStatusCheckBox" runat="server" Text="" /><label for="userStatusCheckBox" class="checkbox-default">User status</label><br />
                            <asp:CheckBox ID="userTypeCheckBox" runat="server" Text="" /><label for="userTypeCheckBox" class="checkbox-default">User type</label><br />
                            <asp:CheckBox ID="loginLogCheckBox" runat="server" Text="" /><label for="loginLogCheckBox" class="checkbox-default">Login</label><br />
                            <asp:CheckBox ID="startDayLogCheckBox" runat="server" Text="" /><label for="startDayLogCheckBox" class="checkbox-default">Start day</label><br />
                            <asp:CheckBox ID="endDayLogCheckBox" runat="server" Text="" /><label for="endDayLogCheckBox" class="checkbox-default">End day</label><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <p class="name">Please choose sessions</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="name">form:</span>
                            <asp:DropDownList ID="fromDropDownList" runat="server" CssClass="listmenu-small">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <span class="name">to:</span>
                            <asp:DropDownList ID="toDropDownList" runat="server" CssClass="listmenu-small">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="exportButton" runat="server" CssClass="button-update" Text="Export to excel" OnClick="exportButton_Click"/>
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
