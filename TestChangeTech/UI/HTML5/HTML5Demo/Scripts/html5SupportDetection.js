/// <reference path="jquery-1.6.2.min.js" />
/// <reference path="modernizr-latest.js" />

isSupportHtml5 = false;
function isSupportHtml5Detection() {
   var isSupportCanvas=supports_canvas();
   var isSupportCanvasText = supports_canvas_text();
   // comment on 2012/01/13 by XMX, do not check video and audio
   //var isSupportVideo = supports_video();
   //var isSupportAudio = supports_audio();
   //if (isSupportCanvas && isSupportCanvasText && isSupportVideo && isSupportAudio) {
   if (isSupportCanvas && isSupportCanvasText) {
       isSupportHtml5 = true;
   }
   return isSupportHtml5;
}
// todo: refactor the supports_html5
function supports_html5(isFirstTimeLoadingPage) {
	isSupportHtml5Detection();
	if (isSupportHtml5) {
	    startLoadingPage(isFirstTimeLoadingPage);
	} else {
	    var message = "Your browser is not support HTML5";
	    var programCode = getProgramCode();
	    CT.Program.GetSpecialStringValueForHtml5Incompatible(null, programCode, function (widget, data) {
	        message = data;
		    alert(message);
	    });

	}
}
function supports_canvas() {
    var isSupportCanvas = !!document.createElement("canvas").getContext;
    return isSupportCanvas;
}
function supports_canvas_text() {
    if (!supports_canvas()) { return false; }
    var tempCanvas = document.createElement('canvas');
    var context = tempCanvas.getContext('2d');
    var isSupportFillText = false;
    if (typeof context.fillText == 'function') {
        isSupportFillText = true;
    }
    return isSupportFillText;
}
function supports_video() {
    var videoElement = document.createElement("video");
    var isSupportVideo = false;
    try {
        if (isSupportVideo = !!videoElement.canPlayType) {
            isSupportVideo = new Boolean(isSupportVideo);
            isSupportVideo.ogg = videoElement.canPlayType('video/ogg; codecs="theora"');

            // Workaround required for IE9, which doesn't report video support without audio codec specified.
            //   bug 599718 @ msft connect
            var h264 = 'video/mp4; codecs="avc1.42E01E';
            isSupportVideo.h264 = videoElement.canPlayType(h264 + '"') || videoElement.canPlayType(h264 + ', mp4a.40.2"');

            isSupportVideo.webm = videoElement.canPlayType('video/webm; codecs="vp8, vorbis"');
        }
    } catch (e) { }
    return isSupportVideo;
}
function supports_audio() {
    var audioElement = document.createElement("audio");
    var isSupportAudio = false;
    try {
        if (isSupportAudio = !!audioElement.canPlayType) {
            isSupportAudio = new Boolean(isSupportAudio);
            isSupportAudio.ogg = audioElement.canPlayType('audio/ogg; codecs="vorbis"');
            isSupportAudio.mp3 = audioElement.canPlayType('audio/mpeg;');
            isSupportAudio.wav = audioElement.canPlayType('audio/wav; codecs="1"');
            isSupportAudio.m4a = audioElement.canPlayType('audio/x-m4a;') || audioElement.canPlayType('audio/aac;');
        }
    } catch (e) { }

    return isSupportAudio;
}

function getProgramCode() {
    if (document.URL.indexOf("P=") > -1) {
        var splitMode = document.URL.split("P=");
        var mode = splitMode[1].split("&");
        return mode[0];
    }
    else if (document.URL.indexOf("") > -1) {
        var splitMode = document.URL.split("ProgramGUID=");
        if (splitMode.length >= 2) {
            var mode = splitMode[1].split("&");
            return mode[0];
        } else {
        return "";
        }
        
    }
    else {
        return "";
    }
}