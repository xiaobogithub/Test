/**
* @author Ole Hartvig
*/
function showImage() {
    $('#hiddenImage').text = "";
    if ($("#hiddenImage").text() != '') {
        var imageUrl = $('#hiddenImage').text();
        var imageMode = $(pageObj).attr("ImageMode");
        var presenterImage = $(pageObj).attr("PresenterImage");
        var illustrationImage = $(pageObj).attr("IllustrationImage");
        var backgroundImage = $(pageObj).attr("BackgroundImage");
        var title = $(pageObj).attr("Title");
        var text = $(pageObj).attr("Text");
        var footerText = $(pageObj).attr("FooterText");
        var screenWidth = $(window).outerWidth();
        var screenHeigh = $(window).outerHeight();
        var image = new Image();
            if (presenterImage != null && presenterImage != "" && presenterImage != undefined) {
                if ($(".wrapper-image").hasClass("illustrationmode")) {
                    $(".wrapper-image").removeClass("illustrationmode");
                } else if ($(".wrapper-image").hasClass("fullscreenmode")) {
                    $(".wrapper-image").removeClass("fullscreenmode");
                } else if ($(".wrapper-image").hasClass("clearmode")) {
                    $(".wrapper-image").removeClass("clearmode");
                }
                $(".wrapper-image").html("<img src='" + imageUrl + "' alt='' id='myImage'/><div class='box-grey'></div>");

            } else if (backgroundImage != undefined && backgroundImage != null && backgroundImage != "") {
//                if ((title == null || title == "" || title == undefined) && (text == null || text == "" || text == undefined) && (footerText == null || footerText == undefined || footerText == "")) {
                    $(".wrapper-image").addClass("fullscreenmode");
                    if ($(".wrapper-image").hasClass("illustrationmode")) {
                        $(".wrapper-image").removeClass("illustrationmode");
                    } else if ($(".wrapper-image").hasClass("clearmode")) {
                        $(".wrapper-image").removeClass("clearmode");
                    }
                    $(".wrapper-image").html("<div class='fullscreenimage'></div>");
                    $(".fullscreenimage").css({ "background-image": "url(" + imageUrl + ")" });

//                } else {
//                    $(".wrapper-image").html("");
//                    if ($(".wrapper-image").hasClass("illustrationmode")) {
//                        $(".wrapper-image").removeClass("illustrationmode");
//                    } else if ($(".wrapper-image").hasClass("fullscreenmode")) {
//                        $(".wrapper-image").removeClass("fullscreenmode");
//                    } else if ($(".wrapper-image").hasClass("clearmode")) {
//                        $(".wrapper-image").removeClass("clearmode");
//                    }
//                }
                } else if (illustrationImage != null && illustrationImage != undefined && illustrationImage != "") {
                    $(".wrapper-image").addClass("illustrationmode");
                if ($(".wrapper-image").hasClass("clearmode")) {
                    $(".wrapper-image").removeClass("clearmode");
                } else if ($(".wrapper-image").hasClass("fullscreenmode")) {
                    $(".wrapper-image").removeClass("fullscreenmode");
                }
               
                $(".wrapper-image").html("<img src='" + imageUrl + "' alt=''/>");
            } else {
            }
    } else {
        $(".wrapper-image").html("");
    }
}
function gotoPage(withAnimation) {
    //Picture and bubbles
    showImage();
    // Load data to page		
    //$('#hiddenElements').load("/"+Math.random()*Math.random(),function(){
    if ($('#white').html() != "") {
        setWhiteBubble('#white');
        var height = $("#white").outerHeight(true);
        $("#white").css({height:height+"px"});
    }
    else {
        if ($('#bubble-white').html() != "")
            setWhiteBubble('FadeOut');
        else
            setWhiteBubble('');
    }

    if ($('#yellow').html() != "") {
        setYellowBubble('#yellow');
    }
    else {
        if ($('#bubble-yellow').html() != "")
            setYellowBubble('FadeOut');
        else
            setYellowBubble('');
    }
    if ($("#black").html() != "") {
        setBlackBubble('#black');
    } else {
        if ($('#bubble-black').html() != "")
            setBlackBubble('FadeOut');
        else
            setBlackBubble('');
    }

    if ($('#blue').html() != "") {
        setBlueBubble('#blue');
    }
    else {
        if ($('#bubble-blue').html() != "")
            setBlueBubble('FadeOut');
        else
            setBlueBubble('');
    }

    //});


    //Misc
    //setTimeout(centerAnimate, 200);
    modeChangeAnimation();
    //setTimeout(modeChangeAnimation, 200);
    if (document.getElementById("actualImage").complete) {
        resizeImg();
    }
}

