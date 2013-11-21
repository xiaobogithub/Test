//logic
var sequenceOrder = 1;
var pageOrder = 1;
var flag = 0;
var proV = [];
var presenterImageSize = null;
var rootXmlUrl = "";
var para = [];
var root;
var sessionName = '';
var tipMessages = [];
var returnDataUrl = "";
var itemScore = [];
var GUIDValue = [];
var isRequired = [];
var stepHistory = [];
var specialStirngs = [];
var step = -1;
var quesFlag = true;
var sequenceOrderBeforeParse = 0;
var pageOrderBeforeparse = 0;
var sessionEndFlag = false;
var historyAnswersObj = [];
var historyAnswers = [];
var historyBlackHeight = [];
var itemsInfo = [];
var isOverLoaded = false;
var isUnderLoaded = false;
var sequenceOrderBeforeGosub = 0;
var pageOrderBeforeGosub = 0;
var seqGuid = '';
var seqGuidBeforeParse = '';
var pageObj;
var bubbleOrangeWidth = 0;
var programRoomName = [];
var backbtnFlag = false;
//if the expression doesn't decide which page to go, OR there is no afterexpression, then just go to next page.
var directllyGo = true;
var ctppInfoLoadingFlag = false;
var oraiRoot;
var ctppXmlobj = '';
var retakeValue = "";
var programVariables = [];
var afterisEnd = false;
var sequenceGUIDBeforeGOSUB = "";
var pageGUIDBeforeGOSUB = "";
var relapseSequenceGUID = "";
var isHelpOrReportButton = false;
var sequenceGUIDBeforeGOSUBArray = [];
var pageGUIDBeforeGOSUBArray = [];
var pageInfoGUIDBeforeGOSUBArray = [];
var isReturnFromGoSub = false;
//var backPageArray = [];
var isBackButton = false;
var isFirstLoaded = false;
var isCatchUpDay = false;
var excuteBefore = true;
var excuteAfter = true;
var goSubReturnTime = 1;
var uersUpdateTimeZone = "";
var userSMSToEmailValue = false;
var myplatform = "";
var goSubStepArr = [];
var isPreviewMode = false;
var isBackFromPreviewEndPage = false;
var isLoadingNextPage = false;
var isGraphTemplate = false;
var graphTemplateObj = null;
var isSamePage = false;
var pageGUIDBeforeExcuteExpression = "";
var pageGUIDAfterExpression = "";
function loadingAnimation(isFirstTimeLoadingPage) {
    $.ajax({
        // url: getIntroDataServerUrl(),
        url: getURL("intro"),
        type: "POST",
        dataType: "text",
        success: function (returnData) {
            var returnCode = returnData.substring(0, 1);
            ctppXmlobj = returnData.substr(2);
            if (returnCode == "1") {
                if (isFirstTimeLoadingPage) {
                    supports_html5(isFirstTimeLoadingPage);
                } else {
                    startLoadingPage(isFirstTimeLoadingPage);
                }
            }

        },
        error: function () {
            alert("failed to loading XML");
        }
    });
}
function startLoadingPage(isFirstTimeLoadingPage) {
    loadIntroPage(ctppXmlobj, function () {
        sessionName = $(root).find("Session").attr("Name");
        // comment by xmx on 2012/08/16
        if (sessionEndFlag && $(root).find("Session").length > 0) {
            setTimeout(sessionEndingAjax, 1000);
        } else {
            if (sessionName == "Login") {
                setTimeout(loginLoadingAjax, 1000);
            } else if (sessionName == "PinCode") {
                setTimeout(pinCodeLoadingAjax, 1000);
            } else {
                setTimeout(loadXmlAjax, 1000);
            }
        }
        // end comment by xmx on 2012/08/16
    });
}
function bubbleBlueNextLoadPage(reginsInfo) {
    isBackButton = false;
    if (step <= 0) {
        goSubReturnTime = 1;
        excuteBefore = true;
    }
    var pageType = pageObj.attr("Type");
    if (pageType == "Get information") {
        saveCheckbox(pageObj);
    }
    var pageGUID = pageObj.attr("GUID");
    pageGUIDBeforeExcuteExpression = pageGUID;
    if ($("#bubble-black").css("display") != "none") {
        historyBlackHeight[pageGUID] = $("#bubble-black").height();
    }
    if ($("#bubble-yellow").css("display") != "none") {
        historyAnswersObj[pageGUID] = $("#bubble-yellow").height();
    }
    //get the current sequence order
    if (sequenceOrder == undefined || sequenceOrder == '' || isNaN(sequenceOrder)) {
        /*if ( $( oraiRoot ).find( 'Session' ).length > 0 ) {
        seqGuidBeforeParse = seqGuid;
        } else {
        seqGuidBeforeParse = '';
        }*/
        seqGuidBeforeParse = seqGuid;
    } else {
        seqGuidBeforeParse = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).attr('GUID');
    }
    if ($(oraiRoot).find("Session").length > 0) {
        sequenceOrderBeforeParse = sequenceOrder;
        pageOrderBeforeparse = pageOrder;
    } else {
        sequenceOrderBeforeParse = '';
        pageOrderBeforeparse = pageOrder;
        // comment by xmx on 2012/05/23
    }


    //send the MSG of the end of the current page to backend;
    msgPageEnd();
    //the login situation
    if (sessionName == 'Login') {
        processLogin();
        //start of processPasswordRemind()
        processPasswordRemind();
        // end of processpasswordRemind()
    } else if (sessionName == "PinCode") {
        processPinCode();
    } else {
        pageObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
						.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
        var pageType = pageObj.attr('Type');
        var isNeedSerialNum = pageObj.attr("IsNeedSerialNumber");
        var pinCode = $(root).attr('IsNeedPinCode');
        if (pageType == 'Account creation') {
            // start of processAccountCreation(isNeedSerialNum, pinCode, reginsInfo);
            processAccountCreation(isNeedSerialNum, pinCode, reginsInfo);
            // end of processAccountCreation(isNeedSerialNum, pinCode, reginsInfo);
        } else if (sessionName == 'relapse') {
            // start of processRelapse();
            processRelapse();
            /*var len = $(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;
            if ($(oraiRoot).find("Session").length > 0 && $(oraiRoot).attr("IsHelpInCTPP") == undefined && $(oraiRoot).attr("IsReportInCTPP") == undefined) {
            if (pageOrder <= len) {
            sequenceOrder = '';
            processLive($(root));

            } else {
            root = oraiRoot;
            sessionName = $(root).find("Session").attr("Name");
            sequenceOrder = sequenceOrderBeforeGosub;
            pageOrder = pageOrderBeforeGosub;
            goToNextDirectlly();
            loadPage($(root));
            }
            } else {
            sequenceOrder = '';
            processLive($(root));
            }*/
            // end of processRelapse();
        } else {
            processLive($(root));
        }
    }
    if (pageType == "Payment") {
        var programGUID = $(root).attr('ProgramGUID');
        var userGUID = $(root).attr('UserGUID');
        var paymentUrl = getURL('payment') + '?ProgramGUID=' + programGUID + '&UserGUID=' + userGUID;
        window.location.replace(paymentUrl);
    }
}
function processPinCode() {
    pageObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
						.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
    var pageType = pageObj.attr('Type');
    if (pageType == "PinCode") {
        var pinCode = $('#bubble-yellow .row').eq(0).children('.formw').children('input').val();
        if (pinCode == "") {
            showTipMessage('PinCodeWrong');
        } else {
            var isFirstTimeLoadingPage = true;
            startLoadingPage(isFirstTimeLoadingPage);
            //reloadedIntro(isFirstTimeLoadingPage);
           /* var proGUID = $(oraiRoot).attr('ProgramGUID');
            var userGUID = $(oraiRoot).attr('UserGUID');
            var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            var pageSequenceOrder = sequenceOrder;
            var pageGUID = pageObj.attr('GUID');
            var pinCodeBackXML = '<XMLModel UserGUID="' + userGUID + '" ' +
											'ProgramGUID="' + proGUID + '" ' +
											'SessionGUID="' + sessionGUID + '" ' +
											'PageSequenceOrder="' + pageSequenceOrder + '" ' +
											'PageGUID="' + pageGUID + '">' +
											'<PinCode  PinCode="' + pinCode + '"/>' +
											'</XMLModel>';
            $.ajax({
                //url: getReturnDataServerUrl(),
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: pinCodeBackXML,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var returnData = data.substr(2);

                    if (returnCode == '1') {
                        tipMessages = [];
                        programRoomName = [];
                        programVariables = [];
                        specialStirngs = [];
                        step = -1;
                        stepHistory = [];
                        proV = [];
                        userUpdateTimeZone = "";
                        userSMSToEmailValue = false;
                        root = returnData;
                        encodingRootXML();
                        oraiRoot = root;
                        introAnimation();
                        sessionName = $(root).find("Session").attr("Name");
                        // start added for retake by XMX on 2011/12/1
                        retakeValue = $(root).attr("IsRetake").toLowerCase();
                        if (retakeValue == "true" || retakeValue == "1") {
                            // getProgramVariable($(root));
                        }
                        var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
                        if (isSMSToEmail == "1" || isSMSToEmail == "true") {
                            userSMSToEmailValue = true;
                        } else {
                            userSMSToEmailValue = false;
                        }

                        // for TimeZone 
                        var userTimeZone = $(oraiRoot).attr("UserTimeZone");
                        var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
                        if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
                            userUpdateTimeZone = userTimeZone;
                        } else {
                            userUpdateTimeZone = programTimeZone;
                        }
                        getTipMessages($(oraiRoot));
                        getSepcialStrings($(oraiRoot));
                        proArr($(oraiRoot));
                    } else if (returnCode == '0') {
                        //showTipMessage('SendPinCodeFailed');
                        showTipMessage('PinCodeWrong');
                        //showNormalMessage(returnData);
                    } else if (returnCode == "2") {
                        if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                            var message = returnData;
                            var ctppHostStr = getURL('ctpp');
                            var ctppUrl = ctppHostStr + '?CTPP=' + message;
                            window.location.replace(ctppUrl);
                        }
                    }
                },
                error: function () {
                    alert(' read  XML unsucessfully');
                }
            });*/
        }
    }
}
function pinCodeLoadingAjax() {
    var pinCode = $('#bubble-yellow .row').eq(0).children('.formw').children('input').val();
    var proGUID = $(oraiRoot).attr('ProgramGUID');
    var userGUID = $(oraiRoot).attr('UserGUID');
    var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
    var pageSequenceOrder = sequenceOrder;
    var pageGUID = pageObj.attr('GUID');
    var pinCodeBackXML = '<XMLModel UserGUID="' + userGUID + '" ' +
											'ProgramGUID="' + proGUID + '" ' +
											'SessionGUID="' + sessionGUID + '" ' +
											'PageSequenceOrder="' + pageSequenceOrder + '" ' +
											'PageGUID="' + pageGUID + '">' +
											'<PinCode  PinCode="' + pinCode + '"/>' +
											'</XMLModel>';
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL("submit"),
        type: 'POST',
        processData: false,
        data: pinCodeBackXML,
        dataType: "text",
        success: function (data) {
            var returnCode = data.substring(0, 1);
            var returnData = data.substr(2);

            if (returnCode == '1') {
                tipMessages = [];
                programRoomName = [];
                programVariables = [];
                specialStirngs = [];
                step = -1;
                stepHistory = [];
                proV = [];
                userUpdateTimeZone = "";
                userSMSToEmailValue = false;
                root = returnData;
                encodingRootXML();
                oraiRoot = root;
                introAnimation();
                sessionName = $(root).find("Session").attr("Name");
                // start added for retake by XMX on 2011/12/1
                retakeValue = $(root).attr("IsRetake").toLowerCase();
                if (retakeValue == "true" || retakeValue == "1") {
                    // getProgramVariable($(root));
                }
                var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
                if (isSMSToEmail == "1" || isSMSToEmail == "true") {
                    userSMSToEmailValue = true;
                } else {
                    userSMSToEmailValue = false;
                }

                /* for TimeZone */
                var userTimeZone = $(oraiRoot).attr("UserTimeZone");
                var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
                if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
                    userUpdateTimeZone = userTimeZone;
                } else {
                    userUpdateTimeZone = programTimeZone;
                }
                getTipMessages($(oraiRoot));
                getSepcialStrings($(oraiRoot));
                proArr($(oraiRoot));
                isFirstLoaded = true;
            } else if (returnCode == '0') {
                //showTipMessage('SendPinCodeFailed');
                showTipMessage('PinCodeWrong');
                //showNormalMessage(returnData);
            } else if (returnCode == "2") {
                if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                    var message = returnData;
                    var ctppHostStr = getURL('ctpp');
                    var ctppUrl = ctppHostStr + '?CTPP=' + message;
                    window.location.replace(ctppUrl);
                }
            }
        },
        error: function () {
            alert(' read  XML unsucessfully');
        }
    });
}
function processAccountCreation(isNeedSerialNum, pinCode, reginsInfo) {
    var emailVal = $('#bubble-yellow .row').eq(0).children('.formw').children('input').val();
    var password = $('#bubble-yellow .row').eq(1).children('.formw').children('input').val();
    var repeatPassword = $('#bubble-yellow .row').eq(2).children('.formw').children('input').val();
    var serialNum = '';
    var mobile = '';
    var timeZone = '';
    timeZone = $('#bubble-yellow .timeZone').find('.formw').find('select.accountCreationSelect').find("option:selected").val();
    if (pinCode != null && pinCode != undefined) {
        if (pinCode == "1") {
            mobile = $('#bubble-yellow .row').eq(3).children('.formw').children('input').val();
        }
    }
    if (reginsInfo == 6) {
        if (isNeedSerialNum != null && isNeedSerialNum != undefined) {
            if (isNeedSerialNum == "1") {
                serialNum = $('#bubble-yellow .row').eq(4).children('.formw').children('input').val();
            }
        }
    }
    if ($('#bubble-yellow').find("input[name=registerOrNot]:checkbox").attr('checked') == 'checked') {
        // checked Don't Register 
        processLive($(root));
    } else {
        // do not checked 'Don't Register'
        if (emailVal == '' || password == '' || repeatPassword == '' || (mobile == '' && pinCode != null && pinCode != undefined && pinCode == '1') || (serialNum == '' && reginsInfo == 6 && isNeedSerialNum != null && isNeedSerialNum != undefined && isNeedSerialNum == "1")) {
            showTipMessage('RegisterInfoRequired');
        } else if (password != repeatPassword) {
            showTipMessage('RegisterPasswordConfirm');
        } else if (checkSerialNumFormat(serialNum) == false && reginsInfo == 6) {
            showTipMessage('SerialNumberFormatError');
        } else if (checkEmailFormat(emailVal) == false) {
            showTipMessage('InvalidEmail');
            /*}else if(checkMobileFormat(mobile)==false && (pinCode!=null && pinCode!=undefined) && pinCode=='1'){
            showNormalMessage('Mobile is needed or Mobile Format is not correct');*/
        } else {
            var proGUID = $(root).attr('ProgramGUID');
            var userGUID = $(root).attr('UserGUID');
            var sessionGUID = $(root).find('Session').attr('GUID');
            var pageSequenceOrder = sequenceOrder;
            var pageGUID = pageObj.attr('GUID');
            var programVariableValue = pageObj.attr("ProgramVariable");
            var registerBackXml = '<XMLModel UserGUID="' + userGUID + '" ' +
											'ProgramGUID="' + proGUID + '" ' +
											'SessionGUID="' + sessionGUID + '" ' +
											'PageSequenceOrder="' + pageSequenceOrder + '" ' +
											'PageGUID="' + pageGUID + '">' +
											'<Register Email="' + emailVal + '" Password="' + password + '" Mobile="' + mobile + '" SerialNumber="' + serialNum + '" TimeZone="' + timeZone + '"/>' +
											'</XMLModel>';
            $.ajax({
                //url: getReturnDataServerUrl(),
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: registerBackXml,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var returnMainData = data.substr(2);

                    if (returnCode == '1') {
                        // root=returnMainData;
                        itemScore[pageGUID] = "1";
                        if (programVariableValue != null && programVariableValue != '' && programVariableValue != undefined) {
                            var provLength = proV.length;
                            for (var i = 0; i < provLength; i++) {
                                if (proV[i][0] == programVariableValue) {
                                    proV[i][2] = itemScore[pageGUID];
                                    break;
                                }
                            }
                        }
                        processLive($(root));
                    } else if (returnCode == '0') {
                        //showNormalMessage(returnMainData);
                        //showTipMessage('RegisterFailed');
                        var message = returnMainData;
                        showMessageForFaildRegister(message);
                    } else if (returnCode == "2") {
                        if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                            var message = returnMainData;
                            var ctppHostStr = getURL('ctpp');
                            var ctppUrl = ctppHostStr + '?CTPP=' + message;
                            window.location.replace(ctppUrl);
                        }
                    }
                },
                error: function () {
                    alert(' read  XML unsucessfully');
                }
            });
        }
    }
}
function processRelapse() {
    var len = $(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;
    if ($(oraiRoot).find("Session").length > 0 && $(oraiRoot).attr("IsHelpInCTPP") == undefined && $(oraiRoot).attr("IsReportInCTPP") == undefined) {
        if (pageOrder <= len) {
            sequenceOrder = '';
            processLive($(root));
        } else {
            root = oraiRoot;
            sessionName = $(root).find("Session").attr("Name");
            sequenceOrder = sequenceOrderBeforeGosub;
            pageOrder = pageOrderBeforeGosub;
            goToNextDirectlly();
            loadPage($(root));
        }
    } else {
        sequenceOrder = '';
        processLive($(root));
    }
}
function processPasswordRemind() {
    if (pageOrder == 2) {
        var email = $('#bubble-yellow input').eq(0).val().trim();
        if (email == "") {
            showTipMessage('PasswordRemiderInfoRequired');
        } else if (checkEmailFormat(email) == false) {
            showTipMessage('InvalidEmail');
        } else {
            var programGUID = $(root).attr('ProgramGUID');
            var userGUID = $(root).attr('UserGUID');
            var sessionGUID = $(root).find("Session").attr('GUID');
            var pagGUID = $(root).find('Page').filter(function () { return parseInt($(this).attr('Order')) == 2 }).attr('GUID');
            var passwordRemindXML = '<XMLModel UserGUID="' + userGUID + '" ' +
											  'ProgramGUID="' + programGUID + '" ' +
											  'SessionGUID="' + sessionGUID + '" ' +
											  'PageSequenceOrder="1" ' +
											  'PageGUID="' + pagGUID + '">' +
											  '<PasswordReminder Email="' + email + '"/>' +
											  '</XMLModel>';
            $.ajax({
                //url: getReturnDataServerUrl(),
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: passwordRemindXML,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var returnMainData = data.substr(2);
                    if (returnCode == "1") {
                        //root=returnMainData;
                        showNormalMessage(returnMainData);
                    } else {
                        showNormalMessage(returnMainData);
                    }

                },
                error: function () {
                    alert('Load return data error');
                }
            });
        }

    }
}
function processLogin() {
    if (pageOrder == 1) {
        var email = $('#bubble-yellow input').eq(0).val().trim();
        var password = $('#bubble-yellow input').eq(1).val().trim();
        if (email == '' || password == '') {
            showTipMessage('LoginInfoRequired');
        } else if (checkEmailFormat(email) == false) {
            showTipMessage('InvalidEmail');
        } else {
        var isFirstTimeLoadingPage = true;
        reloadedIntro(isFirstTimeLoadingPage);
        }
    }
}
function encodingRootXML() {
    /*var reg = new RegExp("&#xD;&#xD;", "g");
    root = root.replace(reg, "<br class='emptyline'>");
    var reg2 = new RegExp("&#xA;&#xA;", "g");
    root = root.replace(reg2, "<br class='emptyline'>");
    var reg3 = new RegExp("&#xD;&#xA;&#xD;&#xA;", "g");
    root = root.replace(reg3, "<br class='emptyline'>");
    var reg6 = new RegExp("&#xA;&#xD;&#xA;&#xD;", "g");
    root = root.replace(reg6, "<br class='emptyline'>");
    var reg4 = new RegExp("&#xD;&#xA;", "g");
    root = root.replace(reg4, "<br>");
    var reg5 = new RegExp("&#xA;&#xD;", "g");
    root = root.replace(reg5, "<br>");
    var reg1 = new RegExp("&#xD;|&#xA;", "g");
    root = root.replace(reg1, "<br>");*/
    /*var reg = new RegExp("&#xD;&#xD;", "g");
    root = root.replace(reg, "\n\n");
    var reg2 = new RegExp("&#xA;&#xA;", "g");
    root = root.replace(reg2, "\r\r");
    var reg3 = new RegExp("&#xD;&#xA;&#xD;&#xA;", "g");
    root = root.replace(reg3, "\n\r\n\r");
    var reg6 = new RegExp("&#xA;&#xD;&#xA;&#xD;", "g");
    root = root.replace(reg6, "\r\n\r\n");
    var reg4 = new RegExp("&#xD;&#xA;", "g");
    root = root.replace(reg4, "\r\n");
    var reg5 = new RegExp("&#xA;&#xD;", "g");
    root = root.replace(reg5, "\n\r");
    var reg1 = new RegExp("&#xD;", "g");
    root = root.replace(reg1, "\r");
    var reg6 = new RegExp("&#xA;", "g");
    root = root.replace(reg6, "\n");*/
}
function loadXmlAjax() {
    ///Load root xml file
    $.ajax({
        //url: getCurrentUrl(),
        url: getURL("data"),
        type: "POST",
        dataType: "text",
        success: function (data) {
            //show();
            var returnCode = data.substring(0, 1);
            root = data.substr(2);
            encodingRootXML();
            oraiRoot = root;
            if (returnCode == '1') {
                introAnimation();
                // added by XMX on 2012/04/20
                if ($(oraiRoot).find("Session").length == 0) {
                    sessionName = 'relapse';
                    relapseSequenceGUID = $(root).attr('PageSequenceGUID');
                    seqGuid = $(root).attr('PageSequenceGUID');
                    isHelpOrReportButton = true;
                    sequenceOrder = "";
                    var pageGuid = $(root).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("GUID");
                    var isBefore = true;
                    pageInfoGUIDBeforeGOSUBArray.push({ 'seqGuidBeforeGoSub': seqGuid, 'pageGuidBeforeGoSub': pageGuid, 'isBefore': isBefore });
                    // goSubStepArr.push({ 'seqGuidBeforeGoSub': seqGuid, 'pageGuidBeforeGoSub': pageGuid, 'isBefore': isBefore });
                }
                // end on 2012/04/20
                //preview situation, set sequence order and page order
                // added by XMX on 2012/01/17
                getURLParameter();
                // end on 2012/01/17
                if (para['Mode'] == 'Preview') {
                    if (para['Object'] == 'Page') {
                        sequenceOrder = parseInt($(root).find('PageSequence').attr('Order'));
                        pageOrder = parseInt($(root).find('Page').attr('Order'));
                    } else if (para['Object'] == 'PageSequence') {
                        sequenceOrder = parseInt($(root).find('PageSequence').attr('Order'));
                        pageOrder = 1;
                    } else {
                        sequenceOrder = 1;
                        pageOrder = 1;
                    }
                    isPreviewMode = true;
                }
                // start added for retake by XMX on 2011/12/1
                retakeValue = $(root).attr("IsRetake").toLowerCase();
                if (retakeValue == "true" || retakeValue == "1") {
                    // getProgramVariable($(root));
                }
                /* for SMS To Email */
                var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
                if (isSMSToEmail == "1" || isSMSToEmail == "true") {
                    userSMSToEmailValue = true;
                } else {
                    userSMSToEmailValue = false;
                }
                /* for TimeZone */
                var userTimeZone = $(oraiRoot).attr("UserTimeZone");
                var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
                if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
                    userUpdateTimeZone = userTimeZone;
                } else {
                    userUpdateTimeZone = programTimeZone;
                }
                //getProgramVariable($(oraiRoot));
                // end added for retake by XMX on 2011/12/1

                getTipMessages($(oraiRoot));
                getSepcialStrings($(oraiRoot));
                //first load, unless it's preview, the sequence order and page order should all be 1 since it's the first load;
                //intlize top bar(should never be changed)
                //showTopBar();
                //intlize variable Array
                proArr($(root));
                //intlize page elements
                backgroundImageLoaded();

                //setTimeout(hiddenRoomInfo,2000);

                var reginsInfo = $('#bubble-yellow .row').length;
                showDialog();
                isFirstLoaded = true;
                /* $("#bubble-blue").bind("click", function () {
                $("#bubble-blue").unbind("click");
                if (isLoadingNextPage) {
                return;
                } else {
                isLoadingNextPage = true;
                setTimeout(function () {
                $("#bubble-blue").bind("click", function () {
                bubbleBlueNextLoadPage(reginsInfo);
                });
                }, 100);
                }
                });*/
                //   var clickEventList = new Array();
                // var lastClickTime = new Date().getTime();
                $("#bubble-blue").bind("click", function () {
                    $(".mask").css("display", "block");
                    bubbleBlueNextLoadPage(reginsInfo);
                });
                /*  $("#bubble-blue").bind("click", function () {
                clickEventList.push(1);
                setTimeout(function () {
                if (clickEventList.length > 0) {
                clickEventList = [];
                bubbleBlueNextLoadPage(reginsInfo);
                }
                }, 1000);

                });*/
            } else if (returnCode == '2') {
                // comment on 2012/10/09
                // if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                if (myplatform != undefined && myplatform != null && myplatform == "Win8") {
                    window.external.notify("SessionClose");
                } else {
                    var message = root;
                    var ctppHostStr = getURL('ctpp');
                    var ctppUrl = ctppHostStr + '?CTPP=' + message;
                    window.location.replace(ctppUrl);
                }
                //  }
            } else {
                //if returnCode=0 ,the code will be start here 
                var message = root;
                setOrangeBubble('FadeOut', function () {
                    showNormalMessage(message);
                });
            }
        },
        error: function () {
            alert(' read root XML unsucessfully')
        }
    });
}
function goToNextDirectlly() {
    if (sessionName == "relapse" && (sequenceOrder == '' || sequenceOrder == 'undefined' || isNaN(sequenceOrder))) {
        var pageLength = $(oraiRoot).find('Relapse').find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;

        if (pageOrder < pageLength) {
            pageOrder = parseInt(pageOrder) + 1;
        } else if (pageOrder == pageLength) {
            var goSubStepArrLength = goSubStepArr.length;
            var currentPageInfo = goSubStepArr.pop();
            seqGuid = currentPageInfo.seqGuidBeforeGoSub;
            pageGuid = currentPageInfo.pageGuidBeforeGoSub;
            isBefore = currentPageInfo.isBefore;
            pageOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("GUID") == pageGuid }).attr("Order"));
            sequenceOrder = parseInt($(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).attr('Order'));
            var pageLengthBeforeGoSub = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;
            if (pageOrder == pageLengthBeforeGoSub) {
                if (isBefore) {
                    excuteBefore = true;
                    if (!isNaN(sequenceOrder)) {
                        sequenceOrder += 1;
                        pageOrder = 1;
                    } else {
                        sequenceOrder = '';
                        // pageOrder += 1;
                       // pageOrder += 1;
                        var goSubStepArrLength = goSubStepArr.length;
                        if (goSubStepArrLength > 0) {
                            var currentPageInfo = goSubStepArr.pop();
                            seqGuid = currentPageInfo.seqGuidBeforeGoSub;
                            pageGuid = currentPageInfo.pageGuidBeforeGoSub;
                            isBefore = currentPageInfo.isBefore;
                            pageOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("GUID") == pageGuid }).attr("Order"));
                            sequenceOrder = parseInt($(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).attr('Order'));
                            pageOrder += 1;
                        }
                    }
                } else {
                    // if (pageInfoGUIDBeforeGOSUBArrayLength > 0) {
                    var goSubStepArrLength = goSubStepArr.length;
                    if (goSubStepArrLength > 0) {
                        goToNextDirectlly();
                    } else {
                        if ($(oraiRoot).find("Session").length == 0) {
                            quesFlag = false;
                            sessionEndFlag = true;
                            directllyGo = false;
                            relapseMsgEnd();
                            return false;
                        } else {
                        console.log("sessioinEnding1");
                            sessionEnding();
                        }
                    }
                }  //end of if(isBefore)
            } else {
                if (isBefore) {
                    excuteBefore = true;
                    pageOrder += 1;
                } else {
                    excuteBefore = true;
                    pageOrder += 1;
                } // end of if(isBefore)
            } //end of  if (pageOrder == pageLengthBeforeGoSub)
          /*  var pageInfoGUIDBeforeGOSUBArrayLength = pageInfoGUIDBeforeGOSUBArray.length;
            if (pageInfoGUIDBeforeGOSUBArrayLength >= goSubReturnTime) {
                seqGuid = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].seqGuidBeforeGoSub;
                pageGuid = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].pageGuidBeforeGoSub;
                var isBefore = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].isBefore;
                goSubReturnTime += 1;
                pageOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("GUID") == pageGuid }).attr("Order"));
                sequenceOrder = parseInt($(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).attr('Order'));
                var pageLengthBeforeGoSub = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;
                if (pageOrder == pageLengthBeforeGoSub) {
                    if (isBefore) {
                        excuteBefore = true;
                        if (!isNaN(sequenceOrder)) {
                            sequenceOrder += 1;
                            pageOrder = 1;
                        } else {
                        sequenceOrder = '';
                            pageOrder += 1;
                        }
                    } else {
                        if (pageInfoGUIDBeforeGOSUBArrayLength > 0) {
                            goToNextDirectlly();
                        } else {
                            if ($(oraiRoot).find("Session").length == 0) {
                                quesFlag = false;
                                sessionEndFlag = true;
                                directllyGo = false;
                                relapseMsgEnd();
                                return false;
                            } else {
                                sessionEnding();
                            }
                        }
                    }
                } else {
                    if (isBefore) {
                        excuteBefore = true;
                        pageOrder += 1;
                    } else {
                        excuteBefore = true;
                        pageOrder += 1;
                    }
                }
            } else {
                if ($(oraiRoot).find("Session").length == 0) {
                    quesFlag = false;
                    sessionEndFlag = true;
                    directllyGo = false;
                    relapseMsgEnd();
                    return false;
                } else {
                    sessionEnding();
                }
            }*/
        }
        if (!isCatchUpDay) {
            if ($(oraiRoot).attr('IsCTPPEnable')) {
                if ($(oraiRoot).attr('IsCTPPEnable').toLowerCase().trim() == '1' || $(oraiRoot).attr('IsCTPPEnable').toLowerCase().trim() == 'true') {
                    if ($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression") && $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression") != '') {
                        var afterExpression = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression").toLowerCase().trim();
                        if (afterExpression == "endpage") {

                            if ($(oraiRoot).find("Session").length == 0) {
                                quesFlag = false;
                                sessionEndFlag = true;
                                directllyGo = false;
                                relapseMsgEnd();
                                return false;
                            } else {
                            /*console.log("sessionEnding2");*/
                                sessionEnding();
                            }
                        }
                    }
                }
            }
        }

    } else {
        excuteBefore = true;
       /* var pageInfoGUIDBeforeGOSUBArrayLength = pageInfoGUIDBeforeGOSUBArray.length;
        if (pageInfoGUIDBeforeGOSUBArrayLength >= goSubReturnTime) {
            seqGuid = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].seqGuidBeforeGoSub;
            pageGuid = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].pageGuidBeforeGoSub;
            var isBefore = pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - goSubReturnTime].isBefore;
            goSubReturnTime += 1;
            pageOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("GUID") == pageGuid }).attr("Order"));
            sequenceOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).attr('Order'));
        }*/
        var goSubStepArrLength = goSubStepArr.length;
        if (goSubStepArrLength > 0) {
            var currentPageInfo = goSubStepArr.pop();
            seqGuid = currentPageInfo.seqGuidBeforeGoSub;
            pageGuid = currentPageInfo.pageGuidBeforeGoSub;
            var isBefore = currentPageInfo.isBefore;
            pageOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("GUID") == pageGuid }).attr("Order"));
            sequenceOrder = parseInt($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).attr('Order'));
        }
        var pageLength = $(root).find("Session").find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').length;
        var pageSequenceLength = $(root).find("Session").find('PageSequence').length;
        if (pageOrder < pageLength) {
            pageOrder = parseInt(pageOrder) + 1;
        } else if (sequenceOrder < pageSequenceLength) {
            pageOrder = 1;
            sequenceOrder = parseInt(sequenceOrder) + 1;
        } else {
        /*console.log("sessionEnding3");*/
            sessionEnding();
        }
    }
}
/*function goToNextDirectlly() {
var pageLength = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } ).find( 'Page' ).length;
var pageSequenceLength = $( root ).find( 'PageSequence' ).length;
if ( pageOrder < pageLength ) {
pageOrder = parseInt( pageOrder ) + 1;
} else if ( sequenceOrder < pageSequenceLength ) {
pageOrder = 1;
sequenceOrder = parseInt( sequenceOrder ) + 1;
} else {
sessionEnding();
}
}*/
function sendAnswerToServer(XMLobj) {
    quesFlag = true;
    var isRequiredArray = [];
    var userGUID = $(XMLobj).attr('UserGUID');
    var programGUID = $(XMLobj).attr('ProgramGUID');
    var sessionGUID = $(XMLobj).find('Session').attr('GUID');
    //var pageGUID = $( XMLobj ).find( "Page" ).attr( 'GUID' );
    var pageGUID = $(pageObj).attr('GUID');
    var pageSequenceGUID = $(XMLobj).find("PageSequence").filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).attr("GUID");
    var relapsePageSequenceGUID = "";
    var relapsePageGUID = "";
    var pageSequenceOrder = sequenceOrder;

    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');
        // pageGUID = $(XMLobj).find("Page").attr('GUID');
        // pageSequenceGUID = $(oraiRoot).find("Relapse").find("PageSequence").attr("GUID");
        // change by XMX on 2012/05/23
        if ($(oraiRoot).find("Session").length > 0) {
            // pageSequenceGUID = $( oraiRoot ).find( "Session" ).find( "PageSequence" ).filter( function () { return $( this ).attr( "Order" ) == sequenceOrderBeforeGosub } ).attr( "GUID" );
            // pageGUID = $( oraiRoot ).find( "Session" ).find( "PageSequence" ).filter( function () { return $( this ).attr( "Order" ) == sequenceOrderBeforeGosub } ).find( "Page" ).filter( function () { return $( this ).attr( "Order" ) } == pageOrderBeforeGosub ).attr( "GUID" );
           /* pageSequenceGUID = pageInfoGUIDBeforeGOSUBArray[0].seqGuidBeforeGoSub;
            pageGUID = pageInfoGUIDBeforeGOSUBArray[0].pageGuidBeforeGoSub;*/
            var goSubStepArrLength=goSubStepArr.length;
            if (goSubStepArrLength > 0) {
                pageSequenceGUID = goSubStepArr[0].seqGuidBeforeGoSub;
                pageGUID = goSubStepArr[0].pageGuidBeforeGoSub;
            }
        } else {
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
        }
        // change by XMX on 2012/05/23
       var order = parseInt($(XMLobj).find("PageSequence").filter(function () { return $(this).attr('GUID') == pageSequenceGUID }).attr("Order"));
        if (typeof order == 'number') {
            pageSequenceOrder = order;
        } else {
            pageSequenceOrder = '';
        }
       // pageSequenceOrder = "";
        relapsePageSequenceGUID = relapseSequenceGUID;
        // relapsePageGUID = $( XMLobj ).find( "Page" ).attr( 'GUID' );
        relapsePageGUID = $(pageObj).attr("GUID"); // added by XMX on 2012/05/23
        if ($(oraiRoot).find("Session").length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            relapsePageSequenceGUID = seqGuid;
        } else {
            sessionGUID = '';
        }
    }
    var answerFeedbackStr = "";
    var questionsObj = $(pageObj).find("Questions");
    if (questionsObj != undefined && questionsObj != null && questionsObj != '') {
        var quesObj = $(questionsObj).find("Question");
        if (quesObj != null && quesObj != 'undefined' && quesObj != '') {
            $(quesObj).each(function (quesIndex, ques) {
                if (quesFlag) {
                    var quesGUID = $(ques).attr("GUID");
                    isRequired[quesGUID] = $(ques).attr('IsRequired');
                    var quesGUID = $(ques).attr('GUID');
                    var quesType = $(ques).attr('Type');
                    if (itemScore[quesGUID] != undefined && itemScore[quesGUID] != null && itemScore[quesGUID] != '') {
                        var programVariableValue = $(ques).attr("ProgramVariable");
                        if (programVariableValue != null && programVariableValue != '' && programVariableValue != undefined) {
                            var provLength = proV.length;
                            for (var i = 0; i < provLength; i++) {
                                if (proV[i][0] == programVariableValue) {

                                    proV[i][2] = itemScore[quesGUID];
                                    break;

                                }
                            }
                        }
                    }
                    if (GUIDValue[quesGUID] != '' && GUIDValue[quesGUID] != undefined && GUIDValue[quesGUID] != '') {
                        if ($(ques).attr("Type") == "CheckBox") {
                            GUIDValue[quesGUID] = GUIDValue[quesGUID].substring(0, GUIDValue[quesGUID].length - 1);
                        }
                        answerFeedbackStr += '<Feedback GUID="' + quesGUID + '" Value="' + GUIDValue[quesGUID] + '"/>';
                    }
                    var index = parseInt(sequenceOrder) + '.' + parseInt(pageOrder);

                    historyAnswers[quesGUID] = GUIDValue[quesGUID];
                    if (isRequired[quesGUID] == 1) {
                        if (itemScore[quesGUID] == undefined) {
                            quesFlag = false;
                        }
                    }
                }

            }); //quesObj each
        } //quesObj end
    } //questionsObj end
    if (quesFlag) {
        var answerBackXml = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageSequenceGUID="' + pageSequenceGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageGUID="' + pageGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '">' +
													'<Feedbacks>';
        answerBackXml += answerFeedbackStr + '</Feedbacks></XMLModel>';
        $.ajax({
            //url: getReturnDataServerUrl(),
            url: getURL("submit"),
            type: 'POST',
            processData: false,
            data: answerBackXml,
            dataType: "text",
            success: function (data) {
                var returnCode = data.substring(0, 1);
                var message = data.substr(2);
                if (returnCode == '1') {
                    //root=message;
                    //loadPage($(root));
                } else {
                    // root=message;
                    // showNormalMessage(message);
                }

            },
            error: function () {
                // alert( 'load XML file error' );
            }
        });
    }
}
//process live
function processLive(XMLobj) {
    if (sequenceOrder != undefined && sequenceOrder != '') {
        pageObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
    } else {
        //directllyGo = false;
        pageObj = $(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
    }

    if ((pageObj.attr("Type") == "Get information") && ($("#bubble-yellow").css("display") != "none")) {
        //if(retakeValue != "true" && retakeValue != "1") added for retake by XMX on 2011/12/1
        if (retakeValue != "true" && retakeValue != "1") {
            sendAnswerToServer($(root));
        } //end if(retakeValue != "true" && retakeValue != "1") added for retake by XMX on 2011/12/1
    }
    if ((pageObj.attr("Type") == "Timer") && ($("#bubble-yellow").css("display") != "none")) {
        getStopWatchData($(root));
    }
    if ((pageObj.attr("Type") == "Choose preferences") && ($("#bubble-yellow").css("display") != "none")) {
        if (retakeValue != "true" && retakeValue != "1") {
            getChoosePreferencesData($(root));
        }
    }
    if (quesFlag) {
        //has after Experssion situation
        if (pageObj.attr('AfterExpression') && pageObj.attr('AfterExpression') != '') {

            var ae = pageObj.attr('AfterExpression');
            var isBefore = false;
            recur(ae, $(root), isBefore);
        }
       
        if (directllyGo == true) {
            goToNextDirectlly();
        }
        if (isPreviewMode) {
            if (sequenceOrder != undefined && sequenceOrder != '' && !isNaN(sequenceOrder)) {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
            } else {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                //console.log( 'currentSequeneGUID:' + seqGuid );
            }
            if (nextPageObj.attr('AfterExpression')) {
                if (nextPageObj.attr('AfterExpression').toLowerCase() == "endpage") {
                    $("#bubble-blue").unbind("click");
                    $("#bubble-blue").css("cursor", "default");
                    isBackFromPreviewEndPage = true;
                    return;
                }
            }
        }
        //check the 'next' page's afterExpression and beforeExpression

        function checkAandBExpression() {
            afterisEnd = false;
            //alert('the page we will go to is: '+ sequenceOrder + '.' + pageOrder+'. Now we check the after and before expression of that page.');
            // changed by XMX on 2012/04/20
            /* var nextPageObj = $( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } )
            .find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } );*/
            if (sequenceOrder != undefined && sequenceOrder != '' && !isNaN(sequenceOrder)) {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
            } else {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                //console.log( 'currentSequeneGUID:' + seqGuid );
            }
           
            // end changed by xmx on 2012/04/20
            if ($(root).attr('IsCTPPEnable')) {
                if ($(root).attr('IsCTPPEnable').toLowerCase().trim() == '1' || $(root).attr('IsCTPPEnable').toLowerCase().trim() == 'true') {
                    if (isCatchUpDay && ($(oraiRoot).find("Session").length > 0 || ($(oraiRoot).attr('IsHelpInCTPP') != 'true' && $(oraiRoot).attr('IsReportInCTPP') != 'true'))) {
                        if (pageObj.attr('AfterExpression') != undefined) {
                            if (pageObj.attr('AfterExpression').toLowerCase().trim() == 'endpage') {
                                if ($(root).attr('IsRetake').trim() == 'true' && nextPageObj.attr('Type').trim() == 'Account creation') {
                                    sequenceOrder = parseInt(nextPageObj.parent().attr('Order'));
                                    pageOrder = parseInt(nextPageObj.attr('Order'));
                                    goToNextDirectlly();
                                } else {
                                /*COMMENT by XMX on 2013/02/19*/
                                    /*afterisEnd = true;
                                    console.log("sessionEnding4");
                                    sessionEnding();*/
                                }
                            }
                        }
                    } else {
                        if (nextPageObj.attr('AfterExpression') != undefined) {
                            if (nextPageObj.attr('AfterExpression').toLowerCase().trim() == 'endpage') {
                                //alert('the next page after expression is: END PAGE, so session end');
                                if ($(root).attr('IsRetake').trim() == 'true' && nextPageObj.attr('Type').trim() == 'Account creation') {
                                    sequenceOrder = parseInt(nextPageObj.parent().attr('Order'));
                                    pageOrder = parseInt(nextPageObj.attr('Order'));
                                    goToNextDirectlly();
                                } else {
                                    afterisEnd = true;
                                    if ($(oraiRoot).find("Session").length > 0 || ($(oraiRoot).attr('IsHelpInCTPP') != 'true' && $(oraiRoot).attr('IsReportInCTPP') != 'true')) {
                                        /*console.log("sessionEnding5");*/
                                        sessionEnding();
                                    } else {
                                        quesFlag = false;
                                        sessionEndFlag = true;
                                        directllyGo = false;
                                        relapseMsgEnd();
                                        return false;
                                    }

                                }
                            }
                        } // end if of nextPageObj.attr( 'AfterExpression' ) != undefined
                    }
                } else {
                    if (isCatchUpDay && ($(oraiRoot).find("Session").length > 0 || ($(oraiRoot).attr('IsHelpInCTPP') != 'true' && $(oraiRoot).attr('IsReportInCTPP') != 'true'))) {
                        if (pageObj.attr('AfterExpression') != undefined) {
                            if (pageObj.attr('AfterExpression').toLowerCase().trim() == 'endpage') {
                                if ($(root).attr('IsRetake').trim() == 'true' && nextPageObj.attr('Type').trim() == 'Account creation') {
                                    sequenceOrder = parseInt(nextPageObj.parent().attr('Order'));
                                    pageOrder = parseInt(nextPageObj.attr('Order'));
                                    goToNextDirectlly();
                                } else {
                                /*comment by xmx on 2013/02/19*/
                                    /*afterisEnd = true;
                                    console.log("sessionEnding6");
                                    sessionEnding();*/
                                }
                            }
                        }
                    }
                }
            }


            if (afterisEnd == false) {
                if (excuteBefore) {
                    if (nextPageObj.attr('BeforeExpression') && nextPageObj.attr('BeforeExpression') != '') {
                        //alert('the before expression is: ' + nextPageObj.attr('BeforeExpression'));
                        var be = nextPageObj.attr('BeforeExpression');
                        //reset the directllyGo as true;
                        directllyGo = true;
                        var isBefore = true;
                        pageGUIDBeforeExpression = nextPageObj.attr("GUID");
                        recur(be, $(root), isBefore);
                        pageGUIDAfterExpression = $(root).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("GUID");
                        if (pageGUIDBeforeExpression == pageGUIDAfterExpression) {
                            isSamePage = true;
                        } else {
                        isSamePage = false;
                        }
                    //alert(directllyGo);
                        if (isSamePage) {
                        } else {
                            if (directllyGo == true) {
                                //means did not go to any page, do nothing
                            } else {
                                //means the before expression lead us to a new page, so check this page again(inclue both after expression and before expression)
                                checkAandBExpression();
                            }
                        }

                    } else {
                        // if nextPageObj.attr("BeforeExpression") == "" || nextPageObj.attr("BeforeExpression") == undefined

                    }
                }
            }

        }
        if (!isSamePage) {
            checkAandBExpression();
        }
        // change by XMX on 2012/04/20
        /*if ( sequenceOrder == '' || sequenceOrder == undefined ) {
        pageOrder += 1;
        }*/
        // end on 2012/04/20

        //alert('After parsering, the final page we will go is:' + sequenceOrder + '.' + pageOrder+'.');
        //if the final sequence order is not equal to the old one, send sequence ending MSG to backend;
        if ($(oraiRoot).find('Session').length > 0) {
            if (sequenceOrder != sequenceOrderBeforeParse) {
                if (!isFirstLoaded) {
                    msgSequenceEnd();
                }
            }
        } else {
            //var pageLength = $( oraiRoot ).find( 'Relapse' ).find( 'PageSequence' ).filter( function () { return $( this ).attr( 'GUID' ) == seqGuid } ).find( 'Page' ).length;
            /* if ( pageOrder == pageLength ) {
            msgSequenceEnd();
            }*/
            //console.log( "seqGuid:" + seqGuid );
            //console.log( "sequenceGUIDBeforeGOSUB:" + sequenceGUIDBeforeGOSUB );
            if (seqGuid != sequenceGUIDBeforeGOSUB) {
                msgSequenceEnd();
            }
        }
        //GUIDValue=[];
        //itemScore=[];

        if (sessionEndFlag == false) {
            getSequenceGUID($(root));
            if (seqGuidBeforeParse == '') {
                if ($(oraiRoot).find('Session').length == 0) {
                    loadPage($(root));
                }
            } else if (seqGuidBeforeParse != seqGuid) {
                getProRoomNameBySeqGUID();
                var programRoomNameBeforeParse = programRoomName[seqGuidBeforeParse];
                var ProgramRoomNameCurrent = programRoomName[seqGuid];
                if ((programRoomNameBeforeParse != undefined && programRoomNameBeforeParse != null && programRoomNameBeforeParse != '') || (ProgramRoomNameCurrent != undefined && ProgramRoomNameCurrent != null && ProgramRoomNameCurrent != '')) {
                    if (programRoomNameBeforeParse !== ProgramRoomNameCurrent) {
                        roomTransitionAnimation();
                        //setTimeout(hiddenRoomInfo,2000);
                    } else {
                        loadPage($(root));
                    }
                } else {
                    loadPage($(root));

                }

            } else {
                loadPage($(root));
            }
            /// ADDED BY XMX for save answer when historyAnswers[quesGUID]!=''
            var pageType = pageObj.attr("Type");
            if (pageType == "Get information") {
                saveAnswers();
            }
            if (pageType == "Choose preferences") {
                var pageGUID = pageObj.attr("GUID");
                var answer = historyAnswers[pageGUID];
                if (answer != null && answer != undefined && answer != '') {
                    saveAnswersForChoosePreferences(answer);
                }
            }
            /// ENDED BY XMX
        }
    }
    else {
        if (pageObj.attr("Type") == "Get information") {
            showTipMessage("QuestionRequired");
        }
    }
}

//program array
function proArr(XMLobj) {
    if ((XMLobj).find('GeneralVariables').find('Variable')) {
        for (var i = 0; i < (XMLobj).find('GeneralVariables').find('Variable').length; i++) {
            proV[i] = [];
            if ((XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Name')!= null && (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Name')!= undefined && (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Name') != "") {
                proV[i][0] = (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Name').trim(); //name
            } else {
            if (isPreviewMode) {
                proV[i][0] = (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Gender').trim(); //name
            }
            }
            proV[i][1] = (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Type');
            proV[i][2] = (XMLobj).find('GeneralVariables').find('Variable').eq(i).attr('Value'); //value;
            if (proV[i][1] != 'String' && proV[i][2] == '') {
                proV[i][2] = 0;
            }
        }
    }
    var tempLength = proV.length;
    if ((XMLobj).find('ProgramVariables').find('Variable')) {
        for (var i = tempLength; i < tempLength + (XMLobj).find('ProgramVariables').find('Variable').length; i++) {
            proV[i] = [];
            proV[i][0] = (XMLobj).find('ProgramVariables').find('Variable').eq(i - tempLength).attr('Name').trim(); //name
            proV[i][1] = (XMLobj).find('ProgramVariables').find('Variable').eq(i - tempLength).attr('Type');
            proV[i][2] = (XMLobj).find('ProgramVariables').find('Variable').eq(i - tempLength).attr('Value'); //value;
            if (proV[i][1] != 'String' && proV[i][2] == '') {
                proV[i][2] = 0;
            }
        };
    }


    //set non-string empty value to 0;
    /* for (var i = 0; i < proV.length; i++) {
    if (proV[i][1] != 'String' && proV[i][2] == '') {
    proV[i][2] = 0;
    }
    }*/



}

//recursion to parse the whole expersion
var ifTrue = false;
//define all the orders
var orders = [];
orders[0] = 'GOTO';
orders[1] = 'SET';
orders[2] = 'GOTO NextPageSequence';
orders[3] = 'GOTO END';
orders[4] = 'GOWEB';
orders[5] = 'GOSUB';

function recur(str, XMLobj, isBefore) {

    //define all IF sentence, include the followed order(s)
    var ifSentence = [];
    var ifCount = -1;

    //define ELSE sentence
    var elseSentence = [];

    //define on if sentence;
    var noIfSentence = [];

    //divide the expression by IF
    if (str.match(/^IF+/) != null) {

        //else part
        if (str.match(/ELSE/) != null) {

            elseSentence[0] = str.split('ELSE')[1].trim();
            str = str.split('ELSE')[0].trim();
        }
        //here the str has one or more 'IF'
        function divideIf() {
            if (str.match(/^IF.+IF.+/)) {
                //if has more 'IF'

                ifCount += 1;
                var temp = str.match(/^IF.+?(?=IF)/).toString(); //(?=pattern)
                str = str.split(temp)[1];
                ifSentence[ifCount] = [];
                ifSentence[ifCount][0] = temp;

                divideIf();

            } else {
                //only has one 'IF'
                ifCount += 1;
                ifSentence[ifCount] = [];
                ifSentence[ifCount][0] = str;

            }
        }
        divideIf();

        for (var i = 0; i < ifSentence.length; i++) {
            for (var j = 0; j < orders.length; j++) {
                var orderCount = ifSentence[i].length - 1;
                function getIf() {
                    if (ifSentence[i][0].lastIndexOf(orders[j]) != -1) {

                        orderCount += 1;

                        var tempIndex = ifSentence[i][0].lastIndexOf(orders[j]);
                        ifSentence[i][orderCount] = ifSentence[i][0].slice(tempIndex, ifSentence[i][0].length);

                        ifSentence[i][0] = ifSentence[i][0].slice(0, tempIndex).trim();

                        getIf();
                    }
                }
                getIf();
                //break; // added by XMX on 2012/04/18
            }
        }


        //get all orders

        for (var i = 0; i < ifSentence.length; i++) {
            divideOrder(ifSentence[i]);

        }

        perform(ifSentence, isBefore);

        if (ifTrue == false) {
            if (elseSentence.length > 0) {
                divideOrder1(elseSentence);
                perform(elseSentence, isBefore);
            }
        }
    } else {
        //consider the situation of has no 'IF'
        //intlize noIfOrder to zero!!!!!!
        noIfOrder = 0;
        noIfSentence[0] = str.trim();
        divideOrder1(noIfSentence);
        perform(noIfSentence, isBefore);
    }

}

function divideOrder(arr) {
    for (var j = 1; j < arr.length; j++) {
        for (var k = 0; k < orders.length; k++) {
            var orderCount1 = arr.length - 1;
            if (arr[j].lastIndexOf(orders[k]) != -1 && arr[j].lastIndexOf(orders[k]) != 0) {
                var tempIndex1 = arr[j].lastIndexOf(orders[k]);
                orderCount1 += 1;
                arr[orderCount1] = arr[j].slice(tempIndex1, arr[j].length).trim();
                arr[j] = arr[j].slice(0, tempIndex1).trim();
                divideOrder(arr);
            }
            //break; // added by XMX on 2012/04/18
        }
    }

}
var noIfOrder = 0;
function divideOrder1(arr) {
    for (var i = 0; i < arr.length; i++) {
        for (var j = 0; j < orders.length; j++) {
            if (arr[i].lastIndexOf(orders[j]) != 0 && arr[i].lastIndexOf(orders[j]) != -1) {
                var tempIndex2 = arr[i].lastIndexOf(orders[j]);
                noIfOrder += 1;
                arr[noIfOrder] = arr[i].slice(tempIndex2, arr[i].length).trim();
                arr[i] = arr[i].slice(0, tempIndex2).trim();
                divideOrder1(arr);
            }
        }
    }
}

//{{{special operators start, we need handle these operators first, becuse the JS EVAL function can't handle these.------------------------------
//GetIndex--------------------------------
function parserGetIndex(str) {
    var temp = str.split('GetIndex(')[1].trim();
    var index = 0;
    var getIndexArr = [];
    var getIndexArrCopy = [];
    var indexCount = -1;
    function processGetIndex() {
        if (temp.indexOf(',') != -1) {
            indexCount += 1;
            getIndexArr[indexCount] = temp.slice(0, temp.indexOf(','));
            temp = temp.slice(temp.indexOf(',') + 1).trim();
            processGetIndex();
        }
    }
    processGetIndex();
    index = getIndexArr[getIndexArr.length - 1];
    getIndexArr.pop();
    //caclulate
    for (var i = 0; i < getIndexArr.length; i++) {
        var temp = getIndexArr[i];
        getIndexArr[i] = getIndexArr[i].replace(/\{V:/g, '').replace(/\}/g, '');
        for (var j = 0; j < proV.length; j++) {
            if (getIndexArr[i].indexOf(proV[j][0]) != -1) {
                var reg = eval('/' + proV[j][0] + '/g');
                getIndexArr[i] = getIndexArr[i].replace(reg, proV[j][2]);
            }
        }
        try { eval(getIndexArr[i]); }
        catch (err) { alert('There are some problems with the expression:\n' + expressionBeforeParsing + '\nThe problem can be caused by the fllow reasons:\n1> Variable does not exist.\n2> Variable is not be given a value.\nplease check your varilables setting!'); sequenceOrder = sequenceOrderBeforeParse; pageOrder = pageOrderBeforeparse; };
        getIndexArr[i] = eval(getIndexArr[i]);
        getIndexArrCopy[i] = getIndexArr[i];
    }
    //sort
    for (var i = 0; i < getIndexArr.length - 1; i++) {
        for (var j = 0; j < getIndexArr.length - 1 - i; j++) {
            if (getIndexArr[j] < getIndexArr[j + 1]) {
                var temp1 = getIndexArr[j];
                getIndexArr[j] = getIndexArr[j + 1];
                getIndexArr[j + 1] = temp1;
            }
        }
    }

    if (temp.match(/\d+\.*\d+/) != null) {
        extra = parseFloat(temp.match(/\d+\.*\d+/).toString());
    }
    //get the value
    var mark = 0;
    if (temp.match(/^max/) != null) {
        mark = getIndexArr[index - 1];
    } else {
        mark = getIndexArr[getIndexArr.length - index];
    }

    for (var i = 0; i < getIndexArrCopy.length; i++) {
        if (getIndexArrCopy[i] == mark) {
            result = i + 1;
        }
    }

}

function ineteadVar(str) {
    //added by XMX for fix the bug SET {V:Ekte}=vate ekte on 2012/04/17
    // var regPattern = /\{(V:){1}[A-Za-z0-9_ÅåÆæØø ]+\}/;
    var regPattern = /\{(V:){1}[A-Za-z0-9_ ]+\}/;
    var isIncludeVariable = regPattern.test(str);
    if (!isIncludeVariable) {
        str = str;
    } else {
        for (var j = 0; j < proV.length; j++) {
            var reg = eval('/' + proV[j][0] + '\\W(?!\\w)/i');
            var reg1 = eval('/' + proV[j][0] + '(?!\\w)/i')
            if (str.match(reg)) {
                str = str.replace(reg1, proV[j][2]);
            }
            var reg2 = eval('/' + proV[j][0] + '$/i');
            if (str.match(reg2)) {
                str = str.replace(reg2, proV[j][2]);
            }
            var reg3 = eval('/' + proV[j][0] + '\\)$/i');
            if (str.match(reg3)) {
                str = str.replace(proV[j][0], proV[j][2]);
            }
        }
    }
    return str;
}

function replaceVariableNameWithVariableValue(expression) {
    //console.log( 'expressionBeforeExcuteReplaceVariableNameWithVariableValue:' + expression );
    var variableList = getVariableListInExpression(expression);
    // console.log( 'variableListInReplaceVariableNameWithVariableValue:' + variableList );
    if (variableList != undefined && variableList != null) {
        var variableListLength = variableList.length;

        for (var i = 0; i < variableListLength; i++) {
            var variableName = variableList[i].split(':')[1].substring(0, variableList[i].split(':')[1].length - 1);
            //  console.log( 'variableName:' + variableName );
            var variableValue = getProgramVariableValue(variableName);
            // console.log( 'variableValue:' + variableValue );
            expression = expression.replace(variableList[i], variableValue);
        }

    } else {
        expression = expression;
    }
    return expression;
}

//special operators END----------------------------------------------------------------------}}}
//var
var extra = 0;
var result = 0;
var expressionBeforeParsing;
//excute when 'IF' is ture or no 'IF', the final step,every keywords go here.
function excuteArr(arr, isBefore) {

    $(arr).each(function (i) {
        //SET

        if (this.indexOf('SET') == 0) {
            expressionBeforeParsing = this;
            // such as Expression is SET {V:test} == {V:test1} + {V:test2} + {V:test3}
            var temp1 = this.split('SET')[1].split('=')[0].trim().replace(/\{V:/g, '').replace(/\}/g, ''); // get the variableName at the left of ==, such as 'test' in the example SET {V:test} == {V:test1} + {V:test2} + {V:test3}
            //console.log( 'temp1InExcuteArrFunction:' + temp1 );
            var count = -1;
            for (var i = 0; i < proV.length; i++) {
                if (proV[i][0] == temp1) {
                    count = i;
                    /* add by XMX */
                    break;
                    /* end by xmx */

                }

            }
            if (count == -1) {
                alert('unavailable variable ' + temp1);
            }
            // comment by XMX on 2012/04/18
            /*var temp2 = this.split( '=' )[1].split( '=' )[0].trim().replace( /\{V:/g, '' ).replace( /\}/g, '' ); // get the variableString at the right of ==,such as 'test1 + test2 +test3' in the example SET {V:test} == {V:test1} + {V:test2} + {V:test3}
            console.log( 'this:' + this );
            console.log( 'this.split( == )[1]:' + this.split( '=' )[1] );
            console.log( 'this.split( == )[1].split(  == )[0]:' + this.split( '=' )[1].split( '=' )[0] );
            console.log( 'temp2InExcuteArrFunction:' + temp2 );
            temp2 = ineteadVar( temp2 );
            console.log( 'temp2InExcuteArrFunctionAfterExcuteIneteadVar:' + temp2 );*/
            // end comment by XMX on 2012/04/18
            // this function is used to replace the code above by XMX on 2012/04/18
            var temp2 = this.split('=')[1].split('=')[0].trim();
            temp2 = replaceVariableNameWithVariableValue(temp2);
            //console.log( 'temp2InExcuteArrFunctionAfterExcutereplaceVariableNameWithVariableValue:' + temp2 );
            //end by XMX on 2012/04/18
            //getIndex situation
            if (temp2.indexOf('GetIndex') != -1) {
                parserGetIndex(temp2);
                proV[count][2] = result;
            } else if (temp2.indexOf('Round') != -1) {
                //Round situation
                var roundEval = temp2.split(',')[0].split('Round(')[1];
                var roundNumber = parseInt(temp2.split(',')[1].split(')')[0]);
                try { eval(roundEval); }
                catch (err) {
                    alert('There are some problems with the expression:\n' + this + '\nThe problem can be caused by the fllow reasons:\n1> Variable does not exist.\n2> Variable is not be given a value.\nplease check your varilables setting!');
                    sequenceOrder = sequenceOrderBeforeParse; pageOrder = pageOrderBeforeparse;
                }
                roundEval = eval(roundEval).toString();
                if (roundEval.indexOf('.') == -1) {
                    proV[count][2] = parseInt(roundEval);
                } else {
                    var decimals = roundEval.indexOf('.') + roundNumber;
                    var nextNumber = roundEval.slice(decimals + 1, decimals + 2);
                    if (parseInt(nextNumber) < 5) {
                        proV[count][2] = parseFloat(roundEval.slice(0, decimals + 1));
                    } else {
                        var lastNumber = (parseInt(roundEval.slice(decimals, decimals + 1)) + 1).toString();
                        var pre = roundEval.slice(0, decimals);
                        proV[count][2] = parseFloat(pre + lastNumber);

                    }

                }
            } else {
                //no special operator situation	
                // comment by XMX on 2012/04/18		
                //temp2 = ineteadVar( temp2 );
                // end comment by XMX on 2012/04/18
                var evalFlag = true;
                try { eval(temp2); }
                catch (err) {

                    proV[count][2] = temp2;

                    evalFlag = false;
                }
                if (evalFlag == true) {
                    proV[count][2] = eval(temp2);

                }


            }
            var userGUID = $(oraiRoot).attr('UserGUID');
            var programGUID = $(oraiRoot).attr('ProgramGUID');
            if ($(oraiRoot).find('Session').length > 0) {
                var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            } else {
                var sessionGUID = '';
            }

            //set every variable changing MSG to backend
            var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '"> <Assignments> <Assingment Variable="' + proV[count][0] + '" Value="' + proV[count][2] + '"/> </Assignments> </XMLModel>';
            $.ajax({
                //url: getReturnDataServerUrl(),
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: msg,
                dataType: "text"
            });
        };

        //GOTO x.x
        if (this.match(/GOTO\s+\d+\.\d+/) != null) {
            sequenceOrder = parseInt(this.split('GOTO')[1].split('.')[0].trim());
            // added by XMX on 2012/04/20
            if (sequenceOrder == 0) {
                sequenceOrder = '';
            }
            // end on 2012/04/20
            pageOrder = parseInt(this.split('GOTO')[1].split('.')[1].trim());
            directllyGo = false;
        }

        //GO TO getIndex

        if (this.indexOf('GOTO GetIndex') == 0) {
            parserGetIndex(this);
            sequenceOrder = result;
            pageOrder = parseInt(extra.toString().split('.')[1]);
            directllyGo = false;
        }

        //GOTO NextPageSequence
        if (this.indexOf('GOTO NextPageSequence') == 0) {
            sequenceOrder += 1;
            pageOrder = 1;
            directllyGo = false;
        }

        //GOTO END
        if (this.indexOf('GOTO END') == 0) {
            if ($(oraiRoot).find("Session").length > 0) {
                /*console.log("sessionEnding7");*/
                sessionEnding();
            } else {
                quesFlag = false;
                sessionEndFlag = true;
                directllyGo = false;
                relapseMsgEnd();
                return false;
            }
        }

        //GOWEB
        if (this.indexOf('GOWEB') == 0) {
            /* ADDED BY xmx */
            var url = this.split('GOWEB')[1].trim();
            if ($(oraiRoot).find("Session").length > 0) {
                sessionEndingForGoWeb(url);
            } else {
                quesFlag = false;
                sessionEndFlag = true;
                directllyGo = false;
                relapseMsgEnd();
                return false;
            }
            /* end by xmx  on 2012/01/11*/
            //comment by xmx on 2012/01/11
            /* var url = this.split('GOWEB')[1].trim();
            window.location.replace(url);
            directllyGo = false;*/
        }

        //GOSUB GOES HERE
        if (this.indexOf('GOSUB') == 0) {
            directllyGo = false;
            if ($(oraiRoot).find("Session").length > 0) {
                sequenceOrderBeforeGosub = sequenceOrder;
                pageOrderBeforeGosub = pageOrder;
            } else {
                pageOrderBeforeGosub = pageOrder;
            }
            pageOrder = 1;
            if (sequenceOrderBeforeGosub != "") {
                var seqGuidBeforeGoSub = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr('Order') == sequenceOrderBeforeGosub }).attr("GUID");
                var pageGuidBeforeGoSub = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr('Order') == sequenceOrderBeforeGosub }).find("Page").filter(function () { return $(this).attr("Order") == pageOrderBeforeGosub }).attr("GUID");
                sequenceGUIDBeforeGOSUB = seqGuidBeforeGoSub;
                pageGUIDBeforeGOSUB = pageGuidBeforeGoSub;
                //sequenceGUIDBeforeGOSUBArray.push( { 'seqGuidBeforeGoSub': seqGuidBeforeGoSub,'isBefore':isBefore } );
                // pageGUIDBeforeGOSUBArray.push( pageGuidBeforeGoSub );
                pageInfoGUIDBeforeGOSUBArray.push({ 'seqGuidBeforeGoSub': seqGuidBeforeGoSub, 'pageGuidBeforeGoSub': pageGuidBeforeGoSub, 'isBefore': isBefore });
                goSubStepArr.push({ 'seqGuidBeforeGoSub': seqGuidBeforeGoSub, 'pageGuidBeforeGoSub': pageGuidBeforeGoSub, 'isBefore': isBefore });
            } else {
                var seqGuidBeforeGoSub = seqGuid;
                pageGuidBeforeGoSub = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr('GUID') == seqGuidBeforeGoSub }).find("Page").filter(function () { return $(this).attr("Order") == pageOrderBeforeGosub }).attr("GUID"); ;
                //sequenceGUIDBeforeGOSUBArray.push( seqGuidBeforeGoSub );
                // pageGUIDBeforeGOSUBArray.push( pageGuidBeforeGoSub );
                pageInfoGUIDBeforeGOSUBArray.push({ 'seqGuidBeforeGoSub': seqGuidBeforeGoSub, 'pageGuidBeforeGoSub': pageGuidBeforeGoSub, 'isBefore': isBefore });
                goSubStepArr.push({ 'seqGuidBeforeGoSub': seqGuidBeforeGoSub, 'pageGuidBeforeGoSub': pageGuidBeforeGoSub, 'isBefore': isBefore });
            }
            //console.log( "sequenceGUIDBeforeGOSUBArray:" + sequenceGUIDBeforeGOSUBArray );
            //console.log( "pageGUIDBeforeGOSUBArray:" + pageGUIDBeforeGOSUBArray );
            relapseSequenceGUID = this.split('{Relapse:')[1].split('}')[0];
            sequenceGUIDBeforeGOSUB = seqGuid;
            seqGuid = this.split('{Relapse:')[1].split('}')[0];
            // root = $( root ).find( 'Relapse' );
            //root = $( oraiRoot ).find( 'Relapse' );
            var relapseSeqObj = $(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid });
            sessionName = 'relapse';
            sequenceOrder = "";
        }

    });
}
function getVariableListInExpression(expression) {
    // var regPattern = '\{(V:){1}[A-Za-z0-9_ ]+\}';
    var regPattern = '\{(V:){1}(\\w|\\s|\u00E5|\u00E6|\u00F8|\u00C5|\u00C6|\u00D8|\u00FC|\u00DC)+\}';
    var regex = new RegExp(regPattern, 'gm');
    var result = expression.match(regex);
    return result;
}
function perform(arr, isBefore) {

    var tempFlag = true;
    $(arr).each(function (i) {
        //IF

        if ($(arr)[i][0].match(/^IF/) != null) {
            tempFlag = false;
            // comment by XMX  on 2012/04/18
            /* var temp = $( $( arr )[i] )[0].replace( /IF/, '' ).trim(); // get the condition after IF ,such as 'IF {V:test} == 0',get {V:test} == 0
            temp = temp.replace( /\{V:/g, '' ).replace( /\}/g, '' ); // remove {V:} from the IF condition ,get test == 0
            for ( var j = 0; j < proV.length; j++ ) {
            var reg = eval( '/' + proV[j][0] + '\\s\\W/i' );
            var reg1 = eval( '/' + proV[j][0] + '\\)/i' )
            if ( temp.match( reg ) || temp.match( reg1 ) ) {
            var reg2 = eval( '/' + proV[j][0] + '/g' );
            temp = temp.replace( reg2, proV[j][2] );
            console.log('temp:'+temp);
            }
            }*/
            // end comment on 2012/04/18 

            // this code used to replace code comment above,added by XMX on 2012/04/18
            var temp = $($(arr)[i])[0].replace(/IF/, '').trim();
            //console.log( 'Expression:' + $( $( arr )[i] )[0] );
            var variableList = getVariableListInExpression($($(arr)[i])[0]);
            // console.log( 'variableList:' + variableList );
            if (variableList != undefined && variableList != null) {
                var varibaleListLength = variableList.length;
                for (var v = 0; v < varibaleListLength; v++) {
                    var variableName = variableList[v].split(':')[1].substring(0, variableList[v].split(':')[1].length - 1);
                    //  console.log( 'variableName:' + variableName );
                    var variableValue = getProgramVariableValue(variableName);
                    // console.log( 'variableValue:' + variableValue );
                    temp = temp.replace(variableList[v], variableValue);
                    // console.log('temp:'+temp);
                }
            } else {
                temp = temp;
            }
            // end this code by XMX on 2012/04/18
            try { eval(temp); }
            catch (err) { alert('There are some problems with the expression:\n' + this + '\nThe problem can be caused by the fllow reasons:\n1> Variable does not exist.\n2> Variable is not be given a value.\nplease check your varilables setting!'); sequenceOrder = sequenceOrderBeforeParse; pageOrder = pageOrderBeforeparse; };

            if (eval(temp) == true) {
                ifTrue = true;
                $($(arr)[i]).each(function (i) {
                    excuteArr(this, isBefore);
                });
                return false; // added by XMX on 2012/05/22,that is because if there are more than one conditioin is true, excute the first one ,then break;
            } else {
                ifTrue = false;
            }
        }
    });

    //has no IF.(So IT's just orders)
    if (tempFlag == true) {
        excuteArr($(arr), isBefore);
    }
}
function loginLoadingAjax() {
    var proGUID = $(root).attr('ProgramGUID');
    var pagGUID = $(root).find('Page').filter(function () { return parseInt($(this).attr('Order')) == 1 }).attr('GUID');
    var userGUID = $(root).attr('UserGUID');
    var sessionGUID = $(root).find('Session').attr('GUID');
    var usr = $('#bubble-yellow input').eq(0).val().trim();
    var pwd = $('#bubble-yellow input').eq(1).val().trim();
    var backXml = '<XMLModel UserGUID = "' + userGUID + '" ProgramGUID = "' + proGUID + '" SessionGUID = "' + sessionGUID + '" PageSequenceOrder = "1" PageGUID = "' + pagGUID + '"><Login Username="' + usr + '" Password="' + pwd + '"/></XMLModel>';
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL("submit"),
        type: 'POST',
        processData: false,
        data: backXml,
        dataType: "text",
        success: function (data) {
            var returnCode = data.substring(0, 1);
            var returnData = data.substr(2);
            if (returnCode == '1') {
                tipMessages = [];
                step = -1;
                stepHistory = [];
                programRoomName = [];
                programVariables = [];
                specialStirngs = [];
                proV = [];
                userUpdateTimeZone = "";
                userSMSToEmailValue = false;

                introAnimation();
                root = returnData;
                encodingRootXML();
                oraiRoot = root;
                sessionName = $(root).find("Session").attr("Name");
                // start added for retake by XMX on 2011/12/1
                retakeValue = $(root).attr("IsRetake").toLowerCase();
                if (retakeValue == "true" || retakeValue == "1") {
                    // getProgramVariable($(root));
                }
                var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
                if (isSMSToEmail == "1" || isSMSToEmail == "true") {
                    userSMSToEmailValue = true;
                } else {
                    userSMSToEmailValue = false;
                }

                /* for TimeZone */
                var userTimeZone = $(oraiRoot).attr("UserTimeZone");
                var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
                if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
                    userUpdateTimeZone = userTimeZone;
                } else {
                    userUpdateTimeZone = programTimeZone;
                }
                //getProgramVariable($(oraiRoot));
                getTipMessages($(oraiRoot));
                getSepcialStrings($(oraiRoot));
                // end added for retake by XMX on 2011/12/1
                showDialog();
                //intlize variable Array
                proArr($(root));
                //itemScore=[];
                //GUIDValue=[];
                //intlize page elements
                //loadPage($(root));
                // setTimeout(hiddenRoomInfo,1500);
                isFirstLoaded = true;

            } else if (returnCode == '2') {
                if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                    var message = data;
                    var ctppHostStr = getURL('ctpp');
                    var ctppUrl = ctppHostStr + '?CTPP=' + message;
                    window.location.replace(ctppUrl);
                }
            } else {
                setOrangeBubble('FadeOut', function () {
                    showNormalMessage(returnData);
                });


            }
        },
        error: function () {
            //alert( ' read  XML unsucessfully' );
        }
    });
}
function sessionEndingAjax() {
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        pageObj = $(root).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid })
						.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
    } else {
        pageObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
						.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
    }
    var sEEndingXML = '';
    var userGUID = $(root).attr('UserGUID');
    var programGUID = $(root).attr('ProgramGUID');
    var sessionGUID = $(root).find('Session').attr('GUID');
    var isRetakeValue = $(root).attr('IsRetake');
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');
        // pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
        // pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

        pageSequenceOrder = "";
        //relapsePageSequenceGUID = relapseSequenceGUID;
        if ($(oraiRoot).find('Session').length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            relapsePageSequenceGUID = relapseSequenceGUID;
        } else {
            sessionGUID = '';
            pageGUID = '';
            pageSequenceGUID = '';
            relapsePageSequenceGUID = seqGuid;
        }
        relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapseSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    }
    if (afterisEnd) {
        var pageGUID = $(pageObj).attr("GUID");
        sEEndingXML = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" IsRetake="' + isRetakeValue + '"  PageGUID="' + pageGUID + '" > <SessionEnding/> </XMLModel>';
    } else {
        sEEndingXML = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" IsRetake="' + isRetakeValue + '"> <SessionEnding/> </XMLModel>';
    }

    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL("submit"),
        type: 'POST',
        processData: false,
        data: sEEndingXML,
        dataType: "text",
        success: function (data) {
            //alert('the quest URL is: ' + getReturnDataServerUrl());
            var returnCode = data.substring(0, 1);
            var message = data.substr(2);
            if (returnCode == '1') {
                tipMessages = [];
                programRoomName = [];
                programVariables = [];
                specialStirngs = [];
                step = -1;
                stepHistory = [];
                proV = [];
                userUpdateTimeZone = "";
                userSMSToEmailValue = false;
                // loadIntroPage added by XMX on 2012/08/16
                //  loadIntroPage(ctppXmlobj, function () {
                introAnimation();
                root = message;
                encodingRootXML();
                // start added for retake by XMX on 2011/12/1
                oraiRoot = root;
                if ($(root).attr("IsRetake") != undefined && $(root).attr("IsRetake") != null && $(root).attr("IsRetake") != "") {
                    retakeValue = $(root).attr("IsRetake").toLowerCase();
                }
                if (retakeValue == "true" || retakeValue == "1") {
                    // getProgramVariable($(root));
                }
                var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
                if (isSMSToEmail == "1" || isSMSToEmail == "true") {
                    userSMSToEmailValue = true;
                } else {
                    userSMSToEmailValue = false;
                }
                /* for TimeZone */
                var userTimeZone = $(oraiRoot).attr("UserTimeZone");
                var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
                if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
                    userUpdateTimeZone = userTimeZone;
                } else {
                    userUpdateTimeZone = programTimeZone;
                }
                //getProgramVariable($(oraiRoot));
                getTipMessages($(oraiRoot));
                getSepcialStrings($(oraiRoot));
                // end added for retake by XMX on 2011/12/1
                //first load, unless it's preview, the sequence order and page order should all be 1 since it's the first load;
                //intlize top bar(should never be changed)
                //showTopBar();
                //intlize variable Array
                proArr($(root));
                //itemScore=[];
                //GUIDValue=[];
                //intlize page elements
                // setTimeout(hiddenRoomInfo,1000);
                //loadPage($(root));
                sessionEndFlag = false;
                //  });
            } else if (returnCode == '2') {
                var platform = $(oraiRoot).attr("Platform");
                var msg = "SessionClose";
                if (platform != undefined && platform != null && platform == "Win8") {
                    // sendNotify(msg);
                    window.external.notify("SessionClose");
                } else {
                    if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                        // introAnimation();
                        root = message;
                        var ctppHostStr = getURL('ctpp');
                        var ctppUrl = ctppHostStr + '?CTPP=' + root;
                        window.location.replace(ctppUrl);
                    }
                }
            } else if (returnCode == '3') {
                var platform = $(oraiRoot).attr("Platform");
                loadSessionEndingPage(message, function () {
                    $("#bubble-blue").css("cursor", "default");
                    $("#bubble-blue").unbind("click");
                    var msg = "SessionClose";
                    if (platform != undefined && platform != null && platform == "Win8") {
                        // sendNotify(msg);
                        window.external.notify("SessionClose");
                    }
                });
            } else {
                setOrangeBubble('FadeOut', function () {
                    showNormalMessage(message);
                });

            }
            sessionEndFlag = false;
            stepHistory = [];
            step = -1;
            directllyGo = true;
            sequenceOrder = 1;
            pageOrder = 1;
            itemScore = [];
            GUIDValue = [];
            historyAnswersObj = [];
            historyAnswers = [];
            // added by XMX on 2012/05/29
            sequenceGUIDBeforeGOSUBArray = [];
            pageGUIDBeforeGOSUBArray = [];
            pageInfoGUIDBeforeGOSUBArray = [];
            goSubStepArr = [];
            isReturnFromGoSub = false;
            isBackButton = false;
            isFirstLoaded = false;
            /*isCatchUpDay = false; *//*This is comment for DTD-200 */
            excuteBefore = true;
            excuteAfter = true;
            goSubReturnTime = 1;
            programRoomName = [];
            //
        },
        error: function () {
            //alert( 'load XML file error' );
        }
    });
}
function sendNotify(msg) {
    //  parent.postMessage("SessionClose","*");
    window.external.notify(msg);
}
function loadSessionEndingPage(message,callback) {
    introAnimation();
    root = message;
    encodingRootXML();
    // start added for retake by XMX on 2011/12/1
    oraiRoot = root;
    if ($(root).attr("IsRetake") != undefined && $(root).attr("IsRetake") != null && $(root).attr("IsRetake") != "") {
        retakeValue = $(root).attr("IsRetake").toLowerCase();
    }
    var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
    if (isSMSToEmail == "1" || isSMSToEmail == "true") {
        userSMSToEmailValue = true;
    } else {
        userSMSToEmailValue = false;
    }
    /* for TimeZone */
    var userTimeZone = $(oraiRoot).attr("UserTimeZone");
    var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
    if (userTimeZone != undefined && userTimeZone != null && userTimeZone != '') {
        userUpdateTimeZone = userTimeZone;
    } else {
        userUpdateTimeZone = programTimeZone;
    }
    //getProgramVariable($(oraiRoot));
    getTipMessages($(oraiRoot));
    getSepcialStrings($(oraiRoot));
    proArr($(root));
    processLive($(root));
    sessionEndFlag = false;
    if (callback && typeof (callback) == "function") {
        callback();
    }
}
//{{{----the common AJAX function for 3 situations: when user click button at the last page / when "GOTO END" in AfterExpression in current page / when 'End Page' in AfterExpression in the next page
function sessionEnding() {
    if ($(oraiRoot).find("Session").length > 0) {
        if (quesFlag) {
            //stepHistory array for 'BACK' button
            sessionEndFlag = true;
            directllyGo = false;
            // comment by xmx on 2012/08/16
            var isFirstTimeLoadingPage = false;
            reloadedIntro(isFirstTimeLoadingPage);
            // end comment by xmx on 2012/08/16
          //  sessionEndingAjax();
        }
    }
}
//{{{----single pass AJAX function: page start / page end / sequence start / sequence end
function msgPageStart() {
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
    var pageGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    var pageSequenceGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).attr('GUID');
    if (pageSequenceGUID == undefined || pageSequenceGUID == null) {
        pageSequenceGUID = "";
    }
    var pageSequenceOrder = sequenceOrder;
    var relapsePageSequenceGUID = "";
    var relapsePageGUID = "";
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');
        // pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
        // pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

        pageSequenceOrder = "";
        //relapsePageSequenceGUID = relapseSequenceGUID;
        if ($(oraiRoot).find("Session").length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            relapsePageSequenceGUID = relapseSequenceGUID;
        } else {
            sessionGUID = '';
            pageGUID = '';
            pageSequenceGUID = '';
            relapsePageSequenceGUID = relapseSequenceGUID;
        }
        relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapsePageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    }
    var pageType = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('Type');
    var isGraph = "0";
    if (pageType == "Graph") {
        isGraph = "1";
    } else {
        isGraph = "0";
    }
    var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageSequenceGUID="' + pageSequenceGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '"> <PageStart IsGraph="' + isGraph + '"/> </XMLModel>';

    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL("submit"),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text",
        success: function (returnData) {
            var returnCode = returnData.substring(0, 1);
            var message = returnData.substr(2);
            graphTemplateObj = null;
            if (returnCode == "1") {
                if (isGraph == "1") {
                    $(".mask").css("display", "none");
                    if (message == "OK") {
                        isBeforePageStart = true;
                        graphTemplateObj = pageObj;
                    } else {
                        graphTemplateObj = $(message);
                        var isBeforePageStart = false;
                        loadDataForGraphTemplate(isBeforePageStart);
                    }
                }

            }
        }
    });
}
function msgPageEnd() {
    isSamePage = false;
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
    var pageSequenceGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).attr('GUID');
    if (pageSequenceGUID == null || pageSequenceGUID == undefined) {
        pageSequenceGUID = "";
    }
    var pageGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    var pageSequenceOrder = sequenceOrder;
    var relapsePageSequenceGUID = "";
    var relapsePageGUID = "";
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');
        //pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
        //pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

        pageSequenceOrder = "";
        //relapsePageSequenceGUID = relapseSequenceGUID;
        if ($(oraiRoot).find("Session").length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            relapsePageSequenceGUID = relapseSequenceGUID;
        } else {
            sessionGUID = '';
            pageGUID = '';
            pageSequenceGUID = '';
            relapsePageSequenceGUID = seqGuid;
        }
        relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapsePageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    }
    var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageSequenceGUID="' + pageSequenceGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '"> <PageEnd/> </XMLModel>';
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL("submit"),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text"
    });
}
function relapseMsgStart() {
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = '';
    var pageSequenceGUID = $(oraiRoot).attr('PageSequenceGUID');
    if ($(oraiRoot).attr('PageGUID') != undefined) {
        var pageGUID = $(oraiRoot).attr('PageGUID');
    } else {
        var pageGUID = '';
    }
    if ($(oraiRoot).attr('IsHelpInCTPP') != undefined) {
        var isHelpInCTPP = $(oraiRoot).attr('IsHelpInCTPP');
        var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '"  PageSequenceGUID="' + pageSequenceGUID + '" IsHelpInCTPP="' + isHelpInCTPP + '"> <RelapseStart/> </XMLModel>';
    }
    if ($(oraiRoot).attr('IsReportInCTPP') != undefined) {
        var isReportInCTPP = $(oraiRoot).attr('IsReportInCTPP');
        var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceGUID="' + pageSequenceGUID + '" IsReportInCTPP="' + isReportInCTPP + '"> <RelapseStart/> </XMLModel>';
    }
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL('submit'),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text"
    });
}
function relapseMsgEnd() {
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = '';
    var pageSequenceGUID = $(oraiRoot).attr('PageSequenceGUID');
    if ($(oraiRoot).attr('PageGUID') != undefined) {
        var pageGUID = $(oraiRoot).attr('PageGUID');
    } else {
        if ($(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression") && $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression") != '') {
            var afterExpression = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("AfterExpression").toLowerCase().trim();
            if (afterExpression == "endpage" || afterExpression == "goto end") {
                var pageGUID = $(oraiRoot).find("PageSequence").filter(function () { return $(this).attr("GUID") == seqGuid }).find("Page").filter(function () { return $(this).attr("Order") == pageOrder }).attr("GUID");
            } else {
                var pageGUID = '';
            }
        } else {
            var pageGUID = '';
        }
    }
    if ($(oraiRoot).attr('IsHelpInCTPP') != undefined) {
        var isHelpInCTPP = $(oraiRoot).attr('IsHelpInCTPP');
        var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '"  PageSequenceGUID="' + pageSequenceGUID + '" IsHelpInCTPP="' + isHelpInCTPP + '"> <RelapseEnd/> </XMLModel>';
    }
    if ($(oraiRoot).attr('IsReportInCTPP') != undefined) {
        var isReportInCTPP = $(oraiRoot).attr('IsReportInCTPP');
        var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceGUID="' + pageSequenceGUID + '" IsReportInCTPP="' + isReportInCTPP + '"> <RelapseEnd/> </XMLModel>';
    }
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL('submit'),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text",
        success: function (returnData) {
            var returnCode = returnData.substring(0, 1);
            var message = returnData.substr(2);
            if (returnCode == '1') {
            } else if (returnCode == '0') {
                showNormalMessage(message);
            } else if (returnCode == '2') {
                var platform = $(oraiRoot).attr("Platform");
                var msg = '';
                if (platform != undefined && platform != null && platform == "Win8") {
                    var isHelpInCTPP = $(oraiRoot).attr('IsHelpInCTPP');
                    var isReportInCTPP = $(oraiRoot).attr('IsReportInCTPP');
                    if (isHelpInCTPP != undefined && isHelpInCTPP != null) {
                        if (isHelpInCTPP == "1" || isHelpInCTPP == "true") {
                            msg = "HelpClose";
                            window.external.notify("HelpClose");
                        }
                    }
                    if (isReportInCTPP != undefined && isReportInCTPP != null) {
                        if (isReportInCTPP == "1" || isReportInCTPP == "true") {
                            msg = "ReportClose";
                            window.external.notify("ReportClose");
                        }
                    }
                    //sendNotify(msg);
                   
                } else {
                    if ($(oraiRoot).attr('IsCTPPEnable') == 'true' || $(oraiRoot).attr('IsCTPPEnable') == '1') {
                        var ctppHostStr = getURL('ctpp');
                        var ctppUrl = ctppHostStr + '?CTPP=' + message;
                        window.location.replace(ctppUrl);
                    }
                }
            }
        },
        error: function (xhr, status) {
            // console.log( 'Status:' + status );
        }
    });
}
function msgSequenceStart() {
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
    var pageGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
    var pageSequenceGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder }).attr('GUID');
    if (pageSequenceGUID == null || pageSequenceGUID == undefined) {
        pageSequenceGUID = "";
    }
    var pageSequenceOrder = sequenceOrder;
    var relapsePageSequenceGUID = "";
    var relapsePageGUID = "";
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');

        //pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
        // pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
        pageSequenceOrder = "";
        if ($(oraiRoot).find("Session").length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            relapsePageSequenceGUID = relapseSequenceGUID;
            pageSequenceOrder = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == pageSequenceGUID }).attr('Order');
        } else {
            sessionGUID = '';
            pageGUID = '';
            pageSequenceGUID = '';
            relapsePageSequenceGUID = seqGuid;
        }
        relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapsePageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

    }
    var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageSequenceGUID="' + pageSequenceGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '"> <PageSequenceStart/> </XMLModel>';
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL('submit'),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text"
    });
}
function msgSequenceEnd() {
    var userGUID = $(oraiRoot).attr('UserGUID');
    var programGUID = $(oraiRoot).attr('ProgramGUID');
    var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
    var pageGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrderBeforeParse }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrderBeforeparse }).attr('GUID');
    var pageSequenceOrder = sequenceOrder;
    var pageSequenceGUID = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrderBeforeParse }).attr('GUID');
    if (pageSequenceGUID == null || pageSequenceGUID == undefined) {
        pageSequenceGUID = "";
    }
    var relapsePageSequenceGUID = "";
    var relapsePageGUID = "";
    if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
        userGUID = $(oraiRoot).attr('UserGUID');
        programGUID = $(oraiRoot).attr('ProgramGUID');
        //pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
        //pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

        pageSequenceOrder = "";

        if ($(oraiRoot).find("Session").length > 0) {
            sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            relapsePageSequenceGUID = relapseSequenceGUID;
        } else {
            sessionGUID = '';
            pageGUID = '';
            pageSequenceGUID = '';
            relapsePageSequenceGUID = seqGuid;
        }
        relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapsePageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');

    }
    var msg = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageGUID="'
	         + pageGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageSequenceGUID="' + pageSequenceGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '"> <PageSequenceEnd/> </XMLModel>';
    $.ajax({
        //url: getReturnDataServerUrl(),
        url: getURL('submit'),
        type: 'POST',
        processData: false,
        data: msg,
        dataType: "text"
    });
}
function saveAnswerForBackBtn(ques, quesIndex) {

    //var pageObj = XMLObj.find('PageSequence').filter(function(){return parseInt($(this).attr('Order')) == sequenceOrder})
    //				.find('Page').filter(function(){return parseInt($(this).attr('Order')) == pageOrder});
    var pageType = pageObj.attr("Type");
    if (pageType == "Get information") {
        var quesType = $(ques).attr("Type");
        var quesGUID = $(ques).attr("GUID");
        var answer = historyAnswers[quesGUID];
        if (quesType == "RadioButton") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForRadioButton(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "CheckBox") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForCheckbox(answer, quesIndex, quesGUID);
            }
        } else if (quesType == "Numeric") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForNumeric(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "Multiline") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForMultipline(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "Singleline") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForSingleline(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "Hidden singleline") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForHiddenSingleline(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "TimePicker") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForTimePicker(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "Slider") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForSlider(answer, quesIndex, quesGUID);
            }
        }
        else if (quesType == "DropDownList") {
            if (answer != '' && answer != null && answer != undefined) {
                saveAnswersForDropdownList(answer, quesIndex, quesGUID);
            }
        }
    }
}
//start added by XMX for retake on 2011/12/1
function getCheckScore(scoreSum) {
    var itemScoreArray = [];
    var root = Math.floor(Math.log(scoreSum) / Math.log(2)) + 1;
    for (var i = 0; i < root; i++) {
        var powValue = Math.pow(2, i);
        var andValue = scoreSum & powValue;
        if (andValue !== 0) {
            itemScoreArray.push(powValue);
        }
    }
    return itemScoreArray;
}
// end added by XMX for retake on 2011/12/1

