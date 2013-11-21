<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="height:580px; padding-left:11px;">
    <br />
    <p>
        Hi,
    </p>
    <br />
    <p>
        This is Changetech
        <%= VersionNumber %>. Please send your feedback to Changetech develop team or create
        issues on jira.
    </p>
    <br />
    <p>
        Best Regards,
    </p>
    <p>
        Change Tech Team
    </p>
    </div>
</asp:Content>
