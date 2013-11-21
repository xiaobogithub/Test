package com.ethos.changetech.controls{
	import flash.display.*;
	import flash.events.*;
	import flash.geom.Rectangle;

	public class ToolbarButton extends Sprite {

		private var _isMouseDown:Boolean = false;
		private var _isCreatingNew:Boolean = false;
		private var _newItem:NewItem;
		private var _canvasRectangle:Rectangle;
		protected var _targetObject:Class = DesignElement;

		public function get targetObject():Class {
			return _targetObject;
		}

		public function ToolbarButton() {
			this.mouseChildren = false;
			this.buttonMode = true;
			this.useHandCursor = true;

			_newItem = new NewItem();
			_canvasRectangle = new Rectangle(100, 0, 800, 600);

			this.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
		}
		public function createNewItem():void {
			trace("create "+_targetObject.toString() +" at " + stage.mouseX.toString() + "," + stage.mouseY.toString());
			this.dispatchEvent(new DesignElementEvent("create", stage.mouseX, stage.mouseY, _targetObject));
		}
		private function mouseDownHandler(_event:MouseEvent):void {
			_isMouseDown = true;
			this.addEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
			this.stage.addEventListener(MouseEvent.MOUSE_UP,mouseUpHandler);
		}
		private function mouseMoveHandler(_event:MouseEvent):void {
			if (_isMouseDown&&!_isCreatingNew) {
				_isCreatingNew = true;
				addChild(_newItem);
				_newItem.startDrag(true);
			}
		}
		private function mouseUpHandler(_event:MouseEvent):void {
			this.removeEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
			this.stage.removeEventListener(MouseEvent.MOUSE_UP, mouseUpHandler);
			_isMouseDown = false;
			if (_isCreatingNew) {
				_isCreatingNew = false;
				_newItem.stopDrag();
				removeChild(_newItem);
				if (!_canvasRectangle.contains(_event.stageX, _event.stageY)) {
					return;
				}
				createNewItem();
			} else {
				trace("mouse up");
			}
		}
	}
}