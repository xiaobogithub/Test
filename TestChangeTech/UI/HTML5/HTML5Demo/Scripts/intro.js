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
    var programDay = "";
    var brandName = "";
    myplatform = $(ctppXmlobj).attr("Platform");
    if ( programName != null && programName != "" && programName != undefined )
    {
        $( "title" ).text( programName.toUpperCase() );
    }
    var inforStr = "<div class='wrapper-splashscreen'><div class='cover'></div>"+
                   "<div class='content'><div class='maintitle'>"+
                    "<p class='franchise-name'></p>" +
                   "<p class='franchise-logo'></p>"+
                   "<p><div class='programtitle'></div></p>" +
                   "<p><img class='loaderanim' src='../Themes/design/loading-small.png' width='20' height='20'></p>" +
                   "</div>" +
                   "<div class='bottomlogo'>" +
                   "<img src='../Themes/design/logo-easychange-small.png' width='136' height='22'>" +
                   "</div></div></div>"

    $("#orange").append(inforStr);
    if (logoImage != '' && logoImage != null && logoImage != undefined) {
        $(".franchise-logo").html("<img src=" + logoUrl + " alt='' />");
    }
    if (programTitle != '' && programTitle != null && programTitle != undefined) {
        $(".programtitle").append("<span class='programName'>" + programTitle + "</span>");
    } else if (programName != '' && programName != null && programName != undefined) {
        $(".programtitle").append("<span class='programName'>" + programName + "</span>");
    }
    if (programDay != "" && programDay != null && programDay != undefined) {
        $(".programtitle").append("<span class='programday'>"+programDay+"</span>")
    }
    if (brandName != "" && brandName != null && brandName != undefined) {
        $(".franchise-name").text(brandName);
    }
    bubbleOrangeWidth = $("#orange").outerWidth(true);
    setTimeout(backgroundFadeIn,100);
    if (callback && typeof (callback) == "function") {
        callback();
    }
}
function loadInroContent() {
    if ($('#orange').html() != "") {
        setOrangeBubble('#orange');
    }
    else {
        if ($('#bubble-orange').html() != "") {
            setOrangeBubble('FadeOut');
        }
        else {
            setOrangeBubble('');
        }
    }
}

function backgroundFadeIn() {
    var logoImage = $(ctppXmlobj).attr("LogoName");
    var programTitle = $(ctppXmlobj).attr("CTPPName");
    var programName = $(ctppXmlobj).attr("ProgramName");
    var subheading = $(ctppXmlobj).attr("CTPPSubheading");
    loadInroContent();
}
function programBarAnimation() {
    $("#controlBar").slideDown(200, function () {
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
    checkIsCatchUpDay();
    if (isCatchUpDay) {
        setOrangeBubble('FadeOut', showTipMessageForCatchUpDay);
    } else {
        setOrangeBubble('FadeOut', programBarAnimation);
    }
}
function reloadedIntro(isFirstLoadingPage) {
    //$(".mask").css("display", "none");
   // $(".wrapper-image").delay(1000).fadeOut(1200);
    $(".wrapper-image").hide();
    $("#bubbles").fadeOut(200, function () {
        $("#controlBar").slideUp(200, function () {
            $("#bubble-orange .bubcontent").html("");
            showProgramRoomName(function () {
                loadingAnimation(isFirstLoadingPage);
            });
            /*loadingAnimation(isFirstLoadingPage);*/
        });

    });
}
function roomTransitionAnimation() {
    // $(".wrapper-image").delay(1000).fadeIn(1200);
    $(".wrapper-image").hide();
    $("#bubbles").fadeOut(200, function () {
        $("#controlBar").slideDown(200, function () {
            $("#bubble-orange .bubcontent").html("");
            showProgramRoomName(function () {
                $(".wrapper-image").delay(10).fadeOut(1200);
                hiddenRoomInfo();
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
    //$(".wrapper-image").fadeOut(500);
    $(".wrapper-image").hide();
    $("#programRoomBar").delay(3500).fadeOut(500, function () {
        $("#programRoomBar").html("");
        $("#bubbles").fadeIn(200, function () {
            $(".button-settings").show();
            $(".waiting-small").html("");
        });
        if (isFirstLoaded) {
            checkBeforeExpresionInFirstPage();
        }
        loadPage($(root));
        isFirstLoaded = false;
        var pageType = pageObj.attr("Type");
        if (pageType == "Get information") {
            saveAnswers();
        }
        if (pageType == "Choose preferences") {
            var pageGUID = pageObj.attr("GUID");
            var answer = historyAnswers[pageGUID];
            if (answer != null && answer != undefined && answer != null) {
                saveAnswersForChoosePreferences(answer);
            }
        }
        /* } );*/
    });
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
    $('#programRoomBar').html('');
    if ( sequenceOrder == "" || isNaN( sequenceOrder ) || sessionName=="relapse" || sequenceOrder == "undefined" ){
     var pageSeqObj=$(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid })
    }else{
    var pageSeqObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder });
    }
var programRoomName = $(pageSeqObj).attr("ProgramRoomName");
if (programRoomName == undefined || programRoomName == null) {
    programRoomName = "";
}
var loadingAnimationStr='<div class="container">'+
	'<ul id="progress">'+
    '<li><div id="layer1" class="ball"></div><div id="layer7" class="pulse"></div></li>'+
    '<li><div id="layer2" class="ball"></div><div id="layer8" class="pulse"></div></li>'+
    '<li><div id="layer3" class="ball"></div><div id="layer9" class="pulse"></div></li>'+
   ' <li><div id="layer4" class="ball"></div><div id="layer10" class="pulse"></div></li>'+
    '<li><div id="layer5" class="ball"></div><div id="layer11" class="pulse"></div></li>'+
    '</ul>'+
'</div>';
var programRoomStr = "<div class='wrapper-splashscreen'>" +
                     "<div class='content'>" +
                     "<div class='maintitle'>" +
                     "<p><div class='chaptertitle'>" + programRoomName + "</div></p><p>&nbsp;</p>" +
                     "<p class='waiting-small'>" + loadingAnimationStr + "</p>" +
                     //"<p class='waiting-small'><img src='../Themes/design/waiting-small.gif' width='72' height='15'></p>" +
                     "</div>" +
                     "<div class='bottomlogo'><img src='../Themes/design/logo-easychange-small.png' width='136' height='22'></div>" +
                     "</div></div>";
$(".button-settings").hide();
$(".button-back").hide();
$("#programRoomBar").html(programRoomStr);
$("#programRoomBar").fadeIn(200);
$('#progress').removeClass('running').delay(10).queue(function (next) {
    $(this).addClass('running');
    next();
});

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


