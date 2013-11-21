package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.PreferenceEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Preference;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.external.ExternalInterface;
	import flash.filters.BitmapFilterQuality;
	import flash.filters.GlowFilter;
	import flash.net.URLRequest;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import flash.system.Security;
	import flash.system.LoaderContext;
	
	public class PreferenceContainer extends Sprite {
		private var _preference:Preference;

		private var _maxWidth:Number=360;
		private var _maxHeight:Number=400;
		private var _actualHeight:Number = _maxHeight;

		private var _nameTextField:StaticTextField;
		// Note: the _imageContainer is for use buttonMode on _imageLoader.
		private var _imageContainer:Sprite;
		private var _infoButton:InfoButton;

		private var _shadowFilter:GlowFilter;
		private var _imageLoaded:Boolean = false;
		private var _isSelected:Boolean = false;

		private var _themeColor:uint = 0x666666;

		public function PreferenceContainer(_preferenceTarget:Preference) {
			trace("[REFACTORING:com.ethos.changetech.controls.PreferenceContainer] - to use ColorableSprite.");
			_preference = _preferenceTarget;
			initText();
			initImage();
			initShadow();
			initInfoButton();
		}
		public function get preference():Preference {
			return _preference;
		}
		public function get themeColor():uint {
			return _themeColor;
		}
		public function set themeColor(_value:uint):void {
			if (_value == _themeColor) {
				return;
			}
			_themeColor = _value;
			_nameTextField.textColor = _themeColor;
			_shadowFilter.color = _themeColor;
			_infoButton.themeColor = _themeColor;
		}
		public function get maxWidth():Number {
			return _maxWidth;
		}
		public function set maxWidth(_value:Number):void {
			if (_value == _maxWidth) {
				return;
			}
			_maxWidth = _value;
			layout();
		}
		public function get maxHeight():Number {
			return _maxHeight;
		}
		public function set maxHeight(_value:Number):void {
			if (_value == _maxHeight) {
				return;
			}
			_maxHeight = _value;
			layout();
		}
		public function get actualHeight():Number {
			return _actualHeight;
		}
		public function get isSelected():Boolean {
			return _isSelected;
		}
		public function set isSelected(_value:Boolean):void {
			if (_value == _isSelected) {
				return;
			}
			_isSelected = _value;
			if (_isSelected) {
				_imageContainer.filters = new Array(_shadowFilter);
			} else {
				_imageContainer.filters = null;
			}
		}
		private function initText():void {
			_nameTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_nameTextField.textColor = _themeColor;
			_nameTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_nameTextField.htmlText = "<preferenceName>" + PageVariableReplacer.replaceAll(_preference.name) + "</preferenceName>";
			addChild(_nameTextField);
		}
		private function initImage():void {
			_imageContainer = new Sprite();
			var _imageLoader:Loader = new Loader();
			_imageLoader.contentLoaderInfo.addEventListener(Event.COMPLETE,imageLoaderCompleteHandler);
			//var _imageURL = String(GlobalValue.getValue("mediaRootURL")) + "originalimagecontainer/" + _preference.imageURL;
			//refactor media url 
			var _imageURL = String(GlobalValue.getValue("originalimagecontainerRoot")) + _preference.imageURL;
			
			var _crossdomainURL = String(GlobalValue.getValue("crossdomainURL"));
			Security.loadPolicyFile(_crossdomainURL);
			var loaderContext = new LoaderContext();
			loaderContext.checkPolicyFile = true;	
			var _imageRequest:URLRequest = new URLRequest(_imageURL);
			_imageLoader.load(_imageRequest,loaderContext);
		}
		private function initShadow():void {
			var _color:Number = _themeColor;
			var _alpha:Number = 0.9;
			var _blurX:Number = 5;
			var _blurY:Number = 5;
			var _strength:Number = 1;
			var _inner:Boolean = false;
			var _knockout:Boolean = false;
			var _quality:Number = BitmapFilterQuality.HIGH;

			_shadowFilter = new GlowFilter(_color, _alpha, _blurX, _blurY, _strength, _quality, _inner, _knockout);
		}
		private function initInfoButton():void {
			_infoButton = new InfoButton();
			_infoButton.themeColor = _themeColor;
			_infoButton.addEventListener(MouseEvent.CLICK, infoButtonClickedHandler);
		}
		private function layout():void {
			_nameTextField.width = _maxWidth;

			if (_imageLoaded) {
				var _xscale:Number = 1;
				var _yscale:Number = 1;
				var _totalScale:Number = 1;
				_imageContainer.scaleX = 1;
				_imageContainer.scaleY = 1;
				if (_imageContainer.width > _maxWidth) {
					_xscale = _maxWidth / _imageContainer.width;
				}
				if (_imageContainer.height > _maxHeight - _nameTextField.height) {
					_yscale = (_maxHeight - _nameTextField.height) / _imageContainer.height;
				}
				_totalScale = Math.min(_xscale, _yscale);
				_imageContainer.scaleX = _totalScale;
				_imageContainer.scaleY = _totalScale;
				_imageContainer.x = (_maxWidth - _imageContainer.width) / 2;
				_imageContainer.y = _nameTextField.y + _nameTextField.height;
				_infoButton.x = _imageContainer.x + _imageContainer.width - _infoButton.width - 3;
				_infoButton.y = _imageContainer.y + _imageContainer.height - _infoButton.height - 3;
				_actualHeight = _imageContainer.y + _imageContainer.height;
			} else {
				_actualHeight = _maxHeight;
			}
			this.dispatchEvent(new PageEvent("arranged", null));
		}
		private function imageLoaderCompleteHandler(_event:Event):void {	
			_imageLoaded = true;
			var _imageLoader:Loader = Loader(_event.target.loader);
			var _imageBitmap:Bitmap = Bitmap(_imageLoader.content);
			_imageBitmap.smoothing = true;
			//_imageLoader.mouseChildren = false;
			_imageContainer.addChild(_imageBitmap);
			_imageContainer.mouseChildren = false;
			_imageContainer.buttonMode = true;
			_imageContainer.useHandCursor = true;
			_imageContainer.addEventListener(MouseEvent.CLICK, imageClickedHandler);
			addChild(_imageContainer);
			addChild(_infoButton);
			layout();
		}
		private function imageClickedHandler(_event:MouseEvent):void {
			this.dispatchEvent(new PreferenceEvent("choosed"));
		}
		private function infoButtonClickedHandler(_event:MouseEvent):void {
			var _messageObject:MessageObject = GlobalValue.getValue("messages")["PreferenceInfo"];
			_messageObject.message = PageVariableReplacer.replaceAll(_preference.description);
			//_messageObject.messageImageURL = String(GlobalValue.getValue("mediaRootURL")) + "originalimagecontainer/" + _preference.imageURL
			//refactor media url 
			_messageObject.messageImageURL = String(GlobalValue.getValue("originalimagecontainerRoot")) + _preference.imageURL;
			
			this.dispatchEvent(new PageEvent("info", _messageObject));
		}
	}
}