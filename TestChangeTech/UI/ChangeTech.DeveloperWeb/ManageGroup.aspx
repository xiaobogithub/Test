<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageGroup.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="groupRepeater" runat="server" OnItemDataBound="groupRepeater_ItemDataBound">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Discription
                    </th>
                    <th>
                        URL
                    </th>
                    <th>
                        <asp:Button ID="addButton" runat="server" Text="Add new" OnClick="addButton_Click" />
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("Name")%>
                </td>
                <td>
                    <%#Eval("Description")%>
                </td>
                <td>
                    Screen url:<asp:Label ID="surlLabel" runat="server" Text=""></asp:Label><br />
                    Login url:<asp:Label ID="lurlLabel" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Button ID="editButton" runat="server" Text="Edit" CommandArgument='<%#Eval("GroupGUID")%>'
                        OnClick="editButton_Click" />
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" CommandArgument='<%#Eval("GroupGUID")%>'
                        OnClientClick="return confirm('Are you sure?');" OnClick="deleteButton_Click" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
