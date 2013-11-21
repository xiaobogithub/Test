<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true" CodeBehind="ListMissedClassUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListMissedClassUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>MissedClassUser list overview </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
    <asp:Repeater ID="rpMissedClassToday" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Gender
                    </th>
                    <th>
                        MobilePhone
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("UserName")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("Gender")%></p>
                </td>
                <td style="width:15%">
                   <p class="counter"> <%#Eval("PhoneNumber")%></p>
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
