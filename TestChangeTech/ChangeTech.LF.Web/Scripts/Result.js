$(function () {
    //eg : http://localhost:64990/minhalsoprofil.html?GoWeb=parameterInfo
    var paramterInfo = null;
    var FAIL_MESSAGE = "fail";
    var parseMillisecondsByOneHour = 3600000;
    var encryptUrl = window.location.href;
    var encryptUrlLength = encryptUrl.length;
    var index = encryptUrl.indexOf("=");
    var parameterInfo = encryptUrl.substr(index + 1, encryptUrlLength);
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/ResultHandler.ashx',
        cache: false,
        type: 'Post',
        dataType: "json",
        async: true,
        data: { parameterInfo: parameterInfo, encryptUrl: encryptUrl },
        success: function (data) {
            if (data == "" || data == undefined || data == null) {
                return;
            }
            var result = eval('(' + data + ')');
            var resultType = result.ResultType;
            var content = result.Content; //Content : ResultVariableList
            switch (resultType) {
                case 0: //Type : ResultLine
                    var resultVariables = result.Content.ResultVaribles;
                    var resultDate = result.Content.ResultDateTime;
                    resultDate = ChangeDateFormat(resultDate);
                    var currentDate = new Date();
                    var parseMillisecondsByCurrentDate = currentDate.getTime();
                    var parseMillisecondsByResultDate = new Date(Date.parse(resultDate.replace(/-/g, "/")));
                    parseMillisecondsByResultDate = parseMillisecondsByResultDate.getTime();
                    var dateSub = parseMillisecondsByCurrentDate - parseMillisecondsByResultDate;
                    if ((parseMillisecondsByCurrentDate - parseMillisecondsByResultDate) > parseMillisecondsByOneHour) {
                        window.location.href = window.location.protocol + "//" + window.location.host + "/" + "minhalsoprofil-void.html";
                        return;
                    }
                    SetResultLineClass(resultVariables);
                    $("div[print='green']").css("display", "none");
                    break;
                default:
                    break;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
});

function ClearGoWebSession() {
    var goWebSession = "timeout";
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/ClearGoWebSessionHandler.ashx',
        cache: false,
        type: 'Post',
        dataType: "json",
        async: false,
        data: { webSession: goWebSession },
        success: function (data) {
            alert("clear goweb session successful.");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
}

function GetGoWebSession() {
    var goWebSession = "";
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/GetGoWebSessionHandler.ashx',
        cache: false,
        type: 'Post',
        dataType: "json",
        async: false,
        success: function (data) {
            goWebSession = data;
            return goWebSession;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
}

function SetResultLineClass(resultVariables) {
    var variableValue = "";
    for (var i = 0; i < resultVariables.length; i++) {
        switch (resultVariables[i].VariableName) {
            case "TOB":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'tobak']").attr("class", "green");
                        $("#tobakreport").attr("print","green")
                        break;
                    case "2":
                        $("a[name = 'tobak']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'tobak']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "ALK":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'alkohol']").attr("class", "green");
                        $("#alkoholreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'alkohol']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'alkohol']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "DRO":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'droger']").attr("class", "green");
                        $("#drogerreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'droger']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'droger']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "KOS":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'kost']").attr("class", "green");
                        $("#kostreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'kost']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'kost']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "MOT":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'motion']").attr("class", "green");
                        $("#motionreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'motion']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'motion']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "LEV":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'levnad']").attr("class", "green");
                        $("#levnadreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'levnad']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'levnad']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "STR":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'stress']").attr("class", "green");
                        $("#stressreport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'stress']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'stress']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
            case "MID":
                variableValue = resultVariables[i].VariableValue;
                switch (variableValue) {
                    case "1":
                        $("a[name = 'midja']").attr("class", "green");
                        $("#midjareport").attr("print", "green")
                        break;
                    case "2":
                        $("a[name = 'midja']").attr("class", "yellow");
                        break;
                    case "3":
                        $("a[name = 'midja']").attr("class", "red");
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}

function ChangeDateFormat(resultDate) {
    var date = new Date(parseInt(resultDate.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hh = date.getHours();
    var mm = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var ss = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    return date.getFullYear() + "-" + month + "-" + day + " " + hh + ":" + mm + ":" + ss;
}