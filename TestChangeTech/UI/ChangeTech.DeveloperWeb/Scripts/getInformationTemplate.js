
function loadDataForSlider(quesIndex, ques) {
    var items = [];
    var defaultScore = 0;
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != '' && captionValue != undefined) {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var minValueStr = $(ques).attr('Caption').split(';')[1];
    var minValueInsteadRoundStr = minValueStr;
    if (minValueStr != null && minValueStr != undefined && minValueStr != '') {
        var minValueInsteadStr = minValueStr.insteadVar();
        minValueInsteadRoundStr = minValueInsteadStr.parserRound();
    }
    var maxValueStr = $(ques).attr('Caption').split(';')[2];
    var maxValueInsteadRoundStr = maxValueStr;
    if (maxValueStr != null && maxValueStr != undefined && maxValueStr != '') {
        var maxValueInsteadStr = maxValueStr.insteadVar();
        maxValueInsteadRoundStr = maxValueInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var itemValue = "";
    var scoreValue = "";
    var itemGuid = "";
    var sliderItem = "";
    var sliderStr =
				   '<div class="sliders sliders' + quesIndex + '"><div>' +
		           '<div class="sliderValue' + quesIndex + '"></div>' +
			       "<div id='slider" + quesIndex + "' class='sliderForm'>" +
		           "</div></div>" +
		           '<div class="sliderValueStr' + quesIndex + '"><span class="minValueStr">' + minValueInsteadRoundStr + '</span><span class="maxValueStr">' + maxValueInsteadRoundStr + '</span></div>' +
		           "<input type='text' id='amount" + quesIndex + "' class='sliderFeedback feedbackLabel' /><input type='text' class='hiddenGuid' id='itemguid" + quesIndex + "' name='itemguid" + quesIndex + "' value='0'/></div>";
    $('#yellow').append(sliderStr);
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .sliders' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
    }
    var ItemsObj = $(ques).find("Items");
    if (ItemsObj != null && ItemsObj != undefined && ItemsObj != '') {
        var itemObj = ItemsObj.find("Item");
        if (itemObj != null && itemObj != undefined && ItemsObj != '') {
            var itemLen = $(itemObj).length;
            var maxValue = parseInt($(itemObj).eq(itemLen - 1).attr("Item"));
            var minValue = parseInt($(itemObj).eq(0).attr("Item"));
            var stepValue = (maxValue - minValue) / (itemLen - 1);
            $(itemObj).each(function (itemIndex, item) {
                sliderItem += $(item).attr("Item") + "|" + $(item).attr("Score") + "|" + $(item).attr("GUID") + ";";
                $("#yellow #itemguid" + quesIndex + "").attr("value", sliderItem);
                if ($(item).attr("Item") == $(itemObj).eq(0).attr("Item")) {
                    itemValue = $(item).attr("Item");
                    scoreValue = $(item).attr("Score");
                    itemGuid = $(item).attr("GUID");
                } // end of if($(item).attr("Item")==$(itemObj).eq(0).attr("Item"))
            }); //end item each
            if (answer != undefined && answer != null && answer != '') {
                var sliderItemGroup = sliderItem.split(";");
                var sliderItemLength = sliderItemGroup.length;
                for (var i = 0; i < sliderItemLength - 1; i++) {
                    var itemTemp = sliderItemGroup[i].split("|");
                    var everyItemScore = sliderItemGroup[i].split("|")[1];
                    var everyItemGuid = sliderItemGroup[i].split("|")[2];
                    if (retakeValue == "true" || retakeValue == "1") {
                        if (everyItemScore == answer) {
                            defaultScore = everyItemScore;
                            itemScore[quesGUID] = defaultScore;
                            GUIDValue[quesGUID] = everyItemGuid;
                        }
                    } else {
                        if (everyItemGuid == answer) {
                            defaultScore = everyItemScore;
                            itemScore[quesGUID] = defaultScore;
                            GUIDValue[quesGUID] = answer;
                        }
                    }
                }
            } else {
                itemScore[quesGUID] = scoreValue;
                GUIDValue[quesGUID] = itemGuid;
            }
            if (answer != undefined && answer != null && answer != '') {
                var sliderValue = parseInt(defaultScore);

                $('#yellow .sliders' + quesIndex + ' #amount' + quesIndex + '').attr('value', defaultScore);
                $('#yellow .sliders' + quesIndex + ' #slider' + quesIndex + '').slider({
                    value: sliderValue,
                    min: minValue,
                    max: maxValue,
                    step: stepValue,
                    slide: function (event, ui) {
                        $('#yellow #amount' + quesIndex + '').val(ui.value);
                    },
                    change: function (event, ui) { { text: ui.value + "%" } }
                });

            } else {
                var sliderValue = parseInt(minValue);
                $('#yellow #amount' + quesIndex + '').attr('value', minValue);
                $('#yellow #slider' + quesIndex + '').slider(
				 {
				     value: sliderValue,
				     min: minValue,
				     max: maxValue,
				     step: stepValue,
				     slide: function (event, ui) { $('#amount' + quesIndex + '').val(ui.value); },
				     change: function (event, ui) { { text: ui.value + "%" } }

				 });

            }

            $('.sliderValue' + quesIndex + '').append('<span class="minValue">' + minValue + '</span>');
            $('.sliderValue' + quesIndex + '').append('<span class="maxValue">' + maxValue + '</span>');
        }
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .sliders' + quesIndex + '').remove();
        }
    }
}
function onClickForSlider(pageObj) {
    var selectedItemValue = [];
    var selectedValue = 0;
    var scoreValue = "";
    var itemGuid = "";
    var itemValue = "";
    var questionsObj = pageObj.find("Questions");
    if (questionsObj != null && questionsObj != undefined && questionsObj != '') {
        var quesObj = questionsObj.find("Question");
        if (quesObj != null && quesObj != '' && quesObj != undefined) {
            $(quesObj).each(function (quesIndex, ques) {
                if ($(ques).attr("Type") == "Slider") {
                    var quesGUID = $(ques).attr("GUID");
                    var answer = historyAnswers[quesGUID];
                    var itemsObj = $(ques).find("Items");
                    if (itemsObj != undefined && itemsObj != null && itemsObj != '') {
                        var itemObj = $(itemsObj).find("Item");
                        if (itemObj != null && itemObj != '' && itemObj != undefined) {
                            var itemLen = $(itemObj).length;
                            var maxValue = parseInt($(itemObj).eq(itemLen - 1).attr("Item"));
                            var minValue = parseInt($(itemObj).eq(0).attr("Item"));
                            var stepValue = parseInt((maxValue - minValue) / (itemLen - 1));
                            $('#bubble-yellow .sliders' + quesIndex + ' #slider' + quesIndex + '').slider({
                                min: minValue,
                                max: maxValue,
                                range: 'min',
                                step: stepValue,
                                slide: function (event, ui) {
                                    $('#bubble-yellow .sliders' + quesIndex + ' #amount' + quesIndex + '').val(ui.value);
                                    selectedItemValue[quesIndex] = ui.value;
                                    $(itemObj).each(function (itemIndex, item) {
                                        itemValue = $(item).attr("Item");
                                        scoreValue = $(item).attr("Score");
                                        itemGuid = $(item).attr("GUID");
                                        if (itemValue == selectedItemValue[quesIndex]) {
                                            itemScore[quesGUID] = scoreValue;
                                            GUIDValue[quesGUID] = itemGuid;
                                        }
                                    });
                                }
                            }); // slider end

                        }
                    }
                } // type==slider end
            }); //quesObj each 
        } //quesObj  if
    } // questionsObj if
}
function loadDataForDropdownList(quesIndex, ques) {
    var optionStr = "";
    var scoreValue;
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != '' && captionValue != undefined) {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var itemsObj = $(ques).find("Items");
    if (itemsObj != null && itemsObj != '' && itemsObj != undefined) {
        var itemObj = itemsObj.find("Item");
        if (itemObj != null && itemsObj != '' && itemsObj != undefined) {
            $(itemObj).each(function (itemIndex, item) {
                var itemValue = $(item).attr("Item");
                var itemInsteadRoundStr = itemValue;
                if (itemValue != null && itemValue != undefined && itemValue != '') {
                    var itemInsteadStr = itemValue.insteadVar();
                    itemInsteadRoundStr = itemInsteadStr.parserRound();
                }
                scoreValue = $(item).attr("Score");

                var feedbackValue = $(item).attr("Feedback");
                var feedbackValueInsteadRoundStr = feedbackValue;
                var optionValue = feedbackValue;
                if (feedbackValue != null && feedbackValue != '' && feedbackValue != undefined) {
                    var feedbackValueInsteadStr = feedbackValue.insteadVar();
                    feedbackValueInsteadRoundStr = feedbackValueInsteadStr.parserRound();
                    var startIndex = feedbackValueInsteadRoundStr.indexOf("<b>");
                    var endIndex = feedbackValueInsteadRoundStr.indexOf("</b>");
                    if (startIndex != -1 && endIndex != -1) {
                        optionValue = feedbackValueInsteadRoundStr;
                    }
                }
                var itemGUID = $(item).attr("GUID");
                if (itemInsteadRoundStr != null && itemInsteadRoundStr != undefined && itemInsteadRoundStr != '') {
                    optionStr += '<option value="' + optionValue + ';' + scoreValue + ';' + itemGUID + '">' + itemInsteadRoundStr + '</option>';
                }
            });
            var dropdownListStr = '<div class="dropdownList dropdownList' + quesIndex + '">' +
	               '<span class="formw">' +
				   '<select class="selectbox" name="dropdownlistGroup' + quesIndex + '"></select>' +
			      '</span></div>';
            $('#yellow').append(dropdownListStr);
            $('#yellow .selectbox').append(optionStr);
            if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
                $('#yellow .dropdownList' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
            }
            var defaultFeedback = "";
            var answer = "";
            if (retakeValue == "true" || retakeValue == "1") {
                answer = quesProgramVariableValue
            } else {
                answer = historyAnswers[quesGUID];
            }
            if (!(answer != undefined && answer != null && answer != '')) {
                defaultFeedback = $('select[name=dropdownlistGroup' + quesIndex + ']').children('option:first-child').val().split(';')[0];
                itemScore[quesGUID] = $('select[name=dropdownlistGroup' + quesIndex + ']').children('option:first-child').val().split(';')[1];
                GUIDValue[quesGUID] = $('select[name=dropdownlistGroup' + quesIndex + ']').children('option:first-child').val().split(';')[2];
                $('select[name=dropdownlistGroup' + quesIndex + ']').children('option:first-child').attr('selected', 'selected');
            }
            if (defaultFeedback != 'undefined' && defaultFeedback != null && defaultFeedback != '') {
                var feedbackStr = '<div class="dropDownListfeedback' + quesIndex + ' feedbackLabel">' + defaultFeedback + '</div>';
                $(feedbackStr).insertAfter($('.selectbox'));
            }
        }
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .dropdownList' + quesIndex + '').remove();
        }
    }
}

