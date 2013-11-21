

function loadTitleAndTextForOtherTemplate(pageObj) {

    var title = pageObj.attr('Title');
    var text = pageObj.attr('Text');
    var footerText = pageObj.attr("FooterText");
    var textArray = text.split(";");
    var textDesc = textArray[0];
    var pageType = pageObj.attr("Type");
    if (title != null && title != undefined && title != '') {
        $('#white').append('<h1>' + title + '</h1>');
        /*if ((text == "" || text == null || text == undefined) && (footerText == "" || footerText == null || footerText == undefined)) {
            $('#white>h1').css({ 'font-size': '40px' })
        }*/
    }
    if (text != null && text != null && text != undefined) {
        if (textDesc != null && textDesc != '' && textDesc != undefined) {
            parseTableStringByRegExpression(textDesc);
            //$('#white').append('<p>' + textDesc + '</p>');	
            // $('#white p').addClass('bubble-text');
        }
    }
    $("#white p").each(function (index, p) {
        if ($(p).text() == "" || $(p).text() == "&nbsp;") {
            $(p).remove();
        }
    });
    $('<p class="emptyline">&nbsp;</p>').insertAfter("#white p.bubble-text");
    $("#white p.emptyline").last().remove();
}

function loadTitleAndText(pageObj) {
    //{{{-------------------------------- intalize a object and give it some method START
    var insteadStr = ''
    String.prototype.insteadVar = function () {
        if (this.match(/\{V:.+?\}/) != null) {
            var temp = this.match(/\{V:.+?\}/).toString();
            var prefixIndex = this.indexOf(temp);
            var suffixIndex = this.indexOf(temp) + temp.length;
            var prefix = this.slice(0, prefixIndex);
            var suffix = this.slice(suffixIndex);
            var variable = temp.split('{V:')[1].split('}')[0].trim();
            var tempFlag = false;
            for (var i = 0; i < proV.length; i++) {
                if (proV[i][0] == variable) {
                    insteadStr = prefix + "<b>"+proV[i][2]+"</b>" + suffix;
                    tempFlag = true;
                }
            }
            if (tempFlag == false) { alert('unavailable variable ' + variable); }
            else { insteadStr.insteadVar(); }
        } else {
            insteadStr = this;
        }
        return insteadStr;
    }
    var insteadRoundStr = '';
    String.prototype.parserRound = function () {
        if (this.match(/Round\(.*?\,.*?\)/) != null) {
            var temp = this.match(/Round\(.*?\,.*?\)/).toString();
            var prefixIndex = this.indexOf(temp);
            var suffixIndex = this.indexOf(temp) + temp.length;
            var prefix = this.slice(0, prefixIndex);
            var suffix = this.slice(suffixIndex);
            var temp1 = eval(temp.split('Round(')[1].split(',')[0]).toString();

            var temp2 = parseInt(temp.split(',')[1].split(')')[0]);
            if (temp1.indexOf('.') == -1) {
                insteadRoundStr = prefix +"<b>"+ temp1 +"</b>"+ suffix;
            } else {
                if (temp2 > 0) {
                    var temp3 = parseInt(temp1.split('.')[1].slice(0, temp2));
                    var temp4 = parseInt(temp1.split('.')[1].slice(temp2, temp2 + 1));
                    var temp6 = parseInt(temp3.toString().slice(temp3.toString().length - 1, temp3.toString().length));
                    if (temp4 > 4 && temp6 < 9) {
                        temp3 += 1;
                    }
                    var temp5 = temp1.split('.')[0] + '.' + temp3;
                    insteadRoundStr = prefix +"<b>"+ temp5+"</b>" + suffix;
                } else {
                    var temp7 = parseInt(temp1.toString().split('.')[0]);
                    var temp8 = parseInt(temp1.toString().split('.')[1].slice(0, 1));
                    if (temp8 > 4) {
                        temp7 += 1;
                    }
                    insteadRoundStr = prefix +"<b>"+ temp7+"</b>" + suffix;
                }
            }

            insteadRoundStr.parserRound();
        } else {
            insteadRoundStr = this;
        }
        return insteadRoundStr;
    }


    //intalize a object and give it some method END --------------------------}}}
    var title = pageObj.attr('Title');
    var text = pageObj.attr('Text');

    var footerText = pageObj.attr("FooterText");
    if (title != null || text != null || footerText != null) {
        //$('#bubbles').prepend(bubbleWhiteBubbleStr);
    }
    if (title != null && title != '' && title != undefined) {
        title.insteadVar();
        insteadStr.parserRound();
        $('#white').append('<h1>' + insteadRoundStr + '</h1>');
       /* if ((text == "" || text == null || text == undefined) && (footerText == "" || footerText == null || footerText == undefined)) {
            $('#white >h1').css({ 'font-size': '40px' })
        }*/
    }
    if (text != null && text != '' && text != undefined) {

        text.insteadVar();

        insteadStr.parserRound();
        var textArr = insteadRoundStr.split("<br class='emptyline'>");
        var length = textArr.length;
        for (var i = 0; i < length; i++) {
            if (textArr[i] != "" && textArr[i] != undefined && textArr[i] != null) {
                parseTableStringByRegExpression(textArr[i]);
                // $('#white').append('<p>' + textArr[i] + '</p>');
                //$('#white p').addClass('bubble-text');
            }
        }
    }
    if (footerText != null && footerText != '' && footerText != undefined) {
        footerText.insteadVar();
        insteadStr.parserRound();
        var footerTextArr = insteadRoundStr.split("<br class='emptyline'>");
        var footerTextLength = footerText.length;
        for (var j = 0; j < footerTextLength; j++) {
            if (footerTextArr[j] != undefined && footerTextArr[j] != "" && footerTextArr[j] != null) {
                $('#white').append('<p>' + footerTextArr[j] + '</p>');
                $('#white p').addClass('bubble-text');
            }
        }
    }
    $("#white p").each(function (index, p) {
        if ($(p).text() == "" || $(p).text() == "&nbsp;") {
            $(p).remove();
        }
    });
    $('<p class="emptyline">&nbsp;</p>').insertAfter("#white p.bubble-text");
    $("#white p.emptyline").last().remove();

}

