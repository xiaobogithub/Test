<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageHelpItem.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageHelpItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="header">
        <h1>Manage help item overview </h1>
        <div class="headermenu">
            <asp:Button ID="addButton" runat="server" Text="Add new" CssClass="button-add" OnClick="addButton_Click" />
        </div>
        <div class="clear"></div>
    </div>   
         <asp:Label ID="programLabel" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
         <br />
    <%-- <table>
        <tr>
            <td>
                Program
            </td>
            <td>
                <asp:DropDownList ID="ProgramDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProgramDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Language
            </td>
            <td>
                <asp:DropDownList ID="LanguageDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LanguageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>--%>
    <asp:Repeater ID="HelpItemRepeater" runat="server" OnItemDataBound="HelpItemRepeater_ItemDataBound">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <td style="width:10%">
                        Order
                    </td>
                    <td style="width:35%">
                        Question
                    </td>
                    <td style="width:45%">
                        Answer
                    </td>
                    <td style="width:10%">
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                  <asp:Button ID="ButtonUp" runat="server" CssClass="button-up" CommandArgument='<%#Eval("HelpItemGUID") %>' OnClick="btnUp_Click"/>
                    <p class="name"> <%#Eval("Order")%></p>
                    <asp:Button ID="ButtonDown" runat="server" CssClass="button-down" CommandArgument='<%#Eval("HelpItemGUID") %>' OnClick="btnDown_Click"/>
                </td>
                <td>
                    <p class="description"><%#Eval("Question")%></p>
                </td>
                <td>
                    <p class="description"><%#Server.HtmlEncode((string)Eval("Answer"))%></p>
                </td>
             <%--   <td>
                    <asp:ImageButton ID="UpImageButton" runat="server" OnClick="UpImageButton_Click" ImageUrl="~/Images/arrow_up_blue.gif" CommandArgument='<%#Eval("HelpItemGUID") %>' />
                </td>
                <td>
                    <asp:ImageButton ID="DownImageButton" runat="server" OnClick="DownImageButton_Click" ImageUrl="~/Images/arrow_down_blue.gif" CommandArgument='<%#Eval("HelpItemGUID") %>' />
                </td>--%>
                <td align="right">
                    <asp:Button ID="editButton" runat="server" Text="Edit" CommandArgument='<%#Eval("HelpItemGUID") %>'
                        OnClick="editButton_Click" CssClass="button-open"/>
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" CommandArgument='<%#Eval("HelpItemGUID") %>'
                        OnClick="deleteButton_Click" CssClass="button-delete" OnClientClick="return confirm('Do you confirm to delete this helpitem?');" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
