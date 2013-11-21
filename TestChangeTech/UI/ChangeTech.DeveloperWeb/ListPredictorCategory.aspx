<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListPredictorCategory.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListPredictorCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h4>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Title %>"></asp:Literal></h4>--%>
<div class="header">
  <h1>List Predictor Category </h1>
  <div class="headermenu">
        <asp:Button ID="btnAdd" runat="server" CssClass="button-add" Text="<%$ Resources:AddPredictorCategory %>" OnClick="btnAdd_Click" Enabled="false" />
  </div>
  <div class="clear"></div>
</div>
    <asp:Repeater ID="rpPredictorCategory" runat="server" OnItemDataBound="rpPredictorCategory_ItemDataBound">
        <HeaderTemplate>
          <div class="list">
            <table>
                <tr>
                    <%--<th>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal>
                    </th>
                    <th>                       
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal>
                    </th>        --%>
                    <%# HeaderString %>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval ("CategoryName")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval ("CategoryDescription")%> </p>
                </td>
                <td>
                    <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Share,Edit %>" Enabled="false"
                        OnClick="btnEdit_Click" CommandArgument='<%#Eval ("CategoryID") %>' CssClass="button-open"/>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share,Delete %>" Enabled="false"
                        OnClientClick="return confirm('Are you sure?')" OnClick="btnDelete_Click" CommandArgument='<%#Eval ("CategoryID") %>' CssClass="button-delete" />
                    </div>
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
