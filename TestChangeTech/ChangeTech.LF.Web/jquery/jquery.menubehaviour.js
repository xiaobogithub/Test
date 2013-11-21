$(document).ready(function(){

var about = $('#about');

$(window).scroll(function(){
	
  if ($(window).scrollTop() >= $('#about').offset().top + 50)
  {
    
		
		$('div#wrapper-sitenav-large').css({'margin-top': '-60px'});
		$('div#pop-submenu').css({'margin-top': '38px'});
		$("div.logosalad").hide();
	
  }
  
  else if ($(window).scrollTop() <= $('#about').offset().top + 50)  
  {
	$('div#wrapper-sitenav-large').css({'margin-top': '0px'});
		$('div#pop-submenu').css({'margin-top': '98px'});
		$("div.logosalad").show();
	
  }
  
});

});