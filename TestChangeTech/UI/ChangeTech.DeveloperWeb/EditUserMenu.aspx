<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditUserMenu.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditUserMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<div class="header">
  <h1>Edit user menu overview</h1>
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
                <p class="name"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:Name %>"></asp:Label> : </p>
            </td>
            <td>
                <asp:TextBox runat="server" ID="nameTxtBox" ReadOnly="true" Text="" CssClass="textfield-largetext"  Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:Text %>"></asp:Label> : </p>
            </td>
            <td>
                <asp:TextBox runat="server" ID="textTxtBox" Text="" CssClass="textfield-largetext" ></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:FormTitle %>"></asp:Label> : </p>
            </td>
            <td>
               <asp:TextBox runat="server" ID="formTitleTxtBox" Text="" CssClass="textfield-largetext" ></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"> <asp:Label ID="Label4" runat="server" Text="<%$ Resources:FormText %>"></asp:Label> : </p>
            </td>
            <td>
               <asp:TextBox runat="server" ID="formTextTxtBox" Text="" CssClass="textfield-largetext" ></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:FormBackButtonText %>"></asp:Label> : </p>
            </td>
            <td>
               <asp:TextBox runat="server" ID="backButtonTextTxtBox" Text="" CssClass="textfield-small" ></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:FormSubmitButtonText %>"></asp:Label> : </p>
            </td>
            <td>
               <asp:TextBox runat="server" ID="submitButtonTextTxtBox" Text="" CssClass="textfield-small" ></asp:TextBox>
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
                <asp:Button runat="server" ID="updateButton" Text="<%$ Resources:UpdateButtonText %>"  CssClass="button-update" OnClick="updateButton_Click" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3"><asp:Label runat="server" ID="msgLbl" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label></td>
        </tr>
        </table>
</div>
</div>
</asp:Content>
