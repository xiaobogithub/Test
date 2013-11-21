<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageSM.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage sms overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
  <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:45%;">&nbsp;</td>
      <td style="width:25%;">&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Short message type:</p></td>
        <td>
             <asp:Label ID="messageTypeLabel" runat="server" Font-Bold="true" Font-Size="Medium" Text="PinCode"></asp:Label>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Text:</p></td>
        <td>
             <asp:TextBox ID="textTextBox" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="5"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
     <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
             <asp:Button ID="okButton" runat="server" Text="OK" onclick="okButton_Click" CssClass="button-update"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" onclick="cancelButton_Click" CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
        <td colspan="3"><p class="name">IE. if the text is " your pinCode is : {0}", the end-user would receive, "your PinCode is:[user's real PinCode]", so {0} represent PinCode.</p></td>
    </tr>
    </table>
    </div>
     </div>
    
   
</asp:Content>
