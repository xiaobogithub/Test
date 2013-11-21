<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Preview.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>
        <b>
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></b></h4>
    <asp:Repeater ID="repeaterSession" runat="server" OnItemDataBound="repeaterSession_ItemDataBound">
        <HeaderTemplate>
            <table>
                <thead>
                    <tr>
                        <th>
                            <asp:Literal runat="server" Text="<%$ Resources:Day %>"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal runat="server" Text="<%$ Resources:Name %>"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal runat="server" Text="<%$ Resources:Share, PageSequence%>"></asp:Literal>
                        </th>
                        <th>
                            <asp:Literal runat="server" Text="<%$ Resources:Action %>"></asp:Literal>
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("Day")%>
                </td>
                <td>
                    <%#Eval("Name")%>
                </td>
                <td>
                    <%#Eval("PageSequenceNumber")%>
                </td>
                <td>
                    <asp:Button ID="btnPreview" runat="server" Text="<%$ Resources:Preview %>" CommandArgument='<%#Eval ("ID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tboby>
            </table>
            <%=PagingString %>
        </FooterTemplate>
    </asp:Repeater>

    <script type="text/javascript">
        function openPage(url) {
            window.open(url);
            return false;
        }
    </script>

</asp:Content>
