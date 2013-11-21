<%@ Page Title="" Language="C#" MasterPageFile="~/HealthProfileSystem/HealthProfileSystem.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.HealthProfileSystem.MainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="header">
  <h1>Order list overview</h1>
  <div class="headermenu">
        <p style="float: right;">
            <asp:Button ID="btnAddNewOrder"  CssClass="button-add" runat="server" Text="NewOrder" OnClick="btnAddNewOrder_Click" />
        </p>
  </div>
  <div class="clear"></div>
</div>  


    <asp:Repeater ID="rpHPOrderList" runat="server"  onitemcommand="rpHPOrderList_ItemCommand"  onitemdatabound="rpHPOrderList_ItemDataBound">
    <%--Header--%>
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th style="width:7%;">
                        Created
                    </th>
                    <th style="width:10%;">
                        Customer
                    </th>
                    <th style="width:30%">
                        Link to startpage
                    </th>
                    <th style="width:5%;">
                        Code
                    </th>
                    <th style="width:5%;">
                        Users
                    </th>
                    <th style="width:5%;">
                        Used
                    </th>
                    <th style="width:7%;">
                        Start date
                    </th>
                    <th style="width:7%;">
                        Stop date
                    </th>
                    <th></th>
                    <th></th>
                    <th style="width:8%;">
                        Operating
                    </th>
                </tr>
        </HeaderTemplate>
        <%--Item--%>
        <ItemTemplate>
            <tr>
                <td  align="left">
                    <%#Convert.ToDateTime(Eval("Created")).ToString("dd.MM.yyyy")%>
                </td>
                <td align="left">
                    <%#Eval("CustomerName")%>
                </td>
                <td align="left">
                    <%#Eval("LinkToStartPageURL")%>
                </td>
                 <td align="right">
                    <%#Eval("Code")%>
                </td>
                <td align="right">
                    <%#Eval("NumberOfEmployees")%>
                </td>
                <td  align="right">
                    <%#Eval("UsedLicence")%>
                </td>
                <td align="left">
                    <%#Convert.ToDateTime(Eval("StartDate")).ToString("dd.MM.yyyy")%>
                </td>
                <td align="left">
                    <%#Convert.ToDateTime(Eval("StopDate")).ToString("dd.MM.yyyy")%>
                </td>
                <td></td><td></td>
                <td align="center">
                    <%--<asp:Button ID="btnMoreInfo" runat="server" Text="<%$ Resources: MoreInfo %>" CommandArgument='<%#Eval("OrderGUID")%>'
                        OnClick="btnMoreInfo_Click" />--%>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit"  CssClass="button-open" OnClick="btnEdit_Click" CommandArgument='<%#Eval("HPOrderGUID") %>' />
                    <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order"  CssClass="button-delete" OnClick="btnCancelOrder_Click" CommandArgument='<%#Eval("HPOrderGUID") %>' />
                    <asp:Button ID="btnCancelledOrder" runat="server" Text="Cancelled" CssClass="button-delete" Visible="false" CommandArgument='<%#Eval("HPOrderGUID") %>' />
                    <asp:Button ID="btnExpiredOrder" runat="server" Text="Expired" CssClass="button-delete" Visible="false" CommandArgument='<%#Eval("HPOrderGUID") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <%--Footer--%>
        <FooterTemplate>
            </table>
            </div>
            <%--<%=PagingString %>--%>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
