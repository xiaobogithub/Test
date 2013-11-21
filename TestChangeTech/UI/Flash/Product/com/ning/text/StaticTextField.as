package com.ning.text{
	import flash.text.TextField;

	public class StaticTextField extends TextField {
		public function StaticTextField(_autoSize:String, _wordWrap:Boolean, _embedFonts:Boolean, _antiAliasType:String) {
			this.selectable = false;
			this.autoSize = _autoSize;
			this.wordWrap = _wordWrap;
			this.embedFonts = _embedFonts;
			this.antiAliasType = _antiAliasType;
			this.text = "";
			this.htmlText = "";
		}
	}
}