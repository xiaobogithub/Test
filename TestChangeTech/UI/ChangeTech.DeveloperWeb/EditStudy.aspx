<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditStudy.aspx.cs" Inherits="ChangeTech.DeveloperWeb.EditStudy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Edit study overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
<div style="height:580px;">
<div class="box-main">
  <table>
    <tr>
      <td style="width:35%;">&nbsp;</td>
      <td style="width:30%;">&nbsp;</td>
      <td style="width:35%;">&nbsp;</td>
    </tr>
     <tr>
        <td><p class="name">Name : </p></td>
        <td>
         <asp:TextBox ID="nameTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="nameTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">Description : </p></td>
        <td>
            <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine"  CssClass="textfield-largetext" Rows="5"></asp:TextBox>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td><p class="name">URL : </p></td>
        <td>
            <asp:HyperLink ID="studyHyperLink" runat="server" Target="_blank"></asp:HyperLink>
        </td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
      <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
            <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click"  CssClass="button-update"/>
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" CssClass="button-delete"/>
        </td>
      <td>&nbsp;</td>
    </tr>
    </table>
    </div>

    <div class="header">
        <div class="headermenu">
            <asp:Button ID="addButton" runat="server" Text="New Study Url" CssClass="button-add" OnClick="addButton_Click" />
        </div>
        <div class="clear"></div>
    </div>

    <asp:Repeater ID="studyRepeater" runat="server">
        <HeaderTemplate>
            <div class="list">
            <table>
                <tr>     
                    <th>
                        URL
                    </th>
                    <th style="width:10%;">
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>   
                <td>
                    <p class="description"><asp:HyperLink ID="urlHyperLink" Text='<%#Eval("RouteURL") %>' NavigateUrl='<%#Eval("RouteURL") %>' runat="server" Target="_blank"></asp:HyperLink></p>
                </td>
                <td>
                    <div class="buttons">
                    <asp:Button ID="editButton" runat="server" Text="Edit" OnClick="editButton_Click" CommandArgument='<%#Eval("StudContentGUID") %>' CssClass="button-open"/>
                    <asp:Button ID="deleteButton" runat="server" Text="Delete" OnClick="deleteButton_Click" CommandArgument='<%#Eval("StudContentGUID") %>'  CssClass="button-delete"/>
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
        </FooterTemplate>
    </asp:Repeater>
    </div>
</asp:Content>
