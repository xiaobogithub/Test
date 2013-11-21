<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAdminForApplication.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddAdminForApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Repeater ID="rpUserList" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <%--<th>
                        <asp:Literal ID="Literal1" runat="server" Text = "<%$Resources: Email %>"></asp:Literal>
                    </th>
                    <th>
                        <asp:Literal ID="Literal2" runat="server" Text = "<%$Resources: FirstName %>"></asp:Literal>
                    </th>
                    <th>
                        <asp:Literal ID="Literal3" runat="server" Text = "<%$Resources: LastName %>"></asp:Literal>
                    </th>--%>
                    <%=HeaderString %>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("UserName")%>
                </td>
                <td>
                    <%#Eval("FirstName")%>
                </td>
                <td>
                    <%#Eval("LastName")%>
                </td>
                <td>
                    <asp:Button ID="btnAddUser" runat="server" Text="<%$Resources:Share,Select %>" CommandArgument='<%#Eval ("UserGuid") %>' OnClick = "btnAddUser_click" />
                </td>
                <tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            <%=PagingString %>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
