<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageUserMenu.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageUserMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="header">
        <h1>Manage help item overview</h1>
        <div class="headermenu">
        </div>
        <div class="clear"></div>
    </div>   
    <asp:Label ID="programLabel" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
    <br />
    <asp:LinkButton ID="addUesrMenuLinkButton" runat="server" Font-Bold="true" Font-Size="Medium" OnClick="addUesrMenuLinkButton_Click">Add user menu</asp:LinkButton>
    
    <asp:Repeater ID="menuRepeater" runat="server" OnItemDataBound="menuRepeater_ItemDataBind">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Name %>" />
                    </th>
                    <th>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Text %>" />
                    </th>
                    <th>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:FormTitle %>" />
                    </th>
                    <th>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:FormText %>" />
                    </th>
                    <th>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:FormBackButtonText %>" />
                    </th>
                    <th>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:FormSubmitButtonText %>" />
                    </th>
                    <th>
                    </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr>
            <td>
                <p class="name"><%#Eval("Name")%></p>
            </td>
            <td>
                <p class="description"><%#Eval("Text")%></p>
            </td>
            <td>
                <p class="description"><%#Eval("FormTitle")%></p>
            </td>
            <td>
                <p class="description"><%#Eval("FormText")%></p>
            </td>
            <td>
                <p class="description"><%#Eval("FormBackButtonText")%></p>
            </td>
            <td>
                <p class="description"><%#Eval("FormSubmitButtonText")%></p>
            </td>
            <td style="width:10%">
                <div class="buttons">
                <asp:Button runat="server" ID="EditButton" Text="<%$Resources:Share,Edit %>" CssClass="button-open" CommandArgument='<%#Eval ("MenuItemGUID") %>'
                    OnClick="EditButton_Click" />
                <asp:Button ID="availableButton" runat="server" Text="Button" CssClass="button-open" CommandArgument='<%#Eval ("MenuItemGUID") %>'
                    OnClick="availableButton_Click" />
                </div>
            </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Label runat="server" ID="msgLbl" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label>
</asp:Content>
