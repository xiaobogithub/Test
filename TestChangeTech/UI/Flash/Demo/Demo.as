package {
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.filters.*;
	import flash.text.TextField;
	import com.ethos.changetech.business.*;
	import com.ethos.changetech.controls.*;
	import com.ethos.changetech.debug.*;
	import com.ethos.changetech.models.*;

	public class Demo extends Sprite {

		private var _login:LoginControl;
		private var _debug:Debug;
		private var _xmlLoader:XmlLoader;
		private var _session:Session;
		private var _pageSequenceOrderNo:int = 1;
		private var _pageSequenceContainer:PageSequenceContainer;

		public function Demo() {
			setupDebug();
			setupLogin();
			setupEvents();
		}
		public function get session():Session {
			return _session;
		}
		public function set session(_value:Session):void {
			if (_value == _session) {
				return;
			}
			_session = _value;
		}
		private function setupDebug():void {
			_debug = new Debug();
			_debug.visible = false;
			addChild(_debug);
		}
		private function setupLogin():void {
			_login = new LoginControl();
			_login.x = (this.width - _login.width)/2;
			_login.y = (this.height - _login.height)/2;
			_login.addEventListener(LoginEvent.PASSED, loginPassed);
			addChild(_login);
		}
		private function setupEvents():void {
			stage.addEventListener(Event.RESIZE, resizeHandler);
		}
		private function loginPassed(_event:LoginEvent):void {
			trace("login passed");
			_login.removeEventListener(LoginEvent.PASSED, loginPassed);
			removeChild(_login);
			_xmlLoader = new XmlLoader("Welcome.xml");
			_xmlLoader.showProgressBar = true;
			_xmlLoader.addEventListener(XmlLoaderEvent.COMPELETED, xmlLoadCompeleted);
			_xmlLoader.load();
			_xmlLoader.x = (this.width - 300)/2;
			_xmlLoader.y = (this.height - 10)/2;
			addChild(_xmlLoader);
		}
		private function resizeHandler(_event:Event):void {
			//trace("resizeHandler: " + _event);
			//trace("stageWidth: " + stage.stageWidth + " stageHeight: " + stage.stageHeight);
		}
		private function xmlLoadCompeleted(_event:XmlLoaderEvent) {
			trace("data loaded");
			removeChild(_xmlLoader);
			_xmlLoader.removeEventListener(XmlLoaderEvent.COMPELETED, xmlLoadCompeleted);
			_session = new Session();
			_session.fromXML(XML(_event.data));
			updatePageSequence();
		}
		private function updatePageSequence():void {
			_pageSequenceContainer = new PageSequenceContainer();
			_pageSequenceContainer.pageSequence = _session.sequences[_pageSequenceOrderNo-1];
			_pageSequenceContainer.addEventListener(PageEvent.CATEGORY, showCategoryDescriptionHandler);
			_pageSequenceContainer.addEventListener(PageEvent.NEXTSEQUENCE, nextSequenceHandler);
			_pageSequenceContainer.addEventListener(PageEvent.PREVIOUSSEQUENCE, previousSequenceHandler);
			addChild(_pageSequenceContainer);
		}
		private function showCategoryDescriptionHandler(_event:PageEvent):void {
			trace("show category");
			var _categoryPanel:CategoryPanel = new CategoryPanel();
			_categoryPanel.title = _session.sequences[_pageSequenceOrderNo-1].predictorCategoryName;
			_categoryPanel.description = _session.sequences[_pageSequenceOrderNo-1].description;
			_categoryPanel.x = (800 - _categoryPanel.width)/2;
			_categoryPanel.y = (600 - _categoryPanel.height)/2;
			_categoryPanel.addEventListener(PageEvent.CATEGORY, hideCategoryDescriptionHandler);
			var _blurFilter:BlurFilter = new BlurFilter(5, 5, BitmapFilterQuality.HIGH);
			_pageSequenceContainer.filters = new Array(_blurFilter);
			_pageSequenceContainer.mouseEnabled = false;
			_pageSequenceContainer.mouseChildren = false;
			addChild(_categoryPanel);
		}
		private function hideCategoryDescriptionHandler(_event:PageEvent):void {
			_pageSequenceContainer.filters = null;
			_pageSequenceContainer.mouseEnabled = true;
			_pageSequenceContainer.mouseChildren = true;
			removeChild((CategoryPanel)(_event.target));
		}
		private function nextSequenceHandler(_event:PageEvent):void {
			trace("Page sequence "+(_pageSequenceOrderNo+1).toString());
			if (_pageSequenceOrderNo<_session.sequences.length) {
				_pageSequenceContainer.removeEventListener(PageEvent.CATEGORY, showCategoryDescriptionHandler);
				_pageSequenceContainer.removeEventListener(PageEvent.NEXTSEQUENCE, nextSequenceHandler);
				_pageSequenceContainer.removeEventListener(PageEvent.PREVIOUSSEQUENCE, previousSequenceHandler);
				removeChild(_pageSequenceContainer);
				_pageSequenceOrderNo++;
				updatePageSequence();
			} else {
				trace("End");
			}
		}
		private function previousSequenceHandler(_event:PageEvent):void {
			if (_pageSequenceOrderNo>1) {
				_pageSequenceContainer.removeEventListener(PageEvent.CATEGORY, showCategoryDescriptionHandler);
				_pageSequenceContainer.removeEventListener(PageEvent.NEXTSEQUENCE, nextSequenceHandler);
				_pageSequenceContainer.removeEventListener(PageEvent.PREVIOUSSEQUENCE, previousSequenceHandler);
				removeChild(_pageSequenceContainer);
				_pageSequenceOrderNo--;
				updatePageSequence();
			} else {
				trace("beginning");
			}
		}
	}
}