/// Get Media Node function
function loadMediaData(pageObj) {
    //var bubbleBlackStr='<div class="bubble-black"><div class="bubble-black-arrow"></div></div>'
    var videoSize;
    var audioSize;
    /*if (navigator.userAgent.toLowerCase().match(/iPhone/i) != null) {
        // if the userAgent is iphone
        videoSize = { width: 270, height: 144 };
        audioSize = { width: 270, height: 29 };
    } else {
        videoSize = { width: 502, height: 284 };
        audioSize = { width: 502, height: 29 };
    }*/
   /* videoSize = { width: 502, height: 284 };
    audioSize = { width: 502, height: 29 };*/
    videoSize = { height: 240 };
//    if (navigator.userAgent.match(/iPhone|iPad|Android/i)) {
//        audioSize = { height: 240 }
//    } else {
//        audioSize = { height: 30 };
    //    }
    audioSize = { height: 30 };
    var mediaNode = pageObj.find("Media");
    if (mediaNode != null && mediaNode != undefined) {
        var mediaType = mediaNode.attr("Type");
        var mediaFile = mediaNode.attr("Media");
        if (mediaType != null && mediaType != "" && mediaType != undefined) {
            if (mediaType == "Video") {
                if (mediaFile != null && mediaFile != '' && mediaFile != undefined) {
                    //$(".bubble-black").remove();
                    //$(bubbleBlackStr).insertBefore("#bubble-blue");
                    //var mediaFileUrl = "http://changetechstorage.blob.core.windows.net/videocontainer/" + mediaFile;
                    var mediaFileUrl = getURL("videocontainerRoot") + mediaFile;
                    var videoStr = ' <div id="videoplayer2">Loading the videoplayer ...</div>' +
						           '<script type="text/javascript">' +
	                                  'jwplayer("videoplayer2").setup(' +
	                                   '{' +
                                      'file: "' + mediaFileUrl + '",' +
                                       'image:"jwplayer/playerposter.jpg",'+
                                     //'file :"http://content.bitsontherun.com/videos/3XnJSIm4-I3ZmuSFT.m4a",' +
                                     //'skin: "jwplayer/skin/video/changetech_video/changetech_video.xml",' +
									  'height: "' + videoSize.height + 'px",' +
									  'width: "100%",' +
									 // 'controlbar: "bottom"' +
                                      '});' +
                                     '</script>';
                    $("#black").append($(videoStr));
                }
            }
            /*else if (mediaType == "Illustration") {
                /// show Illustration here 
                if (mediaFile != null && mediaFile != undefined && mediaFile != '') {
                    //var mediaFileUrl = "http://changetechstorage.blob.core.windows.net/originalimagecontainer/" + mediaFile;
                    var mediaFileUrl = getURL("originalimagecontainerRoot") + mediaFile;
                    $("#black").append('<img src="' + mediaFileUrl + '" alt="' + mediaFile + '" width="485px" />');
                    var bubbleBlackHeight = $("#bubble-black .bubcontent").outerHeight(true);
                    $("#bubble-black").css("text-align", "center");
                    $("#bubble-black").css("height", bubbleBlackHeight);
                }
            } //*/
            else if (mediaType == "Audio") {
                /// show Audito element here 
                if (mediaFile != null && mediaFile != undefined && mediaFile != '') {
                    //var audioUrl = 'http://changetechstorage.blob.core.windows.net/audiocontainer/' + mediaFile;
                    var audioUrl = getURL("audiocontainerRoot") + mediaFile;
                    var audioStr = '<div id="audioplayer">Loading the audioplayer ...</div>' +
						            '<script type="text/javascript">' +
		                             'jwplayer("audioplayer").setup(' +
		                             '{' +
                                    //'flashplayer: "jwplayer/player.swf",' +
                                     'file: "' + audioUrl + '",' +
                                     //'skin: "jwplayer/skin/audio/changetech_audio/changetech_audio.xml",' +
                                     'width:"100%",' +
                                     'height: "'+audioSize.height+'",' +
	                                 //'icons:false,' +
	                                 //'controlbar: "bottom"' +
                                      '});' +
                                     '</script>';
                    $("#black").append($(audioStr));
                }
            }
        }
    }
}

