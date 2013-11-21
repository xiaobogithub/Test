<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UpdatePassword.aspx.cs" Inherits="ChangeTech.DeveloperWeb.UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>
            Update Password
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
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
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: OldPassword %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Required" ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: NewPassword %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Required" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: ConfirmPassword %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textfield-largetext"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="Not the same with new password" ControlToCompare="txtNewPassword" 
                    ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
        <td><p class="divider">&nbsp;</p></td>
        <td><p class="divider">&nbsp;</p></td>
        <td></td>
        </tr>
        <tr>
        <td></td>
            <td>
             <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources: Share,Cancel %>"  CausesValidation="false" onclick="btnCancle_Click"  CssClass="button-delete"/>
                <asp:Button ID="btnUpdate" runat="server" 
                    Text="<%$ Resources: Share, Update %>" onclick="btnUpdate_Click" CssClass="button-update  floatRight" />
                
            </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>
