function resizeImg() {
    var hostImg = $("#backgroundpic");
    var bub = $("#bubbles");
    var main = $("#mainarea");
    var vid = $("#videobox");
    var bgImg = $("#fullbgImg");
    var over = $("#over");
    var under = $("#under");
    var actualImage = $("#actualImage");
    var introBubble = $("#introBubble");

    var bubheight = bub.height() / 2 + 50;

    var winwidth = $(window).width();
    var winheight = $(window).height();
    var mainareaHeight = winheight - 46;


    var imgwidth = actualImage.width;
    var imgheight = actualImage.height;

    var widthratio = winwidth / imgwidth;
    var heightratio = mainareaHeight / imgheight;

    var widthdiff = heightratio * imgwidth;
    var heightdiff = widthratio * imgheight;

    var bgimgwidth = bgImg.width();
    var bgimgheight = bgImg.height();

    var bgwidthratio = winwidth / bgimgwidth;
    var bgheightratio = winheight / bgimgheight;

    var bgwidthdiff = bgheightratio * bgimgwidth;
    var bgheightdiff = bgwidthratio * bgimgheight;

    //var ratio=widthratio < heightratio ? widthratio : heightratio;
    // var actualImageWidth=imgwidth*ratio;
    // var actualImageHeight=imgheight*ratio;
    // hostImg.css({width: actualImageWidth+'px', height: actualImageHeight+'px'});
    if (heightdiff < mainareaHeight) {
        // hostImg.css({width: winwidth+'px', height: heightdiff+'px' }); 
        over.css({ width: winwidth + 'px', height: heightdiff + 'px' });
        //  under.css({width: winwidth+'px', height: heightdiff+'px' }); 
    }
    else {// hostImg.css({width: widthdiff+'px', height: mainareaHeight+'px' });
        over.css({ width: widthdiff + 'px', height: mainareaHeight + 'px' });
        //under.css({width: widthdiff+'px', height: mainareaHeight+'px' });
    }

    if (bgheightdiff > winheight) { bgImg.css({ width: winwidth + 'px', height: winheight + 'px' }); }
    else { bgImg.css({ width: winwidth + 'px', height: winheight + 'px' }); }

    //bub.css({top: winheight/2-bubheight+'px'});
    vid.css({ top: winheight / 8 + 'px' });
    main.css({ height: winheight - 46 + 'px' });
    /*if (navigator.userAgent.toLowerCase().match(/iPhone/i) != null) {
        bub.css({ top: 160 + 'px' });
        introBubble.css({ top: 160 + 'px' });
        // comment for change blend pic when window size changed
        // hostImg.css({height: $("#over").height()+'px', width: imgwidth/(imgheight/260)+'px', top: 46+'px' });
        if (bgImg.height() == winheight) {
            bgImg.css({ height: bgImg.height() + 60 + 'px' });
        }

        //main.css({ height: 160+$('#bubble-white').outerHeight()+$('#bubble-yellow').outerHeight()+$('#bubble-blue').outerHeight()+'px' });
        //$('#page_wrapper').css({ height: main.outerHeight()+46+'px' });
    }
    else {
        bub.css({ top: (winheight - 60) / 6 + 'px' });
        introBubble.css({ top: (winheight - 60) / 6 + 'px' });
    }*/
    bub.css( { top: ( winheight - 60 ) / 6 + 'px' } );
   /* if ($("#white").html() == "" && $("#yellow").html() == "" && $("#black").html() == "") {
        bub.animate({ top: (winheight - 60) / 2 + 'px' }, 500);
    } else {
        bub.css({ top: (winheight - 60) / 6 + 'px' });
    }*/
    introBubble.css( { top: ( winheight - 60 ) / 6 + 'px' } );
}

function windowResize() {
    $(window).resize(function () {
        resizeImg();
        if ($("#hostPic").text() != '') {
            blend();
        }
    });
}


