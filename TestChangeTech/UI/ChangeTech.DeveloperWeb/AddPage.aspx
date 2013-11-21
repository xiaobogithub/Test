<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddPage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddPage" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <asp:Silverlight ID="AddPageSilverlight" runat="server" 
            Height="720" Width="98%" />
    </div>
</asp:Content>
