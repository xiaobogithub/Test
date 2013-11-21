//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {
    import flash.geom.*;
    import flash.media.*;
    import flash.filters.*;

    public class SpecialPropertiesDefault {

        public function SpecialPropertiesDefault(){
            trace("SpecialProperties is a static class and should not be instantiated.");
        }
        public static function _sound_volume_get(_arg1:Object):Number{
            return (_arg1.soundTransform.volume);
        }
        public static function _color_splitter(_arg1, _arg2:Array):Array{
            var _local3:Array = new Array();
            if (_arg1 == null){
                _local3.push({name:"_color_ra", value:1});
                _local3.push({name:"_color_rb", value:0});
                _local3.push({name:"_color_ga", value:1});
                _local3.push({name:"_color_gb", value:0});
                _local3.push({name:"_color_ba", value:1});
                _local3.push({name:"_color_bb", value:0});
            } else {
                _local3.push({name:"_color_ra", value:0});
                _local3.push({name:"_color_rb", value:AuxFunctions.numberToR(_arg1)});
                _local3.push({name:"_color_ga", value:0});
                _local3.push({name:"_color_gb", value:AuxFunctions.numberToG(_arg1)});
                _local3.push({name:"_color_ba", value:0});
                _local3.push({name:"_color_bb", value:AuxFunctions.numberToB(_arg1)});
            };
            return (_local3);
        }
        public static function frame_get(_arg1:Object):Number{
            return (_arg1.currentFrame);
        }
        public static function _sound_pan_get(_arg1:Object):Number{
            return (_arg1.soundTransform.pan);
        }
        public static function _color_property_get(_arg1:Object, _arg2:Array):Number{
            return (_arg1.transform.colorTransform[_arg2[0]]);
        }
        public static function _sound_volume_set(_arg1:Object, _arg2:Number):void{
            var _local3:SoundTransform = _arg1.soundTransform;
            _local3.volume = _arg2;
            _arg1.soundTransform = _local3;
        }
        public static function _autoAlpha_get(_arg1:Object):Number{
            return (_arg1.alpha);
        }
        public static function _filter_splitter(_arg1:BitmapFilter, _arg2:Array):Array{
            var _local3:Array = new Array();
            if ((_arg1 is BlurFilter)){
                _local3.push({name:"_blur_blurX", value:BlurFilter(_arg1).blurX});
                _local3.push({name:"_blur_blurY", value:BlurFilter(_arg1).blurY});
                _local3.push({name:"_blur_quality", value:BlurFilter(_arg1).quality});
            } else {
                trace("??");
            };
            return (_local3);
        }
        public static function init():void{
            Tweener.registerSpecialProperty("_frame", frame_get, frame_set);
            Tweener.registerSpecialProperty("_sound_volume", _sound_volume_get, _sound_volume_set);
            Tweener.registerSpecialProperty("_sound_pan", _sound_pan_get, _sound_pan_set);
            Tweener.registerSpecialProperty("_color_ra", _color_property_get, _color_property_set, ["redMultiplier"]);
            Tweener.registerSpecialProperty("_color_rb", _color_property_get, _color_property_set, ["redOffset"]);
            Tweener.registerSpecialProperty("_color_ga", _color_property_get, _color_property_set, ["greenMultiplier"]);
            Tweener.registerSpecialProperty("_color_gb", _color_property_get, _color_property_set, ["greenOffset"]);
            Tweener.registerSpecialProperty("_color_ba", _color_property_get, _color_property_set, ["blueMultiplier"]);
            Tweener.registerSpecialProperty("_color_bb", _color_property_get, _color_property_set, ["blueOffset"]);
            Tweener.registerSpecialProperty("_color_aa", _color_property_get, _color_property_set, ["alphaMultiplier"]);
            Tweener.registerSpecialProperty("_color_ab", _color_property_get, _color_property_set, ["alphaOffset"]);
            Tweener.registerSpecialProperty("_autoAlpha", _autoAlpha_get, _autoAlpha_set);
            Tweener.registerSpecialPropertySplitter("_color", _color_splitter);
            Tweener.registerSpecialPropertySplitter("_colorTransform", _colorTransform_splitter);
            Tweener.registerSpecialPropertySplitter("_scale", _scale_splitter);
            Tweener.registerSpecialProperty("_blur_blurX", _filter_property_get, _filter_property_set, [BlurFilter, "blurX"]);
            Tweener.registerSpecialProperty("_blur_blurY", _filter_property_get, _filter_property_set, [BlurFilter, "blurY"]);
            Tweener.registerSpecialProperty("_blur_quality", _filter_property_get, _filter_property_set, [BlurFilter, "quality"]);
            Tweener.registerSpecialPropertySplitter("_filter", _filter_splitter);
            Tweener.registerSpecialPropertyModifier("_bezier", _bezier_modifier, _bezier_get);
        }
        public static function _sound_pan_set(_arg1:Object, _arg2:Number):void{
            var _local3:SoundTransform = _arg1.soundTransform;
            _local3.pan = _arg2;
            _arg1.soundTransform = _local3;
        }
        public static function _color_property_set(_arg1:Object, _arg2:Number, _arg3:Array):void{
            var _local4:ColorTransform = _arg1.transform.colorTransform;
            _local4[_arg3[0]] = _arg2;
            _arg1.transform.colorTransform = _local4;
        }
        public static function _filter_property_get(_arg1:Object, _arg2:Array):Number{
            var _local4:uint;
            var _local7:Object;
            var _local3:Array = _arg1.filters;
            var _local5:Object = _arg2[0];
            var _local6:String = _arg2[1];
            _local4 = 0;
            while (_local4 < _local3.length) {
                if ((((_local3[_local4] is BlurFilter)) && ((_local5 == BlurFilter)))){
                    return (_local3[_local4][_local6]);
                };
                _local4++;
            };
            switch (_local5){
                case BlurFilter:
                    _local7 = {blurX:0, blurY:0, quality:NaN};
                    break;
            };
            return (_local7[_local6]);
        }
        public static function _bezier_get(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Array):Number{
            var _local5:uint;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            if (_arg4.length == 1){
                return ((_arg1 + (_arg3 * (((2 * (1 - _arg3)) * (_arg4[0] - _arg1)) + (_arg3 * (_arg2 - _arg1))))));
            };
            _local5 = Math.floor((_arg3 * _arg4.length));
            _local6 = ((_arg3 - (_local5 * (1 / _arg4.length))) * _arg4.length);
            if (_local5 == 0){
                _local7 = _arg1;
                _local8 = ((_arg4[0] + _arg4[1]) / 2);
            } else {
                if (_local5 == (_arg4.length - 1)){
                    _local7 = ((_arg4[(_local5 - 1)] + _arg4[_local5]) / 2);
                    _local8 = _arg2;
                } else {
                    _local7 = ((_arg4[(_local5 - 1)] + _arg4[_local5]) / 2);
                    _local8 = ((_arg4[_local5] + _arg4[(_local5 + 1)]) / 2);
                };
            };
            return ((_local7 + (_local6 * (((2 * (1 - _local6)) * (_arg4[_local5] - _local7)) + (_local6 * (_local8 - _local7))))));
        }
        public static function frame_set(_arg1:Object, _arg2:Number):void{
            _arg1.gotoAndStop(Math.round(_arg2));
        }
        public static function _filter_property_set(_arg1:Object, _arg2:Number, _arg3:Array):void{
            var _local5:uint;
            var _local8:BitmapFilter;
            var _local4:Array = _arg1.filters;
            var _local6:Object = _arg3[0];
            var _local7:String = _arg3[1];
            _local5 = 0;
            while (_local5 < _local4.length) {
                if ((((_local4[_local5] is BlurFilter)) && ((_local6 == BlurFilter)))){
                    _local4[_local5][_local7] = _arg2;
                    _arg1.filters = _local4;
                    return;
                };
                _local5++;
            };
            if (_local4 == null){
                _local4 = new Array();
            };
            switch (_local6){
                case BlurFilter:
                    _local8 = new BlurFilter(0, 0);
                    break;
            };
            _local8[_local7] = _arg2;
            _local4.push(_local8);
            _arg1.filters = _local4;
        }
        public static function _autoAlpha_set(_arg1:Object, _arg2:Number):void{
            _arg1.alpha = _arg2;
            _arg1.visible = (_arg2 > 0);
        }
        public static function _scale_splitter(_arg1:Number, _arg2:Array):Array{
            var _local3:Array = new Array();
            _local3.push({name:"scaleX", value:_arg1});
            _local3.push({name:"scaleY", value:_arg1});
            return (_local3);
        }
        public static function _colorTransform_splitter(_arg1, _arg2:Array):Array{
            var _local3:Array = new Array();
            if (_arg1 == null){
                _local3.push({name:"_color_ra", value:1});
                _local3.push({name:"_color_rb", value:0});
                _local3.push({name:"_color_ga", value:1});
                _local3.push({name:"_color_gb", value:0});
                _local3.push({name:"_color_ba", value:1});
                _local3.push({name:"_color_bb", value:0});
            } else {
                if (_arg1.ra != undefined){
                    _local3.push({name:"_color_ra", value:_arg1.ra});
                };
                if (_arg1.rb != undefined){
                    _local3.push({name:"_color_rb", value:_arg1.rb});
                };
                if (_arg1.ga != undefined){
                    _local3.push({name:"_color_ba", value:_arg1.ba});
                };
                if (_arg1.gb != undefined){
                    _local3.push({name:"_color_bb", value:_arg1.bb});
                };
                if (_arg1.ba != undefined){
                    _local3.push({name:"_color_ga", value:_arg1.ga});
                };
                if (_arg1.bb != undefined){
                    _local3.push({name:"_color_gb", value:_arg1.gb});
                };
                if (_arg1.aa != undefined){
                    _local3.push({name:"_color_aa", value:_arg1.aa});
                };
                if (_arg1.ab != undefined){
                    _local3.push({name:"_color_ab", value:_arg1.ab});
                };
            };
            return (_local3);
        }
        public static function _bezier_modifier(_arg1):Array{
            var _local3:Array;
            var _local4:uint;
            var _local5:String;
            var _local2:Array = [];
            if ((_arg1 is Array)){
                _local3 = _arg1;
            } else {
                _local3 = [_arg1];
            };
            var _local6:Object = {};
            _local4 = 0;
            while (_local4 < _local3.length) {
                for (_local5 in _local3[_local4]) {
                    if (_local6[_local5] == undefined){
                        _local6[_local5] = [];
                    };
                    _local6[_local5].push(_local3[_local4][_local5]);
                };
                _local4++;
            };
            for (_local5 in _local6) {
                _local2.push({name:_local5, parameters:_local6[_local5]});
            };
            return (_local2);
        }

    }
}//package caurina.transitions 
