// get xml data server URL
function getCurrentUrl() {

    var windowUrl = window.location.href;
    if (windowUrl.indexOf('?') >= 0) {
        var parameterStr = windowUrl.split("?")[1];
        var pageStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
        var lastIndex = pageStr.lastIndexOf('/');
        var str = pageStr.substr(lastIndex + 1);
        var paraArray = parameterStr.split('&');
        for (var i = 0; i < paraArray.length; i++) {
            var value = paraArray[i];
            var array = value.split('=');
            paraName = array[0];
            paraValue = array[1];
            para[paraName] = paraValue;
        }

        if (para['Mode'] != null || para['Mode'] != "") {
            if (para['Mode'] == "Preview") {
                rootXmlUrl = windowUrl.replace(str, 'GetPreviewModelXML.ashx');
            } else {
                rootXmlUrl = windowUrl.replace(str, 'AuthenticateUser.ashx');
            }
        } else {
            alert("There is no Mode parameter!");
        }
        return rootXmlUrl;
    }
}
// GET URL for send data to server
function getReturnDataServerUrl() {
    var windowUrl = window.location.href;
    if (windowUrl.indexOf('?') >= 0) {
        var parameterStr = windowUrl.split("?")[1];
        var pageStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
        var lastIndex = pageStr.lastIndexOf('/');
        if (lastIndex >= 0) {
            var str = pageStr.substr(lastIndex + 1);
            returnDataUrl = windowUrl.replace(str, 'AcceptAnswer.ashx');
        }
        return returnDataUrl;
    }
}

