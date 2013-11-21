package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Profile;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.Button;
	import fl.controls.TextInput;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBoxContentProfileTemplate extends MessageBoxContentTemplate {

		// Layout
		private var _submitButtonWidth:Number;

		// Model
		private var _profileModel:Profile;

		// Container
		private var _textTextField:StaticTextField;
		private var _feedbackTextField:StaticTextField;
		private var _submitButton:Button;
		private var _enableAlphaFilter:BlurFilter;
		private var _profileItemLabels:Array;
		private var _profileItemTextInputs:Array;

		// Flag
		private var _isFeedback:Boolean = false;

		public function MessageBoxContentProfileTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			_profileModel = Profile(_object.model);
			if (_profileModel == null) {
				throw new IllegalOperationError("Invalid data for MessageBox");
			}
		}
		override protected function initLayout():void {
			super.initLayout();
			_submitButtonWidth = GlobalValue.getValue("layout")["TipFriendSubmitButtonWidth"];
		}
		override protected function initContainer():void {
			_textTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_textTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_textTextField.htmlText = "<messageText>" + _profileModel.text + "</messageText>";
			addChild(_textTextField);

			_enableAlphaFilter = new BlurFilter(0,0,0);
			_profileItemLabels = new Array();
			_profileItemTextInputs = new Array();

			for (var _i:int = 0; _i < _profileModel.profileItems.length; _i++) {
				var _labelTextField:StaticTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
				_labelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
				_labelTextField.htmlText = "<inputLabel>" + _profileModel.profileItems[_i].name + "</inputLabel>";
				_profileItemLabels.push(_labelTextField);
				addChild(_labelTextField);

				var _textInput:TextInput = new TextInput();
				_textInput.textField.multiline = false;
				_textInput.textField.filters = [_enableAlphaFilter];
				_profileItemTextInputs.push(_textInput);
				addChild(_textInput);
			}
			_submitButton = new Button();
			_submitButton.textField.filters = [_enableAlphaFilter];
			_submitButton.label = _profileModel.submitButtonName;
			_submitButton.addEventListener(MouseEvent.CLICK, submitButtonClickHandler);
			addChild(_submitButton);

			_feedbackTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_feedbackTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			addChild(_feedbackTextField);
		}
		override protected function layout():void {
			var _oldHeight = height;
			var _lastPositionY:Number = 0;

			_textTextField.x = _horizontalPadding;
			_textTextField.width = _contentWidth;
			_lastPositionY = _textTextField.y + _textTextField.height;

			var _maxLabelWidth:Number = 0;
			for (var _i:int = 0; _i < _profileModel.profileItems.length; _i++) {
				var _labelTextField:StaticTextField = _profileItemLabels[_i];
				_maxLabelWidth = Math.max(_maxLabelWidth, _labelTextField.textWidth);
			}
			var _inputFieldPositionX:Number = _maxLabelWidth + _horizontalPadding + _textMargin;
			var _inputFieldWidth:Number = _contentWidth - _maxLabelWidth - _textMargin;

			for (var _i:int = 0; _i < _profileModel.profileItems.length; _i++) {
				var _labelTextField:StaticTextField = _profileItemLabels[_i];
				var _textInput:TextInput = _profileItemTextInputs[_i];
				var _lineHeight:Number = Math.max(_labelTextField.height, _textInput.height);
				_labelTextField.x = _horizontalPadding;
				_labelTextField.y = _lastPositionY + _textMargin + (_lineHeight - _labelTextField.height) / 2;

				_textInput.width = _inputFieldWidth;
				_textInput.x = _inputFieldPositionX;
				_textInput.y = _lastPositionY + _textMargin + (_lineHeight - _textInput.height) / 2;

				_lastPositionY += _textMargin + _lineHeight;
			}
			if (_isFeedback) {
				_feedbackTextField.x = _horizontalPadding;
				_feedbackTextField.y = _lastPositionY + _textMargin;
				_feedbackTextField.width = _contentWidth;
				_lastPositionY = _feedbackTextField.y + _feedbackTextField.height;
			}
			_submitButton.width = _submitButtonWidth;
			_submitButton.x = _contentWidth - _submitButton.width + _horizontalPadding;
			_submitButton.y = _lastPositionY + _textMargin;
			_lastPositionY = _submitButton.y + _submitButton.height;

			height = _lastPositionY;
			if (_oldHeight != height) {
				this.dispatchEvent(new PageEvent("arranged", null));
			}
		}
		private function submitButtonClickHandler(_event:MouseEvent):void {
			_submitButton.enabled = false;
			_feedbackTextField.text = "";
			var _changeProfileXML:XML = <ChangeProfile></ChangeProfile>;
			var _itemsXML:XML = <Items></Items>;

			for (var _i:int = 0; _i < _profileModel.profileItems.length; _i++) {
				if (_profileItemTextInputs[_i].text.length == 0) {
					continue;
				}
				var _profileItemXML:XML = <Item></Item>;
				if (_profileModel.profileItems[_i].format == "email" && !StringExtension.isEmail(_profileItemTextInputs[_i].text)) {
					_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["InvalidEmail"].message + "</messageBoxFailedFeedback>";
					_submitButton.enabled = true;
					_isFeedback = true;
					layout();
					return;
				}
				_profileItemXML.@Name = _profileModel.profileItems[_i].name;
				_profileItemXML.@NewValue = _profileItemTextInputs[_i].text;
				_itemsXML.appendChild(_profileItemXML);
			}
			_changeProfileXML.appendChild(_itemsXML);
			this.dispatchEvent(new SubmitEvent("submit",_changeProfileXML));
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			trace("change profile feedback message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["ProfileChangeFailed"].message + "</messageBoxFailedFeedback>";
					_submitButton.enabled = true;
					_isFeedback = true;
					break;
				case "1" :
					_feedbackTextField.htmlText = "<messageBoxSuccessfulFeedback>" + GlobalValue.getValue("messages")["ProfileChangeSuccessful"].message + "</messageBoxSuccessfulFeedback>";
					_submitButton.enabled = true;
					_isFeedback = true;
					break;
				default :
					throw new IllegalOperationError("Invalid change profile server state.");
			}
			layout();
		}
		override public function submitFailedCallBack(_message:String):void {
			_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["NetworkError"].message + "</messageBoxFailedFeedback>";
			_submitButton.enabled = true;
			_isFeedback = true;
			layout();
		}
	}
}