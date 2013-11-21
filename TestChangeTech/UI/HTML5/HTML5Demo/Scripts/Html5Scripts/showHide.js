$(function() {
  $(document).ready(function(){
  $("#hide").click(function(){
    $(".controlbar").css('marginTop', -37);
	$('.controlbar-mini').css('marginTop', 0);
	$('.button-expand').css('marginTop', 0);
	$('.button-contract').css('marginTop', -9999);
	$('.wrapper-image').css('marginTop', 0);
  });
  $("#show").click(function(){
	$(".controlbar").css('marginTop', 0);
	$('.controlbar-mini').css('marginTop', 40);
	$('.button-expand').css('marginTop', -9999);
	$('.button-contract').css('marginTop', 0);
	$('.wrapper-image').css('marginTop', 20);
	$('.wrapper-image.illustrationmode').css('marginTop', 40);
	$('.wrapper-image.fullscreenmode').css('marginTop', 0);
  });
});

});