package com.ethos.changetech.controls
{
	
	/**
	 * ...
	 * @author changhong
	 */
	public class TableVo 
	{
		private var _tableWidth:Number = 100;
		private var _columnsScale:Array = [100];  //或者 [30,30,40]
		private var _borderColor:Number = 0x000000;
		private var _borderThickness:Number = 1;
		private var _fontColor:Number = 0x000000;
		private var _fontSize:Number = 12;
		private var _dataProvider:Array = [];  //二维数组，每条元素都是一个Array
		
		public function TableVo():void {
			
		}
		
		public function get tableWidth():Number { return _tableWidth; }
		
		public function set tableWidth(value:Number):void 
		{
			if(!isNaN(value)){
				_tableWidth = value;
			}
		}
		
		public function get columnsScale():Array { return _columnsScale; }
		
		public function set columnsScale(value:Array):void 
		{
			_columnsScale = value;
		}
		
		public function get borderColor():Number { return _borderColor; }
		
		public function set borderColor(value:Number):void 
		{
			_borderColor = value;
		}
		
		public function get borderThickness():Number { return _borderThickness; }
		
		public function set borderThickness(value:Number):void 
		{
			_borderThickness = value;
		}
		
		public function get fontColor():Number { return _fontColor; }
		
		public function set fontColor(value:Number):void 
		{
			_fontColor = value;
		}
		
		public function get fontSize():Number { return _fontSize; }
		
		public function set fontSize(value:Number):void 
		{
			_fontSize = value;
		}
		
		public function get dataProvider():Array { return _dataProvider; }
		
		public function set dataProvider(value:Array):void 
		{
			_dataProvider = value;
		}

	}
	
}