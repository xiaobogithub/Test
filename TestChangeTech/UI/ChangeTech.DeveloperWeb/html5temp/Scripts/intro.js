function loadIntroPage(ctppXmlobj, callback) {
    var hiddenElementStr = '<div id="content">' +
			   '<div id="orange" class="orange" style="display:none;"></div>';
    $('#hiddenLoadingInfo').empty().append($(hiddenElementStr));
    var logoImage = $(ctppXmlobj).attr("LogoName");
    // var logoUrl = "http://changetechstorage.blob.core.windows.net/logocontainer/" + logoImage;
    var logoUrl = getURL("logocontainerRoot") + logoImage;
    var programTitle = $(ctppXmlobj).attr("CTPPName");
    var programName = $(ctppXmlobj).attr("ProgramName");
    var subheading = $(ctppXmlobj).attr("CTPPSubheading");
    myplatform = $(ctppXmlobj).attr("Platform");
    if ( programName != null && programName != "" && programName != undefined )
    {
        $( "title" ).text( programName.toUpperCase() );
    }
    var inforStr = "<div class=introCon><div class='progLogo'></div>" +
			           "<div class='info'><div class='innerInfo'></div></div></div>" +
		               "<div class='loading'><p><img src='Images/wait-anim-small.gif'></p></div>";

    $("#orange").append(inforStr);
    if (logoImage != '' && logoImage != null && logoImage != undefined) {
        $(".progLogo").html("<a href=''><img src=" + logoUrl + " alt='' /></a>");
    }
    if (programTitle != '' && programTitle != null && programTitle != undefined) {
        $(".innerInfo").append("<h1>" + programTitle + "</h1>");
    } else if (programName != '' && programName != null && programName != undefined) {
        $(".innerInfo").append("<h1>" + programName + "</h1>");
    }
    if (subheading != null && subheading != undefined && subheading != '') {
        $(".innerInfo").append("<p>" + subheading + "</p>");
    }


    bubbleOrangeWidth = $("#orange").outerWidth(true);
    $("#introBubble").css({ width: "auto" });
    $("#introBubble").animate({ "margin-left": "-" + bubbleOrangeWidth / 2 + "px", left: "50%" }, 200);
    setTimeout(backgroundFadeIn, 100);
    if (callback && typeof (callback) == "function") {
        callback();
    }
}
function loadInroContent() {
    if ($('#orange').html() != "") {
        setOrangeBubble('#orange');
    }
    else {
        if ($('#bubble-orange').html() != "")
            setOrangeBubble('FadeOut');
        else
            setOrangeBubble('');
    }

}

