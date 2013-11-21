package com.ethos.changetech.controls{
	import flash.events.Event;

	public class DesignElementEvent extends Event {
		public static  const CREATE:String = "create";
		public static  const DELETE:String = "delete";
		private var _targetClass:Class;
		private var _x:Number;
		private var _y:Number;

		public function DesignElementEvent(_type,_positionX, _positionY, _targetObject:Class) {
			super(_type);
			_targetClass = _targetObject;
			_x = _positionX;
			_y = _positionY;
		}
		public function get targetClass():Class {
			return _targetClass;
		}
		public function get x():Number {
			return _x;
		}
		public function get y():Number {
			return _y;
		}
		public override function clone():Event {
			return new DesignElementEvent(type,x,y,targetClass);
		}
		public override function toString():String {
			return formatToString("DesignElementEvent","type","bubbles","cancelable","eventPhase");
		}
	}
}