function onClickForDropdownList(pageObj) {
    var questionsObj = $(pageObj).find("Questions");
    if (questionsObj != null && questionsObj != undefined) {
        var quesObj = $(questionsObj).find('Question');
        if (quesObj != null && quesObj != undefined) {
            $(quesObj).each(function (quesIndex, ques) {
                var quesGUID = $(ques).attr("GUID");
                if ($(ques).attr("Type") == "DropDownList") {
                    $('select[name=dropdownlistGroup' + quesIndex + ']').change(function () {
                        var selectedValue = $(this).val();
                        var feedback = $(this).val().split(';')[0];
                        var scoreValue = $(this).val().split(';')[1];
                        var itemGUID = $(this).val().split(';')[2];
                        if (feedback != 'undefined' && feedback != '' && feedback != null) {
                            $('.dropDownListfeedback' + quesIndex + '').html(feedback);
                        }
                        itemScore[quesGUID] = scoreValue;
                        GUIDValue[quesGUID] = itemGUID;
                    });
                }
            });
        }
    }
}
function loadDataForCheckbox(quesIndex, ques) {
    var checkbox = "";
    var feedbackArray = [];
    var feedbackStr = "";
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != undefined && captionValue != '') {
        var captionInsteadStr= captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var checkBoxStr = "<div class='check check" + quesIndex + "'><div class='checkboxrow" + quesIndex + "'></div></div>";
    var itemsObj = $(ques).find("Items");
    if (itemsObj != null && itemsObj != undefined && itemsObj != '') {
        var itemObj = itemsObj.find("Item");
        if (itemObj != '' && itemObj != undefined && itemObj != null) {
            $(itemObj).each(function (itemIndex, item) {
                var itemValue = $(item).attr("Item");
                var itemValueInsteadRoundStr = itemValue;
                if (itemValue != null && itemValue != undefined && itemValue != '') {
                    var itemValueInsteadStr = itemValue.insteadVar();
                    itemValueInsteadRoundStr = itemValueInsteadStr.parserRound();
                }
                var scoreValue = $(item).attr("Score");
                var feedbackValue = $(item).attr("Feedback");
                var feedbackValueInsteadRoundStr = feedbackValue;
                if (feedbackValue != null && feedbackValue != '' && feedbackValue != undefined) {
                    var feedbackValueInsteadStr = feedbackValue.insteadVar();
                    feedbackValueInsteadRoundStr = feedbackValueInsteadStr.parserRound();
                }
                var itemGUID = $(item).attr("GUID");
                var nonQuotFeedbackValue = "";
                if (feedbackValueInsteadRoundStr != '' && feedbackValueInsteadRoundStr != undefined && feedbackValueInsteadRoundStr != null) {
                    nonQuotFeedbackValue = feedbackValueInsteadRoundStr.replace(/\"/g, '&quot;');
                }
                if (itemValueInsteadRoundStr != null && itemValueInsteadRoundStr != undefined && itemValueInsteadRoundStr != '') {
                    checkbox += '<p class="itemRow"><input class="checkbox" type="checkbox" name="checkboxGroup' + quesIndex + '" value="' + nonQuotFeedbackValue + ';' + scoreValue + ';' + itemGUID + '"><label for="check-' + itemIndex + '">' + itemValueInsteadRoundStr + '</label></p>';
                }
            });
            $('#yellow').append(checkBoxStr);
            $('#yellow .checkboxrow' + quesIndex + '').append(checkbox);
            if (answer != '' && answer != null && answer != "undefined") {
                if (retakeValue == "true" || retakeValue == "1") {
                    saveAnswersForCheckbox(answer, quesIndex, quesGUID);
                }
            }
            if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
                $('#yellow .check' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
            }
        }
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .check' + quesIndex + '').remove();
        }
    }
}
function onClickForCheckbox(pageObj) {
    var scoreValue = 0;
    var itemGUID = "";
    var varIndex = 0;
    var feedbackStr = "";
    var answerStr = "";
    var itemGUIDValutStr = "";
    var checkGUIDValueStr = "";
    var questionsObj = $(pageObj).find("Questions");
    var quesObj = $(questionsObj).find('Question');
    $(quesObj).each(function (quesIndex, ques) {
        if ($(ques).attr("Type") == "CheckBox") {
            var quesGUID = $(ques).attr("GUID");
            var answer = historyAnswers[quesGUID];
            if (answer != undefined && answer != '' && answer != null) {
                var answerGUID = answer.split(";");
            }
            $('#bubble-yellow').find('input[name=checkboxGroup' + quesIndex + ']:checkbox').each(function (index, s) {
                var feedback = $(s).val().split(";")[0];
                var score = $(s).val().split(';')[1];
                var itemGUIDValue = $(s).val().split(';')[2];
                $(s).parents("p.itemRow").toggle(function () {
                    $(s).attr("checked","checked");
                }, function () {
                    $(s).removeAttr("checked");
                });
                $(s).parents("p.itemRow").click(function () {
                    if ($(s).attr("checked")) {
                        if (feedback != '' && feedback != 'undefined' && feedback != null) {
                            feedbackStr = '<div class="checkboxfeedback' + index + ' feedbackLabel">' + feedback + '</div>';
                            $(s).parents('.check' + quesIndex + '').append(feedbackStr);
                           /* var height = $("#bubble-yellow .bubcontent").outerHeight(true);
                            $("#bubble-yellow").css('height', height + 10 + 'px');*/
                        }
                    } else {
                        $(s).parents('.check' + quesIndex + '').children('.checkboxfeedback' + index + '').remove();
                       /* var height = $("#bubble-yellow .bubcontent").outerHeight(true);
                        $("#bubble-yellow").css('height', height + 10 + 'px');*/
                    }
                    /* itemScore[quesIndex] = scoreValue;
                    GUIDValue[quesIndex] = itemGUID.substring(0, itemGUID.length);*/
                });

            });
        }

    });
}
function saveCheckbox(pageObj) {
    var scoreValue = 0;
    var itemGUID = "";
    var pageType = pageObj.attr("Type");
    if (pageType == "Get information") {
        var questionsObj = $(pageObj).find("Questions");
        if (questionsObj != null && questionsObj != undefined && questionsObj != '') {
            var quesObj = $(questionsObj).find('Question');
            if (quesObj != null && quesObj != undefined && quesObj != '') {
                $(quesObj).each(function (quesIndex, ques) {
                    var quesGUID = $(ques).attr("GUID");
                    if ($(ques).attr("Type") == "CheckBox") {
                        $('#bubble-yellow').find('input[name=checkboxGroup' + quesIndex + ']:checkbox').each(function (index, s) {
                            var feedback = $(s).val().split(";")[0];
                            var score = $(s).val().split(';')[1];
                            var itemGUIDValue = $(s).val().split(';')[2];
                            if ($(s).attr("checked")) {
                                scoreValue = parseInt(scoreValue) + parseInt(score);
                                itemGUID += itemGUIDValue + ";";
                                itemScore[quesGUID] = scoreValue;
                                GUIDValue[quesGUID] = itemGUID.substring(0, itemGUID.length);
                            }

                        });
                    }
                });
            }
        }
    }
}
function loadDataForRadioButton(quesIndex, ques) {
    var radioButton = "";
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != '' && captionValue != null && captionValue != undefined) {
        var captionInsteadStr = captionValue.insteadVar();
       captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var itemsObj = $(ques).find("Items");
    if (itemsObj != null && itemsObj != undefined) {
        var itemObj = itemsObj.find("Item");
        if (itemObj != null && itemObj != undefined) {
            $(itemObj).each(function (itemIndex, item) {
                var itemValue = $(item).attr("Item");
                var itemInsteadRoundStr=itemValue;
                if (itemValue != '' && itemValue != null && itemValue != 'undefined') {
                    var itemValueInsteadStr = itemValue.insteadVar();
                    itemInsteadRoundStr = itemValueInsteadStr.parserRound();
                }
                var scoreValue = $(item).attr("Score");
                var feedbackValue = $(item).attr("Feedback");
                var feedbackInsteadRoundStr=feedbackValue;
                if (feedbackValue != '' && feedbackValue != null && feedbackValue != 'undefined') {
                    var feedbackValueInsteadStr = feedbackValue.insteadVar();
                    feedbackInsteadRoundStr = feedbackValueInsteadStr.parserRound();
                }
                var itemGUID = $(item).attr('GUID');
                var nonQuotFeedbackValue = "";
                if (feedbackInsteadRoundStr != '' && feedbackInsteadRoundStr != undefined && feedbackInsteadRoundStr != null) {
                    nonQuotFeedbackValue = feedbackInsteadRoundStr.replace(/\"/g, '&quot;');
                }
                radioButton += '<p class="itemRow"><input class="radioButton" type="radio" name="radioButtonGroup' + quesIndex + '" id="radio-' + itemIndex + '" value="' + nonQuotFeedbackValue + ';' + scoreValue + ';' + itemGUID + '"><label for="radio-' + itemIndex + '">' + itemInsteadRoundStr + '</label></p>';
            });
            var radioButtonStr = "<div class='radios radios" + quesIndex + "'><div class='radioButtonRow" + quesIndex + "'></div></div>";
            $('#yellow').append(radioButtonStr);
            $('#yellow .radioButtonRow' + quesIndex + '').append(radioButton);
            if (answer != null && answer != '' && answer != "undefined") {
                saveAnswersForRadioButton(answer, quesIndex, quesGUID);
            }
            if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
                $('#yellow .radios' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
            }
        }
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .radios' + quesIndex + '').remove();
        }
    }
}
function onClickRadioButton(pageObj) {

    var questionsObj = $(pageObj).find("Questions");
    if (questionsObj != undefined && questionsObj != null && questionsObj != '') {
        var quesObj = $(questionsObj).find('Question');
        var feedbackStr = "";
        if (quesObj != undefined && quesObj != null && quesObj != '') {
            $(quesObj).each(function (quesIndex, ques) {
                var type = $(ques).attr("Type");
                if (type == "RadioButton") {
                    var itemsObj = $(ques).find('Items');
                    var itemObj = $(itemsObj).find('Item');
                    var programVariable = $(ques).attr("ProgramVariable");
                    var quesGUID = $(ques).attr("GUID");
                    //$(itemObj).each(function(itemIndex,item){
                    $('#bubble-yellow').find('input[name=radioButtonGroup' + quesIndex + ']:radio').each(function (index, r) {
                        $(r).change(function () {

                            if ($(r).val() != null && $(r).val() != '' && $(r).val() != undefined) {
                                var feedbackValue = $(r).val().split(';')[0];
                                if (feedbackValue != 'undefined') {
                                    feedbackStr = '<div  class="radiofeedback feedbackLabel">' + feedbackValue + '</div>';
                                }
                                var scoreValue = $(r).val().split(';')[1];
                                var selectGUID = $(r).val().split(';')[2];
                                // $(r).parents('.row').siblings().children('.radiofeedback').remove();
                                $(r).parents('.radios').children('.radiofeedback').remove();
                                if (feedbackValue != 'undefined' && feedbackValue != null && feedbackValue != '') {
                                    $(r).parents('.radios').append(feedbackStr);
                                }
                                $(r).parents('.itemRow').siblings().children('.radioButton').attr('checked', false);
                               /* var height = $("#bubble-yellow .bubcontent").outerHeight(true);
                                $("#bubble-yellow").css('height', height + 10 + 'px');*/
                                itemScore[quesGUID] = scoreValue;
                                GUIDValue[quesGUID] = selectGUID;
                                //alert(itemScore[quesIndex]+'radioButton');
                            }
                        });

                    });
                }
            });
        }
    }
}
function loadDataForMultiLine(quesIndex, ques) {
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != undefined && captionValue != '') {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var multiplyStr = "<div class='textarea textarea" + quesIndex + "'><textarea rows='5' class='textfield textareaField'/></div>";
    $('#yellow').append(multiplyStr);
    if (answer != '' && answer != "undefined" && answer != '') {
        saveAnswersForMultipline(answer, quesIndex, quesGUID);
    }
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .textarea' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .textarea' + quesIndex + '').remove();
        }
    }
}
function getInputData(pageObj) {
    var value;
    var questionsObj = $(pageObj).find("Questions");
    var quesObj = $(questionsObj).find('Question');
    $(quesObj).each(function (quesIndex, ques) {
        var type = $(ques).attr('Type');
        var quesGUID = $(ques).attr('GUID');

        if (type == 'Multiline') {
            $('#bubble-yellow').find('.textarea' + quesIndex + '').find('textarea').change(function () {
                value = $(this).val();
                itemScore[quesGUID] = value;
                GUIDValue[quesGUID] = value;
            });
            itemScore[quesGUID] = value;
        } else if (type == 'Singleline') {
            $('#bubble-yellow').find('.singleLine' + quesIndex + '').find('input').change(function () {
                value = $(this).val();
                itemScore[quesGUID] = value;
                GUIDValue[quesGUID] = value;
            });
        }else if (type == 'Hidden singleline') {
            $('#bubble-yellow').find('.hiddenSingleLine' + quesIndex + '').find('input').change(function () {
                value = $(this).val();
                itemScore[quesGUID] = value;
                GUIDValue[quesGUID] = value;
            });
        }
    });

    //var inputtext=$('#bubble-yellow').find('textarea').val();
}
function loadDataForSingleLine(quesIndex, ques) {
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != '' && captionValue != undefined) {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var singlelineStr = "<div class='singleLine singleLine" + quesIndex + "'><input type='text'  class='textfield'/></div>";
    $('#yellow').append(singlelineStr);
    if (answer != '' && answer != "undefined" && answer != null) {
        saveAnswersForSingleline(answer, quesIndex, quesGUID);
    }
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .singleLine' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .singleLine' + quesIndex + '').remove();
        }
    }
}

