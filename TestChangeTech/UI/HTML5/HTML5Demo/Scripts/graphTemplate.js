function loadDataForGraphTemplate(isBeforePageStart) {
    $(".graphcontainer").css("display", "block");
   /* $("#bubbles").css({width:"800px"});*/
    $("#bubble-blue").css({margin:"0 auto"});
    var lineColors = ["#7db1dc", "#50c80f", "#c25c98", "#efc85d", "#afb2f1", "#ce966a", "#b1ab53"];
    var regPattern = /\{V:.+?\}/gi;
    var graphsObj = null;
    var graphsObjAfterPageStart = null;
    /*if (isBeforePageStart) {
        graphsObj = pageObj.find("Graphs");
    } else {
        graphsObj = $(graphTemplateObj);

    }*/
    graphsObj = pageObj.find("Graphs");
    var graphAfterPageStartArr = [];
    if (!isBeforePageStart) {
        graphsObjAfterPageStart = $(graphTemplateObj);
        if (graphsObjAfterPageStart != null && graphsObjAfterPageStart != undefined && graphsObjAfterPageStart != "") {
            var graphObjAfterPageStart = graphsObjAfterPageStart.find("Graph");
            if (graphObjAfterPageStart != null && graphObjAfterPageStart != undefined && graphObjAfterPageStart !="") {
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
    //var graphsObj = pageObj.find("Graphs");
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
                var maxTime = timeRange.split("-")[1];
                var minTime = timeRange.split("-")[0];
                var xTimeRange = parseInt(maxTime) - parseInt(minTime) + 1;
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
                            var pointTypeNum = $(item).attr("PointType");
                            var pointType = "";
                            if (pointTypeNum == "1") {
                                pointType = "square";
                            } else if (pointTypeNum == "2") {
                                pointType = "triangle";
                            } else if (pointTypeNum == "3") {
                                pointType = "diamond";
                            } else if (pointTypeNum == "4") {
                                pointType = "round";
                            } else if (pointTypeNum == "5") {
                                pointType = "del";
                            }

                            //itemNotes += "<p><span class='graphchartcolor'  style='background-color: " + lineColors[itemIndex] + ";'></span>" + itemName + "</p>";
                            var itmeExpression = $(item).attr("Expression");
                            var itemValues = $(item).attr("Values");
                            var itemValuesArr = itemValues.split(";");
                            var linePoint = [];
                            if (itemValues != "") {
                                var itemValuesArrLength = itemValuesArr.length;
                                if (!isBeforePageStart) {
                                    itemValuesArr[itemValuesArrLength - 1] = graphAfterPageStartArr[itemIndex];
                                }
                                var itemsValueStr = "";
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
                                //lineArr.push({ name: itemName, data: linePoint });
                            } else {
                                for (var n = 0; n < xTimeRange; n++) {
                                    linePoint.push(null);
                                }
                            }
                            lineArr.push({ name: itemName, data: linePoint });
                        });
                        //$(".graphlegend").empty().append($(itemNotes));
                        // $(".graphcontainer h1").remove();
                        // $(".graphlegend").after($(graphTitleStr));
                    }
                }
                coordinate.push({ title: graphTitle, type: type, timeRange: timeRange, timeUnit: timeUnit, timeBaseLineUnit: timeBaselineUnit, scoreRange: scoreRange, lines: lineArr, lineColors: lineColors });
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
        var chart = null;
        var chartType = "";
        var graphTitle=mygraph.title;
        var linesSeries = mygraph.lines;
        var lines = [];
        var colors = mygraph.lineColors;
        var timeTextArr = [];
        var timeRange = mygraph.timeRange;
        var scoreRange = mygraph.scoreRange;
        var minScore = scoreRange.split("-")[0];
        var maxScore = scoreRange.split("-")[1];
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
        var lineContact = [];
        var timeTextArrLen = timeTextArr.length;
        for (var j = 0; j < linesSeries.length; j++) {
            for (var n = 0; n < timeTextArrLen; n++) {
                var numbers = linesSeries[j].data[n];
                if (isNaN(numbers)) {
                    numbers = "";
                }
                var timetext = "<b>" + timeTextArr[n] + "</b><br/>" + numbers;
                tooltipArr.push(timetext);
                lineContact.push(numbers);
            }
            var nameValue = linesSeries[j].name;
            var dataValue = linesSeries[j].data;
            var lineItemLen = dataValue.length;
            var leftItemNum = timeTextArrLen - lineItemLen;
            for (var d = 0; d < leftItemNum; d++) {
                dataValue.push(null);
            }
            lines.push({ name: nameValue, data: dataValue });
        }
        var maxValue = Math.max.apply(Math, lineContact);
        var minValue = Math.min.apply(Math, lineContact);
        var ymax = maxScore;
        var ymin = minScore;
        if (!isNaN(maxValue)) {
            if (maxValue > ymax) {
                ymax = maxValue;
            }
        }
        if (!isNaN(minValue)) {
            if (minValue < ymin) {
                ymin = minValue;
            }
        }
        if (type == "Line graph") {
            chartType = 'line';
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'graphcontainer',
                    type: chartType,
                    marginRight: 40,
                    marginBottom: 40

                },
                title: {
                    text: graphTitle,
                    x: -0 //center
                },
                xAxis: {
                    categories: timeTextArr
                },
                yAxis: {
                    title: {
                        text: 'Scror Range'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }],
                    max: ymax,
                    min: ymin
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>' +
                        this.x + ': ' + this.y + '';
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -30,
                    y: -15,
                    borderWidth: 0
                },
                series: lines
            });
        }
        
    }
    
}