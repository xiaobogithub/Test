function validateMethod() {
    $("#validateLink").attr("disabled", "disabled");
    var errorMessageValidateFailed = "Tyvärr kunde vi inte hitta en Sjukvårdsförsäkring på detta personnummeret.";
    var errorMessageMoreThan18 = "Tillgång kräver att du är över 18 år.";
    var personNum = $("#validateCode").val();
    var reg = /^\b(((20)((0[0-9])|(1[0-2])))|(([1][^0-8])?\d{2}))((0[1-9])|1[0-2])((0[1-9])|(1[0-9])|(2[0-9])|(3[01]))[-+]?\d{4}?\b$/;
    //var reg1 = /^(?:(?!0000)[0-9]{4}(?:(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)-\d{4}$/;
    //var reg2 = /^(?:(?!0000)[0-9]{4}(?:(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)\d{4}$/;
    if (personNum.length == 4) {
        ajaxValidatePersonNum(personNum);
    }
    else if (reg.test(personNum)) {
        var dateYear = personNum.substr(0, 4);
        var currentYear = new Date().getFullYear();
        if (currentYear - dateYear >= 18) {
            ajaxValidatePersonNum(personNum);
        }
        else {
            $("#alertmessage").attr("class", "alertmessage");
            $("#alertmessage").text(errorMessageMoreThan18);
            $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
            if ($("#validateLink").attr("disabled") == "disabled") {
                $("#validateLink").removeAttr("disabled");
            }
            window.setTimeout(function () { $("#alertmessage").attr("class", "alertmessage hidden"); }, 3000);
        }
    }
    else {
        $("#alertmessage").attr("class", "alertmessage");
        $("#alertmessage").text(errorMessageValidateFailed);
        $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
        if ($("#validateLink").attr("disabled") == "disabled") {
            $("#validateLink").removeAttr("disabled");
        }
        window.setTimeout(function () { $("#alertmessage").attr("class", "alertmessage hidden"); }, 3000);
    }

}

