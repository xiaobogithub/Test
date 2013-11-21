package com.ethos.changetech.controls{
	import com.ning.data.GlobalValue;
	import com.ning.display.ColorableSprite;
	import com.ning.text.StaticTextField;
	import flash.display.MovieClip;
	import flash.errors.IllegalOperationError;
	import flash.geom.ColorTransform;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class CategoryButton extends ColorableSprite {

		private var _labelTextField:StaticTextField;
		private var _width:Number;
		private var _height:Number;
		private var _minWidth:Number;
		private var _horizontalMargin:Number;

		public function CategoryButton() {
			initLayout();
			initButton();
			initTextField();
			layout();
		}
		private function initLayout():void {
			_width = bg_mc.width;
			_height = bg_mc.height;
			_minWidth = GlobalValue.getValue("layout")["ProgramButtonMinWidth"];
			_horizontalMargin = GlobalValue.getValue("layout")["ProgramButtonTextHorizontalMargin"];
		}
		override public function get width():Number {
			return _width;
		}
		override public function set width(_value:Number):void {
			throw new IllegalOperationError("Can not set width for com.ethos.changetech.controls.CategoryButton");
		}
		override public function get height():Number {
			return _height;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ethos.changetech.controls.CategoryButton");
		}
		public function get minWidth():Number {
			return _minWidth;
		}
		public function set minWidth(_value:Number):void {
			if (_value == _minWidth) {
				return;
			}
			_minWidth = _value;
			if (_minWidth > _width) {
				layout();
			}
		}
		public function get label():String {
			return _labelTextField.htmlText;
		}
		public function set label(_value:String):void {
			if (_value == _labelTextField.htmlText) {
				return;
			}
			_labelTextField.htmlText = _value;
			layout();
		}
		public function get horizontalMargin():Number {
			return _horizontalMargin;
		}
		public function set horizontalMargin(_value:Number):void {
			if (_value == _horizontalMargin) {
				return;
			}
			_horizontalMargin = _value;
			layout();
		}
		override protected function updatePrimaryThemeColor():void {
			//_labelTextField.textColor = _primaryThemeColor;
			setBackgroundColor();
		}
		private function initButton():void {
			this.mouseChildren = false;
			//DTD-1625:Remove link from program room in Flash
			//this.buttonMode = true;
			this.useHandCursor = true;
		}
		private function initTextField():void {
			_labelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			//_labelTextField.textColor = _primaryThemeColor;
			_labelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			addChild(_labelTextField);
		}
		private function layout():void {
			_width = Math.max(_minWidth, _labelTextField.textWidth + 2 * _horizontalMargin);
			bg_mc.width = _width;
			bg_mc.height = _height;		 
			_labelTextField.x = (_width - _labelTextField.width) / 2;
			_labelTextField.y = (_height - _labelTextField.height) / 2;
		}
		private function setBackgroundColor():void {
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _primaryThemeColor;
			bg_mc.transform.colorTransform = _colorTransform;
		}
	}
}