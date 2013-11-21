<%@ Page Title="" Language="C#" MasterPageFile="Monitor.Master" AutoEventWireup="true"
    CodeBehind="ListUserLog.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ListUserLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.6.2.ct.programuserreport.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var timerId = 0;
            var timeInterval = 600000;
            $('#programuserreport').programuserreport({
            });
            timerId = setInterval(function () {
                CT.Program.GetProgramUserReport(null, function (widget, data) {
                    $('#programuserreport').data('programuserreport').loadProgramUserReport(data);
                });
            }, timeInterval);
            $('#chkUpdate').click(function () {
                if ($('#chkUpdate').attr('checked') != "checked")
                    clearInterval(timerId);
                else
                    timerId = setInterval(function () {
                        CT.Program.GetProgramUserReport(null, function (widget, data) {
                            $('#programuserreport').data('programuserreport').loadProgramUserReport(data);
                        });
                    }, timeInterval);
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="header">
  <h1>Userlog list overview</h1>
  <div class="headermenu"></div>
  <div class="clear"></div>
</div>
    <input id="chkUpdate" type="checkbox" checked="checked" />Update per 10 minutes
    <div id="programuserreport" class="list">
        <table id="reportContents">
            <tr id="reportContentsHead">
                <th>
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Program %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ resources:AllUserSV %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:NotCompleteSCR %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ resources:CompleteSCR %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Registered %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ resources:UsersInProg %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ resources:Completed %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ resources:Terminated %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ resources:RegisteredLast24Hours %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ resources:RegisteredLastWeek %>"></asp:Literal>
                </th>
                <th>
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ resources:RegisteredLastMonth %>"></asp:Literal>
                </th>
            </tr>
        </table>
    </div>
</asp:Content>
