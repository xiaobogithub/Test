<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageLoginTemplate.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageLoginTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
    <div class="header">
  <h1>Manage session ending overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<p>&nbsp;</p>
<b><asp:Label ID="programLabel" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label></b>
<p>&nbsp;</p>
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
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="Heading"></asp:Literal> : </p>
            </td>
            <td>
             <asp:TextBox ID="txtHeading" runat="server"  CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="Text"></asp:Literal> : </p>
            </td>
            <td>
                 <asp:TextBox ID="txtText" runat="server" Rows="5" TextMode="MultiLine" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="UserNameText"></asp:Literal> : </p>
            </td>
            <td>
                 <asp:TextBox ID="txtUserNameText" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

         <tr>
            <td>
                <p class="name"> <asp:Literal ID="Literal4" runat="server" Text="PasswordText"></asp:Literal> : </p>
            </td>
            <td>
                 <asp:TextBox ID="txtPasswordText" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal5" runat="server" Text="PrimaryButtonText"></asp:Literal> : </p>
            </td>
            <td>
                 <asp:TextBox ID="txtPrimaryButtonText" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal6" runat="server" Text="ForgetPasswordText"></asp:Literal> : </p>
            </td>
            <td>
                  <asp:TextBox ID="txtSecondaryButtonText" runat="server" Rows="3" TextMode="MultiLine" CssClass="textfield-largetext"></asp:TextBox>
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
             <asp:Button ID="btnOK" runat="server" Text="OK" onclick="btnOK_Click"  CssClass="button-update" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
</div>
</asp:Content>
