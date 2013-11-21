package com.ning.controls{
	import fl.controls.ProgressBar;
	import fl.controls.ProgressBarMode;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.HTTPStatusEvent;
	import flash.events.IEventDispatcher;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.events.SecurityErrorEvent;
	import flash.net.URLLoader;
	import flash.net.URLRequest;
	import flash.net.URLRequestMethod;

	public class StringLoader extends Sprite {
		
		private var _width:Number = 0;
		private var _height:Number = 0;
		private var _path:String;
		private var _data:String;
		private var _target:Object;
		private var _showProgressBar:Boolean = false;

		public function StringLoader(_urlArg:String) {
			_path = _urlArg;
		}
		override public function get width():Number {
			return _width;
		}
		override public function get height():Number {
			return _height;
		}
		public function get path():String {
			return _path;
		}
		public function set path(_value:String):void {
			if (_value == _path) {
				return;
			}
			_path = _value;
		}
		public function get data():String {
			return _data;
		}
		public function get target():Object {
			return _target;
		}
		public function set target(_value:Object):void {
			if(_value == _target) {
				return;
			}
			_target = _value;
		}
		public function get showProgressBar():Boolean {
			return _showProgressBar;
		}
		public function set showProgressBar(_value:Boolean):void {
			if (_value == _showProgressBar) {
				return;
			}
			_showProgressBar = _value;
		}
		public function load():void {
			var _request:URLRequest = new URLRequest(_path);
			var _loader:URLLoader = new URLLoader();
			configureListeners(_loader);
			try {
				_loader.load(_request);
				trace("Downloading data from " + _path);
				setupProgressBar(_loader);
			} catch (_error:Error) {
				trace("Load xml error: " + _error);
			}
		}
		public function send(_sendData:String):void {
			var _request:URLRequest = new URLRequest(_path);
			_request.contentType = "text/xml";
			_request.data = _sendData;
			_request.method = URLRequestMethod.POST;
			var _loader:URLLoader = new URLLoader();
			configureListeners(_loader);
			try {
				_loader.load(_request);
				trace("Sending data to " + _path);
				trace("Data is " + _sendData);
				setupProgressBar(_loader);
			} catch (_error:Error) {
				trace("Send xml error: " + _error);
			}
		}
		private function configureListeners(_loader:IEventDispatcher):void {
			_loader.addEventListener(Event.COMPLETE, loadCompletedHandler);
			_loader.addEventListener(IOErrorEvent.IO_ERROR, loadErrorHandler);
			_loader.addEventListener(Event.CANCEL, cancelHandler);
			_loader.addEventListener(HTTPStatusEvent.HTTP_STATUS, httpStatusHandler);
			_loader.addEventListener(Event.OPEN, openHandler);
			_loader.addEventListener(ProgressEvent.PROGRESS, progressHandler);
			_loader.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
		}
		private function setupProgressBar(_loader:IEventDispatcher):void {
			if (_showProgressBar) {
				var _progressBar:ProgressBar = new ProgressBar();
				_progressBar.mode = ProgressBarMode.EVENT;
				_progressBar.indeterminate = false;
				_progressBar.source = _loader;
				_progressBar.setSize(300, 10);
				addChild(_progressBar);
				_width = _progressBar.width;
				_height = _progressBar.height;
			}
		}
		private function loadCompletedHandler(_event:Event):void {
			_data = _event.target.data;
			this.dispatchEvent(new Event(Event.COMPLETE));
		}
		private function loadErrorHandler(_event:IOErrorEvent):void {
			//this.dispatchEvent(_event);
			//trace(_event.toString());
		}
		private function cancelHandler(_event:Event):void {
			this.dispatchEvent(_event);
		}
		private function httpStatusHandler(_event:HTTPStatusEvent):void {
			this.dispatchEvent(_event);
		}
		private function openHandler(_event:Event):void {
			this.dispatchEvent(_event);
		}
		private function progressHandler(_event:ProgressEvent):void {
			this.dispatchEvent(_event);
		}
		private function securityErrorHandler(_event:SecurityErrorEvent):void {
			this.dispatchEvent(_event);
		}
	}
}