<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true" CodeBehind="ListLoginUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListLoginUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>LoginUser list overview </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
    <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Login5Minutes %>"></asp:Literal></p>
            </td>
            <td>
                <p class="name"><asp:Literal ID="ltlLogin5Minutes" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: LoginOneDay %>"></asp:Literal></p>
            </td>
            <td>
               <p class="name"><asp:Literal ID="ltlLoginOneDay" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"> <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: LoginOneWeek %>"></asp:Literal></p>
            </td>
            <td>
                 <p class="name"><asp:Literal ID="ltlLoginOneWeek" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: LoginOneMonth %>"></asp:Literal></p>
            </td>
            <td>
                 <p class="name"><asp:Literal ID="ltlLoginOneMonth" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        </table>
        </div>
        </div>
</asp:Content>
