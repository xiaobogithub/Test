// layout details
function getTipMessages(XMLObj) {
    var message = $(XMLObj).find('Message');
    if (message != null && message != undefined) {
        $(message).each(function (i, m) {
            var name = $(m).attr("Name");
            var title = $(m).attr("Title");
            var msg = $(m).attr("Message");
            var backBtnName = $(m).attr("BackButtonName");
            tipMessages[name] = { 'Title': title, 'Message': msg, 'BackButtonName': backBtnName };
        });
    }
}
function getSepcialStrings(XMLObj) {
    var specialStringsObj = $(XMLObj).find("SpecialString");
    if (specialStringsObj != null && specialStringsObj != undefined) {
        var specialStringObj = specialStringsObj.find("StringItem");
        if (specialStringObj != null && specialStringObj != undefined) {
            $(specialStringObj).each(function (index, sString) {
                var name = $(sString).attr("Name");
                var value = $(sString).attr("Value");
                specialStirngs[name] = value;
            });
        }
    }
}
function checkReturnDataIsXmlOrMessage(returnMainData) {
    var isXML = false;
    if ($(returnMainData).find('Session').length == 0) {
        isXML = false;
    } else {
        isXML = true;
    }
    return isXML;
}
function checkMobileFormat(mobile) {
    var reg = /^0*(13|15)\d{9}$/;
    return reg.test(mobile);

}
function checkEmailFormat(emailVal) {
    var reg = /^([a-zA-Z0-9_\.-])+@([a-zA-Z0-9_\.-])+(\.[a-zA-Z0-9_-])*/;
    return reg.test(emailVal);
}
function checkSerialNumFormat(serialNum) {
    var reg = /^[XDC][A-E].{9}(..)?$/;
    return reg.test(serialNum);

}
function showNormalMessage(message) {
    var msgNode = "";
    msgNode= document.createTextNode(message);
    $('.upper').html(msgNode);
    $('.upper p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>");
    $('#bubbles').fadeOut(500);
    $('.fadecover').delay(500).fadeIn(500, function () {
        $(".bottomgradient").css('opacity', '0.0');
        $("#programRoomBar").html("");
        $('.wrapper-dialoguebox').fadeIn();
    });
    $(".fadecover,.hideDialoguebox").unbind();
    $(".fadecover,.hideDialoguebox").bind("click", function (e) {
        /* Because the bottomgradient has different view states on portrait and landscape we cannot hide or show it entirely because we don't know its current state */
        e.preventDefault();
        $(".bottomgradient").css('opacity', '1.0');
        $('.wrapper-dialoguebox').fadeOut(function () {
            $('.fadecover').delay(500).fadeOut(function () {
                $("#programRoomBar").html("");
                $(".bottomgradient").css('opacity', '1.0');
                $('#bubbles').fadeIn(500);
            });
        });
    });
}
function showMessageForFaildRegister(messageSendFromServer) {
    var ms = tipMessages["RegisterFailed"];
    if (ms != null && ms != undefined) {
        var title = ms.Title;
        var message = ms.Message;
        var backBtnName = ms.BackButtonName;
        $('.upper').html('<h1>' + title + '</h1><p>' + messageSendFromServer + '</p>');
        $('.lower p a').text(backBtnName);
    } else {
        var msgNode = document.createTextNode(messageSendFromServer);
        $('.upper').html('<p>"' + msgNode + '"</p>');
    }
    $('.upper p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>");
    $('#bubbles').fadeOut(500);
    $('.fadecover').delay(500).fadeIn(500, function () {
        $(".bottomgradient").css('opacity', '0.0');
        $("#programRoomBar").html("");
        $('.wrapper-dialoguebox').fadeIn();
    });
    $(".fadecover,.hideDialoguebox").unbind();
    $(".fadecover,.hideDialoguebox").bind("click", function (e) {
        e.preventDefault();
        $('.wrapper-dialoguebox').fadeOut(function () {
            $('.fadecover').delay(500).fadeOut(function () {
                $("#programRoomBar").html("");
                $(".bottomgradient").css('opacity', '1.0');
                $('#bubbles').fadeIn(500);
            });
        });
    });
}
function showTipMessage(msgName) {
    var ms = tipMessages[msgName];
    if (ms != null && ms != undefined) {
        var title = ms.Title;
        var message = ms.Message;
        var backBtnName = ms.BackButtonName;
        $('.upper').html('<h1>' + title + '</h1><p>' + message + '</p>');
        $('.upper p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>");
        $(".lower p a").text(backBtnName);
        $('#bubbles').fadeOut(500);
        $('.fadecover').delay(500).fadeIn(500, function () {
            $(".bottomgradient").css('opacity', '0.0');
            $("#programRoomBar").html("");
            $('.wrapper-dialoguebox').fadeIn();
        });
        $(".fadecover,.hideDialoguebox").unbind();
        $(".fadecover,.hideDialoguebox").bind("click", function (e) {
            e.preventDefault();
            $('.wrapper-dialoguebox').fadeOut(500, function () {
                $('.fadecover').delay(500).fadeOut(function () {
                    $("#programRoomBar").html("");
                    $(".bottomgradient").css('opacity', '1.0');
                    $('#bubbles').fadeIn(500);

                });
            });
        });
    } else {
        $('.upper').html('<p>No Tip Message &lt;' + msgName + '&gt;</p>');
        $(".lower p a").text("OK");
        $('#bubbles').fadeOut(500);
        $('.fadecover').delay(500).fadeIn(500, function () {
            $(".bottomgradient").css('opacity', '0.0');
            $("#programRoomBar").html("");
            $('.wrapper-dialoguebox').fadeIn();
        });
        $(".fadecover,.hideDialoguebox").unbind();
        $(".fadecover,.hideDialoguebox").bind("click", function (e) {
            e.preventDefault();
            $('.wrapper-dialoguebox').fadeOut(function () {
                $('.fadecover').delay(500).fadeOut(function () {
                    $("#programRoomBar").html("");
                    $(".bottomgradient").css('opacity', '1.0');
                    $('#bubbles').fadeIn(500);
                });
            });
        });
    }

}
function showTipMessageForCatchUpDay() {
    //$(".mask").css("display", "none");
    var msgName = 'CatchingUpEarlyDay';
    var msCatchUpDay = tipMessages[msgName];
    if (msCatchUpDay != null && msCatchUpDay != undefined) {
        var title = msCatchUpDay.Title;
        var message = msCatchUpDay.Message;
        var backBtnName = msCatchUpDay.BackButtonName;
        $('.upper').html('<h1>' + title + '</h1><p>' + message + '</p>');
        $('.lower p a').text(backBtnName);
        $('.upper p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>")
        $('#bubbles').fadeOut(500, function () {
            $('.fadecover').delay(500).fadeIn(500, function () {
                $(".bottomgradient").css('opacity', '0.0');
                $("#programRoomBar").html("");
                $('.wrapper-dialoguebox').fadeIn();
            });

        });
        $(".fadecover,.hideDialoguebox").unbind();
        $(".fadecover,.hideDialoguebox").bind("click", function (e) {
            e.preventDefault();
            $('.wrapper-dialoguebox').fadeOut(function () {
                $('.fadecover').delay(500).fadeOut(function () {
                    $(".bottomgradient").css('opacity', '1.0');
                    $("#programRoomBar").html("");
                    programBarAnimation();
                });

            });
        });
    } else {
        $('.upper').html('<p>No Tip Message &lt;' + msgName + '&gt;</p>');
        $(".lower p a").text("OK");
        $('#bubbles').fadeOut(500);
        $('.fadecover').delay(500).fadeIn(500, function () {
            $(".bottomgradient").css('opacity', '0.0');
            $("#programRoomBar").html("");
            $('.wrapper-dialoguebox').fadeIn();

        });
        $(".fadecover,.hideDialoguebox").unbind();
        $(".fadecover,.hideDialoguebox").bind("click", function (e) {
            e.preventDefault();
            $('.wrapper-dialoguebox').fadeOut(function () {
                $('.fadecover').delay(500).fadeOut(function () {
                    $(".bottomgradient").css('opacity', '1.0');
                    $("#programRoomBar").html("");
                    programBarAnimation();
                });

            });
        });
    }

}

function showTopBar() {
    $('.controlbar .programtitle').empty();
    var isNotShowDayAndSetMenu = $(root).attr("IsNotShowDayAndSetMenu");
    var dayNumber = $(root).find('Session').attr('Day');
    var day = specialStirngs["Day"];
    var CTPPName = $(root).attr('CTPPName');
    var programName = $(root).attr('ProgramName');
    if (CTPPName == undefined || CTPPName == '' || CTPPName == null) {
        if (programName != undefined && programName != null) {
            $('.controlbar .programtitle').html("<span>" + programName + "</span>");
        } else {
        programName = "";
        $('.controlbar .programtitle').html("<span>" + programName + "</span>");
        }
    } else {
    $('.controlbar .programtitle').html("<span>" + CTPPName + "</span>");
    }
    if (dayNumber != undefined && dayNumber != null) {
        if (day != undefined && day != null && day != '') {
            $('.controlbar .programtitle').append('<span class="programday"> ' + day.toUpperCase() + ' ' + dayNumber + '</span>');
        } else {
            $('.controlbar .programtitle').append('<span class="programday"> DAY ' + dayNumber + '</span>');
        }
    }
    var settingMenu = specialStirngs["SettingMenu"];
    if (settingMenu != undefined && settingMenu != null) {
        $(".button-settings .buttontext").html(settingMenu);
    }
    var back = specialStirngs["Back"];
    if (back != undefined && back != null) {
        $(".button-back .buttontext").html(back);
    }
    if (isNotShowDayAndSetMenu != null && isNotShowDayAndSetMenu != undefined && isNotShowDayAndSetMenu != "") {
        if (isNotShowDayAndSetMenu == "1" || isNotShowDayAndSetMenu.toLowerCase() == 'true') {
            $(".programday").css("display", "none");
            $(".button-settings_showMenu").css("display", "none");
        } else {
            $(".programday").removeAttr("style");
            $(".button-settings_showMenu").css("display", "block");
        }
    } else {
        $(".programday").removeAttr("style");
        $(".button-settings_showMenu").css("display", "block");
    }
    //var programTitleWidth = $('.programtitle').width();
    //$(".controlbar .programtitle").css({ "left": "50%", "margin-left": "-" + parseInt(programTitleWidth) / 2 + "px" });
    $(".button-settings").hide();
}
//
//move getReturnDataServerUrl() and getCurrentUrl() from here 
//
function excuteBeforeExpression(nextPageObj) {
    var be = nextPageObj.attr('BeforeExpression');
    directllyGo = true;
    var isBefore = true;
    recur(be, $(root), isBefore);
    if (!directllyGo) {
    }
}
function addClassForHiddenElements() {
    var imageMode = $(pageObj).attr("ImageMode");
    var mediaObj = $(pageObj).find("Media");
    var mediaType = $(mediaObj).attr("Type");
    if ((imageMode != undefined && imageMode != null) || (mediaType != undefined && mediaType != null && mediaType == "Illustration")) {
        if (imageMode == "Preseter") {
            if ($("#contentElement").hasClass("clearmode")) {
                $("#contentElement").removeClass("clearmode");
            } else if ($("#contentElement").hasClass("illustrationmode")) {
                $("#contentElement").removeClass("illustrationmode")
            } else if ($("#contentElement").addClass("fullscreenmode")) {
                $("#contentElement").removeClass("fullscreenmode")
            }
        } else if (imageMode == "Illustration" || mediaType == "Illustration") {
            $("#contentElement").addClass("illustrationmode");
            if ($("#contentElement").hasClass("clearmode")) {
                $("#contentElement").removeClass("clearmode");
            } else if ($("#contentElement").addClass("fullscreenmode")) {
                $("#contentElement").removeClass("fullscreenmode")
            }
        } else if (imageMode == "Fullscreen") {
            $("#contentElement").addClass("fullscreenmode");
            if ($("#contentElement").hasClass("clearmode")) {
                $("#contentElement").removeClass("clearmode");
            } else if ($("#contentElement").hasClass("illustrationmode")) {
                $("#contentElement").removeClass("illustrationmode")
            }
        }
    } else {
        $("#contentElement").addClass("clearmode");
    }
}
function loadPage(XMLobj) {

    isOverLoaded = false;
    //alert('page: '+ sequenceOrder + '.' + pageOrder);
    //send the MSG of the start of the loading page to backend;
    msgPageStart();
    if (isHelpOrReportButton) {
        relapseMsgStart();
        isHelpOrReportButton = false;
    }
    if (step <= 0) {
        goSubReturnTime = 1;
    }
    //if new sequence begain, send the start of new sequence MSG to backend
    var hiddenElementStr = '<div id="contentElement">' +
               '<div id="white" class="white" style="display:none;"></div>' +
			   '<div id="yellow" class="yellow" style="display:none;"></div>' +
		       '<div id="black" class="black" style="display:none;"></div>' +
			   '<div id="blue" class="blue" style="display:none;"></div>';
    $('#hiddenElements').empty().append($(hiddenElementStr));
    var hiddenImage = '<div id="hostPic"></div>';
    $("#hiddenImage").empty().append($(hiddenImage));
    var hiddenBgImg = '<div id="bgImg"></div>';
    $("#hiddenBg").empty().append($(hiddenBgImg));
    var actualImg = '<img id="actualImage" src="" alt="" />';
    $("#hiddenActualImage").empty().append($(actualImg));

    getSequenceGUID(XMLobj);
    if ($(pageObj).attr('Type') == 'SMS') {
        var userGUID = $(oraiRoot).attr('UserGUID');
        var programGUID = $(oraiRoot).attr('ProgramGUID');
        if ($(oraiRoot).find('Session').length > 0) {
            var sessionGUID = $(oraiRoot).find('Session').attr('GUID');
        } else {
            var sessionGUID = '';
        }
        var pageSequenceOrder = pageObj.parent().attr('Order');
        var pageGUID = pageObj.attr('GUID');
        var text = pageObj.attr('Text');
        var time = pageObj.attr('Time');
        var programVariableName = pageObj.attr("ProgramVariable");
        if (programVariableName != null && programVariableName != undefined && programVariableName != "") {
            var programVariableValue = getProgramVariableValue(programVariableName);
            if (programVariableValue != 0 && programVariableValue != undefined && programVariableValue != null && programVariableValue != "") {
                time = programVariableValue;
            }
        }
        var daysToSend = pageObj.attr('DaysToSend');
        if (daysToSend == undefined) {
            daysToSend = "";
        }
        var mobilePhone = $(oraiRoot).find('GeneralVariables').find('Variable').filter(function () { return $(this).attr('Name') == 'MobilePhone' }).attr('Value');
        var msg = '<XMLModel  UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageGUID="' + pageGUID + '"> <SMS Text="' + text + '" Time="' + time + '" Days="' + daysToSend + '" MobilePhone="' + mobilePhone + '"/> </XMLModel>';
        $.ajax({
            //url: getReturnDataServerUrl(),
            url: getURL('submit'),
            type: 'POST',
            processData: false,
            data: msg,
            dataType: "text"
        });
        //console.log( "sequenceGUIDBeforeGOSUBArray:" + sequenceGUIDBeforeGOSUBArray );
        var goSubStepArrLength = goSubStepArr.length;
        //if ($(oraiRoot).find("Session").length == 0 && pageInfoGUIDBeforeGOSUBArray.length > 0) {
        if($(oraiRoot).find("Session").length == 0 && goSubStepArrLength > 0){
            var pageLength = $(oraiRoot).find('Relapse').find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').length;
            if (pageOrder < pageLength) {
                goToNextDirectlly();
                var lastSequenceGuid = seqGuid;
                getSequenceGUID($(root));
                var currentSequenceGuid = seqGuid;
                if (typeof sequenceOrder == 'number') {
                    var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                } else {
                    var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                }
                if (nextPageObj.attr('BeforeExpression') && nextPageObj.attr('BeforeExpression') != '') {
                    excuteBeforeExpression(nextPageObj);
                    if (lastSequenceGuid != currentSequenceGuid) {
                        if (!sessionEndFlag) {
                            getProRoomNameBySeqGUID();
                            var lastProgramRoomName = programRoomName[lastSequenceGuid];
                            var currentProgramRoomName = programRoomName[currentSequenceGuid];
                            if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                                if (lastProgramRoomName != currentProgramRoomName) {
                                    roomTransitionAnimation();
                                } else {
                                    loadPage(XMLobj);
                                }
                            } else {
                                loadPage(XMLobj);
                            }
                        }
                    } else {
                        loadPage(XMLobj);
                    }
                } else {
                if (lastSequenceGuid != currentSequenceGuid) {
                    if (!sessionEndFlag) {
                        getProRoomNameBySeqGUID();
                        var lastProgramRoomName = programRoomName[lastSequenceGuid];
                        var currentProgramRoomName = programRoomName[currentSequenceGuid];
                        if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                            if (lastProgramRoomName != currentProgramRoomName) {
                                roomTransitionAnimation();
                            } else {
                                loadPage(XMLobj);
                            }
                        } else {
                            loadPage(XMLobj);
                        }
                    }
                } else {
                    loadPage(XMLobj);
                }
                }
            } else {
             //   if (goSubReturnTime >= 2) {
             var goSubStepArrLength=goSubStepArr.length;
             if(goSubStepArrLength>0){
                    goToNextDirectlly();
                    var lastSequenceGuid = seqGuid;
                    getSequenceGUID($(root));
                    var currentSequenceGuid = seqGuid;
                    if (typeof sequenceOrder == 'number') {
                        var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                    } else {
                        var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
                    }
                    if (nextPageObj.attr('BeforeExpression') && nextPageObj.attr('BeforeExpression') != '') {
                        excuteBeforeExpression(nextPageObj);
                        if (lastSequenceGuid != currentSequenceGuid) {
                            if (!sessionEndFlag) {
                                getProRoomNameBySeqGUID();
                                var lastProgramRoomName = programRoomName[lastSequenceGuid];
                                var currentProgramRoomName = programRoomName[currentSequenceGuid];
                                if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                                    if (lastProgramRoomName != currentProgramRoomName) {
                                        roomTransitionAnimation();
                                    } else {
                                        loadPage(XMLobj);
                                    }
                                } else {
                                    loadPage(XMLobj);
                                }
                            }
                        } else {
                            loadPage(XMLobj);
                        }
                    } else {
                    if (lastSequenceGuid != currentSequenceGuid) {
                        if (!sessionEndFlag) {
                            getProRoomNameBySeqGUID();
                            var lastProgramRoomName = programRoomName[lastSequenceGuid];
                            var currentProgramRoomName = programRoomName[currentSequenceGuid];
                            if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                                if (lastProgramRoomName != currentProgramRoomName) {
                                    roomTransitionAnimation();
                                } else {
                                    loadPage(XMLobj);
                                }
                            } else {
                                loadPage(XMLobj);
                            }
                        }
                    } else {
                        loadPage(XMLobj);
                    }
                    }
                } else {
                    quesFlag = false;
                    sessionEndFlag = true;
                    directllyGo = false;
                    relapseMsgEnd();
                }
            }
        } else {
            goToNextDirectlly();
            var lastSequenceGuid = seqGuid;
            getSequenceGUID($(root));
            var currentSequenceGuid = seqGuid;
            if (typeof sequenceOrder == 'number') {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return parseInt($(this).attr('Order')) == sequenceOrder })
					.find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
            } else {
                var nextPageObj = $(oraiRoot).find('PageSequence').filter(function () { return $(this).attr('GUID') == seqGuid }).find('Page').filter(function () { return parseInt($(this).attr('Order')) == pageOrder });
            }
            if (nextPageObj.attr('BeforeExpression') && nextPageObj.attr('BeforeExpression') != '') {
                excuteBeforeExpression(nextPageObj);
                if (lastSequenceGuid != currentSequenceGuid) {
                    if (!sessionEndFlag) {
                        getProRoomNameBySeqGUID();
                        var lastProgramRoomName = programRoomName[lastSequenceGuid];
                        var currentProgramRoomName = programRoomName[currentSequenceGuid];
                        if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                            if (lastProgramRoomName != currentProgramRoomName) {
                                roomTransitionAnimation();
                            } else {
                                loadPage(XMLobj);
                            }
                        } else {
                            loadPage(XMLobj);
                        }
                    }
                } else {
                    loadPage(XMLobj);
                }
              //  loadPage(XMLobj);
            } else {
            if (lastSequenceGuid != currentSequenceGuid) {
                if (!sessionEndFlag) {
                    getProRoomNameBySeqGUID();
                    var lastProgramRoomName = programRoomName[lastSequenceGuid];
                    var currentProgramRoomName = programRoomName[currentSequenceGuid];
                    if (typeof lastProgramRoomName == 'string' || typeof currentProgramRoomName == 'string') {
                        if (lastProgramRoomName != currentProgramRoomName) {
                            roomTransitionAnimation();
                        } else {
                            loadPage(XMLobj);
                        }
                    } else {
                        loadPage(XMLobj);
                    }
                }
            } else {
                loadPage(XMLobj);
            }
               // loadPage(XMLobj);
            }
          //  loadPage(XMLobj);
        }
        /*goToNextDirectlly();
        loadPage( XMLobj );*/

    }else {

        if (!isBackButton) {
            step += 1;
            // backPageArray[step] = { 'sequenceGUID': seqGuid, "pageGUID": pageObj.attr( 'GUID' ) };
            // console.log( "backPageArraySequenceGUID:" + backPageArray[step].sequenceGUID );
            // console.log( "backPageArrayPageGUID:" + backPageArray[step].pageGUID );
            stepHistory[step] = pageObj.attr('GUID');
            // console.log( "step:" + step );
            // console.log( "stepHistory:" + stepHistory );
        }
        // Add back button function
        var reginsInfo = $('#bubble-yellow .row').length;
        $('#controlBar .button-back').unbind('click');
        $('#controlBar .button-back').click(function (e) {
            e.preventDefault();
            $("#iframe-settings").hide();
            $(".settings-detail").hide();
            if (isBackFromPreviewEndPage && isPreviewMode) {
                $('#bubble-blue').unbind("click")
                $('#bubble-blue').bind("click", function (e) {
                    e.preventDefault();
                    bubbleBlueNextLoadPage(reginsInfo);
                });
                $('#bubble-blue').css("cursor", "pointer");
                isBackFromPreviewEndPage = false;
            }
            isBackButton = true;
            if (step <= 0) {
                goSubReturnTime = 1;
                excuteBefore = true;
            }
            if (step > 0) {
                root = oraiRoot;
                excuteBefore = true;
                var currentSeqGuid = $(root).find('Page').filter(function () { return $(this).attr('GUID') == stepHistory[stepHistory.length - 1] }).parent().attr('GUID');
                var nextSeqGuid = $(root).find('Page').filter(function () { return $(this).attr('GUID') == stepHistory[stepHistory.length - 2] }).parent().attr('GUID');
                //console.log( "nextSeqGuid:" + nextSeqGuid );
                //console.log( "currentSeqGuid:" + currentSeqGuid );
                // console.log( "sequenceGUIDBeforeGOSUBArray:" + pageInfoGUIDBeforeGOSUBArray );

                /*   var pageInfoGUIDBeforeGOSUBArrayLength = pageInfoGUIDBeforeGOSUBArray.length;
                if (pageInfoGUIDBeforeGOSUBArrayLength > 0) {
                if (nextSeqGuid == pageInfoGUIDBeforeGOSUBArray[pageInfoGUIDBeforeGOSUBArrayLength - 1].seqGuidBeforeGoSub) {
                pageInfoGUIDBeforeGOSUBArray.pop();
                goSubReturnTime -= 1;

                }
                }*/
                seqGuid = nextSeqGuid;
                stepHistory.pop();
                step -= 1;
                pageObj = $(root).find('Page').filter(function () { return $(this).attr('GUID') == stepHistory[step] });
                pageOrder = parseInt(pageObj.attr('Order'));
                sequenceOrder = parseInt(pageObj.parent().attr('Order'));
                //step -= 1;
                //loadPage($(root));
                var pageType = pageObj.attr("Type");
                getSequenceGUID($(root));
                if (nextSeqGuid == '') {
                } else if (currentSeqGuid != nextSeqGuid) {
                    getProRoomNameBySeqGUID();
                    var programRoomNameBeforeParse = programRoomName[nextSeqGuid];
                    var ProgramRoomNameCurrent = programRoomName[currentSeqGuid];
                    if ((programRoomNameBeforeParse != undefined && programRoomNameBeforeParse != null && programRoomNameBeforeParse != '') || (ProgramRoomNameCurrent != undefined && ProgramRoomNameCurrent != null && ProgramRoomNameCurrent != '')) {
                        if (programRoomNameBeforeParse !== ProgramRoomNameCurrent) {
                            roomTransitionAnimation();
                            // setTimeout(hiddenRoomInfo,1000);
                        } else {
                            loadPage($(root));
                            var pageType = pageObj.attr("Type");
                            if (pageType == "Get information") {
                                saveAnswers();
                            }
                            if (pageType == "Choose preferences") {
                                var pageGUID = pageObj.attr("GUID");
                                var answer = historyAnswers[pageGUID];
                                if (answer != null && answer != undefined && answer != null) {
                                    saveAnswersForChoosePreferences(answer);
                                }
                            }
                        }
                    } else {
                        loadPage($(root));
                        var pageType = pageObj.attr("Type");
                        if (pageType == "Get information") {
                            saveAnswers();
                        }
                        if (pageType == "Choose preferences") {
                            var pageGUID = pageObj.attr("GUID");
                            var answer = historyAnswers[pageGUID];
                            if (answer != null && answer != undefined && answer != null) {
                                saveAnswersForChoosePreferences(answer);
                            }
                        }

                    }

                } else {
                    loadPage($(root));
                    var pageType = pageObj.attr("Type");
                    if (pageType == "Get information") {
                        saveAnswers();
                    }
                    if (pageType == "Choose preferences") {
                        var pageGUID = pageObj.attr("GUID");
                        var answer = historyAnswers[pageGUID];
                        saveAnswersForChoosePreferences(answer);
                    }
                }
                //loadPage($(root));


            }
        });

        //loadpageSeqData;
        loadPageSeqCategory(pageSeqObj);
        //getItemInfoByItemGUID(pageObj);
        //loadPageData
        loadPageData(pageObj);
        var presenterImage = pageObj.attr("PresenterImage");
        //var presenterImageUrl = "http://changetechstorage.blob.core.windows.net/originalimagecontainer/" + presenterImage;
        var presenterImageUrl = getURL("originalimagecontainerRoot") + presenterImage;
        gotoPage(true);
        /*var bubHeight = $("#bubble-white .bubcontent").outerHeight(true);
       var whiteHeight = $("#white").outerHeight(true);
        var height = bubHeight;
        if (whiteHeight > height) {
            height = whiteHeight;
        }
        $("#bubble-white").animate({ 'height': height + "px" },500);
        */
        getUserAnswers(pageObj);
        // $("#pic img#backgroundpic").css('width', 'auto');
        directllyGo = true;
        if ($("#hostpic").text() == '') {
            $(".wrapper-image").hide();
        } else {
            // $(".wrapper-image").delay(100).fadeOut(1200);

            var myImage = $('#myImage');
            $(".wrapper-image").css({display:"block"});
            $(".wrapper-image").imagesLoaded(function (myImage, myImage, myImage) {
                if ($(".wrapper-image img.imageMode").length > 0) {
                    /* $(".wrapper-image img.imageMode").fadeIn(500);*/
                    $(".wrapper-image img.imageMode").show();
                } else if ($(".wrapper-image .fullscreenimage").length > 0) {
                    //$(".wrapper-image .fullscreenimage").fadeIn(500);
                    $(".wrapper-image img.imageMode").show();
                }
            });
        }
        //onBlendTransparentImage(presenterImage);
       /* if ($("#bgImg").text() != '') {
            $("#fullbgImg").hide();
            setTimeout(backgrondImageFadeIn, 100);
        }*/
    }

    // start added for retake by XMX on 2011/12/01
    disabledElementAndSetAnswersForRetakeDay();
    // end added for retake by XMX on 2011/12/01
    if ($("#bubble-yellow .loginEmail")) {
        setTimeout(function () { $("#bubble-yellow .loginEmail").focus(); }, 501);
    }
    if (step < 1) {
        $("#controlBar .button-back").hide();
    } else {
        $("#controlBar .button-back").show(200);
    }
    if (isGraphTemplate) {
        /*$("#bubbles").css("width", "800px");*/
        $("#bubble-white").css("display", "none");
        $("#graphcontainer").css("display", "block");
        $("#pic").css("display", "none");
        $(".mask").css("display", "block");
    } else {
        if ($("#white").html() != '') {
            $("#bubble-white").css("display", "block");
        }
        $("#graphcontainer").css("display", "none");
        if ($('#hiddenImage').text() != '') {
            // $(".wrapper-image").delay(100).fadeIn(1200);
            var myImage = $('#myImage');
            $(".wrapper-image").css({ display: "block" });
            $(".wrapper-image").imagesLoaded(function (myImage, myImage, myImage) {
                if ($(".wrapper-image img.imageMode").length > 0) {
                    // $(".wrapper-image img.imageMode").fadeIn(500);
                    $(".wrapper-image img.imageMode").show();
                } else if ($(".wrapper-image .fullscreenimage").length > 0) {
                    // $(".wrapper-image .fullscreenimage").fadeIn(500);
                    $(".wrapper-image .fullscreenimage").show();
                }
            });
               

        } else {
            //            $(".wrapper-image").delay(100).fadeOut(500);
            $(".wrapper-image").hide();
        }
        $(".mask").css("display", "none");
    }
    window.scrollTo(0, 0);
}
function loadDataForPushPictures(pageObj) {
        var interval = parseInt(pageObj.attr("Interval")) * 1000;
        goToNextDirectlly();
        setTimeout("loadPage($(root))", interval);
        showImage(); 
}
function saveAnswers() {
    var questionsObj = pageObj.find("Questions");
    if (questionsObj != '' && questionsObj != null && questionsObj != undefined) {
        var quesObj = questionsObj.find("Question");
        if (quesObj != null && quesObj != '' && quesObj != undefined) {
            $(quesObj).each(function (quesIndex, ques) {
                var quesGUID = $(ques).attr("GUID");
                var answer = historyAnswers[quesGUID];
                if (answer != "" && answer != undefined && answer != null) {
                    //setTimeout(function(){$("#bubble-yellow").animate({height:historyAnswersObj[step]},500)},100);
                   // setHeight();
                    saveAnswerForBackBtn(ques, quesIndex);
                }
            });
        }
    }
}
function setHeight() {
    var pageGUID = pageObj.attr("GUID");
    var bubbleYellowHeight = historyAnswersObj[pageGUID];
    $("#bubble-yellow").animate({ height: bubbleYellowHeight }, 500);
}
function backgrondImageFadeIn() {
    $("#fullbgImg").fadeIn('slow');
}
function getUserAnswers(pageObj) {
    var pageType = pageObj.attr("Type");
    if (pageType == "Get information") {
        //if(retakeValue != "true" && retakeValue != "1"){
        onClickRadioButton(pageObj);
        onClickForCheckbox(pageObj);
        onClickForDropdownList(pageObj);
        onClickForSlider(pageObj);
        onClickForNumeric(pageObj);
        onClickForTimePicker(pageObj);
        //}
    }
    if (pageType == "Choose preferences") {
        choosePreferences();
    }
    if (pageType == "Timer") {
        localStopWatch();
    }
    if (pageType == "Standard") {
        reloadMedia(pageObj);
        if (pageObj.find("Media").attr("Type") == "Video") {
            $("#bubble-black").animate({ height: "240px" });
        } else if (pageObj.find("Media").attr("Type") == "Audio") {
//        if (navigator.userAgent.match(/iPhone|iPad|Android/i)) {
//            $("#bubble-black").animate({ height: "284px" });
//        } else {
//            $("#bubble-black").animate({ height: "29px" });
            //        }
            $("#bubble-black").animate({ height: "30px" });
        }

    }
    onClickForPasswordReminder(pageObj);
    donnotRegister();
    getInputData(pageObj);
    onClickForPinCodeReminder(pageObj);
}
function loadPageData(pageObj) {
    var pageType = pageObj.attr("Type");
    loadMainDataInBubble(pageObj);
}
//
// move getinformation type from here (here is the content in getInformationTemplate.js)
//

