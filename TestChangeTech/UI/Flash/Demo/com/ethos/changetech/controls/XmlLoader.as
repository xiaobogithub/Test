package com.ethos.changetech.controls{
	import fl.controls.*;
	import flash.display.Sprite;
	import flash.events.*;
	import flash.net.*;

	public class XmlLoader extends Sprite {
		private var _path:String;
		private var _showProgressBar:Boolean = false;

		public function XmlLoader(_urlArg:String) {
			_path = _urlArg;
		}
		public function get path():String {
			return _path;
		}
		public function set path(value:String):void {
			if (value == _path) {
				return;
			}
			_path = value;
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
				if (_showProgressBar) {
					var _progressBar:ProgressBar = new ProgressBar();
					_progressBar.mode = ProgressBarMode.EVENT;
					_progressBar.indeterminate = false;
					_progressBar.source = _loader;
					_progressBar.setSize(300, 10);
					addChild(_progressBar);
				}
			} catch (_error:Error) {
				trace("Load xml error: " + _error);
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
		private function loadCompletedHandler(_event:Event):void {
			this.dispatchEvent(new XmlLoaderEvent("compeleted", _event.target.data));
		}
		private function loadErrorHandler(_event:IOErrorEvent):void {
			this.dispatchEvent(new XmlLoaderEvent("failed",""));
		}
		private function cancelHandler(_event:Event):void {
		}
		private function httpStatusHandler(_event:HTTPStatusEvent):void {
		}
		private function openHandler(_event:Event):void {
		}
		private function progressHandler(_event:ProgressEvent):void {
		}
		private function securityErrorHandler(_event:SecurityErrorEvent):void {
		}
	}
}