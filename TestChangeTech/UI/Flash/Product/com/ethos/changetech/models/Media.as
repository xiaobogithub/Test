package com.ethos.changetech.models{
	public class Media {
		private var _page:Page;
		private var _type:String;
		private var _mediaURL:String;

		public function Media(_parentPage:Page, _xml:XML) {
			_page = _parentPage;
			fromXML(_xml);
		}
		public function get page():Page {
			return _page;
		}
		public function get type():String {
			return _type;
		}
		public function get mediaURL():String {
			return _mediaURL;
		}
		public function fromXML(_data:XML):void {
			_type = _data.@Type;
			_mediaURL = _data.@Media;
			validation();
		}
		private function validation():void {
			trace("    ###################################");
			trace("      --media type = " + _type);
			trace("      --meida URL = " + _mediaURL);
		}
	}
}