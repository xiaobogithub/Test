<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="ManageCopyTipMessage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageCopyTipMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage copy tip message overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
        <tr>
            <td style="width:35%;">&nbsp;</td>
            <td style="width:35%;">&nbsp;</td>
            <td style="width:30%;">&nbsp;</td>
        </tr>
        <tr>
            <td><p class="name">Program Language:</p></td>
            <td><asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="True"  CssClass="listmenu-default"  onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                </asp:DropDownList>
             </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td><p class="name">Copy From Program:</p></td>
            <td><asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True"  CssClass="listmenu-large" onselectedindexchanged="ddlProgram_SelectedIndexChanged" >
                </asp:DropDownList>
             </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><p class="divider">&nbsp;</p></td>
            <td><p class="divider">&nbsp;</p></td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnCopy" runat="server" Text="Copy"  CssClass="button-update" onclick="btnCopy_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>  
    </div>
    </div>
</asp:Content>
