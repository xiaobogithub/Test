package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.MediaLoader;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.data.GlobalValue;
	import com.ning.display.ColorableSprite;
	import com.ning.events.LoginEvent;
	import fl.containers.ScrollPane;
	import fl.controls.ScrollPolicy;
	import fl.transitions.easing.*;
	import fl.transitions.Tween;
	import fl.transitions.TweenEvent;
	import fl.motion.Color;
	import fl.core.UIComponent;
	import flash.geom.ColorTransform;
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.BlendMode;
	import flash.display.DisplayObject;
	import flash.display.Loader;
	import flash.display.Shape;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.geom.Point;
	import flash.net.LocalConnection;

	public class PageContainer extends ColorableSprite {

		// Layout
		private var _stageWidth:Number;
		private var _stageHeight:Number;
		private var _backgroundFillColor:Number;
		//private var _backgroundImageFillShape:DisplayObject;
		private var _backgroundImageWidth:Number;
		private var _backgroundImageHeight:Number;
		private var _textBoxInterval:Number;
		private var _textBoxWidth:Number;
		private var _textBoxInnerWidth:Number;
		private var _textBoxMaxHeight:Number;
		private var _textBoxMinHeight:Number;
		private var _textBoxInnerMaxHeight:Number;
		private var _textBoxInnerMinHeight:Number;
		
		private var _textBoxSingleWidth:Number;
		private var _textBoxInnerSingleWidth:Number;
		private var _textBoxSingleMaxHeight:Number;
		private var _textBoxSingleMinHeight:Number;
		private var _textBoxInnerSingleMaxHeight:Number;
		private var _textBoxInnerSingleMinHeight:Number;
		
		private var _textBoxPadding:Number;
		private var _scrollBarWidth:Number;
		
		private var _presenterImageWidth:Number;
		private var _presenterImageHeight:Number;
		private var _presenterImageHorizontalOverlap:Number;
		private var _presenterImageVerticalTopDiff:Number;
		
		private var _presenterImageBigWidth:Number;
		private var _presenterImageBigHeight:Number;
		private var _presenterImageBigHorizontalOverlap:Number;
		private var _presenterImageBigVerticalTopDiff:Number;
		
		
		private var _coverShadowHorizontalOverlap:Number;
		private var _coverShadowEnterHorizontalOverlap:Number;
		private var _navigationBarOverlapX:Number;
		private var _navigationBarOverlapY:Number;
		private var _navigationBarWidth:Number;
		private var _navigationBarHeight:Number;
		
		// Model
		protected var _page:Page;

		// Flag
		protected var _presenterImageLoaded:Boolean = false;
		protected var _backgroundImageLoaded:Boolean = false;
		protected var _coverShadowXMotionFinished:Boolean = false;

		// Containers
		protected var _backgroundContainer:Sprite;
		protected var _backgroundBitmapContainer:Sprite;
		protected var _presenterImageContainer:Sprite;
		protected var _presenterImageBitmapContainer:Sprite;
		protected var _coverShadow:CoverShadow;
		protected var _textBoxBackground:PageContentBackground;
		protected var _textBoxScrollPane:ScrollPane;
		protected var _textBoxScrollPaneSourceContainer:TextBoxContentTemplate;
		protected var _navigationBar:NavigationBar;
		protected var _navigationBarContainer:Sprite;
		protected var _navigationBarButtonContainer:Sprite;
		protected var _setableButton:Sprite;

		// Tween
		private var _presenterImageContainerTweenAlpha:Tween;
		private var _backgroundImageContainerTweenAlpha:Tween;
		private var _coverShadowTweenX:Tween;
		private var _coverShadowTweenAlpha:Tween;
		private var _textBoxBackgroundTweenX:Tween;
		private var _textBoxBackgroundTweenY:Tween;
		private var _textBoxBackgroundTweenWidth:Tween;
		private var _textBoxBackgroundTweenHeight:Tween;
		private var _textBoxBackGroundTweenAlpha:Tween;
		private var _textBoxScrollPaneTweenY:Tween;
		private var _navigationBarTweenX:Tween;
		private var _navigationBarTweenY:Tween;
		private var _navigationBarTweenWidth:Tween;
		private var _navigationBarTweenHeight:Tween;
		private var _navigationBarTweenAlpha:Tween;
		private var _presenterImageBitmap:Bitmap;
		private var _backgroundImageBitmap:Bitmap;
		public function PageContainer() {
			initLayout();
			initContainer();			
		}
		public function get page():Page {
			return _page;
		}
		public function set page(_value:Page):void {
			_page = _value;
			//layout();
		}
		public function get textBoxContent():TextBoxContentTemplate {
			return _textBoxScrollPaneSourceContainer;
		}
		private function get pageWidth():Number {
			if (_page == null) {
				return 0;
			}
			var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			return _page.presenterImage.length > 0?tempWidth + tempTextWidth + _textBoxInterval - _presenterImageHorizontalOverlap:tempTextWidth;
		}
		private function get presenterImageBitmapX():Number {
			if (_page == null) {
				return 0;
			}
			var _positionX:Number;
			/*
			if (_page.presenterImageMode == "Big") {
				if (_page.presenterImagePosition == "Right") {
					_positionX = _presenterImageBigWidth - _presenterImageBitmap.width;
				} else {
					_positionX = 0;
				}
			}else{
				_positionX = (_presenterImageWidth - _presenterImageBitmap.width) / 2;
			}
			*/
			var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
			_positionX = (tempWidth - _presenterImageBitmap.width) / 2;
			return _positionX;
		}

		private function get presenterImageX():Number {
			if (_page == null) {
				return 0;
			}
			
			var _positionX:Number;
			/*
			if (_page.presenterImageMode == "Big") {
				if (_page.presenterImagePosition == "Right") {
					//_positionX = _stageWidth - _presenterImageBigWidth;
					
					var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
					var tempTextWidth = _page.singleMode?_textBoxSingleWidth:_textBoxWidth;
					_positionX = textBoxX + tempTextWidth + _textBoxInterval - _presenterImageHorizontalOverlap;
				} else {
					//_positionX = 0;
					_positionX = (_stageWidth - pageWidth) / 2;
				}
			}else{
				if (_page.presenterImagePosition == "Right") {
					var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
					var tempTextWidth = _page.singleMode?_textBoxSingleWidth:_textBoxWidth;
					_positionX = textBoxX + tempTextWidth + _textBoxInterval - _presenterImageHorizontalOverlap;
				} else {
					_positionX = (_stageWidth - pageWidth) / 2;
				}
			}
			*/
			if (_page.presenterImagePosition == "Right") {
				var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
				var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
				_positionX = textBoxX + tempTextWidth + _textBoxInterval - _presenterImageHorizontalOverlap;
			} else {
				_positionX = (_stageWidth - pageWidth) / 2;
			}
			return _positionX;
		}
		private function get presenterImageY():Number {
			if (_page == null) {
				return 0;
			}
			var _positionY:Number;
			if (_page.presenterImageMode == "Big") {
				//_positionY = _stageHeight - _presenterImageBigHeight;
				_positionY = (_stageHeight - _presenterImageBigHeight - _coverShadow.height / 2) / 2;
			}else{
				_positionY = (_stageHeight - _presenterImageHeight - _coverShadow.height / 2) / 2;
			}
			return _positionY;
		}
		private function get coverShadowX():Number {
			if (_page == null) {
				return 0;
			}
			var _positionX:Number;
			if (_page.presenterImagePosition == "Right") {
				_positionX = _coverShadow.width + presenterImageX + _presenterImageWidth - _coverShadowHorizontalOverlap;
			} else {
				_positionX = presenterImageX + _coverShadowHorizontalOverlap - _coverShadow.width;
			}
			return _positionX;
		}
		private function get coverShadowEnterRoomXStart():Number {
			if (_page == null) {
				return 0;
			}
			var _positionX:Number;
			if (_page.presenterImagePosition == "Right") {
				_positionX = _coverShadow.width + presenterImageX + _presenterImageWidth - _coverShadowEnterHorizontalOverlap;
			} else {
				_positionX = _presenterImageContainer.x + _coverShadowEnterHorizontalOverlap - _coverShadow.width;
			}
			return _positionX;
		}
		private function get coverShadowY():Number {
			var tempHeight = _page.presenterImageMode == "Big"? _presenterImageBigHeight :_presenterImageHeight;
			return _presenterImageContainer.y + tempHeight - _coverShadow.height / 2;
		}
		private function get textBoxX():Number {
			if (_page == null) {
				return 0;
			}
			var _positionX:Number;
			if (_page.presenterImage.length > 0) {
				var tempWidth = _page.presenterImageMode == "Big"?_presenterImageBigWidth:_presenterImageWidth;
				if (_page.presenterImagePosition == "Right") {
					_positionX = (_stageWidth - pageWidth) / 2;
				} else {
					_positionX = (_stageWidth - pageWidth) / 2 + tempWidth + _textBoxInterval - _presenterImageHorizontalOverlap;
					//_positionX = presenterImageX + tempWidth - _presenterImageHorizontalOverlap;
				}
			} else {
				_positionX = (_stageWidth - pageWidth) / 2;
			}
			return _positionX;
		}
		private function get textBoxEnterRoomXStart():Number {
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			var _positionX:Number = (tempTextWidth - 34) / 2 + textBoxX;
			return _positionX;
		}
		private function get textBoxCenterY():Number {
			if (_page == null) {
				return 0;
			}
			var _positionCenterY:Number;
			if (_page.presenterImageMode == "Big") {
				var _positionY = (_stageHeight - _presenterImageBigHeight - _coverShadow.height / 2) / 2;
				_positionCenterY = _positionY + _presenterImageBigHeight / 2 - (_navigationBarHeight - _navigationBarOverlapY) / 2;
			}else {
				_positionCenterY = presenterImageY + _presenterImageHeight / 2 - (_navigationBarHeight - _navigationBarOverlapY) / 2;
			}
			return _positionCenterY;
		}
		private function get textBoxActualWidth():Number {
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			return tempTextWidth;
		}
		private function get textBoxActualHeight():Number {
			return textBoxScrollPaneHeight + _navigationBarOverlapY;
		}
		private function get textBoxScrollPaneHeight():Number {
			if (_textBoxScrollPaneSourceContainer != null) {
				if ( _page.hasVideo )
				{
					return _textBoxInnerSingleMaxHeight + 2 * _textBoxPadding + 30;
				}
				else
				{
					var tempInnerMaxHeight = _page.singleMode ?_textBoxInnerSingleMaxHeight:_textBoxInnerMaxHeight;
					var tempInnerMinHeight = _page.singleMode ?_textBoxInnerSingleMinHeight:_textBoxInnerMinHeight;
					return Math.min(tempInnerMaxHeight,Math.max(_textBoxScrollPaneSourceContainer.height,tempInnerMinHeight)) + 2 * _textBoxPadding;
				}				 
			}
			return 0;
		}
		private function get navigationBarX():Number {
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			return textBoxX + tempTextWidth - _navigationBarOverlapX;
		}
		private function get navigationBarEnterRoomXStart():Number {
			var _positionX:Number = (_navigationBarWidth - 30) / 2 + navigationBarX;
			return _positionX;
		}
		private function get navigationBarY():Number {
			if (_page.hasVideo)
			{
				return textBoxCenterY + _textBoxMaxHeight / 2 - _navigationBarOverlapY + 100;
			}
			else
			{
				return textBoxCenterY + _textBoxMaxHeight / 2 - _navigationBarOverlapY;
			}
		}
		private function get navigationBarEnterRoomYStart():Number {
			var _positionY:Number = (_navigationBarHeight - 50) / 2 + navigationBarY;
			return _positionY;
		}
		override protected function updatePrimaryThemeColor():void {
			//_titleTextField.textColor = _primaryThemeColor;
			//_textBoxBackground.primaryThemeColor = _primaryThemeColor;
			_textBoxBackground.primaryThemeColor = _page.pageSequence.TopBarColor;
			if (_textBoxScrollPaneSourceContainer != null) {
				_textBoxScrollPaneSourceContainer.primaryThemeColor = _page.pageSequence.TopBarColor;
			}
		}
		/*override protected function updateSecondaryThemeColor():void {
		_textTextField.textColor = _secondaryThemeColor;
		_footerTextTextField.textColor = _secondaryThemeColor;
		}*/
		protected function initLayout():void {
			//Debug.trace("single:"+_page.singleMode);
			_backgroundFillColor = GlobalValue.getValue("layout")["BackgroundFillColor"];
			_backgroundImageWidth = GlobalValue.getValue("layout")["BackgroundImageWidth"];
			_backgroundImageHeight = GlobalValue.getValue("layout")["BackgroundImageHeight"];
			
			_textBoxWidth = GlobalValue.getValue("layout")["TextFieldWidth"];
			_textBoxMaxHeight = GlobalValue.getValue("layout")["TextFieldMaxHeight"];
			_textBoxMinHeight = GlobalValue.getValue("layout")["TextFieldMinHeight"];
			_textBoxInterval = GlobalValue.getValue("layout")["TextFieldInterval"];
			
			_textBoxSingleWidth = GlobalValue.getValue("layout")["TextFieldSingleWidth"];
			_textBoxSingleMaxHeight = GlobalValue.getValue("layout")["TextFieldSingleMaxHeight"];
			_textBoxSingleMinHeight = GlobalValue.getValue("layout")["TextFieldSingleMinHeight"];
			
			
			_textBoxPadding = GlobalValue.getValue("layout")["TextFieldPadding"];
			_scrollBarWidth = GlobalValue.getValue("layout")["TextFieldScrollBarWidth"];
			
			_presenterImageWidth = GlobalValue.getValue("layout")["PresenterImageMaxWidth"];
			_presenterImageHeight = GlobalValue.getValue("layout")["PresenterImageHeight"];
			_presenterImageHorizontalOverlap = GlobalValue.getValue("layout")["PresenterImageHorizontalOverlap"];
			_presenterImageVerticalTopDiff = GlobalValue.getValue("layout")["PresenterImageVerticalTopDiff"];
			
			_presenterImageBigWidth = GlobalValue.getValue("layout")["PresenterImageBigMaxWidth"];
			_presenterImageBigHeight = GlobalValue.getValue("layout")["PresenterImageBigHeight"];
			_presenterImageBigHorizontalOverlap = GlobalValue.getValue("layout")["PresenterImageBigHorizontalOverlap"];
			_presenterImageBigVerticalTopDiff = GlobalValue.getValue("layout")["PresenterImageBigVerticalTopDiff"];
			
			_coverShadowHorizontalOverlap = GlobalValue.getValue("layout")["CoverShadowHorizontalOverlap"];
			_coverShadowEnterHorizontalOverlap = GlobalValue.getValue("layout")["CoverShadowEnterHorizontalOverlap"];
			_navigationBarOverlapX = GlobalValue.getValue("layout")["NavigationBarOverlapX"];
			_navigationBarOverlapY = GlobalValue.getValue("layout")["NavigationBarOverlapY"];
			_navigationBarWidth = GlobalValue.getValue("layout")["NavigationBarWidth"];
			_textBoxInnerWidth = _textBoxWidth - 2 * _textBoxPadding;
			_textBoxInnerSingleWidth = _textBoxSingleWidth - 2 * _textBoxPadding;
			_textBoxInnerMaxHeight = _textBoxMaxHeight - 2 * _textBoxPadding - _navigationBarOverlapY;
			_textBoxInnerSingleMaxHeight = _textBoxSingleMaxHeight - 2 * _textBoxPadding - _navigationBarOverlapY;
			_textBoxInnerMinHeight = _textBoxMinHeight - 2 * _textBoxPadding - _navigationBarOverlapY;
			_textBoxInnerSingleMinHeight = _textBoxSingleMinHeight - 2 * _textBoxPadding - _navigationBarOverlapY;
		}
		protected function initContainer():void {
			_backgroundContainer = new Sprite();
			//_backgroundImageFillShape = new Bitmap(new BitmapData(10, 10, false, _backgroundFillColor));
			//_backgroundContainer.addChild(_backgroundImageFillShape);
			_backgroundContainer.alpha = 0;
			addChild(_backgroundContainer);						

			_presenterImageContainer = new Sprite();
			_presenterImageContainer.blendMode = BlendMode.MULTIPLY;
			_presenterImageContainer.alpha = 0;
			addChild(_presenterImageContainer);
			
			_coverShadow = new CoverShadow();
			_coverShadow.alpha = 0;
			addChild(_coverShadow);					
			
			_textBoxBackground = new PageContentBackground();
			_textBoxBackground.alpha = 0;
			addChild(_textBoxBackground);

			_textBoxScrollPane = new ScrollPane();
			_textBoxScrollPane.horizontalScrollPolicy = ScrollPolicy.OFF;
			_textBoxScrollPane.setStyle("contentPadding", _textBoxPadding);
			_textBoxScrollPane.alpha = 0;			
			addChild(_textBoxScrollPane);
							  
			_navigationBar = new NavigationBar();
			_navigationBar.width = _navigationBarWidth;
			_navigationBar.alpha = 0;
			_navigationBarHeight = _navigationBar.height;
			addChild(_navigationBar);

			_navigationBarContainer = new Sprite();
			_navigationBarContainer.alpha = 0;
			addChild(_navigationBarContainer);
		}
		public function setStageSize(_w:Number, _h:Number):void {
			var _sizeChanged:Boolean = false;
			if (_w != _stageWidth) {
				_stageWidth = _w;
				_sizeChanged = true;
			}
			if ( _h != _stageHeight) {
				_stageHeight = _h;
				_sizeChanged = true;
			}
			if (_sizeChanged) {
				layout();
			}
		}
		private function initTextBoxContent():void {
			switch (_page.type) {
				case "Login" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentLoginTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Password reminder" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentPasswordReminderTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Account creation" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentRegisterTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Session ending" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentSessionEndingTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Standard" :
					var tempBoxInnerWidth = _page.singleMode || _page.hasVideo?_textBoxInnerSingleWidth:_textBoxInnerWidth;
					var tempInnerMinHeight = _page.singleMode || _page.hasVideo?_textBoxInnerSingleMinHeight:_textBoxInnerMinHeight;
					var tempInnerMaxHeight = _page.singleMode || _page.hasVideo?_textBoxInnerSingleMaxHeight:_textBoxInnerMaxHeight;
					
					_textBoxScrollPaneSourceContainer = new TextBoxContentStandardTemplate(_page, tempBoxInnerWidth, tempInnerMinHeight, tempInnerMaxHeight, this);
					break;
				case "Get information" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentGetInfoTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Push pictures" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentPushPicturesTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Choose preferences" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentChoosePreferenceTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Timer" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentTimerTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Graph" :
					_textBoxScrollPaneSourceContainer = new TextBoxContentGraphTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "PinCode":
					_textBoxScrollPaneSourceContainer = new TextBoxContentPinCodeTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Payment":
					_textBoxScrollPaneSourceContainer = new TextBoxContentPaymentTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				case "Screening results"://new template at 2012/09/27 by Niel
					_textBoxScrollPaneSourceContainer = new TextBoxContentResultTemplate(_page, _textBoxInnerWidth, _textBoxInnerMinHeight, _textBoxInnerMaxHeight);
					break;
				default :
					throw new IllegalOperationError("Invalid page type at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString());
			}
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.ARRANGED, arrangePageHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.INFO, infoPageHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(SubmitEvent.SUBMIT, submitHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.STARTLOADING, pageStartLoadingHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.STOPLOADING, pageStopLoadingHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(PageEvent.SETBUTTON, setButtonHandler);
			_textBoxScrollPaneSourceContainer.addEventListener(LoginEvent.COMPLETE, loginCompleteHandler);
			_textBoxScrollPaneSourceContainer.primaryThemeColor = _page.pageSequence.TopBarColor;
			_textBoxScrollPane.source = _textBoxScrollPaneSourceContainer;
			_textBoxScrollPane.verticalScrollPosition = 0;
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			_textBoxScrollPane.width = tempTextWidth;
			_textBoxScrollPane.height = textBoxScrollPaneHeight;
			_textBoxScrollPane.update();
			_textBoxBackground.primaryThemeColor = _page.pageSequence.TopBarColor;
		}
		private function initNavigationBarContent():void {
			if (_navigationBarButtonContainer != null && _navigationBarContainer.contains(_navigationBarButtonContainer)) {
				_navigationBarContainer.removeChild(_navigationBarButtonContainer);
			}
			_navigationBarButtonContainer = new Sprite();
			var _primaryButton:PrimaryButton = new PrimaryButton(_page);
			_primaryButton.label = "<buttonName>" + PageVariableReplacer.replaceAll(_page.primaryButtonName) + "</buttonName>";
			//_primaryButton.primaryThemeColor = _primaryThemeColor;
			_primaryButton.addEventListener(MouseEvent.CLICK, primaryButtonClickedHandler);
			_setableButton = _primaryButton;
			_navigationBarButtonContainer.addChild(_primaryButton);
			var _secondaryButton:SecondaryButton = new SecondaryButton();
			_secondaryButton.addEventListener(MouseEvent.CLICK, secondaryButtonClickedHandler);
			if(_page.type == "Login" || _page.type == "Password reminder" || _page.type == "Session ending") {
				_secondaryButton.enabled = false;
			}
			_navigationBarButtonContainer.addChild(_secondaryButton);
			_navigationBarContainer.addChild(_navigationBarButtonContainer);

			_secondaryButton.x = 0;
			_secondaryButton.y = 0;
			_primaryButton.width = _navigationBarWidth - _secondaryButton.width - 10;
			_primaryButton.x = _secondaryButton.x + _secondaryButton.width + 6;
			_primaryButton.y = 0;
		}
		private function doClearance() : void {
			 try{
			   new LocalConnection().connect("clearbitmap");
			   new LocalConnection().connect("clearbitmap");
			 }catch(error : Error){
				   
			 }                      
		}

		private function removeBackground():void {
			if (_backgroundBitmapContainer != null && _backgroundContainer.contains(_backgroundBitmapContainer)) {
				if (_backgroundImageBitmap != null && _backgroundBitmapContainer.contains(_backgroundImageBitmap)) {
					_backgroundBitmapContainer.removeChild(_backgroundImageBitmap);
					_backgroundImageBitmap = null;
				}
				_backgroundContainer.removeChild(_backgroundBitmapContainer);
				doClearance();
				trace("_backgroundContainer.removeChild");
			}
		}
		private function initBackgroundImage():void {
			_backgroundImageLoaded = false;
			if (_backgroundBitmapContainer != null && _backgroundContainer.contains(_backgroundBitmapContainer)) {
				if (_backgroundBitmapContainer.contains(_backgroundImageBitmap)) {
					_backgroundBitmapContainer.removeChild(_backgroundImageBitmap);
					_backgroundImageBitmap = null;
				}
				_backgroundContainer.removeChild(_backgroundBitmapContainer);
				doClearance();
				Debug.trace("_backgroundContainer.removeChild");
			}
			_backgroundBitmapContainer = new Sprite();
			_backgroundContainer.addChild(_backgroundBitmapContainer);
			MediaLoader.loadImage(_page.backgroundImage, backgroundImageLoadCompletehandle);
		}
		private function backgroundImageLoadCompletehandle(_event:Event):void {
			Debug.trace("background image loaded.");
			var _backgroundImageLoader:Loader = Loader(_event.target.loader);
			_backgroundImageBitmap = Bitmap(_backgroundImageLoader.content);
			_backgroundImageBitmap.smoothing = true;
			_backgroundBitmapContainer.addChild(_backgroundImageBitmap);
			var _scale:Number;
			if ((_stageWidth / _stageHeight) > (_backgroundImageBitmap.width / _backgroundImageBitmap.height)) {
				//_scale = _stageHeight / _backgroundImageBitmap.height;
				_scale = _stageWidth / _backgroundImageBitmap.width;
				_backgroundBitmapContainer.scaleX = _scale;
				_backgroundBitmapContainer.scaleY = _scale;
			}else {
				//_scale = _stageWidth / _backgroundImageBitmap.width;
				_scale = _stageHeight / _backgroundImageBitmap.height;
				_backgroundBitmapContainer.scaleX = _scale;
				_backgroundBitmapContainer.scaleY = _scale;
			}
			//_backgroundImageFillShape.width = _stageWidth;
			//_backgroundImageFillShape.height = _stageHeight;
			_backgroundBitmapContainer.x = (_stageWidth - _backgroundBitmapContainer.width) / 2;
			_backgroundBitmapContainer.y = (_stageHeight - _backgroundBitmapContainer.height) / 2;
			_backgroundImageLoaded = true;
			if (_coverShadowXMotionFinished && (_page.backgroundImage.length > 0)) {
				showBackgroundImage();
			}
		}

		private function initPresenterImage():void {
			_presenterImageLoaded = false;
			if (_presenterImageBitmapContainer != null && _presenterImageContainer.contains(_presenterImageBitmapContainer)) {
				_presenterImageContainer.removeChild(_presenterImageBitmapContainer);
				doClearance();
				Debug.trace("_presenterImageContainer.removeChild");
			}
			_presenterImageBitmapContainer = new Sprite();
			_presenterImageContainer.addChild(_presenterImageBitmapContainer);
			MediaLoader.loadImage(_page.presenterImage, presenterImageLoadCompleteHandler);
			_presenterImageContainer.x = presenterImageX;
			_presenterImageContainer.y = presenterImageY;
			if (_page.pageSequence.CoverShadowVisible == "1") {
				if (_page.pageSequence.CoverShadowColor != 0) {
					var cTint:Color=new Color();
					cTint.setTint(_page.pageSequence.CoverShadowColor,1);
					_coverShadow.transform.colorTransform=cTint;
				}				
			}	
			_coverShadow.y = coverShadowY;
		}
		private function presenterImageLoadCompleteHandler(_event:Event):void {
			trace("presenter image loaded.");
			var _presenterImageLoader:Loader = Loader(_event.target.loader);
			//var _presenterImageBitmap:Bitmap = Bitmap(_presenterImageLoader.content);
			_presenterImageBitmap = Bitmap(_presenterImageLoader.content);
			_presenterImageBitmap.smoothing = true;
			var tempHeight = _page.presenterImageMode == "Big"? _presenterImageBigHeight :_presenterImageHeight;
			if (_presenterImageBitmap.height != tempHeight) {
				var _scale:Number = tempHeight / _presenterImageBitmap.height;
				_presenterImageBitmap.scaleX = _scale;
				_presenterImageBitmap.scaleY = _scale;
			}
			//_presenterImageBitmap.x = (_presenterImageWidth - _presenterImageBitmap.width) / 2;
			_presenterImageBitmap.x = presenterImageBitmapX;
			_presenterImageBitmapContainer.addChild(_presenterImageBitmap);
			_presenterImageLoaded = true;
			if (_coverShadowXMotionFinished && (_page.presenterImage.length > 0)) {
				showPresenterImage();
			}
		}
		private function showTextBoxContent():void {
			_textBoxScrollPane.alpha = 1;
			_textBoxScrollPane.mouseChildren = true;
			layout();
		}
		private function hideTextBoxContent():void {
			_textBoxScrollPane.alpha = 0;
			_textBoxScrollPane.mouseChildren = false;
		}
		private function showNavigationBarContent():void {
			_navigationBarContainer.alpha = 1;
			_navigationBarContainer.mouseChildren = true;
			layout();
		}
		private function hideNavigationBarContent():void {
			_navigationBarContainer.alpha = 0;
			_navigationBarContainer.mouseChildren = false;
		}
		private function showBackgroundImage():void {
			_backgroundImageContainerTweenAlpha = new Tween(_backgroundContainer, "alpha", null, 0, 1, 10, false);
		}
		private function showPresenterImage():void {
			_presenterImageContainerTweenAlpha = new Tween(_presenterImageContainer, "alpha", null, 0, 1, 10, false);
		}
		private function hideBackgroundImage():void {
			_backgroundImageContainerTweenAlpha = new Tween(_backgroundContainer, "alpha", null, _backgroundContainer.alpha, 0, 10, false);
			_backgroundImageContainerTweenAlpha.addEventListener(TweenEvent.MOTION_FINISH, backgroundImageHideMotionFinishHandler);
		}
		private function hidePresenterImage():void {
			_presenterImageContainerTweenAlpha = new Tween(_presenterImageContainer, "alpha", null, _presenterImageContainer.alpha, 0, 10, false);
			_presenterImageContainerTweenAlpha.addEventListener(TweenEvent.MOTION_FINISH, presenterImageHideMotionFinishHandler);
		}
		private function backgroundImageHideMotionFinishHandler(_event:TweenEvent):void {
			_backgroundImageContainerTweenAlpha.removeEventListener(TweenEvent.MOTION_FINISH, backgroundImageHideMotionFinishHandler);
			_coverShadowTweenAlpha = new Tween(_coverShadow, "alpha", null, _coverShadow.alpha, 0, 10, false);
		}
		private function presenterImageHideMotionFinishHandler(_event:TweenEvent):void {
			_presenterImageContainerTweenAlpha.removeEventListener(TweenEvent.MOTION_FINISH, presenterImageHideMotionFinishHandler);
			_coverShadowTweenAlpha = new Tween(_coverShadow, "alpha", null, _coverShadow.alpha, 0, 10, false);
		}
		//
		//
		// Enter room transition
		public function enterRoom():void {			
			if (_page.type != "Push pictures") {
				initNavigationBarContent();
			}
			initTextBoxContent();
			if (_page.backgroundImage.length > 0) {
				initBackgroundImage();
			}
			if (_page.presenterImage.length > 0) {
				_coverShadowXMotionFinished = false;
				initPresenterImage();
				presenterImageEnterRoom();
			} else {
				_coverShadowXMotionFinished = true;
				textBoxEnterRoom();
			}
		}
		// TextBox enter room animation
		private function textBoxEnterRoom():void {
			_textBoxBackground.alpha = 1;
			_textBoxBackground.height = 60;
			_textBoxBackground.y = textBoxCenterY - _textBoxBackground.height / 2;
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeOut, textBoxEnterRoomXStart, textBoxX, 12, false);
			var tempTextWidth = _page.singleMode || _page.hasVideo?_textBoxSingleWidth:_textBoxWidth;
			_textBoxBackgroundTweenWidth = new Tween(_textBoxBackground, "width", Regular.easeOut, 34, tempTextWidth, 12, false);
			var tempTextAlpha = _page.hideTextMode?0:1;
			_textBoxBackGroundTweenAlpha = new Tween(_textBoxBackground, "alpha", Regular.easeOut, 0, tempTextAlpha, 12, false);
			_textBoxBackgroundTweenWidth.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundEnterRoomMotionPartOneFinishHandler);
		}
		private function textBoxBackgroundEnterRoomMotionPartOneFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenWidth.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundEnterRoomMotionPartOneFinishHandler);
			var _targetY:Number = textBoxCenterY - textBoxActualHeight / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeOut, _textBoxBackground.y, _targetY, 17, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeOut, _textBoxBackground.height, textBoxActualHeight, 17, false);
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundEnterRoomMotionFinishHandler);

			if (_page.type != "Push pictures") {
				navigationBarEnterRoom();
			}
		}
		private function textBoxBackgroundEnterRoomMotionFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundEnterRoomMotionFinishHandler);
			showTextBoxContent();
			_textBoxBackground.height = textBoxScrollPaneHeight;
			if (_page.type != "Push pictures") {
				showNavigationBarContent();
			}
		}
		// Navigation bar enter room animation
		private function navigationBarEnterRoom():void {
			_navigationBar.alpha = 1;
			var _navigationBarTargetX:Number = (_navigationBarWidth - 330) / 2 + navigationBarX;
			var _navigationBarTargetY:Number = (_navigationBarHeight - 55) / 2 + navigationBarY;
			_navigationBarTweenX = new Tween(_navigationBar, "x", Regular.easeOut, navigationBarEnterRoomXStart, _navigationBarTargetX, 10, false);
			_navigationBarTweenY = new Tween(_navigationBar, "y", Regular.easeOut, navigationBarEnterRoomYStart, _navigationBarTargetY + 10, 10, false);
			_navigationBarTweenWidth = new Tween(_navigationBar, "width", Regular.easeOut, 30, 330, 10, false);
			_navigationBarTweenHeight = new Tween(_navigationBar, "height", Regular.easeOut, 50, 55, 10, false);
			_navigationBarTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, navigationBarEnterRoomMotionPartOneFinishHandler);
		}
		private function navigationBarEnterRoomMotionPartOneFinishHandler(_event:TweenEvent):void {
			_navigationBarTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, navigationBarEnterRoomMotionPartOneFinishHandler);
			_navigationBarTweenX = new Tween(_navigationBar, "x", Regular.easeOut, _navigationBar.x, navigationBarX, 10, false);
			_navigationBarTweenY = new Tween(_navigationBar, "y", Regular.easeOut, _navigationBar.y, navigationBarY, 10, false);
			_navigationBarTweenWidth = new Tween(_navigationBar, "width", Regular.easeOut, _navigationBar.width, _navigationBarWidth, 10, false);
			_navigationBarTweenHeight = new Tween(_navigationBar, "height", Regular.easeOut, _navigationBar.height, _navigationBarHeight, 10, false);
		}
		// Presenter image enter room animation
		private function presenterImageEnterRoom():void {
			_coverShadow.alpha = 1;
			if (_page.pageSequence.CoverShadowVisible == '0')
			{
				_coverShadow.alpha = 0;
			}			
			if (_page.presenterImagePosition == "Right") {
				_coverShadow.scaleX = -1;
			} else {
				_coverShadow.scaleX = 1;
			}
			_coverShadowTweenX = new Tween(_coverShadow, "x", Regular.easeOut, coverShadowEnterRoomXStart, coverShadowX, 10, false);
			_coverShadowTweenX.addEventListener(TweenEvent.MOTION_FINISH, coverShadowXMotionFinishHandler);
		}
		private function coverShadowXMotionFinishHandler(_event:TweenEvent):void {
			_coverShadowTweenX.removeEventListener(TweenEvent.MOTION_FINISH, coverShadowXMotionFinishHandler);
			_coverShadowXMotionFinished = true;
			if (_presenterImageLoaded) {
				showPresenterImage();
			}
			if (_backgroundImageLoaded) {
				showBackgroundImage();
			}
			textBoxEnterRoom();
		}
		//
		//
		// Exit room transition
		public function exitRoom():void {
			textBoxExitRoom();
			navigationBarExitRoom();
			hidePresenterImage();
			hideBackgroundImage();
		}
		// Text box exit room animation
		private function textBoxExitRoom():void {
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", null, _textBoxBackground.x, _textBoxBackground.x, 17, false);
			_textBoxBackgroundTweenX.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomBeforeHideMotionFinishHandler);
		}
		private function textBoxBackgroundExitRoomBeforeHideMotionFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenX.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomBeforeHideMotionFinishHandler);
			hideTextBoxContent();
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", null, _textBoxBackground.x, _textBoxBackground.x, 3, false);
			_textBoxBackgroundTweenX.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomBeforeMotionFinishHandler);
		}
		private function textBoxBackgroundExitRoomBeforeMotionFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenX.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomBeforeMotionFinishHandler);
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeIn, _textBoxBackground.x, _textBoxBackground.x + 50, 17, false);
			var _targetY:Number = textBoxCenterY - 145 / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 17, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, 145, 17, false);
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionPartOneFinishHandler);
			Debug.trace("********");
		}
		private function textBoxBackgroundExitRoomMotionPartOneFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionPartOneFinishHandler);
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeOut, _textBoxBackground.x, _textBoxBackground.x + 210, 7, false);
			var _targetY:Number = textBoxCenterY - 75 / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeOut, _textBoxBackground.y, _targetY, 7, false);
			_textBoxBackgroundTweenWidth = new Tween(_textBoxBackground, "width", Regular.easeOut, _textBoxBackground.width, _textBoxBackground.width - 50, 7, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeOut, _textBoxBackground.height, 75, 7, false);
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionPartTwoFinishHandler);
			Debug.trace("=====================");
		}
		private function textBoxBackgroundExitRoomMotionPartTwoFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionPartTwoFinishHandler);
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeIn, _textBoxBackground.x, _stageWidth, 12, false);
			_textBoxBackgroundTweenWidth = new Tween(_textBoxBackground, "width", Regular.easeIn, _textBoxBackground.width, 17, 12, false);
			_textBoxBackgroundTweenWidth.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionFinishHandler);
		}
		private function textBoxBackgroundExitRoomMotionFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenWidth.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundExitRoomMotionFinishHandler);
			_textBoxBackground.alpha = 0;
		}
		// Navigation bar exit room animation
		private function navigationBarExitRoom():void {
			hideNavigationBarContent();
			_navigationBarTweenX = new Tween(_navigationBar, "x", null, _navigationBar.x, _navigationBar.x, 10, false);
			_navigationBarTweenX.addEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomBeforeMotionFinishHandler);
		}
		private function navigationBarExitRoomBeforeMotionFinishHandler(_event:TweenEvent):void {
			_navigationBarTweenX.removeEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomBeforeMotionFinishHandler);
			_navigationBarTweenX = new Tween(_navigationBar, "x", Regular.easeOut, _navigationBar.x, _navigationBar.x + _navigationBar.width / 2, 20, false);
			_navigationBarTweenY = new Tween(_navigationBar, "y", Regular.easeOut, _navigationBar.y, _navigationBar.y + (_navigationBarHeight - 53) / 2, 20, false);
			_navigationBarTweenWidth = new Tween(_navigationBar, "width", Regular.easeOut, _navigationBar.width, _navigationBar.width - 40, 20, false);
			_navigationBarTweenHeight = new Tween(_navigationBar, "height", Regular.easeOut, _navigationBar.height, 53, 20, false);
			_navigationBarTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomMotionPartOneFinishHandler);
		}
		private function navigationBarExitRoomMotionPartOneFinishHandler(_event:TweenEvent):void {
			_navigationBarTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomMotionPartOneFinishHandler);
			_navigationBarTweenX = new Tween(_navigationBar, "x", Regular.easeIn, _navigationBar.x, _stageWidth, 10, false);
			_navigationBarTweenWidth = new Tween(_navigationBar, "width", Regular.easeIn, _navigationBar.width, 30, 10, false);
			_navigationBarTweenWidth.addEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomMotionFinishHandler);
		}
		private function navigationBarExitRoomMotionFinishHandler(_event:TweenEvent):void {
			_navigationBarTweenWidth.removeEventListener(TweenEvent.MOTION_FINISH, navigationBarExitRoomMotionFinishHandler);
			_navigationBar.alpha = 0;
		}
		//
		//
		// Page transition animation
		public function switchPage():void {
			hideTextBoxContent();
			hideNavigationBarContent();
			if (_page.type != "Push pictures") {
				initNavigationBarContent();
			}
			initTextBoxContent();
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeIn, _textBoxBackground.x, _textBoxBackground.x, 10, false);
			_textBoxBackgroundTweenX.addEventListener(TweenEvent.MOTION_FINISH, textBoxSwitchPageBeforeMotionHandler);
		}
		private function textBoxSwitchPageBeforeMotionHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenX.removeEventListener(TweenEvent.MOTION_FINISH, textBoxSwitchPageBeforeMotionHandler);
			textBoxSwitchPage();
			navigationBarSwitchPage();
			presenterImageSwitchPage();
			backgroundImageSwitchPage();
		}
		// Text box switch page animation
		private function textBoxSwitchPage():void {
			var _heightDiff:Number = (_textBoxBackground.height > textBoxActualHeight) ? -15 : 15;
			var _widthDiff:Number = (_textBoxBackground.width > textBoxActualWidth) ? -15 : 15;
			var tempTextAlpha = _page.hideTextMode?0:1;
			var _alphaDiff:Number = (_textBoxBackground.alpha > tempTextAlpha) ? -0.3 : 0.3;			
			
			var _targetY:Number = textBoxCenterY - (textBoxActualHeight + _heightDiff) / 2;
			_textBoxBackgroundTweenX = new Tween(_textBoxBackground, "x", Regular.easeIn, _textBoxBackground.x, textBoxX, 12, false);
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 10, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, (textBoxActualHeight + _heightDiff), 10, false);
			_textBoxBackgroundTweenWidth = new Tween(_textBoxBackground, "width", Regular.easeIn, _textBoxBackground.width, (textBoxActualWidth + _widthDiff), 10, false);
			
			_textBoxBackGroundTweenAlpha = new Tween(_textBoxBackground, "alpha", Regular.easeOut, _textBoxBackground.alpha, (tempTextAlpha + _alphaDiff), 12, false);
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundSwitchPageMotionPartOneFinishHandler);
			Debug.trace("--------------------");
		}
		private function textBoxBackgroundSwitchPageMotionPartOneFinishHandler(_event:TweenEvent):void {
			var tempTextAlpha = _page.hideTextMode?0:1;
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundSwitchPageMotionPartOneFinishHandler);
			var _targetY:Number = textBoxCenterY - textBoxActualHeight / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 2, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, textBoxActualHeight, 2, false);
			_textBoxBackgroundTweenWidth = new Tween(_textBoxBackground, "width", Regular.easeIn, _textBoxBackground.width, textBoxActualWidth, 2, false);
			_textBoxBackGroundTweenAlpha = new Tween(_textBoxBackground, "alpha", Regular.easeIn, _textBoxBackground.alpha, tempTextAlpha, 2, false);
	
			
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundSwitchPageMotionFinishHandler);
			Debug.trace("&&&&&&&&&&&&&&&&&&&&&&&&&");
		}
		private function textBoxBackgroundSwitchPageMotionFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundSwitchPageMotionFinishHandler);
			showTextBoxContent();
			if (_page.type != "Push pictures") {
				showNavigationBarContent();
			}
		}
		// Navigation bar switch page animation
		private function navigationBarSwitchPage():void {
			_navigationBarTweenX = new Tween(_navigationBar, "x", Regular.easeIn, _navigationBar.x, navigationBarX, 12, false);
			if (_page.type != "Push pictures") {
				_navigationBar.width = _navigationBarWidth;
				_navigationBar.height = _navigationBarHeight;
				_navigationBarTweenAlpha = new Tween(_navigationBar, "alpha", Regular.easeIn, _navigationBar.alpha, 1, 12, false);
			} else {
				_navigationBarTweenAlpha = new Tween(_navigationBar, "alpha", Regular.easeIn, _navigationBar.alpha, 0, 12, false);
			}
		}
		// background image switch page animation
		private function backgroundImageSwitchPage():void {
			var _targetAlpha:Number = _backgroundContainer.alpha == 1 ? 0.1 : 0;
			_backgroundImageContainerTweenAlpha = new Tween(_backgroundContainer, "alpha", null, _backgroundContainer.alpha, _targetAlpha, 5, false);
			_backgroundImageContainerTweenAlpha.addEventListener(TweenEvent.MOTION_FINISH, backgroundImageSwitchPageHideMotionFinishHandler);
		}
		
		// Presenter image switch page animation
		private function presenterImageSwitchPage():void {
			var _targetAlpha:Number = _presenterImageContainer.alpha == 1 ? 0.1 : 0;
			_presenterImageContainerTweenAlpha = new Tween(_presenterImageContainer, "alpha", null, _presenterImageContainer.alpha, _targetAlpha, 5, false);
			_presenterImageContainerTweenAlpha.addEventListener(TweenEvent.MOTION_FINISH, presenterImageSwitchPageHideMotionFinishHandler);
			if (_page.presenterImage.length == 0) {
				_coverShadowTweenAlpha = new Tween(_coverShadow, "alpha", null, _coverShadow.alpha, 0, 5, false);
			}
		}
		
		private function backgroundImageSwitchPageHideMotionFinishHandler(_event:TweenEvent):void {
			_backgroundImageContainerTweenAlpha.removeEventListener(TweenEvent.MOTION_FINISH, backgroundImageSwitchPageHideMotionFinishHandler);
			_backgroundContainer.alpha = 0;
			if (_page.backgroundImage.length > 0) {
				initBackgroundImage();
			}else {
				removeBackground();
			}
		}
		private function presenterImageSwitchPageHideMotionFinishHandler(_event:TweenEvent):void {
			_presenterImageContainerTweenAlpha.removeEventListener(TweenEvent.MOTION_FINISH, presenterImageSwitchPageHideMotionFinishHandler);
			_presenterImageContainer.alpha = 0;
			if (_page.presenterImage.length > 0) {
				_presenterImageContainer.x = presenterImageX;
				_presenterImageContainer.y = presenterImageY;
				_coverShadow.x = coverShadowX;
				_coverShadow.y = coverShadowY;
				if (_page.presenterImagePosition == "Right") {
					_coverShadow.scaleX = -1;
				} else {
					_coverShadow.scaleX = 1;
				}
				_coverShadowTweenAlpha = new Tween(_coverShadow, "alpha", null, _coverShadow.alpha, 1, 5, false);
				initPresenterImage();
			}
		}
		public function layout():void {
			if (_page == null) {
				return;
			}
			if (_page.backgroundImage.length > 0 && _backgroundImageLoaded) {
				if (_backgroundContainer != null && contains(_backgroundContainer)) {
					var _scale:Number;
					/*
					if (_stageWidth / _stageHeight > _backgroundImageBitmap.width / _backgroundImageBitmap.height) {
						_scale = _stageHeight / _backgroundImageBitmap.height;
						_backgroundBitmapContainer.scaleX = _scale;
						_backgroundBitmapContainer.scaleY = _scale;
					}else {
						_scale = _stageWidth / _backgroundImageBitmap.width;
						_backgroundBitmapContainer.scaleX = _scale;
						_backgroundBitmapContainer.scaleY = _scale;
					}
					*/
					if ((_stageWidth / _stageHeight) > (_backgroundImageBitmap.width / _backgroundImageBitmap.height)) {
						//_scale = _stageHeight / _backgroundImageBitmap.height;
						_scale = _stageWidth / _backgroundImageBitmap.width;
						_backgroundBitmapContainer.scaleX = _scale;
						_backgroundBitmapContainer.scaleY = _scale;
					}else {
						//_scale = _stageWidth / _backgroundImageBitmap.width;
						_scale = _stageHeight / _backgroundImageBitmap.height;
						_backgroundBitmapContainer.scaleX = _scale;
						_backgroundBitmapContainer.scaleY = _scale;
					}
					
					_backgroundBitmapContainer.x = (_stageWidth - _backgroundBitmapContainer.width) / 2;
					_backgroundBitmapContainer.y = (_stageHeight - _backgroundBitmapContainer.height) / 2;
				}
			}
			if (_page.presenterImage.length > 0) {
				if (_presenterImageContainer != null && contains(_presenterImageContainer)) {
					_presenterImageContainer.x = presenterImageX;
					_presenterImageContainer.y = presenterImageY;
				}
				if (_coverShadow != null && contains(_coverShadow) && _page.pageSequence.CoverShadowVisible == '1') {
					_coverShadow.x = coverShadowX;
					_coverShadow.y = coverShadowY;
				}
				else if (_page.pageSequence.CoverShadowVisible == '0')
				{
					_coverShadow.x = 0;
					_coverShadow.y = 0;
					_coverShadow.alpha = 0;
				}
			}
			if (_textBoxBackground != null && contains(_textBoxBackground)) {
				_textBoxBackground.x = textBoxX;
				if (_page.hasVideo)
				{
					_textBoxBackground.y = textBoxCenterY - _textBoxBackground.height / 2 + 40;
				}
				else
				{
					_textBoxBackground.y = textBoxCenterY - _textBoxBackground.height / 2;
				}
			}
			if (_textBoxScrollPane != null && contains(_textBoxScrollPane) && _textBoxBackground != null) {
				_textBoxScrollPane.x = textBoxX;
				if (_page.hasVideo)
				{
					_textBoxScrollPane.y = textBoxCenterY - textBoxActualHeight / 2 + 40;
				}
				else
				{
					_textBoxScrollPane.y = textBoxCenterY - textBoxActualHeight / 2;
					if (_page.type == "Screening results")
					_textBoxScrollPane.y += 10;
				}
			}
			if (_navigationBar != null && contains(_navigationBar)) {
				_navigationBar.x = navigationBarX;
				_navigationBar.y = navigationBarY;
				if (_navigationBarContainer != null && contains(_navigationBarContainer)) {
					_navigationBarContainer.x = navigationBarX;
					_navigationBarContainer.y = navigationBarY;
				}
			}
		}
		
		private function arrangePageHandler(_event:PageEvent):void {
			Debug.trace("----------------arrangePageHandler------------");
			var _heightDiff:Number = (_textBoxBackground.height > textBoxActualHeight) ? -15 : 15;
			var _targetY:Number = textBoxCenterY - (textBoxActualHeight + _heightDiff) / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 10, false);
			_textBoxScrollPaneTweenY = new Tween(_textBoxScrollPane, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 10, false);
			_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, (textBoxActualHeight + _heightDiff), 10, false);
			_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionPartOneFinishHandler);
			Debug.trace("^^^^^^^^^^^^^^^^^^^^^^^");
		}
		private function textBoxBackgroundArrangeMotionPartOneFinishHandler(_event:TweenEvent):void {
			_textBoxBackgroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionPartOneFinishHandler);
			var _targetY:Number = textBoxCenterY - textBoxActualHeight / 2;
			_textBoxBackgroundTweenY = new Tween(_textBoxBackground, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 2, false);
			_textBoxScrollPaneTweenY = new Tween(_textBoxScrollPane, "y", Regular.easeIn, _textBoxBackground.y, _targetY, 2, false);
			//_textBoxBackgroundTweenHeight = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, textBoxActualHeight, 2, false);
			//_textBoxBackgroundTweenHeight.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionFinishHandler);
			var tempTween:Tween = new Tween(_textBoxBackground, "height", Regular.easeIn, _textBoxBackground.height, textBoxActualHeight, 2, false);
			tempTween.addEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionFinishHandler);
			Debug.trace("############################");
		}
		private function textBoxBackgroundArrangeMotionFinishHandler(_event:TweenEvent):void {
			//_textBoxBackroundTweenHeight.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionFinishHandler);
			_event.currentTarget.removeEventListener(TweenEvent.MOTION_FINISH, textBoxBackgroundArrangeMotionFinishHandler);
			_textBoxScrollPane.height = textBoxScrollPaneHeight;
			_textBoxScrollPane.update();
			_textBoxScrollPaneSourceContainer.arrangeFeedback();
		}
		private function primaryButtonClickedHandler(_event:MouseEvent):void {
			_textBoxScrollPaneSourceContainer.primaryButtonClickedHandler(_event);
		}
		private function secondaryButtonClickedHandler(_event:MouseEvent):void {
			_textBoxScrollPaneSourceContainer.secondaryButtonClickedHandler(_event);
		}
		private function nextPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("nextpage", null));
		}
		private function previousPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("previouspage", null));
		}
		private function infoPageHandler(_event:PageEvent):void {
			this.dispatchEvent(_event);
		}
		private function submitHandler(_event:SubmitEvent):void {
			this.dispatchEvent(new SubmitEvent("submit", _event.xml, _event.isFeedback));
		}		
		private function pageStartLoadingHandler(_event:PageEvent):void {
			this.dispatchEvent(_event);
		}
		private function pageStopLoadingHandler(_event:PageEvent):void {
			this.dispatchEvent(_event);
		}
		private function setButtonHandler(_event:PageEvent):void {
			if(_event.data.label != null) {
				PrimaryButton(_setableButton).label = _event.data.label;
			}
			if(_event.data.enabled != null) {
				PrimaryButton(_setableButton).enabled = _event.data.enabled;
			}
		}
		private function loginCompleteHandler(_event:LoginEvent):void {
			this.dispatchEvent(_event);
		}
	}
}