<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListProgramRoom.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListProgramRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>program rooms overview</h1>
  <div class="headermenu">  
        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:AddRoom %>"  CssClass="button-add" OnClick="btnAdd_Click" Enabled="false" />
  </div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
    <asp:Repeater ID="rpRoom" runat="server" OnItemDataBound="rpRoom_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <%# HeaderString %>
                    <th>&nbsp;</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width:30%">
                   <p class="name"> <%#Eval ("Name") %></p>
                </td>
                <td style="width:60%">
                    <p class="description"><%#Eval("Description")%>
                </td>
                <td align="right" style="width:10%">
                <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" CssClass="button-open" Text="<%$ Resources:Share,Edit %>" CommandArgument='<%#Eval ("ProgramRoomGuid") %>'
                        Enabled="false" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="button-delete" Text="<%$ Resources:Share,Delete %>" CommandArgument='<%#Eval ("ProgramRoomGuid") %>'
                        Enabled="false" OnClientClick="return confirm('Are you sure?')" OnClick="btnDelete_Click" />
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
    </div>
</asp:Content>
