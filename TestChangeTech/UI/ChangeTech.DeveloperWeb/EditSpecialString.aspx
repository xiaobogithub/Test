<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditSpecialString.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditSpecialString" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
  <h1>Edit SpecialString</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
    <td width="35%">&nbsp;</td>
      <td width="30%">&nbsp;</td>
      <td width="35%">&nbsp;</td>
      </tr>
        <tr>
            <td>
                <p class="name">Language</p>
            </td>
            <td>
                <asp:Label ID="languageLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">String name</p>
            </td>
            <td>
                <asp:Label ID="stringNameLabel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
               <p class="name">Value</p>
            </td>
            <td>
                <asp:TextBox ID="valueTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td></td>
            <td>
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" 
                    onclick="cancelButton_Click" CssClass="button-delete" />
                <asp:Button ID="okButton" runat="server" Text="OK" onclick="okButton_Click"  CssClass="button-update floatRight"/>
                
            </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>
