<%@ Page Title="" Language="C#" MasterPageFile="~/OrderSystem/OrderSystem.Master"
    AutoEventWireup="true" CodeBehind="ListOrder.aspx.cs" Inherits="ChangeTech.DeveloperWeb.OrderSystem.ListOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Order list overview</h1>
  <div class="headermenu">
         <asp:Button ID="btnAddNewOrder" CssClass="button-add" runat="server" Text="<%$ Resources: NewOrder %>" OnClick="btnAddNewOrder_Click" />
  </div>
  <div class="clear"></div>
</div>   
    <asp:Repeater ID="rpOrderList" runat="server" OnItemDataBound="rpOrderList_ItemDataBound" OnItemCommand="rpOrderList_ItemCommand">
        <%--Header--%>
        <HeaderTemplate>
            <div class="list">
            <table>
                <tr>
                    <th>
                        Created
                    </th>
                    <th>
                        Customer
                    </th>
                    <th>
                        Licences
                    </th>
                    <th>
                        UsedLicence
                    </th>
                    <%--<th>
                        OrderStatus
                    </th>--%>
                    <th style="width:10%">
                    </th>
                </tr>
        </HeaderTemplate>
        <%--Item--%>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="user"><%#Convert.ToDateTime(Eval("Created")).ToString("dd.MM.yyyy")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("CustomerName")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("OrderLicences")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("UsedLicences")%></p>
                </td><%--
                <td>
                    <%#Eval("OrderStatus")%>
                </td>--%>
                <td>
                    <div class="buttons">
                    <asp:Button ID="btnMoreInfo" runat="server"  CssClass="button-open" Text="<%$ Resources: MoreInfo %>" CommandArgument='<%#Eval("OrderGUID")%>' OnClick="btnMoreInfo_Click" /><br />
                    <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order"  CssClass="button-open" CommandArgument='<%#Eval("OrderGUID")%>' OnClick="btnCancelOrder_Click"/><br />
                        <asp:Button ID="btnCancelledOrder" runat="server" Text="Cancelled" CssClass="button-delete" CommandArgument='<%#Eval("OrderGUID")%>' Visible="false" /><br />
                    <asp:Button ID="btnExpiredOrder" runat="server" Text="Expired" CssClass="button-delete" CommandArgument='<%#Eval("OrderGUID")%>' Visible="false" />
                         </div>
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
