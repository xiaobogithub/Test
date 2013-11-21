<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true"
    CodeBehind="ListInactiveUser.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListInactiveUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>InactiveUser list overview </h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
<div style="height:580px;">
<div class="box-main">
    <table>
    <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
     <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources: InactiveOneDay %>"></asp:Literal></p>
            </td>
            <td>
                <p class="name"><asp:Literal ID="ltlInactiveOneDay" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources: InactiveOneWeek %>"></asp:Literal></p>
            </td>
            <td>
               <p class="name"><asp:Literal ID="ltlInactiveOneWeek" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <p class="name"> <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources: InactiveOneMonth %>"></asp:Literal></p>
            </td>
            <td>
                 <p class="name"><asp:Literal ID="ltlInactiveOneMonth" runat="server"></asp:Literal></p>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
      <td style="width:35%">&nbsp;</td>
      <td style="width:30%">&nbsp;</td>
      <td style="width:35%">&nbsp;</td>
    </tr>
        </table>
        </div>
        </div>
<%--    <asp:Repeater ID="rpInactiveOneDay" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Gender
                    </th>
                    <th>
                        MobilePhone
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 150px">
                    <%#Eval("UserName")%>
                </td>
                <td style="width: 300px">
                    <%#Eval("Gender")%>
                </td>
                <td style="width: 300px">
                    <%#Eval("PhoneNumber")%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>--%>
</asp:Content>
