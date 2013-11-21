package com.ethos.changetech.controls{
	import com.ethos.changetech.models.MessageObject;
	import com.ning.data.GlobalValue;
	import com.ning.display.SizableSprite;
	import flash.errors.IllegalOperationError;
	
	public class MessageBoxContentTemplate extends SizableSprite {
		// Layout
		protected var _contentWidth:Number;
		protected var _horizontalPadding:Number;
		protected var _textMargin:Number;

		public function MessageBoxContentTemplate(_object:MessageObject, _w:Number) {
			if (_object == null) {
				throw new IllegalOperationError("Invalid data for MessageBox");
			}
			initModel(_object);			
			_contentWidth = _w;

			initLayout();
			initContainer();
			layout();
		}
		protected function initModel(_object:MessageObject):void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.MessageBoxContentTemplate.initModel()]");
		}
		protected function initLayout():void {
			_horizontalPadding = GlobalValue.getValue("layout")["MessageBoxScrollBarWidth"];
			_textMargin = GlobalValue.getValue("layout")["MessageBoxTextMargin"];
		}
		protected function initContainer():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.MessageBoxContentTemplate.initContainer()]");
		}
		protected function layout():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.MessageBoxContentTemplate.layout()]");
		}
		public function submitSuccessfulCallBack(_message:String):void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.MessageBoxContentTemplate.submitSuccessfulCallBack()]");
		}
		public function submitFailedCallBack(_message:String):void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.MessageBoxContentTemplate.submitFailedCallBack()]");
		}
	}
}