/**
* @author Ole Hartvig
*/

function gotoPage(withAnimation) {
    //Picture and bubbles
    $('#hiddenImage').text = '';
    $('#hiddenBg').text = '';
    // var img = new Image();
    // img.onload = function() {
    // var blendImg = new Image();
    // blendImg.onload = function() {
    // Pixastic.process(img, "blend", 
    // {
    // amount : 1, 
    // mode : "multiply", 
    // image : blendImg
    // }
    // );
    // }
    // blendImg.src = "img/fullscreen-pic.jpg";
    // }
    // img.src = "img/hostpic-02.png";

    //  $('#hiddenImage').load("/"+Math.random()*Math.random(),function(){
    if ($('#hiddenImage').text() != '') {
        var newImage = $('#hiddenImage').text();
        //$('img#over').attr("src",newImage);
        $('img#backgroundpic').hide()
			.one('load', function () {
			    // var blendImg = new Image();
			    // blendImg.onload = function() {
			    // Pixastic.process(this, "blend", 
			    // {
			    // amount : 1, 
			    // mode : "multiply", 
			    // image : blendImg
			    // }
			    // );
			    // }
			    // blendImg.src = "img/fullscreen-pic.jpg";

			    $(this).fadeIn(1200);
			})
			.attr('src', newImage) //Set the source so it begins fetching
		    .each(function () {
		        //Cache fix for browsers that don't trigger .load()
		        if (this.complete) $(this).trigger('load');
		    });

        // var over = $('img#backgroundpic').getContext('2d'); 
        // var under = $('img#fullbgImg').getContext('2d');
        // over.blendOnto(under,'multiply');
    } else { $('img#backgroundpic').hide(); $('img#over').attr("src", "").removeAttr("style"); }
    //  });
    //	$('#hiddenBg').load("/"+Math.random()*Math.random(),function(){
    if ($('#hiddenBg').text() != "") {
        var newBg = $('#hiddenBg').text();

        $('img#fullbgImg').hide()
			.one('load', function () {
			    $(this).fadeIn(1200);
			})
			.attr('src', newBg) //Set the source so it begins fetching
		    .each(function () {
		        //Cache fix for browsers that don't trigger .load()
		        if (this.complete) $(this).trigger('load');
		    });
    } else { $('img#fullbgImg').hide(); $('img#fullbgImg').attr("src", "") }

    //	});


    // Load data to page		
    //$('#hiddenElements').load("/"+Math.random()*Math.random(),function(){
    if ($('#white').html() != "") {
        setWhiteBubble('#white');
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
    setTimeout(centerAnimate, 200);
    if (document.getElementById("over").complete) {
        //$.getScript("./Scripts/custom.js");//, function() {setTimeout(loaded, 100);});
        resizeImg();
    }
    //Navigation
    //$('.backbtn').unbind().click(function() {gotoPage(eval(page-1), true);});
    //$('#bubble-blue').unbind().click(function() {gotoPage(eval(parseInt(page)+1), true);});
}

function loaded() {
    window.scrollTo(0, 1); // pan to the bottom, hides the location bar
}
/*function centerAnimate() {
$('body').removeClass('bgblack').addClass('bggradient').delay(100);
if( $('#hiddenImage').text()!='' && $('#bubbles').hasClass('center') ) {
if(navigator.userAgent.toLowerCase().match(/iPhone/i)!= null){
$('#bubbles').removeClass('center').animate({left: '0px'},500).addClass('centerright');
}else{
$('#bubbles').removeClass('center').animate({left: '250px'},500).addClass('centerright');
}
}
else if($('#hiddenBg').text()!='' && $('#hiddenImage').text()=='' && $('#bubbles').hasClass('centerright')) {
$('#bubbles').removeClass('centerright').animate({left: '0px'},500).addClass('center');
}
else if( $('img#backgroundpic').is(':visible')){
$('#bubbles').addClass('centerright');
}
else if($('#hiddenImage').text()!='' && $('#bubbles').hasClass('centerright'))
{
if(navigator.userAgent.toLowerCase().match(/iPhone/i)!= null){
$('#bubbles').removeClass('center').animate({left: '0px'},500).addClass('centerright');
}else{
$('#bubbles').removeClass('center').animate({left: '250px'},500).addClass('centerright');
}
}else{
$('#bubbles').removeClass("centerright").animate({left:'0px'},500).addClass('center');
//$('#bubbles').addClass('center');
		
}
}*/
function centerAnimate() {
    //$('body').removeClass('bgblack').addClass('bggradient').delay(100);
    if ($('#hiddenImage').text() != '') {
        if ($('#bubbles').hasClass('center')) {
           /* if (navigator.userAgent.toLowerCase().match(/iPhone/i) != null) {
                $('#bubbles').removeClass('center').animate({ left: '0px' }, 500).addClass('centerright');
            } else {
                $("#bubbles").removeClass("center").animate({ left: '250px' }, 500).addClass("centerright");
            }*/
            $( "#bubbles" ).removeClass( "center" ).animate( { left: '250px' }, 500 ).addClass( "centerright" );
            // $("#bubbles").removeClass("center").animate({left:'250px'},500).addClass("centerright");
        } else if ($('#bubbles').hasClass('centerright')) {
            /*if (navigator.userAgent.toLowerCase().match(/iPhone/i) != null) {
                $('#bubbles').removeClass('center').animate({ left: '0px' }, 500).addClass('centerright');
            }*/
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
        var newHeight = $(content).outerHeight();
        if (newHeight <= 21) {
            newHeight = 21;
        }
        $(bubbleElement).animate({ height: newHeight }, 500);
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
        $('#bubble-white .bubcontent').html('').fadeOut(200);
        $('#bubble-white').delay(200).animate({ height: 21 }, 500).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-white').css('display') == 'none') {
            $('#bubble-white').fadeIn(500);
        }
        //$('#bubble-white .bubcontent').fadeOut(200);
        $('#bubble-white .bubcontent').hide();
        $(textElement).width($('#bubble-white').width());
        var newHeight = $(textElement).outerHeight();
        if (newHeight <= 21) {
            newHeight = 21;
        }
        $('#bubble-white').animate({ height: newHeight }, 500);
        $('#bubble-white .bubcontent').html($(textElement).html()).delay(500).fadeIn('slow');
    }
    else {
        if ($('#bubble-white').css('display') != 'none') {
            $('#bubble-white').fadeOut(200);
        }
        $('#bubble-white').height(21);
    }
}
function setYellowBubble(textElement) {
    if (textElement == "FadeOut") {
        $('#bubble-yellow .bubcontent').fadeOut(200);
        $('#bubble-yellow').delay(200).animate({ height: 21 }, 500).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-yellow').css('display') == 'none') {
            $('#bubble-yellow').fadeIn(500);
        }
        //$('#bubble-yellow .bubcontent').fadeOut(200);
        $('#bubble-yellow .bubcontent').hide();
        $(textElement).width($('#bubble-yellow').width());
        $('#bubble-yellow').animate({ height: $(textElement).outerHeight() }, 500);
        $('#bubble-yellow .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-yellow').css('display') != 'none') {
            $('#bubble-yellow').fadeOut(200);
        }
        $('#bubble-yellow').height(21);
    }
}
function setBlackBubble(textElement) {
    if (textElement == "FadeOut") {
        $('#bubble-black .bubcontent').fadeOut(200);
        //$('#bubble-black').delay(200).animate({ height: 21 }, 500).fadeOut(500);
        $('#bubble-black').delay(200).fadeOut(500);
    }
    else if (textElement != "") {
        if ($('#bubble-black').css('display') == 'none') {
            $('#bubble-black').fadeIn(500);
        }
        //$('#bubble-black .bubcontent').fadeOut(200);
        $('#bubble-black .bubcontent').hide();
        $(textElement).width($('#bubble-black').width());
      //  $('#bubble-black').animate({ height: $(textElement).outerHeight() }, 500);
        $('#bubble-black .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-black').css('display') != 'none') {
            $('#bubble-black').fadeOut(200);
        }
       // $('#bubble-black').height(21);
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
        $('#bubble-blue').animate({ height: $(textElement).outerHeight() }, 500);
        $('#bubble-blue .bubcontent').html($(textElement).html()).delay(500).fadeIn(200);
    }
    else {
        if ($('#bubble-blue').css('display') != 'none') {
            $('#bubble-blue').fadeOut(200);
        }
        $('#bubble-blue').height(21);
    }
}
function setOrangeBubble(textElement, callback) {
    if (textElement == "FadeOut") {
        $('#bubble-orange .bubcontent').delay(3000).fadeOut(200);
        $('#bubble-orange').delay(3000).animate({ height: 21 }, 500).fadeOut(500, function () {
            if (callback && typeof (callback) == "function") {
                callback();
            } 
        });
    }
    else if (textElement != "") {
        if ($('#bubble-orange').css('display') == 'none') {
            $('#bubble-orange').fadeIn(500);
        }
        //$('#bubble-blue .bubcontent').fadeOut(200);
        $('#bubble-orange .bubcontent').hide();
        $(textElement).width($('#bubble-orange').width());
        $('#bubble-orange').animate({ height: $(textElement).outerHeight() }, 500);
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
        $('#bubble-orange').height(21);
    }
    //alert(typeof (callback));

    //callback();
}