function loadDataForHiddenSingleLine(quesIndex, ques) {
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != '' && captionValue != undefined) {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var hiddensinglelineStr = "<div class='hiddenSingleLine hiddenSingleLine" + quesIndex + "'><input type='password'  class='textfield'/></div>";
    $('#yellow').append(hiddensinglelineStr);
    if (answer != '' && answer != "undefined" && answer != null) {
        saveAnswersForHiddenSingleline(answer, quesIndex, quesGUID);
    }
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .hiddenSingleLine' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .hiddenSingleLine' + quesIndex + '').remove();
        }
    }
}

function loadDataForNumeric(quesIndex, ques) {
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != undefined && captionValue != '') {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer == historyAnswers[quesGUID];
    }
    var numericStr = '<div class="qbox qbox' + quesIndex + '">' +
                   '<div class="qinput">' +
                   "<input name='qty" + quesIndex + "' type='text' class='inputqty' id='qty" + quesIndex + "' value='0' maxlength='10' />" +
                   '</div>' +
                   '<div class="qbtns">' +
                   "<input class='qup'  type='button' name='add" + quesIndex + "'  value='+'/>" +
                   "<input class='qdown'  type='button' name='subtract" + quesIndex + "' value='-'/>" +
                   '</div></div>' +
                   '<div class="clear"></div>';
    $("#yellow").append(numericStr);
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .qbox' + quesIndex + '').prepend('<p>' + captionInsteadRoundStr + '</p>');
        /*if(answer == ''){
        //itemScore[quesGUID]="0";
        //GUIDValue[quesGUID]="0";
        }else{
        itemScore[quesGUID]=answer;
        GUIDValue[quesGUID]=answer;
        }*/
        if (answer != "" && answer != undefined && answer != null) {
            itemScore[quesGUID] = answer;
            GUIDValue[quesGUID] = answer;
        }
        /*else{
        itemScore[quesGUID]="0";
        GUIDValue[quesGUID]="0";
        }*/
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .qbox' + quesIndex + '').remove();
        }
    }
}
function onClickForNumeric(pageObj) {
    var questionsObj = $(pageObj).find("Questions");
    if (questionsObj != undefined && questionsObj != null && questionsObj != '') {
        var quesObj = $(questionsObj).find('Question');
        var numericValue = 0;
        if (quesObj != undefined && quesObj != null && quesObj != '') {
            $(quesObj).each(function (quesIndex, ques) {
                var type = $(ques).attr('Type');
                var quesGUID = $(ques).attr('GUID');
                // added for retake by XMX on 2011/12/1
                var quesprogramVariableName = $(ques).attr("ProgramVariable");
                var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
                /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
                var pvObj= programVariables[quesprogramVariableName];
                if(pvObj != null && pvObj != undefined){
                quesProgramVariableValue = pvObj.VariableValue;
                }
                }*/
                // end added for retake by XMX on 2011/12/1
                var answer = "";
                if (retakeValue == "true" || retakeValue == "1") {
                    answer = quesProgramVariableValue;
                } else {
                    answer = historyAnswers[quesGUID];
                }
                if (type == "Numeric") {
                    if (answer != "" && answer != undefined && answer != null) {
                        itemScore[quesGUID] = answer;
                        GUIDValue[quesGUID] = answer
                    }
                    /*else{
                    itemScore[quesGUID]="0";
                    GUIDValue[quesGUID]="0";
                    }*/
                    $("#bubble-yellow .qbox" + quesIndex + " input[name=qty" + quesIndex + "]:text").bind("change", function () {
                        if ($(this).attr("value") == '') {
                            $("#bubble-yellow .qbox" + quesIndex + "").find('input[name=qty' + quesIndex + ']:text').attr("value", "0");
                        }
                       // if (parseInt($(this).attr("value")) <= 99 && parseInt($(this).attr("value")) >= 0) {
                        if (parseInt($(this).attr("value")) >= 0) {
                            itemScore[quesGUID] = $(this).attr("value");
                            GUIDValue[quesGUID] = $(this).attr("value");
                        }
                    });
                    $("#bubble-yellow .qbox" + quesIndex + " input[name=add" + quesIndex + "]").bind("click", function () {
                       // if (document.getElementById('qty' + quesIndex + '').value < 99) {
                            numericValue = document.getElementById('qty' + quesIndex + '').value++;
                       // }
                        $("#bubble-yellow .qbox" + quesIndex + "").find('input[name=qty' + quesIndex + ']:text').attr("value", (parseInt(numericValue) + 1).toString());
                        itemScore[quesGUID] = parseInt(numericValue) + 1;
                        GUIDValue[quesGUID] = parseInt(numericValue) + 1;
                    });
                    $("#bubble-yellow .qbox" + quesIndex + " input[name=subtract" + quesIndex + "]").bind("click", function () {
                        if (document.getElementById('qty' + quesIndex + '').value > 0) {
                            numericValue = document.getElementById('qty' + quesIndex + '').value--;
                        }
                        $("#bubble-yellow .qbox" + quesIndex + "").find('input[name=qty' + quesIndex + ']:text').attr("value", (parseInt(numericValue) - 1).toString());
                        itemScore[quesGUID] = parseInt(numericValue) - 1;
                        GUIDValue[quesGUID] = parseInt(numericValue) - 1;
                    });
                }
            });
        }
    }
}
function loadDataForTimePicker(quesIndex, ques) {
    var captionText = $(ques).attr("Caption");
    var captionValue = captionText.split(";")[0];
    var captionInsteadRoundStr = captionValue;
    if (captionValue != null && captionValue != undefined && captionValue != '') {
        var captionInsteadStr = captionValue.insteadVar();
        captionInsteadRoundStr = captionInsteadStr.parserRound();
    }
    var quesGUID = $(ques).attr("GUID");
    // added for retake by XMX on 2011/12/1
    var quesprogramVariableName = $(ques).attr("ProgramVariable");
    var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
    /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
    var pvObj= programVariables[quesprogramVariableName];
    if(pvObj != null && pvObj != undefined){
    quesProgramVariableValue = pvObj.VariableValue;
    }
    }*/
    // end added for retake by XMX on 2011/12/1
    var answer = "";
    if (retakeValue == "true" || retakeValue == "1") {
        answer = quesProgramVariableValue;
    } else {
        answer = historyAnswers[quesGUID];
    }
    var setTimeStr = '<div class="timppicker' + quesIndex + ' timepicker">' +
	           '<div class="qclock qclock' + quesIndex + '">' +
        	   '<div class="qinput1"><input type="text" name="qty2' + quesIndex + '" id="qty2' + quesIndex + '" class="inputqty1" value="0" maxlength="2"/></div>' +
               '<div class="qbtns">' +
               '<input class="qup" type="button" name="qinput2Add' + quesIndex + '"  value="+"/>' +
               '<input class="qdown" type="button" name="qinput2Subtract' + quesIndex + '"  value="-"/>' +
               '</div>' +
               '<span class="divider">:</span>' +
               '<div class="qinput2"><input type="text" name="qty1' + quesIndex + '" id="qty1' + quesIndex + '" class="inputqty1" value="0" maxlength="2"/></div>' +
               '<div class="qbtns">' +
               '<input class="qup" type="button" name="qinput1Add' + quesIndex + '"  value="+"/>' +
               '<input class="qdown" type="button" name="qinput1Subtract' + quesIndex + '"  value="-"/>' +
               '</div>' +
    	       '</div>';
    $("#yellow").append(setTimeStr);
    if (answer != '' && answer != null && answer != "undefined") {
        saveAnswersForTimePicker(answer, quesIndex, quesGUID);
    }
    if (captionInsteadRoundStr != null && captionInsteadRoundStr != 'undefined' && captionInsteadRoundStr != '') {
        $('#yellow .timppicker' + quesIndex + '').prepend('<h4>' + captionInsteadRoundStr + '</h4>');
    }
    if (retakeValue == "true" || retakeValue == "1") {
        if (quesprogramVariableName == undefined || quesprogramVariableName == null && quesprogramVariableName == '') {
            $('#yellow .timppicker' + quesIndex + '').remove();
        }
    }
}
function onClickForTimePicker(pageObj) {
    var questionsObj = $(pageObj).find("Questions");
    if (questionsObj != undefined && questionsObj != null && questionsObj != '') {
        var quesObj = $(questionsObj).find('Question');
        if (quesObj != undefined && quesObj != null && quesObj != '') {
            $(quesObj).each(function (quesIndex, ques) {
                var type = $(ques).attr('Type');
                var quesGUID = $(ques).attr('GUID');
                // added for retake by XMX on 2011/12/1
                var quesprogramVariableName = $(ques).attr("ProgramVariable");
                var quesProgramVariableValue = getProgramVariableValue(quesprogramVariableName);
                /*if (quesprogramVariableName != null && quesprogramVariableName != undefined && quesprogramVariableName != '') {
                var pvObj= programVariables[quesprogramVariableName];
                if(pvObj != null && pvObj != undefined){
                quesProgramVariableValue = pvObj.VariableValue;
                }
                }*/
                // end added for retake by XMX on 2011/12/1
                var answer = "";
                if (retakeValue == "true" || retakeValue == "1") {
                    answer = quesProgramVariableValue;
                } else {
                    answer = historyAnswers[quesGUID];
                }
                if (type == "TimePicker") {
                    var mins = document.getElementById("qty1" + quesIndex + "");
                    var minsValue = 0;
                    var hoursValue = 0;
                    var allMinutes = 0;
                    mins.value = 0;
                    if (answer != "" && answer != null && answer != "undefined") {
                        hoursValue = parseInt(answer / 60);
                        minsValue = parseInt(answer % 60);
                        mins.value = minsValue;
                    }
                    $("#bubble-yellow .qclock" + quesIndex + " .qinput2 input[name=qty1" + quesIndex + "]:text").bind("change", function () {
                        if (mins.value == '') {
                            mins.value = 0;
                        } else if (mins.value > 59) {
                            mins.value = 59;
                        } else {
                            mins.value = $(this).attr("value");
                        }
                        minsValue = mins.value;
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });

                    $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput1Add" + quesIndex + "]").bind("click", function () {
                        if (mins.value == 59) {
                            mins.value = 0;
                        } else {
                            mins.value++;
                        }
                        minsValue = mins.value;
                        $("#bubble-yellow .timppicker" + quesIndex + "").find('input[name=qty1' + quesIndex + ']:text').attr("value", parseInt(minsValue));
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });
                    $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput1Subtract" + quesIndex + "]").bind("click", function () {
                        if (mins.value == 0) {
                            mins.value = 59;
                        } else {
                            mins.value--;
                        }
                        minsValue = mins.value;
                        $("#bubble-yellow .timppicker" + quesIndex + "").find('input[name=qty1' + quesIndex + ']:text').attr("value", parseInt(minsValue));
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });

                    var hours = document.getElementById("qty2" + quesIndex + "");
                    hours.value = 0;
                    if (answer != "" && answer != null && answer != "undefined") {
                        hoursValue = parseInt(answer / 60);
                        minsValue = parseInt(answer % 60);
                        hours.value = hoursValue;
                    }
                    $("#bubble-yellow .qclock" + quesIndex + " .qinput1 input[name=qty2" + quesIndex + "]:text").bind("change", function () {
                        if (hours.value == '') {
                            hours.value = 0;
                        } else if (hours.value > 23) {
                            hours.value = 23;
                        } else {
                            hours.value = $(this).attr("value");
                        }
                        hoursValue = hours.value;
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });
                    $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput2Add" + quesIndex + "]").bind("click", function () {
                        if (hours.value == 23) {
                            hours.value = 0;
                        } else {
                            hours.value++;
                        }
                        hoursValue = hours.value;
                        $("#bubble-yellow .timppicker" + quesIndex + "").find('input[name=qty2' + quesIndex + ']:text').attr("value", parseInt(hoursValue));
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });
                    $("#bubble-yellow .qclock" + quesIndex + " input[name=qinput2Subtract" + quesIndex + "]").bind("click", function () {
                        if (hours.value == 0) {
                            hours.value = 23;
                        } else {
                            hours.value--;
                        }
                        hoursValue = hours.value;
                        $("#bubble-yellow .timppicker" + quesIndex + "").find('input[name=qty2' + quesIndex + ']:text').attr("value", parseInt(hoursValue));
                        allMinutes = parseInt(hoursValue) * 60 + parseInt(minsValue);
                        itemScore[quesGUID] = allMinutes;
                        GUIDValue[quesGUID] = allMinutes;
                    });


                }
            });
        }
    }
}

