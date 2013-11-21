
	var sequenceOrder =1;
	var pageOrder = 1;
	var flag = 0;
	var proV = [];
	var rootXmlObject=null;
	var presenterImageSize=null;
	var rootXmlUrl="";
	var para=[];
	var root;
	var dropwondFeedbackArray=[];
	function loadXmlAjax(){
	 ///Load root xml file
	$.ajax({
		 url: getCurrentUrl(),
		 type: "POST",
     dataType: "text",
     success: function(data) {
			 show();
			 var returnCode=data.substring(0,1);
			 if(returnCode=='1'){
					showTopBar(data);
					getCurrentUrl();
					if(para['Mode'] == 'Preview'){
						if(para['Object']=='Page'){
							sequenceOrder = $(root).find('PageSequence').attr('Order');
							pageOrder = $(root).find('Page').attr('Order');
							
							}else if(para['Object']=='PageSequence'){
								sequenceOrder = $(root).find('PageSequence').attr('Order');
								pageOrder=1;
							}else{
								sequenceOrder=1;
								pageOrder=1;
							}
						}	
						
						//first load
						loadPage($(root));
						
						proArr($(root));
						// when click contiue, load again
						onClickPageChange(root);
						
					}else{
						//if returnCode=0 ,the code will be start here 
						alert("returnCode is 0")
					}
     },
		 error: function(){
			  alert(' read root XML unsucessfully')
		 }
	});
	}
	//load page
	function showTopBar(data){
		root=data.substring(2);
		rootXmlObject=root;
		//var logoImage=$(root).find('Session').attr('Logo');
		//var logoUrl="";
		//if(logoImage!=null){
		//	logoUrl = 'http://changetechstorage.blob.core.windows.net/logocontainer/' +logoImage ;
		//}else{
		//	logoUrl="";
		//}
		var dayNumber = $(root).find('Session').attr('Day');
		var day = $(root).find('SpecialString').attr('Value');
		//if(logoImage!=null){
			//$('.progtitle').append('<img src="' + logoUrl + '" alt="logo"/>');
		//}else{
			//$('.progtitle').append('');
		//}
		if(dayNumber!=null && day!=null){
			$('.progtitle').append('<span>' +day+" "+ dayNumber + '</span>');
		}else if(dayNumber==null && day!=null){
			$('.progtitle').append('<span>' +day+ '</span>');
		}else if(dayNumber!=null && day==null){
			$('.progtitle').append('<span>' +dayNumber+ '</span>');
		}else {
			$('.progtitle').append('<span></span>');
		}
	}
	function getCurrentUrl(){
		//var windowUrl=window.location.href;
		//var windowUrl="http://changetech.cloudapp.net/ChangeTech.html?Mode=Preview&Object=Page&PgGUID=df266c12-657f-4369-a112-ee0fa7fb0f73&SessionGUID=6dc5c799-61c8-453e-b3ae-b7452a38221a&PgSequenceGUID=ab7e008c-7390-4fc9-a900-6b60b0a6aa47&LanguageGUID=&UserGUID=1b5071d3-7fd4-489d-a068-874fbd907b00";
		var windowUrl="http://program.changetech.no/ChangeTech.html?P=L8D8PR&Mode=Live&Security=95EDECE33687B80AF581295699F16B0AD3FDA3C644973095B1C00ECDF0D6CF8B";
		var parameterStr=windowUrl.split("?")[1]; 
		var pageStr=windowUrl.split("?")[0];// get http://changetech.cloudapp.net/ChangeTech.html
		var lastIndex=pageStr.lastIndexOf('/');
		var str=pageStr.substr(lastIndex+1);
		var paraArray=parameterStr.split('&');
        for(var i=0;i<paraArray.length;i++){
			var value=paraArray[i];
			var array=value.split('=');
			paraName=array[0];
			paraValue=array[1];
		    para[paraName]=paraValue;
		}
		
		if(para['Mode']!=null || para['Mode']!=""){
		  if( para['Mode']=="Preview"){
           rootXmlUrl=windowUrl.replace(str,'GetPreviewModelXML.ashx');
		  }else{
		   rootXmlUrl=windowUrl.replace(str,'AuthenticateUser.ashx');
		  }
		}else{
			alert("There is no Mode parameter!");
		}
		//return rootXmlUrl;
		return 'root.xml';	
	}
	
	function loadPage(XMLobj){
		var hiddenElementStr='<div id="content">'+
               '<div id="white" class="white" style="display:none;"></div>'+
			   '<div id="yellow" class="yellow" style="display:none;"></div>'+
		       '<div id="black" class="black" style="display:none;"></div>'+
			   '<div id="blue" class="blue" style="display:none;"></div>';
	$('#hiddenElements').empty().append($(hiddenElementStr));
	var hiddenImage='<div id="hostPic"></div>';
	$("#hiddenImage").empty().append($(hiddenImage));
    var hiddenBgImg='<div id="bgImg"></div>';
	$("#hiddenBg").empty().append($(hiddenBgImg));
		 pageSeqObj=XMLobj.find('PageSequence').filter(function(){return parseInt($(this).attr('Order')) == sequenceOrder});
		 pageObj = XMLobj.find('PageSequence').filter(function(){return parseInt($(this).attr('Order')) == sequenceOrder}).find('Page').filter(function(){return parseInt($(this).attr('Order')) == pageOrder});
		 quesObj=pageObj.find("Questions").find("Question");
  // alert(pageSeqObj.attr("Order")+"/"+pageObj.attr("Order"))
		 //loadpageSeqData;
		 loadPageSeqCategory(pageSeqObj);
		 //loadPageData
		 loadPageData(pageObj);
		 //showDialog
		 showDialog();
		// OnResize();	
		
		 //type == Account creation
	 var presenterImage=pageObj.attr("PresenterImage");
   var presenterImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+presenterImage;
	 onBlendTransparentImage(presenterImageUrl);
	 onClickRadioButton();
	 onClickForCheckbox();
	 onClickForDropdownList();
	  onClickForSlider(pageObj);
	  onClickForPasswordReminder(pageObj); 
	   gotoPage(true);
	}
