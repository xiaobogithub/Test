<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditProgramStatus.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditProgramStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<h4>
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Title %>"></asp:Literal>
    </h4>--%>
    <table>
        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txtProgramStatusName" runat="server" Width="200px"></asp:TextBox><span
                    style="color: Red">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramStatusName"
                    ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal>
            </td>
            <td>
                <asp:TextBox ID="txtProgramStatusDescription" runat="server" TextMode="MultiLine"
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Share,Update %>" Width="60px"
                    OnClick="btnUpdate_Click" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:Share,Cancel %>"
                    NavigateUrl="~/ListProgramStatus.aspx"></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
