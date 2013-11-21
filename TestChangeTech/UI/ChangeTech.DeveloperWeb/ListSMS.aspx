<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListSMS.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Sms list overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
  <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:45%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
    </tr>
    <tr align="center">
        <td><p class="name">Email:</p></td>
        <td>
            <asp:TextBox ID="emailTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
        </td>
      <td><asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" CssClass="button-update"/></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    <br />
    <asp:Repeater ID="smRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th>
                        User email
                    </th>
                    <th>
                        Mobile
                    </th>
                    <th>
                        Message
                    </th>
                    <th>
                        Send date
                    </th>
                    <th>
                        IsSent
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Email")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("MobilePhone")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Message")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("SendDate")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("IsSent")%></p>
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
