/// <reference path="jquery-1.6.2.min.js" />

function loadDataForChoosePreferenceTemplate(pageObj) {

    var preferenceLiStr = "";
    var popupDiv = "";
    var preferencesObj = pageObj.find("Preferences");
    if (preferencesObj != undefined && preferencesObj != null && preferencesObj != '') {
        var preferenceObj = $(preferencesObj).find("Preference");
        if (preferenceObj != null && preferenceObj != undefined && preferenceObj != '') {
            $(preferenceObj).each(function (preferenceIndex, preference) {
                var imageAttr = $(preference).attr("Image");
                //var imageUrl = "http://changetechstorage.blob.core.windows.net/originalimagecontainer/" + imageAttr;
                var imageUrl = getURL("originalimagecontainerRoot") + imageAttr;
                var nameAttr = $(preference).attr("Name");
                var descriptionAttr = $(preference).attr("Description");
                var answerAttr = $(preference).attr("Answer");
                var ButtonName = $(preference).attr("ButtonName");
                var preferenceGUID = $(preference).attr("GUID");
                if ((nameAttr != '' && nameAttr != undefined && nameAttr != null) || (descriptionAttr != '' && descriptionAttr != undefined && descriptionAttr != null)) {
                    popupDiv = "<div class='popupPreferenceDesc'><div id='pre-bubble-white'><div class='pre-bubcontent'><h1>" + nameAttr + "</h1><p>" + descriptionAttr + "</p></div><div class='bubble-white-arrow'></div></div></div>";
                }
                if (imageAttr != undefined && imageAttr != '' && imageAttr != null) {
                    preferenceLiStr += "<li id='preferencesLi" + preferenceIndex + "'><img src='" + imageUrl + "' alt='" + nameAttr + "' id='preferencesImg" + preferenceIndex + "' width='150px' height='120px' />" + popupDiv + "<input type='hidden' value='" + preferenceGUID + "' /></li>";
                }
            });
        }
    }
    var PreferenceStr = '<div class="preferences"><ul>' + preferenceLiStr + '</ul><input type="hidden" value="" id="selectedGUID" /></div>';
    if (preferenceLiStr != '') {
        $("#yellow").append(PreferenceStr);
    }
}
function getChoosePreferencesData(XMLobj) {
    quesFlag = true;
    var userGUID = $(XMLobj).attr('UserGUID');
    var programGUID = $(XMLobj).attr('ProgramGUID');
    var sessionGUID = $(XMLobj).find('Session').attr('GUID');
    //var pageGUID = $( pageObj ).attr( 'GUID' );
    var pageGUID=$( root ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } ).find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } ).attr( 'GUID' );
    var pageSequenceGUID = $( XMLobj ).find( "PageSequence" ).filter( function () { return $( this ).attr( "Order" ) == sequenceOrder } ).attr( "GUID" ); ;
    var relapsePageGUID = "";
    var relapsePageSequenceGUID = "";
    var pageSequenceOrder = sequenceOrder;
    if ( sessionName == "relapse" || isNaN( sequenceOrder ) || sequenceOrder == "" )
    {
       
        relapsePageGUID = $( pageObj ).attr( 'GUID' );
       
        pageSequenceOrder = "";
		if($(oraiRoot).find('Session').length > 0){
		    sessionGUID = $(oraiRoot).find('Session').attr('GUID');
			pageGUID = pageGUIDBeforeGOSUB;
            pageSequenceGUID = sequenceGUIDBeforeGOSUB;
			relapsePageSequenceGUID = relapseSequenceGUID;
		}else{
			sessionGUID='';
			pageGUID = '';
            pageSequenceGUID = '';
			relapsePageSequenceGUID = seqGuid;
		}
    }
    var selectedPreferenceStr = $(".preferences #selectedGUID").val();
    var selectedPreferenceData = selectedPreferenceStr.substring(0, selectedPreferenceStr.length);
    var answerBackXml = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageGUID="' + pageGUID + '"  RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '">' +
					 '<Feedbacks><Feedback GUID="" Value="' + selectedPreferenceData + '"/></Feedbacks></XMLModel>';
    var preferencesObj = pageObj.find("Preferences");
    if ($(preferencesObj).length > 0) {
        var perferenceObj = $(preferencesObj).find("Preference");
        if ($(perferenceObj).length > 0) {
            $(perferenceObj).each(function (preferenceIndex, preference) {
                var currentGUID = $(".preferences #preferencesLi" + preferenceIndex + "").find("input[type=hidden]").val();
                var programVariableValue = $(preference).attr("ProgramVariable");
                if (programVariableValue != null && programVariableValue != undefined && programVariableValue != '') {
                    var provLength = proV.length;
                    for (var i = 0; i < provLength; i++) {
                        if (proV[i][0] == programVariableValue) {
                            if (itemScore[currentGUID] != undefined) {
                                proV[i][2] = itemScore[currentGUID];
                            }
                            break;
                        }
                    }
                }
            });
        }
    }
    if ($(".preferences #selectedGUID").val() == "") {
        quesFlag = false;
        showTipMessage("PreferenceRequired");
    }
    if (quesFlag && $(".preferences #selectedGUID").val() != "") {
        $.ajax({
            //url: getReturnDataServerUrl(),
			url:getURL('submit'),
            type: 'POST',
            processData: false,
            data: answerBackXml,
            dataType: "text"
        });
    }
}
function choosePreferences() {
    if (retakeValue != "true" && retakeValue != "1") {
        var pageGUID = $(pageObj).attr("GUID");
        var maxPreferenceValue = parseInt($(pageObj).attr("MaxPreferences"));
        var selectPreferenceNum = 0;
        var selectedGUIDStr = "";
        var answer = historyAnswers[pageGUID];
        var lastSelectedPreferenceId = "";
        var lastItemGUID = "";
        if (answer == null || answer == undefined)
            answer = [];
        $(".preferences ul li").each(function (liIndex, li) {
            $(this).click(function () {
                var currentGUID = $(this).children("input[type=hidden]").val();
                var answerArrayLen = answer.length;
                var existIndex = -1;
                for (var i = 0; i < answerArrayLen; i++) {
                    var everyGUID = answer[i];
                    if (everyGUID == currentGUID) {
                        existIndex = i;
                        break;
                    }
                }
                if (maxPreferenceValue > 1) {
                    if (existIndex >= 0) {
                        $(this).removeClass("preferenceSelected");
                        itemScore[currentGUID] = 0;
                        answer.splice(existIndex, 1);
                        historyAnswers[pageGUID] = answer;
                        $(".preferences #selectedGUID").val(answer.join(";"));
                    } else {
                        if (answer.length < maxPreferenceValue) {
                            $(this).addClass("preferenceSelected");
                            itemScore[currentGUID] = 1;
                            $(".preferences ul li.preferenceSelected").css({ "cursor": "pointer" });
                            answer.push(currentGUID);
                            historyAnswers[pageGUID] = answer;
                            $(".preferences #selectedGUID").val(answer.join(";"));
                        }
                    }
                } else {
                    if (existIndex < 0) {
                        lastSelectedPreferenceId = $(".preferences ul li.preferenceSelected").attr("id");
                        lastItemGUID = $(".preferences ul li.preferenceSelected input[type=hidden]").val();
                        if (lastSelectedPreferenceId != undefined && lastSelectedPreferenceId != "" && lastSelectedPreferenceId != null) {
                            $("#" + lastSelectedPreferenceId).removeClass("preferenceSelected");
                            itemScore[lastItemGUID] = 0;
                            answer.shift();
                            historyAnswers.shift();
                            $(this).addClass("preferenceSelected");
                            itemScore[currentGUID] = 1;
                            $(".preferences ul li.preferenceSelected").css({ "cursor": "pointer" });
                            answer.push(currentGUID);
                            historyAnswers[pageGUID] = answer;
                            $(".preferences #selectedGUID").val(answer.join(";"));
                        } else {
                            $(this).addClass("preferenceSelected");
                            itemScore[currentGUID] = 1;
                            $(".preferences ul li.preferenceSelected").css({ "cursor": "pointer" });
                            answer.push(currentGUID);
                            historyAnswers[pageGUID] = answer;
                            $(".preferences #selectedGUID").val(answer.join(";"));
                        }
                    }
                }
            });
            $(this).mouseover(function () {
                $(this).find(".popupPreferenceDesc").css({ "display": "block" });
                $(this).addClass("hoverLi");
            }).mouseleave(function () {
                $(this).find(".popupPreferenceDesc").hide();
                $(this).removeClass("hoverLi");
            });
        });
    }
}