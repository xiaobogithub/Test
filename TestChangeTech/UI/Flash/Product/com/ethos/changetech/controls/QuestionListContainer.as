package com.ethos.changetech.controls{
	import com.ning.display.ColorableSprite;
	import fl.core.UIComponent;
	import flash.errors.IllegalOperationError;

	public class QuestionListContainer extends ColorableSprite {

		private var _contentWidth:Number;
		private var _actualWidth:Number;
		private var _actualHeight:Number;

		private var _itemList:Array;

		public function QuestionListContainer() {
			_itemList = new Array();
		}
		override public function get width():Number {
			return _actualWidth;
		}
		override public function set width(_value:Number):void {
			contentWidth = _value;
		}
		override public function get height():Number {
			return _actualHeight;
		}
		override public function set height(_value:Number):void {
			throw new IllegalOperationError("Can not set height for com.ethos.changetech.controls.QuestionContainer");
		}
		public function get contentWidth():Number {
			return _contentWidth;
		}
		public function set contentWidth(_value:Number):void {
			if (_value == _contentWidth) {
				return;
			}
			_contentWidth = _value;
			layout();
		}
		public function get actualWidth():Number {
			return _actualWidth;
		}
		public function get actualHeight():Number {
			return _actualHeight;
		}
		public function get items():Array{
			return _itemList;
		}
		public function addItem(_item:UIComponent) {
			addChild(_item);
			_itemList.push(_item);
			layout();
		}
		private function layout():void {
			var _lastPositionY:Number = 0;
			var _maxWidth:Number = 0;
			for each(var _item:UIComponent in _itemList){
				_item.width = _contentWidth;
				_item.x = 0;
				_item.y = _lastPositionY;
				_lastPositionY = _item.y + _item.height;
				_maxWidth = Math.max(_maxWidth, _item.width);
			}
			_actualWidth = _maxWidth;
			_actualHeight = _lastPositionY;
		}
	}
}