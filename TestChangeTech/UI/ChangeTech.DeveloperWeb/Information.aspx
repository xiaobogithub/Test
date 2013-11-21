<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Information.aspx.cs" Inherits="ChangeTech.DeveloperWeb.Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: Tip%>"></asp:Literal>
        <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources: Login%>" NavigateUrl="~/Default.aspx"></asp:HyperLink>
    </div>

    <script language="javascript">
        setTimeout("location.href ='Default.aspx'", 3000)
    </script>

</asp:Content>
