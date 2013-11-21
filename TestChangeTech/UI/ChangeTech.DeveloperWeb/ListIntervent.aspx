<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListIntervent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListIntervent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h4>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Title %>"></asp:Literal></h4>--%>
    
    <div class="header">
  <h1>List Intervent </h1>
  <div class="headermenu">
        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:AddIntervent %>" OnClick="btnAdd_Click" Enabled="false" CssClass="button-add" />
  </div>
  <div class="clear"></div>
</div>    
<div class="box-main">
    <table style="height:60px">
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,InterventCategory %>"></asp:Literal></p>
            </td>
            <td style="padding:20px 0px 20px 0px;">
                <asp:DropDownList ID="ddlInterventCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIntervent_SelectedIndexChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </div>
    <h1>&nbsp;</h1>
    <asp:Repeater ID="rpIntervent" runat="server" OnItemDataBound="rpIntervent_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <%--<td>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Share,Intervent %>"></asp:Literal>
                    </td>--%>
                    <%#HeaderString %>
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("InterventName")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Description")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("InterventCategoryName")%></p>
                </td>
                <td style="width:10%;">
                <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:Share,Edit %>" CommandArgument='<%#Eval ("InterventGUID") %>'
                        OnClick="btnEdit_Click" Enabled="false" CssClass="button-open" />
                <asp:Button ID="mergeButton" runat="server" Text="Merge with.." CommandArgument='<%#Eval ("InterventGUID") %>'
                        OnClick="mergeButton_Click" Enabled="false" CssClass="button-open" />
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share,Delete %>" CommandArgument='<%#Eval ("InterventGUID") %>'
                        Enabled="false" OnClientClick="return confirm('Do you confirm you want to delete this intervent?')" OnClick="btnDelete_Click" CssClass="button-delete" />
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
