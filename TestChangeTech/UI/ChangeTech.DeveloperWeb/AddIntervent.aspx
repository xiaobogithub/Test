<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddIntervent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddIntervent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <h4>
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Title %>"></asp:Literal>
    </h4>--%>
     <div class="header">
  <h1>Add predictor overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>

<div style="height:580px;">
<div class="box-main">
  <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
     <tr>
        <td><p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal> : </p></td>
        <td>
         <asp:TextBox ID="txtInterventName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtInterventName" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"> <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal>: </p></td>
        <td>
              <asp:TextBox ID="txtInterventDescription" runat="server" TextMode ="MultiLine"  CssClass="textfield-largetext" Rows="5"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name"><asp:Literal ID="Literal3" runat="server" Text = "<%$ Resources:Share,InterventCategory %>"></asp:Literal> : </p></td>
        <td>
            <asp:DropDownList ID="ddlInterventCategory" runat="server" CssClass="listmenu-large">
                </asp:DropDownList>
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
                <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:Share,Add %>"  onclick="btnAdd_Click"  CssClass="button-update"/>
                     <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>
