package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.TipFriend;
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

	public class MessageBoxContentTipFriendTemplate extends MessageBoxContentTemplate {
		// Layout
		private var _submitButtonWidth:Number;

		// Model
		private var _tipFriendModel:TipFriend;

		// Container
		private var _textTextField:StaticTextField;
		private var _emailTextInput:TextInput;
		private var _feedbackTextField:StaticTextField;
		private var _submitButton:Button;
		private var _enableAlphaFilter:BlurFilter;

		// Flag
		private var _isFeedback:Boolean = false;

		public function MessageBoxContentTipFriendTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			_tipFriendModel = TipFriend(_object.model);
			if (_tipFriendModel == null) {
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
			_textTextField.htmlText = "<messageText>" + _tipFriendModel.text + "</messageText>";
			addChild(_textTextField);

			_enableAlphaFilter = new BlurFilter(0,0,0);

			_emailTextInput = new TextInput();
			_emailTextInput.textField.multiline = false;
			_emailTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_emailTextInput);

			_submitButton = new Button();
			_submitButton.textField.filters = [_enableAlphaFilter];
			_submitButton.label = _tipFriendModel.submitButtonName;
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

			var _lineHeight:Number = Math.max(_emailTextInput.height, _submitButton.height);
			_submitButton.width = _submitButtonWidth;
			_submitButton.x = _contentWidth - _submitButton.width + _horizontalPadding;
			_submitButton.y = _lastPositionY + _textMargin + (_lineHeight - _submitButton.height) / 2;

			_emailTextInput.x = _horizontalPadding;
			_emailTextInput.y = _lastPositionY + _textMargin + (_lineHeight - _emailTextInput.height) / 2;
			_emailTextInput.width = _contentWidth - _submitButton.width - _textMargin;

			_lastPositionY += _textMargin + _lineHeight;

			if (_isFeedback) {
				_feedbackTextField.x = _horizontalPadding;
				_feedbackTextField.y = _lastPositionY + _textMargin;
				_feedbackTextField.width = _contentWidth;
				_lastPositionY = _feedbackTextField.y + _feedbackTextField.height;
			}
			height = _lastPositionY;
			if (_oldHeight != height) {
				this.dispatchEvent(new PageEvent("arranged", null));
			}
		}
		private function submitButtonClickHandler(_event:MouseEvent):void {
			_submitButton.enabled = false;
			_feedbackTextField.text = "";

			if (StringExtension.isEmail(_emailTextInput.text)) {
				var _tipXML:XML = <TipFriend></TipFriend>;;
				_tipXML.@Invitee = _emailTextInput.text;
				this.dispatchEvent(new SubmitEvent("submit",_tipXML));
			} else {
				_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["InvalidEmail"].message + "</messageBoxFailedFeedback>";
				_submitButton.enabled = true;
				_isFeedback = true;
				layout();
			}
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			trace("tip friend feedback message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["TipFriendFailed"].message + "</messageBoxFailedFeedback>";
					_submitButton.enabled = true;
					_isFeedback = true;
					break;
				case "1" :
					_feedbackTextField.htmlText = "<messageBoxSuccessfulFeedback>" + GlobalValue.getValue("messages")["TipFriendSuccessful"].message + "</messageBoxSuccessfulFeedback>";
					_submitButton.enabled = true;
					_isFeedback = true;
					break;
				default :
					throw new IllegalOperationError("Invalid tip friend server state.");
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
