<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListProgramStatus.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListProgramStatus"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h4>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Title %>"></asp:Literal></h4>--%>
    <asp:Repeater ID="rpProgramStatus" runat="server" OnItemDataBound="rpProgramStatus_ItemDataBound">
        <HeaderTemplate>
            <table>
                <tr>
                    <%#HeaderString %>
                    <th>
                    </th>
                    <th>
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:AddProgramStatus %>" Width="130px"
                            OnClick="btnAdd_Click" Enabled="false" />
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 150px">
                    <%#Eval ("Name") %>
                </td>
                <td style="width: 300px">
                    <%#Eval("Description")%>
                </td>
                <td>
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Share,Edit %>" CommandArgument='<%#Eval ("ID") %>'
                        OnClick="btnEdit_Click" Enabled="false" />
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share,Delete %>" CommandArgument='<%#Eval ("ID") %>'
                        Enabled="false" OnClientClick="return confirm('Are you sure?')" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            <%#PagingString %>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
