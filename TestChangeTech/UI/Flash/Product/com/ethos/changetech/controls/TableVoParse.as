package com.ethos.changetech.controls
{
	
	/**
	 * ...
	 * @author changhong
	 */
	public class TableVoParse 
	{
		public function TableVoParse():void {
			initClass();
		}
		
		private function initClass():void {
			
		}

		/*
			将一段字符串解析为 TableVo
			格式如下:
				<table width="200px" columns="20%,20%,20%,30%,10%" color="0x990000" thickness="1px" fontColor="0x000000" fontSize="12px">abc,abc,1,abc,2;bcd,bcd,2,bcd,3;cde,cde,3,cde,4;</table>
		*/
		public static function parseData(str:String):TableVo {
			try{
				var tableStr = str;
				trace(tableStr);
				tableStr = tableStr.replace(new RegExp("'","gm"),'"'); 
				trace(tableStr);
				//trace(tableStr.indexOf("["));
				//while(tableStr.indexOf("[")>=0)
				//{
					//tableStr = tableStr.replace("[","<");
				//}
				//trace(tableStr);
				//tableStr = tableStr.replace(new RegExp("]","gm"),'>');
				//trace(tableStr);
				var xml:XML = new XML(tableStr);
				var tempTv:TableVo = new TableVo();
				tempTv.tableWidth = getNumber(xml.@width, "px");
				tempTv.borderColor = Number(xml.@color);
				tempTv.borderThickness = getNumber(xml.@thickness, "px");
				tempTv.fontColor = Number(xml.@fontColor);
				tempTv.fontSize = getNumber(xml.@fontSize, "px");
				tempTv.columnsScale = xml.@columns.split(",").map(getNewArray);
				tempTv.dataProvider = xml.toString().split(";").map(getNewData);
			}catch (e) {
				throw new Error("wrong data format。");
			}
			return tempTv;
		}
		
		private static function getNewArray(element:*, index:int, arr:Array):Number {
            return getNumber(element, "%");
        }

		private static function getNewData(element:*, index:int, arr:Array):Array {
            return element.split(",");
        }
		
		private static function getNumber(value:String, str:String):Number {
			var regStr='^([0-9]+)'+str;
			var reg:RegExp = new RegExp(regStr);
			if (reg.test(value)) {
				return reg.exec(value)[1];
			}else {
				return NaN;
			}
		}
	}
	
}