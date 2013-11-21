<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditInterventCategory.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditInterventCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <h4>
        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Title %>"></asp:Literal>
    </h4>--%>
    <div class="header">
  <h1>Edit Intervent Category </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div> 
<div style="height:580px;">
    <div class="box-main">
    <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtInterventCategoryName" runat="server"  CssClass="textfield-largetext"></asp:TextBox><span class="requiredMarked">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtInterventCategoryName" ErrorMessage="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtInterventCategoryDescription" runat="server" TextMode ="MultiLine" Rows="7" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text = "<%$ Resources:Predictor %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlPredictor" runat="server" CssClass="listmenu-default">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Share,Update %>" onclick="btnUpdate_Click" CssClass="button-update"/>
                     <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
            </td>
        </tr>
    </table>
    </div>
     </div>
</asp:Content>
