<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profile.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>
            Profile
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <div style="height: 580px;">
        <div class="box-main">
            <table>
                <tr>
                    <td style="width: 30%;">
                        &nbsp;
                    </td>
                    <td style="width: 45%;">
                        &nbsp;
                    </td>
                    <td style="width: 25%;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="name">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: UserName %>"></asp:Literal></p>
                    </td>
                    <td>
                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="name">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: Gender %>"></asp:Literal></p>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="name">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: MobilePhone %>"></asp:Literal></p>
                    </td>
                    <td>
                        <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="name">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources: AuthorityInformation %>"></asp:Literal></p>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkSuperAdmin" runat="server" Enabled="false" Text="<%$ Resources: SuperAdmin %>" />
                        <asp:CheckBox ID="chkAdmin" runat="server" Enabled="false" Text="<%$ Resources: Admin %>" />
                        <asp:CheckBox ID="chkCreate" runat="server" Enabled="false" Text="<%$ Resources: Create %>" />
                        <asp:CheckBox ID="chkEdit" runat="server" Enabled="false" Text="<%$ Resources: Edit %>" />
                        <asp:CheckBox ID="chkDelete" runat="server" Enabled="false" Text="<%$ Resources: Delete %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <p class="divider">
                            &nbsp;</p>
                    </td>
                    <td>
                        <p class="divider">
                            &nbsp;</p>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnChangePwd" runat="server" Text="Change Pwd" OnClick="btnChangePwd_Click"
                            CssClass="button-update" />
                        <asp:Button ID="btnEditProfile" runat="server" Text="<%$ Resources: Share,EditProfile %>"
                            OnClick="btnEditProfile_Click" CssClass="button-update" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
