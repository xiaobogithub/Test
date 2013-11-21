package com.ethos.changetech.controls
{
	import flash.display.Sprite;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	import flash.text.TextFormat;
	import flash.text.TextFormatAlign;
	import flash.text.AntiAliasType;
	
	/**
	 * ...
	 * @author changhong
	 */
	public class Table extends Sprite 
	{
		private var _dataProvider:TableVo;
		private var _tfm:TextFormat;
		private var _lineSprite:Sprite;
		private var _heightArray:Array;
		public function Table(tableVo:TableVo = null):void {
			initClass();
			if(tableVo){
				setData(tableVo);
			}
		}
		
		private function initClass():void {
			//do nothing.
		}
		
		public function setData(tableVo:TableVo):void {
			_dataProvider = tableVo;
			clearAll();
			initTextFormat();
			initTextField();
			initTableLine();
		}
		
		private function initTextFormat():void {
			_tfm = new TextFormat();
			_tfm.font = "Arial";
			_tfm.size = _dataProvider.fontSize;
			_tfm.color = _dataProvider.fontColor;
			_tfm.align = TextFormatAlign.LEFT;
		}
		
		private function initTextField():void {
			var kuan:Number = _dataProvider.tableWidth;
			var hang:Number = _dataProvider.dataProvider.length;
			var lie:Number = _dataProvider.dataProvider[0].length;
			var tempY:Number = -1;
			_heightArray = new Array();
			for (var i:int = 0; i < hang; i++ ) {
				var tempX:Number = 0;
				var changeY:Number = 0;
				for (var j:int = 0; j < lie; j++ ) {
					var tf:TextField = new TextField();
					tf.autoSize = TextFieldAutoSize.LEFT;
					tf.multiline = true;
					tf.wordWrap = true;
					tf.embedFonts = true;
					tf.antiAliasType = AntiAliasType.ADVANCED;
					tf.width = kuan * _dataProvider.columnsScale[j] / 100;
					tf.defaultTextFormat = _tfm;
					tf.htmlText = _dataProvider.dataProvider[i][j];
					tf.x = tempX;
					tf.y = tempY;
					tempX += tf.width;
					addChild(tf);
					changeY = Math.max(changeY, tf.height);
				}
				tempY += changeY;
				_heightArray.push(tempY);
				changeY = 0;
			}
		}
		
		private function initTableLine():void {
			_lineSprite = new Sprite();
			addChild(_lineSprite);
			_lineSprite.graphics.lineStyle(_dataProvider.borderThickness, _dataProvider.borderColor, 1);
			var kuan:Number = _dataProvider.tableWidth;
			var gao:Number = _heightArray[_heightArray.length - 1];
			var hang:Number = _dataProvider.dataProvider.length;
			var lie:Number = _dataProvider.dataProvider[0].length;
			_lineSprite.graphics.lineTo(kuan, 0);
			_lineSprite.graphics.moveTo(0, 0);
			_lineSprite.graphics.lineTo(0, gao);
			for (var i:int = 0; i < hang; i++ ) {
				_lineSprite.graphics.moveTo(0, _heightArray[i]);
				_lineSprite.graphics.lineTo(kuan, _heightArray[i]);
			}
			var tempX:Number = 0;
			for (var j:int = 0; j < lie; j++ ) {
				tempX += _dataProvider.columnsScale[j] * kuan / 100;
				_lineSprite.graphics.moveTo(tempX,0);
				_lineSprite.graphics.lineTo(tempX, gao);
			}
		}
		
		private function clearAll():void {
			var num:Number = numChildren;
			for (var i:int = 0; i < num; i++ ) {
				removeChildAt(0);
			}
		}
		
		public function get dataProvider():TableVo { return _dataProvider; }
		
		public function set dataProvider(value:TableVo):void 
		{
			_dataProvider = value;
			setData(_dataProvider);
		}
	}
	
}