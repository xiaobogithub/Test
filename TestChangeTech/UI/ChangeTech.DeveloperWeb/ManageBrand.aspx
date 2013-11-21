<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
 CodeBehind="ManageBrand.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageBrand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="header">
  <h1>Manage Brands </h1>
  <div class="headermenu">
    <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="Add brand" CssClass="button-add" />
  </div>
  <div class="clear"></div>
</div>   

<asp:Repeater ID="brandRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th style="width:40%">
                        Brand name
                    </th>
                    <th>
                        Brand URL
                    </th>
                    <th style="width:10%">
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("BrandName")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("BrandURL")%></p>
                </td>
                <td align="right">
                    <asp:Button ID="editButton" runat="server" Text="Open" CommandArgument='<%#Eval("BrandGUID") %>' OnClick="editButton_Click" CssClass="button-open" />
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" CommandArgument='<%#Eval("BrandGUID") %>' OnClick='deleteButton_Click' CssClass="button-delete" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
