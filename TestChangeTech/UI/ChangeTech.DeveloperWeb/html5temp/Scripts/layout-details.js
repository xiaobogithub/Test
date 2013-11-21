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
    $(".mask").css("display", "none");
    var msgNode = document.createTextNode(message);
    var msgInfo = $('#popupDialog').html('<p class="normalMsg"></p><div class="popupButWrapper"><input type="button" value="OK" id="popupBtn"><div>');
    $('#popupDialog .normalMsg').append(msgNode);
    $('#popupDialog .normalMsg br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>");
    $('#programbar').slideUp();
    $('#bubbles').fadeOut(500);
    $('#main-wrapper').delay(500).fadeIn(500, function () {
        $('#popupDialog').fadeIn();
    });
    $("#popupBtn").click(function () {
        $('#popupDialog').fadeOut(function () {
            $('#main-wrapper').delay(500).fadeOut(function () {
                $("#programbar").slideDown(function () {
                    $('#bubbles').fadeIn(500);
                });
            });
        });
    });

    $('#main-wrapper').click(function () {
        $('#popupDialog').fadeOut(function () {
            $('#main-wrapper').delay(500).fadeOut(function () {
                $("#programbar").slideDown(function () {
                    $('#bubbles').fadeIn(500);
                });
            });
        });
    });
}
function showMessageForFaildRegister(messageSendFromServer) {
    $(".mask").css("display", "none");
    var ms = tipMessages["RegisterFailed"];
    if (ms != null && ms != undefined) {
        var title = ms.Title;
        var message = ms.Message;
        var backBtnName = ms.BackButtonName;
        $('#popupDialog').html('<h1>' + title + '</h1><p>' + messageSendFromServer + '</p><div class="popupButWrapper"><input type="button" value="' + backBtnName + '" id="popupBtn"></div>');
    } else {
        var msgNode = document.createTextNode(messageSendFromServer);
        var msgInfo = $('#popupDialog').html('<p class="normalMsg"></p><div class="popupButWrapper"><input type="button" value="OK" id="popupBtn"><div>');
        $('#popupDialog .normalMsg').append(msgNode);
    }
    $('#popupDialog p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>");
    $('#programbar').slideUp();
    $('#bubbles').fadeOut(500);
    $('#main-wrapper').delay(500).fadeIn(500, function () {
        $('#popupDialog').fadeIn();
    });
    $("#popupBtn").click(function () {
        $('#popupDialog').fadeOut(function () {
            $('#main-wrapper').delay(500).fadeOut(function () {
                $("#programbar").slideDown(function () {
                    $('#bubbles').fadeIn(500);
                });
            });
        });
    });

    $('#main-wrapper').click(function () {
        $('#popupDialog').fadeOut(function () {
            $('#main-wrapper').delay(500).fadeOut(function () {
                $("#programbar").slideDown(function () {
                    $('#bubbles').fadeIn(500);
                });
            });
        });
    });
}
function showTipMessage(msgName) {
    $(".mask").css("display","none");
    var ms = tipMessages[msgName];
    if (ms != null && ms != undefined) {
        var title = ms.Title;
        var message = ms.Message;
        var backBtnName = ms.BackButtonName;
        $('#popupDialog').html('<h1>' + title + '</h1><p>' + message + '</p><div class="popupButWrapper"><input type="button" value="' + backBtnName + '" id="popupBtn"></div>');
        $('#popupDialog p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>")
        $('#programbar').slideUp();
        $('#bubbles').fadeOut(500);
        $('#main-wrapper').delay(500).fadeIn(500, function () {
            $('#popupDialog').fadeIn();
        });

        $("#popupBtn").click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    $("#programbar").slideDown(function () {
                        $('#bubbles').fadeIn(500);
                    });
                });
            });
        });
        $('#main-wrapper').click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    $("#programbar").slideDown(function () {
                        $('#bubbles').fadeIn(500);
                    });
                });
            });
        });
    } else {
        $('#popupDialog').html('<p>No Tip Message &lt;' + msgName + '&gt;</p><div class="popupButWrapper"><input type="button" value="OK" id="popupBtn"></div>');
        $('#programbar').slideUp();
        $('#bubbles').fadeOut(500);
        $('#main-wrapper').delay(500).fadeIn(500, function () {
            $('#popupDialog').fadeIn();
        });


        $("#popupBtn").click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    $("#programbar").slideDown(function () {
                        $('#bubbles').fadeIn(500);
                    });
                });
            });
        });
        $('#main-wrapper').click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    $("#programbar").slideDown(function () {
                        $('#bubbles').fadeIn(500);
                    });
                });
            });
        });
    }

}
function showTipMessageForCatchUpDay() {
    $(".mask").css("display", "none");
    var msgName = 'CatchingUpEarlyDay';
    var ms = tipMessages[msgName];
    if (ms != null && ms != undefined) {
        var title = ms.Title;
        var message = ms.Message;
        var backBtnName = ms.BackButtonName;
        $('#popupDialog').html('<h1>' + title + '</h1><p>' + message + '</p><div class="popupButWrapper"><input type="button" value="' + backBtnName + '" id="popupBtn"></div>');
        $('#popupDialog p br.emptyline').replaceWith("<p class='emptyline'>&nbsp;</p>")
        $('#programbar').slideUp();
        $('#bubbles').fadeOut(500);
        $('#main-wrapper').delay(500).fadeIn(500, function () {
            $('#popupDialog').fadeIn();
        });

        $("#popupBtn").click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    /* $( "#programbar" ).slideDown( function () {
                    $( '#bubbles' ).fadeIn( 500 );
                    } );*/
                    programBarAnimation();
                });

            });
        });
    } else {
        $('#popupDialog').html('<p>No Tip Message &lt;' + msgName + '&gt;</p><div class="popupButWrapper"><input type="button" value="OK" id="popupBtn"></div>');
        $('#programbar').slideUp();
        $('#bubbles').fadeOut(500);
        $('#main-wrapper').delay(500).fadeIn(500, function () {
            $('#popupDialog').fadeIn();
        });
        $("#popupBtn").click(function () {
            $('#popupDialog').fadeOut(function () {
                $('#main-wrapper').delay(500).fadeOut(function () {
                    /*$( "#programbar" ).slideDown( function () {
                    $( '#bubbles' ).fadeIn( 500 );
                    } );*/
                    programBarAnimation();
                });

            });
        });
    }

}
/*function showTopBar() {
$('.progtitle').empty();
var dayNumber = $(root).find('Session').attr('Day');
var day = specialStirngs["Day"];
if(day != undefined && day !=null){
$('.progtitle').append('<span>' + day.toUpperCase() + '</span>');
}else{
$('.progtitle').append('<span>DAG</span>');
}
if(dayNumber !=undefined && dayNumber !=null){
$('.progtitle').append(' <span>' + dayNumber + '</span>');
}
}*/
function showTopBar() {
    $('.programtitle').empty();
    var isNotShowDayAndSetMenu = $(root).attr("IsNotShowDayAndSetMenu");
    var dayNumber = $(root).find('Session').attr('Day');
    var day = specialStirngs["Day"];
    var CTPPName = $(root).attr('CTPPName');
    var programName = $(root).attr('ProgramName');
    if (CTPPName == undefined || CTPPName == '' || CTPPName == null) {
        if (programName != undefined && programName != null) {
            $('.programtitle').html("<span>" + programName + "</span>");
        } else {
        programName = "";
            $('.programtitle').html("<span>" + programName + "</span>");
        }
    } else {
        $('.programtitle').html("<span>"+CTPPName+"</span>");
    }
    if (day != undefined && day != null) {
        $('.programtitle').append('<div class="programdayDiv"><span class="programday">' + day.toUpperCase() + '</span></div>');
    } else {
        $('.programtitle').append('<div class="programdayDiv"><span class="programday">DAG</span></div>');
    }
    if (dayNumber != undefined && dayNumber != null) {
        $('.programdayDiv').append('<span class="programday">' + dayNumber + '</span>');
    }
    var settingMenu = specialStirngs["SettingMenu"];
    if (settingMenu != undefined && settingMenu != null) {
        $(".button-settings").html(settingMenu);
    }
    var back = specialStirngs["Back"];
    if (back != undefined && back != null) {
        $(".button-back .button-back-text").html(back);
    }
    if (isNotShowDayAndSetMenu != null && isNotShowDayAndSetMenu != undefined && isNotShowDayAndSetMenu != "") {
        if (isNotShowDayAndSetMenu == "1" || isNotShowDayAndSetMenu.toLowerCase() == 'true') {
            $(".programdayDiv").css("display", "none");
            $(".button-settings").css("display", "none");
        } else {
            $(".programdayDiv").removeAttr("style");
            $(".button-settings").css("display", "block");
        }
    } else {
        $(".programdayDiv").removeAttr("style");
        $(".button-settings").css("display", "block");
    }
        var programTitleWidth = $('.programtitle').width();
        $(".programtitle").css({ "left": "50%", "margin-left": "-" + parseInt(programTitleWidth) / 2 + "px" });
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
    var hiddenElementStr = '<div id="content">' +
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
        $('.button-back').unbind('click');
        $('.button-back').click(function () {
            if (isBackFromPreviewEndPage && isPreviewMode) {
                $('#bubble-blue').unbind("click")
                $('#bubble-blue').bind("click", function () {
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


        //showDialog
        //showDialog();

        gotoPage(true);
        getUserAnswers(pageObj);
        // $("#pic img#backgroundpic").css('width', 'auto');
        directllyGo = true;
        if ($("#hostpic").text() == '') {
            $("#backgroundpic").hide();
        } else {
            $("#backgroundpic").fadeOut(200);
        }
       /* if (presenterImage != null && presenterImage != undefined && presenterImage != "") {
            onBlendTransparentImage(presenterImage);
        }
        if ($("#bgImg").text() != '') {
            $("#fullbgImg").hide();
            setTimeout(backgrondImageFadeIn, 100);
        }*/
        if (presenterImage != null && presenterImage != undefined && presenterImage != "") {
            showIllustrationImage("Illustration", presenterImage);
        }
        var mediaObj = pageObj.find("Media");
        if (mediaObj.length > 0) {
            var mediaType = $(mediaObj).attr("Type");
            var mediaName = $(mediaObj).attr("Media");
            showIllustrationImage(mediaType, mediaName);
        }
    }

    // start added for retake by XMX on 2011/12/01
    disabledElementAndSetAnswersForRetakeDay();
    // end added for retake by XMX on 2011/12/01
    if ($("#bubble-yellow .loginEmail")) {
        setTimeout(function () { $("#bubble-yellow .loginEmail").focus(); }, 501);
    }
    if (step < 1) {
        $(".button-back").hide();
    } else {
        $(".button-back").show(200);
    }
    if (isGraphTemplate) {
        $("#bubbles").css("width", "800px");
        $("#bubble-white").css("display", "none");
        $(".graphcontainer").css("display", "block");
        $("#pic").css("display", "none");
        $(".mask").css("display", "block");
    } else {
        $("#bubbles").css("width", "542px");
        if ($("#white").html() != '') {
            $("#bubble-white").css("display", "block");
        }
        $(".graphcontainer").css("display", "none");
        $("#bubble-blue").css("margin", "0 0 20px");
        if ($('#hiddenImage').text() != '') {
            $("#pic").css("display", "block");
        }
        $(".mask").css("display", "none");
    }
   // $(".mask").css("display","none");
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
                    setHeight();
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
            $("#bubble-black").animate({ height: "284px" });
        } else if (pageObj.find("Media").attr("Type") == "Audio") {
            $("#bubble-black").animate({ height: "29px" });
        } else {
            $("#bubble-black").css({ height: "auto" });
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
    loadBackgroundImage(pageObj);
    loadPresenterImage(pageObj);
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
        var isBeforePageStart = true;
        loadDataForGraphTemplate(isBeforePageStart);
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
        //$('#pic').append('<img  id="over" src="Images/overtest.png"/>');
        //$('#pic').append('<canvas id="under"></canvas>');
        //$('#pic').append('<canvas id="backgroundpic"></canvas>');
    } else {
        $("#hostPic").empty();
    }
    //showMainBubblePosition(pageObj);
}
function showMainBubblePosition(pageObj) {
    //var presenterImagePosition=pageObj.attr("PresenterImagePosition");
    //var presenterImage=pageObj.attr("PresenterImage");
    //if(presenterImage!=null){
    //	$("#bubbles").addClass('centerright').removeClass('center');
    //	$(".centerright").css({'right':'0px'});
    //}else{
    //	$("#bubbles").addClass('center').removeClass('centerright');
    //	$('#bubbles').css({'right':'0px'});
    //}
    //if(navigator.userAgent.toLowerCase().match(/iPhone/i)== null) {
    //$('#bubbles').removeClass("centerright").animate({left: '0px'},500).addClass("center");
    // }
    //$.getScript("Scripts/custom.js");
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
function showDialog() {
    var hasHelpFunction = false;
    var hasTipFriendFunction = false;
    var hasPauseProgramFunction = false;
    var hasProfileFunction = false;
    var hasExitProgramFunction = false;
    var hasTimeZoneFunction = false;
    var hasSMSToEmailFunction = false;
    //prepare for setting menu
    var settingMenu = $(oraiRoot).find("SettingMenu");
    if (settingMenu != null && settingMenu != undefined) {
        $(settingMenu).find("MenuItem").each(function (index, settingMenuItem) {
            var functionName = "";
            var name = "";
            if ($(settingMenuItem).attr("FunctionName") != null && $(settingMenuItem).attr("FunctionName") != undefined) {
                functionName = $(settingMenuItem).attr("FunctionName");
            }
            if ($(settingMenuItem).attr("Name") != null && $(settingMenuItem).attr("Name") != undefined) {
                name = $(settingMenuItem).attr("Name");
            }
            if (functionName == "HelpFunction") {
                if (name != null && name != undefined && name != '') {
                    $('.setting-menu').append('<span><a href="#x" class="help">' + name + '</a></span>');
                } else {
                    $('.setting-menu').append('<span><a href="#x" class="help">' + functionName + '</a></span>');
                }
                hasHelpFunction = true;
            }
            if (functionName == "TipFriendFunction") {
                if (name != null && name != undefined && name != '') {
                    $('.setting-menu').append('<span><a href="#x" class="mail">' + name + '</a></span>');
                } else {
                    $('.setting-menu').append('<span><a href="#x" class="mail">' + functionName + '</a></span>');
                }
                hasTipFriendFunction = true;
            }

            if (functionName == "PauseProgramFunction") {
                if (name != null && name != undefined && name != '') {
                    $('.setting-menu').append('<span><a href="#x" class="pause">' + name + '</a></span>');
                } else {
                    $('.setting-menu').append('<span><a href="#x" class="pause">' + functionName + '</a></span>');
                }
                hasPauseProgramFunction = true;
            }

            if (functionName == "ProfileFunction") {
                if (name != null && name != undefined && name != '') {
                    $('.setting-menu').append('<span><a href="#x" class="email">' + name + '</a></span>');
                } else {
                    $('.setting-menu').append('<span><a href="#x" class="email">' + functionName + '</a></span>');
                }
                hasProfileFunction = true;
            }

            if (functionName == "ExitProgramFunction") {
                if (name != null && name != undefined && name != '') {
                    $('.setting-menu').append('<span><a href="#x" class="shutdown">' + name + '</a></span>');
                } else {
                    $('.setting-menu').append('<span><a href="#x" class="shutdown">' + functionName + '</a></span>');
                }
                hasExitProgramFunction = true;
            }
            var isSupportTimeZone = $(oraiRoot).attr("IsSupportTimeZone");
            if (isSupportTimeZone == "1" || isSupportTimeZone == "true") {
                if (functionName == "TimeZoneFunction") {
                    if (name != null && name != undefined && name != '') {
                        $('.setting-menu').append('<span><a href="#x" class="timeZone">' + name + '</a></span>');
                    } else {
                        $('.setting-menu').append('<span><a href="#x" class="timeZone">' + functionName + '</a></span>');
                    }
                    hasTimeZoneFunction = true;
                }
            }

            var isSMSToEmail = $(oraiRoot).attr("IsSMSToEmail");
          /*  if (isSMSToEmail == "1" || isSMSToEmail == "true") {*/
                if (functionName == "SMSToEmailFunction") {
                    if (name != null && name != undefined && name != "") {
                        $('.setting-menu').append('<span><a href="#x" class="smsToEmail">' + name + '</a></span>');
                    } else {
                        $('.setting-menu').append('<span><a href="#x" class="smsToEmail">' + functionName + '</a></span>');
                    }
                    hasSMSToEmailFunction = true;
                }
           /* }*/
        });
    }
    //show settings menu
    $(".button-settings").click(function () {
        $('#programbar').slideUp();
        $('#bubbles').fadeOut(500);
        $('.setting-menu-wrapper').delay(500).fadeIn(500);
        $('.setting-menu').css({ 'top': ($(document).height() - $('.setting-menu').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-menu').width()) / 2 + 'px' }).delay(1000).fadeIn(500);
    });
    //close settings menu
    $('.setting-menu em a').click(function () {
        $('.setting-menu').fadeOut(500);
        $('.setting-menu-wrapper').delay(500).fadeOut(500);
        $('#programbar').delay(1000).slideDown();
        $('#bubbles').delay(1000).fadeIn(500);
    });
    //list datails
    function settingsList() {
        $('.setting-list-box dt').toggle(
			function () {
			    $(this).siblings('dd').show();
			    $(this).parent().addClass('expand');
			    $(this).addClass('expand');
			},
			function () {
			    $(this).siblings('dd').hide();
			    $(this).parent().removeClass('expand');
			    $(this).removeClass('expand');
			}
		);
        $('.setting-detail-close').click(function () {
            $('.setting-detail').fadeOut(500);
            $('.setting-menu').css({ 'top': ($(document).height() - $('.setting-menu').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-menu').width()) / 2 + 'px' }).delay(500).fadeIn(500);
        });
        var newEmailAddress = "";
        if (specialStirngs["New_email_address"] != null && specialStirngs["New_email_address"] != undefined && specialStirngs["New_email_address"] != "") {
            newEmailAddress = specialStirngs["New_email_address"];
        }
        $(".setting-detail input[name = 'e-mail']").focus(function () {
            if ($(this).val() == newEmailAddress) {
                $(this).val('');
            }
        });
        $(".setting-detail input[name = 'e-mail']").blur(function () {
            if ($(this).val() == '') {
                $(this).val(newEmailAddress);
            }
        });
    }

    //FAQ button
    if (hasHelpFunction) {
        $('.setting-menu span a.help').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            var helpObj = $(oraiRoot).find("Help");
            if (helpObj != null && helpObj != undefined) {
                var helpTitle = "";
                var backBtnName = "";
                if ($(helpObj).attr("Title") != null && $(helpObj).attr("Title") != undefined) {
                    helpTitle = $(helpObj).attr("Title");
                }
                if ($(helpObj).attr("BackButtonName") != null && $(helpObj).attr("BackButtonName") != undefined) {
                    backBtnName = $(helpObj).attr("BackButtonName");
                }
                $('.setting-detail').append('<h2>' + helpTitle + '</h2> <div class="setting-list-box"></div>');
                $(helpObj).find('HelpItem').each(function (i, helpItem) {
                    var helpItemTitle = $(helpItem).attr("Title");
                    var helpItemText = $(helpItem).attr("Text")
                    $('.setting-list-box').append('<dl><dt>' + helpItemTitle + '<dt><dd>' + helpItemText + '<dd></dl>');
                });
            }
            settingsList();
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
        });
    }
    //TipFriend button
    if (hasTipFriendFunction) {
        $('.setting-menu span a.mail').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
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
                }
                if ($(tipFriendObj).attr("SubmitButtonName") != null && $(tipFriendObj).attr("SubmitButtonName") != undefined) {
                    tipFriendSubmitBtnName = $(tipFriendObj).attr("SubmitButtonName");
                }
                $('.setting-detail').append('<h2>' + tipFriendTitle + '<span>' + tipFriendText + '</h2><input name="e-mail" type="text" value="' + newEmailAddress + '"><div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + tipFriendSubmitBtnName + '</button></div>');
                settingsList();

            }
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
                var inputEmailValue = $(".setting-detail input[name = 'e-mail']").val();
                var userGUID = $(oraiRoot).attr('UserGUID');
                var programGUID = $(oraiRoot).attr('ProgramGUID');
                if (checkEmailFormat(inputEmailValue)) {
                    var msg = '<TipFriend Invitee="' + inputEmailValue + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '"/>';
                    $.ajax({
                        //url: getReturnDataServerUrl(),
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            $('.return-msg').html(data);
                        }
                    });
                } else {
                    var ms = tipMessage["InvalidEmail"];
                    if (ms != null && ms != undefined) {
                        var message = ms.Message;
                        $('.return-msg').html(message);
                    }
                    $(".setting-detail input[name = 'e-mail']").val(newEmailAddress);
                }

            });
        });
    }
    //ProgramStatus button
    if (hasPauseProgramFunction) {
        $('.setting-menu span a.pause').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            $('.setting-detail').append('<h2>' + $(oraiRoot).find('ProgramStatus').attr('Title') + '<span>' + $(oraiRoot).find('ProgramStatus').attr('Text') + '</h2><select name="pause time"><option>1 week</option><option>2 weeks</option><option>3 weeks</option><option>4 weeks</option></select><div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + $(oraiRoot).find('ProgramStatus').attr('SubmitButtonName') + '</button></div>');
            settingsList();
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
                var week = parseInt($(".setting-detail select[name = 'pause time']").get(0).selectedIndex) + 1;
                var msg = '<ProgrameStatus Status="pause" Week="' + week + '" UserGUID="' + $(oraiRoot).attr('UserGUID') + '" ProgramGUID="' + $(oraiRoot).attr('ProgramGUID') + '"/>';
                $.ajax({
                    //url: getReturnDataServerUrl(),
                    url: getURL('submit'),
                    type: 'POST',
                    processData: false,
                    data: msg,
                    dataType: "text",
                    success: function (data) {
                        $('.return-msg').html(data);
                    }
                });
            });
        });
    }

    //Profile button
    if (hasProfileFunction) {
        $('.setting-menu span a.email').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            var newEmailAddress = "";
            if (specialStirngs["New_email_address"] != null && specialStirngs["New_email_address"] != undefined && specialStirngs["New_email_address"] != "") {
                newEmailAddress = specialStirngs["New_email_address"];
            }
            $('.setting-detail').append('<h2>' + $(oraiRoot).find('Profile').attr('Title') + '<span>' + $(oraiRoot).find('Profile').attr('Text') + '</h2><h3>' + $(oraiRoot).find('Profile').find('Item').attr('OldValue') + '</h3><input name="e-mail" type="text" value="' + newEmailAddress + '"><div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + $(oraiRoot).find('Profile').attr('SubmitButtonName') + '</button></div>');
            settingsList();
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
                if (checkEmailFormat($(".setting-detail input[name = 'e-mail']").val())) {
                    var msg = '<ChangeProfile UserGUID="' + $(oraiRoot).attr('UserGUID') + '" ProgramGUID="' + $(oraiRoot).attr('ProgramGUID') + '"> <Items> <Item Name="Email" NewValue="' + $(".setting-detail input[name = 'e-mail']").val() + '"/> </Items> </ChangeProfile>';
                    $.ajax({
                        //url: getReturnDataServerUrl(),
                        url: getURL('submit'),
                        type: 'POST',
                        processData: false,
                        data: msg,
                        dataType: "text",
                        success: function (data) {
                            $('.return-msg').html(data);
                        }
                    });
                } else {
                    $('.return-msg').html('Dette er ikke en gyldig e-post adresse');
                    $(".setting-detail input[name = 'e-mail']").val(newEmailAddress);
                }
            });
        });
    }
    //ExitProgram button
    if (hasExitProgramFunction) {
        $('.setting-menu span a.shutdown').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            $('.setting-detail').append('<h2>' + $(oraiRoot).find('ExitProgram').attr('Title') + '<span>' + $(oraiRoot).find('ExitProgram').attr('Text') + '</span></h2><div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + $(oraiRoot).find('ExitProgram').attr('SubmitButtonName') + '</button></div>');
            settingsList();
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
                var msg = '<ProgrameStatus Status="exit" UserGUID="' + $(oraiRoot).attr('UserGUID') + '" ProgramGUID="' + $(oraiRoot).attr('ProgramGUID') + '"/>';
                $.ajax({
                    //url: getReturnDataServerUrl(),
                    url: getURL('submit'),
                    type: 'POST',
                    processData: false,
                    data: msg,
                    dataType: "text",
                    success: function (data) {
                        $('.return-msg').html(data);
                    }
                });
            });
        });
    }
    if (hasTimeZoneFunction) {
        $('.setting-menu span a.timeZone').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            var timeZoneTitle = $(oraiRoot).find('TimeZone').attr('Title');
            var timeZoneText = $(oraiRoot).find('TimeZone').attr('Text');
            var timeZoneSubmitBtn = $(oraiRoot).find('TimeZone').attr('SubmitButtonName');
            var userTimeZone = $(oraiRoot).attr("UserTimeZone");
            var programTimeZone = $(oraiRoot).attr("ProgramTimeZone");
            var isSupportTimeZone = $(oraiRoot).attr("IsSupportTimeZone");
            if (isSupportTimeZone == "1" || isSupportTimeZone == "true") {
                var timeZoneObj = $(oraiRoot).find("TimeZoneList");
                if (timeZoneObj != undefined && timeZoneObj != null) {
                    var selectObj = $(timeZoneObj).find("select");
                    if (selectObj != undefined && selectObj != null) {
                        var selectStr = "<select class='selectbox accountCreationSelect'>" + $(timeZoneObj).find("select")[0].innerHTML + '</select>';
                        var timeZoneStr = '<div class="settingMenuTimezone timeZone">' + selectStr + '</div>';
                    }
                }
                settingsList();
            }

            $('.setting-detail').append('<h2>' + timeZoneTitle + '<span>' + timeZoneText + '</span></h2>' + timeZoneStr + '<div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + timeZoneSubmitBtn + '</button></div>');
            $('.settingMenuTimezone').find('select.accountCreationSelect').find("option:selected").removeAttr("selected");
            $('.settingMenuTimezone').find('select.accountCreationSelect').find("option").each(function (i, option) {
                var optionValue = $(option).val();
                if (optionValue == userUpdateTimeZone) {
                    $(this).attr("selected", "selected");
                }
            });

            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            var userGUID = $(oraiRoot).attr('UserGUID');
            var programGUID = $(oraiRoot).attr('ProgramGUID');
            //  var timeZoneValue = $('.settingMenuTimezone').find('select.accountCreationSelect').find("option:selected").val();
            //    var msg = '<TimeZone Value="' + timeZoneValue + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '"/>';
            var timeZoneValue = userUpdateTimeZone;
            var msg = '<TimeZone Value="' + timeZoneValue + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '"/>';
            $("select.accountCreationSelect").change(function () {
                timeZoneValue = $(this).val();
                msg = '<TimeZone Value="' + timeZoneValue + '" UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '"/>';
            });
            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
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
                        }
                        $('.return-msg').html(message);
                    }
                });
            });
        });
    }

    if (hasSMSToEmailFunction) {
        $('.setting-menu span a.smsToEmail').click(function () {
            $('.setting-menu').fadeOut(500);
            $('.setting-detail').html('<div class="setting-detail-close"><a href="#x"></a></div>');
            var smsToEmailTitle = $(oraiRoot).find('SmsToEmail').attr('Title');
            var smsToEmailText = $(oraiRoot).find('SmsToEmail').attr('Text');
            var smsToEmailSubmitBtn = $(oraiRoot).find('SmsToEmail').attr('SubmitButtonName');
            var smsToEmailBackBtn = $(oraiRoot).find('SmsToEmail').attr('BackButtonName');
            var isSmsToEmail = $(oraiRoot).attr("IsSMSToEmail");
            /*  if (isSmsToEmail == "1" || isSmsToEmail == "true") {*/
            var smsToEmailString = specialStirngs['SMS_To_Email'];
           // var smsToEmailStr = "Set send message using SMS to Email.";
            if (smsToEmailString == undefined || smsToEmailString == null || smsToEmailString == "") {
                smsToEmailString = "SMS To Email";
            }
            var smsToEmailStr = "<div class='settingMenuSmsToEmail'><input type='checkbox' class='smsToEmailCheckBox' /><span class='smsToEmailLabel'>" + smsToEmailString + "</span></div>";
            settingsList();
            /* }*/
            $('.setting-detail').append('<h2>' + smsToEmailTitle + '<span>' + smsToEmailText + '</span></h2>' + smsToEmailStr + '<div class="return-msg">&nbsp;</div><div class="button-wrapper"><button>' + smsToEmailSubmitBtn + '</button></div>');
            $('.setting-detail').css({ 'top': ($(document).height() - $('.setting-detail').height()) / 3 + 'px', 'left': ($(document).width() - $('.setting-detail').width()) / 2 + 'px' }).fadeIn(500);
            var userGUID = $(oraiRoot).attr('UserGUID');
            var programGUID = $(oraiRoot).attr('ProgramGUID');
            var msg = "";
            if (userSMSToEmailValue) {
                var value = userSMSToEmailValue;
                $(".setting-detail .settingMenuSmsToEmail  input:checkbox ").attr("checked", "checked");
                msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userGUID + '"  ProgramGUID="' + programGUID + '"/>';
            } else {
                var value = userSMSToEmailValue;
                $(".setting-detail .settingMenuSmsToEmail  input:checkbox ").removeAttr("checked");
                msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userGUID + '"  ProgramGUID="' + programGUID + '"/>';
            }
            $(".setting-detail .settingMenuSmsToEmail  input:checkbox ").click(function () {
                if ($(this).attr("checked")) {
                    value = true;
                    msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userGUID + '"  ProgramGUID="' + programGUID + '"/>';
                } else {
                    value = false;
                    msg = '<SMSToEmail   Value="' + value + '" UserGUID="' + userGUID + '"  ProgramGUID="' + programGUID + '"/>';
                }
            });

            $('.setting-detail .button-wrapper button').unbind('click');
            $('.setting-detail .button-wrapper button').click(function () {
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
                        }
                        $('.return-msg').html(message);
                    }
                });
            });
        });
    }

    //return to sequence
    $('.setting-menu-wrapper').click(function () {
        $('.setting-menu,.setting-detail').fadeOut(500);
        $('.setting-menu-wrapper').delay(500).fadeOut(500);
        $('#programbar').delay(1000).slideDown();
        $('#bubbles').delay(1000).fadeIn(500);
    });
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
            $(".preferences ul li").each(function (liIndex, li) {
                var currentGUID = $(this).children("input[type=hidden]").val();
                for (var i = 0; i < selectedPreGUID.length; i++) {
                    if (selectedPreGUID[i] == currentGUID) {
                        $(this).addClass("retakepreferenceSelected");
                    }
                }
            });
            $(".preferences #selectedGUID").val(selectedPreGUID.join(";"));
            $(".preferences ul li.preferenceSelected").css({ "cursor": "none" });

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
                        }
                        else if (quesType == "Hidden singleline") {
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
                        } 
                        else if (quesType == "Numeric") {
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
            setRetakeBubbleYellowHeight();
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