package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.models.Question;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.data.GlobalValue;
	import com.ning.display.SizableSprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;

	public class TextBoxContentGetInfoTemplate extends TextBoxContentStandardBasedTemplate {

		private var _questionContainerArray:Array;
		private var _questionsHeight:Number = 0;

		public function TextBoxContentGetInfoTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override public function set primaryThemeColor(_value:uint):void {
			super.primaryThemeColor = _value;
			for (var _i:int = 0; _i < _questionContainerArray.length; _i++) {
				_questionContainerArray[_i].primaryThemeColor = _primaryThemeColor;
			}
		}
		override protected function initContent():void {
			super.initContent();
			if(_page.footerText.length > 0 ) {
				_isFooter = true;
			}
		}
		override protected function updateInnerContent():void {
			_questionContainerArray = new Array();
			_innerContainer = new SizableSprite();
			if (_page.questions.length > 5 || _page.questions.length == 0) {
				throw new IllegalOperationError("Invalid question amount at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + " : The amount of question should not be from 1 to 5.");
			}
			for each (var _question:Question in _page.questions) {
				var _questionContainer:QuestionContainer = new QuestionContainer(_question);
				_questionContainer.addEventListener(PageEvent.ARRANGED, arrangePageHandler);
				SizableSprite(_innerContainer).addChild(_questionContainer);
				_questionContainerArray.push(_questionContainer);
			}
			addChild(_innerContainer);
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			for (var _i:int = 0; _i<_questionContainerArray.length; _i++) {
				_questionContainerArray[_i].contentWidth = _contentWidth;
			}
			questionsLayout();
			_innerContainer.x = _horizontalPadding;
			_innerContainer.y = _lastPositionY;
			_innerContainer.width = _contentWidth;
			_innerContainer.height = _questionsHeight;
			_lastPositionY = _innerContainer.y + _questionsHeight;
			trace("_lastPositionY after question = " + _lastPositionY);
			return _lastPositionY;
		}
		private function questionsLayout():void {
			var _lastPositionY:Number = 0;
			for each (var _questionContainer:QuestionContainer in _questionContainerArray) {
				_questionContainer.y = _lastPositionY + _textMargin;
				_questionsHeight = _questionContainer.y + _questionContainer.height;
				_lastPositionY = _questionsHeight;
			}
		}
		private function arrangePageHandler(_event:PageEvent):void {
			layout();
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (checkAndSubmit()) {
				nextPage();
			} else {
				_event.target.enabled = true;
			}
		}
		private function checkAndSubmit():Boolean {
			var _feedbacksXML:FeedbacksXML = new FeedbacksXML();
			for (var _i:int = 0; _i < _page.questions.length; _i++) {
				var _question:Question = _page.questions[_i];
				if (_question.guid.length == 0) {
					throw new IllegalOperationError("Invalid question guid at question[" + _question.guid + "]");
				}
				if (_question.answerString.length == 0 && _question.isRequired && String(GlobalValue.getValue("IsRetake")).toLowerCase() == "false") {
					infoPage(GlobalValue.getValue("messages")["QuestionRequired"]);
					return false;
				}
				_feedbacksXML.addFeedback(new FeedbackXMLNode(_question.guid, _question.answerString));
			}
			this.dispatchEvent(new SubmitEvent("submit", _feedbacksXML.xml));
			return true;
		}
	}
}