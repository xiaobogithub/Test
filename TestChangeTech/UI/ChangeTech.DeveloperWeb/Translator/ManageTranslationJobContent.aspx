<%@ Page Title="" Language="C#" MasterPageFile="~/Translator/Translator.Master" AutoEventWireup="true" CodeBehind="ManageTranslationJobContent.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageTranslationJobContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.6.2.ct.translationjobcontent.js"></script>

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
            var translationJobGUID = GetArgsFromHref(window.location.href, "TranslationJobGUID");
            $('#translationjobcontent').translationjobcontent({
                translationJobGUID: translationJobGUID
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="header">
  <h1>Manage TranslationJob content overview</h1>
  <div class="headermenu">
  </div>
  <div class="clear"></div>
</div>   
    <div id="translationjobcontent" class="list">
        <table id="translationJobContents" width="90%">
            <tr id="translationJobContentHead">
                <th width="30%">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ resources:Session %>"></asp:Literal>
                </th>
                <th width="6%">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Elements %>"></asp:Literal>
                </th>
                <th width="6%">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:Words %>"></asp:Literal>
                </th>
                <th width="6%">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ resources:Completed %>"></asp:Literal>
                </th>
                <th width="42%">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ resources:Notes %>"></asp:Literal>
                </th>
                <th width="10%">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Action %>"></asp:Literal>
                </th>
            </tr>
        </table>
    </div>
</asp:Content>
