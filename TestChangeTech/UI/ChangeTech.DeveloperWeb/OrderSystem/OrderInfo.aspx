<%@ Page Title="" Language="C#" MasterPageFile="~/OrderSystem/OrderSystem.Master"
    AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" 
    Inherits="ChangeTech.DeveloperWeb.OrderSystem.OrderInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Order info overview</h1>
  <div class="headermenu">
    <asp:Button ID="btnBack" runat="server" Text="<%$ Resources: Back %>" CssClass="button-open" OnClick="btnBack_Click" />
  </div>
  <div class="clear"></div>
</div>   
    <asp:Repeater ID="rpOrderInfo" runat="server">
        <%--Header--%>
        <HeaderTemplate>
            <div class="list">
            <table >
                <tr>
                    <th>
                        ProgramName
                    </th>
                    <th>
                        Licences
                    </th>
                    <th>
                        UsedLicence
                    </th>
                    <th>
                        LastRegisted
                    </th>
                </tr>
        </HeaderTemplate>
        <%--Item--%>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("ProgramName")%></p>
                </td>
                <td style="width:15%;">
                    <p class="counter"><%#Eval("Licences")%></p>
                </td>
                <td style="width:15%;">
                    <p class="counter"><%#Eval("UsedLicences")%></p>
                </td>
                <td style="width:15%;">
                    <p class="user"><%#Eval("LastRegisted")%></p>
                </td>
            </tr>
        </ItemTemplate>
        <%--Footer--%>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
