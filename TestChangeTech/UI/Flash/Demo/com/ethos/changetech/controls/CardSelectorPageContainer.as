package com.ethos.changetech.controls{
	//import fl.controls.*;
	//import flash.display.*;
	import flash.errors.IllegalOperationError;
	//import flash.events.*;
	//import flash.text.*;
	import com.ethos.changetech.models.*;

	public class CardSelectorPageContainer extends PlainPageContainer {

		private var _cardSelectors:Array;
		private var _selectedCard:CardSelector;
		private var _cardSliders:Array;

		public function CardSelectorPageContainer() {
			super();
			_cardSelectors = new Array();
			_cardSliders = new Array();
		}
		override public function set page(_value:Page):void {
			if (_value == super.page) {
				return;
			}
			super.page = _value;
			createCardSelectors();
			createCardSliders();
			updateView();
		}
		private function createCardSelectors():void {
			if (page.questions.length!= 1) {
				throw new IllegalOperationError("Not support more than one cardSelector question - at page sequence No." + page.pageSequence.pageSequenceOrderNo.toString() + " page No." + page.pageOrderNo.toString());
			}
			var _cardNames:Array = page.questions[0].answerAlternatives.split(';');
			trace("-------------------------------");
			trace("cards:");
			for (var _i:int = 0; _i<_cardNames.length; _i++) {
				var _cardSelector:CardSelector = new CardSelector();
				_cardSelector.addEventListener(CardSelectorEvent.SELECTED, cardSelectedHandler);
				_cardSelector.title = _cardNames[_i];
				_cardSelector.unit = page.questions[0].unit;
				_cardSelectors.push(_cardSelector);
			}
			trace("-------------------------------");
			var _variables:Array = page.questions[0].variables.split(';');
			if (_variables.length != _cardSelectors.length) {
				throw new IllegalOperationError("The amount of variables should match the amount of cardSelectors - at page sequence No." + page.pageSequence.pageSequenceOrderNo.toString() + " page No." + page.pageOrderNo.toString());
			}
			for (_i = 0; _i<_variables.length; _i++) {
				var _cardSelectorsVariables:Array = _variables[_i].split(',');
				_cardSelectors[_i].setEnabled(_cardSelectorsVariables.pop());
				_cardSelectors[_i].variables = _cardSelectorsVariables;
				trace(_cardSelectors[_i].title + ", enable = " + _cardSelectors[_i].enabled);
				addChild(_cardSelectors[_i]);
			}
			this.x+=280;
		}
		private function createCardSliders():void {
			var _subQuestions:Array = page.questions[0].subQuestions;
			trace("sub questions amount = " + _subQuestions.length.toString());
			for (var _i:int = 0; _i<_subQuestions.length; _i++) {
				var _cardSlider:CardSlider = new CardSlider();
				var _sliderAlternatives:Array = _subQuestions[_i].answerAlternatives.split(';');
				_cardSlider.title = _sliderAlternatives.shift();
				_cardSlider.leftLabel = _sliderAlternatives.shift();
				_cardSlider.rightLabel = _sliderAlternatives.shift();
				_cardSlider.from = _sliderAlternatives.shift();
				_cardSlider.to = _sliderAlternatives.shift();
				_cardSlider.step = _sliderAlternatives.shift();
				_cardSliders.push(_cardSlider);
				addChild(_cardSlider);
			}
			if (_cardSliders[0].contentWidth<_cardSliders[0].numberContainer.width) {
				this.x += (_cardSliders[0].numberContainer.width - _cardSliders[0].contentWidth)/2;
			}
		}
		override protected function updateView():void {
			super.updateView();

			for (var _i:int = 0; _i<_cardSelectors.length; _i++) {
				_cardSelectors[_i].x = (120+15)*(_i%2) - 280;
				_cardSelectors[_i].y = (130+15)*(Math.floor(_i/2));
			}
			var _lastYPosition = bodyTextTextField.y + bodyTextTextField.height;
			for each (var _cardSlide:CardSlider in _cardSliders) {
				//TODO:
				_cardSlide.contentWidth = contentWidth;
				_cardSlide.textMargin = textMargin;
				_cardSlide.x = bodyTextTextField.x;
				_cardSlide.y =_lastYPosition  + textMargin;
				_lastYPosition += textMargin +_cardSlide.height;
			}
			bodyPanel.contentHeight = _lastYPosition - bodyTitleTextField.y;
		}
		private function cardSelectedHandler(_event:CardSelectorEvent):void {
			_selectedCard = (CardSelector)(_event.target);
			//TODO: set old selectedCard unselected.
			//TODO: connect card variables to sliders.
		}
	}
}