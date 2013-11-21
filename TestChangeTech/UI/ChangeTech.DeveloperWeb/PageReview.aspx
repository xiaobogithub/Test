<%@ Page Title="" Language="C#" MasterPageFile="~/TempSite.Master"  AutoEventWireup="true"
    CodeBehind="PageReview.aspx.cs" Inherits="ChangeTech.DeveloperWeb.PageReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.6.2.ct.pagereview.js"></script>
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
            var sessionguid = GetArgsFromHref(window.location.href, "SessionGUID");
            var programpage = GetArgsFromHref(window.location.href, "ProgramPg");
            var sessionpage = GetArgsFromHref(window.location.href, "SessionPg");
            var userguid = GetArgsFromHref(window.location.href, "UserGUID");

            $('#pagereview').pagereview({
                sessionGuid: sessionguid,
                programPage: programpage,
                sessionPage: sessionpage,
                userGuid: userguid
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
            width: 90%;
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
    <asp:HyperLink ID="hpProgramLink" runat="server" Text="<%$ Resources: Share, Programs%>"
        ToolTip="<%$ Resources: Share, GoPrograms%>"></asp:HyperLink>
        </li>
        <li>
    <asp:HyperLink ID="hpEditProgramLink" runat="server" Text="<%$ Resources: Share, EditProgram%>"
        ToolTip="<%$ Resources: Share, GoEditProgram%>"></asp:HyperLink>
        </li>
        <li>
        <span class="lastlinode">
        <asp:Literal ID="ltlPageReview" Text="<%$ Resources:Share, PageReview%>" runat="server"></asp:Literal>
    </span>
    </li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="alertmessage hidden" id="errorMsgDiv">Update failed!</div>
<div class="confirmationmessage hidden" id="successMsgDiv">The page is updated!</div>
<div class="header">
  <h1>Day review</h1>
  <div class="headermenu">
   <asp:Button ID="btnAddSeq" runat="server" CssClass="button-add" Text="<%$ resources:AddPageSequence %>" OnClick="btnAddSeq_Click"/>
  </div>
  <div class="clear"></div>
</div>
    <div id="pagereview" class="list">
        <table id="pageContents">
            <tr id="pageContentsHead">
                <th  style="width:5%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal1" runat="server" Text="Seq"></asp:Literal>
                </th>
                <th style="width:5%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal2" runat="server" Text="Page"></asp:Literal>
                </th>
                <th style="width:10%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal6" runat="server" Text="Image"></asp:Literal>
                </th>
                <th style="width:40%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal3" runat="server" Text="Heading & body & button"></asp:Literal>
                </th>
                <th style="width:10%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal7" runat="server" Text="Before"></asp:Literal>
                </th>
                <th style="width:10%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal8" runat="server" Text="After"></asp:Literal>
                </th>
                <th style="width:10%; text-align:left; padding-left:10px;"">
                    <asp:Literal ID="Literal9" runat="server" Text="Template"></asp:Literal>
                </th>
                <th style="width:10%;">
                </th>
            </tr>
        </table>
    </div>
    <div id="result" style="display: none" title="New Page">
        <table>
            <tr>
                <td>
                    Template:
                </td>
                <td>
                    <select id="template" style="width: 90%">
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Heading:
                </td>
                <td>
                    <input id="heading" type="text" />
                </td>
            </tr>
            <tr>
                <td>
                    Body:
                </td>
                <td>
                    <textarea cols="50" rows="6" id="body"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    Primary button name:
                </td>
                <td>
                    <input id="primarybuttonname" type="text" />
                </td>
            </tr>
        </table>
    </div>
    <div id="presenterimage" style="display: none" title="Presenter Image">
        <iframe id="imagemanager" frameborder="0" height="100%" width="100%" src=""><%--ImageManager.aspx--%>
        </iframe>
    </div>
</asp:Content>
