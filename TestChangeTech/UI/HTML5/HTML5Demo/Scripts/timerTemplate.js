function loadDataForTimerTemplate(pageObj) {
    var timerStr = '<div class="clock1" style="margin: 0 auto;"></div>' +
		         ' <script type="text/javascript">' +
		         '$(function() {' +
			    '$(".clock1").stopwatch();' +
		        '});' +
	             '</script>';
    $("#yellow").html(timerStr);
}
function localStopWatch() {
    $("#bubble-yellow .clock1").stopwatch();
    var yellowBubContHeight = $("#bubble-yellw .bubcontent").outerHeight(true);
    $("#bubble-yellow").css({ "height": yellowBubContHeight + "px" });
}
function getStopWatchData(XMLobj) {
    quesFlag = true;
    var userGUID = $(XMLobj).attr('UserGUID');
    var programGUID = $(XMLobj).attr('ProgramGUID');
    var sessionGUID = $(XMLobj).find('Session').attr('GUID');
    //var pageGUID = $( pageObj ).attr( 'GUID' );
    var pageGUID = $( oraiRoot ).find( 'PageSequence' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == sequenceOrder } ).find( 'Page' ).filter( function () { return parseInt( $( this ).attr( 'Order' ) ) == pageOrder } ).attr( 'GUID' );
    var pageSequenceOrder = sequenceOrder;
    var pageSequenceGUID = $( oraiRoot ).find( "PageSequence" ).filter( function () { return parseInt( $( this ).attr( "Order" ) ) == sequenceOrder } ).attr( "GUID" );
    var relapsePageGUID = "";
    var relapsePageSequenceGUID = "";
    if ( sessionName == "relapse" || isNaN( sequenceOrder ) || sequenceOrder == "" )
    {
       
        pageSequenceOrder = "";
        relapsePageGUID = $(pageObj).attr("GUID");
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
    var hours = $("#bubble-yellow .clock1  .display").find("span.hr").text();
    var minus = $("#bubble-yellow .clock1  .display").find("span.min").text();
    var seconds = $("#bubble-yellow .clock1  .display").find("span.sec").text();
    var sumSecsForHour = transactionHoursToSeconds(hours);
    var sumSecsForMin = transactionMinusToSeconds(minus);
    var sumSecsForSec = calculateSeconds(seconds);
    var sumSeconds = parseInt(sumSecsForHour) + parseInt(sumSecsForMin) + parseInt(sumSecsForSec);
    var answerBackXml = '<XMLModel UserGUID="' + userGUID + '" ProgramGUID="' + programGUID + '" SessionGUID="' + sessionGUID + '" PageSequenceOrder="' + pageSequenceOrder + '" PageGUID="' + pageGUID + '" RelapsePageSequenceGUID="' + relapsePageSequenceGUID + '" RelapsePageGUID="' + relapsePageGUID + '">' +
					 '<Feedbacks><Feedback GUID="" Value="' + sumSeconds + '" /></Feedbacks></XMLModel>';
    // if(retakeValue != "true" && retakeValue != "1") is added for retake by XMX on 2011/12/1
    if (retakeValue != "true" && retakeValue != "1") {
        var programVariableValue = pageObj.attr("ProgramVariable");
        if (programVariableValue != null && programVariableValue != undefined && programVariableValue != '') {
            var provLength = proV.length;
            for (var i = 0; i < provLength; i++) {
                if (proV[i][0] == programVariableValue) {
                    proV[i][2] = sumSeconds;
                    break;
                }
            }
        }
        if (($("#bubble-yellow .clock1 input.start").css("display") == 'none') || sumSeconds == 0) {
            quesFlag = false;
            showTipMessage("TimerRequired");
        }
        if (quesFlag && sumSeconds > 0) {
            $.ajax({
                //url: getReturnDataServerUrl(),
				url:getURL('submit'),
                type: 'POST',
                processData: false,
                data: answerBackXml,
                dataType: "text"
            });
        }

    } // end for if(retakeValue != "true" && retakeValue != '1') added for retake by XMX on 2011/12/01

}
function transactionHoursToSeconds(hours) {

    // 个位
    var units = hours.substr(0, 1);
    //十位
    var tens = hours.substr(1);
    var hoursToSec = 0;
    if (tens == "0") {
        hoursToSec = parseInt(units) * 3600;
    } else {
        hoursToSec = parseInt(hours) * 3600;
    }
    return hoursToSec;
}
function transactionMinusToSeconds(minutes) {
    // 个位
    var units = minutes.substr(0, 1);
    //十位
    var tens = minutes.substr(1);
    var minustoSec = 0;
    if (tens == "0") {
        minustoSec = parseInt(units) * 60;
    } else {
        minustoSec = parseInt(minutes) * 60;
    }
    return minustoSec;

}
function calculateSeconds(seconds) {
    // 个位
    var units = seconds.substr(0, 1);
    //十位
    var tens = seconds.substr(1);
    var secsToSec = 0;
    if (tens == "0") {
        secsToSec = parseInt(units);
    } else {
        secsToSec = parseInt(seconds);
    }
    return secsToSec;
}