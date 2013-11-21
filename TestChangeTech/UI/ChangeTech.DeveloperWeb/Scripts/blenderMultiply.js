//remove resingImage from here  changed on 2011/10/12 
var size;
var sizeBg;
function onBlendTransparentImage(presenterImage) {
    if (presenterImage != undefined) {
        resizeImg();
        ImageLoadedComplete(presenterImage, function () {
            var pageType = pageObj.attr("Type");
            if (pageType == "Push pictures") {
                var interval = parseInt(pageObj.attr("Interval")) * 1000;
                goToNextDirectlly();
                setTimeout("loadPage($(root))", interval);
            }
            showImage();
        });
    } else {
        var presenterImageUrl = "";
        if ($("#over").attr("src") == "") {
            $("#backgroundpic").hide();
        }
    }
}
function presenterImagefadeIn() {
    $("#backgroundpic").fadeIn('slow');
}
function blend() {
    size = { width: $("#over").width(), height: $("#over").height() };
    sizeBg = { width: '1600', height: '1000' };
    var destnationY = 1000 - parseInt(size.height);
    var blendSize = {
        destX: 0,
        destY: destnationY,
        sourceX: 0,
        sourceY: 0,
        width: size.width,
        height: size.height
    }
    var data = {}, contexts = {};
    $.each(['over'], function (i, s) {
        var canvas = $('<canvas>').attr(size)[0];
        var ctx = contexts[s] = canvas.getContext('2d');
        drawImage(ctx, s, size);
    });
    $.each(['under'], function (i, s) {
        var canvas = $('<canvas>').attr(sizeBg)[0];
        var ctx = contexts[s] = canvas.getContext('2d');
        drawImage(ctx, s, sizeBg);
    });
    $.each(['backgroundpic'], function (i, s) {
        contexts[s] = $('#' + s).attr(sizeBg)[0].getContext('2d');
    });
    $(contexts.backgroundpic.canvas).attr(sizeBg);
    drawImage(contexts.backgroundpic, 'under', sizeBg);
    contexts.over.blendOnto(contexts.backgroundpic, 'multiply', blendSize);
}
function drawImage(ctx, imgOrId, ctxSize) {
    if (typeof imgOrId == 'string') {
        ctx.drawImage($('#' + imgOrId)[0], 0, 0, ctxSize.width, ctxSize.height);
    } else {
        ctx.drawImage(imgOrId, 0, 0, ctxSize.width, ctxSize.height);
    }
}
//comment to add Push pictures template ,no other reason to comment this, 
/*function ImageLoadedComplete(presenterImage) {
    presenterImageLoaded(presenterImage);
    backgroundImageLoaded();
}*/
// added for Push pictures
function ImageLoadedComplete(presenterImage,callback) {
    //presenterImageLoaded(presenterImage);
    //backgroundImageLoaded();
    if(callback && typeof (callback) == "function"){
		callback();
	}
}
function blendOnBothLoaded() {
    if (document.getElementById('over').complete && document.getElementById('under').complete) {
        if ($("#over").width() > 0) {
            $("#backgroundpic").hide();
            blend();
            setTimeout(presenterImagefadeIn, 100);
            //$("#backgroundpic").show();
        }
    }
}
function ImageLoaded(image) {
    document.getElementById('actualImage').onload = function () {
        if (document.getElementById('actualImage').complete) {
        }
    }
}
function presenterImageLoaded(presenterImage) {
    document.getElementById('over').onload = function () {
        if (document.getElementById('over').complete) {
            setTimeout(
			function () {
			    isOverLoaded = true;
			    if (isUnderLoaded) {
			        blendOnBothLoaded();
			    }
			});
        }
    }
    var presenterImageUrl = '../RequestResource.aspx?target=Image&media=' + presenterImage + '';
    $('#over').attr('src', presenterImageUrl);
}
function backgroundImageLoaded() {
    document.getElementById("under").onload = function () {
        if (document.getElementById('under').complete) {
            setTimeout(
			function () {
			    isUnderLoaded = true;
			    if (isOverLoaded) {
			        blendOnBothLoaded();
			    }
			});
        }
    }
    $('#under').attr('src', 'Images/background.jpg');
}


 
