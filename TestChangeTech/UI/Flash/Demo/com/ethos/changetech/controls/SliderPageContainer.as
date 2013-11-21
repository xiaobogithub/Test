package com.ethos.changetech.controls{
	import fl.controls.*;
	//import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	//import flash.text.*;
	import com.ethos.changetech.models.*;

	public class SliderPageContainer extends PlainPageContainer {
		private var _sliders:Array;

		public function SliderPageContainer() {
			_sliders = new Array();
		}
		override public function set page(_value:Page):void {
			if (_value == super.page) {
				return;
			}
			super.page = _value;
			createSliders();
			updateView();
		}
		private function createSliders():void {
			for (var _i:int = 0; _i<page.questions.length; _i++) {
				if (page.questions[_i].type != "Slider") {
					throw new IllegalOperationError("Invalid question type in slider page (demo version) - at page sequence No." + page.pageSequence.pageSequenceOrderNo.toString() + " page No." + page.pageOrderNo.toString());
				}
				var _slider:NumberSlider = new NumberSlider();
				var _sliderAlternatives:Array = page.questions[_i].answerAlternatives.split(';');
				_slider.title = _sliderAlternatives.shift();
				_slider.leftLabel = _sliderAlternatives.shift();
				_slider.rightLabel = _sliderAlternatives.shift();
				_slider.from = _sliderAlternatives.shift();
				_slider.to = _sliderAlternatives.shift();
				_slider.step = _sliderAlternatives.shift();
				_sliders.push(_slider);
				addChild(_slider);
			}
		}
		override protected function updateView():void {
			super.updateView();

			var _lastYPosition = bodyTextTextField.y + bodyTextTextField.height;
			for each (var _slider:NumberSlider in _sliders) {
				_slider.contentWidth = contentWidth;
				_slider.textMargin = textMargin;
				_slider.x = bodyTextTextField.x;
				_slider.y =_lastYPosition  + textMargin;
				_lastYPosition += textMargin +_slider.height;
			}
			bodyPanel.contentHeight = _lastYPosition - bodyTitleTextField.y;
		}
	}
}