function loaded() {
    window.scrollTo(0, 1); // pan to the bottom, hides the location bar
}
function modeChangeAnimation() {
    var wrapperBub = $('.wrapper-bubbles,#contentElement');
    var imageMode = $(pageObj).attr("ImageMode");
    var presenterImage = $(pageObj).attr("PresenterImage");
    var illustrationImage = $(pageObj).attr("IllustrationImage");
    var backgroundImage = $(pageObj).attr("BackgroundImage");
    var pageType = $(pageObj).attr("Type");
    /*console.log(presenterImage + "presenterImage");
    console.log(illustrationImage + "illustrationImage");
    console.log(backgroundImage + "backgroundImage");*/
    var title = $(pageObj).attr("Title");
    var text = $(pageObj).attr("Text");
    var footerText = $(pageObj).attr("FooterText");
    if (presenterImage != undefined && presenterImage != null && presenterImage != "") {
        if (wrapperBub.hasClass("fullscreenmode")) {
            wrapperBub.removeClass("fullscreenmode");
        } else if (wrapperBub.hasClass("clearmode")) {
            wrapperBub.removeClass("clearmode");
        } else if (wrapperBub.hasClass("illustrationmode")) {
            wrapperBub.removeClass("illustrationmode");
        }
        if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
            wrapperBub.removeClass("clearmode-fullscreenmode");
        }
       if (pageType == "Graph") {
            wrapperBub.addClass("fullscreenmode");
            if (wrapperBub.hasClass("clearmode")) {
                wrapperBub.removeClass("clearmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            } 
            if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
                wrapperBub.removeClass("clearmode-fullscreenmode");
            }
        }
    } else if (backgroundImage != undefined && backgroundImage != null && backgroundImage != "") {
        if ((title == null || title == "" || title == undefined) && (text == null || text == "" || text == undefined) && (footerText == null || footerText == undefined || footerText == "")) {
            wrapperBub.addClass("fullscreenmode");
            if (wrapperBub.hasClass("clearmode")) {
                wrapperBub.removeClass("clearmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            }
            if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
                wrapperBub.removeClass("clearmode-fullscreenmode");
            }
        } else {
            wrapperBub.addClass("clearmode");
            wrapperBub.addClass("clearmode-fullscreenmode");
            if (wrapperBub.hasClass("fullscreenmode")) {
                wrapperBub.removeClass("fullscreenmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            }
        }
        if (pageType == "Graph") {
            wrapperBub.addClass("fullscreenmode");
            if (wrapperBub.hasClass("clearmode")) {
                wrapperBub.removeClass("clearmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            }
            if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
                wrapperBub.removeClass("clearmode-fullscreenmode");
            }
        } 
    } else if (illustrationImage != null && illustrationImage != undefined && illustrationImage != "") {
        wrapperBub.addClass("illustrationmode");
        if (wrapperBub.hasClass("fullscreenmode")) {
            wrapperBub.removeClass("fullscreenmode");
        } else if (wrapperBub.hasClass("clearmode")) {
            wrapperBub.removeClass("clearmode");
        }
        if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
            wrapperBub.removeClass("clearmode-fullscreenmode");
        }
        if (pageType == "Graph") {
            wrapperBub.addClass("fullscreenmode");
            if (wrapperBub.hasClass("clearmode")) {
                wrapperBub.removeClass("clearmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            } 
            if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
                wrapperBub.removeClass("clearmode-fullscreenmode");
            }
        } 
    } else {
        wrapperBub.addClass("clearmode");
        if (wrapperBub.hasClass("fullscreenmode")) {
            wrapperBub.removeClass("fullscreenmode");
        }else if (wrapperBub.hasClass("illustrationmode")) {
            wrapperBub.removeClass("illustrationmode");
        }
        if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
            wrapperBub.removeClass("clearmode-fullscreenmode");
        }
        if (pageType == "Graph") {
            wrapperBub.addClass("fullscreenmode");
            if (wrapperBub.hasClass("clearmode")) {
                wrapperBub.removeClass("clearmode");
            } else if (wrapperBub.hasClass("illustrationmode")) {
                wrapperBub.removeClass("illustrationmode");
            }
            if (wrapperBub.hasClass("clearmode-fullscreenmode")) {
                wrapperBub.removeClass("clearmode-fullscreenmode");
            }
        } 
    }
}
function centerAnimate() {
    //$('body').removeClass('bgblack').addClass('bggradient').delay(100);
    if ($('#hiddenImage').text() != '') {
        if ($('#bubbles').hasClass('center')) {
            $( "#bubbles" ).removeClass( "center" ).animate( { left: '250px' }, 500 ).addClass( "centerright" );
        } else if ($('#bubbles').hasClass('centerright')) {
        } else {
            $("#bubbles").addClass("centerright").animate({ left: "250px" }, 500);
        }
    } else {
        if ($('#bubbles').hasClass('center')) {
            $("#bubbles").css({ "left": "0" }).animate(500);
        } else if ($('#bubbles').hasClass('centerright')) {
            $("#bubbles").removeClass("centerright").animate({ left: "0px" }, 500).addClass("center");
        } else {
            $("#bubbles").animate({ left: "0px" }, 500).addClass("center");
        }

    }

}
function setBubbleContent(bubbleColor, content) {
    var bubbleElement = $('#bubble-' + bubbleColor);
    var bubbleContent = $('#bubble-' + bubbleColor + ' .bubcontent');

    if (content == "FadeOut") {
        $(bubbleContent).html('').fadeOut(200);
        $(bubbleElement).delay(200).animate({ height: 21 }, 500).fadeOut(500);
    }
    else if (content != "") {
        if ($(bubbleElement).css('display') == 'none') {
            $(bubbleElement).fadeIn(500);
        }
        $(bubbleContent).fadeOut(200);
        $(content).width($(bubbleElement).width());
        var newHeight = $(content).height();
        if (newHeight <= 21) {
            newHeight = 21;
        }
        $(bubbleElement).animate({ height: newHeight }, 1000);
        $(bubbleElement + ' .bubcontent').html($(content).html()).delay(500).fadeIn(200);
    }
    else {
        if ($(bubbleElement).css('display') != 'none') {
            $(bubbleElement).fadeOut(200);
        }
        $(bubbleElement).height(21);
    }
}
function setWhiteBubble(textElement) {
    if (textElement == "FadeOut") {
//        $('#bubble-white .bubcontent').html('').fadeOut(200);
        //        $('#bubble-white').delay(200).animate({ height: 21 }, 500).fadeOut(500);
        $('#bubble-white .bubcontent').html('').fadeOut(200);
        $('#bubble-white').delay(200).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-white').css('display') == 'none') {
            $('#bubble-white').fadeIn(500);
        }
        $('#bubble-white .bubcontent').fadeOut(200);
       /* $(textElement).width($('#bubble-white').width());*/
        /*var newHeight = $(textElement).outerHeight();
        if (newHeight <= 21) {
            newHeight = 21;
        }
        $('#bubble-white').animate({ height: newHeight+"px" }, 1500);
        */
       // $('#bubble-white').animate({ height: "190px" }, 500);
//       $('#bubble-white').css({ "-webkit-transition": "all 1.2s linear", "-moz-transition": "all 1.2s linear", "-o-transition": "all 1.2s linear", "transition": "all 1.2s linear" });
        $('#bubble-white .bubcontent').html($(textElement).html()).fadeIn(500);
    }
    else {
        if ($('#bubble-white').css('display') != 'none') {
            $('#bubble-white').fadeOut(200);
        }
       // $('#bubble-white').height(21);
    }
}
function setYellowBubble(textElement) {
    if (textElement == "FadeOut") {
        $('#bubble-yellow .bubcontent').fadeOut(200);
        //$('#bubble-yellow').delay(200).animate({ height: 21 }, 500).fadeOut(500);
        $('#bubble-yellow').delay(200).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-yellow').css('display') == 'none') {
            $('#bubble-yellow').fadeIn(500);
        }
        //$('#bubble-yellow .bubcontent').fadeOut(200);
        $('#bubble-yellow .bubcontent').hide();
       /* $(textElement).width($('#bubble-yellow').width());*/
       
       //$('#bubble-yellow').animate({ height: $(textElement).outerHeight(true) }, 1000);
        $('#bubble-yellow .bubcontent').html($(textElement).html()).fadeIn(500);
    }
    else {
        if ($('#bubble-yellow').css('display') != 'none') {
            $('#bubble-yellow').fadeOut(200);
        }
       //$('#bubble-yellow').height(21);
    }
}
function setBlackBubble(textElement) {
    if (textElement == "FadeOut") {
        $('#bubble-black .bubcontent').fadeOut(200);
        $('#bubble-black').delay(200).animate({ height: 21 }, 500).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-black').css('display') == 'none') {
            $('#bubble-black').fadeIn(500);
        }
        //$('#bubble-black .bubcontent').fadeOut(200);
        $('#bubble-black .bubcontent').hide();
        $(textElement).width($('#bubble-black').width());
        $('#bubble-black').animate({ height: $(textElement).outerHeight() }, 500);
        $('#bubble-black .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-black').css('display') != 'none') {
            $('#bubble-black').fadeOut(200);
        }
        $('#bubble-black').height(21);
    }
}
function setBlueBubble(textElement) {
    if (textElement == "FadeOut") {
        $('#bubble-blue .bubcontent').fadeOut(200);
        $('#bubble-blue').delay(200).animate({ height: 21 }, 500).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-blue').css('display') == 'none') {
            $('#bubble-blue').fadeIn(500);
        }
        //$('#bubble-blue .bubcontent').fadeOut(200);
        $('#bubble-blue .bubcontent').hide();
        $(textElement).width($('#bubble-blue').width());
        var paddtingTop = $("#bubble-blue").css("padding-top");
        var paddingBottom = $("#bubble-blue").css("padding-bottom");
        var height = parseInt(paddingBottom) + $(textElement).outerHeight();
        //$('#bubble-blue').animate({ height: $(textElement).outerHeight(true) }, 500);
        //        $('#bubble-blue').animate({ height: height }, 500);
//        $('#bubble-blue').css({"-webkit-transition":"all 1.2s linear","-moz-transition":"all 1.2s linear","-o-transition":"all 1.2s linear","transition":"all 1.2s linear"});
        $('#bubble-blue .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-blue').css('display') != 'none') {
            $('#bubble-blue').fadeOut(200);
        }
        $('#bubble-blue').height(32);
    }
}
function setOrangeBubble(textElement, callback) {
    if (textElement == "FadeOut") {
        $('#bubble-orange .bubcontent').delay(500).fadeOut(200);
        $('#bubble-orange').delay(500).animate({ height: 21 }, 500).fadeOut(500, function () {
            if (callback && typeof (callback) == "function") {
                callback();
            } 
        });
    }
    else if (textElement != "") {
        if ($('#bubble-orange').css('display') == 'none') {
            $('#bubble-orange').fadeIn(500);
            $('#bubble-orange .bubcontent').html("");
        }
        $('#bubble-orange .bubcontent').hide();
        $('#bubble-orange .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-orange').css('display') != 'none') {
            $('#bubble-orange').fadeOut(200, function () {
                if (callback && typeof (callback) == "function") {
                    callback();
                } 
            });
        }
        //$('#bubble-orange').height(21);
    }
    //alert(typeof (callback));

    //callback();
}