function loadDataForGetInformationTemplate(pageObj) {
    var questionsObj = pageObj.find("Questions");
    if (questionsObj != null && questionsObj != undefined) {
        var quesObj = questionsObj.find("Question");
        if (quesObj != null && quesObj != undefined) {
            $(quesObj).each(function (quesIndex, ques) {
                // var captionText = $(ques).attr("Caption");
                // var captionValue = captionText.split(";")[0];
                // if (captionValue != null && captionValue != 'undefined' && captionValue != '') {
                //     $('#yellow').append('<p>' + captionValue + '</p>');
                // }
                var quesType = $(ques).attr("Type");
                var isRequired = $(ques).attr("IsRequired");
                var programVariable = $(ques).attr('ProgramVariable');
                if (quesType != null && quesType != '' && quesType != undefined) {
                    if (quesType == "Slider") {
                        loadDataForSlider(quesIndex, ques);
                    } // questiong type is slider
                    else if (quesType == "DropDownList") {
                        loadDataForDropdownList(quesIndex, ques);
                    } //question type is DropdownList
                    else if (quesType == "CheckBox") {
                        loadDataForCheckbox(quesIndex, ques);
                    } // question type is CheckBox
                    else if (quesType == "RadioButton") {
                        loadDataForRadioButton(quesIndex, ques);
                    } else if (quesType == "Multiline") {
                        loadDataForMultiLine(quesIndex, ques);
                    } else if (quesType == "Singleline") {
                        loadDataForSingleLine(quesIndex, ques);
                    } else if (quesType == "Hidden singleline") {
                        loadDataForHiddenSingleLine(quesIndex, ques);
                    } else if (quesType == "Numeric") {
                        loadDataForNumeric(quesIndex, ques);
                    } else if (quesType == "TimePicker") {
                        loadDataForTimePicker(quesIndex, ques);
                    }
                }
            });
        }
    }
}

function getItemInfoByItemGUID(ques, quesIndex) {
    var quesGUID = ques.attr("GUID");
    var itemsObj = ques.find("Items");
    if (itemsObj != '' && itemsObj != undefined && itemsObj != null) {
        var itemObj = itemsObj.find("Item");
        if (itemObj != '' && itemObj != undefined && itemObj != null) {
            itemObj.each(function (index, item) {
                var feedback = $(item).attr("Feedback");
                var score = $(item).attr("Score");
                var item = $(item).attr("Item");
                var itemGUID = $(item).attr("GUID");
                itemsInfo[itemGUID] = { 'Feedback': feedback, 'Score': score, 'Item': item };
            });

        }
    }
}
