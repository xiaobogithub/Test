function toggle1(showHideDiv, switchTextDiv) {
	var ele = document.getElementById(showHideDiv);
	var text = document.getElementById(switchTextDiv);
	if(ele.style.display == "block") {
    		ele.style.display = "none";
  	}
	else {
		ele.style.display = "block";
	}
}

$(function() {
  var moveLeft = -320;
  var moveDown = 10;

  $('a#trigger').hover(function(e) {
    $('div#pop-up').show();
      //.css('top', e.pageY + moveDown)
      //.css('left', e.pageX + moveLeft)
      //.appendTo('body');
  }, function() {
    $('div#pop-up').hide();
  });

  $('a#trigger').mousemove(function(e) {
    $("div#pop-up").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
  });

});