<%@ Page Title="" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="UsersByEmailOrMobile.aspx.cs" Inherits="ChangeTech.DeveloperWeb.UsersByEmailOrMobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Search users overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
    <div class="box-main">
    <table>
        <tr>
            <td style="width:30%;">&nbsp;</td>
            <td style="width:50%;">&nbsp;</td>
            <td style="width:20%">&nbsp;</td>
        </tr>
        <tr>
            <td>
                    <p class="name"><asp:Literal ID="ltlProgram" runat="server"></asp:Literal></p>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">User Email</p>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">Mobile Number</p>
            </td>
                <td>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td><p class="divider">&nbsp;</p></td>
                <td><p class="divider">&nbsp;</p></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnUser" runat="server" Text="Show User" CssClass="button-open" OnClick="btnUser_Click" />
                </td>
            </tr>
    </table>
    </div>

    <asp:Repeater ID="CompanyUserRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tbody>
                    <tr>
                        <th style="width: 150px">
                            <asp:Literal ID="Literal2" Text="<%$ Resources:Email%>" runat="server"></asp:Literal>
                        </th>
                        <th style="width: 100px">
                            <asp:Literal ID="Literal3" Text="<%$ Resources:Phone%>" runat="server"></asp:Literal>
                        </th>
                        <th style="width: 100px">
                            <asp:Literal ID="Literal4" Text="<%$ Resources:Status%>" runat="server" />
                        </th>
                        <th style="width: 100px">
                            <asp:Literal ID="Literal1" Text="<%$ Resources:RegisterDate%>" runat="server" />
                        </th>
                        <th style="width: 100px">
                            <asp:Literal ID="Literal7" Text="<%$ Resources:CurrentDay%>" runat="server" />
                        </th>
                        <th style="width: 50px">
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Email")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("MobilePhone")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("Status")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("RegisterDate")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("CurrentDay")%></p>
                </td>
                <td >
                    <asp:Button ID="EditUserButton" runat="server" Text="<%$ Resources:Edit%>"  CssClass="button-open"
                        CommandArgument='<%#Eval("ProgramUserGUID")%>' OnClick="EditUser_Click" />
                    <asp:Button ID="DeleteUserButton" runat="server" Text="<%$ Resources:Delete%>"  CssClass="button-delete"
                        CommandArgument='<%#Eval("ProgramUserGUID")%>' OnClick="DeleteUser_Click" OnClientClick="return confirm('Do you confirm to delete this user?');" />
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
