//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import mx.containers.*;
    import mx.utils.*;

    public class GradientCanvas extends Canvas {

        private var _boffsetx:Number
        private var _boffsety:Number
        private var _offsetx:Array
        private var _offsety:Array
        private var _colorsconfig:Array
        private var _spreadMethod:String
        private var _bspreadMethod:String
        private var _bottomRightRadius:Number
        private var _topLeftRadius:Number
        private var _ngradients:Number
        private var _bgradientRatio:Array
        private var _gradientRatio:Array
        private var _bangle:Number
        private var _angle:Array
        private var _fillAlphas:Array
        private var _topRightRadius:Number
        private var _bfillAlphas:Array
        private var _bfocalPointRatio:Number
        private var _focalPointRatio:Array
        private var _binterpolationMethod:String
        private var _bottomLeftRadius:Number
        private var _interpolationMethod:String
        private var _gradientType:String
        private var _bgradientType:String
        private var _borderThickness:Number
        private var bStylePropChanged:Boolean = true
        private var _fillColors:Array
        private var _bfillColors:Array

        public static const DEFAULT_CORNERRADIUS:Number = 0;
        public static const DEFAULT_OFFSETX:Number = 0;
        public static const DEFAULT_OFFSETY:Number = 0;
        public static const DEFAULT_BORDERTHICKNESS:Number = 0;
        public static const DEFAULT_GRADIENTTYPE:String = "linear";
        public static const DEFAULT_FILLCOLORS:String = "0xFFFFFF";
        public static const DEFAULT_INTERPOLATIONMETHOD:String = "rgb";
        public static const DEFAULT_COLORSCONFIGURATION:Number = 1;
        public static const DEFAULT_BOTTOMLEFTRADIUS:Number = 0;
        public static const DEFAULT_ANGLE:Number = 0;
        public static const DEFAULT_GRADIENTRATIO:Number = 0xFF;
        public static const DEFAULT_TOPRIGHTRADIUS:Number = 0;
        public static const DEFAULT_BOTTOMRIGHTRADIUS:Number = 0;
        public static const DEFAULT_TOPLEFTRADIUS:Number = 0;
        public static const DEFAULT_FOCALPOINTRATIO:Number = 0;
        public static const DEFAULT_SPREADMETHOD:String = "pad";
        public static const DEFAULT_FILLALPHAS:Number = 1;

        public function GradientCanvas(){
            _colorsconfig = new Array(1);
            _ngradients = _colorsconfig.length;
            super();
        }
        private function getAllStyles():void{
            var _local1:Array;
            var _local2:Number;
            var _local3:Number;
            var _local4:Array;
            if (getStyle("gradientType") != undefined){
                _gradientType = getStyle("gradientType");
            } else {
                _gradientType = DEFAULT_GRADIENTTYPE;
            };
            if (getStyle("bordergradientType") != undefined){
                _bgradientType = getStyle("bordergradientType");
            } else {
                _bgradientType = DEFAULT_GRADIENTTYPE;
            };
            if (getStyle("colorsConfiguration") != undefined){
                _colorsconfig = ArrayUtil.toArray(getStyle("colorsConfiguration"));
            } else {
                _ngradients = _colorsconfig.length;
            };
            if (getStyle("fillColors") != undefined){
                _fillColors = new Array();
                _local1 = new Array();
                _local1 = getStyle("fillColors").toString().split(",");
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _fillColors.push(_local1.slice(0, _colorsconfig[_local2]));
                    _local1 = _local1.slice(_colorsconfig[_local2]);
                    _local2++;
                };
            } else {
                _fillColors = new Array(DEFAULT_FILLCOLORS);
            };
            if (getStyle("borderColors") != undefined){
                _bfillColors = getStyle("borderColors");
            } else {
                _bfillColors = new Array(DEFAULT_FILLCOLORS);
            };
            if (getStyle("borderThickness") != undefined){
                _borderThickness = getStyle("borderThickness");
            } else {
                _borderThickness = DEFAULT_BORDERTHICKNESS;
            };
            if (getStyle("fillAlphas") != undefined){
                _fillAlphas = new Array();
                _local1 = new Array();
                _local1 = getStyle("fillAlphas").toString().split(",");
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _fillAlphas.push(_local1.slice(0, _colorsconfig[_local2]));
                    _local1 = _local1.slice(_colorsconfig[_local2]);
                    _local2++;
                };
            } else {
                _fillAlphas = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _local1 = new Array();
                    _local3 = 0;
                    while (_local3 < _colorsconfig[_local2]) {
                        _local1.push(DEFAULT_FILLALPHAS);
                        _local3++;
                    };
                    _fillAlphas.push(_local1);
                    _local2++;
                };
            };
            if (getStyle("borderAlphas") != undefined){
                _bfillAlphas = getStyle("borderAlphas");
            } else {
                _bfillAlphas = new Array();
                _local2 = 0;
                while (_local2 < _bfillColors.length) {
                    _bfillAlphas.push(DEFAULT_FILLALPHAS);
                    _local2++;
                };
            };
            if (getStyle("gradientRatio") != undefined){
                _gradientRatio = getStyle("gradientRatio");
                _local1 = new Array();
                _local1 = getStyle("gradientRatio").toString().split(",");
                _local4 = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _local4.push(_local1.slice(0, _colorsconfig[_local2]));
                    _local1 = _local1.slice(_colorsconfig[_local2]);
                    _local2++;
                };
                _gradientRatio = _local4;
            } else {
                _gradientRatio = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _gradientRatio.push(generateDefaultRatio(_colorsconfig[_local2]));
                    _local2++;
                };
            };
            if (getStyle("bordergradientRatio") != undefined){
                _bgradientRatio = getStyle("bordergradientRatio");
            } else {
                _bgradientRatio = new Array();
                _local2 = 0;
                while (_local2 < _bfillColors.length) {
                    _bgradientRatio = generateDefaultRatio(_bfillColors.length);
                    _local2++;
                };
            };
            if (getStyle("angle") != undefined){
                _local1 = new Array();
                _local1.push(getStyle("angle"));
                _angle = _local1[0].toString().split(",");
            } else {
                _angle = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _angle.push(DEFAULT_ANGLE);
                    _local2++;
                };
            };
            if (getStyle("borderAngle") != undefined){
                _bangle = getStyle("borderAngle");
            } else {
                _bangle = DEFAULT_ANGLE;
            };
            if (getStyle("spreadMethod") != undefined){
                _spreadMethod = getStyle("spreadMethod");
            } else {
                _spreadMethod = DEFAULT_SPREADMETHOD;
            };
            if (getStyle("borderSpreadMethod") != undefined){
                _bspreadMethod = getStyle("borderSpreadMethod");
            } else {
                _bspreadMethod = DEFAULT_SPREADMETHOD;
            };
            if (getStyle("interpolationMethod") != undefined){
                _interpolationMethod = getStyle("interpolationMethod");
            } else {
                _interpolationMethod = DEFAULT_INTERPOLATIONMETHOD;
            };
            if (getStyle("borderInterpolationMethod") != undefined){
                _binterpolationMethod = getStyle("borderInterpolationMethod");
            } else {
                _binterpolationMethod = DEFAULT_INTERPOLATIONMETHOD;
            };
            if (getStyle("focalPointRatio") != undefined){
                _focalPointRatio = new Array();
                _local1 = new Array();
                _local1.push(getStyle("focalPointRatio"));
                _focalPointRatio = _local1[0].toString().split(",");
            } else {
                _focalPointRatio = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _focalPointRatio.push(DEFAULT_FOCALPOINTRATIO);
                    _local2++;
                };
            };
            if (getStyle("borderfocalPointRatio") != undefined){
                _bfocalPointRatio = getStyle("borderfocalPointRatio");
            } else {
                _bfocalPointRatio = DEFAULT_FOCALPOINTRATIO;
            };
            if (getStyle("offsetX") != undefined){
                _local1 = new Array();
                _local1.push(getStyle("offsetX"));
                _offsetx = _local1[0].toString().split(",");
            } else {
                _offsetx = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _offsetx.push(DEFAULT_OFFSETX);
                    _local2++;
                };
            };
            if (getStyle("borderOffsetX") != undefined){
                _boffsetx = getStyle("borderOffsetX");
            } else {
                _boffsetx = DEFAULT_OFFSETX;
            };
            if (getStyle("offsetY") != undefined){
                _local1 = new Array();
                _local1.push(getStyle("offsetY"));
                _offsety = _local1[0].toString().split(",");
            } else {
                _offsety = new Array();
                _local2 = 0;
                while (_local2 < _colorsconfig.length) {
                    _offsety.push(DEFAULT_OFFSETY);
                    _local2++;
                };
            };
            if (getStyle("borderOffsetY") != undefined){
                _boffsety = getStyle("borderOffsetY");
            } else {
                _boffsety = DEFAULT_OFFSETY;
            };
            if (((((((((!((getStyle("topLeftRadius") == 0))) && (!((getStyle("topLeftRadius") == undefined))))) || (((!((getStyle("topRightRadius") == 0))) && (!((getStyle("topRightRadius") == undefined))))))) || (((!((getStyle("bottomLeftRadius") == 0))) && (!((getStyle("bottomLeftRadius") == undefined))))))) || (((!((getStyle("bottomRightRadius") == 0))) && (!((getStyle("bottomRightRadius") == undefined))))))){
                if (getStyle("topLeftRadius") != undefined){
                    _topLeftRadius = getStyle("topLeftRadius");
                } else {
                    _topLeftRadius = DEFAULT_TOPLEFTRADIUS;
                };
                if (getStyle("topRightRadius") != undefined){
                    _topRightRadius = getStyle("topRightRadius");
                } else {
                    _topRightRadius = DEFAULT_TOPRIGHTRADIUS;
                };
                if (getStyle("bottomLeftRadius") != undefined){
                    _bottomLeftRadius = getStyle("bottomLeftRadius");
                } else {
                    _bottomLeftRadius = DEFAULT_BOTTOMLEFTRADIUS;
                };
                if (getStyle("bottomRightRadius") != undefined){
                    _bottomRightRadius = getStyle("bottomRightRadius");
                } else {
                    _bottomRightRadius = DEFAULT_BOTTOMRIGHTRADIUS;
                };
            } else {
                _topLeftRadius = getStyle("cornerRadius");
                _topRightRadius = getStyle("cornerRadius");
                _bottomLeftRadius = getStyle("cornerRadius");
                _bottomRightRadius = getStyle("cornerRadius");
            };
        }
        override public function styleChanged(_arg1:String):void{
            super.styleChanged(_arg1);
            if ((((((((((((((((((((((((((((((((((((((((((((((((((((((_arg1 == "fillColors")) || ((_arg1 == "fillAlphas")))) || ((_arg1 == "cornerRadius")))) || ((_arg1 == "angle")))) || ((_arg1 == "spreadMethod")))) || ((_arg1 == "gradientType")))) || ((_arg1 == "gradientRatio")))) || ((_arg1 == "offsetX")))) || ((_arg1 == "offsetY")))) || ((_arg1 == "interpolationMethod")))) || ((_arg1 == "topLeftRadius")))) || ((_arg1 == "topRightRadius")))) || ((_arg1 == "bottomLeftRadius")))) || ((_arg1 == "bottomRightRadius")))) || ((_arg1 == "focalPointRatio")))) || ((_arg1 == "gradientType")))) || ((_arg1 == "borderColors")))) || ((_arg1 == "borderAlphas")))) || ((_arg1 == "bordergradientRatio")))) || ((_arg1 == "borderThickness")))) || ((_arg1 == "borderOffsetX")))) || ((_arg1 == "borderOffsetY")))) || ((_arg1 == "borderfocalPointRatio")))) || ((_arg1 == "borderSpreadMethod")))) || ((_arg1 == "borderInterpolationMethod")))) || ((_arg1 == "borderAngle")))) || ((_arg1 == "colorsConfiguration")))){
                bStylePropChanged = true;
                invalidateDisplayList();
                return;
            };
        }
        private function drawRect(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Object=null, _arg6:Object=null, _arg7:Object=null, _arg8:Object=null, _arg9:String=null, _arg10:Array=null, _arg11:String=null, _arg12:String=null, _arg13:Number=0, _arg14:Object=null):void{
            if (((!(_arg3)) || (!(_arg4)))){
                return;
            };
            var _local15:Graphics = graphics;
            _local15.beginGradientFill(_arg9, (_arg6 as Array), (_arg7 as Array), _arg10, (_arg8 as Matrix), _arg11, _arg12, _arg13);
            _local15.drawRoundRectComplex(_arg1, _arg2, _arg3, _arg4, _arg5.tl, _arg5.tr, _arg5.bl, _arg5.br);
            if (_arg14){
                _local15.drawRoundRectComplex(_arg14.x, _arg14.y, _arg14.w, _arg14.h, _arg5.tl, _arg5.tr, _arg5.bl, _arg5.br);
            };
            _local15.endFill();
        }
        public function set colorsConfiguration(_arg1:Array):void{
            _colorsconfig = _arg1;
            invalidateDisplayList();
        }
        private function generateDefaultRatio(_arg1:Number):Array{
            var _local5:Number;
            var _local2:Number = (0xFF / (_arg1 - 1));
            var _local3:Array = new Array();
            var _local4:Number = 0;
            while (_local4 < _arg1) {
                _local5 = (0xFF - (_local2 * _local4));
                _local3.push(_local5);
                _local3.sort(Array.NUMERIC);
                _local4++;
            };
            return (_local3);
        }
        public function get colorsConfiguration():Array{
            return (_colorsconfig);
        }
        public function get numberGradients():Number{
            return (_colorsconfig.length);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local3:Graphics;
            var _local4:Object;
            var _local5:Number;
            var _local6:Matrix;
            var _local7:Matrix;
            var _local8:Object;
            super.updateDisplayList(_arg1, _arg2);
            if (bStylePropChanged == true){
                getAllStyles();
                _local3 = graphics;
                _local3.clear();
                _local4 = {tl:_topLeftRadius, tr:_topRightRadius, bl:_bottomLeftRadius, br:_bottomRightRadius};
                _local5 = 0;
                while (_local5 < _colorsconfig.length) {
                    _local6 = new Matrix();
                    _local6.createGradientBox(_arg1, _arg2, ((Math.PI * _angle[_local5]) / 180), _offsetx[_local5], _offsety[_local5]);
                    drawRect(0, 0, _arg1, _arg2, _local4, _fillColors[_local5], _fillAlphas[_local5], _local6, _gradientType.split(",")[_local5], _gradientRatio[_local5], _spreadMethod.split(",")[_local5], _interpolationMethod.split(",")[_local5], _focalPointRatio[_local5]);
                    _local5++;
                };
                if (_borderThickness > 0){
                    _local7 = new Matrix();
                    _local7.createGradientBox((_arg1 + (2 * _borderThickness)), (_arg2 + (2 * _borderThickness)), ((Math.PI * _bangle) / 180), _boffsetx, _boffsety);
                    _local8 = new Object();
                    _local8.x = 0;
                    _local8.y = 0;
                    _local8.w = _arg1;
                    _local8.h = _arg2;
                    drawRect(-(_borderThickness), -(_borderThickness), (_arg1 + (2 * _borderThickness)), (_arg2 + (2 * _borderThickness)), _local4, _bfillColors, _bfillAlphas, _local7, _bgradientType, _bgradientRatio, _bspreadMethod, _binterpolationMethod, _bfocalPointRatio, _local8);
                };
            };
        }

    }
}//package com.redbox.changetech.view.components 
