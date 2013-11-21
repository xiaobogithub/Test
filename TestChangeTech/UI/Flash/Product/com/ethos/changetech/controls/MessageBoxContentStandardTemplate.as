package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.events.Event;
	import flash.net.URLRequest;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBoxContentStandardTemplate extends MessageBoxContentTemplate {

		// Layout
		private var _imageWidthMaxPercentage:Number;

		private var _message:String;
		private var _imageURL:String;

		private var _messageTextField:StaticTextField;
		private var _imageBitmap:Bitmap;

		public function MessageBoxContentStandardTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			_message = _object.message;
			_imageURL = _object.messageImageURL;
		}
		override protected function initLayout():void {
			super.initLayout();
			_imageWidthMaxPercentage = GlobalValue.getValue("layout")["MessageBoxImageWidthMaxPercentage"];
		}
		override protected function initContainer():void {
			_messageTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_messageTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_messageTextField.htmlText = "<messageText>" + _message + "</messageText>";
			addChild(_messageTextField);

			if (_imageURL != null && _imageURL.length > 0) {
				var _imageLoader:Loader = new Loader();
				_imageLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, imageLoaderCompleteHandler);
				_imageLoader.load(new URLRequest(_imageURL));
			}
		}
		override protected function layout():void {
			var _oldHeight = height;
			var _imageWidth:Number = 0;
			var _imageHeight:Number = 0;
			if (_imageBitmap != null) {
				_imageWidth = _imageBitmap.width + _textMargin;
				_imageHeight = _imageBitmap.height;
				_imageBitmap.x = _horizontalPadding;
			}
			_messageTextField.width = _contentWidth - _imageWidth;
			_messageTextField.x = _horizontalPadding + _imageWidth;

			if (_imageBitmap != null && _imageHeight >= _messageTextField.height) {
				_imageBitmap.y = 0;
				_messageTextField.y = (_imageBitmap.height - _messageTextField.height) / 2;
				height = _imageBitmap.height;
			} else {
				_messageTextField.y = 0;
				if (_imageBitmap != null) {
					_imageBitmap.y = (_messageTextField.height - _imageBitmap.height) / 2;
				}
				height = _messageTextField.height;
			}
			if(_oldHeight != height) {
				this.dispatchEvent(new PageEvent("arranged", null));
			}
		}
		private function imageLoaderCompleteHandler(_event:Event):void {
			trace("message image loaded.");
			var _imageLoader:Loader = Loader(_event.target.loader);
			_imageBitmap = Bitmap(_imageLoader.content);
			_imageBitmap.smoothing = true;
			if (_imageBitmap.width > _contentWidth * _imageWidthMaxPercentage) {
				var _scale:Number = (_contentWidth * _imageWidthMaxPercentage)/ _imageBitmap.width;
				_imageBitmap.scaleX = _scale;
				_imageBitmap.scaleY = _scale;
			}
			addChild(_imageBitmap);
			layout();
		}
	}
}