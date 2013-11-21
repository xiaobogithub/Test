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

$(function(){
	var root = loadXML('root.xml');
	var logo = 'http://changetechstorage.blob.core.windows.net/logocontainer/' + (root.getElementsByTagName('Session')[0].getAttribute('Logo'));
	var dayNumber = root.getElementsByTagName('Session')[0].getAttribute('Day');
	var day = root.getElementsByTagName('SpecialString')[0].getAttribute('Value');
	var layout = loadXML('layoutsetting.xml');
	var bgColor = '#' + layout.getElementsByTagName('Setting')[0].getAttribute('Value').slice(2);
	var height = layout.getElementsByTagName('Setting')[1].getAttribute('Value') + 'px';
	$('.header').css({'background':bgColor, 'height':height, 'line-height':height});
	$('.header').html('<img src="' + logo + '" alt="logo"/>' + day + dayNumber);
});