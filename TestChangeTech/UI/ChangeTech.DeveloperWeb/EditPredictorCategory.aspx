<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPredictorCategory.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditPredictorCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<h4>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Title %>"></asp:Literal></h4>--%>
        <div class="header">
  <h1>Edit Predictor </h1>
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
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Share,Name %>"></asp:Literal> : </p>
            </td>
            <td>
                <asp:TextBox ID="txtPredictorCategoryName" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Share,Description %>"></asp:Literal> : </p>
            </td>
            <td>
                <asp:TextBox ID="txtPredictorCategoryDescription" runat="server" 
                    TextMode = "MultiLine" Rows="7" CssClass="textfield-largetext"></asp:TextBox>
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
            <td>
                <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Share,Cancel %>" CausesValidation="False" OnClick="cancelButton_Click"  CssClass="button-delete"/>
                <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Share,Update %>"  onclick="btnUpdate_Click" CssClass="button-update floatRight" />
            </td>
        </tr>
    </table>
   </div>
</div>
</asp:Content>
