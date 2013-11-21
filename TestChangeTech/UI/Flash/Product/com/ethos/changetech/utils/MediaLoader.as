package com.ethos.changetech.utils{
	import com.ning.data.GlobalValue;
	import flash.display.Loader;
	import flash.events.Event;
	import flash.net.URLRequest;
	import flash.system.Security;
	import flash.system.LoaderContext;

	public class MediaLoader {
		public static function loadImage(_imageName:String, _loadCompleteHandler:Function):void {
			var _imageLoader:Loader = new Loader();
			_imageLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, _loadCompleteHandler);
			//var _imageURL = String(GlobalValue.getValue("mediaRootURL")) + "originalimagecontainer/" + _imageName;
			//refactor media url 
			var _imageURL = String(GlobalValue.getValue("originalimagecontainerRoot")) + _imageName;
			
			var _crossdomainURL = String(GlobalValue.getValue("crossdomainURL"));
			trace("Image URL = " + _imageURL);
			trace("Cross domain URL =" + _crossdomainURL);
			var _imageRequest:URLRequest = new URLRequest(_imageURL);
			Security.loadPolicyFile(_crossdomainURL);
			var loaderContext = new LoaderContext();
			loaderContext.checkPolicyFile = true;
			_imageLoader.load(_imageRequest,loaderContext);
		}
	}
}