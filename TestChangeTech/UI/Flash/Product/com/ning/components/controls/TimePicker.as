package com.ning.components.controls{
	import fl.controls.NumericStepper;
	import fl.core.UIComponent;
	import flash.events.Event;

	public class TimePicker extends UIComponent {
		public var hour_ns:NumericStepper = new NumericStepper();
		public var minute_ns:NumericStepper = new NumericStepper();

		private var _hour:Number;
		private var _minute:Number;

		public function TimePicker() {
			setupNumberSteppers();
			setNow();
			layout();
		}
		override public function get height():Number {
			return Math.max(hour_ns.height,minute_ns.height);
		}
		//set height
		public function get hour():Number {
			return _hour;
		}
		public function set hour(_value:Number):void {
			if (_value == _hour) {
				return;
			}
			_hour = _value;
			hour_ns.value = _value;
		}
		public function get minute():Number {
			return _minute;
		}
		public function set minute(_value:Number):void {
			if (_value == _minute) {
				return;
			}
			_minute = _value;
			minute_ns.value = _value;
		}
		public function get value():String {
			return toString();
		}
		public function set value(_value:String):void {
			if (_value == toString()) {
				return;
			}
			var _timeArray:Array = _value.split(":");
			hour = Number(_timeArray[0]);
			minute = Number(_timeArray[1]);
		}
		override public function toString():String {
			return _hour.toString() + ":" + _minute.toString();
		}
		private function setupNumberSteppers():void {
			hour_ns.minimum = 0;
			hour_ns.maximum = 23;
			hour_ns.stepSize = 1;
			hour_ns.addEventListener(Event.CHANGE, timeChangeHandler);
			addChild(hour_ns);
			minute_ns.minimum = 0;
			minute_ns.maximum = 59;
			minute_ns.stepSize = 1;
			minute_ns.addEventListener(Event.CHANGE, timeChangeHandler);
			addChild(minute_ns);
		}
		private function setNow():void {
			var _date:Date = new Date();
			hour = _date.hours;
			minute = _date.minutes;
		}
		private function layout():void {
		}
		private function timeChangeHandler(_event:Event):void {
			_hour = hour_ns.value;
			_minute = minute_ns.value;
			this.dispatchEvent(new Event("change"));
		}
	}
}