function LoadOrderContent(languageGuid,orderTypeGuid,orderContents) {
    $("#ordersystem").ordercontent({
        languageGuid: languageGuid,
        orderContents: orderContents,
        orderTypeGuid: orderTypeGuid
    });
}


$.fn.numeral = function () {
    this.css("ime-mode", "disabled");
    this.bind("keypress", function () {
        if ( this.value.length <= 4) //Up Down Left Right.
            return true;
        else
            return false;
    });
    this.bind("keydown", function () {
        if (window.event.keyCode > 46 && window.event.keyCode <= 57 //Number key.
        || window.event.keyCode >= 96 && window.event.keyCode <= 105
        || window.event.keyCode == 8 //BackSpace.
        || window.event.keyCode >= 37 && window.event.keyCode <= 40) //Up Down Left Right.
            return true;
        else
            return false;
    });
    this.bind("blur", function () {
        if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
            this.value = this.value.substr(0, this.value.length - 1);
        } else if (isNaN(this.value)) {
            this.value = "";
        }
        else if (this.value.length>4) {
            this.value = this.value.substr(0, 5); //maxLength is 5.
        }
    });
    this.bind("paste", function () {
        var s = clipboardData.getData('text');
        if (!/\D/.test(s));
        value = s.replace(/^0*/, '');
        return false;
    });
    this.bind("dragenter", function () {
        return false;
    });
    this.bind("keyup", function () {
        if (/(^0+)/.test(this.value)) {
            this.value = this.value.replace(/^0*/, '');
        }
    });
};