function loadPageData(pageObj){
	    //LoadTopBarStyle
		//loadTopBarStyle(pageObj);
		 //Load Background
		loadBackgroundImage(pageObj);
		//Load PresenterImage
		loadPresenterImage(pageObj);
		//LoadButtonPrimaryValue
		loadButtonPrimary(pageObj);
        //LoadTextAndTitle
	   loadMainDataInBubble(pageObj);
		
		// LoadMediaData
		 loadMediaData(pageObj);
}
function loadDataForSlider(quesIndex,ques){
	var items=[];
	 var sliderStr=
				   '<div class="row">'+
			       "<input type='text'id='amount"+quesIndex+"' style='border:0; color:#069; font-weight:bold; background:none;' />"+
			       "<div id='slider"+quesIndex+"' style='width:480px; margin:4px 0 12px 0;'>"+
				   "</div></div>";
			$('#yellow').append(sliderStr);
			var itemObj=$(ques).find("Items").find("Item");
			var itemLen=$(itemObj).length;
			var maxValue=$(itemObj).eq(itemLen-1).attr("Item");
			var minValue=$(itemObj).eq(0).attr("Item");
			 $( '#slider'+quesIndex+'' ).slider(
				 {value:1,min:1,max: maxValue,step: 1,slide: function( event, ui )
				 {
				 $( '#amount'+quesIndex+'').val(ui.value);
				 }
			  });
			$(itemObj).each(function(itemIndex,item){
              var itemValue=$(item).attr("Item");
			  var scoreValue=$(item).attr("Score");
			 var itemGuid=$(item).attr("GUID");
			  items[itemValue]=scoreValue; 
             });
}
function onClickForSlider(pageObj){
	var quesObj=pageObj.find("Questions").find("Question");
	$(quesObj).each(function(quesIndex,ques){
		var itemObj=$(ques).find("Items").find("Item");
			var itemLen=$(itemObj).length;
			var maxValue=$(itemObj).eq(itemLen-1).attr("Item");
			var minValue=$(itemObj).eq(0).attr("Item");
		 $( '#slider'+quesIndex+'' ).slider(
				 {value:1,min:1,max: maxValue,step: 1,slide: function( event, ui )
				 {
				 $( '#amount'+quesIndex+'').val(ui.value);
				 }
			  });
	});
}
function loadDataForDropdownList(quesIndex,ques){
	var optionStr="";
	
	var itemObj=$(ques).find("Items").find("Item");
	$(itemObj).each(function(itemIndex,item){
		var itemValue=$(item).attr("Item");
		var scoreValue=$(item).attr("Score");
		var feedbackValue=$(item).attr("Feedback");
		optionStr+='<option value='+itemIndex+'>'+itemValue+'</option>';
		dropwondFeedbackArray[itemIndex]=feedbackValue;
	});
	var dropdownListStr='<div class="row">'+
			      '<span class="label">you like:</span>'+
	               '<span class="formw">'+
				   '<select class="selectbox"></select>'+
			      '</span></div>';
	$('#yellow').append(dropdownListStr);
	$('#yellow .selectbox').append(optionStr);
	var feedbackStr='<p class="dropDownListfeedback">'+dropwondFeedbackArray[0]+'</p>'
	$(feedbackStr).insertAfter($('.selectbox'));
	
}
function onClickForDropdownList(){
	$('.selectbox').change(function(){
	var selectedValue=$(this).val();
	var feedback=dropwondFeedbackArray[selectedValue];
	$(".dropDownListfeedback").text(feedback);
	});
	
}
function loadDataForCheckbox(quesIndex,ques){
	var checkbox="";
	var feedbackArray=[];
	 var feedbackStr="";
	var itemObj=$(ques).find("Items").find("Item");
	$(itemObj).each(function(itemIndex,item){
		var itemValue=$(item).attr("Item");
		var scoreValue=$(item).attr("Score");
		var feedbackValue=$(item).attr("Feedback");
		feedbackArray[scoreValue]=feedbackValue;
		checkbox+='<div class="itemRow"><input class="checkbox" type="checkbox" name="'+itemValue+'" value="'+feedbackValue+'">'+itemValue+'</div>';
	});
	var checkBoxStr="<div class='row'><div class='checkboxrow'></div></div>";
    $('#yellow').append(checkBoxStr);
	$('#yellow .checkboxrow').append(checkbox);

          
}
function onClickForCheckbox(){
	$(":checkbox").each(function(index,s){
			$(s).change(function(){
			  if(!$(s).hasClass("checked"))
               {
                $(s).addClass("checked");
				var feedbackStr='<span class="checkboxfeedback">'+$(s).val()+'</span>';
	            $(s).parents('.itemRow').append(feedbackStr);
              }else{
              $(s).removeClass('checked');
               $(s).parents('.itemRow').children('.checkboxfeedback').remove();
			  }
			});
	   });
}
function loadDataForRadioButton(quesIndex,ques){
var radioButton="";
var itemObj=$(ques).find("Items").find("Item");
$(itemObj).each(function(itemIndex,item){
		var itemValue=$(item).attr("Item");
		var scoreValue=$(item).attr("Score");
		var feedbackValue=$(item).attr("Feedback");
		radioButton+='<div class="itemRow"><input class="radioButton" type="radio" name="'+itemValue+'" value="'+feedbackValue+'">'+itemValue+'</div>';
	});
	var radioButtonStr="<div class='row'><div class='radioButtonRow'></div></div>";
    $('#yellow').append(radioButtonStr);
	$('#yellow .radioButtonRow').append(radioButton);	
}
function onClickRadioButton(){
	$(":radio").each(function(index,r){
		$(r).change(function(){
			var feedbackStr='<p  class="radiofeedback"><span>'+$(r).val()+'</span></p>';
			$(r).parents('.radioButtonRow').children('.radiofeedback').remove();
				$(r).parents('.radioButtonRow').append(feedbackStr);
			$(this).parents().siblings().children('input').attr('checked',false);
			$(this).parents().siblings().children('.radiofeedback').remove();			
		})
	})
}
function loadDataForMultiLine(quesIndex,ques){
var multiplyStr="<div class='row'><div class='multiplyRow'><textarea  cols='57' rows='10' autofocus='true' /></div></div>";
 $('#yellow').append(multiplyStr);
}
function loadDataForSingleLine(quesIndex,ques){
 var singlelineStr="<div class='row'><div class='singlelineRow'><input type='text' width='480px'  autofocus='true' /></div></div>";
 $('#yellow').append(singlelineStr);
}
function  loadDataForNumeric(quesIndex,ques){


}
function loadDataForGetInformationTemplate(pageObj){
			var quesObj=pageObj.find("Questions").find("Question");
			$(quesObj).each(function(quesIndex,ques){
			var caption=$(ques).attr("Caption");
			 if(caption!=null){
			 $('#yellow').append('<p>'+caption+'</p>');
			 }
		    var quesType=$(ques).attr("Type");
			if(quesType=="Slider"){
			  loadDataForSlider(quesIndex,ques);
			}// questiong type is slider
			else if(quesType=="DropDownList"){
			loadDataForDropdownList(quesIndex,ques);
			}//question type is DropdownList
			else if(quesType=="CheckBox"){
				loadDataForCheckbox(quesIndex,ques);
			}// question type is CheckBox
			else if(quesType=="RadioButton"){
             loadDataForRadioButton(quesIndex,ques);
			}else if(quesType=="Multiline"){
            loadDataForMultiLine(quesIndex,ques);
			}else if(quesType=="Singleline"){
             loadDataForSingleLine(quesIndex,ques);
			}else if(quesType=="Numeric"){
              loadDataForNumeric(quesIndex,ques);
			}
			});          
}
function loadDataForLogin(pageObj){
	 //$(bubbleYellowStr).insertBefore("#bubble-blue");
	 var pageType=pageObj.attr("Type");
	 var textArray=pageObj.attr('Text').split(";");
	 var footerText=pageObj.attr('FooterText');
	  var loginStr="";
	 for(var i=1;i<textArray.length;i++){
		 if(textArray[i]!=""){
         loginStr+='<div class="row"><span class="label">'+textArray[i]+'</span><span class="formw"><input class="textfield" type="text" size="25" /></span></div>';
		 }
	 }
	 $("#yellow").append(loginStr);
	 if(footerText!=null){
	  $('#yellow ').append('<p id="bubble-login"><a href="javascript:void(0)" title="'+footerText+'" class="'+pageType+'">'+ footerText + '</a></p>');
	 } 
	
}
//*******************
	function loadAccountCreation(pageObject, isNeedSerialNumber){
    var text=pageObject.attr("Text").split(";");
   // $("#white").append('<p>'+text[0]+'</p>');
    var bubbleForm='<form>';
    for(var tmp=1;tmp<text.length-1;tmp++){
         bubbleForm+='<div class="row"><span class="label">'+text[tmp]+'</span><span class="formw"><input class="textfield" type="text" size="25" /></span></div>'
      }
    bubbleForm+='<div class="row"><input class="checkbox" type="checkbox" name="option1" value="Milk">'+ text[text.length-1] +'</div>';
    if(isNeedSerialNumber == 1) {
      bubbleForm += '<div class="row"><span class="label">Serial Number:</span><span class="formw"><input class="textfield" type="text" size="25" /></span></div>';
    }
    bubbleForm += '</form>';
    $("#yellow").append($(bubbleForm));
	}
	//*******************
