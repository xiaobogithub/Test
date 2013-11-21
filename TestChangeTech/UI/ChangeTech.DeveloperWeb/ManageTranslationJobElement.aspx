<%@ Page Title="" Language="C#" MasterPageFile="~/Translator.Master" AutoEventWireup="true"
    CodeBehind="ManageTranslationJobElement.aspx.cs" Inherits="ChangeTech.DeveloperWeb.ManageTranslationJobElement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Themes/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.6.2.ct.translationjobelement.js"></script>
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
            var translationJobContentGUID = GetArgsFromHref(window.location.href, "TranslationJobContentGUID");
            $('#translationjobelement').translationjobelement({
                translationJobContentGUID: translationJobContentGUID
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
            height: 150px;
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
        .emptyline
        {
            line-height: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="translationjobelement">
        <div id="validationsummary" class="error">
        </div>
        <table id="translationJobElements" width="90%">
            <tr id="translationJobElementHead">
                <th width="5%">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ resources:ElementNo %>"></asp:Literal>
                </th>
                <th width="10%">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ resources:Position %>"></asp:Literal>
                </th>
                <th width="25%">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ resources:Original %>"></asp:Literal>
                </th>
                <th width="5%">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ resources:Max %>"></asp:Literal>
                </th>
                <th width="25%">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ resources:GoogleTranslate %>"></asp:Literal>
                </th>
                <th width="25%">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ resources:Translated %>"></asp:Literal>
                </th>
            </tr>
        </table>
    </div>
</asp:Content>
