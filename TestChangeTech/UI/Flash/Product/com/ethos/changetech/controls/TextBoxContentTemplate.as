package com.ethos.changetech.controls{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Page;
	import com.hexagonstar.util.debug.Debug;
	import com.ning.data.GlobalValue;
	import com.ning.display.SizableSprite;
	import flash.events.MouseEvent;

	public class TextBoxContentTemplate extends SizableSprite {

		// Layout
		protected var _horizontalPadding:Number;
		protected var _textMargin:Number;
		protected var _width:Number;
		protected var _contentWidth:Number;
		protected var _minHeight:Number;
		protected var _maxHeight:Number;
		protected var _primaryThemeColor:uint;

		// Model
		protected var _page:Page;

		// Flag
		protected var _sized:Boolean = false;

		public function TextBoxContentTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			_page = _targetPage;
			_width = _w;
			_minHeight = _minH;
			_maxHeight = _maxH;
			initLayout();
			initContent();
			layout();
		}
		override public function get width():Number {
			return _width;
		}
		public function set primaryThemeColor(_value:uint):void {
			if(_value == _primaryThemeColor) {
				return;
			}
			_primaryThemeColor = _value;
		}
		protected function initLayout():void {
			_horizontalPadding = GlobalValue.getValue("layout")["TextFieldScrollBarWidth"];
			_textMargin = GlobalValue.getValue("layout")["TextFieldTextSpacing"];
			_contentWidth = _width - 2 * _horizontalPadding;
		}
		protected function initContent():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.TextBoxContentSource.initContent()]");
		}
		protected function layout():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.TextBoxContentSource.layout()]");
		}
		protected function infoPage(_messageObject:MessageObject):void {
			this.dispatchEvent(new PageEvent("info", _messageObject));
		}
		protected function arrangePage():void {
			this.dispatchEvent(new PageEvent("arranged", null));
		}
		protected function nextPage():void {
			stage.focus = stage;
			this.dispatchEvent(new PageEvent("nextpage", null));
		}
		protected function previousPage():void {
			this.dispatchEvent(new PageEvent("previouspage", null));
		}
		protected function startLoadingPage():void {
			this.dispatchEvent(new PageEvent("startloading", null));
		}
		protected function stopLoadingPage():void {
			this.dispatchEvent(new PageEvent("stoploading", null));
		}
		public function freeze():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.TextBoxContentSource.freeze()]");
		}
		public function unfreeze():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.TextBoxContentSource.unfreeze()]");
		}
		public function arrangeFeedback():void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.TextBoxContentSource.arrandeFeedback()]");
		}
		public function primaryButtonClickedHandler(_event:MouseEvent):void {
			nextPage();
		}
		public function secondaryButtonClickedHandler(_event:MouseEvent):void {
			previousPage();			
		}
		public function submitSuccessfulCallBack(_message:String):void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.PageContainer.submitSuccessfulCallBack()]");
		}
		public function submitFailedCallBack(_message:String):void {
			trace("[ABSTRACTE: com.ethos.changetech.controls.PageContainer.submitFailedCallBack()]");
		}
	}
}