// update below function by XMX for retake on 2011/12/1
function saveAnswersForCheckbox(answer, quesIndex, quesGUID) {
    var itemGuidStr = "";
    var scoreSum = 0;
    var answerArray = answer.split(";");
    var answerNumer = answer.split(";").length; // how many checkbox the user checked
    $("#bubble-yellow").find('input[name=checkboxGroup' + quesIndex + ']:checkbox').each(function (index, s) {
        var feedback = $(s).attr("value").split(";")[0];
        var score = $(s).attr("value").split(";")[1];
        var itemGuid = $(s).attr("value").split(";")[2];
        if (retakeValue == "true" || retakeValue == "1") {
            var itemScoreValue = getCheckScore(answer);
            var checkItemNum = itemScoreValue.length;
            for (var j = 0; j <= checkItemNum; j++) {
                var everyItemScore = itemScoreValue[j];
                if (everyItemScore == score) {
                    $(s).attr("checked", true);
                    if (feedback != '' && feedback != 'undefined' && feedback != null) {
                        var feedbackStr = '<div class="checkboxfeedback' + index + ' feedbackLabel">' + feedback + '</div>';
                        $(s).parents('.check' + quesIndex + '').append(feedbackStr);
                    }
                }
            }
        } else {
            for (var i = 0; i < answerNumer; i++) {
                if (itemGuid == answerArray[i]) {
                    $(s).attr("checked", true);
                    if (feedback != '' && feedback != 'undefined' && feedback != null) {
                        var feedbackStr = '<div class="checkboxfeedback' + index + ' feedbackLabel">' + feedback + '</div>';
                        $(s).parents('.check' + quesIndex + '').append(feedbackStr);
                    }
                }
            }
            if ($(s).attr("checked")) {
                itemGuidStr += itemGuid + ";";
                scoreSum = parseInt(scoreSum) + parseInt(score);
            }
            itemScore[quesGUID] = scoreSum;
            GUIDValue[quesGUID] = itemGuidStr;
        }
    });
}

