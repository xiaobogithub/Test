<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ListStudy.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListStudy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>List of studies </h1>
  <div class="headermenu">
        <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" CssClass="button-add"  Text="Add new" />
  </div>
    <div class="clear"></div>
</div>
    <asp:Repeater ID="studyRepeater" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table cellpadding:"0px" cellspacing:"0px">
                <tr style=" height:23px; ">
                      <th style="width:70%; text-align:left; ">
                        &nbsp;&nbsp;&nbsp;Study name
                    </th>
                    <th style="width:15%; text-align:right;">
                        Description
                    </th>
                    <th style="width:15%; text-align:left;">
                    <p style="height:15px;"></p>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                <p class="name"><%#Eval ("Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("Description")%></p>
                </td>
                <td>
                    <asp:Button ID="editButton" runat="server"  CssClass="button-open"  Text="Open" CommandArgument='<%#Eval("StudyGUID") %>' OnClick="editButton_Click" />
                    <asp:Button ID="deleteButton" runat="server"  CssClass="button-delete"  Text="Delete" CommandArgument='<%#Eval("StudyGUID") %>' OnClick='deleteButton_Click' />
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
</asp:Content>
