/**
 * @author Ole Hartvig
 */
function Step1() {

	//Picture and bubbles

	$('#pic').html('<img src="img/hostpic-01.png" width="368" height="596" id="backgroundpic">');
	$('img#backgroundpic').hide();
	$('img#backgroundpic').load(function() {
		$('img#backgroundpic').hide().delay(400).fadeIn('slow');
	});
	setWhiteBubble('#white1');
	setYellowBubble('');
	setBlueBubble('#blue1');

	//Misc
	$('body').removeClass("bgblack").addClass("bggradient");
	$('#bubbles').addClass("centerright");
	$.getScript("js/custom.js");
setTimeout(loaded, 100);
					//Navigation
	$('.backbtn').unbind();//.click(function() {Step1();});
	$('#bubble-blue').unbind().click(function() {Step2();});
}
function Step2() {

	//Picture and bubbles
	$('#img#backgroundpic').hide();
	if($('#pic img').attr('src')!='img/hostpic-01.png')	{
		$('#pic').hide();
		$('#pic').html('<img src="img/hostpic-01.png" width="368" height="596" id="backgroundpic">');
		$('img#backgroundpic').load(function() {
			$('img#backgroundpic').delay(400).fadeIn('slow');
			$('#pic').delay(400).fadeIn('slow');
		});
	}

	setWhiteBubble('#white2');
	setYellowBubble('#yellow2');
	setBlueBubble('#blue1');

	$("#bubble-yellow #slider").slider({
		value : 100,
		min : 0,
		max : 500,
		step : 50,
		slide : function(event, ui) {
			$("#bubble-yellow #amount").val("$" + ui.value);
		}
	});
	$("#amount").val("$" + $("#bubble-yellow #slider").slider("value"));
	
	//Misc
	$('body').removeClass("bgblack").addClass("bggradient");
	$('#bubbles').addClass("centerright");
	$.getScript("js/custom.js");
setTimeout(loaded, 100);
					//Navigation
	$('.backbtn').unbind().click(function() {setYellowBubble('FadeOut');Step1();});
	$('#bubble-blue').unbind().click(function() {Step3();});
};
function Step3() {

	//Picture and bubbles
	$('#pic').hide();
	$('img#fullbgImg').hide();
	$('#pic').html('<img src="img/fullscreen-pic.jpg" class="fullbg" id="fullbgImg">');
	$('img#fullbgImg').load(function() {
		$.getScript("js/custom.js");
		$('#pic').delay(400).fadeIn('slow');
		$('img#fullbgImg').delay(400).fadeIn('slow');
	});

	//Misc
	$('body').removeClass("bgblack").addClass("bggradient");
	if(navigator.userAgent.toLowerCase().match(/iPhone/i)== null) {
		$('#bubbles').removeClass("centerright").animate({left: '0px'},500).addClass("center");
	}
	$.getScript("js/custom.js");

	setWhiteBubble('#white3');
	setYellowBubble('#yellow2');
	setBlueBubble('#blue1');
setTimeout(loaded, 100);
					//Navigation
	$('.backbtn').unbind().click(function() {if(navigator.userAgent.toLowerCase().match(/iPhone/i)== null) {$('#bubbles').animate({left: '250px'},500).addClass("centerright");}Step2();});
	$('#bubble-blue').unbind().click(function() {Step4();});
}
function Step4() {

	//Picture and bubbles
	$('#pic').hide();
	
	setWhiteBubble('#white1');
	setYellowBubble('FadeOut');
	setBlueBubble('#blue1');
	
	//Misc
	$('body').removeClass("bgblack").addClass("bggradient");
	$('#bubbles').removeClass("centerright").addClass("center");
	$.getScript("js/custom.js");
setTimeout(loaded, 100);
					//Navigation
	$('.backbtn').unbind().click(function() {Step3();});
	$('#bubble-blue').unbind().click(function() {Step5();});
}
function Step5() {

	//Picture and bubbles
	$('#pic').hide();

	setWhiteBubble('FadeOut');
	setYellowBubble('');
	setBlueBubble('FadeOut');

	$('#mainarea').load('vid.html', function() {
		$('#video').gVideo({
			childtheme : 'smalldark'
		});
		$.getScript("js/custom.js");
	});
	//Misc
	$('body').addClass("bgblack");

	$('<link>').appendTo('head').attr({
		rel : 'stylesheet',
		type : 'text/css',
		href : 'css/ghindaVideoPlayer.css'
					});
setTimeout(loaded, 100);
					//Navigation
	$('.backbtn').unbind().click(function() {
		$('#mainarea').load('bub.html', function() { 
			setWhiteBubble('#white1');
			setYellowBubble('');
			setBlueBubble('#blue1');
			$('body').removeClass("bgblack").addClass("bggradient");
			//$('body').css('background','none');
			$('#bubbles').removeClass("centerright").animate({left: '0px'},500).addClass("center");
			$.getScript("js/custom.js");
		});
		Step4();
	});
	//$('#bubble-blue').unbind().click(function() {Step6();});
}