package com.ethos.changetech.models{
	import com.hexagonstar.util.debug.Debug;
	public class PageSequence {
		private var _session:Session;
		private var _guid:String;
		private var _order:String;
		private var _name:String;
		private var _categoryName:String;
		private var _categoryDescription:String;
		private var _programRoomName:String;
		private var _programRoomDescription:String;
		private var _primaryThemeColor:uint;
		private var _secondaryThemeColor:uint;
		private var _pages:Array;
		private var _index:Number;
		private var _isRelapse:Boolean;
		
		private var _topBarColor:uint;
		private var _primaryButtonColorNormal:String;
		private var _primaryButtonColorOver:String;
		private var _primaryButtonColorDown:String;
		private var _primaryButtonColorDisable:String;
		private var _coverShadowColor:uint;
		private var _coverShadowVisible:String;

		public function PageSequence(_parentSession:Session, _xml:XML, index:Number, isRelapse:Boolean) {
			_index=index;
			_session=_parentSession;
			_pages = new Array();
			_isRelapse=isRelapse;
			fromXML(_xml);
		}

		public function get session():Session {
			return _session;
		}
		public function get order():String {
			return _order;
		}
		public function get guid():String {
			return _guid;
		}
		public function get name():String {
			return _name;
		}
		public function get categoryName():String {
			return _categoryName;
		}
		public function get categoryDescription():String {
			return _categoryDescription;
		}
		public function get programRoomName():String {
			return _programRoomName;
		}
		public function get programRoomDescription():String {
			return _programRoomDescription;
		}
		public function get primaryThemeColor():uint {
			return _primaryThemeColor;
		}
		public function get secondaryThemeColor():uint {
			return _secondaryThemeColor;
		}
		public function get pages():Array {
			return _pages;
		}
		public function get isRelapse():Boolean {
			return _isRelapse;
		}
		public function get TopBarColor():uint {
			return _topBarColor;
		}
		public function get PrimaryButtonColorNormal():String {
			return _primaryButtonColorNormal;
		}
		public function get PrimaryButtonColorOver():String {
			return _primaryButtonColorOver;
		}
		public function get PrimaryButtonColorDown():String {
			return _primaryButtonColorDown;
		}
		public function get PrimaryButtonColorDisable():String {
			return _primaryButtonColorDisable;
		}
		public function get CoverShadowColor():uint{
			return _coverShadowColor;
		}
		public function get CoverShadowVisible():String{
			return _coverShadowVisible;
		}
		public function fromXML(_data:XML):void {
			_guid=_data.@GUID;
			_order=_data.@Order;
			// _order == "" means relapse
//			if (_order == "")
//			{
//				_order = _guid;
//			}
			_name=_data.@Name;
			_categoryName=_data.@CategoryName;
			_categoryDescription=_data.@CategoryDescription;
			_programRoomName=_data.@ProgramRoomName;
			_programRoomDescription=_data.@ProgramRoomDescription;
			_primaryThemeColor=_data.@PrimaryThemeColor;
			_secondaryThemeColor=_data.@SecondaryThemeColor;
			_primaryButtonColorNormal=_data.@PrimaryButtonColorNormal;
			_primaryButtonColorOver=_data.@PrimaryButtonColorOver;
			_primaryButtonColorDown=_data.@PrimaryButtonColorDown;
			_primaryButtonColorDisable=_data.@PrimaryButtonColorDisable;
			_topBarColor=_data.@TopBarColor;
			_coverShadowColor=_data.@CoverShadowColor;
			_coverShadowVisible=_data.@CoverShadowVisible;
			//validation();
			var _pageXMLList:XMLList=_data.Page;
			if (_pageXMLList.length()>0) {
				for each (var _pageNode:XML in _pageXMLList) {
					addPage(new Page(this, _pageNode,_index));
				}
				//_pages.sortOn("order", Array.NUMERIC);
				//Debug.trace("    page amount = " + _pages.length.toString());
			} else {
				//Debug.trace("    No page in this sequence!");
			}
		}
		private function validation():void {
			Debug.trace("----------------------------------------------------");
			Debug.trace("  **page sequcnce No. "+_order);
			Debug.trace("    page sequence name = "+_name);
			Debug.trace("    page sequence predictor category name = "+_categoryName);
			Debug.trace("    page sequence predictor category description = "+_categoryDescription);
			Debug.trace("    page sequence program room name = "+_programRoomName);
			Debug.trace("    page sequence program room description = "+_programRoomDescription);
		}
		private function addPage(_page:Page):void {
			_pages.push(_page);
			if (! _isRelapse) {
				_session.pages.push(_page);
			}
		}
	}
}