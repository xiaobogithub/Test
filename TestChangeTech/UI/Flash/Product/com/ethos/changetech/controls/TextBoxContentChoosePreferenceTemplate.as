package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.PreferenceEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.models.Preference;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.data.GlobalValue;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;

	public class TextBoxContentChoosePreferenceTemplate extends TextBoxContentStandardBasedTemplate {

		private var _preferences:Array;
		private var _isAnswer:Boolean = false;
		private var _nextAnswerPosition:int = 0;

		public function TextBoxContentChoosePreferenceTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			_preferences = new Array();
			super(_targetPage, _w, _minH, _maxH);
		}
		override public function set primaryThemeColor(_value:uint):void {
			super.primaryThemeColor = _value;
			if (!_isAnswer) {
				for (var _i:int = 0; _i < _preferences.length; _i++) {
					_preferences[_i].themeColor = _primaryThemeColor;
				}
			}
		}
		override protected function updateInnerContent():void {
			if (_isAnswer) {
				return;
			}
			var _preferencesAmount = _page.preferences.length;
			if (_preferencesAmount < 1 || _preferencesAmount > 9) {
				throw new IllegalOperationError("Invalid preferences amount at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString() + " : The amount of preferences should be from 1 to 9.");
			}
			for (var _i:int = 0; _i < _preferencesAmount; _i++) {
				var _preferenceContainer = new PreferenceContainer(_page.preferences[_i]);
				trace("[REFACTORING: com.ethos.changetech.controls.PreferencePageContainer.updatePreferences()] - _preferenceContainer inherits from ColorableSprite.");
				//_preferenceContainer.themeColor = _primaryThemeColor;
				_preferenceContainer.addEventListener(PageEvent.ARRANGED, preferenceArrangedHandler);
				_preferenceContainer.addEventListener(PageEvent.INFO, preferenceInfoHandler);
				if (String(GlobalValue.getValue("IsRetake")).toLowerCase() != "true")
				{
					//_preferenceContainer.themeColor = _primaryThemeColor;
					_preferenceContainer.addEventListener(PreferenceEvent.CHOOSED, preferenceChoseHandler);
				}
				_preferences.push(_preferenceContainer);
				if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
				{
					if (String(_page.preferences[_i].pageVariable) == "1")
					{
						_preferenceContainer.isSelected = true;
					}
					//_preferenceContainer.enabled = false;
				}
				addChild(_preferenceContainer);
			}
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			if (!_isAnswer) {
				var _maxPreferenceWidth:Number;
				var _maxPreferenceHeight:Number;
				var _preferencesAmount = _page.preferences.length;
				if (_preferencesAmount < 3 ) {
					_maxPreferenceWidth = (_contentWidth - (_preferencesAmount - 1) * _textMargin) / _preferencesAmount;
					_maxPreferenceHeight = _maxHeight - _lastPositionY - 2 * _textMargin;
				} else {
					_maxPreferenceWidth = (_contentWidth - 2 * _textMargin) / 3;
					var _lineNumber:Number = Math.ceil(_preferencesAmount / 3);
					_maxPreferenceHeight = (_maxHeight - 2 * _textMargin - (_lineNumber - 1) * _textMargin) / _lineNumber;
				}
				var _startPostionY:Number = _lastPositionY + _textMargin;
				var _preferenceContainer:PreferenceContainer;
				for (var _i:int = 0; _i<_preferences.length; _i++) {
					_preferenceContainer = _preferences[_i];
					_preferenceContainer.maxWidth = _maxPreferenceWidth;
					_preferenceContainer.maxHeight = _maxPreferenceHeight;
					_preferenceContainer.x = _horizontalPadding + (_i % 3) * (_maxPreferenceWidth + _textMargin);
					_preferenceContainer.y = _startPostionY + Math.floor(_i / 3) * (_maxPreferenceHeight + _textMargin);
					_lastPositionY = Math.max(_lastPositionY, _preferenceContainer.y + _preferenceContainer.actualHeight);
				}
				_lastPositionY += _textMargin;
			}
			return _lastPositionY;
		}
		private function preferenceArrangedHandler(_event:PageEvent):void {
			layout();
		}
		private function preferenceInfoHandler(_event:PageEvent):void {
			this.dispatchEvent(_event);
		}
		private function preferenceChoseHandler(_event:PreferenceEvent):void {
			var _container:PreferenceContainer = _event.target as PreferenceContainer;
			var _number:Number = 0;
			for each (var _containerItem:PreferenceContainer in _preferences) {
				if (_containerItem.isSelected == true) {
					_number++;
				}
			}
			if (_container.isSelected) {
				_container.isSelected = !_container.isSelected;
			} else if (_number < _page.maxPreferences) {
				_container.isSelected = !_container.isSelected;
			}
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (!_isAnswer && checkAndSubmit()) {
				_isAnswer = true;
				updateAnswer(_nextAnswerPosition);
			} else if (_isAnswer) {
				updateAnswer(_nextAnswerPosition);
			} else {
				_event.target.enabled = true;
			}
		}
		private function checkAndSubmit():Boolean {
			trace("submit choose preferences.");
			var _feedbacksXML:FeedbacksXML = new FeedbacksXML();
			var _isSelected:Boolean = false;
			var _value:String = new String();
			for (var _i:int = 0; _i < _preferences.length; _i++) {
				if (_preferences[_i].isSelected) {
					_isSelected = true;
					if (_value.length>0) {
						_value += ";";
					}
					_value += _preferences[_i].preference.guid;
					_preferences[_i].preference.pageVariable = 1;
				} else {
					_preferences[_i].preference.pageVariable = 0;
				}
			}
			if (_isSelected) {
				if (_value.length == 0) {
					throw new IllegalOperationError("Invalid preferences amount or preference GUID at page sequence No." + _page.pageSequence.order.toString() + " page No." + _page.order.toString());
				}
				var _feedbackXMLNode:FeedbackXMLNode = new FeedbackXMLNode("", _value);
				_feedbacksXML.addFeedback(_feedbackXMLNode);
				this.dispatchEvent(new SubmitEvent("submit", _feedbacksXML.xml));
				return true;
			} else {
				infoPage(GlobalValue.getValue("messages")["PreferenceRequired"]);
				return false;
			}
		}
		private function updateAnswer(_position:int):void {
			var _hasAnswer:Boolean = false;
			//for (_position; _position < _preferences.length; _position++) {
			//if (_preferences[_position].isSelected) {
			//_nextAnswerPosition = _position + 1;
			//_hasAnswer = true;
			//break;
			//}
			//}
			//if (_hasAnswer) {
			//removeChild(_mainPageContainer);
			//removeChild(_coverShadow);
			//initContainer();
			//initContent();
			//_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_preferences[_position].preference.answer) + "</pageText>";
			//_continueButton.label = "<buttonName>" + PageVariableReplacer.replaceAll(_preferences[_position].preference.buttonName) + "</buttonName>";
			//_continueButton.enabled = true;
			//layout();
			//} else {
			nextPage();
			//}
		}
	}
}