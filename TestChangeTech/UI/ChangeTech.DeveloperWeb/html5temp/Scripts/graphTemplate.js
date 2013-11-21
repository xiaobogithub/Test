function loadDataForGraphTemplate(isBeforePageStart) {
    if (document.getElementById("cvs") != null && document.getElementById("cvs") != undefined) {
        RGraph.Clear(document.getElementById("cvs"));
        RGraph.Reset(document.getElementById("cvs"));
    }
    $(".graphcontainer").css("display", "block");
    $("#bubbles").css({width:"800px"});
    $("#bubble-blue").css({margin:"0 auto"});
    var lineColors = ["#7db1dc", "#50c80f", "#c25c98", "#efc85d", "#afb2f1", "#ce966a", "#b1ab53"];
    var regPattern = /\{V:.+?\}/gi;
    var graphsObj = null;
    var graphsObjAfterPageStart = null;
//    if (isBeforePageStart) {
//         graphsObj = pageObj.find("Graphs");
//    } else {
//         graphsObj = $(graphTemplateObj);
//    }
    graphsObj = pageObj.find("Graphs");
    var graphAfterPageStartArr = [];
    if (!isBeforePageStart) {
        graphsObjAfterPageStart = $(graphTemplateObj);
        if (graphsObjAfterPageStart != null && graphsObjAfterPageStart != undefined && graphsObjAfterPageStart != "") {
            var graphObjAfterPageStart = graphsObjAfterPageStart.find("Graph");
            if (graphObjAfterPageStart != null && graphObjAfterPageStart != undefined && graphObjAfterPageStart != "") {
                $(graphObjAfterPageStart).each(function (indexAfterPageStart, graphAfterPageStart) {
                    var graphAfterPageStartItems = $(graphAfterPageStart).find("Items");
                    if (graphAfterPageStartItems != null && graphAfterPageStartItems != undefined && graphAfterPageStartItems != '') {
                        $(graphAfterPageStartItems).each(function (itemsAfterPageStartIndex, itemsAfterPageStart) {
                            var itemAfterPageStart = $(itemsAfterPageStart).find("Item");
                            if (itemAfterPageStart != undefined && itemAfterPageStart != null && itemAfterPageStart != "") {
                                $(itemAfterPageStart).each(function (itemAfterPageStart, itemAfterPageStart) {
                                    var itemAfterPageStartValues = $(itemAfterPageStart).attr("Values");
                                    graphAfterPageStartArr.push(itemAfterPageStartValues);
                                });
                            }
                        });
                    }
                });
            }
        }
    }
    console.log(graphAfterPageStartArr);
    var graphTitle = pageObj.attr("Title");
    var graphTitleStr = "<h1>" + graphTitle + "</h1>";
    if (graphsObj != null && graphsObj != undefined && graphsObj != '') {
        var graphObj = graphsObj.find("Graph");
        var coordinate = [];
        if (graphObj != null && graphObj != undefined && graphObj != '') {
            $(graphObj).each(function (index, graph) {
                var type = $(graph).attr("Type");
                var scoreRange = $(graph).attr("ScoreRange");
                var timeRange = $(graph).attr("TimeRange");
                var timeUnit = $(graph).attr("TimeUnit");
                var timeBaselineUnit = $(graph).attr("TimeBaselineUnit");
                //var badScoreRangeStr = $(graph).attr("BadScoreRange");
                //var mediumScoreRangeStr = $(graph).attr("MediumScoreRange");
                //var goodScoreRangeStr = $(graph).attr("GoodScoreRange");
                //var badScoreRangeArr = badScoreRangeStr.split(";");
                //var mediumScoreRangeArr = mediumScoreRangeStr.split(";");
                // var goodScoreRangeArr = goodScoreRangeStr.split(";");
                var itemNotes = '';
                var graphItems = $(graph).find("Items");
                if (graphItems != null && graphItems != undefined && graphItems != '') {
                    var graphItem = graphItems.find("Item");
                    var lineArr = [];
                    if (graphItem != null && graphItem != undefined && graphItem != '') {
                        $(graphItem).each(function (itemIndex, item) {
                            var itemName = $(item).attr("Name");
                            itemNotes += "<p><span class='graphchartcolor'  style='background-color: " + lineColors[itemIndex] + ";'></span>" + itemName + "</p>";
                            var itmeExpression = $(item).attr("Expression");
                            var itemValues = $(item).attr("Values");
                            if (itemValues == '') {
                                itemValues = '0';
                            }
                            var itemValuesArr = itemValues.split(";");
                            if (itemValues != "") {
                                var itemValuesArrLength = itemValuesArr.length;
                                if (!isBeforePageStart) {
                                    itemValuesArr[itemValuesArrLength - 1] = graphAfterPageStartArr[itemIndex];
                                }
                                var itemsValueStr = "";
                                var linePoint = [];
                                for (var i = 0; i < itemValuesArrLength; i++) {
                                    var value = itemValuesArr[i];
                                    var pattern = /\{V:.+?\}/;
                                    if (pattern.test(value)) {
                                        var matches = value.match(regPattern);
                                        var matchesLength = matches.length;
                                        for (var j = 0; j < matchesLength; j++) {
                                            var variable = matches[j];
                                            if (variable != null && variable != undefined && variable != '') {
                                                var variableName = variable.split("{V:")[1].split("}")[0];
                                                var variableValue = getProgramVariableValue(variableName).toString();
                                                //itemValues = itemValues.replace(variable, variableValue);
                                                value = value.replace(variable, variableValue);
                                            }
                                        }
                                        var pointValue = eval(value);
                                    } else {
                                        pointValue = eval(value);
                                    }
                                    linePoint.push(pointValue);
                                }
                                lineArr.push(linePoint);
                            }
                        });
                        $(".graphlegend").empty().append($(itemNotes));
                        $(".graphcontainer h1").remove();
                        $(".graphlegend").after($(graphTitleStr));
                    }
                }
                coordinate.push({ type: type, timeRange: timeRange, timeUnit: timeUnit, timeBaseLineUnit: timeBaselineUnit, scoreRange: scoreRange, linesArr: lineArr, lineColors: lineColors });
            });
            drawGraph(coordinate);
        }
    }
}
function drawGraph(coordinate) {
    var coordinateLen = coordinate.length;
    for (var i = 0; i < coordinateLen; i++) {
        var mygraph = coordinate[i];
        var type = mygraph.type;
        var myLine = null;
        var lines = [];
        if (type == "Line graph") {
            lines = mygraph.linesArr;
            var timeTextArr = [];
            var timeRange = mygraph.timeRange;
            var scoreRange = mygraph.scoreRange;
            var minScore = parseFloat(scoreRange.split("-")[0]);
            var maxScore = parseFloat(scoreRange.split("-")[1]);
            var minTime = timeRange.split("-")[0];
            var maxTime = timeRange.split("-")[1];
            var timeUnit = mygraph.timeUnit;
            var timeBaseLineUnit = mygraph.timeBaseLineUnit;
            if (parseInt(minTime) <= 0) {
                timeTextArr.push(timeBaseLineUnit);
            }
            for (var i = 1; i <= parseInt(maxTime); i++) {
                timeTextArr.push(timeUnit + i);
            }
            var tooltipArr = [];
            var timeTextArrLen = timeTextArr.length;
            var lineContact = [];
            for (var j = 0; j < lines.length; j++) {
                for (var n = 0; n < timeTextArrLen; n++) {
                    var numbers = lines[j][n];
                    if (isNaN(numbers)) {
                        numbers = "";
                    }
                    var timetext = "<b>" + timeTextArr[n] + "</b><br/>" + numbers;
                    tooltipArr.push(timetext);
                    lineContact.push(numbers);
                }
                var lineItemLen = lines[j].length;
                var leftItemNum = timeTextArrLen - lineItemLen;
                for (var d = 0; d < leftItemNum; d++) {
                    lines[j].push(null);
                }
            }
               
            //if (lines.length != 0) {
                var maxValue = Math.max.apply(Math, lineContact);
                var minValue = Math.min.apply(Math, lineContact);
                var ymax = maxScore;
                var ymin = minScore;
                if (!isNaN(maxValue)) {
                   /* if (maxValue > ymax) {
                        ymax = maxValue;
                    }*/
                    ymax = maxValue;
                }
                if (!isNaN(minValue)) {
                    if (minValue < ymin) {
                        ymin = minValue;
                     }
                }
                myLine = new RGraph.Line('cvs',lines)
                .Set('chart.labels', timeTextArr)
                .Set('chart.colors', mygraph.lineColors)
                .Set('chart.ymax', ymax)
                .Set('chart.ymin', ymin)
                .Set('chart.gutter.left', 20)
                .Set('chart.gutter.right', 15)
                .Set('chart.gutter.bottom', 25)
                .Set('chart.tooltips.hmargin', 25)
                .Set('chart.linewidth', 6)
                .Set('chart.hmargin', 25)
                .Set('chart.text.color', '#6290b6')
                .Set('chart.background.grid.autofit', true)
                .Set('chart.background.grid.vlines', false)
                .Set('chart.background.grid.color', '#6290b6')
                .Set('chart.background.grid.border', false)
                .Set('chart.noxaxis', false)
                .Set('chart.noyaxis', false)
                .Set('chart.axis.color', '#6290b6')
                .Set('chart.text.color', '#6290b6')
                .Set('chart.spline', false)
                .Set('chart.tooltips', tooltipArr)
                .Set('tickmarks', 'circle')
                .Set('ticksize', 3)
                if (RGraph.isOld()) {
                    myLine.Set('chart.shadow.offsetx', 3);
                    myLine.Set('chart.shadow.offsety', 3);
                    myLine.Set('chart.shadow.color', '#aaa');
                    myLine.Draw();
                } else {
                    myLine.Set('chart.tooltips', tooltipArr);
                    //RGraph.Effects.Line.jQuery.UnfoldFromCenterTrace(myLine, { 'duration': 100 });
                    myLine.Draw();
                }
            //}
        }
    }

}