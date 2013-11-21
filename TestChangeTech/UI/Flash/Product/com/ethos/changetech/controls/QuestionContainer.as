package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.Question;
	import com.ethos.changetech.models.QuestionItem;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ning.controls.GroupableCheckBox;
	import com.ning.data.GlobalValue;
	import com.ning.display.ColorableSprite;
	import com.ning.components.controls.MultiNumericStepper;
	import com.ning.controls.NumberSlider;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.ComboBox;
	import fl.controls.NumericStepper;
	import fl.controls.RadioButton;
	import fl.controls.RadioButtonGroup;
	import fl.controls.TextArea;
	import fl.controls.TextInput;
	import fl.data.DataProvider;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.events.TextEvent;
	import flash.filters.BlurFilter;
    import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class QuestionContainer extends ColorableSprite {	
		private var _question:Question;

		private var _labelTextField:StaticTextField;
		private var _feedbackTextField:StaticTextField;
		private var _questionControl:Sprite;
		private var _enableAlphaFilter:BlurFilter;

		private var _contentWidth:Number;
		private var _actualWidth:Number;
		private var _actualHeight:Number;

		private var _isFeedback:Boolean = false;
		private var _isInText:Boolean = false;

		public function QuestionContainer(_targetQuestion:Question) {
			_question = _targetQuestion;
			initTextField();
			initFilter();
			initQuestionControl();
			layout();
		}
		override public function get width():Number {
			return _actualWidth;
		}
		override public function set width(_value:Number):void {
			throw new IllegalOperationError("Can not set width for com.ethos.changetech.controls.QuestionContainer");
		}
		override public function get height():Number {
			return _actualHeight;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ethos.changetech.controls.QuestionContainer");
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value == _contentWidth) {
				return;
			}
			_contentWidth = _value;
			layout();
		}
		public function get actualWidth():Number {
			return _actualWidth;
		}
		public function get actualHeight():Number {
			return _actualHeight;
		}
		public function get question():Question {
			return _question;
		}
		override protected function updatePrimaryThemeColor():void {
			//_labelTextField.textColor = _primaryThemeColor;
			switch(_question.type){
				case "Slider" :
				NumberSlider(_questionControl).primaryThemeColor = _primaryThemeColor;
				break;
				case "TimePicker" :
				MultiNumericStepper(_questionControl).primaryThemeColor = _primaryThemeColor;
				break;
			}			
		}
		override protected function updateSecondaryThemeColor():void {
			//_feedbackTextField.textColor = _secondaryThemeColor;
			switch(_question.type){
				case "Slider" :
				NumberSlider(_questionControl).secondaryThemeColor = _secondaryThemeColor;
				break;
				case "TimePicker" :
				MultiNumericStepper(_questionControl).secondaryThemeColor = _secondaryThemeColor;
				break;
			}		
		}
		/*public function insertable():Boolean {
			switch (_question.type) {
				case "Numeric" :
					return true;
				case "Singleline" :
					return false;
				case "Multiline" :
					return false;
				case "CheckBox" :
					return false;
				case "RadioButton" :
					return false;
				case "DropDownList" :
					return false;
				case "Slider" :
					return false;
				case "TimePicker" :
					return true;
				default :
					throw new IllegalOperationError("Invalid question type at question[" + _question.guid + "]");
			}
		}*/
		private function hasLabel():Boolean {
			switch (_question.type) {
				case "Numeric" :
					return true;
				case "Singleline" :
					return true;
				case "Multiline" :
					return true;
				case "CheckBox" :
					return true;
				case "RadioButton" :
					return true;
				case "DropDownList" :
					return true;
				case "Slider" :
					return false;
				case "TimePicker" :
					return true;
				default :
					throw new IllegalOperationError("Invalid question type at question[" + _question.guid + "]");
			}
		}
		private function hasFeedback():Boolean {
			switch (_question.type) {
				case "Numeric" :
					return false;
				case "Singleline" :
					return false;
				case "Multiline" :
					return false;
				case "CheckBox" :
					return true;
				case "RadioButton" :
					return true;
				case "DropDownList" :
					return true;
				case "Slider" :
					return true;
				case "TimePicker" :
					return false;
				default :
					throw new IllegalOperationError("Invalid question type at question[" + _question.guid + "]");
			}
		}
		private function initTextField():void {
			_labelTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_labelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			//_labelTextField.textColor = _primaryThemeColor;
			if (_question.label.length > 0 && hasLabel()) {
				_labelTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_question.label) + "</pageText>";
				addChild(_labelTextField);
			}
			_feedbackTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_feedbackTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			//_feedbackTextField.textColor = _secondaryThemeColor;
		}
		private function initFilter():void {
			_enableAlphaFilter = new BlurFilter(0,0,0);
		}
		private function initQuestionControl():void {
			switch (_question.type) {
				case "Numeric" :
					var _numericStepper:NumericStepper = new NumericStepper();
					_numericStepper.textField.filters = [_enableAlphaFilter];
					//_numericStepper.textField.maxChars = 2;
					_numericStepper.stepSize = 1;
					_numericStepper.minimum = 0;
					_numericStepper.maximum = int.MAX_VALUE;
					if (_question.pageVariable != null) {
						if (_question.answer != _question.pageVariable) {
							_question.answer = _question.pageVariable;
							_question.answerString = _question.pageVariable.toString();
						}
					}
					if (_question.answer != null) {
						_numericStepper.value = Number(_question.answer);
					} else {
						_question.pageVariable = _numericStepper.value;
						_question.answer = _numericStepper.value;
						_question.answerString = _numericStepper.value.toString();
					}
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
					{
						_numericStepper.enabled = false;
					}
					_numericStepper.addEventListener(Event.CHANGE, numericStepperChangeHandler);
					_questionControl = _numericStepper;
					break;
				case "Singleline" :
					var _singleInput:TextInput = new TextInput();
					_singleInput.textField.filters = [_enableAlphaFilter];
					_singleInput.textField.multiline = false;
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
					{
						if (_question.pageVariable != null){
							_singleInput.text = _question.pageVariable.toString();
							if(_question.answer != _question.pageVariable){
								_question.answer = _question.pageVariable;
								_question.answerString = _question.pageVariable.toString();
							}
						}
						_singleInput.enabled = false;
					}
					if (_question.answer != null) {
						_singleInput.text = _question.answer.toString();
					} else {
						_question.pageVariable = _singleInput.text;
						_question.answer = _singleInput.text;
						_question.answerString = _singleInput.text;
					}
					_singleInput.addEventListener(Event.CHANGE, textInputChangeHandler);
					_questionControl = _singleInput;
					break;
				case "Multiline" :
					var _multilineInput:TextArea = new TextArea();
					_multilineInput.textField.filters = [_enableAlphaFilter];
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
					{
						if (_question.pageVariable != null){
							_multilineInput.text = _question.pageVariable.toString();
							if(_question.answer != _question.pageVariable){
								_question.answer = _question.pageVariable;
								_question.answerString = _question.pageVariable.toString();
							}
						}
						_multilineInput.enabled = false;
					}
					if (_question.answer != null) {
						_multilineInput.text = _question.answer.toString();
					} else {
						_question.pageVariable = _multilineInput.text;
						_question.answer = _multilineInput.text;
						_question.answerString = _multilineInput.text;
					}
					_multilineInput.addEventListener(Event.CHANGE, textInputChangeHandler);
					_questionControl = _multilineInput;
					
					break;
				case "CheckBox" :
					var _checkBoxListContainer:QuestionListContainer = new QuestionListContainer();
					var _totalScore:int = parseInt(String(_question.pageVariable));
					_question.pageVariable = 0;
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
					{
						_question.pageVariable = _totalScore;
					}
					for each (var _questionItem:QuestionItem in _question.questionItems) {
						var _checkBoxItem:GroupableCheckBox = new GroupableCheckBox;
						_checkBoxItem.textField.filters = [_enableAlphaFilter];
						_checkBoxItem.label = PageVariableReplacer.replaceAll(_questionItem.label);
						_checkBoxItem.value = _questionItem;
						if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
						{
							var _score:int = parseInt(String(QuestionItem(_checkBoxItem.value).score));
							if ((_totalScore % (_score * 2)) >= _score)
							{
								_checkBoxItem.selected = true;
								if(_question.answer == null){
									_question.answer = new Array();
								}
								_question.answer.push(_checkBoxItem.value);
								if (_question.answerString.length > 0) {
									_question.answerString += ";";
								}
								_question.answerString += _checkBoxItem.value.guid;
								var _feedbackString:String = PageVariableReplacer.replaceAll(_checkBoxItem.value.feedback);
								if(_feedbackString != null && _feedbackString.length > 0) {						
									_feedbackTextField.htmlText += "<pageText>" + _feedbackString + "</pageText>";
									_isFeedback = true;
								}
							}
							_checkBoxItem.enabled = false;
						}
						else{
							if(_question.answer != null && (_question.answer as Array).indexOf(_questionItem) != -1){
								_checkBoxItem.selected = true;
								_question.pageVariable += _questionItem.score;			
								var _feedbackString:String = PageVariableReplacer.replaceAll(_checkBoxItem.value.feedback);
								if(_feedbackString != null && _feedbackString.length > 0) {
									_feedbackTextField.htmlText += "<pageText>" + _feedbackString + "</pageText>";
									_isFeedback = true;
								}
							}
						}
						_checkBoxItem.addEventListener(MouseEvent.CLICK, checkBoxClickHandler);
						_checkBoxListContainer.addItem(_checkBoxItem);
					}					
					_questionControl = _checkBoxListContainer;
					break;
				case "RadioButton" :
					var _radioButtonListContainer:QuestionListContainer = new QuestionListContainer();
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() != "true")
					{
						_question.pageVariable = 0;
					}
					var _groupName:String = "radioButtonGroup";
					var _radioButtonGroup:RadioButtonGroup = new RadioButtonGroup(_groupName);
					for each (var _questionItem:QuestionItem in _question.questionItems) {
						var _radioButtonItem:RadioButton = new RadioButton;
						_radioButtonItem.textField.filters = [_enableAlphaFilter];
						_radioButtonItem.group = _radioButtonGroup;
						_radioButtonItem.label = PageVariableReplacer.replaceAll(_questionItem.label);
						_radioButtonItem.value = _questionItem;
						if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
						{
							_radioButtonItem.enabled = false;
							if (_question.pageVariable == _questionItem.score)
							{
								_radioButtonItem.selected = true;
								var _feedbackString:String = PageVariableReplacer.replaceAll(_questionItem.feedback);
								if(_feedbackString != null && _feedbackString.length > 0) {							
									_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
									_isFeedback = true;
								}
								_question.answer = _questionItem;
								_question.answerString = _questionItem.guid;
							}
						}
						else
						{
							if(QuestionItem(_question.answer) == _questionItem){
								_radioButtonItem.selected = true;
								_question.pageVariable = _questionItem.score;
								var _feedbackString:String = PageVariableReplacer.replaceAll(_questionItem.feedback);
								if(_feedbackString != null && _feedbackString.length > 0) {							
									_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
									_isFeedback = true;
								}
							}
						}
						_radioButtonItem.addEventListener(MouseEvent.CLICK, radioButtonClickHandler);
						_radioButtonListContainer.addItem(_radioButtonItem);
					}
					_questionControl = _radioButtonListContainer;
					break;
				case "DropDownList" :
					var _dropDown:ComboBox = new ComboBox();
					_dropDown.textField.filters = [_enableAlphaFilter];
					_dropDown.prompt = _question.label;
					for each (var _questionItem:QuestionItem in _question.questionItems) {
						_dropDown.addItem({label:PageVariableReplacer.replaceAll(_questionItem.label), value: _questionItem});
						if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
						{
							if(_question.pageVariable == _questionItem.score) {
								_dropDown.selectedIndex = _dropDown.length - 1;
								_question.answer = _questionItem;
								_question.answerString = _questionItem.guid;
							}
						}
						else
						{
							if(_question.answer == _questionItem) {
								_dropDown.selectedIndex = _dropDown.length - 1;
								_question.pageVariable = _questionItem.score;
							}
						}
					}
					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
					{
						_dropDown.enabled = false;
					}

					if(_question.answer == null){
						_dropDown.selectedIndex = 0;
						_question.pageVariable = _question.questionItems[0].score;
						_question.answer = _dropDown.selectedItem.value;
						_question.answerString = _dropDown.selectedItem.value.guid;
					}
					_dropDown.addEventListener(Event.CHANGE, dropDownChangeHandler);
					_questionControl = _dropDown;
					var _feedbackString:String = PageVariableReplacer.replaceAll(_dropDown.selectedItem.value.feedback);
					if(_feedbackString != null && _feedbackString.length > 0) {							
						_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
						_isFeedback = true;
					}
					break;
				case "Slider" :
					var _slider:NumberSlider = new NumberSlider();
					_slider.styleSheet = StyleSheet(GlobalValue.getValue("css"));
					var _labelArray:Array = StringExtension.smartSplit(_question.label, ";");
					_slider.title =_labelArray[0].length > 0 ? "<pageText>" + _labelArray[0] + "</pageText>" : _labelArray[0];
					if (_labelArray.length >= 2){
						_slider.leftLabel = _labelArray[1].length > 0 ? "<sliderSideLabel>" + _labelArray[1] + "</sliderSideLabel>" : _labelArray[1];
					}
					if (_labelArray.length >= 3) {
						_slider.rightLabel = _labelArray[2].length > 0 ? "<sliderSideLabel>" + _labelArray[2] + "</sliderSideLabel>" : _labelArray[2];
					}					
					var _dataProvider:DataProvider = new DataProvider();
					for(var _i:int = 0; _i < _question.questionItems.length; _i++){
						var _questionItem:QuestionItem = _question.questionItems[_i];
						if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
						{
							if (_question.pageVariable == _questionItem.score) {
								_question.answer = _questionItem;
								_question.answerString = _questionItem.guid;
							}
							_dataProvider.addItem({number:_questionItem.label, value:_questionItem, enabled:false});
						} else {
							_dataProvider.addItem({number:_questionItem.label, value:_questionItem, enabled:true});
						}
					}
					_slider.dataProvider = _dataProvider;
					_slider.primaryThemeColor = _primaryThemeColor;
					_slider.secondaryThemeColor = _secondaryThemeColor;
					
					if (_question.answer != null) {
						_slider.selectedNumber = int(QuestionItem(_question.answer).label);
					} else {
						_question.answer = _slider.selectedItem.value;
						_question.answerString = _slider.selectedItem.value.guid;
					}
					_question.pageVariable = QuestionItem(_question.answer).score;
					_slider.addEventListener(Event.CHANGE, sliderChangeHandler);
					_questionControl = _slider;
					var _feedbackString:String = PageVariableReplacer.replaceAll(_slider.selectedItem.value.feedback);
					if(_feedbackString != null && _feedbackString.length > 0) {						
						_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
						_isFeedback = true;
					}
					break;
				case "TimePicker" :
					var _timePicker:MultiNumericStepper = new MultiNumericStepper();
					_timePicker.styleSheet = StyleSheet(GlobalValue.getValue("css"));
					_timePicker.delimiter = ":";
					var _dataProvider:DataProvider = new DataProvider();
					for(var _i:int = 0; _i < _question.questionItems.length; _i++) {
						var _questionItem:QuestionItem = _question.questionItems[_i];
						if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
						{
							_dataProvider.addItem({label:_questionItem.label, range:_questionItem.range, enabled:false});
						}
						else{
							_dataProvider.addItem({label:_questionItem.label, range:_questionItem.range, enabled:true});
						}
					}
					_timePicker.dataProvider = _dataProvider;
					_timePicker.primaryThemeColor = _primaryThemeColor;
					_timePicker.secondaryThemeColor = _secondaryThemeColor;
					_timePicker

					if (GlobalValue.getValue("FirstLevelDataOfTimepicker") != null)
					{
						_question.answer = GlobalValue.getValue("FirstLevelDataOfTimepicker");
						_question.answerString = GlobalValue.getValue("FirstLevelDataOfTimepicker").toString();
						_question.pageVariable = question.answerString;
					}else {				
					if (_question.pageVariable != null){
						if(_question.answer != _question.pageVariable){
							_question.answer = _question.pageVariable;
							_question.answerString = _question.pageVariable.toString();
						}
					}
					}
					
					if (_question.answer != null) {
						_timePicker.value = _question.answer.toString();
					} else {
						_question.pageVariable = _timePicker.value;
						_question.answer = _timePicker.value;
						_question.answerString = _timePicker.value;
					}				

					if (String(GlobalValue.getValue("IsRetake")).toLowerCase() != "true")
					{
						_timePicker.addEventListener(Event.CHANGE, timePickerChangeHandler);
						//_timePicker.addEventListener(TextEvent.TEXT_INPUT, timePickerChangeHandler);
						//_timePicker.addEventListener(MouseEvent.ROLL_OUT, timePickerChangeHandler);
					}
					_questionControl = _timePicker;
					break;
				default :
					throw new IllegalOperationError("Invalid question type at question[" + _question.guid + "]");
			}
			addChild(_questionControl);
			if(_isFeedback){
				addChild(_feedbackTextField);
			}
		}
		public function layout():void {
			trace("question container layout");
			var _lastPositionY:Number = 0;
			_labelTextField.width = _contentWidth;
			if (_question.label.length > 0 && hasLabel()) {
				_lastPositionY = _labelTextField.y + _labelTextField.height;
			}
			_questionControl.y = _lastPositionY;
			trace("_questionControl.y = " + _questionControl.y);
			switch (_question.type) {
				case "Numeric" :
					_actualWidth = 60;
					_questionControl.width = _actualWidth;
					break;
				case "TimePicker" :
					_actualWidth = _questionControl.width;
					break;
				case "Singleline" :
					_actualWidth = _contentWidth;
					_questionControl.width = _actualWidth;
					_questionControl.height = 20;
					break;
				case "Multiline" :
					_actualWidth = _contentWidth;
					_questionControl.width = _actualWidth;
					_questionControl.height = 100;
					break;
				case "CheckBox" :
				case "RadioButton" :
				case "DropDownList" :
				case "Slider" :
					_actualWidth = _contentWidth;
					_questionControl.width = _actualWidth;
					break;
				default :
					throw new IllegalOperationError("Invalid question type at question[" + _question.guid + "]");
			}
			_lastPositionY += _questionControl.height;
			_feedbackTextField.width = _contentWidth;
			_feedbackTextField.y = _lastPositionY;
			if (_isFeedback) {
				_lastPositionY = _feedbackTextField.y + _feedbackTextField.height;
			}
			_actualHeight = _lastPositionY;
			this.dispatchEvent(new PageEvent("arranged", null));
		}
		private function numericStepperChangeHandler(_event:Event):void {
			_question.pageVariable = _event.currentTarget.value;
			_question.answer = _event.currentTarget.value;
			_question.answerString = _event.currentTarget.value.toString();
		}
		private function textInputChangeHandler(_event:Event):void {
			_question.pageVariable = _event.currentTarget.text;
			_question.answer = _event.currentTarget.text;
			_question.answerString = _event.currentTarget.text;
		}
		private function checkBoxClickHandler(_event:MouseEvent):void {
			_isFeedback = false;
			if (contains(_feedbackTextField)) {
				removeChild(_feedbackTextField);
			}
			_feedbackTextField.htmlText = "";
			_question.pageVariable = 0;
			_question.answer = null;
			_question.answerString = "";
			for each (var _checkBoxItem:GroupableCheckBox in QuestionListContainer(_questionControl).items) {
				if (_checkBoxItem.selected) {
					_question.pageVariable += QuestionItem(_checkBoxItem.value).score;
					if(_question.answer == null){
						_question.answer = new Array();
					}
					_question.answer.push(_checkBoxItem.value);
					if (_question.answerString.length > 0) {
						_question.answerString += ";";
					}
					_question.answerString += _checkBoxItem.value.guid;
					var _feedbackString:String = PageVariableReplacer.replaceAll(_checkBoxItem.value.feedback);
					if(_feedbackString != null && _feedbackString.length > 0) {						
						_feedbackTextField.htmlText += "<pageText>" + _feedbackString + "</pageText>";
						_isFeedback = true;
					}
				}
			}
			if (_isFeedback) {
				addChild(_feedbackTextField);
			}
			layout();
		}
		private function radioButtonClickHandler(_event:MouseEvent):void {
			_isFeedback = false;
			var _feedbackString:String = PageVariableReplacer.replaceAll(_event.currentTarget.value.feedback);
			if(_feedbackString != null && _feedbackString.length > 0) {					
				_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
				_isFeedback = true;
			}
			_question.pageVariable = _event.currentTarget.value.score;
			_question.answer = _event.currentTarget.value;
			_question.answerString = _event.currentTarget.value.guid;
			if(!_isFeedback && contains(_feedbackTextField)) {
				removeChild(_feedbackTextField);
			}														   
			if (_isFeedback && !contains(_feedbackTextField)) {
				addChild(_feedbackTextField);
			}
			layout();
		}
		private function dropDownChangeHandler(_event:Event):void {
			_isFeedback = false;
			var _feedbackString:String = PageVariableReplacer.replaceAll(_event.currentTarget.selectedItem.value.feedback);
			if(_feedbackString != null && _feedbackString.length > 0) {		
				_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
				_isFeedback = true;
			}
			_question.pageVariable = _event.currentTarget.selectedItem.value.score;
			_question.answer = _event.currentTarget.selectedItem.value;
			_question.answerString = _event.currentTarget.selectedItem.value.guid;
			if(!_isFeedback && contains(_feedbackTextField)) {
				removeChild(_feedbackTextField);
			}														   
			if (_isFeedback && !contains(_feedbackTextField)) {
				addChild(_feedbackTextField);
			}
			layout();
		}
		private function sliderChangeHandler(_event:Event):void{
			_isFeedback = false;
			var _feedbackString:String = PageVariableReplacer.replaceAll(_event.currentTarget.selectedItem.value.feedback);
			if(_feedbackString != null && _feedbackString.length > 0) {
				_feedbackTextField.htmlText = "<pageText>" + _feedbackString + "</pageText>";
				_isFeedback = true;
			}
			_question.pageVariable = _event.currentTarget.selectedItem.value.score;
			_question.answer = _event.currentTarget.selectedItem.value;
			_question.answerString = _event.currentTarget.selectedItem.value.guid;
			if(!_isFeedback && contains(_feedbackTextField)) {
				removeChild(_feedbackTextField);
			}														   
			if (_isFeedback && !contains(_feedbackTextField)) {
				addChild(_feedbackTextField);
			}
			layout();
		}
		private function timePickerChangeHandler(_event:Event):void {
			trace(_event.currentTarget.value);
			//stage.focus = stage;
			_question.pageVariable = _event.currentTarget.value;
			_question.answer = _event.currentTarget.value;
			_question.answerString = _event.currentTarget.value;
			GlobalValue.setValue("FirstLevelDataOfTimepicker", _question.answerString);
			//layout();
		}
	}
}