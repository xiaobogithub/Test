<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Title="Manage Program Language"
    CodeBehind="ManageProgramLanguage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageProgramLanguage" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
  <h1>Manage program language overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
    </asp:ToolkitScriptManager>
    <p>
        <asp:Label ID="programNameLbl" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <br />
    <asp:Silverlight ID="ManageLanguageSliverLight" runat="server" Height="720" Width="98%" />
    <%--    <asp:Timer ID="StatusCheckTimer" runat="server" OnTick="StatusCheckTimer_Tick" Interval="6000"
            Enabled="true">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
            <asp:Label ID="testLbl" runat="server"></asp:Label>
         </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="StatusCheckTimer" EventName="Tick" />
         </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
