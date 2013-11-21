<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManagePaymentOrder.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManagePaymentOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="header">
  <h1>Manage Payment Order </h1>
  <div class="headermenu">
    
  </div>
  <div class="clear"></div>
</div>    
<div class="box-main">
    <table>
    <tr>
     <td width="35%">&nbsp;</td>
      <td width="30%">&nbsp;</td>
      <td width="35%">&nbsp;</td>
      </tr>
        <tr>
            <td>
                <p class="name">Program</p>
            </td>
            <td>
                <asp:DropDownList ID="programDropDownList" runat="server" AutoPostBack="true"
                    onselectedindexchanged="programDropDownList_SelectedIndexChanged" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">Language</p>
            </td>
            <td>
                <asp:DropDownList ID="languageDropDownList" runat="server" AutoPostBack="true"
                    onselectedindexchanged="languageDropDownList_SelectedIndexChanged" CssClass="listmenu-default">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
    <h1>&nbsp;</h1>
    <asp:Repeater ID="orderRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <td>
                        Order Id
                    </td>
                    <td>
                        User Email
                    </td>
                    <td>
                        Amount
                    </td>
                    <td>
                        Currency Code
                    </td>
                    <td>
                        Transaction Id
                    </td>
                    <td>
                        Paid time
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("OrderID")%></p>
                </td>
                <td>
                    <p class="user"><%#Eval("UserEmail")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("Amount")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("CurrencyCode")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("TransationID")%></p>
                </td>
                <td>
                    <p class="counter"><%#Eval("PayTime")%></p>
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