function backgroundFadeIn() {
    var logoImage = $(ctppXmlobj).attr("LogoName");
    var programTitle = $(ctppXmlobj).attr("CTPPName");
    var programName = $(ctppXmlobj).attr("ProgramName");
    var subheading = $(ctppXmlobj).attr("CTPPSubheading");
    $('body').removeClass('bgblack').addClass('bggradient').delay(100).fadeIn(200, function () {
        loadInroContent();
        var startColor = $(ctppXmlobj).attr("CTPPTopBackLightColor");
        if (startColor == undefined || startColor == "") {
            startColor = "#5b93c7";
        }
        var endColor = $(ctppXmlobj).attr("CTPPTopBackDarkColor");
        if (endColor == undefined || endColor == '') {
            endColor = "#3e6e9a";
        }
        var subHeadingColor = $(ctppXmlobj).attr("CTPPTopSubheadColor");
        if (subHeadingColor == undefined || subHeadingColor == "") {
            subHeadingColor = "#d1e7fd";
        }
        if (subHeadingColor != null && subHeadingColor != undefined && subHeadingColor != '') {
            $(".info .innerInfo p").css({ "color": subHeadingColor });
            if (logoImage != null && logoImage != undefined && logoImage != '') {
                $(".info .innerInfo").css({ "border-left-width": "2px", "border-left-style": "dotted", "border-left-color": subHeadingColor });
            } else {
                $(".progLogo").css({ "padding-right": "0px" });
                $(".info .innerInfo").css({ "border-left": "none", "padding-left": "0px" });
            }
        }
        $("#bubble-orange .introCon").attr("style", "background:" + endColor + ";background:-webkit-gradient(linear, left top, left bottom, from(" + startColor + "), to(" + endColor + "));background:-moz-linear-gradient(top, " + startColor + ", " + endColor + ");background:-webkit-linear-gradient(top, " + startColor + ", " + endColor + ");background-o-linear-gradient(top, " + startColor + ", " + endColor + ")");
    });
}
function programBarAnimation() {
    $("#programbar").slideDown(200, function () {
        showTopBar();
        roomTransitionAnimation();
    });
}
function checkIsCatchUpDay() {
    var missDayStr = $(oraiRoot).attr("MissedDay").toLowerCase();
    var retakeValueStr = $(oraiRoot).attr("IsRetake").toLowerCase();
    var shouldDoDayStr = $(oraiRoot).attr("ShouldDoDay");
    var noCatchUp = $(oraiRoot).attr("IsNoCatchUp");
    if (shouldDoDayStr != "" && shouldDoDayStr != undefined) {
        var shouldDoDay = parseInt(shouldDoDayStr);
    }
    if ($(oraiRoot).find("Session").length > 0 || ($(oraiRoot).attr("IsHelpInCTPP") != "true" && $(oraiRoot).attr("IsReportInCTPP") != "true")) {
        if (missDayStr == "true" && retakeValueStr != "true" && retakeValueStr != "1" && noCatchUp != "1") {
            isCatchUpDay = true;
        } else if (noCatchUp == "1") {
            isCatchUpDay = false;
        } else {
            isCatchUpDay = false;
        }
    } else {
        isCatchUpDay = false;
    }
}
function introAnimation() {
    //setOrangeBubble('FadeOut', programBarAnimation);
    /*var missDayStr = $( oraiRoot ).attr( "MissedDay" ).toLowerCase();
    var retakeValueStr = $( oraiRoot ).attr( "IsRetake" ).toLowerCase();
    var shouldDoDayStr = $(oraiRoot).attr("ShouldDoDay");
    var noCatchUp = $(oraiRoot).attr("IsNoCatchUp");
    if ( shouldDoDayStr != ""  && shouldDoDayStr != undefined) {
        var shouldDoDay = parseInt( shouldDoDayStr );
    }
    if ($(oraiRoot).find("Session").length > 0 || ($(oraiRoot).attr("IsHelpInCTPP") != "true" && $(oraiRoot).attr("IsReportInCTPP") != "true")) {
        if (missDayStr == "true" && retakeValueStr != "true" && retakeValueStr != "1" && noCatchUp !="1") {
            setOrangeBubble('FadeOut', showTipMessageForCatchUpDay);
            isCatchUpDay = true;
        } else if (noCatchUp == "1") {
            setOrangeBubble('FadeOut', programBarAnimation);
            isCatchUpDay = false;
        } else {
            setOrangeBubble('FadeOut', programBarAnimation);
            isCatchUpDay = false;
        }
    } else {
        setOrangeBubble( 'FadeOut', programBarAnimation );
        isCatchUpDay = false;
    }*/
    checkIsCatchUpDay();
    if (isCatchUpDay) {
        setOrangeBubble('FadeOut', showTipMessageForCatchUpDay);
    } else {
        setOrangeBubble('FadeOut', programBarAnimation);
    }
}
function reloadedIntro(isFirstLoadingPage) {
    $(".mask").css("display", "none");
    $("#bubbles").fadeOut(200, function () {
        $("#pic").fadeOut(200, function () {
            $("#programbar").slideUp(200, function () {
                //showProgramRoomName();
                loadingAnimation(isFirstLoadingPage);
            });
        });

    });
}
function roomTransitionAnimation() {
    $(".mask").css("display", "none");
    $("#bubbles").fadeOut(200, function () {
        $("#pic").fadeOut(200, function () {
            $('.button-back').hide();
            showProgramRoomName(function () {
                $("#programRoomBar").slideDown(200, function () {
                    $(".body-loading").fadeIn(200, function () {
                        hiddenRoomInfo();
                    });
                });
            });
        });



    });
}
function publicCheckBeforeAndAfterExpression() {
    if ( sequenceOrder != undefined && sequenceOrder != '' ) {
        pageObj = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } )
					.find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
    } else {
        //directllyGo = false;
        pageObj = $( root ).find( 'PageSequence' ).filter( function () { return $( this ).attr( 'GUID' ) == seqGuid } )
					.find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
    }
    afterisEnd = false;
    //alert('the page we will go to is: '+ sequenceOrder + '.' + pageOrder+'. Now we check the after and before expression of that page.');
    // changed by XMX on 2012/04/20
    /* var nextPageObj = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } )
    .find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );*/
    if ( sequenceOrder != undefined && sequenceOrder != '' && !isNaN( sequenceOrder ) ) {
        var nextPageObj = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } )
					.find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
    } else {
        var nextPageObj = $( root ).find( 'PageSequence' ).filter( function () { return $( this ).attr( 'GUID' ) == seqGuid } ).find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
        //console.log( 'currentSequeneGUID:' + seqGuid );
    }
    // end changed by xmx on 2012/04/20
    if ( $( root ).attr( 'IsCTPPEnable' ) ) {
        if ( $( root ).attr( 'IsCTPPEnable' ).toLowerCase().trim() == '1' || $( root ).attr( 'IsCTPPEnable' ).toLowerCase().trim() == 'true' ) {
            if ( nextPageObj.attr( 'AfterExpression' ) != undefined ) {
                if ( nextPageObj.attr( 'AfterExpression' ).toLowerCase().trim() == 'endpage' ) {
                    //alert('the next page after expression is: END PAGE, so session end');
                    if ( $( root ).attr( 'IsRetake' ).trim() == 'true' && nextPageObj.attr( 'Type' ).trim() == 'Account creation' ) {
                        sequenceOrder = parseInt( nextPageObj.parent().attr( 'Order' ) );
                        pageOrder = parseInt( nextPageObj.attr( 'Order' ) );
                        goToNextDirectlly();
                    } else {
                        afterisEnd = true;
                        sessionEnding();
                    }
                }
            }
        }
    }


    if ( afterisEnd == false ) {
        if ( nextPageObj.attr( 'BeforeExpression' ) && nextPageObj.attr( 'BeforeExpression' ) != '' ) {
            //alert('the before expression is: ' + nextPageObj.attr('BeforeExpression'));
            var be = nextPageObj.attr( 'BeforeExpression' );

            //reset the directllyGo as true;
            directllyGo = true;
            recur( be, $( root ) );
            //alert(directllyGo);
            if ( directllyGo == true ) {
                //means did not go to any page, do nothing
            } else {
                //means the before expression lead us to a new page, so check this page again(inclue both after expression and before expression)
                publicCheckBeforeAndAfterExpression();
            }

        } else {
            // if nextPageObj.attr("BeforeExpression") == "" || nextPageObj.attr("BeforeExpression") == undefined

        }
    }

}
function checkBeforeExpresionInFirstPage() {
    if ( sequenceOrder != undefined && sequenceOrder != '' ) {
        pageObj = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } )
					.find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
    } else {
        //directllyGo = false;
        pageObj = $( root ).find( 'PageSequence' ).filter( function () { return $( this ).attr( 'GUID' ) == seqGuid } )
					.find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );
    }
    if ( pageObj.attr( 'BeforeExpression' ) && pageObj.attr( 'BeforeExpression' ) != '' ) {
        //alert('the before expression is: ' + nextPageObj.attr('BeforeExpression'));
        var be = pageObj.attr( 'BeforeExpression' );

        //reset the directllyGo as true;
        directllyGo = true;
        recur( be, $( root ) );
        //alert(directllyGo);
        if ( directllyGo == true ) {
            //means did not go to any page, do nothing
        } else {
            //means the before expression lead us to a new page, so check this page again(inclue both after expression and before expression)
            publicCheckBeforeAndAfterExpression();
        }

    } else {
        // if nextPageObj.attr("BeforeExpression") == "" || nextPageObj.attr("BeforeExpression") == undefined

    }
}
function hiddenRoomInfo() {
    $( ".body-loading" ).delay( 500 ).fadeOut( 200, function () {
        $( "#programRoomBar" ).slideUp( 200, function () {
            $( "#bubbles" ).fadeIn( 200, function () {
                $("#pic").fadeIn(200);
               // comment for hidden button-back on first page of a day
               // $( '.button-back' ).show( 200 );
            } );
            if ( isFirstLoaded ) {
                checkBeforeExpresionInFirstPage();
            }
            loadPage( $( root ) );
            isFirstLoaded = false;
            var pageType = pageObj.attr( "Type" );
            if ( pageType == "Get information" ) {
                saveAnswers();
            }
            if ( pageType == "Choose preferences" ) {
                var pageGUID = pageObj.attr( "GUID" );
                var answer = historyAnswers[pageGUID];
                if ( answer != null && answer != undefined && answer != null ) {
                    saveAnswersForChoosePreferences( answer );
                }
            }
        } );
    } );
}

