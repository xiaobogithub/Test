$(function () {
    $(document).ready(function () {
        $("#hide").click(function (e) {
            e.preventDefault();
            $(".controlbar").css('marginTop', -37);
            $('.controlbar-mini').css('marginTop', 0);
            $('.button-expand').css('marginTop', 0);
            $('.button-contract').css('marginTop', -9999);
            $('.wrapper-image').css('marginTop', 0);
        });
        $("#show").click(function (e) {
            e.preventDefault();
            $(".controlbar").css('marginTop', 0);
            $('.controlbar-mini').css('marginTop', 40);
            $('.button-expand').css('marginTop', -9999);
            $('.button-contract').css('marginTop', 0);
            $('.wrapper-image').css('marginTop', 20);
            $('.wrapper-image.illustrationmode').css('marginTop', 40);
            $('.wrapper-image.fullscreenmode').css('marginTop', 0);
        });
        window.onresize = function () {
            if ($(".controlbar-mini").css('display') == 'none') {
                $('.controlbar').css('margin-top', 0);
            } else if ($(".controlbar-mini").css('display') == 'block') {
                if (parseInt($(".controlbar-mini").css('margin-top')) == 0) {
                    $('.controlbar').css('margin-top', -37);
                } else {
                    $('.controlbar').css('margin-top', 0);
                }
            }
        }
    });

});