package com.ethos.changetech.controls{
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import fl.transitions.Tween;
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.geom.ColorTransform;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;

	public class RoomLoader extends MovieClip {
		
		private var _width;
		private var _minWidth:Number;
		private var _horizontalMargin:Number;
		
		private var _labelTextField:StaticTextField;
		
		private var _tweenWidth:Tween;
		
		public function RoomLoader() {
			initLayout();
			initTextField();
			this.addEventListener(Event.ADDED_TO_STAGE, addedToStageHandler);
		}
		override public function get width():Number {
			return _width;
		}
		public function set label(_value:String) {
			_labelTextField.htmlText = "<roomLoaderLabel>" + _value + "</roomLoaderLabel>";
			layout();
		}
		public function set primaryThemeColor(_value:uint) {
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _value;
			bg_mc.transform.colorTransform = _colorTransform;
		}
		private function initLayout():void {
			_minWidth = GlobalValue.getValue("layout")["RoomLoaderMixWidth"];
			_horizontalMargin = GlobalValue.getValue("layout")["RoomLoaderTextHorizontalMargin"];
		}
		private function initTextField():void {
			_labelTextField = new StaticTextField(TextFieldAutoSize.LEFT, false, true, AntiAliasType.ADVANCED);
			_labelTextField.styleSheet = StyleSheet(GlobalValue.getValue("css"));
			labelHolder_mc.addChild(_labelTextField);
		}
		private function layout(){
			_width = Math.max(_minWidth, _labelTextField.textWidth + 2 * _horizontalMargin);
			mask_mc.x = 0;
			mask_mc.y = 0;
			mask_mc.width = 1;
			bg_mc.width = _width;
			_labelTextField.x = (_width - _labelTextField.width) / 2;
			_labelTextField.y = (bg_mc.height - _labelTextField.height) / 2;
		}
		private function addedToStageHandler(_event:Event):void {
			_tweenWidth = new Tween(mask_mc, "width", null, 1, _width, 20, false);
		}
	}
}