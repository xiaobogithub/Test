function resizeImg() {
    var wrapperImage = $(".wrapper-image");
    var bubWhiteWrapper = $("#bubble-white");
    var bubWhiteWrapperCon = $("#bubble-white .bubcontent");
    var bubYellowWrapper = $("#bubble-yellow");
    var bubYellowWrapperCon = $("#bubble-yellow .bubcontent");
    var bubYellowPreference = $("#bubble-yellow .bubcontent .preferences");
    var bubWhiteWrapperConHeight = bubWhiteWrapperCon.height();
    var bubYellowWrapperConHeight = bubYellowWrapperCon.height();
    /*if (bubYellowPreference != undefined && bubYellowPreference != null) {
        bubYellowWrapperConHeight = bubYellowPreference.outerHeight(true);
        console.log(bubYellowWrapperConHeight+"height");
    }*/
    var winwidth = $(window).width();
    var winheight = $(window).height();
    var whiteHeight = $("#contentElement .white").css("height");
    // bubWhiteWrapper.animate({ "height": whiteHeight + "px" },500);
    //bubYellowWrapper.animate({ "height": bubYellowWrapperConHeight + "px" },500);
}

function windowResize() {
    $(window).resize(function () {
        resizeImg();
    });
}