function loadMainDataInBubble(pageObj){
		 var pageType=pageObj.attr("Type");
		 //var bubbleYellowStr='<div id="bubble-yellow"><form><div class="bubcontent"></div><div class="bubble-yellow-arrow"></div></form></div>';
		 if((pageType=="Login") || (pageType=="Password reminder")|| pageType=="Account creation"){
          loadTitleAndTextForOtherTemplate(pageObj);
		 }else{
          loadTitleAndText(pageObj);
		}
		
		if(pageType=="Get information"){
         loadDataForGetInformationTemplate(pageObj);
		}
		if(pageType=="Login" || pageType=="Password reminder"){
         loadDataForLogin(pageObj);
		 
		}
		if(pageObj.attr("Type")=="Account creation"){
           var isNeedSerialNumber = $(rootXmlObject).attr("IsNeedSerialNumber");
           loadAccountCreation(pageObj, isNeedSerialNumber);
         }
	  
		
	}
	function onClickForPasswordReminder(pageObj){
	 var pageType=pageObj.attr("Type");
	 $("#bubble-login a").click(function(){
		  if(pageType=="Login"){
		  sequenceOrder =1;
	      pageOrder = 2;
		  }else{	 
           sequenceOrder =1;
	       pageOrder = 1;
		  }
		  loadPage($(rootXmlObject));
	     });
}
function loadTitleAndTextForOtherTemplate(pageObj){
	  
	    var title=pageObj.attr('Title');
		 var text=pageObj.attr('Text');
		 var footerText=pageObj.attr("FooterText");
		 var textArray=text.split(";");
         var textDesc=textArray[0];
		 var pageType=pageObj.attr("Type");
		if(title!= null){
			$('#white').append('<h1>'+ title + '</h1>');
			if(text==""){
			$('#white>h1').css({'font-size':'40px'})
			}
		}
		if(text != null){
			if(textDesc!=null){
		$('#white').append('<p>'+ textDesc + '</p>');
			}
		}
}
function loadTitleAndText(pageObj){
	  var title=pageObj.attr('Title');
	  var text=pageObj.attr('Text');
	  var footerText=pageObj.attr("FooterText");
		 if(title!= null || text != null || footerText!=null){	
			//$('#bubbles').prepend(bubbleWhiteBubbleStr);
			}
		if(title!= null){
			$('#white').append('<h1>'+ title + '</h1>');
			if(text==""){
			$('#white >h1').css({'font-size':'40px'})
			}
		}
		if(text != null){
			$('#white').append('<p>'+ text + '</p>');
		}
		if(footerText!=null){
			$('#white').append('<p>'+ footerText + '</p>');
		}
		
}
function loadPageSeqCategory(pageSeqObj){
		 /// PageSequence CategoryName property
		 var categoryName=pageSeqObj.attr("CategoryName");
		 var categoryDescription=pageSeqObj.attr("CategoryDescription");
	}

