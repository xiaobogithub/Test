jQuery(function($) {

	var hostImg = $("#backgroundpic");
	var bub = $("#bubbles");
	var main = $("#mainarea");
	var vid = $("#videobox");	
	var bgImg = $("#fullbgImg");
	
	var bubheight = bub.height()/2+50;
 
    function resizeImg() {
		
	  var winwidth = $(window).width();
      var winheight = $(window).height();
	  
      var imgwidth = hostImg.width();
      var imgheight = hostImg.height();

      var widthratio = winwidth / imgwidth;
      var heightratio = winheight / imgheight;
 
      var widthdiff = heightratio * imgwidth;
      var heightdiff = widthratio * imgheight;
	  	  
	  var bgimgwidth = bgImg.width();
      var bgimgheight = bgImg.height();
	  
	  var bgwidthratio = winwidth / bgimgwidth;
      var bgheightratio = winheight / bgimgheight;
	  
	  var bgwidthdiff = bgheightratio * bgimgwidth;
      var bgheightdiff = bgwidthratio * bgimgheight;
 
      //var ratio=widthratio<heightratio ? widthratio : heightratio;
	 // var actualImageWidth=imgwidth*ratio;
	 // var actualImageHeight=imgheight*ratio;
	 // hostImg.css({width: actualImageWidth+'px', height: actualImageHeight+'px'});
	  if (heightdiff<winheight) { hostImg.css({width: winwidth+'px', height: heightdiff+'px' }); } 
	  else { hostImg.css({width: widthdiff+'px', height: winheight+'px' }); }
	  
	  if (bgheightdiff>winheight) { bgImg.css({width: winwidth+'px', height: bgheightdiff+'px' }); } 
	  else { bgImg.css({width: bgwidthdiff+'px', height: winheight+'px' }); }
	  
	  //bub.css({top: winheight/2-bubheight+'px'});
	  vid.css({top: winheight/8+'px'});
	  main.css({ height: winheight-46+'px' });
	  if(navigator.userAgent.toLowerCase().match(/iPhone/i)!= null) {
	    bub.css({ top: 160+'px' });
	    hostImg.css({height: 260+'px', width: imgwidth/(imgheight/260)+'px', top: 46+'px' });
	    if(bgImg.height()==winheight) {
	    	bgImg.css({height: bgImg.height()+60+'px'});
	    }
	    
	    //main.css({ height: 160+$('#bubble-white').outerHeight()+$('#bubble-yellow').outerHeight()+$('#bubble-blue').outerHeight()+'px' });
	  	//$('#page_wrapper').css({ height: main.outerHeight()+46+'px' });
	  }
	  else {
	  	bub.css({ top: (winheight-60)/6+'px' });
	  }
    }
	
    resizeImg();
    $(window).resize(function() {
      resizeImg();
      alert('width:'+bgimgwidth+' - height:'+bgimgheight);
    }); 
});

