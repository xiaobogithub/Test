<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTPPS.aspx.cs" Inherits="ChangeTech.DeveloperWeb.CTPPSmartPhone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%= ProgramNameForTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="apple-touch-fullscreen" content="YES" />
    <meta name="viewport" content="width=480, user-scalable=yes, target-densitydpi=low-dpi;" />
    <link href="/Themes/mobile.css" rel="stylesheet" type="text/css" />
    <link href="/Themes/actionbuttonsforsmartphone.css" rel="stylesheet" type="text/css" />
    <link href="/Themes/added.css" rel="stylesheet" type="text/css" />
    <link <%= ("href=\"" + MobileBookmarkLink + "\"") %> rel="apple-touch-icon" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.2.min.js"></script>
    <script src="/Scripts/context_blender.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
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
    //Mark: this part js file/codes is not compatible with the JQuery Resize function. Till now, no solution.
    //Now in this smart phone version, it has no use. But in regular CTPP version, it is used for the login popup.
    --%>
    <script type="text/javascript">
        function DisplayLoginMenu() {
            var regularContentDiv = document.getElementById('regularContent');
            var loginDiv = document.getElementById('dialoguebox');
            regularContentDiv.style.display = "none";
            loginDiv.style.display = "block";
        }

        function HideLoginMenu() {
            var regularContentDiv = document.getElementById('regularContent');
            var loginDiv = document.getElementById('dialoguebox');
            regularContentDiv.style.display = "block";
            loginDiv.style.display = "none";
        }

        jQuery(document).ready(function ($) {
            var loginImageUrl = $("#programlogo").css("background-image");
            var protocol = window.location.protocol;
            if (loginImageUrl != "" && loginImageUrl != null && loginImageUrl !=undefined) {
                loginImageUrl = loginImageUrl.replace("http:", protocol);
            }
            $("#programlogo").css("background-image", loginImageUrl);
            var loginText = $("#<%=lblLoginTitle.ClientID %>").text();
            $("#loginLink").html(loginText);

            //debugger;
            $("#imgCont img").css({ height: $("#programwindow").height() + 'px' });

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
            var imgHeight = $("#programwindow").height();
            var containerWidth = $("#programwindow").width();
            var containerHeight = $("#programwindow").height();

            windowResize();

            onBlendTransparentImage();

            var isOverLoaded = false;
            var isUnderLoaded = false;
            function onBlendTransparentImage() {
                presenterImageLoaded();
                backgroundImageLoaded();
            }
            function windowResize() {
                jQuery(window).resize(function (e) {
                    containerWidth = $(window).width() * 0.9;
                    imgWidth = $("#over").width();
                    blend();
                });
            }
           
            function blendOnBothLoaded() {
                if (document.getElementById("over").complete && document.getElementById("under").complete) {
                    // $("img#over").load(function () {
                    if ($("#over").width() > 0) {
                        imgWidth = $("#over").width();
                        blend();
                    }
                    // });
                }
            }

            function blend() {
                var data = {}, contexts = {};
                var size;
                size = { width: imgWidth, height: imgHeight };
                var sizeUnder;
                sizeUnder = { width: containerWidth, height: containerHeight };
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
                    });
                }
                $('#over').attr('src', $("img#over").src);
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
                            blendOnBothLoaded();
                        }
                    });
                }
                $('#under').attr('src', '../Images/background.jpg');
                document.getElementById("under").onload = loadBackgroundImageHandler;
                if (document.getElementById('under').complete) {
                    loadBackgroundImageHandler();
                }
            }
        });

        
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
    <div id="wrapper_topline" runat="server">
        <div id="topline" runat="server">
            <div id="usermenu" runat="server" style="text-align: right; color: Red;">
                <asp:LoginView ID="LoginView" runat="server">
                    <LoggedInTemplate>
                        <asp:LoginName ID="LoginName" ForeColor="#d1e7fd" runat="server" />
                        <asp:LinkButton ID="logoutLnkBtn" ForeColor="#d1e7fd" Text="Logout" runat="server"
                            OnClick="btnLogout_Click" CausesValidation="false">
                        </asp:LinkButton>
                    </LoggedInTemplate>
                    <AnonymousTemplate>
                        <a id="loginLink" style="color: #d1e7fd;" onclick="DisplayLoginMenu();" href="javascript:void(0);">
                            Login</a>
                    </AnonymousTemplate>
                </asp:LoginView>
            </div>
            <div class="clear">
            </div>
            <div id="programlogo" runat="server" clientidmode="Static" class="logo">
            </div>
            <div id="programtitle" class="programtitle" runat="server" clientidmode="Static">
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div id="regularContent" runat="server" clientidmode="Static">
        <div id="wrapper-programwindow">
            <div id="programwindow">
                <div class="bubble-white" style="z-index:1;">
                    <h1 class="bubble-text" id="h1Header" runat="server">
                    </h1>
                    <p id="pDescription" runat="server">
                    </p>
                    <div class="bubble-white-arrow">
                    </div>
                </div>
                <div id="imgCont" runat="server" class="hostpic" style="z-index:0;">
                </div>
            </div>
        </div>
        <div class="container-default">
            <h2 id="Container_default_homescreen_Heading" runat="server">
            </h2>
            <p id="Container_default_homescreen_Text" runat="server">
            </p>
        </div>
        <div id="DivReportButtonArea" runat="server" clientidmode="Static">
            <h2 id="ReportButtonHeading" runat="server" clientidmode="Static">
            </h2>
            <p id="ReportButtonLinkArea" runat="server" clientidmode="Static">
            </p>
        </div>
        <div id="DivHelpButtonArea" runat="server" clientidmode="Static">
            <h2 id="HelpButtonHeading" runat="server" clientidmode="Static">
            </h2>
            <p id="HelpButtonLinkArea" runat="server" clientidmode="Static">
            </p>
        </div>
        <div class="container-default">
            <p id="Container_default_below_help_Text" runat="server">
            </p>
        </div>
    </div>

    <%--Login Div--%>
    <div id="dialoguebox" runat="server" clientidmode="Static" style="display: none">
        <a href="javascript:void(0);" onclick="HideLoginMenu();" id="linkClosed" runat="server"
            class="closewindow"></a>
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
        </div>
        <div class="loginbutton">
            <p>
                <asp:Button ID="loginbutton" runat="server" ClientIDMode="Static" Text="Log in" OnClick="btnLogin_Click"
                    CssClass="formbutton" />
            </p>
        </div>
    </div>
    <div class="container-default" style="display: none;">
        This space is available for other messages and/or marketing purposes</div>
    <div class="container-footer">
        &copy; 2012 Changetech</div>
    </form>
</body>
</html>