function getProRoomNameBySeqGUID() {
    var pageSeq = $(oraiRoot).find("PageSequence");
    pageSeq.each(function (seqIndex, sequence) {
        var seqGUID = $(sequence).attr("GUID");
        var proRoomName = $(sequence).attr("ProgramRoomName");
        if (proRoomName != null && proRoomName != undefined && proRoomName != '') {
            programRoomName[seqGUID] = proRoomName;
        }
    });

}
function showProgramRoomName(callback) {
    $( '.programRoomName' ).empty();
    if ( sequenceOrder == "" || isNaN( sequenceOrder ) || sessionName=="relapse" || sequenceOrder == "undefined" ){
     var pageSeqObj=$(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid })
    }else{
    var pageSeqObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder });
    }
    var programRoomName = $(pageSeqObj).attr("ProgramRoomName");
    $(".programRoomName").empty().html(programRoomName);
    if (callback && typeof (callback) == "function") {
        callback();
    }
}
function getSequenceGUID(XMLobj) {
    if (sequenceOrder != sequenceOrderBeforeParse) {
        msgSequenceStart();
    }
    /*if (XMLobj.find('Session').length > 0) {
        sessionName = $(root).find("Session").attr("Name");

    } else {
        sessionName = 'relapse';
        sequenceOrder = '';
    }*/
    if ( sequenceOrder != undefined && sequenceOrder != '' && !isNaN( sequenceOrder ) ) {
        sessionName = $( root ).find( "Session" ).attr( "Name" );
    } else {
        sessionName = 'relapse';
        sequenceOrder = '';
    }
    if (sequenceOrder != undefined && sequenceOrder != '' && !isNaN(sequenceOrder)) {
        pageSeqObj = XMLobj.find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder });
        pageObj = XMLobj.find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
        seqGuid = pageSeqObj.attr('GUID');
    }
    if ( sequenceOrder == undefined || sequenceOrder == '' || isNaN( sequenceOrder ) ) {
        if ( $( oraiRoot ).find( 'Session' ).length > 0 ) {
            if ( XMLobj.find( "Relapse" ).length > 0 ) {
               // seqGuid = XMLobj.find( "Relapse" ).find( "PageSequence" ).attr( "GUID" );
            } else {
               // seqGuid = XMLobj.find( "PageSequence" ).attr( "GUID" );
            }
        } else {
       // seqGuid = $( oraiRoot ).attr('PageSequenceGUID');
        }
        pageSeqObj = XMLobj.find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid });
        pageObj = XMLobj.find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
        //seqGuid = pageSeqObj.attr('GUID');
    }
    
}


