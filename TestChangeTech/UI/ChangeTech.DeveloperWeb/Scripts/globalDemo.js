/*parse XML*/
loadXML = function(fileRoute){
	var xmlDoc=null;
	 if (window.ActiveXObject){
			xmlDoc = new ActiveXObject('Msxml2.DOMDocument');
			xmlDoc.async=false;
			xmlDoc.load(fileRoute);
	}
	else if (document.implementation && document.implementation.createDocument){
			var xmlhttp = new window.XMLHttpRequest();
			xmlhttp.open("GET",fileRoute,false);
			xmlhttp.send(null);
			var xmlDoc = xmlhttp.responseXML.documentElement;
	}
	else {xmlDoc=null;}
	return xmlDoc;
}
/// When PresenterImagePosition on Right
function appendContetPresentImageOnRight(title,text,footerText,presenterImagePosition){
    if($('#bubbles').find("#bubble-white").length>0){
       $('#bubbles').find("#bubble-white").remove(); 
	 }
	// $(".centerright").css({'right':'inherit'});

	// $("#pic #backgroundpic").css({'left':'inherit','right':'0px'});
     $('#bubbles').prepend('<div id="bubble-yellow"><div class="bubcontent"></div><div class="bubble-yellow-arrow"></div></div>');
	 if(title!=null){
     $('#bubble-yellow .bubcontent').append('<h1>'+ title +'</h1>');
	 }
	 if(text!=null){
	 $('#bubble-yellow .bubcontent').append('<p>'+ text + '</p>');
	 }
	 if(footerText!=null){
	  $('#bubble-yellow .bubcontent').append('<p>'+ footerText + '</p>');
	 }
						 
}
/// when the PresenterImagePosition on the Left
function appendContetPresentImageOnLeft(title,text,footerText,presenterImagePosition){
	if($('#bubbles').find("#bubble-yellow").length>0){
       $('#bubbles').find("#bubble-yellow").remove();
	 }
	  $(".centerright").css({'right':'0px'});
	 $('#bubbles').prepend('<div id="bubble-white"><div class="bubcontent"></div><div class="bubble-white-arrow"></div></div>');
	 if(title!=null){
	 $('#bubble-white .bubcontent').append('<h1>'+ title +'</h1>');
	 }
	 if(text!=null){
	 $('#bubble-white .bubcontent').append('<p>'+ text + '</p>');
	 }
	 if(footerText!=null){
	  $('#bubble-white .bubcontent').append('<p>'+ footerText + '</p>');
	 }
}
///load Media Property value for Media Node
loadMediaPropertyValue=function (pageNode){
	var mediaNode=pageNode.find("Media");
	if(mediaNode!=null){
	var mediaType=mediaNode.attr("Type");
	var mediaFile=mediaNode.attr("Media");
	if(mediaType=="Video"){
	if(mediaFile!=null){
	//var mediaFileUrl="http://changetechstorage.blob.core.windows.net/videocontainer/86573c8d-cc38-ee18-52a4-e1602c90afab.flv";
	var mediaFileUrl="video/movie.mp4";
	var videoStr='<div id="videobox">'+
                 '<video controls="controls" id="video">'+
				 '<source src="'+mediaFileUrl+'" type="video/ogv; codecs=theora, vorbis">'+
                 '<source src="'+mediaFileUrl+'" type="video/mp4; codecs=avc1.42E01E, mp4a.40.2">'+
		         '<embed src="'+mediaFileUrl+'" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true"></embed>'+
		        
                 '</video></div>';
				  $('<link>').appendTo('head').attr({
		                 rel: 'stylesheet',
		                 type: 'text/css',
		                 href: 'css/ghindaVideoPlayer.css'
	                     });
                            $("#mainarea").html(videoStr);
				   }
	  }
	}
	
    }
/// Load Page properties value for Page Node
 loadPagePropertyValue=function(root,pageOrderNo){
	var pageNode=$(root).find("Page").eq(pageOrderNo);
    var title=pageNode.attr("Title");
	var text=pageNode.attr("Text");
	var type=pageNode.attr("Type");
	var buttonPrimaryName=pageNode.attr("ButtonPrimaryName");
	var buttonSecondaryName=pageNode.attr("ButtonSecondaryName");
	var presenterImage=pageNode.attr("PresenterImage");
	var presenterImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+presenterImage;
	var presenterImagePosition=pageNode.attr("PresenterImagePosition");
	var presenterMode=pageNode.attr("PresenterMode");
	var beforeExpression=pageNode.attr("BeforeExpression");
	var afterExpression=pageNode.attr("AfterExpression");
	var order=pageNode.attr("Order");
	var footerText=pageNode.attr("FooterText");
	var backgroundImage=pageNode.attr("BackgroundImage");
	var pargentPageSeq=pageNode.parents("PageSequence").attr("Order");
	
   if(backgroundImage!=null ||backgroundImage!=undefined){
     var backgroundImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+backgroundImage;
		$('#pic').html('<img src="'+backgroundImageUrl+'"  class="fullbg" id="fullbgImg">');
	}
	if(title!= null || text != null || footerText != null || presenterImagePosition != null ){
		if(presenterImagePosition=="Right"){
			appendContetPresentImageOnRight(title,text,footerText,presenterImagePosition)
		}else 
		{	
		appendContetPresentImageOnLeft(title,text,footerText,presenterImagePosition)
		}
	}
	if(presenterImage != null || presenterImage!=undefined ){
		var presenterImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+presenterImage;
		$('#pic').html('<img src="'+presenterImageUrl+'"  id="backgroundpic">');
	}
					
		if(buttonPrimaryName!=null || buttonPrimaryName!="" || buttonPrimaryName!=undefined ){
			$('#bubble-blue .bubcontent').html('<p>'+buttonPrimaryName+'</p>');
	}
	loadMediaPropertyValue(pageNode);
	}
	/// load the properties values for PageSequence Node
