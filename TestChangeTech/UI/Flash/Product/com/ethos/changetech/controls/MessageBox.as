package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;
	import com.ning.display.ColorableSprite;
	import com.ning.display.SizableSprite;
	import com.ning.text.StaticTextField;
	import fl.containers.ScrollPane;
	import fl.controls.ScrollPolicy;
	import flash.display.MovieClip;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;	
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBox extends ColorableSprite {

		// Layout
		private var _width:Number;
		private var _innerWidth:Number;
		private var _minHeight:Number;
		private var _maxHeight:Number;
		private var _textMargin:Number;
		private var _scrollBarWidth:Number;

		// Containers
		private var _scrollPane:ScrollPane;
		private var _scrollPaneSourceContainer:MessageBoxContentTemplate;
		private var _titleTextField:StaticTextField;
		private var _buttonTextField:StaticTextField;

		// Model
		private var _messageObject:MessageObject;

		public function MessageBox(_object:MessageObject) {
			_messageObject = _object;
			initLayout();
			initButton();
			initContainer();
			layout();
		}
		public function get textBoxContent():MessageBoxContentTemplate {
			return _scrollPaneSourceContainer;
		}
		override public function get width():Number {
			return bgPanel.width;
		}
		override public function get height():Number {
			return bgPanel.height;
		}
		override protected function updatePrimaryThemeColor():void {
			_titleTextField.textColor = _primaryThemeColor;
			_buttonTextField.textColor = _primaryThemeColor;
		}
		private function initLayout():void {
			_width = GlobalValue.getValue("layout")["MessageBoxWidth"];
			_minHeight = GlobalValue.getValue("layout")["MessageBoxMinHeight"];
			_maxHeight = GlobalValue.getValue("layout")["MessageBoxMaxHeight"];
			_textMargin = GlobalValue.getValue("layout")["MessageBoxTextMargin"];
			_scrollBarWidth = GlobalValue.getValue("layout")["MessageBoxScrollBarWidth"];
			_innerWidth = _width - 2 * _textMargin - 2 * _scrollBarWidth;
		}
		private function initButton():void {
			backButton.buttonMode = true;
			backButton.useHandCursor = true;
			backButton.mouseChildren = false;
			backButton.addEventListener(MouseEvent.CLICK, backButtonClickedHandler);
		}
		private function initContainer():void {
			_titleTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_titleTextField.textColor = _primaryThemeColor;
			_titleTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_titleTextField.htmlText = "<messageTitle>" + _messageObject.title + "</messageTitle>";
			addChild(_titleTextField);

			switch(_messageObject.type){
				case "standard" : 
					_scrollPaneSourceContainer = new MessageBoxContentStandardTemplate(_messageObject, _innerWidth);
					break;
				case "help" :
					_scrollPaneSourceContainer = new MessageBoxContentHelpTemplate(_messageObject, _innerWidth);
					break;
				case "tipFriend" :
					_scrollPaneSourceContainer = new MessageBoxContentTipFriendTemplate(_messageObject, _innerWidth);
					break;
				case "profile" :
					_scrollPaneSourceContainer = new MessageBoxContentProfileTemplate(_messageObject, _innerWidth);
					break;
				case "pause" :
					_scrollPaneSourceContainer =  new MessageBoxContentPauseTemplate(_messageObject, _innerWidth);
					break;
				case "exit" :
					_scrollPaneSourceContainer = new MessageBoxContentProgramStatusTemplate(_messageObject, _innerWidth);
					break;
				case "timeZone" :
					_scrollPaneSourceContainer = new MessageBoxContentTimeZoneTemplate(_messageObject, _innerWidth);
					break;
				case "smsToEmail" :
					_scrollPaneSourceContainer = new MessageBoxContentSmsToEmailTemplate(_messageObject, _innerWidth);
					break;
				default:
					throw new IllegalOperationError("Invalid message type.");
			}
			
			_scrollPaneSourceContainer.addEventListener(PageEvent.ARRANGED, arrangeScrollPaneSourceHandler);
			_scrollPaneSourceContainer.addEventListener(SubmitEvent.SUBMIT, submitSettingHandler);
			_scrollPane = new ScrollPane();
			_scrollPane.horizontalScrollPolicy = ScrollPolicy.OFF;
			_scrollPane.source = _scrollPaneSourceContainer;
			addChild(_scrollPane);

			_buttonTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_buttonTextField.mouseEnabled = false;
			_buttonTextField.textColor = _primaryThemeColor;
			_buttonTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_buttonTextField.htmlText = "<messageButton>" + _messageObject.backButtonLabel + "</messageButton>";
			addChild(_buttonTextField);
		}
		private function layout():void {
			var _lastPositionY:Number = 0;

			bgPanel.width = _width;

			_titleTextField.width = _width - _textMargin * 2;
			_titleTextField.x = (_width - _titleTextField.textWidth) / 2;
			_titleTextField.y = _textMargin;
			_lastPositionY = _titleTextField.y + _titleTextField.textHeight + _textMargin;

			line.y = _lastPositionY;
			_lastPositionY = line.y + line.height + _textMargin;

			var _innerMinHeight = _minHeight - _lastPositionY - backButton.height - 3 * _textMargin;
			var _innerMaxHeight = _maxHeight - _lastPositionY - backButton.height - 3 * _textMargin;

			if (_innerMinHeight <= 0 || _innerMaxHeight <= 0) {
				throw new IllegalOperationError("Invalid minHeight or maxHeight setting for MessageBox. The value is too small to show message.");
			}
			_scrollPane.width = _width - 2 * _textMargin;
			_scrollPane.height = Math.min(_innerMaxHeight, Math.max(_scrollPaneSourceContainer.height, _innerMinHeight));
			_scrollPane.x = _textMargin;
			_scrollPane.y = _lastPositionY;
			_scrollPane.update();
			_lastPositionY = _scrollPane.y + _scrollPane.height + _textMargin;

			backButton.x = (_width - backButton.width) / 2;
			backButton.y = _lastPositionY + _textMargin;
			_buttonTextField.x = backButton.x + (backButton.width - _buttonTextField.width) / 2;
			_buttonTextField.y = backButton.y + (backButton.height - _buttonTextField.height) / 2;
			bgPanel.height = backButton.y + backButton.height + _textMargin;
			this.dispatchEvent(new PageEvent("arranged", null));
		}
		private function backButtonClickedHandler(_event:MouseEvent):void {
			this.dispatchEvent(new PageEvent("category", null));
			/*if (_messageObject.type == "timeZone")
			{
				var timeZoneXml:XML =   <TimeZone></TimeZone>;
				timeZoneXml.@value = GlobalValue.getValue("CurrentTimeZone");
				this.dispatchEvent(new SubmitEvent("submit", timeZoneXml));
			}else if (_messageObject.type == "smsToEmail")
			{
				var smsToEmailXml:XML =   <SMSToEmail></SMSToEmail>;
				smsToEmailXml.@value = GlobalValue.getValue("selectedSMSToEmail");
				this.dispatchEvent(new SubmitEvent("submit", smsToEmailXml));
			}*/
		}
		private function arrangeScrollPaneSourceHandler(_event:PageEvent):void {
			layout();
		}
		private function submitSettingHandler(_event:SubmitEvent):void {
			this.dispatchEvent(new SubmitEvent("submit", _event.xml));
		}
	}
}