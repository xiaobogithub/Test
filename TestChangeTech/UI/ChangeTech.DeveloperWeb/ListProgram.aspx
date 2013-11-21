<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListProgram.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ProgramManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>List of programs </h1>
  <div class="headermenu">
    <asp:Button ID="btnAddNewProgram" runat="server" CssClass="button-add" Text="<%$ Resources:Share, AddProgram %>"
                            OnClick="btnAddNewProgram_Click" Enabled="false" Width="147px" />
  </div>
  <div class="clear"></div>
</div>
    <asp:Repeater ID="rpProgram" runat="server" OnItemDataBound="rpProgram_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table style="" cellpadding:"0px" cellspacing:"0px">
                <tr style=" height:23px; "  >
                    <%-- <%# HeaderString %>--%>
                    <th style="width:70%; text-align:left;">
                        &nbsp;&nbsp;&nbsp;Name & Description
                    </th>
                   <%-- <th style="width:15%; text-align:right;">
                        NumberOfUsers
                    </th>--%>
                    <th style="width:15%; text-align:left; padding-right:20px; ">
                        <asp:DropDownList ID="ProgramSortDDL" runat="server" CssClass="listmenu-sortby" AutoPostBack="true" OnSelectedIndexChanged="ProgramSortDDL_SelectedIndexChanged" >
                        <asp:ListItem Value="Name">Name</asp:ListItem>
                        <asp:ListItem Value="Date">Date</asp:ListItem>
                        <asp:ListItem Value="Users">Users</asp:ListItem>
                        </asp:DropDownList>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                <p class="name"><%#Eval ("ProgramName")%></p>
                <p class="description"> <%# Eval("Description") != null ? ((string)Eval("Description")).Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>") : Eval("Description")%></p>
                </td>
                <%--<td>
                    <p class="useramount"><%#Eval("NumberOfUsers")+" Users"%></p>
                </td>--%>
                <%--<td  style="width: 10%">
                    <asp:LinkButton ID="programManagerLinkButton" runat="server" CommandArgument='<%#Eval ("Guid")%>'
                        OnClick="programManagerLinkButton_Click"><%# Eval("ProjectManager")%></asp:LinkButton>
                </td>--%>
                <td>
                    <asp:Button ID="btnEdit" runat="server" CssClass="button-open" Text="<%$ Resources:OpenButton %>" OnClick="btnEdit_Click"
                        Enabled="false" CommandArgument='<%#Eval ("Guid")%>' />
                        <asp:DropDownList runat="server" ID="MoreOptionsDDL"  DataValueField='<%#Eval ("Guid")%>' CssClass="listmenu-small" AutoPostBack="true"  OnSelectedIndexChanged="MoreOptionsDDL_SelectedIndexChanged" >
                            <asp:ListItem>More options</asp:ListItem>
                            <asp:ListItem Value="Assign managers">Assign managers</asp:ListItem>
                            <asp:ListItem Value="Copy program">Copy program</asp:ListItem>
                        </asp:DropDownList>
                    <asp:HiddenField ID="HiddenFieldProgramGuid" Value='<%#Eval ("Guid")%>'  runat="server" />
                    <%-- <asp:Button ID="btnSecurity" runat="server" Text="<%$ Resources: ProgramUserButtonName%>"
                        Enabled="false" CommandArgument='<%#Eval ("Guid")%>' OnClick="btnSecurity_Click" />
                    <asp:Button ID="btnPreview" runat="server" Text="<%$ Resources: PreviewButtonName%>"
                        CommandArgument='<%#Eval ("Guid")%>' OnClick="btnPreview_Click" />--%>
                    <%--<asp:Button ID="btnMakeCopy" runat="server" Text="<%$ Resources:CopyProgram %>" Enabled="false"
                        OnClick="btnMakeCopy_Click" CommandArgument='<%#Eval ("Guid")%>' />--%>
                    <%-- <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:DeleteButtonName %>"
                        Enabled="false" OnClientClick="return confirm('You are deleting program, are you sure you want to do this action?')"
                        OnClick="btnDelete_Click" CommandArgument='<%#Eval ("Guid")%>' />--%>
                </td>
            </tr>           
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
            <div class="pagenav">
            <%# PagingString %>
            <div class="clear"></div>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <%--<script type="text/javascript">
        $("tr:odd").addClass("tr_odd");
        $("tr:even").addClass("tr_even");
    </script>--%>
    <%--<asp:LinkButton ID="preLinkButton" runat="server" OnClick="preLinkButton_Click">pre</asp:LinkButton>
    <asp:Label ID="pageNumberLabel" runat="server" Text=""></asp:Label>
    <asp:LinkButton ID="nextLinkButton" OnClick="nextLinkButton_Click" runat="server">next</asp:LinkButton>--%>
</asp:Content>
