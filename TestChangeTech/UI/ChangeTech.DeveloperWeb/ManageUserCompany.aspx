<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageUserCompany.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageUserCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>
            Manage user company overview</h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <asp:Repeater ID="joinedCompanyRepeater" runat="server">
        <HeaderTemplate>
            <div class="list">
                <table style="vertical-align: top">
                    <tr>
                        <th>
                            Company name
                        </th>
                        <th>
                            Description
                        </th>
                        <th>
                            Overdue time
                        </th>
                        <th>
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name">
                        <%#Eval("CompanyName")%></p>
                </td>
                <td>
                    <p class="description">
                        <%#Eval("ComayDescription")%></p>
                </td>
                <td>
                    <p class="description">
                        <%#Convert.ToDateTime(Eval("OverDueTime")).ToString("yyyy-MM-dd")%></p>
                </td>
                <td style="width: 150px">
                    <div class="buttons">
                        <asp:Button ID="editButton" runat="server" CssClass="button-open" Text="Edit" CommandArgument='<%#Eval("CompanyRightGUID") %>'
                            OnClick="editButton_Click" />
                        <asp:Button ID="deleteButton" runat="server" Text="Delete" CssClass="button-delete"
                            CommandArgument='<%#Eval("CompanyRightGUID") %>' OnClientClick="return confirm('Are you sure?');"
                            OnClick="deleteButton_Click" />
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table> </div>
        </FooterTemplate>
    </asp:Repeater>
    <br />
    <br />
    <div class="header">
        <h1>
        </h1>
        <div class="headermenu">
            <asp:Button ID="addCompanyButton" runat="server" CssClass="button-add" Text="register company"
                OnClick="addCompanyButton_Click" /></div>
        <div class="clear">
        </div>
    </div>
    <asp:Repeater ID="NotJoinRepeater" runat="server">
        <HeaderTemplate>
            <div class="list">
                <table>
                    <tr>
                        <th>
                            Comany name
                        </th>
                        <th>
                            Description
                        </th>
                        <th>
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name">
                        <%#Eval("Name")%></p>
                </td>
                <td>
                    <p class="description">
                        <%#Eval("Description")%></p>
                </td>
                <td>
                    <div class="buttons">
                        <asp:Button ID="addButton" runat="server" Text="Add" CssClass="button-update" OnClick="addButton_Click"
                            CommandArgument='<%#Eval("CompanyGUID")%>' />
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table> </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
