function loadDataForResultTemplate(pageObj) {
    var resultGraphsObj = pageObj.find("ResultGraphs");
    var resultLinesObj = pageObj.find("ResultLines");
    var imageArr = [];
    if (resultGraphsObj != undefined && resultGraphsObj != null) {
        var resultGraph = resultGraphsObj.find("ResultGraph");
        if (resultGraph != undefined && resultGraph != null) {
            $(resultGraph).each(function (graphIndex,graph) {
            var graphGUID=$(graph).attr("GUID");
            var value = $(graph).attr("VariableValue");
            var image = $(graph).attr("Image");
                imageArr[value] = image;
            });
        }
    }
    var lineStr = "";
    if (resultLinesObj != undefined && resultLinesObj != null) {
        var resultLine = resultLinesObj.find("ResultLine");
        if (resultLine != undefined && resultLine != null) {
            $(resultLine).each(function (lineIndex, line) {
                var lineGUID = $(line).attr("GUID");
                var url = $(line).attr("URL");
                var text = $(line).attr("Text");
                var programVariableName = $(line).attr("ProgramVariable");
                var proVariableValue = getProgramVariableValue(programVariableName).toString();
                var image = imageArr[proVariableValue];
                if (image != null && image != undefined && image != "") {
                    var imageUrl = getURL("originalimagecontainerRoot") + image;
                } else {
                    var imageUrl = "";
                }
                if (url != null && url != undefined && url != "") {
                    var textStr = "<td class='textColumn'><a href=" + url + " title=" + text + " target='_blank'>" + text + " </a></td>";
                } else {
                    var textStr = "<td class='textColumn'>"+text+"</td>";
                }
                var imageStr = "<td class='imageColumn'><img src='" + imageUrl + "'  alt='" + text + "' /></td>";
                lineStr += "<tr>" + textStr + "" + imageStr + "</tr>";
            });
            var resultStr = "<div id='resultContainer'><table class='resultTable'>" + lineStr + "</table></div>";
            if (lineStr != "") {
                $("#white").append(resultStr);
            }
        }
    }
    setTimeout(bubbleContentHeight,1000);
}
function bubbleContentHeight() {
    var bubbleContentHeight = $("#bubble-white .bubcontent").outerHeight(true);
    $("#bubble-white").css("height", bubbleContentHeight + "px");
}