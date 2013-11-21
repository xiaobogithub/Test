<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master" AutoEventWireup="true"
    CodeBehind="ManageDailyReportSMS.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageDailyReportSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.6.2.ct.managedailyreportsms.js"></script>
    <script type="text/javascript">
        function GetArgsFromHref(sHref, sArgName) {
            var args = sHref.split("?");
            var retval = "";
            if (args[0] == sHref) {
                return retval;
            }
            var str = args[1];
            args = str.split("&");
            for (var i = 0; i < args.length; i++) {
                str = args[i];
                var arg = str.split("=");
                if (arg.length <= 1) continue;
                if (arg[0] == sArgName) retval = arg[1];
            }
            return retval.replace('%7b', '{').replace('%7d', '}');
        }

        $(document).ready(function () {
            //             Using parameters on URL
//            var sessionguid = GetArgsFromHref(window.location.href, "SessionGUID");
//            var programpage = GetArgsFromHref(window.location.href, "ProgramPg");
//            var sessionpage = GetArgsFromHref(window.location.href, "SessionPg");
            var programguid = GetArgsFromHref(window.location.href, "ProgramGUID");
            $('#managedailyreportsms').managedailyreportsms({
//                sessionGuid: sessionguid,
//                programPage: programpage,
//                sessionPage: sessionpage,
                programGuid: programguid
            });
        });
    </script>
    <style type="text/css">
        .requiredSign
        {
            color: Red;
        }
        
        .error
        {
            text-align: left;
            font-weight: bolder;
            color: Red;
            display: block;
        }
        
        .ct-bg-LightGray
        {
            background-color: #dddddd;
        }
        .ct-bg-DarkGray
        {
            background-color: #bbbbbb;
        }
        .ct-image
        {
            width: 70px;
        }
        .ct-edit-textarea
        {
            height: 70px;
            width: 100%;
        }
        #result table
        {
            width: 90%;
            height: 90%;
        }
        #result table select
        {
            width: 90%;
        }
        #result table input
        {
            width: 90%;
        }
        #result table textarea
        {
            width: 90%;
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="ContentSiteMapPath" ContentPlaceHolderID="SiteMapPath" runat="server">
    <li>
        <asp:HyperLink ID="hpProgramLink" runat="server" Text="<%$ Resources: Share, Programs%>" ToolTip="<%$ Resources: Share, GoPrograms%>"></asp:HyperLink>
    </li>
    <li>
    <asp:HyperLink ID="hpEditProgramLink" runat="server" Text="<%$ Resources: Share, EditProgram%>" ToolTip="<%$ Resources: Share, GoEditProgram%>"></asp:HyperLink>
    </li>    
    <li>
        <span class="lastlinode">
            <asp:Literal ID="ltlPageReview" Text="<%$ Resources:Share, ManageDailyReportSMS%>" runat="server"></asp:Literal>
        </span>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage daily report SMS overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>

    <div id="managedailyreportsms">
    <div class="box-main">
        <table>
            <tr>
                <td style="width:20%;">&nbsp;</td>
                <td style="width:45%;">&nbsp;</td>
                <td style="width:35%;">&nbsp;</td>
            </tr>
            <tr>
                <td id="dailySMSTimeNameTD" >
                        <p class="name">
                            <asp:Literal ID="Literal4" runat="server" Text="Program Daily SMS Time:"></asp:Literal>
                            <br />
                            <label style="color:Red">(The rule to input, HH:mm)</label>
                        </p>
                </td>
                <td id="dailySMSTimeTD" align="center">
                    <%--<input type="text" maxlength="5"  style="width:100;"  />--%>
                </td>
            <td id="dailySMSTimeUpdateTD"><p class="description">Click the left blank to enter the dailySMSTime and click here to update it to database.</p></td>
            </tr>
        </table>
        </div>
        <br /><br />
        <div class="list">
        <table id="DailySMSTable">
            <tr id="DailySMSTableHead">
                <th style="text-align:left;width:10%;">
                    <asp:Literal ID="Literal1" runat="server" Text="Session Num"></asp:Literal>
                </th>
                <th style="text-align:left;width:50%;">
                    <asp:Literal ID="Literal2" runat="server" Text="Session Description"></asp:Literal>
                </th>
                <th style="text-align:left;width:35%;">
                    <asp:Literal ID="Literal3" runat="server" Text="Daily SMS Content"></asp:Literal>
                </th>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>