//
// move Login and AccountCreation and PasswordReminder from here to login-AccountCreation.js
//
function loadMainDataInBubble(pageObj) {
    var pageType = pageObj.attr("Type");
    //var bubbleYellowStr='<div id="bubble-yellow"><form><div class="bubcontent"></div><div class="bubble-yellow-arrow"></div></form></div>';
    /*loadBackgroundImage(pageObj);
    loadPresenterImage(pageObj);*/
    loadImages(pageObj);
    loadButtonPrimary(pageObj);
    if ((pageType == "Login") || (pageType == "Password reminder") || pageType == "Account creation" || pageType=="PinCode") {
        loadTitleAndTextForOtherTemplate(pageObj);

    } else {
        loadTitleAndText(pageObj);

    }
    if (pageType == "Standard") {
        isGraphTemplate = false;
        loadMediaData(pageObj);
    }
    if (pageType == "Get information") {
        isGraphTemplate = false;
        loadDataForGetInformationTemplate(pageObj);
    }
    if (pageType == "Login" || pageType == "Password reminder") {
        isGraphTemplate = false;
        loadDataForLogin(pageObj);
    }
    if (pageType == "PinCode") {
        isGraphTemplate = false;
        loadDataForPinCode(pageObj);
    }
    if (pageObj.attr("Type") == "Account creation") {
        isGraphTemplate = false;
        loadAccountCreation(pageObj);
    }
    if (pageObj.attr("Type") == "Timer") {
        isGraphTemplate = false;
        loadDataForTimerTemplate(pageObj);
    }
    if (pageObj.attr("Type") == "Choose preferences") {
        isGraphTemplate = false;
        loadDataForChoosePreferenceTemplate(pageObj);
    }
    if (pageObj.attr("Type") == "Screening results") {
        isGraphTemplate = false;
        loadDataForResultTemplate(pageObj);
    }
    if (pageObj.attr("Type") == "Graph") {
        isGraphTemplate = true;
        //loadDataForGraphTemplate(pageObj);
        var isBeforePageStart = true;
        loadDataForGraphTemplate(isBeforePageStart);
    }
    if (pageObj.attr("Type") == "Push pictures") {
        loadDataForPushPictures(pageObj);
    }
}

