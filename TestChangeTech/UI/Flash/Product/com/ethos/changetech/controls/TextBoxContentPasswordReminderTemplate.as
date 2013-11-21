package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import com.ning.text.StringExtension;
	import fl.controls.TextInput;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.filters.BlurFilter;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	
	public class TextBoxContentPasswordReminderTemplate extends TextBoxContentStandardBasedTemplate {
		
		private var _emailLabelTextField:StaticTextField;
		private var _emailTextInput:TextInput;
		private var _backToLoginSprite:Sprite;
		private var _enableAlphaFilter:BlurFilter;
		
		public function TextBoxContentPasswordReminderTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function updateInnerContent():void {
			var _textArray:Array = StringExtension.smartSplit(_page.text, ";");
			_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_textArray[0]) + "</pageText>";
			_footerTextTextField.htmlText = "<hyperlink>" + PageVariableReplacer.replaceAll(_page.footerText) + "</hyperlink>";
			
			_emailLabelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_emailLabelTextField.textColor = 0x999999;
			_emailLabelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));		
			_emailLabelTextField.htmlText = "<inputLabel>" + _textArray[1] + "</inputLabel>";
			addChild(_emailLabelTextField);

			_enableAlphaFilter = new BlurFilter(0,0,0);
			
			_emailTextInput = new TextInput();
			_emailTextInput.textField.filters = [_enableAlphaFilter];
			addChild(_emailTextInput);
			
			_backToLoginSprite = new Sprite();
			_backToLoginSprite.buttonMode = true;
			_backToLoginSprite.useHandCursor = true;
			_backToLoginSprite.addEventListener(MouseEvent.CLICK, backToLoginClickHandler);		
			addChild(_backToLoginSprite);
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			_emailLabelTextField.x = _horizontalPadding;
			_emailLabelTextField.y = _lastPositionY + 2 * _textMargin;
			_lastPositionY = _emailLabelTextField.y + _emailLabelTextField.height;

			_emailTextInput.x = _emailLabelTextField.x + _emailLabelTextField.textWidth + _textMargin;
			_emailTextInput.y = _emailLabelTextField.y - (_emailTextInput.height - _emailLabelTextField.height) / 2;
			_emailTextInput.width = _contentWidth - _emailLabelTextField.width - _textMargin;
			
			return _lastPositionY;
		}
		override protected function footerTextFieldLayout(_lastPositionY:Number):Number {
			_footerTextTextField.x = _horizontalPadding;
			_footerTextTextField.y = _lastPositionY + 2 * _textMargin;
			_footerTextTextField.width = _contentWidth;
			
			_backToLoginSprite.x = _horizontalPadding;
			_backToLoginSprite.y = _lastPositionY + 2 * _textMargin;
			_backToLoginSprite.graphics.clear();
			_backToLoginSprite.graphics.beginFill(0xFFFFFF, 0);
			_backToLoginSprite.graphics.drawRect(0, 0, _footerTextTextField.textWidth, _footerTextTextField.textHeight);
			_backToLoginSprite.graphics.endFill();
			_lastPositionY = _backToLoginSprite.y + _backToLoginSprite.height;
			
			_lastPositionY += 5;
			return _lastPositionY;
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (_emailTextInput.text.length == 0) {
				infoPage(GlobalValue.getValue("messages")["PasswordRemiderInfoRequired"]);
				this.dispatchEvent(new PageEvent("setbutton", {enabled:true}));
				return;
			}
			_emailTextInput.enabled = false;
			var _passwordReminderXML:XML = <PasswordReminder></PasswordReminder>;
			_passwordReminderXML.@Email = _emailTextInput.text;
			this.dispatchEvent(new SubmitEvent("submit", _passwordReminderXML));
		}
		private function backToLoginClickHandler(_event:MouseEvent):void {
			previousPage();
		}
	}
	
}