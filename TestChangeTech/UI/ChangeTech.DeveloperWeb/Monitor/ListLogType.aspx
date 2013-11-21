<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true" CodeBehind="ListLogType.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListLogType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>LogType list overview </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<asp:Repeater ID="rpLogType" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th>
                        Log Type Name
                    </th>
                    <th>                       
                        Log Priority
                    </th>      
                    <th>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <p class="name"><%#Eval("Name")%></p>
                </td>
                <td>
                    <p class="description"><%#Eval("LogPriority.Name")%></p>
                </td>                
                <td>
                    <asp:Button ID="btnEdit" runat="server" CssClass="button-open" Text="<%$ Resources:Share,Edit %>" OnClick="btnEdit_Click" CommandArgument='<%#Eval("ID")%>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
