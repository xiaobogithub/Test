<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditCompanyRight.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditCompanyRight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit company right overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
<div style="height:580px;">
<div class="box-main">
  <table>
  <tr>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:55%;">&nbsp;</td>
      <td style="width:15%;">&nbsp;</td>
    </tr>
    <tr>
            <td colspan="3">
                <asp:Label ID="LabelProgramInfo" Font-Bold="true" Font-Size="Medium" runat="server" Text="Program information"></asp:Label>
            </td>
        </tr>
    <tr>
        <td><p class="name">Name:</p></td>
        <td>
             <asp:Label ID="programNameLabel" Font-Bold="true" Font-Size="Small" runat="server" Text=""></asp:Label>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Version:</p></td>
        <td>
            <asp:Label ID="versionLabel" runat="server" Font-Bold="true" Font-Size="Small"  Text=""></asp:Label>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
    <tr>
            <td colspan="3">
            <asp:Label ID="LabelCompanyInfo" Font-Bold="true" Font-Size="Medium" runat="server" Text="Company information"></asp:Label>
            </td>
        </tr>
    <tr>
        <td><p class="name">Name:</p></td>
        <td>
             <asp:Label ID="companyLabel" runat="server" Font-Bold="true" Font-Size="Small"  Text=""></asp:Label>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Overdue time:</p></td>
        <td>
                <asp:TextBox ID="overdueTextBox" runat="server"  CssClass="textfield-extention" ></asp:TextBox>
                <act:CalendarExtender ID="CalendarExtender" Format="yyyy-MM-dd" TargetControlID="overdueTextBox" runat="server">
                </act:CalendarExtender>
                <asp:Button ID="updateButton" runat="server" CssClass="button-update" Text="Update" OnClick="updateButton_Click" />
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Screen URL:</p></td>
        <td>
             <asp:HyperLink ID="screenurlHyperLink"  Font-Bold="true" Font-Size="Small"  runat="server" Target="_blank"></asp:HyperLink>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Login URL:</p></td>
        <td>
            <asp:HyperLink ID="loginHyperLink" runat="server" Font-Bold="true" Font-Size="Small"  Target="_blank"></asp:HyperLink>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr style="height:10px">

    </tr>
    </table>   
    </div>
    </div>
</asp:Content>
