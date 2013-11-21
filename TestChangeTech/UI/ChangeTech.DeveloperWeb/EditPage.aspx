<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="EditPage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditPage" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentSiteMapPath" ContentPlaceHolderID="SiteMapPath" runat="server">
    <li>
    <asp:HyperLink ID="hpProgramLink" runat="server" Text="<%$ Resources: Share, Programs%>"
        ToolTip="<%$ Resources: Share, GoPrograms%>"></asp:HyperLink>
        </li>
        <li>
    <asp:HyperLink ID="hpEditProgramLink" runat="server" Text="<%$ Resources: Share, EditProgram%>"
        ToolTip="<%$ Resources: Share, GoEditProgram%>"></asp:HyperLink>
        </li>
        <li>
    <asp:HyperLink ID="hpEditSessionLink" runat="server" Text="<%$ Resources: Share, EditSession%>"
        ToolTip="<%$ Resources: Share, GoEditSession%>"></asp:HyperLink>
        </li>
        <li>
    <asp:HyperLink ID="hpEditPageSequence" runat="server" Text="<%$ Resources: Share, EditPageSequence%>"
        ToolTip="<%$ Resources: Share, GoEditPageSequence%>"></asp:HyperLink>
        </li>
        <li>
        <span  class="lastlinode">
        <asp:Literal ID="ltlEditPage" Text="<%$ Resources:Share, EditPage%>" runat="server"></asp:Literal>
    </span>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br /><br />
<div class="alertmessage hidden">There is an error somewhere</div>
<div class="confirmationmessage hidden">The program is updated!</div>
<div class="header">
  <h1>Edit page</h1>
  <div class="headermenu">
  <asp:Button  runat="server" ID="BtnPreviousPage" Text="Previous page"  CssClass="button-previous" OnClick="lnkBtnPreviousPage_Click" />
  <asp:Button  runat="server" ID="BtnNextPage" Text="Next page"  CssClass="button-next" OnClick="lnkBtnNextPage_Click" />
        <%--<asp:LinkButton runat="server" ID="lnkBtnPreviousPage" CssClass="button-previous" OnClick="lnkBtnNextPage_Click"
            Text="Previous page"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lnkBtnNextPage" CssClass="button-previous"  OnClick="lnkBtnNextPage_Click"
            Text="Next page"></asp:LinkButton>--%>
  </div>
  <div class="clear"></div>
</div>
    <div  style=" margin:0 auto; width:800px;">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <p>
            <asp:Label ID="warnLbl" runat="server" Width="98%" Text="<%$ Resources:Share, NotChangeableBecauseOfPublished%>"
                Visible="false" ForeColor="Red"></asp:Label></p>
        <div style="border: 1px solid gray;">
            <asp:Silverlight ID="EditPageSliverLight" runat="server" Height="720" Width="98%" />
        </div>
    </div>
</asp:Content>
