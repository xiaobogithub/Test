<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPopUp.aspx.cs" Inherits="ChangeTech.DeveloperWeb.UserPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select user</title>
    <meta http-equiv="Expires" content="0"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <base target="_self"/>
    <link href="Themes/default.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="css/general.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="css/added.css" rel="stylesheet" type="text/css" media="all"/>
    <script type="text/javascript" src="Scripts/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="Scripts/json2.js"></script>
    <script type="text/javascript" src="Scripts/ct.base.js"></script>
    <script type="text/javascript" src="Scripts/ct.program.js"></script>
    <script type="text/javascript" src="Scripts/Global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
  <h1>User Popup overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
    <div class="box-main">
        <table border="0">
            <tr>
                <td style="width:30%;">&nbsp;</td>
                <td style="width:70%;">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <p class="name">Email</p>
                </td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="name">User status</p>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserStatus" runat="server" CssClass="listmenu-default">
                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                        <asp:ListItem Text="Screening" Value="Screening"></asp:ListItem>
                        <asp:ListItem Text="Registered" Value="Registered"></asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Terminated" Value="Terminated"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        <asp:ListItem Text="Paused" Value="Paused"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                   <p class="name"> User type</p>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="listmenu-default">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="User" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Tester" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Customer" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                    <td><p class="divider">&nbsp;</p></td>
                    <td><p class="divider">&nbsp;</p></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="button-update" OnClick="SearchButton_Click" />
                </td>
            </tr>
        </table>
    </div>
        <asp:Repeater ID="UserRepeater" runat="server">
            <HeaderTemplate>
            <div class="list">
                <table>
                    <tr>
                        <td>
                            E-mail
                        </td>
                        <td>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><p class="name"><%#Eval("UserName")%></p></td>
                    <td><asp:LinkButton ID="selectLinkButton" runat="server" OnClick="selectLinkButton_Click"
                            CommandArgument='<%#Eval("UserName") %>'>Select</asp:LinkButton>
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
       <%-- <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            PageSize="12" OnPageIndexChanging="UserGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="E-mail" DataField="UserName" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="selectLinkButton" runat="server" OnClick="selectLinkButton_Click"
                            CommandArgument='<%#Eval("UserName") %>'>Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
    </form>
</body>
</html>
