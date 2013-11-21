<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditHelpItem.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditHelpItem" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit help item overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:25%">&nbsp;</td>
      <td style="width:40%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Order : </p>
            </td>
            <td>
                <asp:DropDownList ID="OrderDropDownList" runat="server" CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Question : </p>
            </td>
            <td>
                <asp:TextBox ID="QuestionTextBox" runat="server" TextMode="MultiLine" CssClass="textfield-largetext" Rows="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="QuestionTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name">Answer:</p>
            </td>
            <td>
                <asp:TextBox ID="AnswerTextBox" runat="server" TextMode="MultiLine"  CssClass="textfield-largetext" Rows="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AnswerTextBox"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
        <td>&nbsp;</td>
            <td align="right">
                <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click"  CssClass="button-update"/>
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
</div>
</asp:Content>
