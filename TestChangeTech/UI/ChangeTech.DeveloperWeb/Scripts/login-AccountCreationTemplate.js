
function loadDataForLogin(pageObj) {
    //$(bubbleYellowStr).insertBefore("#bubble-blue");
    var pageType = pageObj.attr("Type");
    var textArray = pageObj.attr('Text').split(";");
    var footerText = pageObj.attr('FooterText');
    var loginStr = "<form>";
    for (var i = 1; i < textArray.length; i++) {
        if (textArray[i] != "" && textArray[i] != undefined && textArray[i] != null) {
            loginStr += '<div class="row"><span class="label">' + textArray[i] + '</span><span class="formw"><input class="textfield loginEmail" type="text" size="25" /></span></div>';
        }
    }
    loginStr += '</form>';
    $("#yellow").append(loginStr);
    $("#yellow").children('form').children(".row").eq(1).children("span").eq(1).html('<input class="textfield" type="password" size="25" name="password" /> ');
    if (footerText != null && footerText != '' && footerText != undefined) {
        $('#yellow ').append('<p id="bubble-login"><a href="" onfocus="this.blur();" title="' + footerText + '" class="' + pageType + '">' + footerText + '</a></p>');
    }

}
function loadDataForPinCode(pageObj) {
    var pageType = pageObj.attr("Type");
    var textArray = pageObj.attr('Text').split(";");
    var footerText = pageObj.attr('FooterText');
    var pinCodeStr = "<form>";
    if (textArray[1] != "" && textArray[1] != undefined && textArray[1] != null) {
        pinCodeStr += '<div class="row"><span class="label">' + textArray[1] + '</span><span class="formw"><input class="textfield loginEmail" type="text" size="25"  /></span></div>';
    }
    pinCodeStr += '</form>';
    $("#yellow").append(pinCodeStr);
    if (textArray[2] != "" && textArray[2] != undefined && textArray[2] != null) {
        $('#yellow ').append('<p id="bubble-pincode"><a href="" onfocus="this.blur();" title="' + textArray[2] + '" class="' + pageType + '">' + textArray[2] + '</a></p>');
    }
}
//*******************
function loadAccountCreation(pageObject) {
    var text = pageObject.attr("Text").split(";");
    var isNeedSerialNumber = $(root).attr("IsNeedSerialNumber");
    var isNeedPinCode = $(root).attr("IsNeedPinCode");
    var isTwoParts = $(root).attr("IsContainTwoParts");
    var bubbleForm = '<form>';
    for (var tmp = 1; tmp <= 3; tmp++) {
        if (text[tmp] != '') {
            bubbleForm += '<div class="row"><span class="label">' + text[tmp] + '</span>' +
			         '<span class="formw">' +
		              '<input class="textfield" type="text" size="25" /></span></div>';
        }
    }
    if (isNeedPinCode == 1 && isTwoParts == 0) {
        if (text[4] != '' && text[4] != undefined) {
            bubbleForm += '<div class="row"><span class="label">' + text[4] + '</span><span class="formw"><input class="textfield" type="text" size="25" /></span></div>';
        }
    }
    if (isNeedSerialNumber == 1) {
        if (text[6] != '' && text[6] != undefined) {
            bubbleForm += '<div class="row"><span class="label">' + text[6] + '</span><span class="formw"><input class="textfield" type="text" size="25" /></span></div>';
        }
    }
    var isSupportTimeZone = $(oraiRoot).attr("IsSupportTimeZone");
    var timeZoneSpecialString = specialStirngs["TimeZone"];
    if (timeZoneSpecialString == undefined || timeZoneSpecialString == null || timeZoneSpecialString == "") {
        timeZoneSpecialString = "TimeZone";
    }
    if (isSupportTimeZone == "1" || isSupportTimeZone == "true") {
        var timeZoneObj = $(oraiRoot).find("TimeZoneList");
        if (timeZoneObj != undefined && timeZoneObj != null) {
            var selectObj = $(timeZoneObj).find("select");
            if (selectObj != undefined && selectObj != null) {
                var selectStr = "<select class='selectbox accountCreationSelect'>" + $(timeZoneObj).find("select")[0].innerHTML + '</select>';
                bubbleForm += '<div class="row timeZone"><span class="label">' + timeZoneSpecialString + '</span><span class="formw">' + selectStr + '</span></div>';
            }
        }
    }
    if (text[5] != '' && text[5] != undefined) {
        bubbleForm += '<div class="row"><span class="noRegister"><input class="checkbox" type="checkbox" name="registerOrNot"  value="" >' + text[5] + '</span></div>';
    }
    bubbleForm += '</form>';
    $("#yellow").append(bubbleForm);
    $("#yellow").children('form').children(".row").eq(1).children("span").eq(1).html('<input class="textfield" type="password" size="25"  /> ');
    $("#yellow").children('form').children(".row").eq(2).children('span').eq(1).html('<input class="textfield" type="password" size="25" />');
}
//*******************
function onClickForPasswordReminder(pageObj) {
    var pageType = pageObj.attr("Type");
    $("#bubble-login a").click(function (e) {
        e.preventDefault();
        if (pageType == "Login") {
            sequenceOrder = 1;
            pageOrder = 2;
        } else {
            sequenceOrder = 1;
            pageOrder = 1;
        }
        loadPage($(root));
    });
}
function onClickForPinCodeReminder(pageObj) {
    var pageType = pageObj.attr("Type");
    if (pageType == "PinCode") {
        $("#bubble-pincode a").click(function (e) {
            e.preventDefault();
            var programGUID = $(root).attr('ProgramGUID');
            var userGUID = $(root).attr('UserGUID');
            var sessionGUID = $(root).find("Session").attr('GUID');
            var pageSequenceOrder = sequenceOrder;
            var pagGUID = $(root).find('Page').filter(function () { return parseInt($(this).attr('Order')) == 1 }).attr('GUID');
            var pinCodeRemindXML = '<XMLModel UserGUID="' + userGUID + '" ' +
											  'ProgramGUID="' + programGUID + '" ' +
											  'SessionGUID="' + sessionGUID + '" ' +
											  'PageSequenceOrder="' + pageSequenceOrder + '" ' +
											  'PageGUID="' + pagGUID + '">' +
											  '<PinCodeReminder/>' +
											  '</XMLModel>';
            $.ajax({
                //url: getReturnDataServerUrl(),
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: pinCodeRemindXML,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var returnMainData = data.substr(2);
                    if (returnCode == "0") {
                        showNormalMessage(returnMainData);
                    } else if (returnCode == "1") {
                        showNormalMessage(returnMainData);
                    } else if (returnCode == "3") {
                        //root=returnMainData;
                        showTipMessage("SendPinCodeSuccess");
                    } else if (returnCode == "4") {
                        showTipMessage("SendPinCodeFailed");

                    }

                },
                error: function () {
                    alert('error');
                }
            });

        });
    }
}
function donnotRegister() {
    $('#bubble-yellow').find("input[name=registerOrNot]:checkbox").change(function () {
        if ($(this).attr("checked")) {
            $(this).parents(".row").siblings().children(".formw").children("input").attr("disabled", "true");
            $(this).parents(".row").siblings().children(".formw").children("input").css({ "background": "#eee" });
            if ($(this).parents(".row").siblings().children(".formw").children("input").val() != '') {
                $(this).parents(".row").siblings().children(".formw").children("input").attr("value", "");
            }

        } else {
            $(this).parents(".row").siblings().children(".formw").children("input").removeAttr("disabled");
            $(this).parents(".row").siblings().children(".formw").children("input").css({ "background": "#fff" });
        }
    });
}