function beforeValidateCodeMethod() {
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/AuthenticateHandler.ashx',
        cache: false,
        type: 'Post',
        async: true,
        data: { validateCode: $("#validateCode").val() },
        success: function (data) {
            var dialoguebox = "";
            var errorMessageValidateFailed = "Tyvärr kunde vi inte hitta en Sjukvårdsförsäkring på detta personnummeret.";
            var errorMessageMoreThan18 = "Tillgång kräver att du är över 18 år.";
            var personNum = $("#validateCode").val();
            var reg = /^\b(((20)((0[0-9])|(1[0-2])))|(([1][^0-8])?\d{2}))((0[1-9])|1[0-2])((0[1-9])|(1[0-9])|(2[0-9])|(3[01]))[-+]?\d{4}?\b$/;
            //            var reg1 = /^(?:(?!0000)[0-9]{4}(?:(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)-\d{4}$/;
            //            var reg2 = /^(?:(?!0000)[0-9]{4}(?:(?:0[1-9]|1[0-2])(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)\d{4}$/;
            if (personNum.length==4) {
                if (data == "success") {
                    RedirectToProgramLink();
                }
                else {
                    dialoguebox = $("#wrapper-dialoguebox")[0];
                    if (dialoguebox.style.display != "block") {
                        dialoguebox.style.display = "block";
                        $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
                    }
                }
            }
            else if (reg.test(personNum)) {
                var dateYear = personNum.substr(0, 4);
                var currentYear = new Date().getFullYear();
                if (currentYear - dateYear >= 18) {
                    $("#alertmessage").attr("class", "alertmessage hidden");
                    $("#alertmessage").text(errorMessageValidateFailed);
                    if (data == "success") {
                        RedirectToProgramLink();
                    }
                    else {
                        dialoguebox = $("#wrapper-dialoguebox")[0];
                        if (dialoguebox.style.display != "block") {
                            dialoguebox.style.display = "block";
                            $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
                        }
                    }
                }
                else {
                    dialoguebox = $("#wrapper-dialoguebox")[0];
                    if (dialoguebox.style.display != "block") {
                        dialoguebox.style.display = "block";
                        $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
                    }
                }
            } else {
                if (data == "success") {
                    RedirectToProgramLink();
                }
                else {
                    dialoguebox = $("#wrapper-dialoguebox")[0];
                    if (dialoguebox.style.display != "block") {
                        dialoguebox.style.display = "block";
                        $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
                    }
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
}

function ajaxValidatePersonNum(personNum) {
    $.ajax({
        url: window.location.protocol + '//' + window.location.host + '/Handler/AuthenticateHandler.ashx',
        cache: false,
        type: 'Post',
        async: true,
        data: { validateCode: personNum },
        success: function (data) {
            var errorMessageValidateFailed = "Tyvärr kunde vi inte hitta en Sjukvårdsförsäkring på detta personnummeret.";
            var errorMessageMoreThan18 = "Tillgång kräver att du är över 18 år.";
            if (data == "success") {
                $("#alertmessage").attr("class", "alertmessage hidden")
                RedirectToProgramLink();
            }
            else {
                var dialoguebox = $("#wrapper-dialoguebox")[0];
                if (dialoguebox.style.display != "block") {
                    dialoguebox.style.display = "block";
                }
                $("#alertmessage").attr("class", "alertmessage");
                $("#alertmessage").text(errorMessageValidateFailed);
                $("#validateCode").val("ÅÅÅÅMMDD-9999").css("color", "gray");
                if ($("#validateLink").attr("disabled") == "disabled") {
                    $("#validateLink").removeAttr("disabled");
                }
                window.setTimeout(function () { $("#alertmessage").attr("class", "alertmessage hidden"); }, 3000);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status + ", " + thrownError);
        }
    });
}



function RedirectToProgramLink() {
    switch ($("#actionLink").attr("program")) {
        case "stressemindre":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=86822N";
            break;
        case "slutaroka":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=LPDRVX";
            break;
        case "minskaalkohol":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=208D4V";
            break;
        case "mabattre":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=4DJ48P";
            break;
        case "litesundare":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=8V20F8";
            break;
        case "halsoprofil":
            window.location.href = "https://program.changetech.no/ChangeTech.html?Mode=Trial&P=4FX26V";
            break;
    }
}

function HtmlToPDF() {
    var currentUrl = window.location.href;
    if (confirm("Tryck OK för att spara din hälsoprofil.")) {
        window.location.href = window.location.protocol + '//' + window.location.host + '/Handler/ConvertToPdfHandler.ashx?requestUrl=' + currentUrl;
    }
    
//    $.ajax({
//        url: window.location.protocol + '//' + window.location.host + '/Handler/ConvertToPdfHandler.ashx',
//        cache: false,
//        type: 'Post',
//        async: true,
//        data: { CurrentUrl: currentUrl },
//        success: function (data) {
//            if (data == "success") {
//                alert("Converted to the PDF version successful !");
//            }
//            else {
//                alert("Converted to the PDF version fail !");
//            }
//        },
//        error: function (xhr, ajaxOptions, thrownError) {
//            alert(xhr.status + ", " + thrownError);
//        }
//    });
}

$(function () {
    $("#btnHtmlToPdf").click(HtmlToPDF);
    var codeFormat = "ÅÅÅÅMMDD-9999";
    var validateCode = $("#validateCode");
    validateCode.focus(function () {
        if (validateCode.val() == codeFormat) {
            validateCode.val("");
        }
    }).blur(function () {
        if (validateCode.val().length <= 0) {
            validateCode.val(codeFormat).css("color", "gray");
        }
    });

//    validateCode.keyup(function (event) {
//        var keyCode = event.which;
//        var personNum = validateCode.val();
//        var cursorPosition = $(this).position();
//        switch (cursorPosition) {
//            case 0:
//                $(this).val(codeFormat);
//                $(this).position(0)
//                break;
//            case 1:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "ÅÅÅMMDD-9999");
//                break;
//            case 2:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "ÅÅMMDD-9999");
//                break;
//            case 3:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "ÅMMDD-9999");
//                break;
//            case 4:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "MMDD-9999");
//                break;
//            case 5:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "MDD-9999");
//                break;
//            case 6:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "DD-9999");
//                break;
//            case 7:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "D-9999");
//                break;
//            case 8: // add "-"
//                if (keyCode == 8) {
//                    ChangeInputMessagePos8($(this), cursorPosition, cursorPosition + 1, "-9999", keyCode);
//                }
//                else {
//                    ChangeInputMessage($(this), cursorPosition, cursorPosition + 1, "-9999");
//                }
//                break;
//            case 9:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition - 1, "9999");
//                break;
//            case 10:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "999");
//                break;
//            case 11:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "99");
//                break;
//            case 12:
//                ChangeInputMessage($(this), cursorPosition, cursorPosition, "9");
//                break;
//            default:
//                ChangeInputMessage($(this), 13, 13, "");
//                break;
//        }
//    });
})

//reminder end-user input content.
function ChangeInputMessage(object, contentLength, surcorPos, fillContent) {
    var personNum = $("#validateCode").val();
    var personNumBak = $("#validateCode").val();
    personNum = personNum.substr(0, contentLength);
    personNum = personNum.replace(personNum, personNum + fillContent);
    object.val(personNum);
    if (contentLength == 9 && personNum.substr(8, 1) == "-") {
        object.position(surcorPos + 1);
    }
    else {
        if (contentLength == 8 && object.val().substr(8, 1) != "-") {
            personNum = $("#validateCode").val();
            personNum = personNum.substr(0, contentLength);
            personNum = personNum.replace(personNum, personNum + fillContent);
            object.val(personNum);
            object.position(surcorPos-1);
        }
        else {
            object.position(surcorPos);
        }
    }
}

function ChangeInputMessagePos8(object, contentLength, surcorPos, fillContent,keycode) {
    var personNum = $("#validateCode").val();
    var personNumBak = $("#validateCode").val();
    personNum = personNum.substr(0, contentLength);
    personNum = personNum.replace(personNum, personNum + fillContent);
    object.val(personNum);
    if (contentLength == 9 && personNum.substr(8, 1) == "-") {
        object.position(surcorPos + 1);
    }
    else {
        if (contentLength == 8 && keycode==8) {
            personNum = $("#validateCode").val();
            personNum = personNum.substr(0, contentLength);
            personNum = personNum.replace(personNum, personNum + fillContent);
            object.val(personNum);
            object.position(surcorPos - 1);
        }
        else {
            object.position(surcorPos);
        }
    }
}

$.fn.extend({
    position: function (value) {
        var elem = this[0];
        if (elem && (elem.tagName == "TEXTAREA" || elem.type.toLowerCase() == "text")) {
            if ($.browser.msie) {
                var rng;
                if (elem.tagName == "TEXTAREA") {
                    rng = event.srcElement.createTextRange();
                    rng.moveToPoint(event.x, event.y);
                } else {
                    rng = document.selection.createRange();
                }
                if (value === undefined) {
                    rng.moveStart("character", -event.srcElement.value.length);
                    return rng.text.length;
                } else if (typeof value === "number") {
                    var index = this.position();
                    index > value ? (rng.moveEnd("character", value - index)) : (rng.moveStart("character", value - index));
                    rng.select();
                }
            } else {
                if (value === undefined) {
                    return elem.selectionStart;
                } else if (typeof value === "number") {
                    elem.selectionEnd = value;
                    elem.selectionStart = value;
                }
            }
        } else {
            if (value === undefined)
                return undefined;
        }
    }
})