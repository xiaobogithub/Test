<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true" CodeBehind="ListRegisteredUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListRegisteredUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>RegisteredUser list overview </h1>
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
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: RegisteredToday %>"></asp:Literal></p>
            </td>
            <td>
                <p class="name"><asp:Literal ID="ltlRegisteredToday" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: RegisteredOneWeek %>"></asp:Literal></p>
            </td>
            <td>
               <p class="name"> <asp:Literal ID="ltlRegisteredOneWeek" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: RegisteredOneMonth %>"></asp:Literal></p>
            </td>
            <td>
                 <p class="name"><asp:Literal ID="ltlRegisteredOneMonth" runat="server"></asp:Literal></p>
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