loadPageSeqProperty=function (root,pageSeqOrderNo){
         var pageSeqNode=$(root).find("PageSequence").eq(pageSeqOrderNo);
         var categoryName=pageSeqNode.attr("CategoryName");
		 var categoryDes=pageSeqNode.attr("CategoryDescription");
		 var topBarColor=pageSeqNode.attr("TopBarColor");
		 var PrimaryButtonColorNormal=pageSeqNode.attr("PrimaryButtonColorNormal");
		 var PrimaryButtonColorOver=pageSeqNode.attr("PrimaryButtonColorOver");
		 var order=pageSeqNode.attr("Order");
		 var PrimaryButtonColorDown=pageSeqNode.attr("PrimaryButtonColorDown");
		 var PrimaryButtonColorDisable=pageSeqNode.attr("PrimaryButtonColorDisable");
		 var CoverShadowVisible=pageSeqNode.attr("CoverShadowVisible");
		}
loadPage=function (root,pageSeqOrderNo,pageOrderNo){
		  if($('#bubble-white').get(0)){
			$('#bubble-white').remove();
		   }
           loadPageSeqProperty(root,pageSeqOrderNo);
		   loadPagePropertyValue(root,pageOrderNo);
		   }

$(function(){
	var flag = 0;
	//var rootxml;
	//var layoutxml;
	$.ajax({
		 url: 'root.xml',
		 type: "POST",
     dataType: "xml",
     success: function(root) {
			 	var logo = 'http://changetechstorage.blob.core.windows.net/logocontainer/' + $(root).find('Session').attr('Logo');
				var dayNumber = $(root).find('Session').attr('Day');
				var day = $(root).find('SpecialString').attr('Value');
				$('.progtitle').html('<img src="' + logo + '" alt="logo"/><span>' + day + dayNumber + '</span>');
				var pageSeqOrderNo=0;
				var pageOrderNo=0;
				//first load
				loadPage(root,pageSeqOrderNo,pageOrderNo);
				// when click conyiue, load again
				$('#bubble-blue').click(function(){
					//alert('count:'+count+ ' page:' + root.getElementsByTagName('Page').length);
					
					var pageSeqLen=$(root).find("PageSequence").length;
                    var allPageLen=root.getElementsByTagName('Page').length;

					/*if(pageSeqOrderNo<pageSeqLen){
					var pageNodes=$(root).find("PageSequence").eq(pageSeqOrderNo).find("Page");
					var pageLenInSeq=pageNodes.length;
					pageOrderNo=0;
					if(pageOrderNo<pageLenInSeq){
					     pageOrderNo+=1;
						loadPage(pageSeqOrderNo,pageOrderNo);
						return;
					  }
					}*/
					if(pageOrderNo<allPageLen){
						   pageOrderNo+=1;
						  loadPage(root,pageSeqOrderNo,pageOrderNo); 
						}
				});
				/*if all XML loaded, hide cover*/
        flag+=1;
				if(flag == 2){
					show();
				}
     },
		 error: function(){alert(' read root XML unsucessfully')},
	});

	$.ajax({
		url: 'layoutsetting.xml',
		 type: "POST",
     dataType: "xml",
     success: function(layout){
			var layout=$(layout).find('Setting');
			 var bgColor = '#' + layout.filter(function(){return $(this).attr('Name') == 'BackgroundColor'}).attr('Value').slice(2);
			 var height =layout.filter(function(){return $(this).attr('Name') == 'TopBarHeight'}).attr('Value') + 'px';
			 $('#topbar').css({'background':bgColor, 'height':height});
			 var presenterImageHeight=layout.filter(function(){
				 return $(this).attr("Name")=="PresenterImageHeight"
			 }).attr("Value")+"px";
		     var presenterImageMaxWidth=layout.filter(function(){
				 return $(this).attr("Name")=="PresenterImageMaxWidth"
			 }).attr("Value");
			 var presenterImageVerticalTopDiff=layout.filter(function(){
				 return $(this).attr("Name")=="PresenterImageVerticalTopDiff"
			 }).attr("Value");
			 var presenterImageHorizontalOverlap=layout.filter(function(){
                 return $(this).attr("Name")=="PresenterImageHorizontalOverlap"
			 }).attr("Value");
			 var presenterImageBigHeight=layout.filter(function(){
				 return $(this).attr("Name")=="PresenterImageBigHeight"
			 }).attr("Value")+"px";
			 
			 $("#pic img").css({'height':presenterImageBigHeight});

			 

			 	/*if all XML loaded, hide cover*/
        flag+=1;
				if(flag == 2){
					show();
				}
     },
		 error: function(){alert(' read layout XML unsucessfully')},
	});
	function show(){
		$('.body-wrapper').html('<p><h1>Load XML sucessfully!</h1></p>').delay(1000).fadeOut();
	}

	
});