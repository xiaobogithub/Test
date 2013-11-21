$(function () {
    var encryptUrl = "";
    var FAIL_MESSAGE = "fail";
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/GetGoWebSessionHandler.ashx',
        cache: false,
        type: 'Post',
        async: false,
        success: function (data) {
            // error message.
            encryptUrl = data;
            var encryptUrlLength = encryptUrl.length;
            var index = encryptUrl.indexOf("=");
            var parameterInfo = encryptUrl.substr(index + 1, encryptUrlLength);
            DecypteGoWebUrl(parameterInfo, encryptUrl);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
});


function DecypteGoWebUrl(parameterInfo, encryptUrl) {
    var parseMillisecondsByOneHour = 3600000;
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
                    var parseMillisecondsByResultDate = parseMillisecondsByResultDate.getTime();
                    var dateSub = parseMillisecondsByCurrentDate - parseMillisecondsByResultDate;
                    if ((parseMillisecondsByCurrentDate - parseMillisecondsByResultDate) > parseMillisecondsByOneHour) {
                        $("#minhalsoprofil").attr("href", "minhalsoprofil.html");
                        $("#dropdown-minhalsoprofil").attr("href", "minhalsoprofil.html").attr("class", "dropdown-minhalsoprofil");
                        $("#dropdown2").attr("class", "dropdown2 hidden");
                    }
                    else {
                        $("#minhalsoprofil").attr("href", encryptUrl);
                        $("#dropdown-minhalsoprofil").attr("href", encryptUrl).attr("class", "dropdown-minhalsoprofil active");
                        $("#dropdown2").attr("class", "dropdown2");
                    }
                    break;
                default:
                    break;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
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