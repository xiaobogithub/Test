<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MergeIntervent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.MergeIntervent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="height:580px; padding-left:11px;">
<div class="header">
  <h1>Please select an intervent to merge with intervent <span style="color:Red"><asp:Label ID="interventLabel" runat="server" Text=""></asp:Label></span></h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table>
    <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Share,Predictor %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="predictorDropdownList" runat="server" AutoPostBack="true"  CssClass="listmenu-large" OnSelectedIndexChanged="predictorDropDownListChanged">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Share,InterventCategory %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="interventCategoryDropdownList" runat="server" AutoPostBack="true" CssClass="listmenu-large" 
                    OnSelectedIndexChanged="interventCategoryDropdownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Share,Intervent %>"></asp:Literal></p>
            </td>
            
            <td>
                <asp:DropDownList ID="interventDropdownList" runat="server" AutoPostBack="true" CssClass="listmenu-large" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
      <td style="width:25%;">&nbsp;</td>
      <td style="width:40%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
    </table>
    </div>
    <h1>&nbsp;</h1>

    <asp:Button ID="mergeButton" runat="server" CssClass="button-update" Text="Merge intervent"
        OnClick="mergeButton_Click" />
    <h1>&nbsp;</h1>
    <div>
        ie. Please select an intervent with <span style="color:Red">Introduce</span> which means:
        All the page sequences in the intervent you selected will be moved to <span style="color:Red">Introduce</span>.
    </div>
    </div>
</asp:Content>
