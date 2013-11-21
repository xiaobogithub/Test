<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ManageSpecialString.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageSpecialString" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="header">
  <h1>Manage SpecialString</h1>
  <div class="headermenu">
     
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <table>
     <tr>
      <td style="width:15%">&nbsp;</td>
      <td style="width:20%">&nbsp;</td>
      <td style="width:50%">&nbsp;</td>
      <td style="width:25%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name">Language:</p>
            </td>
            <td>
                <asp:DropDownList ID="LanguageDropDownList" runat="server" AutoPostBack="true" CssClass="listmenu-default"
                    onselectedindexchanged="LanguageDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="newSpecailStringNameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            </td>
            <td align="center">
               <asp:Button ID="newButton" runat="server" Text="Add new"  onclick="newButton_Click" CssClass="button-add"/>
            </td>
        </tr>
        <tr>
          <td></td>
          <td></td>
            <td>
                <asp:Label ID="msgLbl" runat="server"></asp:Label>
            </td>
            <td class="trNoPaddingBottom"></td>
        </tr>
    </table>
    </div>
    <h1>&nbsp;</h1>
    <asp:Repeater ID="specialStringRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <td style="width:40%">
                        Special string
                    </td>
                    <td style="width:40%">
                        Translate value
                    </td>
                    <td style="width:20%">
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Value")%></p>
                </td>
                <td>
                    <asp:Button ID="editButton" runat="server" Text="Edit" CommandArgument='<%#Eval("Name") %>' OnClick="editButton_Click" CssClass="button-open"/>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
                </div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