// 
// move LoadTitleAndTextForOtherTemplate() and loadTitleAndText(pageObj) to standardTemplate.js
//

function loadPageSeqCategory(pageSeqObj) {
    /// PageSequence CategoryName property
    var categoryName = pageSeqObj.attr("CategoryName");
    var categoryDescription = pageSeqObj.attr("CategoryDescription");
}

function loadButtonPrimary(pageObj) {
    /// buttonPrimaryName Property
    var buttonPrimaryName = pageObj.attr("ButtonPrimaryName");
    if (buttonPrimaryName != null && buttonPrimaryName != '' && buttonPrimaryName != undefined) {
        $('#blue').html('<p>' + buttonPrimaryName + '</p>');
    }
}
function loadImages(pageObj) {
    var imageMode = $(pageObj).attr("ImageMode");
    var presenterImage = $(pageObj).attr("PresenterImage");
    var illustrationImage = $(pageObj).attr("IllustrationImage");
    var backgroundImage = $(pageObj).attr("BackgroundImage");
    var mediaObj = $(pageObj).find("Media");
    var mediaType = $(mediaObj).attr("Type");
    var imageName = "";
    if (presenterImage != undefined && presenterImage != null && presenterImage != "") {
        imageName = $(pageObj).attr("PresenterImage");
    } else if (backgroundImage != null && backgroundImage != undefined && backgroundImage != "") {
        imageName = $(pageObj).attr("BackgroundImage");
    } else if (illustrationImage != null && illustrationImage != "" && illustrationImage != undefined) {
        imageName = $(pageObj).attr("IllustrationImage");
    } else if (mediaType!=undefined && mediaType!=null && mediaType == "Illustration") {
        imageName = $(mediaObj).attr("Media");
    }
    if (imageName != "") {
        var imageUrl = '../RequestResource.aspx?target=Image&media=' + imageName + '';
        $("#hostPic").html(imageUrl);
        $('#actualImage').attr("src", imageUrl);
    } else {
        $("#hostPic").empty();
    }
    //console.log(document.getElementById("actualImage").complete+"loadImage");
}
function loadPresenterImage(pageObj) {
    /// presenterImage Property
    var presenterImage = pageObj.attr("PresenterImage");
    var presenterMode = pageObj.attr("PresenterMode");
   
    var backgroundImage = pageObj.attr("BackgroundImage");
    if (presenterImage != null && presenterImage != '' && presenterImage != undefined) {
        // var presenterImageUrl = "http://changetechstorage.blob.core.windows.net/originalimagecontainer/" + presenterImage;
        var presenterImageUrl = '../RequestResource.aspx?target=Image&media=' + presenterImage + '';
        $('#hostPic').html(presenterImageUrl);
        $('#actualImage').attr("src", presenterImageUrl);
    } else {
        $("#hostPic").empty();
    }
}
function loadBackgroundImage(pageObj) {
    /// BackgroundImage Property
    var backgroundImage = pageObj.attr("BackgroundImage");
    if (backgroundImage != null && backgroundImage != undefined && backgroundImage != '') {
        //var backgroundImageUrl = "http://changetechstorage.blob.core.windows.net/originalimagecontainer/" + backgroundImage;
        var backgroundImageUrl = getURL("originalimagecontainerRoot") + backgroundImage;
        $('#bgImg').html(backgroundImageUrl);
    } else {
        $('#bgImg').empty();
    }
}
//
// move loadMediaData to standardTemplate.js from here 
//
function show() {
    $('.body-wrapper').html('<p><h1>Load XML successfully!</h1></p>').delay(500).fadeOut();
}
function showSettings() {
    $(".button-settings").click(function (e) {
        e.preventDefault();
        $('#controlBar,.wrapper-image,.wrapper-bubbles,#graphcontainer').hide();
        var settingStr = specialStirngs["SettingMenu"];
        if (settingStr != null && settingStr != undefined) {
            $("#iframe-settings h1").html(settingStr);
        }
        $("body").addClass("settings-body");
        $("#iframe-settings").fadeIn('slow');
        $(".wrapper-settings").fadeIn('slow');
        $(".settings-button-close").fadeIn('fast');
        // $(".settings-button-close").show();

    });
}
function showDialog() {
    $('#iframe-settings .settingsmenu').html("");
    $('.settings-detail .settings').html("");
    var hasHelpFunction = false;
    var hasTipFriendFunction = false;
    var hasPauseProgramFunction = false;
    var hasProfileFunction = false;
    var hasExitProgramFunction = false;
    var hasTimeZoneFunction = false;
    var hasSMSToEmailFunction = false;
    var functionName = "";
    var name = "";
    //prepare for setting menu
    var settingMenu = $(oraiRoot).find("SettingMenu");
    if (settingMenu != null && settingMenu != undefined) {
        $(settingMenu).find("MenuItem").each(function (index, settingMenuItem) {
            if ($(settingMenuItem).attr("FunctionName") != null && $(settingMenuItem).attr("FunctionName") != undefined) {
                functionName = $(settingMenuItem).attr("FunctionName");
            } else {
                functionName = "";
            }
            if ($(settingMenuItem).attr("Name") != null && $(settingMenuItem).attr("Name") != undefined) {
                name = $(settingMenuItem).attr("Name");
            } else {
                name = "";
            }
            var settingsMenu = $(window.frames["settings"]).find(".settingsmenu");
            if (functionName == "HelpFunction") {
                if (name != null && name != undefined && name != '') {
                    //$('.setting-menu').append('<span><a href="#x" class="help">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="help"><li>' + name + '</li></a>');
                } else {
                    // $('.setting-menu').append('<span><a href="#x" class="help">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="help"><li>' + functionName + '</li></a>');
                }
                hasHelpFunction = true;
            }
            if (functionName == "TipFriendFunction") {
                if (name != null && name != undefined && name != '') {
                    //$('.setting-menu').append('<span><a href="#x" class="mail">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="mail"><li>' + name + '</li></a></span>');
                } else {
                    // $('.setting-menu').append('<span><a href="#x" class="mail">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();"  class="mail"><li>' + functionName + '</li></a></span>');
                }
                hasTipFriendFunction = true;
            }

            if (functionName == "PauseProgramFunction") {
                if (name != null && name != undefined && name != '') {
                    //$('.setting-menu').append('<span><a href="#x" class="pause">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="pause"><li>' + name + '</li></a>');
                } else {
                    //$('.setting-menu').append('<span><a href="#x" class="pause">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="pause"><li>' + functionName + '</li></a>');
                }
                hasPauseProgramFunction = true;
            }

            if (functionName == "ProfileFunction") {
                if (name != null && name != undefined && name != '') {
                    //$('.setting-menu').append('<span><a href="#x" class="email">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="email"><li>' + name + '</li></a>');
                } else {
                    // $('.setting-menu').append('<span><a href="#x" class="email">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="email"><li>' + functionName + '</li></a>');
                }
                hasProfileFunction = true;
            }

            if (functionName == "ExitProgramFunction") {
                if (name != null && name != undefined && name != '') {
                    //$('.setting-menu').append('<span><a href="#x" class="shutdown">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="shutdown"><li>' + name + '</li></a>');
                } else {
                    //$('.setting-menu').append('<span><a href="#x" class="shutdown">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="shutdown" ><li>' + functionName + '</li></a>');
                }
                hasExitProgramFunction = true;
            }
            var isSupportTimeZone = $(oraiRoot).attr("IsSupportTimeZone");
            if (isSupportTimeZone == "1" || isSupportTimeZone == "true") {
                if (functionName == "TimeZoneFunction") {
                    if (name != null && name != undefined && name != '') {
                        //$('.setting-menu').append('<span><a href="#x" class="timeZone">' + name + '</a></span>');
                        $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="timeZone"><li>' + name + '</li></a>');
                    } else {
                        //$('.setting-menu').append('<span><a href="settings-timezone.html">' + functionName + '</a></span>');
                        $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="timeZone"><li>' + functionName + '</li></a>');
                    }
                    hasTimeZoneFunction = true;
                }
            }

            var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
            /*  if (isSMSToEmail == "1" || isSMSToEmail == "true") {*/
            if (functionName == "SMSToEmailFunction") {
                if (name != null && name != undefined && name != "") {
                    // $('.setting-menu').append('<span><a href="#x" class="smsToEmail">' + name + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="smsToEmail"><li>' + name + '</li></a></span>');
                } else {
                    // $('.setting-menu').append('<span><a href="#x" class="smsToEmail">' + functionName + '</a></span>');
                    $('.settingsmenu').append('<a href="" onfocus="this.blur();" class="smsToEmail"><li>' + functionName + '</li></a></span>');
                }
                hasSMSToEmailFunction = true;
            }
            /* }*/
        });
    }
        //show settings menu
    $(".hideSettingsmenu").click(function (e) {
        e.preventDefault();
        $(".wrapper-settings").fadeOut('fast');
        $("#iframe-settings").fadeOut('slow');
        $(".settings-button-close").fadeOut('fast');
        $(".settings-detail").hide();
        //  $("#iframe-settings,.settings-detail,.settings-button-close").fadeOut('fast');
        $('#controlBar').slideDown();
        if ($(".wrapper-image").html() == "") {
            $(".wrapper-image").hide();
        } else {
            var myImage = $('#myImage');
            $(".wrapper-image").css({ display: "block" });
            $(".wrapper-image").imagesLoaded(function (myImage, myImage, myImage) {
                if ($(".wrapper-image img.imageMode").length > 0) {
                    // $(".wrapper-image img.imageMode").fadeIn();
                    $(".wrapper-image img.imageMode").show();
                } else if ($(".wrapper-image .fullscreenimage").length > 0) {
                    // $(".wrapper-image .fullscreenimage").fadeIn();
                    $(".wrapper-image .fullscreenimage").show();
                }
            });
        }
        if (isGraphTemplate) {
            $("#graphcontainer").css({ display: "block" });
        } else {
            $("#graphcontainer").css({ display: "none" });
        }
         $(".wrapper-bubbles").fadeIn();
        if ($("body").hasClass("settings-body")) {
            $("body").removeClass("settings-body");
        }
        // $("body").css({ "background": "#D6DDE3", "line-height": "1" });
        // $(".bottomgradient").css("opacity", "1");
        // showSettings();

    });
    showSettings();
    $(".settings-detail .settings .cancel").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
    //close settings menu
   /*$('.setting-menu em a').unbind();
    $('.setting-menu em a').bind("click",function () {
        $('.setting-menu').fadeOut(500);
        $('.setting-menu-wrapper').delay(500).fadeOut(500);
        $('#controlBar').delay(1000).slideDown();
        $('#bubbles').delay(1000).fadeIn(500);
    });*/
    //list datails
    function settingsList() {
        var newEmailAddress = "";
        if (specialStirngs["New_email_address"] != null && specialStirngs["New_email_address"] != undefined && specialStirngs["New_email_address"] != "") {
            newEmailAddress = specialStirngs["New_email_address"];
        }
        $(".settings-detail input[name = 'e-mail']").focus(function () {
            if ($(this).val() == newEmailAddress) {
                $(this).val('');
            }
        });
        $(".settings-detail input[name = 'e-mail']").blur(function () {
            if ($(this).val() == '') {
                $(this).val(newEmailAddress);
            }
        });
    }
    var userguid = $(oraiRoot).attr('UserGUID');
    var programguid = $(oraiRoot).attr('ProgramGUID');
    //FAQ button
    if (hasHelpFunction) {
        $('.settingsmenu a.help').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var helpObj = $(oraiRoot).find("Help");
            if (helpObj != null && helpObj != undefined) {
                var helpTitle = "";
                var backBtnName = "";
                if ($(helpObj).attr("Title") != null && $(helpObj).attr("Title") != undefined) {
                    helpTitle = $(helpObj).attr("Title");
                }
                if ($(helpObj).attr("BackButtonName") != null && $(helpObj).attr("BackButtonName") != undefined) {
                    backBtnName = $(helpObj).attr("BackButtonName");
                } else {
                    backBtnName = "Cancel";
                }
                //$('.setting-detail').append('<h2>' + helpTitle + '</h2> <div class="setting-list-box"></div>');
                $('.settings-detail .settings').append('<div class="col-1full"> <p class="spacefiller">&nbsp;</p><h1>' + helpTitle + '</h1></div><div class="help-list"></div>');
                $(helpObj).find('HelpItem').each(function (i, helpItem) {
                    var helpItemTitle = $(helpItem).attr("Title");
                    var helpItemText = $(helpItem).attr("Text")
                    //$('.setting-list-box').append('<dl><dt>' + helpItemTitle + '<dt><dd>' + helpItemText + '<dd></dl>');
                    $('.help-list').append('<div class="col-1third"><h3>' + helpItemTitle + '</h3></div><div class="col-2third last"><p>' + helpItemText + '</p></div><div class="clear"></div>');
                });
                $('.help-list').append('<p class="spacefiller">&nbsp;</p>');
            }
            //settingsList();
            // $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
        });
    }
    //TipFriend button
    if (hasTipFriendFunction) {
        $('.settingsmenu a.mail').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var newEmailAddress = "";
            if (specialStirngs["New_email_address"] != null && specialStirngs["New_email_address"] != undefined && specialStirngs["New_email_address"] != "") {
                newEmailAddress = specialStirngs["New_email_address"];
            }

            var tipFriendObj = $(oraiRoot).find("TipFriend");
            if (tipFriendObj != null && tipFriendObj != undefined) {
                var tipFriendTitle = "";
                var tipFriendText = "";
                var tipFriendBackBtnName = "";
                var tipFriendSubmitBtnName = "";
                if ($(tipFriendObj).attr("Title") != null && $(tipFriendObj).attr("Title") != undefined) {
                    tipFriendTitle = $(tipFriendObj).attr("Title");
                }
                if ($(tipFriendObj).attr("Text") != null && $(tipFriendObj).attr("Text") != undefined) {
                    tipFriendText = $(tipFriendObj).attr("Text");
                }
                if ($(tipFriendObj).attr("BackButtonName") != undefined && $(tipFriendObj).attr("BackButtonName") != null) {
                    tipFriendBackBtnName = $(tipFriendObj).attr("BackButtonName");
                } else {
                    tipFriendBackBtnName = "Cancel";
                }
                if ($(tipFriendObj).attr("SubmitButtonName") != null && $(tipFriendObj).attr("SubmitButtonName") != undefined) {
                    tipFriendSubmitBtnName = $(tipFriendObj).attr("SubmitButtonName");
                } else {
                    tipFriendSubmitBtnName = "OK";
                }
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + tipFriendTitle + '</h1> <p>&nbsp;</p><p>' + tipFriendText + '</p><p class="emptyline">&nbsp;</p><form><input type="text" name="e-mail"  class="textfield-default seventyfivepercent" id="textfield" value="' + newEmailAddress + '" /></form><p class="spacefiller">&nbsp;</p></div>');
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink grey cancel" >' + tipFriendBackBtnName + '</a><a href="" onfocus="this.blur();" class="actionlink blue">' + tipFriendSubmitBtnName + '</a></div>');

                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    var inputEmailValue = $(".settings-detail .settings input[name = 'e-mail']").val();
                    if (checkEmailFormat(inputEmailValue)) {
                        var msg = '<TipFriend Invite="' + inputEmailValue + '" UserGUID="' + userguid + '" ProgramGUID="' + programguid + '"/>';
                        $.ajax({
                            //url: getReturnDataServerUrl(),
                            url: getURL('submit'),
                            type: 'POST',
                            processData: false,
                            data: msg,
                            dataType: "text",
                            success: function (data) {
                                showErrorSettingMessage(tipFriendTitle, data, tipFriendSubmitBtnName);
                            }
                        });
                    } else {
                        var ms = tipMessage["InvalidEmail"];
                        if (ms != null && ms != undefined) {
                            var message = ms.Message;
                            showErrorSettingMessage(tipFriendTitle, message, tipFriendSubmitBtnName);
                        }
                        $(".settings-detail input[name = 'e-mail']").val(newEmailAddress);
                    }

                });
            }
            settingsList();
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();

            });
        });
    }
    //ProgramStatus button
    if (hasPauseProgramFunction) {
        $('.settingsmenu a.pause').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var pauseProgramObj = $(oraiRoot).find('ProgramStatus');
            if (pauseProgramObj != null && pauseProgramObj != undefined) {
                var pauseProTitle = "";
                var pauseProText = "";
                var pauseProBackbtnName = "";
                var pauseProSubmitBtnName = "";
                if ($(pauseProgramObj).attr('Title') != null && $(pauseProgramObj).attr('Title') != undefined) {
                    pauseProTitle = $(pauseProgramObj).attr('Title');
                }
                if ($(pauseProgramObj).attr('Text') != null && $(pauseProgramObj).attr('Text') != undefined) {
                    pauseProText = $(pauseProgramObj).attr('Text');

                }
                var weekStr = "week";
                if (specialStirngs["Week"] != null && specialStirngs["Week"] != undefined) {
                    weekStr = specialStirngs["Week"];
                }

                if ($(pauseProgramObj).attr("BackButtonName") != null && $(pauseProgramObj).attr("BackButtonName") != undefined) {
                    pauseProBackbtnName = $(pauseProgramObj).attr("BackButtonName");
                } else {
                    pauseProBackbtnName = "Cancel";
                }
                if ($(pauseProgramObj).attr("SubmitButtonName") != null && $(pauseProgramObj).attr("SubmitButtonName") != undefined) {
                    pauseProSubmitBtnName = $(pauseProgramObj).attr("SubmitButtonName");
                } else {
                    pauseProSubmitBtnName = "OK";
                }
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + pauseProTitle + '</h1><p>&nbsp;</p><p>' + pauseProText + '</p><p class="emptyline">&nbsp;</p><p><select name="select" id="select"><option selected>1' + weekStr + '</option><option>2' + weekStr + '</option><option>3' + weekStr + '</option><option>4' + weekStr + '</option></select></p><p class="spacefiller">&nbsp;</p></div>');
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink grey cancel">' + pauseProBackbtnName + '</a><a href="" onfocus="this.blur();" class="actionlink blue">' + pauseProSubmitBtnName + '</a></div>');
                //settingsList();
                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    var week = parseInt($(".settings-detail .settings select[name = 'select']").get(0).selectedIndex) + 1;
                    var msg = '<ProgrameStatus Status="pause" Week="' + week + '" UserGUID="' + userguid + '" ProgramGUID="' + programguid + '"/>';
                    $.ajax({
                        //url: getReturnDataServerUrl(),
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            showCorrectSettingMessage(pauseProTitle, data, pauseProSubmitBtnName);
                        }
                    });
                });
            }
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();
            });
        });
    }

    //Profile button
    if (hasProfileFunction) {
        $('.settingsmenu a.email').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var newEmailAddress = "";
            if (specialStirngs["New_email_address"] != null && specialStirngs["New_email_address"] != undefined && specialStirngs["New_email_address"] != "") {
                newEmailAddress = specialStirngs["New_email_address"];
            }
            var profileObj = $(oraiRoot).find('Profile');
            if (profileObj != null && profileObj != undefined) {
                var profileTitle = "";
                var profileText = "";
                var profileBackBtnName = "";
                var profileSubmitBtnName = "";
                var profileOldEmailValue = "";
                if (profileObj.attr("Title") != null && profileObj.attr("Title") != undefined) {
                    profileTitle = profileObj.attr("Title");
                }
                if (profileObj.attr("Text") != null && profileObj.attr("Text") != undefined) {
                    profileText = profileObj.attr("Text");
                }
                if (profileObj.attr("BackButtonName") != null && profileObj.attr("BackButtonName") != undefined) {
                    profileBackBtnName = profileObj.attr("BackButtonName");
                } else {
                    profileBackBtnName = "Cancel";
                }
                if (profileObj.attr("SubmitButtonName") != null && profileObj.attr("SubmitButtonName") != undefined) {
                    profileSubmitBtnName = profileObj.attr("SubmitButtonName");
                } else {
                    profileSubmitBtnName = "OK";
                }
                var profileItem = profileObj.find('Item');
                if (profileItem != null && profileItem != undefined) {
                    if (profileItem.attr("OldValue") != null && profileItem.attr("OldValue") != undefined) {
                        profileOldEmailValue = profileItem.attr("OldValue");
                    }
                }
                $(".settings-detail .settings").append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + profileTitle + '</h1><p>&nbsp;</p><p>' + profileText + '</p><p>&nbsp;</p><h3>' + profileOldEmailValue + '</h3><p class="emptyline">&nbsp;</p><form><input type="text" name="e-mail" id="textfield" class="textfield-default seventyfivepercent" value="' + newEmailAddress + '"/></form><p class="spacefiller">&nbsp;</p></div>');
                $(".settings-detail .settings").append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink blue">' + profileSubmitBtnName + '</a></div>');
                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    var emailValue = $(".settings-detail .settings form input[name = 'e-mail']").val();
                    if (checkEmailFormat(emailValue)) {
                        var msg = '<ChangeProfile UserGUID="' + userguid + '" ProgramGUID="' + programguid + '"> <Items> <Item Name="Email" NewValue="' + emailValue + '"/> </Items> </ChangeProfile>';
                        $.ajax({
                            //url: getReturnDataServerUrl(),
                            url: getURL('submit'),
                            type: 'POST',
                            processData: false,
                            data: msg,
                            dataType: "text",
                            success: function (data) {
                                showCorrectSettingMessage(profileTitle, data, profileSubmitBtnName);
                            }
                        });
                    } else {
                        var text = "Dette er ikke en gyldig e-post adresse";
                        showErrorSettingMessage(profileTitle, text, profileSubmitBtnName);
                        $(".settings-detail .settings form input[name = 'e-mail']").val(newEmailAddress);
                    }
                });
            }
            settingsList();
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();
            });
        });
    }
    //ExitProgram button
    if (hasExitProgramFunction) {
        $('.settingsmenu a.shutdown').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var exitProgramObj = $(oraiRoot).find('ExitProgram');
            if (exitProgramObj != null && exitProgramObj != undefined) {
                var exitProgramTtile = "";
                var exitProgramText = "";
                var exitProgramBackBtnName = "";
                var exitProgramSubmitBtnName = "";
                if (exitProgramObj.attr("Title") != null && exitProgramObj.attr("Title") != undefined) {
                    exitProgramTitle = exitProgramObj.attr("Title");
                }
                if (exitProgramObj.attr("Text") != undefined && exitProgramObj.attr("Text") != null) {
                    exitProgramText = exitProgramObj.attr("Text");
                }
                if (exitProgramObj.attr("BackButtonName") != null && exitProgramObj.attr("BackButtonName") != undefined) {
                    exitProgramBackBtnName = exitProgramObj.attr("BackButtonName");
                } else {
                    exitProgramBackBtnName = "Cancel";
                }
                if (exitProgramObj.attr("SubmitButtonName") != null && exitProgramObj.attr("SubmitButtonName") != undefined) {
                    exitProgramSubmitBtnName = exitProgramObj.attr("SubmitButtonName");
                } else {
                    exitProgramSubmitBtnName = "OK";
                }
                $(".settings-detail .settings").append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + exitProgramTitle + '</h1><p>&nbsp;</p><p>' + exitProgramText + '</p><p class="spacefiller">&nbsp;</p></div>');
                $(".settings-detail .settings").append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink grey cancel">' + exitProgramBackBtnName + '</a><a href="" onfocus="this.blur();" class="actionlink blue">' + exitProgramSubmitBtnName + '</a> </div>');
                //settingsList();
                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    var msg = '<ProgrameStatus Status="exit" UserGUID="' + userguid + '" ProgramGUID="' + programguid + '"/>';
                    $.ajax({
                        //url: getReturnDataServerUrl(),
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            showCorrectSettingMessage(exitProgramTitle, data, exitProgramSubmitBtnName);
                        }
                    });
                });
            }
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();
            });
        });
    }
    if (hasTimeZoneFunction) {
        $('.settingsmenu a.timeZone').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var timeZoneObj = $(oraiRoot).find('TimeZone');
            var userTimeZone = $(oraiRoot).attr("UserTimeZone");
            var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
            var isSupportTimeZone = $(oraiRoot).attr("IsSupportTimeZone");
            if (timeZoneObj != null && timeZoneObj != undefined) {
                var timeZoneTitle = "";
                var timeZoneText = "";
                var timeZoneBackBtnName = "";
                var timeZoneSubmitBtnName = "";
                if (timeZoneObj.attr("Title") != null && timeZoneObj.attr("Title") != undefined) {
                    timeZoneTitle = timeZoneObj.attr("Title");
                }
                if (timeZoneObj.attr("Text") != null && timeZoneObj.attr("Text") != undefined) {
                    timeZoneText = timeZoneObj.attr("Text");
                }
                if (timeZoneObj.attr("BackButtonName") != null && timeZoneObj.attr("BackButtonName") != undefined) {
                    timeZoneBackBtnName = timeZoneObj.attr("BackButtonName");
                } else {
                    timeZoneBackBtnName = "Cancel";
                }
                if (timeZoneObj.attr("SubmitButtonName") != null && timeZoneObj.attr("SubmitButtonName") != undefined) {
                    timeZoneSubmitBtnName = timeZoneObj.attr("SubmitButtonName");
                } else {
                    timeZoneSubmitBtnName = "OK";
                }
                if (isSupportTimeZone == "1" || isSupportTimeZone == "true") {
                    var timeZoneObj = $(oraiRoot).find("TimeZoneList");
                    if (timeZoneObj != undefined && timeZoneObj != null) {
                        var selectObj = $(timeZoneObj).find("select");
                        if (selectObj != undefined && selectObj != null) {
                            var selectStr = "<select class='seventyfivepercent  accountCreationSelect' name='select' id='select'>" + $(timeZoneObj).find("select")[0].innerHTML + '</select>';
                            var timeZoneStr = selectStr;
                        }
                    }
                    //settingsList();
                }

                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + timeZoneTitle + '</h1><p>&nbsp;</p><p>' + timeZoneText + '</p><p class="emptyline">&nbsp;</p><p class="setting-timezone">' + timeZoneStr + '</p> <p class="spacefiller">&nbsp;</p></div>');
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink grey cancel">' + timeZoneBackBtnName + '</a><a href="" onfocus="this.blur();" class="actionlink blue">' + timeZoneSubmitBtnName + '</a></div>');
                $('.setting-timezone').find('select.accountCreationSelect').find("option:selected").removeAttr("selected");
                $('.setting-timezone').find('select.accountCreationSelect').find("option").each(function (i, option) {
                    var optionValue = $(option).val();
                    if (optionValue == userUpdateTimeZone) {
                        $(this).attr("selected", "selected");
                    }
                });
                var timeZoneValue = userUpdateTimeZone;
                var msg = '<TimeZone Value="' + timeZoneValue + '" UserGUID="' + userguid + '" ProgramGUID="' + programguid + '"/>';
                $("select.accountCreationSelect").change(function () {
                    timeZoneValue = $(this).val();
                    msg = '<TimeZone Value="' + timeZoneValue + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '"/>';
                });
                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    $.ajax({
                        //url: getReturnDataServerUrl(),
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            var returnCode = data.substring(0, 1);
                            var message = data.substr(2);
                            if (returnCode == "1") {
                                userUpdateTimeZone = timeZoneValue;
                                showCorrectSettingMessage(timeZoneTitle, message, timeZoneSubmitBtnName);
                            } else {
                                showCorrectSettingMessage(timeZoneTitle, message, timeZoneSubmitBtnName);
                            }
                        }
                    });
                });
            }
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();
            });
        });
    }

    if (hasSMSToEmailFunction) {
        $('.settingsmenu a.smsToEmail').click(function (e) {
            e.preventDefault();
            showSettingContent();
            var smsToEmailString = specialStirngs['SMS_To_Email'];
            if (smsToEmailString == undefined || smsToEmailString == null || smsToEmailString == "") {
                smsToEmailString = "SMS To Email";
            }
            var smsToEmailStr = "<p class='settingMenuSmsToEmail'><span><input type='checkbox' class='smsToEmailCheckBox' /><label class='smsToEmailLabel'>" + smsToEmailString + "</label></span></p>";
            var smsToEmailObj = $(oraiRoot).find('SmsToEmail');
            if (smsToEmailObj != null && smsToEmailObj != undefined) {
                var smsToEmailTitle = "";
                var smsToEmailText = "";
                var smsToEmailBackBtnName = "";
                var smsToEmailSubmitBtnName = "";
                var isSmsToEmail = false;
                if (smsToEmailObj.attr("Title") != null && smsToEmailObj.attr("Title") != undefined) {
                    smsToEmailTitle = smsToEmailObj.attr("Title");
                }
                if (smsToEmailObj.attr("Text") != null && smsToEmailObj.attr("Text") != undefined) {
                    smsToEmailText = smsToEmailObj.attr("Text");
                }
                if (smsToEmailObj.attr("BackButtonName") != null && smsToEmailObj.attr("BackButtonName") != undefined) {
                    smsToEmailBackBtnName = smsToEmailObj.attr("BackButtonName");
                } else {
                    smsToEmailBackBtnName = "Cancel";
                }
                if (smsToEmailObj.attr("SubmitButtonName") != null && smsToEmailObj.attr("SubmitButtonName") != undefined) {
                    smsToEmailSubmitBtnName = smsToEmailObj.attr("SubmitButtonName");
                } else {
                    smsToEmailSubmitBtnName = "OK";
                }
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + smsToEmailTitle + '</h1><p>&nbsp;</p><p>' + smsToEmailText + '</p> <p class="emptyline">&nbsp;</p><p>' + smsToEmailStr + '</p> <p class="spacefiller">&nbsp;</p></div>');
                $('.settings-detail .settings').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink grey cancel">' + smsToEmailBackBtnName + '</a><a href="" onfocus="this.blur();" class="actionlink blue">' + smsToEmailSubmitBtnName + '</a> </div>');
                //settingsList();
                var msg = "";
                if (userSMSToEmailValue) {
                    var value = userSMSToEmailValue;
                    $(".settings-detail .settingMenuSmsToEmail  input:checkbox ").attr("checked", "checked");
                    msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userguid + '"  ProgramGUID="' + programguid + '"/>';
                } else {
                    var value = userSMSToEmailValue;
                    $(".settings-detail .settingMenuSmsToEmail  input:checkbox ").removeAttr("checked");
                    msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userguid + '"  ProgramGUID="' + programguid + '"/>';
                }
                $(".settings-detail .settingMenuSmsToEmail  input:checkbox ").click(function (e) {
                    e.preventDefault();
                    if ($(this).attr("checked")) {
                        value = true;
                        msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userguid + '"  ProgramGUID="' + programguid + '"/>';
                    } else {
                        value = false;
                        msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userguid + '"  ProgramGUID="' + programguid + '"/>';
                    }
                });

                $('.settings-detail .settings .blue').unbind('click');
                $('.settings-detail .settings .blue').click(function (e) {
                    e.preventDefault();
                    $.ajax({
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            var returnCode = data.substring(0, 1);
                            var message = data.substr(2);
                            if (returnCode == "1") {
                                userSMSToEmailValue = value;
                                showCorrectSettingMessage(smsToEmailTitle, message, smsToEmailSubmitBtnName);
                            } else {
                                showErrorSettingMessage(smsToEmailTitle, message, smsToEmailSubmitBtnName);
                            }
                        }
                    });
                });
            }
            $(".settings-detail .settings .cancel").click(function (e) {
                e.preventDefault();
                returnToSettingsList();
            });
        });
    }
   
    //return to sequence
    /*$('.wrapper-settings .button-back').click(function () {
        $('.settings-menu,.settings-detail').fadeOut(500);
       // $('.setting-menu-wrapper').delay(500).fadeOut(500);
        $('#controlBar').delay(1000).slideDown();
        $('#bubbles').delay(1000).fadeIn(500);
        $(".bottomgradient").css("opacity", "1");
    });*/
    //input field details
    
}

