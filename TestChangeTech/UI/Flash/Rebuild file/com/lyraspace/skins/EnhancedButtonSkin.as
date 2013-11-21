//Created by Action Script Viewer - http://www.buraks.com/asv
package com.lyraspace.skins {
    import flash.display.*;
    import mx.core.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.utils.*;
    import mx.skins.*;
    import mx.skins.halo.*;

    public class EnhancedButtonSkin extends Border {

        private static var cache:Object = {};

        override public function get measuredWidth():Number{
            return (UIComponent.DEFAULT_MEASURED_MIN_WIDTH);
        }
        override public function get measuredHeight():Number{
            return (UIComponent.DEFAULT_MEASURED_MIN_HEIGHT);
        }
        private function getColorRatios(_arg1:String, _arg2:Number):Array{
            var _local4:Number;
            var _local5:int;
            var _local3:Array = getStyle(_arg1);
            if (((!((_local3 == null))) && ((_local3.length == _arg2)))){
                return (_local3);
            };
            if (_arg1 != "fillColorRatios"){
                return (getColorRatios("fillColorRatios", _arg2));
            };
            _local4 = (0xFF / (_arg2 - 1));
            _local3 = [];
            _local5 = 0;
            while (_local5 < _arg2) {
                _local3[_local5] = Math.round((_local5 * _local4));
                _local5++;
            };
            return (_local3);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local36:Number;
            var _local37:Array;
            var _local38:Array;
            var _local39:Number;
            super.updateDisplayList(_arg1, _arg2);
            var _local3:Array = getStyle("cornerRadii");
            var _local4:Number = getStyle("cornerRadius");
            if (_local3 == null){
                if (isNaN(_local4)){
                    _local4 = 0;
                };
                _local3 = [_local4, _local4, _local4, _local4];
            };
            var _local5:Array = getStyle("fillColors");
            if (_local5.length == 1){
                _local5.push(_local5[0]);
            };
            var _local6:Array = _local5;
            var _local7:Array = getStyle("selectedFillColors");
            if (_local7 == null){
                _local7 = [_local5[1], _local5[1]];
            };
            var _local8:Array = getStyle("overFillColors");
            if (_local8 == null){
                _local8 = _local7;
            };
            var _local9:Array = getStyle("disabledFillColors");
            if (_local9 == null){
                _local9 = _local5;
            };
            var _local10:Array = getStyle("downFillColors");
            if (_local10 != null){
                _local37 = getColorRatios("downFillColorRatios", _local10.length);
                _local38 = getAlphas("downFillAlphas", _local10.length);
            };
            var _local11:Array = getColorRatios("fillColorRatios", _local5.length);
            var _local12:Array = getColorRatios("overFillColorRatios", _local8.length);
            var _local13:Array = getColorRatios("selectedFillColorRatios", _local7.length);
            var _local14:Array = getColorRatios("disabledFillColorRatios", _local9.length);
            var _local15:Array = getAlphas("fillAlphas", _local5.length);
            var _local16:Array = getAlphas("overFillAlphas", _local8.length);
            var _local17:Array = getAlphas("selectedFillAlphas", _local7.length);
            var _local18:Array = [];
            var _local19:int;
            while (_local19 < _local15.length) {
                _local18[_local19] = Math.max(0, (_local15[_local19] - 0.15));
                _local19++;
            };
            _local19 = _local15.length;
            while (_local19 < _local5.length) {
                _local15.push(_local15[(_local19 - 1)]);
                _local19++;
            };
            var _local20:Array = _local15;
            StyleManager.getColorNames(_local5);
            StyleManager.getColorNames(_local6);
            StyleManager.getColorNames(_local8);
            StyleManager.getColorNames(_local10);
            StyleManager.getColorNames(_local7);
            var _local21:Array = getStyle("highlightAlphas");
            var _local22:uint = getStyle("themeColor");
            var _local23:uint = getStyle("borderThickness");
            var _local24:Array = getStyle("borderColors");
            if (_local24 == null){
                _local39 = getStyle("borderColor");
                _local24 = [_local39, ColorUtil.adjustBrightness(_local39, -50)];
            };
            var _local25:Array = getStyle("overBorderColors");
            if (_local25 == null){
                _local25 = [_local22, ColorUtil.adjustBrightness2(_local22, -25)];
            };
            var _local26:Array = getStyle("downBorderColors");
            if (_local26 == null){
                _local26 = [_local22, ColorUtil.adjustBrightness2(_local22, -25)];
            };
            var _local27:Array = getStyle("selectedBorderColors");
            if (_local27 == null){
                _local27 = _local25;
            };
            var _local28:Number = getStyle("borderAlpha");
            if (isNaN(_local28)){
                _local28 = 1;
            };
            var _local29:Number = getStyle("disabledBorderAlpha");
            if (isNaN(_local29)){
                _local29 = (_local28 / 0.5);
            };
            var _local30:Object = calcDerivedStyles(_local22, _local5[0], _local5[1]);
            var _local31:Boolean;
            if ((parent is Button)){
                _local31 = Button(parent).emphasized;
            };
            var _local32:Object = radiiUtil(_local3, 0);
            var _local33:Object = radiiUtil(_local3, 1);
            var _local34:Object = radiiUtil(_local3, 2);
            var _local35:uint = _local23;
            graphics.clear();
            switch (name){
                case "selectedUpSkin":
                case "selectedOverSkin":
                    drawRoundRect(0, 0, _arg1, _arg2, _local32, _local27, _local28, verticalGradientMatrix(0, 0, _arg1, _arg2), GradientType.LINEAR, null, {x:(_local35 + 1), y:(_local35 + 1), w:((_arg1 - (2 * _local35)) - 2), h:((_arg2 - (2 * _local35)) - 2), r:_local34});
                    drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, _local7, _local17, verticalGradientMatrix(0, 0, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local13);
                    drawRoundRect((_local35 + 1), (_local35 + 1), ((_arg1 - (2 * _local35)) - 2), (((_arg2 - (2 * _local35)) - 2) / 2), {tl:_local34.tl, tr:_local34.tr, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local21, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2)));
                    break;
                case "upSkin":
                    if (_local31){
                        drawRoundRect(0, 0, _arg1, _arg2, _local32, _local24, _local28, verticalGradientMatrix(0, 0, _arg1, _arg2), GradientType.LINEAR, null, {x:(_local35 + 1), y:(_local35 + 1), w:((_arg1 - (2 * _local35)) - 2), h:((_arg2 - (2 * _local35)) - 2), r:_local34});
                        drawRoundRect((_local35 + 1), (_local35 + 1), ((_arg1 - (2 * _local35)) - 2), ((_arg2 - (2 * _local35)) - 2), _local34, _local6, _local20, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local11);
                        drawRoundRect((_local35 + 1), (_local35 + 1), ((_arg1 - (2 * _local35)) - 2), (((_arg2 - (2 * _local35)) - 2) / 2), {tl:_local34.tl, tr:_local34.tr, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local21, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2)));
                    } else {
                        drawRoundRect(0, 0, _arg1, _arg2, _local32, _local24, _local28, verticalGradientMatrix(0, 0, _arg1, _arg2), GradientType.LINEAR, null, {x:_local35, y:_local35, w:(_arg1 - (2 * _local35)), h:(_arg2 - (2 * _local35)), r:_local33});
                        drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, _local6, _local20, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local11);
                        drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2), {tl:_local33.tl, tr:_local33.tr, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local21, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2)));
                    };
                    break;
                case "overSkin":
                    drawRoundRect(0, 0, _arg1, _arg2, _local32, _local25, _local28, verticalGradientMatrix(0, 0, _arg1, _arg2), GradientType.LINEAR, null, {x:_local35, y:_local35, w:(_arg1 - (2 * _local35)), h:(_arg2 - (2 * _local35)), r:_local33});
                    drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, _local8, _local16, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local12);
                    drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2), {tl:_local33.tl, tr:_local33.tr, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local21, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2)));
                    break;
                case "downSkin":
                case "selectedDownSkin":
                    drawRoundRect(0, 0, _arg1, _arg2, _local32, _local25, _local28, verticalGradientMatrix(0, 0, _arg1, _arg2));
                    if (_local10 != null){
                        drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, _local10, _local38, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local37);
                    } else {
                        drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, [_local30.fillColorPress1, _local30.fillColorPress2], _local15[0], verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))));
                    };
                    drawRoundRect((_local35 + 1), (_local35 + 1), ((_arg1 - (2 * _local35)) - 2), (((_arg2 - (2 * _local35)) - 2) / 2), {tl:_local34.tl, tr:_local34.tr, bl:0, br:0}, [0xFFFFFF, 0xFFFFFF], _local21, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), ((_arg2 - (2 * _local35)) / 2)));
                    break;
                case "disabledSkin":
                case "selectedDisabledSkin":
                    drawRoundRect(0, 0, _arg1, _arg2, _local32, _local24, _local29, verticalGradientMatrix(0, 0, _arg1, _arg2), GradientType.LINEAR, null, {x:_local35, y:_local35, w:(_arg1 - (2 * _local35)), h:(_arg2 - (2 * _local35)), r:_local33});
                    drawRoundRect(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35)), _local33, _local9, _local18, verticalGradientMatrix(_local35, _local35, (_arg1 - (2 * _local35)), (_arg2 - (2 * _local35))), GradientType.LINEAR, _local14);
                    break;
            };
        }
        private function getAlphas(_arg1:String, _arg2:Number):Array{
            var _local4:int;
            var _local3:Array = getStyle(_arg1);
            if (((!((_local3 == null))) && ((_local3.length == _arg2)))){
                return (_local3);
            };
            if (_arg1 != "fillAlphas"){
                return (getAlphas("fillAlphas", _arg2));
            };
            _local3 = [];
            _local4 = 0;
            while (_local4 < _arg2) {
                _local3.push(0.5);
                _local4++;
            };
            return (_local3);
        }
        private function radiiUtil(_arg1:Array, _arg2:Number):Object{
            var _local3:Object = {};
            _local3.tl = Math.max(0, (_arg1[0] - _arg2));
            _local3.tr = Math.max(0, (_arg1[1] - _arg2));
            _local3.br = Math.max(0, (_arg1[2] - _arg2));
            _local3.bl = Math.max(0, (_arg1[3] - _arg2));
            return (_local3);
        }

        private static function calcDerivedStyles(_arg1:uint, _arg2:uint, _arg3:uint):Object{
            var _local5:Object;
            var _local4:String = HaloColors.getCacheKey(_arg1, _arg2, _arg3);
            if (!cache[_local4]){
                _local5 = (cache[_local4] = {});
                HaloColors.addHaloColors(_local5, _arg1, _arg2, _arg3);
            };
            return (cache[_local4]);
        }

    }
}//package com.lyraspace.skins 
