<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddPageSequence.aspx.cs" Inherits="ChangeTech.DeveloperWeb.AddPageSequence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<p>&nbsp;</p>
    <div class="box-main">
    <table style="text-align:left; height:70px">
    <tr>
        <td style="width:6%;padding-top:3px;">
        </td>
        <td style="width:22%;padding-top:3px;">
            <p class="name"><asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Share,Predictor %>"></asp:Literal></p>
        </td>
        <td style="width:22%;padding-top:3px;">
            <p class="name"><asp:Literal ID="Literal7" runat="server" Text="<%$ resources:Share,InterventCategory %>"></asp:Literal></p>
        </td>
        <td style="width:22%;padding-top:3px;">
            <p class="name"><asp:Literal ID="Literal8" runat="server" Text="<%$ resources:Share,Intervent %>"></asp:Literal></p>
        </td>
        <td style="width:22%;padding-top:3px;">
            <p class="name"><asp:Literal ID="Literal9" runat="server" Text="<%$ resources:Share,ProgramRoom %>"></asp:Literal></p>
        </td>
        <td style="width:6%;padding-top:3px;">
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
                <asp:DropDownList ID="predictorDropdownList" runat="server" AutoPostBack="true" CssClass="listmenu-default" OnSelectedIndexChanged="predictorDropDownListChanged">
                </asp:DropDownList>
        </td>
            <td>
                <asp:DropDownList ID="interventCategoryDropdownList" runat="server" AutoPostBack="true" CssClass="listmenu-default" OnSelectedIndexChanged="interventCategoryDropdownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="interventDropdownList" runat="server"  AutoPostBack="true" CssClass="listmenu-default" onselectedindexchanged="intervnetDropdownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlProgramRoom" runat="server"  DataTextField="Name" CssClass="listmenu-default" DataValueField="ProgramRoomGuid" onselectedindexchanged="ddlProgramRoom_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
    </tr>
    </table>
    </div>

    <div class="header">
        <h1>Page sequence in the chosn intervent</h1>
        <div class="headermenu">
            <asp:Button ID="newPageSequenceButton" runat="server" Text="<%$ resources:NewPageSequence %>"  CssClass="button-add" OnClick = "newPageSequenceButton_Click" />
        </div>
        <div class="clear"></div>
    </div>
    <asp:Repeater ID="pageSequenceRepeater" runat="server" OnItemDataBound="pageSequenceRepeater_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                    <tr>
                        <%=HeaderString %>
                        <th style="width: 150px">
                            &nbsp;
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ resources:OrderNumber %>"></asp:Literal>
                            <asp:DropDownList ID="ddlOrder" runat="server" CssClass="listmenu-small"></asp:DropDownList>
                        </th>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td>
                        <p class="name"><%#Eval("Name")%></p>
                    </td>
                    <td>
                        <p class="counter"><%#Eval("CountOfPages")%></p>
                    </td>
                    <td >
                        <p class="description"><%#Eval("UsedInProgram")%></p>
                    </td>
                    <td>
                        <div class="buttons">
                        <asp:Button ID="UseButton" runat="server" CssClass="button-open"  Text="<%$ resources:UseButtonText %>" CommandArgument='<%#Eval ("PageSequenceID") %>' OnClick="UseButton_Click" />
                        <asp:Button ID="PreviewButton" runat="server" CssClass="button-open"  Text="<%$ resources:PreviewButtonText %>" CommandArgument='<%#Eval ("PageSequenceID") %>' />
                        <asp:Button ID="RemoveButton" runat="server" CssClass="button-delete"  Text="<%$ resources:RemoveButtonText %>" CommandArgument='<%#Eval ("PageSequenceID") %>' 
                            OnClientClick ="return confirm('All pages of this page sequence will be deleted also. Do you confirm this page sequence?')"  OnClick="RemoveButton_Click" />
                        <%--
                        <asp:Button ID="EditButton" runat="server" Text="<%$ resources:EditButtonText %>" CommandArgument='<%#Eval ("PageSequenceID") %>' OnClick="EditButton_Click" />
                        --%>
                        </div>
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
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <script language="javascript" type="text/javascript">
        function openPage(url) {
            window.open(url);
            return false;
        }
    </script>
</asp:Content>
