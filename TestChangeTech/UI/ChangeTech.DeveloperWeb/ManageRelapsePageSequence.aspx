<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="ManageRelapsePageSequence.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageRelapsePageSequence" %>

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
            <span class="lastlinode">
                <asp:Literal ID="ltlEditSession" Text="<%$ Resources:Share, EditSession%>" runat="server"></asp:Literal>
            </span>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="alertmessage hidden">There is an error somewhere</div>
<div class="confirmationmessage hidden">The program is updated!</div>
<div class="header">
  <h1>Subroutine pagesequence list</h1>
  <div class="headermenu">
  <asp:Button ID="addButton" runat="server" Text="Add subroutine" CssClass="button-add"  OnClick="addButton_Click" />
  </div>
  <div class="clear"></div>
</div>

    <asp:Repeater ID="relapseRepeater" runat="server" onitemdatabound="relapseRepeater_ItemDataBound">
        <HeaderTemplate>
            <div class="list">
            <table>
                    <tr>
                        <th style="width:20%; text-align:left; padding-left:10px;">
                            Page sequence
                        </th>
                        <th style="width:60%; text-align:left; padding-left:10px;">
                            Description
                        </th>
                        <th style="width:20%; text-align:left; padding-left:10px;">
                            &nbsp;
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("PageSequenceName")%></p>
                </td>
                <td>
                    <p class="name"><%#Eval("PageSequenceDescription")%></p>
                </td>
                <td>
                <div class="buttons">
                    <asp:Button ID="editButton" runat="server" CssClass="button-open" Text="Edit" CommandArgument='<%#Eval("PageSequenceGUID")%>' OnClick="editButton_Click" />
                    <asp:Button ID="delRelapseButton" runat="server" CssClass="button-delete" Text="Delete" OnClick="delRelapseButton_Click" CommandArgument='<%#Eval("RelapseGUID") %>' />
                    <asp:DropDownList ID="moreOptionsDDL" runat="server" DataValueField='<%#Eval("PageSequenceGUID")%>' DataTextField='<%#Eval("RelapseGUID") %>' AutoPostBack="true" CssClass="listmenu-small" OnSelectedIndexChanged="moreOptionsDDL_SelectedIndexChanged">
                                    <asp:ListItem>More options</asp:ListItem>
                                    <asp:ListItem Value="Preview">Preview</asp:ListItem>
                                    <asp:ListItem Value="Use For CTPP">Use For CTPP</asp:ListItem>
                                    <%--<asp:ListItem Value="Delete day">Delete day</asp:ListItem>--%>
                    </asp:DropDownList>
                    <%--<asp:Button ID="preRelapseButton" runat="server" Text="Preview" CommandArgument='<%#Eval("PageSequenceGUID")%>' />
                    <asp:Button ID="useRelapseForCTPP"  Width="120px" runat="server" Text="Use For CTPP"  OnClick="useRelapseForCTPP_Click" CommandArgument='<%#Eval("RelapseGUID") %>' />--%>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <script type="text/javascript">
        function openPage(url) {
            mywindow = window.open(url);
            return false;
        }
    </script>

</asp:Content>