function getIntroDataServerUrl() {
    var windowUrl = window.location.href;
    if (windowUrl.indexOf('?') >= 0) {
        var parameterStr = windowUrl.split("?")[1];
        var pageStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
        var lastIndex = pageStr.lastIndexOf('/');
        var str = pageStr.substr(lastIndex + 1);
        var paraArray = parameterStr.split('&');
        for (var i = 0; i < paraArray.length; i++) {
            var value = paraArray[i];
            var array = value.split('=');
            paraName = array[0];
            paraValue = array[1];
            para[paraName] = paraValue;
        }

        if (para['Mode'] != null || para['Mode'] != "") {
            if (para['Mode'] == "Preview") {
                rootXmlUrl = windowUrl.replace(str, 'GetPreviewModelXML.ashx');
                rootXmlUrl += "&CTPP=true";
            } else {
                rootXmlUrl = windowUrl.replace(str, 'AuthenticateUser.ashx');
                rootXmlUrl += "&CTPP=true";
            }
        } else {
            alert("There is no Mode parameter!");
        }
        return rootXmlUrl;
    }
}
/************ Update js from Flash************/
 //    var isReady = false;
        //var dataURL = getXMLURL();
        var mediaURLRoot = GetWebSiteRoot();
        //    var validateUserURL = getValidateUserURL();
        //    var submitURL = getSubmitURL();
        function checkJSReady() {
            //Please put this statement at where the ASP.Net and JS is ready to send data to flash.
            isReady = true;
            //

            return isReady;
        }
        function sendDataURL() {
            return dataURL;
        }
        function sendMediaURLRoot() {
            return mediaURLRoot;
        }
        function getMode() {
            var splitMode = document.URL.split("Mode=");
            if (splitMode != null && splitMode.length > 1) {
                var mode = splitMode[1].split("&");
                return mode[0];
            }
            else {
                return "";
            }
        }
        function getSecurityQueryString() {
            var splitSecurity = document.URL.split("Security=");
            if (splitSecurity != null && splitSecurity.length > 1) {
                var Security = splitSecurity[1].split("&");
                var returnStr = Security[0].replace("###", "");
                var flag = true;
                while (flag) {
                    if (returnStr != returnStr.replace("#", "")) {
                        returnStr = returnStr.replace("#", "");
                    }
                    else {
                        flag = false;
                    }
                }
                return returnStr;
            }
            else {
                return "";
            }
        }
        function windowClose() {
            window.open('', '_parent', '');
            window.close();
        }
        function getURL(option) {
			var pageStr=getPageStr();
			var domainStr=getDomainStr();
            switch (option) {
                case "submit":
                    // return window.location.href.replace(pageStr, "AcceptAnswer.ashx");
                    return "root.xml";
                case "data":
//                    if (getMode() != "Preview") {
//                        return window.location.href.replace(pageStr, "AuthenticateUser.ashx");
//                    }
//                    else{
//                        return window.location.href.replace(pageStr, "GetPreviewModelXML.ashx");
                    //					}
                    return "root.xml";
                case "mediaRoot":
                    return GetWebSiteRoot();
                case "css":
                    return "Flash/ChangeTech.css?V=2640";
                case "layout":
                    // return "Flash/layoutsetting.xml";
                    return window.location.href.replace(pageStr, "GetLayoutSetting.ashx");
                case "crossdomain":
                    //return "http://changetechstorage.blob.core.windows.net/";
                    return GetWebSiteRoot() + "crossdomain.xml";
                case "payment":
					var paymentUrl=domainStr+"/Payment.aspx";
                    //return "http://program.changetech.no/Payment.aspx";
					return paymentUrl;
                case "ctpp":
					var ctppUrl=domainStr+"/CTPP.aspx";
                    //return "http://program.changetech.no/CTPP.aspx";
					return ctppUrl;
				case "intro":
//					if (getMode() != "Preview") {
//					    return window.location.href.replace(pageStr, "AuthenticateUser.ashx") + "&CTPP=true";
//                    }
//                    else{
//                        return window.location.href.replace(pageStr, "GetPreviewModelXML.ashx") + "&CTPP=true";
				    //                    }
				    return "intro.xml";
                case "originalimagecontainerRoot":
                    return GetWebSiteRoot() + "originalimagecontainer/";
                case "videocontainerRoot":
                    return GetWebSiteRoot() + "videocontainer/";
                case "audiocontainerRoot":
                    return GetWebSiteRoot() + "audiocontainer/";
                case "logocontainerRoot":
                    return GetWebSiteRoot() + "logocontainer/";
            }
        }
        function GetWebSiteRoot() {
            //            var urlSplitArray = window.location.href.split("ChangeTech.html");
            //            return urlSplitArray[0] + "RequestResource.aspx";
            var protocol = window.location.protocol;
            var webSiteRoot = protocol + "//changetechstorage.blob.core.windows.net/";
            return webSiteRoot;
        }
        function getMovie(movieName) {
            if (navigator.appName.indexOf("Microsoft") != -1) {
                return window[movieName];
            } else {
                return document[movieName];
            }
        } 
		function getPageStr(){
			var windowUrl=window.location.href;
			 if (windowUrl.indexOf('?') >= 0) {
				 var parameterStr = windowUrl.split("?")[1];
                 var domainStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
                 var lastIndex = domainStr.lastIndexOf('/');
                 var pageStr = domainStr.substr(lastIndex + 1); // Flash is "ChangeTechF.html,HTML5 is "ChangeTech5.html";
				 return pageStr;
			 }

		}
		function getDomainStr() {
		    var windowUrl = window.location.href;
		    var domainStr = "";
		    if (windowUrl.indexOf('?') >= 0) {
		        var pageStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
		        var lastIndex = pageStr.lastIndexOf('/');
		        domainStr = pageStr.substr(0, lastIndex);
		    }
		    return domainStr; //get http://changetech.cloudapp.net/
		}
		function getURLParameter() {

		    var windowUrl = window.location.href;
		    if (windowUrl.indexOf('?') >= 0) {
		        var parameterStr = windowUrl.split("?")[1];
		        var pageStr = windowUrl.split("?")[0]; // get http://changetech.cloudapp.net/ChangeTech.html
		        var lastIndex = pageStr.lastIndexOf('/');
		        var str = pageStr.substr(lastIndex + 1);
		        var paraArray = parameterStr.split('&');
		        for (var i = 0; i < paraArray.length; i++) {
		            var value = paraArray[i];
		            var array = value.split('=');
		            paraName = array[0];
		            paraValue = array[1];
		            para[paraName] = paraValue;
		        }
		    }
		}