function reloadMedia(pageObj) {

    var mediaNode = pageObj.find("Media");
    if (mediaNode != undefined && mediaNode != null) {
        var mediaAttr = mediaNode.attr("Media");
        if (mediaAttr != '' && mediaAttr != undefined && mediaAttr != null) {
            loadMediaData(pageObj);
        }
    }
}
// added on 2011-11-24 for parse Text string on page,main function is to parse text with table
function parseTableString(tableStr) {
    var wholeTable = "";
    var xml = "";
    if ($.browser.msie) {
        xml = new ActiveXObject("Microsoft.XMLDOM");
        xml.async = false;
        xml.loadXML(tableStr);
    } else {
        xml = new DOMParser().parseFromString(tableStr, "text/xml");
    }

    var tableElement = $(tableStr).filter("table");
    var columnsAttrValue = $(tableElement).attr("columns");
    var columnsAttr = "";
    var columnsAttrArray = [];
    if (columnsAttrValue != undefined && columnsAttrValue != null && columnsAttrValue != '') {
        columnsAttr = columnsAttrValue;
        columnsAttrArray = columnsAttr.split(",");
    }
    var colorAttrValue = $(tableElement).attr("color");
    var colorAttr = "";
    if (colorAttrValue != undefined && colorAttrValue != null && colorAttrValue != '') {
        colorAttr = "#" + colorAttrValue.slice(2);
    }
    var fontColorAttrValue = $(tableElement).attr("fontColor");
    var fontColorAttr = "";
    if (fontColorAttrValue != undefined && fontColorAttrValue != null && fontColorAttrValue != '') {
        fontColorAttr = "#" + fontColorAttrValue.slice(2);
    }
    var widthAttrValue = $(tableElement).attr("width");
    var widthAttr = "";
    if (widthAttrValue != undefined && widthAttrValue != null && widthAttrValue != '') {
        if (widthAttrValue.indexOf("px") < 0) {
            widthAttr = widthAttrValue + "px";
        } else {
            widthAttr = widthAttrValue;
        }
    }
    var fontSizeAttrValue = $(tableElement).attr("fontSize");
    var fontSizeAttr = "";
    if (fontSizeAttrValue != undefined && fontSizeAttrValue != null && fontSizeAttrValue != '') {
        fontSizeAttr = fontSizeAttrValue;
    }

    var thicknessAttrValue = $(tableElement).attr("thickness");
    var thicknessAttr = "";
    var thicknessStyle = "";
    var tdStyle = "";
    if (thicknessAttrValue != null && thicknessAttrValue != undefined && thicknessAttrValue != '') {
        if (thicknessAttrValue.indexOf("px") < 0) {
            thicknessAttr = thicknessAttrValue + "px";
        } else {
            thicknessAttr = thicknessAttrValue;
        }
        thicknessStyle = "solid";
        tdStyle = 'border-color:' + colorAttr + ';border-width:' + thicknessAttr + ';border-style:' + thicknessStyle + '';
    }
    var tableStyle = 'width:' + widthAttr + ';font-size:' + fontSizeAttr + ';color:' + fontColorAttr + ';border-collapse:collapse;';

    var startIndex = tableStr.indexOf(">");
    var endIndex = tableStr.indexOf("</table>");
    var tableContent = tableStr.substring(startIndex + 1, endIndex);
    var tableRows = tableContent.split(";");
    var rowsLength = tableRows.length;
    var rowStr = "";
    for (var r = 0; r < rowsLength; r++) {
        var tableRow = tableRows[r];
        var tableColumns = tableRow.split(",");
        var columnsLength = tableColumns.length;
        var columnStr = "";
        for (var c = 0; c < columnsLength; c++) {
            var columns = tableColumns[c];
            var tdWidth = "";
            if (columnsAttrArray[c] != '' && columnsAttrArray[c] != null && columnsAttrArray[c] != undefined) {
                tdWidth = columnsAttrArray[c];
            }
            columnStr += "<td style='" + tdStyle + "' width='" + tdWidth + "'>" + columns + "</td>";
        }
        rowStr += "<tr>" + columnStr + "</tr>";
    }

    wholeTable = "<table cellspacing='0' cellpadding='0' style='" + tableStyle + "'>" + rowStr + "</table>";
    return wholeTable;
}
function parseTableStringByRegExpression(text) {
    var strBeforeTable = "";
    var strAfterTable = "";
    var strTable = "";
    var pattern = /<table.*?>[\s\S]*?<\/table>/gm;
    if (pattern.test(text)) {
        strBeforeTable = RegExp.leftContext;
        strAfterTable = RegExp.rightContext;
        strTable = RegExp.lastMatch;
        if (pattern.test(strBeforeTable)) {

            parseTableStringByRegExpression(strBeforeTable);
        } else {
            $("#white").append("<p>" + strBeforeTable + "</p>");
        }
        if (parseTableString(strTable) != '') {
            $("#white").append("<p>" + parseTableString(strTable) + "</p>");
        }
        if (pattern.test(strAfterTable)) {
            parseTableStringByRegExpression(strAfterTable);
        } else {
            $("#white").append("<p>" + strAfterTable + "</p>");
        }

    } else {
        $("#white").append("<p>" + text + "</p>");
    }
    $('#white p').addClass('bubble-text');
}
