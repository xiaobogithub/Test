<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddTranslationJob.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddTranslationJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="header">
  <h1>Add Translation Job </h1>
  <div class="headermenu">
    
  </div>
  <div class="clear"></div>
</div> 
<div class="box-main">
    <table>
    <tr>
    <td width="35%">&nbsp;</td>
      <td width="30%">&nbsp;</td>
      <td width="35%">&nbsp;</td>
      </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Program %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ProgramName" DataValueField="Guid"
                     AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" CssClass="listmenu-sortby nonFloat">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ resources:FromLanguage %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlFromLanguage" runat="server" Width="200px" DataTextField="Name" DataValueField="LanguageGUID" CssClass="listmenu-sortby nonFloat">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal3" runat="server" Text="<%$ resources:ToLanguage %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlToLanguage" runat="server" DataTextField="Name" Width="200px" DataValueField="LanguageGUID" CssClass="listmenu-sortby nonFloat">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal4" runat="server" Text="<%$ resources:DefaultTranslatedField %>"></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlDefaultTranslatedField" runat="server" Width="200px"  CssClass="listmenu-sortby nonFloat">
                    <asp:ListItem Text="OriginalText" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="GoogleTranslation" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Nothing" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td><p class="divider"></p></td>
        <td><p class="divider"></p></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="<%$ resources:Share,Cancel %>" OnClick="btnCancel_Click" CssClass="button-delete " />
                <asp:Button ID="btnAdd" runat="server" Text="<%$ resources:Share,Add %>" OnClick="btnAdd_Click" CssClass="button-update floatRight" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