function saveAnswersForRadioButton(answer, quesIndex, quesGUID) {
    $("#bubble-yellow").find('input[name=radioButtonGroup' + quesIndex + ']:radio').each(function (index, s) {
        var feedback = $(s).attr("value").split(";")[0];
        var score = $(s).attr("value").split(";")[1];
        var itemGuid = $(s).attr("value").split(";")[2];
        if (retakeValue == "true" || retakeValue == "1") {
            if (answer == score) {
                $(s).attr("checked", "checked");
                if (feedback != "undefined" && feedback != '') {
                    $(s).parents(".radios" + quesIndex + "").append("<div class='radiofeedback feedbackLabel'>" + feedback + "</div>");
                }
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        } else {
            if (itemGuid == answer) {
                $(s).attr("checked", "checked");
                if (feedback != "undefined" && feedback != '') {
                    $(s).parents(".radios" + quesIndex + "").append("<div class='radiofeedback feedbackLabel'>" + feedback + "</div>");
                }
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        }
    });
}

function saveAnswersForDropdownList(answer, quesIndex, quesGUID) {
    var feedback = "";
    var score = "";
    var itemGuid = "";
    var feedbackStr = "";
    var selectOptionObj = $("#bubble-yellow .dropdownList" + quesIndex + "").find("select[name=dropdownlistGroup" + quesIndex + "]").children("option");
    selectOptionObj.each(function (index, option) {
        feedback = $(option).val().split(";")[0];
        score = $(option).val().split(";")[1];
        itemGuid = $(option).val().split(";")[2];
        if (retakeValue == "true" || retakeValue == "1") {
            if (answer == score) {
                $(option).attr("selected", "selected");
                $(option).siblings().removeAttr("selected");
                if (feedback != 'undefined' && feedback != '' && feedback != null) {
                    feedbackStr = '<div class="dropDownListfeedback' + quesIndex + ' feedbackLabel">' + feedback + '</div>';
                    $(feedbackStr).insertAfter($('.selectbox'));
                }
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        } else {
            if (itemGuid == answer) {
                $(option).attr("selected", "selected");
                $(option).siblings().removeAttr("selected");
                if (feedback != 'undefined' && feedback != '' && feedback != null) {
                    feedbackStr = '<div class="dropDownListfeedback' + quesIndex + ' feedbackLabel">' + feedback + '</div>';
                    $(feedbackStr).insertAfter($('.selectbox'));
                }
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        }
    });
}


function saveAnswersForSlider(answer, quesIndex, quesGUID) {
    var itemGroup = $("#bubble-yellow .sliders" + quesIndex + "").find('input[name=itemguid' + quesIndex + ']:text').val().split(";");
    var uiValue = 0;
    for (var i = 0; i < itemGroup.length; i++) {
        var itemValue = itemGroup[i].split("|")[0];
        var score = itemGroup[i].split("|")[1];
        var itemGuid = itemGroup[i].split("|")[2];
        if (retakeValue == "true" || retakeValue == "1") {
            if (answer == score) {
                uiValue = itemValue;
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        } else {
            if (itemGuid == answer) {
                uiValue = itemValue;
                itemScore[quesGUID] = score;
                GUIDValue[quesGUID] = itemGuid;
            }
        }
    }

    $('#bubble-yellow .sliders' + quesIndex + ' #slider' + quesIndex + '').slider(
    {
        value: parseInt(uiValue),
        slide: function (event, ui) {
            $('#bubble-yellow .sliders' + quesIndex + ' #amount' + quesIndex + '').val(ui.value);
            for (var i = 0; i < itemGroup.length; i++) {
                var itemValueSlide = itemGroup[i].split("|")[0];
                var scoreSlide = itemGroup[i].split("|")[1];
                var itemGuidSlide = itemGroup[i].split("|")[2];
                if (itemValueSlide == ui.value) {
                    itemScore[quesGUID] = scoreSlide;
                    GUIDValue[quesGUID] = itemGuidSlide;
                }
            }
        }
    });
    $('#amount' + quesIndex + '').attr("value", uiValue);

}

function saveAnswersForTimePicker(answer, quesIndex, quesGUID) {
    var hours = parseInt(answer / 60);
    var minutes = parseInt(answer % 60);
    $(".timppicker" + quesIndex + "").find('input[name=qty2' + quesIndex + ']:text').attr("value", hours);
    $(".timppicker" + quesIndex + "").find('input[name=qty1' + quesIndex + ']:text').attr("value", minutes);
    itemScore[quesGUID] = answer;
    GUIDValue[quesGUID] = answer;
}

function saveAnswersForSingleline(answer, quesIndex, quesGUID) {
    $('#bubble-yellow').find('.singleLine' + quesIndex + '').find('input').val(answer);
    if (retakeValue != 'true' || retakeValue != "1") {
        itemScore[quesGUID] = answer;
        GUIDValue[quesGUID] = answer;
    }
}

function saveAnswersForHiddenSingleline(answer, quesIndex, quesGUID) {
    $('#bubble-yellow').find('.hiddenSingleLine' + quesIndex + '').find('input').val(answer);
    if (retakeValue != 'true' || retakeValue != "1") {
        itemScore[quesGUID] = answer;
        GUIDValue[quesGUID] = answer;
    }
}

function saveAnswersForMultipline(answer, quesIndex, quesGUID) {
    $('#bubble-yellow').find('.textarea' + quesIndex + '').find('textarea').val(answer);
    itemScore[quesGUID] = answer;
    GUIDValue[quesGUID] = answer;
}

function saveAnswersForNumeric(answer, quesIndex, quesGUID) {
    $("#bubble-yellow .qbox" + quesIndex + "").find('input[name=qty' + quesIndex + ']:text').attr("value", parseInt(answer).toString());
}
// end update all above functions by XMX for retake on 2011/12/1
function saveAnswersForChoosePreferences(answer) {
    $(".preferences #selectedGUID").val(answer.join(";"));
    //var preferenceGUID=answer.split(";");
    var selectedPreferenceLen = answer.length;
    $(".preferences ul li").each(function (liIndex, li) {
        for (var i = 0; i < selectedPreferenceLen; i++) {
            var evereyPreGUID = answer[i];
            if ($(this).find("input[type=hidden]").val() == evereyPreGUID) {
                $(this).addClass("preferenceSelected");
            }
        }
    });


}
/** ADDED BY xmx */
function sessionEndingForGoWeb(url) {
    var regPattern = /\{V:.+?\}/;
    var urlStr = url;
    var hasVariable = regPattern.test(urlStr);
    if (hasVariable) {
        if (urlStr.indexOf("?") != -1) {
            var pageStr = urlStr.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
            var parameterStr = urlStr.split("?")[1];
            var paraArray = parameterStr.split('&');
            for (var i = 0; i < paraArray.length; i++) {
                var parameter = paraArray[i];
                var paraName = parameter.split("=")[0];
                var paraValue = parameter.split("=")[1];
                if (regPattern.test(paraValue)) {
                    var variableName = paraValue.split("{V:")[1].split("}")[0];
                    var variableValue = getProgramVariableValue(variableName).toString();
                    urlStr = urlStr.replace(paraValue, variableValue);
                }
            }
        }
    }
    if (quesFlag) {
        sessionEndFlag = true;
        directllyGo = false;
        pageObj = $(root).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
						.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
        var sEEndingXML = '';
        var goWebXML = '';
        var userGUID = $(root).attr('UserGUID');
        var programGUID = $(root).attr('ProgramGUID');
        var sessionGUID = $(root).find('Session').attr('GUID');
        var isRetakeValue = $(root).attr('IsRetake');
        if (sessionName == "relapse" || isNaN(sequenceOrder) || sequenceOrder == "") {
            userGUID = $(oraiRoot).attr('UserGUID');
            programGUID = $(oraiRoot).attr('ProgramGUID');
            if ($(oraiRoot).find('Session').length > 0) {
                sessionGUID = $(oraiRoot).find('Session').attr('GUID');
            } else {
                sessionGUID = '';
            }
            // pageSequenceGUID = $(oraiRoot).find("Relapse").find('PageSequence').attr("GUID");
            // pageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == pageSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
            pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
            pageSequenceOrder = "";
            relapsePageSequenceGUID = relapseSequenceGUID;
            relapsePageGUID = $(oraiRoot).find("Relapse").find('PageSequence').filter(function () { return $(this).attr("GUID") == relapseSequenceGUID }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder }).attr('GUID');
        }
        if (afterisEnd) {
            var pageGUID = $(pageObj).attr("GUID");
            sEEndingXML = '<XMLModel PageGUID="' + pageGUID + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" IsRetake="' + isRetakeValue + '"> <SessionEnding/> </XMLModel>';
        } else {
            sEEndingXML = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" IsRetake="' + isRetakeValue + '"> <SessionEnding/> </XMLModel>';
        }
        if (hasVariable) {
           var  testUrl = urlStr.replace(/&/gi, "&amp;");
           goWebXML = '<XMLModel  UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" IsRetake="' + isRetakeValue + '" URL="' + testUrl + '"><GoWeb/> </XMLModel>';
            $.ajax({
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: goWebXML,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var message = data.substr(2);
                    if (returnCode == 0) {
                        alert("errorMessage For GoWeb to LF");
                    } else if (returnCode == 1) {
                        var redirectURL = message;
                        $.ajax({
                            url: getURL("submit"),
                            type: 'POST',
                            processData: false,
                            data: sEEndingXML,
                            dataType: "text",
                            success: function (data) {
                                var sessionEndReturnCode = data.substring(0, 1);
                                var sessionEndMessage = data.substr(2);
                                window.location.replace(redirectURL);
                                directllyGo = false;
                                sessionEndFlag = false;
                            },
                            error: function () {
                                alert("Error is sessionEndingForGoWeb");
                            }
                        });
                    }
                },
                error: function () {
                    alert("GOWEB to LF");
                }
            });
        } else {
            $.ajax({
                url: getURL("submit"),
                type: 'POST',
                processData: false,
                data: sEEndingXML,
                dataType: "text",
                success: function (data) {
                    var returnCode = data.substring(0, 1);
                    var message = data.substr(2);
                    window.location.replace(urlStr);
                    directllyGo = false;
                    sessionEndFlag = false;
                },
                error: function () {
                    alert("Error is sessionEndingForGoWeb");
                }
            });
        }
    }
}