package com.ethos.changetech.controls{
	import com.ethos.changetech.models.Media;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.MediaLoader;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.data.GlobalValue;
	import com.ning.components.media.SoundPlayer;	
	import fl.transitions.Tween;
	import fl.video.AutoLayoutEvent;
	import fl.video.FLVPlayback;
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;

	public class TextBoxContentStandardTemplate extends TextBoxContentStandardBasedTemplate {

		// Layout
		private var _mediaWidthPercentage:Number;
		
		// Tween
		private var _illustrationTweenAlpha:Tween;
		
		private var _pageContainer:PageContainer;
		
		private var tb:Table;
		
		public function set pageContainer(_value:PageContainer):void{
			_pageContainer = _value;
		}

		public function TextBoxContentStandardTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number, pageContainer:PageContainer) {
			super(_targetPage, _w, _minH, _maxH);			
			_pageContainer = pageContainer;
		}
		override protected function initLayout():void {
			super.initLayout();
			_mediaWidthPercentage = Math.min(1, Math.max(GlobalValue.getValue("layout")["MediaWidthPercentage"], 0));
		}
		override protected function updateInnerContent():void {				
			if(_tabelStringField!=null && _tabelStringField.length > 0)
			{				
			tb = new Table();
				layout();
				var tv:TableVo = TableVoParse.parseData(_tabelStringField);
						tb = new Table();
						_innerContainer=tb;
						addChild(_innerContainer);									
						tb.x = 790;
						tb.y = 500;
						tb.setData(tv);
						
				//layout();
				//_pageContainer.layout();
				
			}
			if (_page.medias.length == 0) {
				return;
			}
			if (_page.medias.length > 1) {
				throw new IllegalOperationError("Invalid media amount at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + " : The amount of media in standard template should be not more than one.");
			}
			var _media:Media = _page.medias[0];
			//var _mediaRoot:String = String(GlobalValue.getValue("mediaRootURL"));
			//refactor media url 
			
			switch (_media.type) {
				case "Illustration" :
					MediaLoader.loadImage(_media.mediaURL, illustrationLoadCompleteHandler);
					break;
				case "Audio" :
					Debug.trace("Hello media.");
					//var _soundURL:String = _mediaRoot + "audiocontainer/" + _media.mediaURL;
					//refactor media url 
					 var _soundURL:String = String(GlobalValue.getValue("audiocontainerRoot"))  + _media.mediaURL;
					Debug.trace("Audio URL = " + _soundURL);
					var _soundPlayer:SoundPlayer = new SoundPlayer();					
					_soundPlayer.width = _contentWidth * _mediaWidthPercentage;
					_soundPlayer.skinBackgroundAlpha = 0.2;
					_soundPlayer.skinBackgroundColor = 0xcccccc;
					_soundPlayer.source = _soundURL;
					//_soundPlayer.source ="http://20090806.xspop.cn/831music/%E9%83%AD%E5%BE%B7%E7%BA%B2/%E5%B8%88%E5%82%85%E7%BB%8F%EF%BC%88%E9%83%AD%E5%BE%B7%E7%BA%B2%20%E5%BC%A0%E6%96%87%E9%A1%BA%EF%BC%89.mp3";
					_innerContainer = _soundPlayer;
					addChild(_innerContainer);
					break;
				case "Video" :
					//var _videoURL:String = _mediaRoot + "videocontainer/" + _media.mediaURL;
					//refactor media url 
					 var _videoURL:String = String(GlobalValue.getValue("videocontainerRoot"))  + _media.mediaURL;
					 
					trace("Video URL = "+ _videoURL);
					var _flvPlayback:FLVPlayback = new FLVPlayback();
					trace("yyyyyyyyyyyy"+_contentWidth);
					_flvPlayback.width = _contentWidth * _mediaWidthPercentage;
					_flvPlayback.height = _flvPlayback.width * 0.49;
					_flvPlayback.addEventListener(AutoLayoutEvent.AUTO_LAYOUT, videoAutoLayoutHandler);					
					_flvPlayback.skin = "Flash/SkinOverPlaySeekStop.swf";
					_flvPlayback.skinAutoHide = true;
					_flvPlayback.skinBackgroundAlpha = 0.2;
					_flvPlayback.skinBackgroundColor = 0xcccccc;
					_flvPlayback.autoPlay = false;
					_flvPlayback.source = _videoURL;
					_innerContainer = _flvPlayback;
					addChild(_innerContainer);
					break;
				default :
					throw new IllegalOperationError("Invalid media type at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString());
			}
		}
		private function videoAutoLayoutHandler(_event:AutoLayoutEvent):void {
			layout();
		}
		private function illustrationLoadCompleteHandler(_event:Event):void {
			trace("illustration image loaded.");
			var _illustrationLoader:Loader = Loader(_event.target.loader);
			var _illustrationBitmap:Bitmap = Bitmap(_illustrationLoader.content);
			_illustrationBitmap.smoothing = true;
			//if (_illustrationBitmap.width > _contentWidth * _mediaWidthPercentage) {
				//var _scaleX:Number = (_contentWidth * _mediaWidthPercentage)/ _illustrationBitmap.width;
				//_illustrationBitmap.scaleX = _scale;
				//_illustrationBitmap.scaleY = _scale;
			//}
			_illustrationBitmap.scaleX = 0.3;
			_illustrationBitmap.scaleY = 0.3;
			_innerContainer = _illustrationBitmap;
			_innerContainer.alpha = 0;
			addChild(_innerContainer);
			layout();
			_pageContainer.layout();
		}
		override public function arrangeFeedback():void {
			_illustrationTweenAlpha = new Tween(_innerContainer, "alpha", null, _innerContainer.alpha, 1, 5, false);
		}
	}
}