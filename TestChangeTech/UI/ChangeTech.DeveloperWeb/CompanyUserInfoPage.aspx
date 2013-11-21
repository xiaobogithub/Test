<%@ Page Title="Company User Information" Language="C#" MasterPageFile="~/3rdParty.Master"
    AutoEventWireup="true" CodeBehind="CompanyUserInfoPage.aspx.cs" Inherits="ChangeTech.DeveloperWeb.CompanyUserInfoPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Company users info overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>
<div class="box-main">
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
    <table >
     <tr>
            <td style="width:20%;">&nbsp;</td>
            <td style="width:30%;">&nbsp;</td>
            <td style="width:20%">&nbsp;</td>
            <td style="width:30%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <p class="name">Account Information</p>
            </td>
            <td align="left">
                <asp:Button runat="server" ID="updateButton" Text="Update" CssClass="button-update" OnClick="updateButton_Click" />
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <p class="name">Email</p>
            </td>
            <td style="width: 30%">
                <asp:TextBox ID="emailTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
            <td align="left" style="width: 15%">
                <p class="name">Status</p>
            </td>
            <td style="width: 30%">
                <asp:DropDownList ID="StatusDropdownList" CssClass="listmenu-default" runat="server">
                    <asp:ListItem Text="Screening"></asp:ListItem>
                    <asp:ListItem Text="Registered"></asp:ListItem>
                    <asp:ListItem Text="Active"></asp:ListItem>
                    <asp:ListItem Text="Terminated"></asp:ListItem>
                    <asp:ListItem Text="Paused"></asp:ListItem>
                    <asp:ListItem Text="Completed"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <p class="name">First name</p>
            </td>
            <td>
                <asp:TextBox ID="firstNameTextBox" runat="server" CssClass="textfield-small"></asp:TextBox>
            </td>
            <td align="left">
                <p class="name">Last name</p>
            </td>
            <td>
                <asp:TextBox ID="lastNameTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <p class="name">Gender</p>
            </td>
            <td>
                <asp:DropDownList ID="genderDropDownList" runat="server" CssClass="listmenu-default">
                    <asp:ListItem Text="Male"></asp:ListItem>
                    <asp:ListItem Text="Female"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left">
                <p class="name">Mobile Phone</p>
            </td>
            <td>
                <asp:TextBox ID="phoneTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <p class="name">Register Date</p>
            </td>
            <td>
                <asp:Label ID="registerDateLbl" runat="server"></asp:Label>
            </td>
            <td align="left">
                <p class="name">Current Day</p>
            </td>
            <td>
                <asp:Label ID="currentDayLbl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">PinCode</p>
            </td>
            <td>
                <asp:TextBox ID="pinCodeTextBox" runat="server"  CssClass="textfield-small"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <p class="name">Activity Log</p>
            </td>
            <td align="left">
                <asp:Button ID="FilterButton" runat="server" Text="Filter" CssClass="button-update" OnClick="FilterButton_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Session</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlSession" runat="server" CssClass="listmenu-default" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
            </td>
            <td>
                <p class="name">Activity Type</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="listmenu-default">
                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    <asp:ListItem Text="SubmitPage" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Login" Value="2"></asp:ListItem>
                    <asp:ListItem Text="EndDay" Value="3"></asp:ListItem>
                    <asp:ListItem Text="PageAssignment" Value="4"></asp:ListItem>
                    <asp:ListItem Text="StartDay" Value="5"></asp:ListItem>
                    <asp:ListItem Text="SendReminderEmail" Value="6"></asp:ListItem>
                    <asp:ListItem Text="PageStart" Value="7"></asp:ListItem>
                    <asp:ListItem Text="PageEnd" Value="8"></asp:ListItem>
                    <asp:ListItem Text="PageSequenceStart" Value="9"></asp:ListItem>
                    <asp:ListItem Text="PageSequenceEnd" Value="10"></asp:ListItem>
                    <asp:ListItem Text="SendPinCodeSM" Value="11"></asp:ListItem>
                    <asp:ListItem Text="SendShortMessage" Value="12"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name">Between</p>
            </td>
            <td>
                <asp:TextBox ID="txtBegin" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="beginCalendarExtender" TargetControlID="txtBegin" Format="yyyy-MM-dd"
                    runat="server">
                </act:CalendarExtender>
            </td>
            <td>
                <p class="name">And</p>
            </td>
            <td>
                <asp:TextBox ID="txtEnd" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="endCalendarExtender" TargetControlID="txtEnd" Format="yyyy-MM-dd"
                    runat="server">
                </act:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>

    <asp:Repeater ID="rptLog" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table  >
                <tr>
                    <th style="width: 150px">
                        <asp:Literal ID="litActivityType" runat="server" Text="Activity Type"></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litProgram" runat="server" Text="Program"></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litSession" runat="server" Text="Session"></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litActivityDate" runat="server" Text="Activity Date"></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litMessage" runat="server" Text="Message"></asp:Literal>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 150px">
                    <p class="name"><%# Eval("Type")%></p>
                </td>
                <td style="width: 150px">
                    <p class="name"><%# Eval("Program.ProgramName")%></p>
                </td>
                <td style="width: 150px">
                    <p class="name"><%# Eval("Session.Name")%></p>
                </td>
                <td style="width: 150px">
                    <%# Eval("ActivityDateTime")%>
                </td>
                <td style="width: 150px">
                   <p class="description"> <%# Eval("Message")%></p>
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
