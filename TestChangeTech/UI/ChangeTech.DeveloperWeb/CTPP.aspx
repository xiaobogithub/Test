<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTPP.aspx.cs" Inherits="ChangeTech.DeveloperWeb.CTPP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%= ProgramNameForTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="/Themes/ctpp-screen7.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="/jwplayer/jwplayer.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.6.2.min.js"></script>
    <link rel="stylesheet" href="/Themes/dialoguebox.css" type="text/css" media="screen" />
    <link rel="Stylesheet" href="Themes/actionbuttons.css" type="text/css" media="screen" />
    <script src="/Scripts/context_blender.js" type="text/javascript"></script>
    <script type="text/javascript">
        //var GB_ROOT_DIR = "http://www.newme.no/greybox/";
        //debugger;
        var GB_ROOT_DIR = getRootPath() + "/NewMe/";   //"http://localhost:41265/NewMe/";
        function getRootPath() {
            var strFullPath = window.document.location.href;
            var strPath = window.document.location.pathname;
            var pos = strFullPath.indexOf(strPath);
            var prePath = strFullPath.substring(0, pos);
            return prePath;
        }
    </script>
    <link href="/Themes/gb_styles.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="/Scripts/AJS.js" type="text/javascript"></script>
    <script src="/Scripts/AJS_fx.js" type="text/javascript"></script>
    <script src="/Scripts/gb_scripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var isOverLoaded = false;
        var isUnderLoaded = false;
        jQuery(document).ready(function ($) {

            $("#forgotPwdLinkA").click(function () {
                (function ($) {
                    $find('LoginModelDia').hide();
                    $find('ForgotPwdDia').show();
                })(jQuery);
            });

            var browserVersion = $.browser.version;
            var isIE = $.browser.msie;
            var loginImageUrl = $("#programlogo img").attr("src");
            var protocol = window.location.protocol;
            if (loginImageUrl != "" && loginImageUrl != undefined && loginImageUrl != null) {
                loginImageUrl = loginImageUrl.replace("http:", protocol);
            }
            $("#programlogo img").attr("src", loginImageUrl);
            var loginText = $("#<%=lblLoginTitle.ClientID %>").text();
            $("#loginLink").html(loginText);
            //debugger;
            $("#imgCont img").css({ height: $("#box").height() + 'px' });

            //program sub color
            var subColor = '<%= ProgramSubColor %>';
            if (subColor != "") {
                $("#usermenu").children().css("color", subColor);
            }
            else {
                $("#usermenu").children().css("color", "#d1e7fd");
            }

            //draw the presenter image with background

            var imgWidth = $("#over").width();
            var imgHeight = $("#over").height();
            if (isIE && (browserVersion == "8.0" || browserVersion == "7.0")) {

                if ($("#over")) {
                    $("#over").css("display", "block");
                }
                if ($("#out")) {
                    $("#out").css("display", "none");
                }
                $("#imgCont").css({ "background-color": "#ffffff", "margin-left": "10px", "-webkit-border-radius": "0 0 0 5px", "-moz-border-radius": "0 0 0 5px", "border-radius": "0 0 0 5px", "width": "1070px" });
            } else {
                onBlendTransparentImage();
            }


            //Set default open/close settings
            $('.acc_container').hide(); //Hide/close all containers
            //$('.acc_trigger:first').addClass('active').next().show(); //Add "active" class to first trigger, then show/open the immediate next container

            //On Click
            $('.acc_trigger').click(function () {
                if ($(this).next().is(':hidden')) { //If immediate next container is closed...
                    $('.acc_trigger').removeClass('active').next().slideUp(); //Remove all .acc_trigger classes and slide up the immediate next container
                    $(this).toggleClass('active').next().slideDown(); //Add .acc_trigger class to clicked trigger and slide down the immediate next container
                }
                return false; //Prevent the browser jump to the link anchor
            });

            //Set default open/close settings
            $('.acc_container_grey').hide(); //Hide/close all containers
            //$('.acc_trigger_grey:first').addClass('active').next().show(); //Add "active" class to first trigger, then show/open the immediate next container

            //On Click
            $('.acc_trigger_grey').click(function () {
                if ($(this).next().is(':hidden')) { //If immediate next container is closed...
                    $('.acc_trigger_grey').removeClass('active').next().slideUp(); //Remove all .acc_trigger classes and slide up the immediate next container
                    $(this).toggleClass('active').next().slideDown(); //Add .acc_trigger class to clicked trigger and slide down the immediate next container
                }
                return false; //Prevent the browser jump to the link anchor
            });

            function onBlendTransparentImage() {
                presenterImageLoaded();
                backgroundImageLoaded();
            }
            function blendOnBothLoaded() {
                console.log("overCompleteOutblendOnBothLoaded:" + document.getElementById("over").complete);
                console.log("underCompleteOutblendOnBothLoaded:" + document.getElementById("under").complete);
                if (document.getElementById("over").complete && document.getElementById("under").complete) {
                    // $("img#over").load(function () {
                    //debugger;
                    console.log("overCompleteInblendOnBothLoaded:" + document.getElementById("over").complete);
                    console.log("underCompleteInblendOnBothLoaded:" + document.getElementById("under").complete);
                    if ($("#over").width() > 0) {
                        imgWidth = $("#over").width();
                    }
                    blend();
                    //  });
                    //$('#backgroundpic' ).attr('src', presenterImageUrl);
                    //$('#over').attr('src', '../Images/abc.jpg');
                    //$('#under').attr('src', '../Images/under01.png');
                }
            }
            function blend() {
                //debugger;
                var data = {}, contexts = {};
                var size;
                size = { width: imgWidth, height: imgHeight };
                var sizeUnder;
                sizeUnder = { width: 1070, height: imgHeight };
                var blendSize = {
                    destX: 10,
                    destY: 0,
                    sourceX: 0,
                    sourceY: 0,
                    width: size.width,
                    height: size.height
                }
                $.each(['over'], function (i, s) {
                    var canvas = $('<canvas>').attr(size)[0];
                    var ctx = contexts[s] = canvas.getContext('2d');
                    drawImage(ctx, s, size);
                });

                $.each(['under'], function (i, s) {
                    var canvas = $('<canvas>').attr(sizeUnder)[0];
                    var ctx = contexts[s] = canvas.getContext('2d');
                    drawImage(ctx, s, sizeUnder);
                });


                $.each(['out'], function (i, s) {
                    contexts[s] = $('#' + s).attr(sizeUnder)[0].getContext('2d');
                });
                $(contexts.out.canvas).attr(sizeUnder);
                drawImage(contexts.out, 'under', sizeUnder);
                contexts.over.blendOnto(contexts.out, 'multiply', blendSize);
            }
            function drawImage(ctx, imgOrId, imageSize) {

                if (typeof imgOrId == 'string') {
                    ctx.drawImage($('#' + imgOrId)[0], 0, 0, imageSize.width, imageSize.height);
                } else {
                    ctx.drawImage(imgOrId, 0, 0, imageSize.width, imageSize.height);
                }
            }
            function presenterImageLoaded() {
                var loaded = false;
                function loadPresenterImageHandler() {
                    if (loaded) {
                        return;
                    }
                    loaded = true;
                    setTimeout(function () {
                        isOverLoaded = true;
                        if (isUnderLoaded) {
                            blendOnBothLoaded();
                        }
                    }, 100);
                }
                var overImage = new Image();
                overImage.onload = function () {
                    console.log("overImage:" + overImage.complete + "/ RedayState" + overImage.readyState);
                }
                $('#over').attr('src', $("img#over").attr("src"));
                document.getElementById('over').onload = loadPresenterImageHandler;

                if (document.getElementById('over').complete) {
                    loadPresenterImageHandler();
                }
            }
            function backgroundImageLoaded() {
                var loaded = false;
                function loadBackgroundImageHandler() {
                    if (loaded) {
                        return;
                    }
                    loaded = true;
                    setTimeout(function () {
                        isUnderLoaded = true;
                        if (isOverLoaded) {
                            console.log("overCompleteInloadBackgroundImageHandler:" + document.getElementById('over').complete + "/ RedayState" + document.getElementById('over').readyState);
                            console.log("underCompleteInloadBackgroundImageHandler:" + document.getElementById('under').complete + "/ RedayState" + document.getElementById('under').readyState);
                            blendOnBothLoaded();
                        }
                    }, 100);
                }
                var underImage = new Image();
                underImage.onload = function () {
                    console.log("underImage:" + underImage.complete + "/ RedayState" + underImage.readyState);
                }
                $('#under').attr('src', '../Images/background.jpg');
                document.getElementById("under").onload = loadBackgroundImageHandler;
                if (document.getElementById("under").complete) {
                    loadBackgroundImageHandler();
                }
            }
        });


        function PopupLoginMenu() {
            (function ($) {
                $find('LoginModelDia').show();
            })(jQuery);
        }

        function playVideo(path, mediaName) {
            //debugger;
            (function ($) {
                $("#divMediaBubble").css('display', 'block');
                $("#hMediaName").html(mediaName.toString());

                //jwplayer("jwmediaplayer").setup({
                jwplayer("jwmediaplayer").setup({
                    //jwplayer("Div2").setup({
                    file: path.toString(),
                    skin: "/jwplayer/skin/video/changetech_video.zip",
                    flashplayer: "/jwplayer/player.swf",
                    width: 502,
                    height: 314,
                    controlbar: "bottom",
                    autostart: true
                });
                $find('MediaModal').show();
            })(jQuery);
        }

        function playVideoBox(path, mediaName) {
            jwplayer("videoplayerInBox").setup({
                file: path.toString(),
                skin: "/jwplayer/skin/video/changetech_video.zip",
                flashplayer: "/jwplayer/player.swf",
                width: 320,
                height: 180,
                controlbar: "bottom",
                autostart: true
            });
        }

        function playAudio(path, mediaName) {
            //debugger;
            (function ($) {
                $("#divMediaBubble").css('display', 'block');
                $("#hMediaName").html(mediaName.toString());

                jwplayer("jwmediaplayer").setup({
                    //file: "/RequestResource.aspx?target=Video&media=e1377a16-7770-c460-7ba2-86ba12e7d8be.flv&name=DinForEvig_4.flv",
                    //file: "/jwplayer/DinForEvig_3v2.flv",
                    file: path.toString(),
                    skin: "/jwplayer/skin/audio/changetech_audio.zip",
                    flashplayer: "/jwplayer/player.swf",
                    width: 502,
                    height: 30,
                    icons: false,
                    controlbar: "bottom",
                    autostart: true
                });
                $find('MediaModal').show();
            })(jQuery);
        }
        function stopMedia() {
            //debugger;
            jwplayer("jwmediaplayer").remove();
        }
    </script>
    <script type="text/javascript">
        //Google Analytics code
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-28200226-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</head>
<body>
    <form id="form1" runat="server" action="###">
    <div id="fb-root">
    </div>
    <script type="text/javascript">
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));
    </script>
    <Ajax:ToolkitScriptManager runat="server" ID="toolkitScriptManage1">
    </Ajax:ToolkitScriptManager>
    <asp:HiddenField ID="LoginHiddenControl" runat="server" />
    <%--Login popup--%>
    <Ajax:ModalPopupExtender ID="LoginModelDia" runat="server" PopupControlID="dialoguebox"
        TargetControlID="LoginHiddenControl" CancelControlID="linkClosed" BackgroundCssClass="modalBackground"
        CacheDynamicResults="true">
    </Ajax:ModalPopupExtender>
    <Ajax:ModalPopupExtender ID="MediaModal" runat="server" PopupControlID="divMediaBubble"
        TargetControlID="LoginHiddenControl" CancelControlID="mediaClose" BackgroundCssClass="modalBackground"
        CacheDynamicResults="true">
    </Ajax:ModalPopupExtender>
    <%--forgotten pwd popup--%>
    <Ajax:ModalPopupExtender ID="ForgotPwdDia" runat="server" PopupControlID="forgotPwdDialoguebox"
        TargetControlID="LoginHiddenControl" CancelControlID="forgotPwdLinkClosed" BackgroundCssClass="modalBackground"
        CacheDynamicResults="true">
    </Ajax:ModalPopupExtender>
    <div id="programpagebar">
        <div id="programribbon_program01" runat="server">
            <div id="globalmenu">
                <ul>
                    <li>
                        <asp:HyperLink ID="ForSideLink" runat="server">Forside</asp:HyperLink>
                    </li>
                </ul>
            </div>
            <div id="usermenu" runat="server" style="text-align: right; color: Red;">
                <asp:LoginView ID="LoginView" runat="server">
                    <LoggedInTemplate>
                        <asp:LoginName ID="LoginName" ForeColor="#d1e7fd" runat="server" />
                        <asp:LinkButton ID="logoutLnkBtn" ForeColor="#d1e7fd" Text='Logout' runat="server"
                            OnClick="btnLogout_Click" CausesValidation="false" >
                        </asp:LinkButton>
                    </LoggedInTemplate>
                    <AnonymousTemplate>
                        <a id="loginLink" style="color: #d1e7fd;" onclick="PopupLoginMenu()" href="###">
                        Login
                        </a>
                    </AnonymousTemplate>
                </asp:LoginView>
            </div>
            <div class="clear">
            </div>
            <div id="programlogo" style="width: 111;">
                <img alt='' src="" runat="server" id="imgProgramLogo" /></div>
            <div id="programtitle" runat="server">
                <h1 runat="server" id="h1ProgramName">
                </h1>
                <h2>
                    <asp:Label ID="programSubheadingInCTPPlbl" runat="server"></asp:Label></h2>
            </div>
            <%--<div style="text-align: right; margin-right: 20px;">
                <asp:Button ID="btnHelpRelapseButton" runat="server" Visible="false" ForeColor="Black"
                    Text="Help" OnClick="btnHelpRelapseButton_Click" /><br />
                <asp:Button ID="btnReportRelapseButton" runat="server" Visible="false" ForeColor="Black"
                    Text="Report" OnClick="btnReportRelapseButton_Click" />
            </div>--%>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="box">
        <div class="clear">
        </div>
        <div id="imgCont" runat="server" style="padding-left: 10px;">
            <%--<img alt='' id="over" src="../RequestResource.aspx?target=Image&media=b81aa07c-c1fb-4142-9b8a-593a06b69dd4.png" style="display:none"/>
            <img alt='' id="over" src="###" style="display:none"/>
            <img alt='' id="under" src="../Images/bg-programwindow.png" style="display:none;"/>
            <canvas id="out" ></canvas>--%>
        </div>
        <div id="bubble-white">
            <div class="bubble-white">
                <h1 class="bubble-text" id="h1Header" runat="server">
                </h1>
                <p class="emptyline">
                    &nbsp;</p>
                <p class="bubble-text" id="pDescription" runat="server">
                </p>
                <div class="bubble-white-arrow">
                </div>
            </div>
        </div>
        <div class="pricebubble-top" runat="server" id="priceInfo" style="width: 10; position: relative;">
            <div class="wrapper-cta-big">
                <asp:HyperLink ID="BuyLink" runat="server" CssClass="cta-big">
                    <asp:Label runat="server" ID="lblBuy" Text="Meld deg på"></asp:Label>
                    <div class="cta-price">
                        <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label></div>
                </asp:HyperLink>
            </div>
            <p class="clear">
            </p>
            <p>
                <asp:HyperLink ID="BuySubLink"  runat="server"></asp:HyperLink>
            </p>
        </div>
        <div id="programwindow-right">
            <div id="container_facts" runat="server" clientidmode="Static">
                <h5 id="h5Fact1Header" runat="server">
                    Varighet</h5>
                <div class="description" id="divFact1Content" runat="server">
                    6 uker, 3 sesjoner per uke</div>
                <h5 id="h5Fact2Header" runat="server">
                    Oppstartstid</h5>
                <div class="description" id="divFact2Content" runat="server">
                    Mandager</div>
                <h5 id="h5Fact3Header" runat="server">
                    Pris</h5>
                <div class="description" id="divFact3Content" runat="server">
                    Kr 235,- for hele programmet</div>
                <h5 id="h5Fact4Header" runat="server">
                    Kanaler</h5>
                <div class="description" id="divFact4Content" runat="server">
                    e-post og web</div>
                <div class="introvideo" runat="server" id="videoArea">
                    <asp:HyperLink ID="hplinkImageForVideo" href="###" CssClass="introvideo-thumb" runat="server">
                    </asp:HyperLink>
                    <h3 runat="server" id="VideoSubH">
                    </h3>
                    <p runat="server" id="VideoSubP">
                    </p>
                </div>
            </div>
            <div id="DivReportButtonArea" runat="server" clientidmode="Static">
                <h2 id="ReportButtonHeading" runat="server" clientidmode="Static">
                    <%--Hvordan har du det?--%>
                </h2>
                <p id="ReportButtonLinkArea" runat="server" clientidmode="Static" >
                    <%--<a href="#" class="b-checkin" >Report here!</a>--%></p>
            </div>
            <div id="DivHelpButtonArea" runat="server" clientidmode="Static">
                <h2 id="HelpButtonHeading" runat="server" clientidmode="Static">
                    <%--I ferd med å sprekke?--%>
                </h2>
                <p id="HelpButtonLinkArea" runat="server" clientidmode="Static">
                    <%--<a href="#" class="b-emergency">Get help!</a>--%></p>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
    </div>
    <div id="container-schedule-header">
        <div id="schedule-header">
            <h1>
                <asp:Label runat="server" ID="lblDaysinprogram" Text=""></asp:Label>
            </h1>
            <p>
                <asp:Label runat="server" ID="lblClickaday" Text=""></asp:Label>
            </p>
        </div>
        <div id="socialsharing">
            <%--<asp:HyperLink ID="googleLnk" Visible="false" runat="server" Target="_blank">
                    <asp:Image ID="ibGoogleNew" runat="server" ImageUrl="~/Images/googleplus.jpg" Height="20"
                        ToolTip="Share this" />
                </asp:HyperLink>--%>
            <%--<asp:HyperLink ID="fackbookLnk" runat="server" Target="_blank">
                    <asp:Image ID="ibFacebookNew" runat="server" ImageUrl="~/Images/like.png" Height="20"
                        Width="92" ToolTip="Share this!" />
                </asp:HyperLink>--%>
            <div class="facebook-like">
                <div class="fb-like" runat="server" id="fblikeDiv" data-href="https://www.newme.no/"
                    data-send="false" data-width="320" data-show-faces="false">
                </div>
            </div>
            <%--<asp:HyperLink ID="twitterLnk" Visible="false" runat="server" Target="_blank">
                    <asp:Image ID="ibTwitterNew" runat="server" ImageUrl="~/Images/twitter.jpg" Height="20"
                        ToolTip="Share this" />
                </asp:HyperLink>--%>
            <%--<p>
                <span runat="server" id="spBeforeFB"></span>
                <asp:HyperLink ID="fackbookSiderLink" runat="server" Target="_blank">
                    Facebook sider
                </asp:HyperLink>
            </p>--%>
        </div>
        <div class="clear">
        </div>
        <div class="container-schedule-header-border-part1">
        </div>
        <div class="container-schedule-header-border-part2">
        </div>
        <div class="container-schedule-header-border-part3">
        </div>
    </div>
    <div id="container-main">
        <div id="container-schedule">
            <div class="container-taken-box">
                <asp:Repeater ID="rpSessions" runat="server" OnItemDataBound="rpSessions_ItemDataBound"
                    OnItemCommand="rpSessions_ItemCommand">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="hiddenSession" Visible="false" Text='<%# Eval("ID")%>' />
                        <asp:Button runat="server" ID="hiddenSessionDayNum" Visible="false" Text='<%# Eval("Day")%>' />
                        <asp:Button runat="server" ID="hiddenSessionIsHasDone" Visible="false" Text='<%# Eval("IsHasDone")%>' />
                        <%#Eval("DIVSTRStart")%>
                        <div class="acc_trigger day-title" runat="server" id="dayTitle">
                            <div class="day-number">
                                <%# Eval("Day")%></div>
                            <a href="#" class="tabtitle">
                                <%# Eval("Name")%></a>
                            <div class="outer-day-label">
                                <asp:Label ID="lblCompleted" CssClass="day-label-completed" runat="server" Text="Completed"></asp:Label>
                            </div>
                        </div>
                        <div class="acc_container" runat="server" id="accContainer">
                            <div class="block">
                                <table width="610" border="0">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <ul>
                                                    <li>
                                                        <asp:Button ID="btnOpenDay" CssClass="openday" CommandName="OpenDay" CommandArgument='<%# Eval("Day") %>'
                                                            runat="server" Text="Not available" Enabled="false" />
                                                    </li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="day-infotext" valign="top" style="width: 290">
                                                <p>
                                                    <%# Eval("Description")%>
                                                </p>
                                            </td>
                                            <td valign="top" style="width: 310px;">
                                                <asp:Repeater ID="rpDownloadList" runat="server">
                                                    <ItemTemplate>
                                                        <ul>
                                                            <%# ((string)Container.DataItem)%>
                                                        </ul>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <%#Eval("DIVSTREnd")%>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <%--<center>
                <%=PagingString %></center>--%>
            <div class="clear">
            </div>
            <div class="pricebubble-bottom" runat="server" id="priceInfo2">
                <div class="cta-demo">
                    <asp:HyperLink ID="BuySubLink2" runat="server"></asp:HyperLink>
                </div>
                <div class="wrapper-cta-big">
                    <asp:HyperLink ID="BuyLink2" runat="server" CssClass="cta-big">
                        <asp:Label runat="server" ID="lblBuy2" Text="Meld deg på"></asp:Label>
                        <div class="cta-price">
                            <asp:Label ID="lblPrice2" runat="server" Text=""></asp:Label></div>
                    </asp:HyperLink>
                </div>
            </div>
        </div>
        <div id="container-client">
            <div>
                <asp:HyperLink ID="hlPromotion1" runat="server" CssClass="container-ad" Target="_self">
                </asp:HyperLink>
            </div>
            <div><%--rel="gb_page_center[650, 400]"--%>
                <asp:HyperLink ID="hlPromotion2" runat="server" CssClass="container-ad" Target="_self">
                </asp:HyperLink>
            </div>
            <div class="container-smallprogramlist">
                <ul>
                    <li class="smallprogramlist">
                        <h2>
                            <asp:Label ID="lblBrandName" runat="server" Text=""></asp:Label>
                        </h2>
                    </li>
                    <asp:ListView ID="listOtherCTPP" runat="server" OnItemDataBound="listOtherCTPP_ItemDataBound"
                        OnItemCommand="listOtherCTPP_ItemCommand">
                        <ItemTemplate>
                            <li class="programlist" runat="server" id="liOtherOldRow">
                                <asp:LinkButton runat="server" ID="lbOtherList" CommandName="otherCTPPClick" CommandArgument='<%# Eval("ProgramGUID")%>'>
                                    <div id="divOtherProgram1" class="programlist-thumbnail" runat="server">
                                        <asp:Label ID="lblCTPPGuid" runat="server" Visible="false" Text='<%# Eval("ProgramGUID")%>'></asp:Label>
                                        <asp:Image ID="otherCTPPLogo" runat="server" ImageAlign="Middle" Width="50px" Height="40px" />
                                    </div>
                                    <div class="programtitle" id="divOtherProgramName" runat="server">
                                        <%# Eval("ProgramName")%></div>
                                    <div class="slogan" id="divOtherProgramSub" runat="server">
                                        <%# Eval("ProgramSubheading")%></div>
                                </asp:LinkButton>
                            </li>
                            <li class="smallprogramlist" runat="server" id="liOtherNewRow">
                                <asp:LinkButton runat="server" ID="lbOtherList2" CommandName="otherCTPPClick2" CommandArgument='<%# Eval("ProgramGUID")%>'>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="footer">
            <img alt='' src="" runat="server" id="imgBrandLogo" style="width: auto; height: 20px;"
                class="logo-newme-footer" />
            <asp:HyperLink ID="facebookBottomLink" runat="server" Target="_blank" CssClass="logo-facebook-footer">
            </asp:HyperLink>
            <asp:Label ID="lblProgramName" runat="server" Text="[This program]"></asp:Label>
            <asp:Label ID="lblProvidedBy" runat="server" Text="is a service provided by"></asp:Label>
            <asp:Label ID="lblBrandNameBottom" runat="server" Text="[Company]"></asp:Label>
            <asp:Label ID="lblUseFrom" runat="server" Text="using EasyChange Technology from"></asp:Label>
            <a href="https://changetech.no">Changetech</a>. © 2011
            <asp:Label ID="lblBrandNameBottom2" runat="server" Text="[Company]"></asp:Label>
            <asp:Label ID="lblAllRightsReserved" runat="server" Text="All rights reserved."></asp:Label>
        </div>
    </div>
    <!-- Login Dialog -->
    <div id="dialoguebox" runat="server" style="display: none">
        <a href="#" id="linkClosed" runat="server" class="closewindow"></a>
        <div class="logscheme">
            <h2>
                 <asp:Label ID="lblLoginTitle" runat="server" Text="Log in" ></asp:Label></h2>
            <p class="emptyline">
                &nbsp;</p>
            <asp:Label runat="server" ID="lblWrongInfo" ForeColor="Red" Visible="false"></asp:Label>
            <p>
                 <asp:Label ID="lblUserName" runat="server" Text="UserName"></asp:Label></p>
            <label for="username">
            </label>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="textfield"></asp:TextBox>
            <p class="emptyline">
                &nbsp;</p>
            <p>
                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label></p>
            <p>
                <label for="password">
                </label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textfield"></asp:TextBox>
            </p>
            <p class="emptyline">
                &nbsp;</p>
            <p>
               <%-- <asp:HyperLink ID="forgotPwdLink" runat="server">Forgotten password?</asp:HyperLink>--%>
                <a id="forgotPwdLinkA" style="cursor:pointer;" runat="server">Forgotten password?</a>
            </p>
        </div>
        <div class="logbutton">
            <p>
                <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_Click" CssClass="formbutton" />
            </p>
        </div>
    </div>

     <!-- ForgotPassword Dialog -->
    <div id="forgotPwdDialoguebox" runat="server" style="display: none;">
        <a id="forgotPwdLinkClosed" runat="server" class="closewindow" href="#"></a>
        <div class="logscheme">
            <h2>
                 <asp:Label ID="lblForgotPwd" runat="server" Text="Forgotten password" ></asp:Label></h2>
            <p class="emptyline">
                &nbsp;</p>
            <asp:Label runat="server" ID="lblForgotPwdWrongInfo" ForeColor="Red" Visible="false"></asp:Label>
            <p>
                 <asp:Label ID="lblForgotPwdUserName" runat="server" Text="UserName"></asp:Label></p>
            <label for="username">
            </label>
            <asp:TextBox ID="txtForgotPwdUserName" runat="server" CssClass="textfield"></asp:TextBox>
            <p class="emptyline">&nbsp;</p>
            <%--<p><asp:Label ID="lblMobilePhone" runat="server" Text="Mobile"></asp:Label></p>
            <p>
                <label for="mobilephone">
                </label>
                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="textfield"></asp:TextBox>
            </p>--%>
        </div>
        <div class="logbutton">
            <p>
                <asp:Button ID="btnForgotPassword" runat="server" Text="Send" OnClick="btnForgotPassword_Click" Width="250px" CssClass="formbutton" />
            </p>
        </div>
    </div>

    <div class="bubble-black" id="divMediaBubble" runat="server" style="display: none">
        <a href="javascript:void(0);" id="mediaClose" title="Close (Esc)" onclick="stopMedia();"
            class="mediaclosewindow"></a>
        <h1 id="hMediaName">
            Name
        </h1>
        <div id="jwmediaplayer">
            load...
        </div>
    </div>
    </form>
</body>
</html>