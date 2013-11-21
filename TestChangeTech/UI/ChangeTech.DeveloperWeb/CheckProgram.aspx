<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckProgram.aspx.cs"
    Inherits="ChangeTech.DeveloperWeb.CheckProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="header">
    <h1>Check program overview</h1>
        <div class="headermenu"></div>
        <div class="clear"></div>
    </div>
    <br />
    <asp:Label ID="programLabel" runat="server" Font-Bold="true" Font-Size="Large" Text=""></asp:Label><br /><br />
    <div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    <tr>
      <td style="text-align:center;">
            <asp:Button ID="checkExpressionButton" CssClass="button-update" runat="server" Text="Check expression" OnClick="checkExpressionButton_Click"/>
      </td>
      <td style="text-align:center;">
             <asp:Button ID="checkPagevariableButton" CssClass="button-update" runat="server" Text="Check pagevariable" OnClick="checkPagevariableButton_Click"/>
      </td>
      <td style="text-align:center;">
            <asp:Button ID="checkSettingButton" CssClass="button-update" runat="server" Text="Check setting"  OnClick="checkSettingButton_Click" />
      </td>
    </tr>
    </table>
    </div>
    <br />
    <asp:Label ID="tipLabel" runat="server"  Font-Bold="true" Font-Size="Large" Text=""></asp:Label><br /><br />
    <div style="text-align:center">
    <asp:ListBox ID="resultListBox" runat="server" Height="400px" Width="600px"></asp:ListBox>    
    </div>
</asp:Content>
