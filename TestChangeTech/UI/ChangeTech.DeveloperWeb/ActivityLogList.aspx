<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ActivityLogList.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ActivityLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <h1>
            Activity Log List
        </h1>
        <div class="headermenu">
        </div>
        <div class="clear">
        </div>
    </div>
    <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </act:ToolkitScriptManager>
    <div class="box-main" style="padding-bottom:10px">
    <table>
    <tr>
    <td width="35%">&nbsp;</td>
    <td width="30%">&nbsp;</td>
    <td width="35%">&nbsp;</td>
    </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="litProgram" runat="server" Text='<%$ Resources:Program %>'></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlProgram" runat="server" DataTextField="ProgramName" DataValueField="ProgramGuid"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"  CssClass="listmenu-large">
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">Language</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged"  CssClass="listmenu-default">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="litSession" runat="server" Text='<%$ Resources:Session %>'></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlSession" runat="server" DataTextField="Name" DataValueField="ID" CssClass="listmenu-large" >
                </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name">User status</p>
            </td>
            <td>
                <asp:DropDownList ID="ddlUserStatus" runat="server"  CssClass="listmenu-default">
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
        <%--<tr>
            <th>
                <asp:Literal ID="Literal2" runat="server" Text='<%$ Resources:UserStatus %>'></asp:Literal>
            </th>
            <td>
                <asp:DropDownList ID="ddlUserStatus" runat="server" OnSelectedIndexChanged="ddlUserStatus_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                    <asp:ListItem Text="Screening" Value="Screening"></asp:ListItem>
                    <asp:ListItem Text="Registered" Value="Registered"></asp:ListItem>
                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                    <asp:ListItem Text="Terminated" Value="Terminated"></asp:ListItem>
                    <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                    <asp:ListItem Text="Paused" Value="Paused"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <th>
                <asp:Literal ID="Literal3" runat="server" Text='<%$ Resources:UserType %>'></asp:Literal>
            </th>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                    <asp:ListItem Text="User" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Tester" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Customer" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td>
                <p class="name"><asp:Literal ID="litUser" runat="server" Text='<%$ Resources:User %>'></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="UserTextBox" runat="server" CssClass="textfield-largetext"></asp:TextBox>
                <asp:LinkButton ID="userLinkButton" runat="server" OnClientClick="SelectUser();">[Select user]</asp:LinkButton>
                <%-- <asp:DropDownList ID="ddlUser" runat="server" DataTextField="UserName" DataValueField="UserGuid">
                </asp:DropDownList>--%>
            </td>
            </tr>
            <tr>
            <td>
                <p class="name"><asp:Literal ID="Literal1" runat="server" Text='<%$ Resources:ActivityType %>'></asp:Literal></p>
            </td>
            <td>
                <asp:DropDownList ID="ddlActivityType" runat="server"  CssClass="listmenu-default">
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
                <p class="name"><asp:Literal ID="litBegin" runat="server" Text='<%$ Resources:Begin %>'></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtBegin" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="beginCalendarExtender" TargetControlID="txtBegin" Format="yyyy-MM-dd"
                    runat="server">
                </act:CalendarExtender>
            </td>
            </tr>
             <tr>
            <td>
                <p class="name"><asp:Literal ID="litEnd" runat="server" Text='<%$ Resources:End %>'></asp:Literal></p>
            </td>
            <td>
                <asp:TextBox ID="txtEnd" runat="server" CssClass="textfield-small"></asp:TextBox>
                <act:CalendarExtender ID="endCalendarExtender" TargetControlID="txtEnd" Format="yyyy-MM-dd"
                    runat="server">
                </act:CalendarExtender>
            </td>
        </tr>
        <tr>
      <td><p class="divider">&nbsp;</p></td>
      <td><p class="divider">&nbsp;</p></td>
    </tr>
        <tr>
        <td></td>
            <td>
                <asp:Button ID="searchButton" runat="server" Text="Search" onclick="searchButton_Click" CssClass="button-update floatRight" />
            </td>
        </tr>
    </table>
    </div>

    <asp:Repeater ID="rptLog" runat="server">
        <HeaderTemplate>
        <div class="list">
            <table>
                <tr>
                    <th style="width: 150px">
                        <asp:Literal ID="litActivityType" runat="server" Text='<%$ Resources:ActivityType %>'></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litUserName" runat="server" Text='<%$ Resources:UserName %>'></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litProgram" runat="server" Text='<%$ Resources:Program %>'></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litSession" runat="server" Text='<%$ Resources:Session %>'></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litActivityDate" runat="server" Text='<%$ Resources:ActivityDate %>'></asp:Literal>
                    </th>
                    <th style="width: 150px">
                        <asp:Literal ID="litMessage" runat="server" Text='<%$ Resources:Message %>'></asp:Literal>
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 150px">
                    <p class="name"><%# Eval("Type")%></p>
                </td>
                <td style="width: 150px">
                    <p class="description"><%# Eval("User.UserName")%></p>
                </td>
                <td style="width: 150px">
                    <p class="description"><%# Eval("Program.ProgramName")%></p>
                </td>
                <td style="width: 150px">
                    <p class="description"><%# Eval("Session.Name")%></p>
                </td>
                <td style="width: 150px">
                    <p class="name"><%# Eval("ActivityDateTime")%></p>
                </td>
                <td style="width: 150px">
                    <p class="description"><%# Eval("Message")%></p>
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
    <div>
    <br />
         <p class="name"> Help: First, please select a program, after that the session list will be updated automatically, select the correct session you want. </p>
        <p>If you want view one user's log, you should click [select user], there will be an small pop up page with an user list, select your user. </p>
        <p>If you want a special activity type, you could select the activity type list. </p>
        <p>Each action will cause the page refresh one time to filter the log records.</p>
    </div>
    <%--  <asp:GridView ID="LogGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true"
        PageSize="12" OnPageIndexChanging="LogGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField HeaderText='<%$ Resources:ActivityType %>' DataField="Type" />
            <asp:TemplateField HeaderText='<%$ Resources:UserName %>'>
                <ItemTemplate>
                    <%# Eval("User.UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Program %>'>
                <ItemTemplate>
                    <%# Eval("Program.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Session %>'>
                <ItemTemplate>
                    <%# Eval("Session.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText='<%$ Resources:ActivityDate %>' DataField="ActivityDateTime" />
            <asp:BoundField HeaderText='<%$ Resources:Message %>' DataField="Message" />
        </Columns>
    </asp:GridView>--%>

    <script language="javascript" type="text/javascript">
        function SelectUser() {

            var txtUserPath = document.getElementById("<%=UserTextBox.ClientID %>");
            var IndexValue = document.getElementById('<%=ddlProgram.ClientID %>').selectedIndex;
            var ProgramGuid = document.getElementById('<%=ddlProgram.ClientID %>').options[IndexValue].value;
            var IndexValueLanguage = document.getElementById('<%=ddlLanguage.ClientID %>').selectedIndex;
            var LanguageGuid = document.getElementById('<%=ddlLanguage.ClientID %>').options[IndexValueLanguage].value;
            var arr = showModalDialog('UserPopUp.aspx?ProgramGUID=' + ProgramGuid + '&LanguageGUID=' + LanguageGuid, window, 'dialogWidth:600px;dialogHeight:800px;help:no;scroll:yes;status:no');

            if (arr) {
                var nodePath = arr
                txtUserPath.value = nodePath;
            }
        }
    </script>

</asp:Content>