function loadButtonPrimary(pageObj){
		/// buttonPrimaryName Property
		var buttonPrimaryName=pageObj.attr("ButtonPrimaryName");
		if(buttonPrimaryName!=null){
			$('#blue').html('<p>'+buttonPrimaryName+'</p>');
		}
	}
function loadPresenterImage(pageObj){
		/// presenterImage Property
		var presenterImage=pageObj.attr("PresenterImage");
		var presenterMode=pageObj.attr("PresenterMode");
	    var presenterImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+presenterImage;
        var backgroundImage=pageObj.attr("BackgroundImage");
		if(presenterImage != null){
			$('#hostPic').html(presenterImageUrl);
			//$('#pic').append('<img  id="over" src="Images/overtest.png"/>');
			//$('#pic').append('<canvas id="under"></canvas>');
			//$('#pic').append('<canvas id="backgroundpic"></canvas>');
		}else{
			$("#hostPic").empty();
		}
		//showMainBubblePosition(pageObj);
	}
 function showMainBubblePosition(pageObj){
		//var presenterImagePosition=pageObj.attr("PresenterImagePosition");
		//var presenterImage=pageObj.attr("PresenterImage");
		//if(presenterImage!=null){
		//	$("#bubbles").addClass('centerright').removeClass('center');
		//	$(".centerright").css({'right':'0px'});
		//}else{
		//	$("#bubbles").addClass('center').removeClass('centerright');
		//	$('#bubbles').css({'right':'0px'});
		//}
		//if(navigator.userAgent.toLowerCase().match(/iPhone/i)== null) {
		//$('#bubbles').removeClass("centerright").animate({left: '0px'},500).addClass("center");
	   // }
		//$.getScript("Scripts/custom.js");
	}
 function loadBackgroundImage(pageObj){
		/// BackgroundImage Property
		var backgroundImage=pageObj.attr("BackgroundImage");
		if(backgroundImage!=null || backgroundImage!=undefined){
        var backgroundImageUrl="http://changetechstorage.blob.core.windows.net/originalimagecontainer/"+backgroundImage;
		$('#bgImg').html(backgroundImageUrl);
      	}else{
        $('#bgImg').empty();
		}
	}


	//program array
	function proArr(XMLobj){
		if((XMLobj).find('ProgramVariables').find('Variable')){
			for(var i=0; i<(XMLobj).find('ProgramVariables').find('Variable').length; i++){
				proV[i]=[];
				proV[i][0] = (XMLobj).find('ProgramVariables').find('Variable').eq(i).attr('Name');//name
				proV[i][1] = (XMLobj).find('ProgramVariables').find('Variable').eq(i).attr('Type');
				proV[i][2] = (XMLobj).find('ProgramVariables').find('Variable').eq(i).attr('Value');//value;
			};
		}	
	}
	
	
	/// Get Media Node function
	/// Get Media Node function
