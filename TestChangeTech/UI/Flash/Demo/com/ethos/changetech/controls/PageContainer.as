package com.ethos.changetech.controls{
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	import flash.text.TextField;
	import com.ethos.changetech.models.*;

	public class PageContainer extends Sprite {
		private var _page:Page;
		private var _type:String;

		public function get page():Page {
			return _page;
		}
		public function set page(_value:Page):void {
			if (_value == _page) {
				return;
			}
			_page = _value;
			_type = getPageType();
			createPage();
		}
		private function getPageType():String {
			// TODO: we don't have mixed question type, that means all the questions in one page should be the same type.
			if (_page.questions.length==0) {
				return "plain";
			}
			if (_page.questions[0].type=="Cardselector") {
				return "cardselector";
			}
			if (_page.questions[0].type=="Radio") {
				return "radio";
			}
			if (_page.questions[0].type=="Slider") {
				return "slider";
			} else {
				throw new IllegalOperationError("Invalid question type at page sequence No." + _page.pageSequence.pageSequenceOrderNo.toString() + " page No." + _page.pageOrderNo.toString());
			}
		}
		private function createPage():void {
			switch (_type) {
				case "plain" :
					createPlainPage();
					break;
				case "cardselector" :
					createCardSelectorPage();
					break;
				case "radio" :
					createRadioPage();
					break;
				case "slider" :
					createSliderPage();
					break;
				default :
					throw new IllegalOperationError("Invalid question type at page sequence No." + _page.pageSequence.pageSequenceOrderNo.toString() + " page No." + _page.pageOrderNo.toString());
			}
		}
		private function createPlainPage():void {
			trace("createPlainPage");
			var _plainPageContainer = new PlainPageContainer();
			_plainPageContainer.page = _page;
			//_plainPageContainer.contentPadding = 13;
			//_plainPageContainer.titleBottomMargin = 5;
			//_plainPageContainer.contentWidth = 360;
			_plainPageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_plainPageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			addChild(_plainPageContainer);
		}
		private function createCardSelectorPage():void {
			trace("createCardSelectorPage");
			var _cardSelectorPageContainer = new CardSelectorPageContainer();
			_cardSelectorPageContainer.page = _page;
			_cardSelectorPageContainer.contentWidth = 300;
			_cardSelectorPageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_cardSelectorPageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			addChild(_cardSelectorPageContainer);
		}
		private function createRadioPage():void {
			trace("createRadioPage");
			var _radioPageContainer = new RadioPageContainer();
			_radioPageContainer.page = _page;
			_radioPageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_radioPageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			addChild(_radioPageContainer);
		}
		private function createSliderPage():void {
			trace("createSliderPage");
			var _sliderPageContainer = new SliderPageContainer();
			_sliderPageContainer.page = _page;
			_sliderPageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_sliderPageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			addChild(_sliderPageContainer);
		}
		private function nextPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("nextpage"));
		}
		private function previousPageHandler(_event:PageEvent):void {
			this.dispatchEvent(new PageEvent("previouspage"));
		}
	}
}