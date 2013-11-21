package com.ethos.changetech.controls{
	import com.ning.data.GlobalValue;
	import flash.display.BlendMode;
	import flash.display.GradientType;
	import flash.display.SpreadMethod;
	import flash.display.Sprite;
	import flash.geom.Matrix;
	import flash.geom.Rectangle;

	public class Background extends Sprite {

		private var _backgroundColor:uint;
		private var _topBarHeight:uint;
		private var _topBarColor:uint = 0x5CAB3C;
		// Top shadow
		private var _topShadowColors:Array;
		private var _topShadowAlphas:Array;
		private var _topShadowRatios:Array;
		private var _topShadowHeight:Number;
		// Top corners
		private var _topCornerColors:Array;
		private var _topCornerAlphas:Array;
		private var _topCornerRatios:Array;
		private var _topCornerGradientWidth:Number;
		private var _topCornerGradientHeight:Number;
		private var _topCornerWidth:Number;
		private var _topCornerHeight:Number;
		// Bottom shadow
		private var _bottomShadowColors:Array;
		private var _bottomShadowAlphas:Array;
		private var _bottomShadowRatios:Array;
		private var _bottomShadowHeight:Number;
		// Bottom corners
		private var _bottomCornerColors:Array;
		private var _bottomCornerAlphas:Array;
		private var _bottomCornerRatios:Array;
		private var _bottomCornerGradientWidth:Number;
		private var _bottomCornerGradientHeight:Number;
		private var _bottomCornerWidth:Number;
		private var _bottomCornerHeight:Number;

		public function Background() {
			cacheAsBitmap = true;
			//blendMode = BlendMode.MULTIPLY;
			_backgroundColor = GlobalValue.getValue("layout")["BackgroundColor"];
			_topBarColor = GlobalValue.getValue("layout")["TopBarColor"];
			_topBarHeight = GlobalValue.getValue("layout")["TopBarHeight"];
			_topShadowColors = String(GlobalValue.getValue("layout")["BackgroundTopShadowColors"]).split(",");
			_topShadowAlphas = String(GlobalValue.getValue("layout")["BackgroundTopShadowAlphas"]).split(",");
			_topShadowRatios = String(GlobalValue.getValue("layout")["BackgroundTopShadowRatios"]).split(",");
			_topShadowHeight = GlobalValue.getValue("layout")["BackgroundTopShadowHeight"];
			_topCornerColors = String(GlobalValue.getValue("layout")["BackgroundTopCornerColors"]).split(",");
			_topCornerAlphas = String(GlobalValue.getValue("layout")["BackgroundTopCornerAlphas"]).split(",");
			_topCornerRatios = String(GlobalValue.getValue("layout")["BackgroundTopCornerRatios"]).split(",");
			_topCornerGradientWidth = GlobalValue.getValue("layout")["BackgroundTopCornerGradientWidth"];
			_topCornerGradientHeight = GlobalValue.getValue("layout")["BackgroundTopCornerGradientHeight"];
			_topCornerWidth = GlobalValue.getValue("layout")["BackgroundTopCornerWidth"];
			_topCornerHeight = GlobalValue.getValue("layout")["BackgroundTopCornerHeight"];
			_bottomShadowColors = String(GlobalValue.getValue("layout")["BackgroundBottomShadowColors"]).split(",");
			_bottomShadowAlphas = String(GlobalValue.getValue("layout")["BackgroundBottomShadowAlphas"]).split(",");
			_bottomShadowRatios = String(GlobalValue.getValue("layout")["BackgroundBottomShadowRatios"]).split(",");
			_bottomShadowHeight = GlobalValue.getValue("layout")["BackgroundBottomShadowHeight"];
			_bottomCornerColors = String(GlobalValue.getValue("layout")["BackgroundBottomCornerColors"]).split(",");
			_bottomCornerAlphas = String(GlobalValue.getValue("layout")["BackgroundBottomCornerAlphas"]).split(",");
			_bottomCornerRatios = String(GlobalValue.getValue("layout")["BackgroundBottomCornerRatios"]).split(",");
			_bottomCornerGradientWidth = GlobalValue.getValue("layout")["BackgroundBottomCornerGradientWidth"];
			_bottomCornerGradientHeight = GlobalValue.getValue("layout")["BackgroundBottomCornerGradientHeight"];
			_bottomCornerWidth = GlobalValue.getValue("layout")["BackgroundBottomCornerWidth"];
			_bottomCornerHeight = GlobalValue.getValue("layout")["BackgroundBottomCornerHeight"];
		}
		public function get topBarColor():uint {
			return _topBarColor;
		}
		public function set topBarColor(_value:uint):void {
			_topBarColor = _value;
		}
		public function draw(_stageWidth:Number, _stageHeight:Number):void {
			graphics.clear();
			drawBackgroundColor(_stageWidth, _stageHeight);
			drawBottomShadow(_stageWidth, _stageHeight);
			drawBottomCorners(_stageWidth, _stageHeight);	
			drawTopCorners(_stageWidth, _stageHeight);
			drawTopShadow(_stageWidth, _stageHeight);
			drawTopBar(_stageWidth, _stageHeight);					
		}
		private function drawBackgroundColor(_stageWidth:Number, _stageHeight:Number):void {
			graphics.beginFill(_backgroundColor);
			graphics.drawRect(0, 0, _stageWidth, _stageHeight);
			graphics.endFill();
		}
		private function drawTopShadow(_stageWidth:Number, _stageHeight:Number):void {
			var _topMatrix = new Matrix();
			_topMatrix.createGradientBox(_stageWidth, _topShadowHeight, Math.PI / 2);
			graphics.beginGradientFill(GradientType.LINEAR, _topShadowColors, _topShadowAlphas, _topShadowRatios, _topMatrix, SpreadMethod.PAD);
			graphics.drawRect(0, 0, _stageWidth, _topShadowHeight);
		}
		private function drawTopCorners(_stageWidth:Number, _stageHeight:Number):void {
			var _topLeftCornerMatrix = new Matrix();
			_topLeftCornerMatrix.createGradientBox(_topCornerGradientWidth, _topCornerGradientHeight, Math.PI / 3);
			graphics.beginGradientFill(GradientType.LINEAR, _topCornerColors, _topCornerAlphas, _topCornerRatios, _topLeftCornerMatrix, SpreadMethod.PAD);
			graphics.drawRect(0, 0, _topCornerWidth, _topCornerHeight);

			var _topRightCornerMatrix = new Matrix();
			_topRightCornerMatrix.createGradientBox(_topCornerGradientWidth, _topCornerGradientHeight, Math.PI * 2 / 3, _stageWidth - _topCornerGradientWidth, 0);
			graphics.beginGradientFill(GradientType.LINEAR, _topCornerColors, _topCornerAlphas, _topCornerRatios, _topRightCornerMatrix, SpreadMethod.PAD);
			graphics.drawRect(_stageWidth - _topCornerWidth, 0, _topCornerWidth, _topCornerHeight);
		}
		private function drawTopBar(_stageWidth:Number, _stageHeight:Number):void {
			graphics.beginFill(_topBarColor);
			graphics.drawRect(0, 0, _stageWidth, _topBarHeight);
			graphics.endFill();
		}
		private function drawBottomShadow(_stageWidth:Number, _stageHeight:Number):void {
			var _bottomMatrix = new Matrix();
			_bottomMatrix.createGradientBox(_stageWidth, _bottomShadowHeight, Math.PI / 2, 0, _stageHeight - _bottomShadowHeight);
			graphics.beginGradientFill(GradientType.LINEAR, _bottomShadowColors, _bottomShadowAlphas, _bottomShadowRatios, _bottomMatrix, SpreadMethod.PAD);
			graphics.drawRect(0, _stageHeight - _bottomShadowHeight, _stageWidth, _bottomShadowHeight);
		}
		private function drawBottomCorners(_stageWidth:Number, _stageHeight:Number):void {
			var _bottomLeftCornerMatrix = new Matrix();
			_bottomLeftCornerMatrix.createGradientBox(_bottomCornerGradientWidth, _bottomCornerGradientHeight, -Math.PI / 3, 0, _stageHeight - _bottomCornerGradientHeight);
			graphics.beginGradientFill(GradientType.LINEAR, _bottomCornerColors, _bottomCornerAlphas, _bottomCornerRatios, _bottomLeftCornerMatrix, SpreadMethod.PAD);
			graphics.drawRect(0, _stageHeight - _bottomCornerHeight, _bottomCornerWidth, _bottomCornerHeight);

			var _bottomRightCornerMatrix = new Matrix();
			_bottomRightCornerMatrix.createGradientBox(_bottomCornerGradientWidth, _bottomCornerGradientHeight, Math.PI * 4 / 3, _stageWidth - _bottomCornerGradientWidth, _stageHeight - _bottomCornerGradientHeight);
			graphics.beginGradientFill(GradientType.LINEAR, _bottomCornerColors, _bottomCornerAlphas, _bottomCornerRatios, _bottomRightCornerMatrix, SpreadMethod.PAD);
			graphics.drawRect(_stageWidth - _bottomCornerWidth, _stageHeight - _bottomCornerHeight, _bottomCornerWidth, _bottomCornerHeight);
		}

	}
}