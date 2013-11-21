<%@ Page Title="Company User" Language="C#" MasterPageFile="~/3rdParty.Master" AutoEventWireup="true"
    CodeBehind="CompanyUsers.aspx.cs" Inherits="ChangeTech.DeveloperWeb.CompanyUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Company users overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>

    <asp:Repeater ID="CompanyUserRepeater" runat="server" onitemdatabound="CompanyUserRepeater_ItemDataBound">
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
                        <th style="width: 100px">

                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"> <%#Eval("Email")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("MobilePhone")%></p>
                </td>
                <td>
                    <p class="description"> <%#Eval("Status")%></p>
                </td>
                <td>
                   <p class="description">  <%#Eval("RegisterDate")%>     </p>               
                </td>
                <td>
                   <p class="description">  <%#Eval("CurrentDay")%></p>
                </td>
                <td align="right">
                    <asp:Button ID="EditUserButton" runat="server" Text="<%$ Resources:Edit%>" CssClass="button-open" CommandArgument='<%#Eval("ProgramUserGUID")%>' OnClick="EditUser_Click" />
                    <asp:Button ID="DeleteUserButton" runat="server" Text="<%$ Resources:Delete%>" CssClass="button-delete" CommandArgument='<%#Eval("ProgramUserGUID")%>' OnClick="DeleteUser_Click" OnClientClick="return confirm('Do you confirm to delete this user?');" />
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
