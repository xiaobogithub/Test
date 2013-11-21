<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddLanguage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddLanguage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Add language overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
        <table>
            <tr>
                <td style="width:20%;">&nbsp;</td>
                <td style="width:45%;">&nbsp;</td>
                <td style="width:35%;">&nbsp;</td>
            </tr>
             <tr>
                <td><p class="name">Language name:</p></td>
                <td>
                    <asp:TextBox ID="languageNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="languageNameTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                    <asp:Button ID="addButton" runat="server" Text="Add" CssClass="button-update" OnClick="addButton_Click" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CssClass="button-delete" OnClick="cancelButton_Click" CausesValidation="False" />
                </td>
                <td>&nbsp;</td>
            </tr>
    </table>
    </div>
    </div>
</asp:Content>
