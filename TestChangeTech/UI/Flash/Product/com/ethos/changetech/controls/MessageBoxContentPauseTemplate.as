package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.ProgramStatus;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import fl.controls.Button;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import fl.controls.ComboBox; 
	import fl.data.DataProvider; 

	public class MessageBoxContentPauseTemplate extends MessageBoxContentTemplate {

		// Layout
		private var _submitButtonWidth:Number;

		// Model
		private var _programStatusModel:ProgramStatus;

		// Container
		private var _textTextField:StaticTextField;
		private var _feedbackTextField:StaticTextField;
		private var _submitButton:Button;
		private var _enableAlphaFilter:BlurFilter;
		private var _pauseWeekComboBox:ComboBox;
		// Flag
		private var _type:String;
		private var _isFeedback:Boolean = false;

		public function MessageBoxContentPauseTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			_programStatusModel = ProgramStatus(_object.model);
			_type = _object.type;
			if (_programStatusModel == null) {
				throw new IllegalOperationError("Invalid data for MessageBox");
			}
		}
		override protected function initLayout():void {
			super.initLayout();
			_submitButtonWidth = GlobalValue.getValue("layout")["PauseProgramSubmitButtonWidth"];				
		}
		override protected function initContainer():void {
			_textTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_textTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_textTextField.htmlText = "<messageText>" + _programStatusModel.text + "</messageText>";
			addChild(_textTextField);

			_enableAlphaFilter = new BlurFilter(0,0,0);
			
			_submitButton = new Button();
			_submitButton.textField.filters = [_enableAlphaFilter];
			_submitButton.label = _programStatusModel.submitButtonName;
			if (GlobalValue.getValue("IsPaused") == "true")
			{
				_submitButton.enabled=false;
			}
			_submitButton.addEventListener(MouseEvent.CLICK, submitButtonClickHandler);
			addChild(_submitButton);
			
			_pauseWeekComboBox = new ComboBox();
			var _pauseWeekArray:Array = new Array ({label:"1 week", data:"1"}, {label:"2 week", data:"2"}, {label:"3 week", data:"3"}, {label:"4 week", data:"4"});
			_pauseWeekComboBox.dataProvider = new DataProvider(_pauseWeekArray);
			_pauseWeekComboBox.dropdownWidth = 50;
			addChild(_pauseWeekComboBox);

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

			_submitButton.width = _submitButtonWidth;
			//_submitButton.x = (_contentWidth - _submitButton.width) / 2 + _horizontalPadding;
			_submitButton.x = _contentWidth - _horizontalPadding - _submitButtonWidth;
			_submitButton.y = _lastPositionY + 2 * _textMargin;
			_lastPositionY = _submitButton.y + _submitButton.height;

			_pauseWeekComboBox.x = _horizontalPadding * 2;
			_pauseWeekComboBox.y = _submitButton.y;
			_pauseWeekComboBox.width = 100;
			
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
			var _programeStatusXML:XML = <ProgrameStatus></ProgrameStatus>;
			_programeStatusXML.@Status = _type;
			_programeStatusXML.@Week = _pauseWeekComboBox.selectedIndex + 1;
			trace(_programeStatusXML);
			this.dispatchEvent(new SubmitEvent("submit",_programeStatusXML));
		}
		override public function submitSuccessfulCallBack(_message:String):void {
			trace("programe status feedback message:");
			trace(_message);
			var _flag:String = _message.charAt(0);
			switch (_flag) {
				case "0" :
					_feedbackTextField.htmlText = "<messageBoxFailedFeedback>" + GlobalValue.getValue("messages")["PauseProgramFailed"].message + "</messageBoxFailedFeedback>";	
					_submitButton.enabled = true;
					_isFeedback = true;
					break;
				case "1" :
					_feedbackTextField.htmlText = "<messageBoxSuccessfulFeedback>" + GlobalValue.getValue("messages")["PauseProgramSuccessful"].message + "</messageBoxSuccessfulFeedback>";							
					_submitButton.enabled = false;
					_isFeedback = true;
					GlobalValue.setValue("IsPaused", "true");
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