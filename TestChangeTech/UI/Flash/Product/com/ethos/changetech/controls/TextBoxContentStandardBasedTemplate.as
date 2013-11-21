package com.ethos.changetech.controls{
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import flash.display.DisplayObject;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class TextBoxContentStandardBasedTemplate extends TextBoxContentTemplate {

		protected var _titleTextField:StaticTextField;
		protected var _textTextField:StaticTextField;
		protected var _footerTextTextField:StaticTextField;
		protected var _innerContainer:DisplayObject;		
		public var _tabelStringField;

		protected var _isFooter:Boolean = false;		

		public function TextBoxContentStandardBasedTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function initContent():void {
			_titleTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_titleTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			if (_page.title.length > 0) {
				if (!_page.singleMode) {
					_titleTextField.htmlText = "<pageTitle>" + PageVariableReplacer.replaceAll(_page.title) + "<pageTitle>";
				}else {
					_titleTextField.htmlText = "<onlyPageTitle>" + PageVariableReplacer.replaceAll(_page.title) + "</onlyPageTitle>";
				}
			}
			addChild(_titleTextField);

			_textTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);			
			_textTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			trace(_page.text);
			if (_page.text.length > 0) {
				if (!_page.singleMode){
					var _bodyText = PageVariableReplacer.replaceAll(_page.text);
					trace(_bodyText.indexOf("<table"));
					if(_bodyText.indexOf("<table")>0)
					{
						//trace("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
						//trace(_bodyText.substring(_bodyText.indexOf("[table"),(_bodyText.length)));
						_tabelStringField = _bodyText.substring(_bodyText.indexOf("<table"),(_bodyText.indexOf("</table>")+8));						
						_textTextField.htmlText = "<pageText>" +_bodyText.substring(0,(_bodyText.indexOf("<table")-1)) +_bodyText.substring(_bodyText.indexOf("</table>")+8,(_bodyText.length)) + "<pageText>";		
						}
					else
					{
						_textTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_page.text) + "<pageText>";
						}				
				}else {
					_textTextField.htmlText = "<onlyPageText>" + PageVariableReplacer.replaceAll(_page.text) + "<onlyPageText>";
				}
			}
			trace(_textTextField.htmlText);
			addChild(_textTextField);
			
			_footerTextTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			//_footerTextTextField.textColor = _secondaryThemeColor;
			_footerTextTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			_footerTextTextField.htmlText = "<pageText>" + PageVariableReplacer.replaceAll(_page.footerText) + "</pageText>";
			addChild(_footerTextTextField);
			updateInnerContent();
		}
		override protected function layout():void {
			var _lastPositionY:Number = 0;
			_lastPositionY=getPositionY();
			/*if (_page.title.length > 0) {
				_titleTextField.x = _horizontalPadding;
				_titleTextField.width = _contentWidth;
				if(_page.text.length == 0 && (_minHeight > _titleTextField.height)){
					_titleTextField.y = _lastPositionY  + (_minHeight - _titleTextField.height) / 2;
				}else {
					_titleTextField.y = _lastPositionY + _textMargin;
				}
				_lastPositionY = _titleTextField.y + _titleTextField.height;
			}
			if (_page.text.length > 0) {
				_textTextField.x = _horizontalPadding;
				_textTextField.width = _contentWidth;
				if(_page.title.length == 0 && (_minHeight > _textTextField.height)){
					_textTextField.y = _lastPositionY  + (_minHeight - _textTextField.height) / 2;
				}else {
					_textTextField.y = _lastPositionY + _textMargin;
				}
				_lastPositionY = _textTextField.y + _textTextField.height;
			}*/
			
			_lastPositionY = innerContainerLayout(_lastPositionY);
			_lastPositionY = footerTextFieldLayout(_lastPositionY);
			if (_lastPositionY != height) {
				height = _lastPositionY;
				arrangePage();
			}
		}
		
		protected function getPositionY():Number{
			var _lastPositionY:Number = 0;
			if (_page.title.length > 0) {
				_titleTextField.x = _horizontalPadding;
				_titleTextField.width = _contentWidth;
				if(_page.text.length == 0 && (_minHeight > _titleTextField.height)){
					_titleTextField.y = _lastPositionY  + (_minHeight - _titleTextField.height) / 2;
				}else {
					_titleTextField.y = _lastPositionY + _textMargin;
				}
				_lastPositionY = _titleTextField.y + _titleTextField.height;
			}
			if (_page.text.length > 0) {
				_textTextField.x = _horizontalPadding;
				_textTextField.width = _contentWidth;
				if(_page.title.length == 0 && (_minHeight > _textTextField.height)){
					_textTextField.y = _lastPositionY  + (_minHeight - _textTextField.height) / 2;
				}else {
					_textTextField.y = _lastPositionY + _textMargin;
				}
				_lastPositionY = _textTextField.y + _textTextField.height;
			}
			return _lastPositionY;
		}
		
		
		protected function innerContainerLayout(_lastPositionY:Number):Number {
			if (_innerContainer != null) {
				_innerContainer.x = (_contentWidth - _innerContainer.width) / 2 + _horizontalPadding;
				_innerContainer.y = _lastPositionY + _textMargin;
				_lastPositionY = _innerContainer.y + _innerContainer.height;
			}
			return _lastPositionY;
		}
		protected function footerTextFieldLayout(_lastPositionY:Number):Number {
			if (_isFooter) {
				_footerTextTextField.x = _horizontalPadding;
				_footerTextTextField.y = _lastPositionY + _textMargin;
				_footerTextTextField.width = _contentWidth;
				_lastPositionY = _footerTextTextField.y + _footerTextTextField.height;
				return _lastPositionY;
			}
			_footerTextTextField.alpha = 0;
			return _lastPositionY;
		}
		protected function updateInnerContent():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.StandardBasedTextBoxContent.updateInnerContent()]");
		}
	}
}