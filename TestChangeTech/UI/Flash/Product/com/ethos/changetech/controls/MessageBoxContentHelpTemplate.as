package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.Help;
	import com.ethos.changetech.models.HelpItem;
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;	
	import com.ning.text.StaticTextField;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class MessageBoxContentHelpTemplate extends MessageBoxContentTemplate {
		// Model
		private var _helpModel:Help;
		
		// Flag
		private var _selectedIndex:int = -1;

		// Containers
		private var _helpTitles:Array;
		private var _helpTitleClickers:Array;
		private var _textTextField:StaticTextField;

		public function MessageBoxContentHelpTemplate(_object:MessageObject, _w:Number) {
			super(_object, _w);
		}
		override protected function initModel(_object:MessageObject):void {
			_helpModel = Help(_object.model);
			if (_helpModel == null) {
				throw new IllegalOperationError("Invalid data for MessageBox");
			}
		}
		override protected function initContainer():void {
			_helpTitles = new Array();
			_helpTitleClickers = new Array();
			for each (var _helpItemModel:HelpItem in _helpModel.items) {
				var _titleTextField:StaticTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
				_titleTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
				_titleTextField.htmlText = "<helpItemTitle>" + _helpItemModel.title + "</helpItemTitle>";
				_helpTitles.push(_titleTextField);
				addChild(_titleTextField);
				
				var _titleClicker:Sprite = new Sprite();
				_titleClicker.buttonMode = true;
				_titleClicker.useHandCursor = true;
				_titleClicker.addEventListener(MouseEvent.CLICK, titleClickHandler);
				_helpTitleClickers.push(_titleClicker);
				addChild(_titleClicker);
			}
			_textTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_textTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));			
		}
		override protected function layout():void {
			var _oldHeight:Number = height;
			var _lastPositionY:Number = 0;
			
			for(var _i:int = 0; _i< _helpTitles.length; _i++ ) {
				_helpTitles[_i].x = _horizontalPadding;
				_helpTitles[_i].y = _lastPositionY;
				_helpTitles[_i].width = _contentWidth;
				
				_helpTitleClickers[_i].x = _horizontalPadding;
				_helpTitleClickers[_i].y = _lastPositionY;
				_helpTitleClickers[_i].graphics.clear();
				_helpTitleClickers[_i].graphics.beginFill(0xFFFFFF, 0);
				_helpTitleClickers[_i].graphics.drawRect(0, 0, _helpTitles[_i].textWidth, _helpTitles[_i].textHeight);
				_helpTitleClickers[_i].graphics.endFill();
				
				_lastPositionY = _helpTitleClickers[_i].y + _helpTitleClickers[_i].height;
				
				if(_i == _selectedIndex) {
					_textTextField.x = _horizontalPadding;
					_textTextField.y = _lastPositionY;
					_textTextField.width = _contentWidth;
					_lastPositionY = _textTextField.y + _textTextField.height + _textMargin;
				} else {
					_lastPositionY += _textMargin;
				}
			}
			
			height = _lastPositionY;
			if(_oldHeight != height) {
				this.dispatchEvent(new PageEvent("arranged", null));
			}
		}
		private function titleClickHandler(_event:MouseEvent):void {
			_selectedIndex = _helpTitleClickers.indexOf(_event.target);
			_textTextField.htmlText = "<helpItemText>" + _helpModel.items[_selectedIndex].text + "</helpItemText>";
			if(!contains(_textTextField)) {
				addChild(_textTextField);
			}
			layout();
		}
	}
}