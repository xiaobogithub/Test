package com.ethos.changetech.controls{
	import com.ning.display.ColorableSprite;
	import fl.motion.Color;
	import flash.display.MovieClip;
	import flash.errors.IllegalOperationError;
	import flash.events.MouseEvent;
	import flash.geom.ColorTransform;

	public class SecondaryButton extends ColorableSprite {

		private var _enabled:Boolean = true;

		public function SecondaryButton() {
			initButton();
			initVisual();
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
				setEnabledBackgroundColor();
			} else {
				this.mouseEnabled = false;
				setdisabledBackgroundColor();
			}
		}
		override public function get width():Number {
			return bg_mc.width;
		}
		override public function set width(_value:Number):void {
			if (_value == bg_mc.width) {
				return;
			}
			bg_mc.width = _value;
			layout();
		}
		override public function get height():Number {
			return bg_mc.height;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ethos.changetech.controls.SecondaryButton");
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
		private function initVisual():void {
			symbol_mc.gotoAndStop("normal");
			bg_mc.gotoAndStop("normal");
		}
		private function layout():void {
			symbol_mc.x = bg_mc.x + (bg_mc.width - symbol_mc.width) / 2;
			symbol_mc.y = bg_mc.y + (bg_mc.height - symbol_mc.height) / 2;
		}
		private function setEnabledBackgroundColor():void {
			var _color = new Color();
			_color.brightness = 0;
			bg_mc.transform.colorTransform = _color;
			symbol_mc.transform.colorTransform = _color;
		}
		private function setdisabledBackgroundColor():void {
			var _color = new Color();
			_color.brightness = 0.25;
			bg_mc.transform.colorTransform = _color;
			symbol_mc.transform.colorTransform = _color;
		}
		private function clickHandler(_event:MouseEvent):void {
			enabled = false;
		}
		private function mouseDownHandler(_event:MouseEvent):void {
			symbol_mc.gotoAndStop("down");
			bg_mc.gotoAndStop("down");
		}
		private function mouseNormalHandler(_event:MouseEvent):void {
			symbol_mc.gotoAndStop("normal");
			bg_mc.gotoAndStop("normal");
		}
		private function mouseOverHandler(_event:MouseEvent):void {
			symbol_mc.gotoAndStop("over");
			bg_mc.gotoAndStop("over");
		}
	}
}