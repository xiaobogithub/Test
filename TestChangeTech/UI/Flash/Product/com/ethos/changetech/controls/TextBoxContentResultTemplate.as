package com.ethos.changetech.controls{
	import com.ethos.changetech.data.PageVariables;
	import com.ethos.changetech.models.Media;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.MediaLoader;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.data.GlobalValue;
	import com.ning.components.media.SoundPlayer;	
	import com.ning.text.StaticTextField;
	import fl.transitions.Tween;
	import fl.video.AutoLayoutEvent;
	import fl.video.FLVPlayback;
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.net.navigateToURL;
	import flash.net.URLRequest;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormat;

	public class TextBoxContentResultTemplate extends TextBoxContentStandardBasedTemplate {

		// Layout
		private var _mediaWidthPercentage:Number;
		
		// Tween
		private var _illustrationTweenAlpha:Tween;
		
		private var _pageContainer:PageContainer;
		
		private var tb:Table;
		private var resultArray:Array;
		
		public function set pageContainer(_value:PageContainer):void{
			_pageContainer = _value;
		}

		public function TextBoxContentResultTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number, pageContainer:PageContainer=null) {
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
			
			this.y += 130;
			
			var lineLen:uint = _page.xmlData.ResultLines.ResultLine.length();
			resultArray = [];
			for (var i:uint = 0; i < lineLen; i++)
			{
				var mc:MovieClip = createText();
				mc.tt.text = _page.xmlData.ResultLines.ResultLine[i].@Text;
				addChild(mc);
				resultArray.push(mc);
				mc.url = _page.xmlData.ResultLines.ResultLine[i].@Url;
				
				var varValue:String = String(PageVariables.getProperty(_page.xmlData.ResultLines.ResultLine[i].@ProgramVariable));
				var imgURL:String = _page.xmlData.ResultGraphs.ResultGraph.(@VariableValue == varValue).@Image;
				trace("Image: "+imgURL);
				var loader:Loader = new Loader();
				loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioError);
				loader.contentLoaderInfo.addEventListener(Event.COMPLETE, loadedImg);
				loader.name = i.toString();
				loader.load(new URLRequest(GlobalValue.getValue("originalimagecontainerRoot")+imgURL));
				//addChild(loader);
				trace("URL :"+GlobalValue.getValue("originalimagecontainerRoot")+imgURL);
			}
			
			
			
		}
		
		private function loadedImg(e:Event):void 
		{
			var loader:Loader = e.target.loader;			
			
			/*var sy:Number = 1;
			
			if (loader.height > 30)
			sy = 30 / loader.height;
			
			loader.height *= sy;
			loader.width *= sy;*/
			
			loader.x = 380;
			//loader.y = resultArray[loader.name].y + (resultArray[loader.name].height - loader.height) / 2;
			loader.y = (resultArray[loader.name].height - loader.height) / 2;
			
			resultArray[loader.name].addChild(loader);
			
			layout();
		}
		
		private function ioError(e:IOErrorEvent):void 
		{
			trace(e);
		}
		
		private function createText():MovieClip 
		{
			var tf:StaticTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			var textF:TextFormat = new TextFormat("Arial", 16, 0x333333);
			tf.defaultTextFormat = textF;
			var mc:MovieClip = new MovieClip();
			mc.tt = tf;
			mc.addChild(tf);
			mc.buttonMode = true;
			mc.mouseChildren = false;
			mc.addEventListener(MouseEvent.MOUSE_OVER, overText);
			mc.addEventListener(MouseEvent.MOUSE_OUT, outText);
			mc.addEventListener(MouseEvent.CLICK, clickText);
			return mc;			
		}
		
		private function clickText(e:MouseEvent):void 
		{
			navigateToURL(new URLRequest(e.currentTarget.url),"_self");
		}
		
		private function outText(e:MouseEvent):void 
		{
			e.currentTarget.tt.textColor = 0x333333;
		}
		
		private function overText(e:MouseEvent):void 
		{
			e.currentTarget.tt.textColor = 0x000000;
		}
		
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			var _maxLabelWidth:Number = 0;
			
			_textTextField.y -= 10;
			
			_lastPositionY = _textTextField.y + _textTextField.height+20;
			
			for (var i in resultArray)
			{
				resultArray[i].x = _horizontalPadding;
				//resultArray[i].y = _lastPositionY +i*(resultArray[i].height+10)+5;
				resultArray[i].y = _lastPositionY +10;
				_lastPositionY = resultArray[i].y + resultArray[i].height;
			}
			//_lastPositionY = resultArray[resultArray.length-1].y + resultArray[resultArray.length-1].height;
			
			return _lastPositionY;
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