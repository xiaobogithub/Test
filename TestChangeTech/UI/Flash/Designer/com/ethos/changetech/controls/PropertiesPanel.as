package com.ethos.changetech.controls{
	import flash.display.Sprite;

	public class PropertiesPanel extends Sprite {
		private var _target:Object;
		
		public function get target():Object{
			return _target;
		}
		public function set target(_value:Object):void{
			if(_value == _target){return;}
			_target = _value;
			updateProperties();
		}
		protected function updateProperties():void{
			trace("ABSTRACT: update properties in sub class");
		}
		public function updateLocation():void{
			trace("ABSTRACT: update location in sub class");
		}
	}
}