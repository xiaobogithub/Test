package com.ethos.changetech.controls{
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import fl.motion.Color;
	import flash.display.GradientType;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.geom.Matrix;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class ContinueButton extends Sprite {
		
		private var _enabled:Boolean = true;
		private var _horizontalMargin:Number = 23;
		
		private var _buttonColorNormal:Array;
		private var _buttonColorOver:Array;
		private var _buttonColorDown:Array;
		
		private var _backgroundColor:Sprite;
		private var _backgroundMatrix:Matrix;
		private var _labelTextField:StaticTextField;

		public function ContinueButton() {
			initValue();
			initButton();
			initBackground();
			initTextField();
			layout();
		}
		override public function get width():Number {
			return mask_mc.width;
		}
		override public function set width(_value:Number):void {
			if (_value == mask_mc.width) {
				return;
			}
			mask_mc.width = _value;
			_backgroundMatrix.createGradientBox(mask_mc.width, mask_mc.height, Math.PI / 2, 0, 0);
			setColorNormal();
			layout();
		}
		override public function get height():Number {
			return mask_mc.height;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ethos.changetech.controls.ContinueButton");
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
		public function get enabled():Boolean {
			return _enabled;
		}
		public function set enabled(_value:Boolean):void {
			if (_value == _enabled) {
				return;
			}
			_enabled = _value;
			if (_enabled) {
				this.mouseEnabled = true;
				_labelTextField.textColor = 0xffffff;
				setEnabledBackgroundColor();
			} else {
				this.mouseEnabled = false;
				_labelTextField.textColor = 0xcccccc;
				setdisabledBackgroundColor();
			}
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
		private function initValue():void{
			_buttonColorNormal = String(GlobalValue.getValue("layout")["PrimaryButtonColorNormal"]).split(",");
			_buttonColorOver = String(GlobalValue.getValue("layout")["PrimaryButtonColorOver"]).split(",");
			_buttonColorDown = String(GlobalValue.getValue("layout")["PrimaryButtonColorDown"]).split(",");
		}
		private function initButton():void {
			this.buttonMode = true;
			this.useHandCursor = true;
			this.mouseChildren = false;
			this.addEventListener(MouseEvent.CLICK, clickHandler);
			this.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
			this.addEventListener(MouseEvent.MOUSE_OUT, mouseNormalHandler);
			this.addEventListener(MouseEvent.MOUSE_OVER, mouseOverHandler);			
		}
		private function initBackground():void {
			_backgroundColor = new Sprite();
			bg_mc.addChild(_backgroundColor);
			_backgroundMatrix = new Matrix();
  			_backgroundMatrix.createGradientBox(mask_mc.width, mask_mc.height, Math.PI / 2, 0, 0);
			setColorNormal();
		}
		private function initTextField():void {
			_labelTextField = new StaticTextField(TextFieldAutoSize.LEFT, true, true, AntiAliasType.ADVANCED);
			_labelTextField.textColor = 0xffffff;
			_labelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			addChild(_labelTextField);
		}
		private function layout():void {
			_labelTextField.width = bg_mc.width - 2 * _horizontalMargin;
			_labelTextField.x = _horizontalMargin;
			_labelTextField.y = (mask_mc.height - _labelTextField.textHeight) / 2;
		}
		private function setColorNormal():void {
			_backgroundColor.graphics.clear();
			_backgroundColor.graphics.beginGradientFill(GradientType.LINEAR, _buttonColorNormal, [1,1], [0,255], _backgroundMatrix);
			_backgroundColor.graphics.drawRect(0, 0, mask_mc.width, mask_mc.height);
			_backgroundColor.graphics.endFill();
		}
		private function setColorOver():void {
			_backgroundColor.graphics.clear();
			_backgroundColor.graphics.beginGradientFill(GradientType.LINEAR, _buttonColorOver, [1,1], [0,255], _backgroundMatrix);
			_backgroundColor.graphics.drawRect(0, 0, mask_mc.width, mask_mc.height);
			_backgroundColor.graphics.endFill();
		}
		private function setColorDown():void {
			_backgroundColor.graphics.clear();
			_backgroundColor.graphics.beginGradientFill(GradientType.LINEAR, _buttonColorDown, [1,1], [0,255], _backgroundMatrix);
			_backgroundColor.graphics.drawRect(0, 0, mask_mc.width, mask_mc.height);
			_backgroundColor.graphics.endFill();
		}
		private function setEnabledBackgroundColor():void {
			var _color = new Color();
			_color.brightness = 0;
			bg_mc.transform.colorTransform = _color;
		}
		private function setdisabledBackgroundColor():void {
			var _color = new Color();
			_color.brightness = 0.25;
			bg_mc.transform.colorTransform = _color;
		}
		private function clickHandler(_event:MouseEvent):void{
			enabled = false;
		}
		private function mouseNormalHandler(_event:MouseEvent):void {
			setColorNormal();
		}
		private function mouseDownHandler(_event:MouseEvent):void {
			setColorDown();
		}
		private function mouseOverHandler(_event:MouseEvent):void {
			setColorOver();
		}
	}
}