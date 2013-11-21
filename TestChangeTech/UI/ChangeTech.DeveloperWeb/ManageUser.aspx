<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ManageUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage user overview</h1>
  <div class="headermenu">
        <asp:Button ID="addLinkButton" runat="server" Text="<%$Resources:Share,NewUser %>" CssClass="button-add" OnClick="addLinkButton_Click" />
  </div>
  <div class="clear"></div>
</div>
    <div class="box-main">
    <table>
    <tr>
      <td style="width:15%;">&nbsp;</td>
      <td style="width:50%;">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="queryTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Button ID="searchButton" runat="server" Text="Search" 
                    onclick="searchButton_Click"  CssClass="button-update" />
            </td>
        </tr>
    </table>
    </div>

    <asp:Repeater ID="usersRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <%=HeaderString %>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:LinkButton ID="userLinkButton" runat="server" CssClass="name" Text='<%#Eval ("UserName") %>'
                        CommandArgument='<%#Eval("UserGuid") %>' OnClick="userLinkButton_Click"></asp:LinkButton>
                </td>
                <td>
                    <p class="description"><%#Eval ("FirstName") %></p>
                </td>
                <td>
                    <p class="description"><%#Eval ("LastName") %></p>
                </td>
                <td>
                   <p class="description"> <%#Eval ("PhoneNumber") %></p>
                </td>
                <td>
                    <p class="description"><%#Eval ("Gender") %></p>
                </td>
                <td>
                   <p class="description"> <%#Eval("UserType") %></p>
                    <%--<%# Enum.GetName(typeof(ChangeTech.Models.UserTypeEnum), Eval("UserType"))%>--%>
                </td>
                <td>
                    <asp:Button ID="deleteButton" runat="server" CommandArgument='<%#Eval ("UserGuid") %>'
                        Text="<%$Resources:Share,Delete %>" CssClass="button-delete" OnClientClick='return confirm("Are you sure?");'
                        OnClick="deleteButton_click" />
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
</asp:Content>
