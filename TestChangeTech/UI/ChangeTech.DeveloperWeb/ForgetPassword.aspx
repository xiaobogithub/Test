<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ForgetPassword.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%" style="line-height:30px; margin-top:50px;">
        <tr>
            <td style="width:20%"></td>
            <td style="width:20%">
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Email %>"></asp:Literal>
            </td>
            <td style="width:40%">
                <asp:TextBox ID="txtUserEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtUserEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:MobilePhone %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtMobilePhone" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
        <td colspan = "3" align="center">
            <asp:Button ID="btnRetrievePassword" runat="server"  Width="150px"
                Text="<%$ Resources:GetPassword %>" onclick="btnRetrievePassword_Click" /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <asp:Label ID="MsgLbl" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
