package com.ethos.changetech.controls{
	import com.ethos.changetech.models.Media;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.MediaLoader;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.components.media.SoundPlayer;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import fl.transitions.Tween;
	import fl.transitions.TweenEvent;
	import flash.display.Bitmap;
	import flash.display.DisplayObject;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import flash.utils.Timer;
	import flash.utils.setTimeout;
	
	public class TextBoxContentPushPicturesTemplate extends TextBoxContentTemplate {
		
		private var _pushPictureContainer:Sprite;
		private var _timer:Timer;
		
		private var _pushPictureContainerTweenAlpha:Tween;
		private var _textTextField:StaticTextField;
		protected var _innerContainer:DisplayObject;
		
		public function TextBoxContentPushPicturesTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
			if (_page.interval <= 0) {
				throw new IllegalOperationError("Invalid interval at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + ": Interval must be set for push picturres page.");
			}
			/*
			if(_page.pushPicture.length <= 0) {
				throw new IllegalOperationError("Push picture url at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + ": Push picture must be set for push picturres page.");
			}
			*/
		}
		override protected function initContent():void {
			_pushPictureContainer = new Sprite();
			addChild(_pushPictureContainer);
			_textTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_textTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			if (_page.text.length > 0) {
				_textTextField.htmlText = "<pushPictureText>" + PageVariableReplacer.replaceAll(_page.text) + "<pushPictureText>";
			}
			addChild(_textTextField);
			Debug.trace(_page.pushPicture.length);
			if (_page.pushPicture.length > 0) {
				MediaLoader.loadImage(_page.pushPicture, pushPictureLoadCompleteHandler);
			}else {
				setTimeout(layout, 500);
			}
		}
		private function pushPictureLoadCompleteHandler(_event:Event):void {
			Debug.trace("push picture loaded.");
			var _pushPictureLoader:Loader = Loader(_event.target.loader);
			var _pushPictureBitmap:Bitmap = Bitmap(_pushPictureLoader.content);
			_pushPictureBitmap.smoothing = true;
			var _scaleX:Number = 1;
			var _scaleY:Number = 1;
			if (_pushPictureBitmap.width > _contentWidth) {
				_scaleX = _contentWidth / _pushPictureBitmap.width;				
			}
			if (_pushPictureBitmap.height > _maxHeight) {
				_scaleY = (_maxHeight - 60)  / _pushPictureBitmap.height;
			}
			var _scale = Math.min(_scaleX, _scaleY);
			_pushPictureBitmap.scaleX = _scale;
			_pushPictureBitmap.scaleY = _scale;
			_pushPictureBitmap.x = 0;
			_pushPictureBitmap.y = 60;
			_pushPictureContainer.addChild(_pushPictureBitmap);
			Debug.trace("_pushPictureContainer.height:"+_pushPictureContainer.height);
			_pushPictureContainer.alpha = 0;
			layout();
		}
		override public function freeze():void {
			if (_timer != null) {
				_timer.stop();
			}
		}
		override public function unfreeze():void {
			if (_timer != null) {
				_timer.start();
			}
		}	
		override protected function layout():void {
			_textTextField.x = _horizontalPadding;
			_textTextField.y = _textMargin;
			_textTextField.width = _contentWidth;
			if (_page.pushPicture.length > 0) {
				_pushPictureContainer.x = (_contentWidth - _pushPictureContainer.width) / 2 + _horizontalPadding;
				_pushPictureContainer.y = 0;
				if (_pushPictureContainer.height != height) {
					height = _pushPictureContainer.height;
				}
			}else {
				height = _textTextField.y + _textTextField.height;
			}
			arrangePage();
		}
		protected function updateInnerContent():void {
			if (_page.medias.length == 0) {
				return;
			}
			if (_page.medias.length > 1) {
				throw new IllegalOperationError("Invalid media amount at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + " : The amount of media in standard template should be not more than one.");
			}
			var _media:Media = _page.medias[0];
			//var _mediaRoot:String = String(GlobalValue.getValue("mediaRootURL"));
			//refactor media url 
			var _mediaRoot:String = String(GlobalValue.getValue("audiocontainerRoot"));
			switch (_media.type) {
				case "Audio" :
					//var _soundURL:String = _mediaRoot + "audiocontainer/" + _media.mediaURL;
					//refactor media url 
					var _soundURL:String = _mediaRoot + _media.mediaURL;
					
					var _soundPlayer:SoundPlayer = new SoundPlayer();	
					_soundPlayer.autoPlay = true;
					_soundPlayer.source = _soundURL;
					_soundPlayer.alpha = 0;
					_innerContainer = _soundPlayer;
					addChild(_innerContainer);
					break;
				default :
					throw new IllegalOperationError("Invalid media type at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString());
			}
		}
		override public function arrangeFeedback():void {
			Debug.trace("arrangeFeedback");
			_pushPictureContainerTweenAlpha = new Tween(_pushPictureContainer, "alpha", null, _pushPictureContainer.alpha, 1, 5, false);
			_pushPictureContainerTweenAlpha.addEventListener(TweenEvent.MOTION_FINISH, _pushPictureContainerMotionFinishHandler);
		}
		private function _pushPictureContainerMotionFinishHandler(_event:TweenEvent):void {
			updateInnerContent();
			_pushPictureContainerTweenAlpha.removeEventListener(TweenEvent.MOTION_FINISH, _pushPictureContainerMotionFinishHandler);
			startTimer();
		}
		private function startTimer():void {
			_timer = new Timer(100, _page.interval * 10);
			_timer.addEventListener(TimerEvent.TIMER_COMPLETE, timerCompleteHandler);
			_timer.start();
		}		
		private function timerCompleteHandler(_event:TimerEvent):void {
			nextPage();
		}
	}
}