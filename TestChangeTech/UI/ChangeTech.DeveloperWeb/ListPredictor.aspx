<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListPredictor.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListPredictor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h4>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Title %>"></asp:Literal></h4>--%>
<div class="header">
  <h1>List Predictor </h1>
  <div class="headermenu">
        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:AddPredictor %>"  OnClick="btnAdd_Click" Enabled="false" CssClass="button-add" />
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table style="height:60px">
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,PredictorCategory %>"></asp:Literal></p>
            </td>
            <td style="padding:20px 0px 20px 0px;">
                <asp:DropDownList ID="ddlPredictorCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPredictorCategory_SelectedIndexChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </div>
    <h1>&nbsp;</h1>
    <asp:Repeater ID="rpPredictor" runat="server" OnItemDataBound="rpPredictor_ItemDataBound1">
        <HeaderTemplate>
          <div class="list">
            <table>
                <tr>
                    <%-- <td>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Share,PredictorCategory %>"></asp:Literal>
                    </td>--%>
                    <%# HeaderString %>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval ("Name") %></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Description")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("CategoryName")%></p>
                </td>
                <td>
                    <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Share,Edit %>" CommandArgument='<%#Eval ("PredictorID") %>'
                        Enabled="false" OnClick="btnEdit_Click" CssClass="button-open" />
                 <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share,Delete %>" CommandArgument='<%#Eval ("PredictorID") %>'
                        Enabled="false" OnClientClick="return confirm('Are you sure?')" OnClick="btnDelete_Click" CssClass="button-delete" />
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
