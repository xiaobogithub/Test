<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageLanguage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageLanguage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="height:580px;">
<div class="header">
  <h1>Manage Language </h1>
  <div class="headermenu">
        <asp:Button ID="btnAdd" runat="server" Text="add language" OnClick="btnAdd_Click" CssClass="button-add" />
  </div>
  <div class="clear"></div>
</div>  

<asp:Repeater ID="rpLanguage" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <td width="70%">
                        language name
                    </td>
                    <td width="15%" align="right">the count of programs</td>      
                    <td width="15%">
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Name")%></p>
                </td>
                <td>
                    <p class="useramount"><%#Eval("Count")%></p>
                </td>                
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Share,Delete %>" Enabled="false"
                        OnClientClick="return confirm('Are you sure?')" OnClick="btnDelete_Click" CommandArgument='<%#Eval ("LanguageGUID") %>' CssClass="button-delete" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
                </div>
        </FooterTemplate>
    </asp:Repeater>
    </div>
</asp:Content>
