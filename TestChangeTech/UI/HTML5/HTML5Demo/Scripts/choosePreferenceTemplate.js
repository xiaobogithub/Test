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
                    // popupDiv = "<div class='popupPreferenceDesc'><div id='pre-bubble-white'><div class='pre-bubcontent'><h1>" + nameAttr + "</h1><p>" + descriptionAttr + "</p></div><div class='bubble-white-arrow'></div></div></div>";
                    popupDiv = "<h2>" + nameAttr + "</h2> <p>" + descriptionAttr + "</p>";
                }
                if (imageAttr != undefined && imageAttr != '' && imageAttr != null) {
                    //preferenceLiStr += "<li id='preferencesLi" + preferenceIndex + "'><img src='" + imageUrl + "' alt='" + nameAttr + "' id='preferencesImg" + preferenceIndex + "' width='150px' height='120px' />" + popupDiv + "<input type='hidden' value='" + preferenceGUID + "' /></li>";
                    preferenceLiStr += "<p class='pictureversion' id='preferencesLi" + preferenceIndex + "'><input type='radio' name='' id='' value='' /><label class='tooltip' title='" + popupDiv + "' for=''><img src='" + imageUrl + "' alt='' id='preferencesImg" + preferenceIndex + "'/></label><input type='hidden' value='" + preferenceGUID + "'/></p>";
                }
            });
        }
    }
    var PreferenceStr = "<div class='preferences'>" + preferenceLiStr + '<input type="hidden" value="" id="selectedGUID" /><div class="clear"></div></div>';
    if (preferenceLiStr != '') {
        $("#yellow").append(PreferenceStr).addClass("clearmode");
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
    //var selectedPreferenceData = selectedPreferenceStr.substring(0, selectedPreferenceStr.length);
    var selectedPreferenceData = selectedPreferenceStr;
    var answerBackXml = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageGUID="' + pageGUID + '"  RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '">' +
					 '<Feedbacks><Feedback GUID="" Value="' + selectedPreferenceData + '"/></Feedbacks></XMLModel>';
    var preferencesObj = pageObj.find("Preferences");
    if ($(preferencesObj).length > 0) {
        var perferenceObj = $(preferencesObj).find("Preference");
        if ($(perferenceObj).length > 0) {
            $(perferenceObj).each(function (preferenceIndex, preference) {
                var currentGUID = $(".bubcontent .preferences #preferencesLi" + preferenceIndex + "").find("input[type=hidden]").val();
                var programVariableName = $(preference).attr("ProgramVariable");
                if (programVariableName != null && programVariableName != undefined && programVariableName != '') {
                    var provLength = proV.length;
                    for (var cc = 0; cc < provLength; cc++) {
                        if (proV[cc][0] == programVariableName) {
                            //console.log(proV[cc][0]);
                            if (itemScore[currentGUID] != undefined) {
                                proV[cc][2] = itemScore[currentGUID];
                            }
                            break;
                        }
                    }
                }
            });
        }
    }
    if (selectedPreferenceStr == "") {
        quesFlag = false;
        showTipMessage("PreferenceRequired");
    }
    if (quesFlag && selectedPreferenceStr != "") {
        $.ajax({
            //url: getReturnDataServerUrl(),
			url:getURL('submit'),
            type: 'POST',
            processData: false,
            data: answerBackXml,
            dataType: "text"
        });
        var answer = historyAnswers[pageGUID];
        itemScore[selectedPreferenceStr] = 1;
        answer=selectedPreferenceStr;
        historyAnswers[pageGUID] = answer;
    }
}
function choosePreferences() {
    if (retakeValue != "true" && retakeValue != "1") {
        var pageGUID = $(pageObj).attr("GUID");
        var maxPreferenceValue = parseInt($(pageObj).attr("MaxPreferences"));
        var answer = historyAnswers[pageGUID];
       // console.log(historyAnswers[pageGUID] + "historyAnswers");
        if (answer != "" && answer != undefined && answer != null) {
            $(".preferences #selectedGUID").val(answer);
            $(".preferences").children("input[value=" + answer + "]").siblings("input[type=radio]").attr("checked","checked");
        }
        $(".preferences .pictureversion").each(function (pIndex, pObj) {
            $(this).click(function () {
                $(this).children("input[type=radio]").attr("checked", "checked");
                $(this).siblings("p").children("input[type=radio]").removeAttr("checked");
                var currentGUID = $(this).children("input[type=hidden]").val();
                itemScore[currentGUID] = 1;
                $(this).parents(".preferences").children("#selectedGUID").val(currentGUID);             
            });
        });
    }
    $(".preferences .tooltip").tooltipster();
}