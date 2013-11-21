package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.Session;
	import com.ethos.changetech.utils.MenuFunctions;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import fl.controls.ComboBox;
	import fl.data.DataProvider;
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.net.URLRequest;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import flash.system.Security;
	import flash.system.LoaderContext;

	public class SessionContainer extends Sprite {

		private var _logoBitmap:Bitmap = null;
		private var _settingMenu:ComboBox;
		private var _dayPanel:DayPanel;
		private var _dayLabel:StaticTextField;
		private var _dayTextField:StaticTextField;
		private var _categoryButton:CategoryButton;
		private var _enableAlphaFilter:BlurFilter;

		private var _programRoomName:String;
		private var _day:int;

		private var _stageWidth:Number = 800;
		private var _stageHeight:Number = 600;
		private var _logoLeftMargin:Number;
		private var _logoTopMargin:Number;
		private var _logoMaxHeight:Number;
		private var _toolBarRightMargin:Number;
		private var _toolBarTopMargin:Number;
		private var _toolBarInnerMargin:Number;
		private var _settingMenuWidth:Number;
		private var _programButtonRightMargin:Number;
		private var _programButtonBottomMargin:Number;

		public function SessionContainer() {
			initLayout();
			initTools();
			initCategoryButton();
		}
		public function set logo(_value:String):void {
			if (_logoBitmap != null) {
				trace("cannot change logo!");
				return;
			}
			if (_value != null && _value.length > 0) {
				var _logoLoader:Loader = new Loader();
				_logoLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, logoLoadCompleteHandler);
				_logoLoader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, logoLoadIOErrorHandler);
				trace("media root" + String(GlobalValue.getValue("mediaRootURL")));
				//var _logoURL = String(GlobalValue.getValue("mediaRootURL")) + "logocontainer/" + _value;
				//refactor media url 
				var _logoURL = String(GlobalValue.getValue("logocontainerRoot")) + _value;
				
				var _crossdomainURL = String(GlobalValue.getValue("crossdomainURL"));
				trace("Logo URL = " + _logoURL);
				Security.loadPolicyFile(_crossdomainURL);
				var loaderContext = new LoaderContext();
				loaderContext.checkPolicyFile = true;

				_logoLoader.load(new URLRequest(_logoURL),loaderContext);
			}
		}
		
		private function logoLoadIOErrorHandler(e:IOErrorEvent):void 
		{
			trace(e);
		}
		public function setDay(_value:int, _dayText:String):void {
			_day = _value;
			if (_dayText != ""){
				_dayLabel.htmlText = "<dayLabel>"+_dayText+"</dayLabel>";
			}
			else {
				_dayLabel.htmlText = "<dayLabel>Dag</dayLabel>";
			}
			_dayTextField.htmlText = "<dayText>" + _value.toString() + "</dayText>";
			_dayLabel.x = (_dayPanel.width - _dayLabel.width) / 2;
			_dayTextField.x = (_dayPanel.width - _dayTextField.width) / 2;
			var _textHeight = _dayLabel.textHeight + _dayTextField.textHeight;
			_dayLabel.y = (_dayPanel.height - _textHeight) / 2;
			_dayTextField.y = _dayLabel.y + _dayLabel.textHeight;
			if (!contains(_dayPanel)) {
				if (GlobalValue.getValue("IsNotShowDayAndSetMenu") != "1")//DTD152 
				addChild(_dayPanel);
			}
			updateSettingMenu();
			layout();
		}
		public function set settingMenuProvider(_value:DataProvider):void {
			_settingMenu.dataProvider = _value;
		}
		public function set settingMenuTitle(_value:String):void {
			_settingMenu.prompt = _value;
		}
		public function get programRoomName():String {
			return _programRoomName;
		}
		private function initLayout():void {
			_logoLeftMargin = GlobalValue.getValue("layout")["LogoLeftMargin"];
			_logoTopMargin = GlobalValue.getValue("layout")["LogoTopMargin"];
			_logoMaxHeight = GlobalValue.getValue("layout")["LogoMaxHeight"];
			_toolBarRightMargin = GlobalValue.getValue("layout")["ToolBarRightMargin"];
			_toolBarTopMargin = GlobalValue.getValue("layout")["ToolBarTopMargin"];
			_toolBarInnerMargin = GlobalValue.getValue("layout")["ToolBarInnerMargin"];
			_settingMenuWidth = GlobalValue.getValue("layout")["SettingMenuWidth"];
			_programButtonRightMargin = GlobalValue.getValue("layout")["ProgramButtonRightMargin"];
			_programButtonBottomMargin = GlobalValue.getValue("layout")["ProgramButtonButtomMargin"];

			_enableAlphaFilter = new BlurFilter(0,0,0);
		}
		private function initTools():void {
			_settingMenu = new ComboBox();
			_settingMenu.textField.filters = [_enableAlphaFilter];			
			_settingMenu.width = _settingMenuWidth;
			_settingMenu.addEventListener(Event.CHANGE, menuChangeHandler);

			_dayPanel = new DayPanel();

			_dayLabel = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_dayLabel.styleSheet = StyleSheet(GlobalValue.getValue("css"));		
			_dayLabel.htmlText = "<dayLabel>Dag</dayLabel>";
			_dayPanel.addChild(_dayLabel);

			_dayTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_dayTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_dayPanel.addChild(_dayTextField);
		}
		private function initCategoryButton():void {
			_categoryButton = new CategoryButton();
			//DTD-1625:Remove link from program room in Flash
			//_categoryButton.addEventListener(MouseEvent.CLICK, categoryButtonClickedHandler);
		}
		public function setCategory(_categoryName:String, _categoryButtonBackgroundColor:uint):void {
			_programRoomName = _categoryName;
			_categoryButton.primaryThemeColor = _categoryButtonBackgroundColor;
			if (_programRoomName != null && _programRoomName.length > 0) {
				_categoryButton.label = "<programButtonName>" + _programRoomName + "</programButtonName>";
				if (!contains(_categoryButton)) {
					addChild(_categoryButton);
				}
			} else if ((_programRoomName == null || _programRoomName.length == 0) && contains(_categoryButton)) {
				removeChild(_categoryButton);
			}
			layout();
		}
		public function revertMenu():void {
			_settingMenu.selectedIndex = -1;
		}
		private function updateSettingMenu():void {
			if (contains(_settingMenu)) {
				_settingMenu.enabled = false;
				removeChild(_settingMenu);
			}
			//if (_day == 0 || String(GlobalValue.getValue("mode")) == "Preview" || String(GlobalValue.getValue("mode")) == "Trial") {
				//return;
			//}
			_settingMenu.enabled = true;
			if (GlobalValue.getValue("IsNotShowDayAndSetMenu") != "1")//DTD152 
			addChild(_settingMenu);
		}
		public function exitRoom():void {
			if (contains(_categoryButton)) {
				removeChild(_categoryButton);
			}
		}
		private function layout():void {
			if (_logoBitmap != null && contains(_logoBitmap)) {
				_logoBitmap.x = _logoLeftMargin;
				_logoBitmap.y = _logoTopMargin;
			}
			if (contains(_dayPanel)) {
				_dayPanel.x = _stageWidth - _toolBarRightMargin - _dayPanel.width;
				_dayPanel.y = _toolBarTopMargin;
				//DTD:1586 Hide the day panel in the type of relapse in CTPP
				_dayPanel.visible = GlobalValue.getValue("PageType") == "Normal"?true:false;
			}
			if (_dayPanel != null && contains(_settingMenu)) {
				_settingMenu.x = _dayPanel.x - _toolBarInnerMargin - _settingMenu.width;
				_settingMenu.y = _dayPanel.y + (_dayPanel.height - _settingMenu.height) / 2;
			}
			if (contains(_categoryButton)) {
				_categoryButton.x = _stageWidth - _programButtonRightMargin - _categoryButton.width;
				_categoryButton.y = _stageHeight - _programButtonBottomMargin - _categoryButton.height;
			}
			
			
		}
		private function logoLoadCompleteHandler(_event:Event):void {
			trace("Logo image loaded.");
			var _logoLoader:Loader = Loader(_event.target.loader);
			trace("*****************************************");
			trace(_logoLoader.content);
			_logoBitmap = Bitmap(_logoLoader.content);
			_logoBitmap.smoothing = true;
			if (_logoBitmap.height > _logoMaxHeight) {
				var _scale:Number = _logoMaxHeight / _logoBitmap.height;
				_logoBitmap.scaleX = _scale;
				_logoBitmap.scaleY = _scale;
			}
			addChild(_logoBitmap);
			layout();
		}
		private function categoryButtonClickedHandler(_event:MouseEvent):void {
			this.dispatchEvent(new PageEvent("category", null));
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
		private function menuChangeHandler(_event:Event):void {
			var _function:Function = MenuFunctions[_settingMenu.selectedItem.functionName];
			if(_function != null) {
				_function.apply(null, [this]);
			}
		}
	}
}