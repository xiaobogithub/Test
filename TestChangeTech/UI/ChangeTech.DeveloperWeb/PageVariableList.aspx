<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PageVariableList.aspx.cs" Inherits="ChangeTech.DeveloperWeb.PageVariableList" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:Silverlight ID="ManagePageVariable" runat="server" Height="720" Width="98%" />
</asp:Content>