function loadMediaData(pageObj){
	//var bubbleBlackStr='<div class="bubble-black"><div class="bubble-black-arrow"></div></div>'
	var mediaNode=pageObj.find("Media");
	var mediaType=mediaNode.attr("Type");
	var mediaFile=mediaNode.attr("Media");
    if(mediaType!=null && mediaType!=""){
	if(mediaType=="Video"){
	if(mediaFile!=null){
	 //$(".bubble-black").remove();
	//$(bubbleBlackStr).insertBefore("#bubble-blue");
	var mediaFileUrl="http://changetechstorage.blob.core.windows.net/videocontainer/"+mediaFile;
	var videoStr='<p><video src="'+mediaFileUrl+'" height="284" id="container" poster="Images/video-loader.png" width="504"></video></p>'+
             '<script type="text/javascript">'+ 
	          'jwplayer("container").setup('+
	           '{'+
		      ' skin: "http://www.pindrop.no/clients/changetech/newme/program/order2/player/glow.zip",'+
              ' flashplayer: "jwplayer/player.swf"'+
               ' });'+
           '</script>';
        $("#black").append($(videoStr));
      }
	 }
	 else if(mediaType=="Illustration"){
		/// show Illustration here 
		
		var mediaFileUrl="http://changetechstorage.blob.core.windows.net/videocontainer/"+mediaFile;
		$("#black").append('<img src="Images/audio-breath.jpg" alt="'+mediaFile+'" width="485px" />');
	 }//
	else if(mediaType=="Audio"){
     /// show Audito element here 
	if(mediaFile!=null){
	 var audioUrl='http://changetechstorage.blob.core.windows.net/videocontainer/'+mediaFile;
     var audioStr= '<p><div id="testfile3">Loading the player ...</div><script type="text/javascript">'+
		 'jwplayer("testfile3").setup('+
		 '{'+
        'flashplayer: "jwplayer/player.swf",'+
         'file: "'+audioUrl+'",'+
	    // 'height: 150,'+
		 'width: 492,'+
		 'screencolor: "fff",'+
	     //'image: "Images/audio-breath.jpg"'+
         'height: 30,'+
	     'icons:false,'+
	     'controlbar: "bottom"'+
          '});'+
         '</script></p>';
     $("#black").append($(audioStr));
	}
	}
  }
}

	   /// Load layoutSetting XML FILE

	//recursion to parse the whole expersion
	var ifTrue = false;
	//define all the orders
	var orders=[];
	orders[0] = 'GOTO';
	orders[1] = 'SET';
	var noIfOrder = 0;
	function recur(str, XMLobj){
		//define all IF sentence, include the followed order(s)
		var ifSentence = [];
		var ifCount = -1;

		//define ELSE sentence
		var elseSentence = [];
		
		//define on if sentence;
		var noIfSentence =[];
		
		//divide the expression by IF
		if(str.match(/^IF+/)!=null){
			//else part
			if(str.match(/\[ELSE/) != null){
				elseSentence[0] = str.split('[ELSE')[1].split(']')[0].trim();
				str = str.split('[ELSE')[0];
			}
			//here the str has one or more 'IF'
			function divideIf(){
				if(str.match(/^IF.+IF.+/)){
					//if has more 'IF'
					ifCount+=1;
					var temp =str.match(/^IF[^IF]+/).toString();
					str = str.split(temp)[1];
					ifSentence[ifCount]=[];
					ifSentence[ifCount][0] = temp;
					divideIf();
				}else{
					//only has one 'IF'
					ifCount+=1;
					ifSentence[ifCount]=[];
					ifSentence[ifCount][0] = str;
				}
			}
			divideIf();
			for(var i=0; i<ifSentence.length; i++){
				for(var j=0; j<orders.length; j++){
					var orderCount = ifSentence[i].length-1;
					function getIf(){
						if(ifSentence[i][0].lastIndexOf(orders[j]) != -1){
							orderCount +=1;
							var tempIndex = ifSentence[i][0].lastIndexOf(orders[j]);
							ifSentence[i][orderCount] = ifSentence[i][0].slice(tempIndex,ifSentence[i][0].length);
							ifSentence[i][0] = ifSentence[i][0].slice(0,tempIndex).trim();
							
							getIf();
						}
					}
					getIf();
				}	
			}
			//get all orders
			for(var i=0; i<ifSentence.length; i++){
				divideOrder(ifSentence[i]);
			}
			perform(ifSentence);
			if(ifTrue == false){
				divideOrder1(elseSentence);
				perform(elseSentence);
			}
		}else{
		  //consider the situation of has no 'IF'
			noIfSentence[0] = str;
			divideOrder1(noIfSentence);
			perform(noIfSentence);
		}
	}
	
	function divideOrder(arr){
		for(var j=1; j<arr.length; j++){
			for(var k=0; k<orders.length; k++){
				var orderCount1 = arr.length-1;
				if(arr[j].lastIndexOf(orders[k])!=-1 && arr[j].lastIndexOf(orders[k])!=0){
					var tempIndex1 = arr[j].lastIndexOf(orders[k]);
					orderCount1+=1;
					arr[orderCount1] = arr[j].slice(tempIndex1,arr[j].length).trim();
					arr[j] = arr[j].slice(0,tempIndex1).trim();
					divideOrder(arr);
				}
			}
		}
		
	}
	function divideOrder1(arr){
		for(var i=0; i<arr.length; i++){
			for(var j=0; j<orders.length; j++){
				if(arr[i].lastIndexOf(orders[j])!= 0 && arr[i].lastIndexOf(orders[j]) != -1){
					var tempIndex2 = arr[i].lastIndexOf(orders[j]);
					noIfOrder +=1;
					arr[noIfOrder] = arr[i].slice(tempIndex2,arr[i].length).trim();
					arr[i] = arr[i].slice(0,tempIndex2).trim();
					divideOrder1(arr);
				}	
			}
		}
	}
	
	
	
	//excute when 'IF' is ture or no 'IF'
	function excuteArr(arr){
		$(arr).each(function(i){
			//SET
			if(this.indexOf('SET') == 0){
				var temp1 = this.split('SET')[1].split('=')[0].trim().replace(/\{V:/g, '').replace(/\}/g,'');
				var temp2 = this.split('=')[1].split('=')[0].trim().replace(/\{V:/g, '').replace(/\}/g,'');
				var count=0;
				for(var i=0; i<proV.length; i++){
					if(proV[i][0] == temp1){
						count = i;
					}
				}
				
				for(var j=0; j<proV.length; j++){
					if(temp2.indexOf(proV[j][0])!=-1){
						var reg = eval('/' + proV[j][0] + '/g')
						temp2 = temp2.replace(reg, proV[j][2]);
					}
				}
				proV[count][2] = eval(temp2);
			};
			//GOTO
			if(this.match(/GOTO\s\d+\.\d+/) != null){
				sequenceOrder = this.split('GOTO')[1].split('.')[0].trim();
				pageOrder = this.split('GOTO')[1].split('.')[1].trim();
				loadPage($(rootXmlObject));
			}
		});
	}
	
	function perform(arr){
		
			var tempFlag = true;
			$(arr).each(function(i){				
				//IF
				
				if(typeof($(arr)[i])!= 'string' && $($(arr)[i])[0].match(/^IF/)!=null){
					tempFlag = false;
					var temp = $($(arr)[i])[0].replace(/IF/,'').trim();
					temp = temp.replace(/\{V:/g ,'').replace(/\}/g,'');
					for(var j=0; j<proV.length; j++){
						if(temp.indexOf(proV[j][0])!=-1){
							var reg = eval('/' + proV[j][0] + '/g')
							temp = temp.replace(reg, proV[j][2]);
						}
					}
					if(eval(temp) == true){
						ifTrue = true;
						$($(arr)[i]).each(function(i){
							excuteArr(this);
						});
					}
				}
				
			});
			//all IF failed.(So IT's Else or just orders)
			if(tempFlag == true){
				excuteArr($(arr));
			}
	}
	
function onClickPageChange(root){
	var sessionName=$(root).find("Session").attr("Name");
	if(sessionName=="Login"){
		$("#bubble-blue").click(function(){
			if(pageOrder == 1 && $('#bubble-yellow input').eq(0).val().trim() == '' ||$('#bubble-yellow input').eq(1).val().trim() == ''){
				var msg = $(root).find('Message').filter(function(){return $(this).attr('Name') == 'LoginInfoRequired'});
				$('#popupDialog').html('<h1>'+ msg.attr('Title') + '</h1><p>' + msg.attr('Message') +'</p><input type="button" value="'+ msg.attr('BackButtonName') +'" id="popupBtn">').show();
				$('#main-wrapper').show();
				$("#popupBtn").click(function(){
					$('#main-wrapper').hide(); 
					$('#popupDialog').hide();
				});
			}else if(pageOrder == 1 && $('#bubble-yellow input').eq(0).val().trim() != '' && $('#bubble-yellow input').eq(1).val().trim() != ''){
				var proGUID = $(root).attr('ProgramGUID');
				var pagGUID = $(root).find('Page').filter(function(){return parseInt($(this).attr('Order')) == 1}).attr('GUID');
				var usr = $('#bubble-yellow input').eq(0).val().trim();
				var pwd = $('#bubble-yellow input').eq(1).val().trim();
				var backXml = '<XMLModel UserGUID = "" ProgramGUID = "' + $(root).attr('ProgramGUID') + proGUID +  '" SessionGUID = "" PageSequenceOrder = "1" PageGUID = "' + pagGUID + '"><Login Username="' + usr + '" Password="' + pwd + '"/></XMLModel>';
				 $.ajax({
					 url: 'root.xml',
					 processData: false,
					 //data: backXml,
					 success: function(abc) {
						 //alert($(abc).toString());
						 loadXmlAjax();
					 },
					 error: function(){
							alert(' read root XML unsucessfully');
					 }
				 });
			}
			
		});
	}else if(sessionName=="Register"){
		alert("retister")
	}else{
		$("#bubble-blue").click(function(){
			var pageObj = $(root).find('PageSequence').filter(function(){return parseInt($(this).attr('Order')) == sequenceOrder})
							.find('Page').filter(function(){return parseInt($(this).attr('Order')) == pageOrder});
			//has after Experssion situation
			if(pageObj.attr('AfterExpression') && pageObj.attr('AfterExpression') != ''){
				var ae = pageObj.attr('AfterExpression');
				recur(ae, $(root));
			}else{
				//if has no afterexperssion, go to next page directlly
				if(pageOrder < $(root).find('PageSequence').filter(function(){return parseInt($(this).attr('Order')) == sequenceOrder}).find('Page').length-1){
					pageOrder+=1;
				}else if( sequenceOrder < $(root).find('PageSequence').length-1){
					pageOrder = 1;
					sequenceOrder+=1;
				}else{
					alert('this is already the last page!');
				}
			}
			//show elements on the current page
			loadPage($(root));
		});																				
	}
}
	function show(){
		$('.body-wrapper').html('<p><h1>Load XML sucessfully!</h1></p>').delay(500).fadeOut();
	}
	function showDialog(){
		 $(".setbtn").click(function(){
			  var dialogHeight=($("#popupDialog").css('height').replace("px",""));
			  $('#main-wrapper').css({'display':'block'}); 
			  $('#popupDialog').css({'display':'block','margin-top':'-'+(dialogHeight/2)});
		 });
		 $("#popupBtn").click(function(){
			$('#main-wrapper').css({'display':'none'}); 
			$('#popupDialog').css({'display':'none'});
		});
	}

function OnResize(){
	var hostImg = $("#backgroundpic");
	var bub = $("#bubbles");
	var main = $("#mainarea");
	var vid = $("#videobox");	
	var bgImg = $("#fullbgImg");
	var over = $("#over");
	var under = $("#under");
	var mainWrapper=$("#main-wrapper");
	
	var vidheight = vid.height()/2-20;
	var bubheight = bub.height()/2+50;
 
    function resizeImg() {
		
	  var winwidth = $(window).width();
      var winheight = $(window).height();
	  
     var imgwidth = hostImg.width();
     var imgheight = hostImg.height();
	 // var imgwidth=over.width();
	 // var imgheight=over.height();

      var widthratio = winwidth / imgwidth;
      var heightratio = winheight / imgheight;
 
      var widthdiff = heightratio * imgwidth;
      var heightdiff = widthratio * imgheight;
	  //var ratio=widthratio<heightratio ? widthratio : heightratio;
	 // var actualImageWidth=imgwidth*ratio;
	 // var actualImageHeight=imgheight*ratio;
	  	  
	  var bgimgwidth = bgImg.width();
      var bgimgheight = bgImg.height();
	  
	  var bgwidthratio = winwidth / bgimgwidth;
      var bgheightratio = winheight / bgimgheight;
	  
	  var bgwidthdiff = bgheightratio * bgimgwidth;
      var bgheightdiff = bgwidthratio * bgimgheight;
 // over.css({width: actualImageWidth+'px', height: actualImageHeight+'px' });
 // hostImg.css({width: actualImageWidth+'px', height: actualImageHeight+'px'});
 // under.css({width: actualImageWidth+'px', height: actualImageHeight+'px'});

	  if (heightdiff<winheight) { 
		  hostImg.css({width: winwidth+'px', height: heightdiff+'px'}); 
		  //over.css({width: winwidth+'px', height: heightdiff+'px'}); 
		 // under.css({width: winwidth+'px', height: heightdiff+'px'}); 

		  } 
	  else {
		hostImg.css({width: widthdiff+'px', height: winheight+'px' });
	   // over.css({width: widthdiff+'px', height: winheight+'px' });
	   // under.css({width: widthdiff+'px', height: winheight+'px' });

		 }
	  
	  if (bgheightdiff>winheight) { bgImg.css({width: winwidth+'px', height: bgheightdiff+'px' }); } 
	  else { bgImg.css({width: bgwidthdiff+'px', height: winheight+'px' }); }
	  
	  bub.css({top: winheight/2-bubheight+'px'});
	  vid.css({top: winheight/2-vidheight+'px'});
	  main.css({ height: winheight-46+'px' });
	  mainWrapper.css({height:winheight-46+'px'});
      getImageSize(over.width(),over.height());
    }
	
    resizeImg();
    $(window).resize(function() {
      resizeImg();
    }); 
}
function getImageSize(width,height){

	var presenterImageWidth=width;
	var presenterImageHeight=height;
	presenterImageSize={width:presenterImageWidth,height:presenterImageHeight};
}
function onBlendTransparentImage(presenterImageUrl){
	///Using CORS the maintainer of http://narwhalart.example can very easily indicate that all images can be used by http://unicornimages.example (or in fact all origins). To do so all that is required to change is that the server has to add the following HTTP headers for the clip art resources:
    //access-control-allow-origin: https://unicornimages.example
    //access-control-allow-credentials: true
 $('#over').load(function(){ 
	  blend();
  });
 //$('#backgroundpic' ).attr('src', presenterImageUrl);
 $('#over' ).attr('src', 'Images/overtest.png'); 
}
function blend(){
	var data = {}, contexts = {};
    var size;
	size = { width:presenterImageSize.width, height:presenterImageSize.height };
     $.each(['over'],function(i,s){	
	 var canvas=$('<canvas>').attr(size)[0];
	 var ctx = contexts[s] =canvas.getContext('2d');	
	 drawImage(ctx,s);
	 try{
	 var imageData=ctx.getImageData(0,0,size.width,size.height);
	 data[s] = imageData.data;
	 }catch(e){
		alert(e.message);
	 }
	});
	$.each(['under'],function(i,s){
		var ctx1=contexts[s] = $('#'+s).attr(size)[0].getContext('2d');
		var grd=contexts[s].createLinearGradient(0,0,size.width,size.height);
        grd.addColorStop(0,"#ffffff");
        grd.addColorStop(1,"#d7e1eb");
        contexts[s].fillStyle=grd;
        contexts[s].fillRect(0,0,size.width,size.height);
   });
	$.each(['backgroundpic'],function(i,s){
		contexts[s] = $('#'+s).attr(size)[0].getContext('2d');
	});
    $(contexts.backgroundpic.canvas).attr(size);
	drawImage(contexts.backgroundpic,'under');
	contexts.over.blendOnto(contexts.backgroundpic,'multiply');
}
function drawImage(ctx,imgOrId){
	if (typeof imgOrId == 'string'){		
	ctx.drawImage($('#'+imgOrId)[0],0,0,presenterImageSize.width,presenterImageSize.height);	
	}else{
		ctx.drawImage(imgOrId,0,0,presenterImageSize.width,presenterImageSize.height);
	}
}   