//
//move onResize(),getImageSize(),blend(),drawImage() function to blenderMultiply.js
//

// start added by XMX for retake  on 2011/12/1
/*function getProgramVariable(XMLObj) {
var programVariablesObj = $(XMLObj).find("ProgramVariables");
var generalVariablesObj = $(XMLObj).find("GeneralVariables");
if (programVariablesObj != null && programVariablesObj != '' && programVariablesObj != undefined) {
var variablesObj = $(programVariablesObj).find("Variable");
if (variablesObj != null && variablesObj != undefined && variablesObj != '') {
$(variablesObj).each(function (variableIndex, variable) {
var variableName = $(variable).attr("Name");
var variableType = $(variable).attr("Type");
var variableValue = $(variable).attr("Value");
if (variableName != undefined && variableName != null && variableName != '') {
programVariables[variableName] = { "VariableValue": variableValue, "VariableType": variableType };
}
});
}
}
if (generalVariablesObj != null && generalVariablesObj != undefined && generalVariablesObj != '') {
var generalVariableObj = $(generalVariablesObj).find("Variable");
if (generalVariableObj != null && generalVariableObj != '' && generalVariableObj != undefined) {
$(generalVariableObj).each(function (generalVarIndex, generalVariable) {
var generalVarName = $(generalVariable).attr("Name");
var generalVarType = $(generalVariable).attr("Type");
var generalVarValue = $(generalVariable).attr("Value");
if (generalVarName != undefined && generalVarName != null && generalVarName != '') {
programVariables[generalVarName] = { "VariableValue": generalVarValue, "VariableType": generalVarType };
}
});
}
}
}*/
function showErrorSettingMessage(title,message,btn) {
    $(".settings").hide();
    $(".settings-detail .settings-message").append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1 class="warnning">' + title + '</h1><p>&nbsp;</p><p>' + message + '</p><p class="spacefiller">&nbsp;</p></div>');
    $('.settings-detail .settings-message').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink blue">' + btn + '</a></div>');
    $(".settings-message").show();
    if (!$("body").hasClass("settings-body")) {
        $("body").addClass("settings-body");
    }
    $(".settings-detail .settings-message .blue").unbind();
    $(".settings-detail .settings-message .blue").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
    $(".settings-button-back .button-back").unbind();
    $(".settings-button-back .button-back").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
}
function showCorrectSettingMessage(title,message,btn) {
    $(".settings").hide();
    $(".settings-detail .settings-message").append('<div class="col-1full centeraligned"><p class="spacefiller">&nbsp;</p><h1>' + title + '</h1><p>&nbsp;</p><p>' + message + '</p><p class="spacefiller">&nbsp;</p></div>');
    $('.settings-detail .settings-message').append('<div class="col-1full centeraligned"><a href="" onfocus="this.blur();" class="actionlink blue">' + btn + '</a></div>');
    $(".settings-message").show();
    if (!$("body").hasClass("settings-body")) {
        $("body").addClass("settings-body");
    }
    $(".settings-detail .settings-message .blue").unbind();
    $(".settings-detail .settings-message .blue").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
    $(".settings-button-back .button-back").unbind();
    $(".settings-button-back .button-back").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
}
function showSettingContent() {
    $(".wrapper-image").hide();
    $(".wrapper-bubbles").hide();
    $('#iframe-settings').hide();
    $(".settings-detail .settings-message").hide();
    $(".settings-detail .settings,.settings-detail .settings-message").html("");
    $(".settings-detail").show();
    $(".settings-detail .settings").show();
    $('.settings-button-back .button-back').show();
    if (!$("body").hasClass("settings-body")) {
        $("body").addClass("settings-body");
    }

    $(".settings-button-back .button-back").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
    $(".settings-detail .settings .cancel").click(function (e) {
        e.preventDefault();
        returnToSettingsList();
    });
}
function returnToSettingsList() {
    $(".wrapper-image").hide();
    $(".wrapper-bubbles").hide();
    $(".settings-detail").hide();
    if ($("body").hasClass("settings-body")) {
        $("body").addClass("settings-body");
    }
    $("#iframe-settings").show(); 
}
function setRetakeBubbleYellowHeight() {
    var bubbleContentHeight = $("#bubble-yellow .bubcontent").outerHeight(true);
    $("#bubble-yellow").animate({ height: bubbleContentHeight }, 500);
}
function hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable() {
    $('#bubble-yellow .bubcontent p').remove();
    if ($('#bubble-yellow .bubcontent div').hasClass("clear")) {
        $('#bubble-yellow .bubcontent div[class=clear]').remove();
    }
    if ($('#bubble-yellow .bubcontent').children().length <= 0) {
        $('#bubble-yellow').hide();
        $('#bubble-yellow .bubcontent').hide();
    }
}
function disabledElementAndSetAnswersForRetakeDay() {
    if (retakeValue == "true" || retakeValue == "1") {
        var pageType = $(pageObj).attr("Type");
        if (pageType == "Timer") {
            $("#bubble-yellow .clock1 input").attr("disabled", "disabled");
            var programVariableName = $(pageObj).attr("ProgramVariable");
            var proVariableValue = getProgramVariableValue(programVariableName);
            /*var proVariableObj= programVariables[programVariableName];
            var proVariableValue="";
            if(proVariableObj != null && proVariableObj != undefined){
            proVariableValue=parseInt(proVariableObj.VariableValue);
            }*/
            var hours = parseInt(proVariableValue / 3600);
            var leftNum = parseInt(proVariableValue % 3600);
            var minutes = parseInt(leftNum / 60);
            var seconeds = parseInt(leftNum % 60);
            if (!isNaN(hours)) {
                if (hours.toString().length == 2) {
                    $(".stopwatch .display span.hr").html(hours.toString());
                } else {
                    $(".stopwatch .display span.hr").html("0" + hours.toString());
                }
            }
            if (!isNaN(minutes)) {
                if (minutes.toString().length == 2) {
                    $(".stopwatch .display span.min").html(minutes.toString());
                } else {
                    $(".stopwatch .display span.min").html("0" + minutes.toString());
                }
            }
            if (!isNaN(seconeds)) {
                if (seconeds.toString().length == 2) {
                    $(".stopwatch .display span.sec").html(seconeds);
                } else {
                    $(".stopwatch .display span.sec").html("0" + seconeds.toString());
                }
            }

        }
        if (pageType == "Account creation") {
            $("#bubble-yellow form .row input").attr("disabled", "disabled");
            $("#bubble-yellow form .row .noRegister input.checkbox").attr("checked", true);
        }
        if (pageType == "Choose preferences") {
            var selectedPreGUID = [];
            var answer = "";
            var preferencesObj = $(pageObj).find("Preferences");
            if (preferencesObj != null && preferencesObj != undefined && preferencesObj != "") {
                var preferenceObj = $(preferencesObj).find("Preference");
                if (preferenceObj != null && preferenceObj != undefined && preferenceObj != '') {
                    $(preferenceObj).each(function (preferenceIndex, preference) {
                        var preferenceGUID = $(preference).attr("GUID");
                        var preProgramVariableName = $(preference).attr("ProgramVariable");
                        var preProgramVariableValue = getProgramVariableValue(preProgramVariableName);
                        /* var preProgramVariableObj = programVariables[preProgramVariableName];
                        if (preProgramVariableObj != null && preProgramVariableObj != undefined) {
                        preProgramVariableValue = preProgramVariableObj.VariableValue;
                        }*/
                        if (preProgramVariableValue != undefined && preProgramVariableValue != null) {
                            answer = preProgramVariableValue;
                            if (answer == "1") {
                                selectedPreGUID.push(preferenceGUID);
                            }
                        }
                    });
                }
            }
            $(".preferences .pictureversion").each(function () {
                var currentGUID = $(this).children("input[type=hidden]").val();
                for (var i = 0; i < selectedPreGUID.length; i++) {
                    if (selectedPreGUID[i] == currentGUID) {
                        $(this).addClass("retakepreferenceSelected");
                    }
                }
            });
            $(".preferences #selectedGUID").val(selectedPreGUID.join(";"));
            $(".preferences .pictureversion").css({ "cursor": "none" });

        }
        if (pageType == "Get information") {
            var questionsObj = $(pageObj).find("Questions");
            if (questionsObj != null && questionsObj != undefined && questionsObj != "") {
                var questionObj = $(questionsObj).find("Question");
                if (questionObj != null && questionObj != undefined && questionObj != '') {
                    $(questionObj).each(function (quesIndex, ques) {
                        var quesGUID = $(ques).attr("GUID");
                        var quesType = $(ques).attr("Type");
                        var quesProgramVariableName = $(ques).attr("ProgramVariable");
                        var quesProgramVariableValue = getProgramVariableValue(quesProgramVariableName);
                        /*var quesProgramVariableObj = programVariables[quesProgramVariableName]
                        if (quesProgramVariableObj != null && quesProgramVariableObj != undefined) {
                        quesProgramVariableValue = quesProgramVariableObj.VariableValue;
                        }*/
                        if (quesType == "Slider") {
                            $('#bubble-yellow .sliders' + quesIndex + ' #slider' + quesIndex + '').slider("disable");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != undefined && answer != null && answer != '') {
                                    saveAnswersForSlider(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .sliders' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "DropDownList") {
                            $('select[name=dropdownlistGroup' + quesIndex + ']').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != null && answer != undefined && answer != '') {
                                    saveAnswersForDropdownList(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .dropdownList' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "CheckBox") {
                            $('#bubble-yellow').find('input[name=checkboxGroup' + quesIndex + ']:checkbox').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != '' && answer != undefined && answer != null) {
                                    saveAnswersForCheckbox(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .check' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "RadioButton") {
                            $('#bubble-yellow').find('input[name=radioButtonGroup' + quesIndex + ']:radio').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != null && answer != undefined && answer != '') {
                                    saveAnswersForRadioButton(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .radios' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "Multiline") {
                            $('#bubble-yellow').find('.textarea' + quesIndex + '').find('textarea').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != null && answer != undefined && answer != '') {
                                    saveAnswersForMultipline(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .textarea' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }

                        } else if (quesType == "Singleline") {
                            $('#bubble-yellow').find('.singleLine' + quesIndex + '').find('input').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != undefined && answer != null && answer != '') {
                                    saveAnswersForSingleline(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .singleLine' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "Hidden singleline") {
                            $('#bubble-yellow').find('.hiddenSingleLine' + quesIndex + '').find('input').attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != undefined && answer != null && answer != '') {
                                    saveAnswersForHiddenSingleline(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .hiddenSingleLine' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();
                            }
                        } else if (quesType == "Numeric") {
                            $("#bubble-yellow .qbox" + quesIndex + " input[name=qty" + quesIndex + "]:text").attr("disabled", "disabled");
                            $("#bubble-yellow .qbox" + quesIndex + " input[name=add" + quesIndex + "]").attr("disabled", "disabled");
                            $("#bubble-yellow .qbox" + quesIndex + " input[name=subtract" + quesIndex + "]").attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != null && answer != undefined && answer != '') {
                                    saveAnswersForNumeric(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .qbox' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();

                            }
                        } else if (quesType == "TimePicker") {
                            $("#bubble-yellow .qclock" + quesIndex + " .qinput2 input[name=qty1" + quesIndex + "]:text").attr("disabled", "disabled");
                            $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput1Add" + quesIndex + "]").attr("disabled", "disabled");
                            $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput1Subtract" + quesIndex + "]").attr("disabled", "disabled");
                            $("#bubble-yellow .qclock" + quesIndex + " .qinput1 input[name=qty2" + quesIndex + "]:text").attr("disabled", "disabled");
                            $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput2Add" + quesIndex + "]").attr("disabled", "disabled");
                            $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput2Subtract" + quesIndex + "]").attr("disabled", "disabled");
                            if (quesProgramVariableValue != undefined && quesProgramVariableValue != null) {
                                var answer = quesProgramVariableValue;
                                if (answer != null && answer != undefined && answer != '') {
                                    saveAnswersForTimePicker(answer, quesIndex, quesGUID);
                                }
                            } else {
                                $('#bubble-yellow .bubcontent .timppicker' + quesIndex + '').remove();
                                hiddenBubbleYellowWhenVariableIsEmptyOrNoVariable();

                            }
                        }

                    });

                }
            }
           // setRetakeBubbleYellowHeight();
        }
    }
}
// end added for retake by XMX on 2011/12/1
// started added for retake by XMX on 2011/12/8
function getProgramVariableValue(programVariableName) {
    var proVariableValue = "";
    var prograVariableLength = proV.length;
    if (programVariableName != null && programVariableName != undefined) {
        for (var i = 0; i < prograVariableLength; i++) {
            if (proV[i][0] == programVariableName) {
                proVariableValue = proV[i][2];
                break;
            }
        }
        return proVariableValue;
    }
}
// end added for retake by XMX on 2011/12/8
