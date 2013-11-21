package com.ning.text{
	import flash.text.TextFormat;

	public class SimpleFormat extends TextFormat {
		public function SimpleFormat(_align:String, _bold:Object, _color:uint, _font:String, _size:Number) {
			//super();
			this.align = _align;
			this.bold = _bold;
			this.color = _color;
			this.font = _font;
			this.size = _size;			
		}
	}
}