package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.events.*;
	import flash.text.TextField;
	import com.ethos.changetech.models.*;

	public class PageSequenceContainer extends Sprite {
		private var _pageSequence:PageSequence;
		private var _pageOrderNo = 1;
		private var _pageContainer:PageContainer;
		//private var _enabled:Boolean = true;

		public function PageSequenceContainer() {
			dayTextField.selectable = false;
			goalTestField.selectable = false;
			categoryButton.addEventListener(MouseEvent.CLICK, categoryButtonClickedHandler);
		}
		//public function get enabled():Boolean {
		//return _enable;
		//}
		//public function set enabled(_value:Boolean):void {
		//if (_value == _enabled) {
		//return;
		//}
		//_enabled = _value;
		//_pageContainer.enabled = _value
		//categoryButton.mouseMode = _value;
		//}
		public function get pageSequence():PageSequence {
			return _pageSequence;
		}
		public function set pageSequence(_value:PageSequence):void {
			if (_value == _pageSequence) {
				return;
			}
			_pageSequence = _value;
			dayTextField.text = _value.session.day.toString();
			categoryButton.label = _value.predictorCategoryName;
			updatePage();
		}
		private function categoryButtonClickedHandler(_event:MouseEvent):void {
			this.dispatchEvent(new PageEvent("category"));
		}
		private function updatePage():void {
			_pageContainer = new PageContainer();
			_pageContainer.page = _pageSequence.pages[_pageOrderNo-1];
			_pageContainer.x = (800 - _pageContainer.width)/2;
			_pageContainer.y = (600 - _pageContainer.height)/2;
			_pageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_pageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			addChild(_pageContainer);
		}
		private function nextPageHandler(_event:PageEvent):void {
			if (_pageOrderNo<_pageSequence.pages.length) {
				_pageContainer.removeEventListener(PageEvent.NEXTPAGE, nextPageHandler);
				_pageContainer.removeEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
				removeChild(_pageContainer);
				_pageOrderNo++;
				updatePage();
			} else {
				this.dispatchEvent(new PageEvent("nextsequence"));
			}
		}
		private function previousPageHandler(_event:PageEvent):void {
			if (_pageOrderNo>1) {
				_pageContainer.removeEventListener(PageEvent.NEXTPAGE, nextPageHandler);
				_pageContainer.removeEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
				removeChild(_pageContainer);
				_pageOrderNo--;
				updatePage();
			} else {
				this.dispatchEvent(new PageEvent("previoussequence"));
			}
		}
	}
}