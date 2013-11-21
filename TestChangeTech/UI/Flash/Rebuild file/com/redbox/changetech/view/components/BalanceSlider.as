//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.binding.*;
    import mx.containers.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import caurina.transitions.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceSlider extends Canvas implements IBindingClient {

        public var _BalanceSlider_Label1:Array
        private var _1541031935_fillColors1:Array
        private var _1063571914textColor:Number = 14412460
        private var _2000515510numbers:Canvas
        private var _1541031936_fillColors2:Array
        private var _1649915028numberRep:Repeater
        private var _703731515_BalanceSlider_Canvas4:Canvas
        mx_internal var _watchers:Array
        private var _316393669_fillAlphas2:Array
        private var _58020982slider_mask:Canvas
        private var _316393670_fillAlphas1:Array
        private var _109526449slide:HSlider
        private var _94851343count:Number = 12
        private var _1921875508bgHeight:Number = 35
        private var _1019779949offset:Number = 5
        public var valueType:String
        mx_internal var _bindingsBeginWithWord:Object
        private var _sliderValue:Number
        mx_internal var _bindingsByDestination:Object
        public var _BalanceSlider_GradientCanvas1:GradientCanvas
        public var _BalanceSlider_GradientCanvas2:GradientCanvas
        private var _1649904333numberGap:Number = 40
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        private var _3141bg:Canvas

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceSlider(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"bg", propertiesFactory:function ():Object{
                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:GradientCanvas, id:"_BalanceSlider_GradientCanvas1", stylesFactory:function ():void{
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, percentHeight:50, colorsConfiguration:[2]});
                    }}), new UIComponentDescriptor({type:GradientCanvas, id:"_BalanceSlider_GradientCanvas2", stylesFactory:function ():void{
                        this.gradientRatio = [0, 0xFF];
                        this.angle = [90];
                        this.borderAlphas = [0];
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, percentHeight:50, colorsConfiguration:[2]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"slider_mask", stylesFactory:function ():void{
                    this.backgroundColor = 0xFF0000;
                }, propertiesFactory:function ():Object{
                    return ({percentHeight:100, x:5});
                }}), new UIComponentDescriptor({type:Canvas, id:"numbers", events:{creationComplete:"__numbers_creationComplete"}, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:Repeater, id:"numberRep", propertiesFactory:function ():Object{
                        return ({childDescriptors:[new UIComponentDescriptor({type:Label, id:"_BalanceSlider_Label1", propertiesFactory:function ():Object{
                            return ({styleName:"sliderNumber"});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:HSlider, id:"slide", events:{thumbRelease:"__slide_thumbRelease", change:"__slide_change", mouseUp:"__slide_mouseUp"}, propertiesFactory:function ():Object{
                    return ({snapInterval:0.1, styleName:"balanceSlider", minimum:0, maximum:11, liveDragging:true, showDataTip:false, allowTrackClick:false});
                }})]});
            }});
            _1541031935_fillColors1 = [1665423, 2729956];
            _1541031936_fillColors2 = [2729956, 1665423];
            _316393670_fillAlphas1 = [1, 1];
            _316393669_fillAlphas2 = [1, 1];
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
        }
        private function get _fillColors2():Array{
            return (this._1541031936_fillColors2);
        }
        public function __slide_thumbRelease(_arg1:SliderEvent):void{
            snapToInt(_arg1);
        }
        private function set _fillColors2(_arg1:Array):void{
            var _local2:Object = this._1541031936_fillColors2;
            if (_local2 !== _arg1){
                this._1541031936_fillColors2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_fillColors2", _local2, _arg1));
            };
        }
        public function set bg(_arg1:Canvas):void{
            var _local2:Object = this._3141bg;
            if (_local2 !== _arg1){
                this._3141bg = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bg", _local2, _arg1));
            };
        }
        private function set _fillColors1(_arg1:Array):void{
            var _local2:Object = this._1541031935_fillColors1;
            if (_local2 !== _arg1){
                this._1541031935_fillColors1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_fillColors1", _local2, _arg1));
            };
        }
        public function get count():Number{
            return (this._94851343count);
        }
        public function get slider_mask():Canvas{
            return (this._58020982slider_mask);
        }
        public function get offset():Number{
            return (this._1019779949offset);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceSlider_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceSliderWatcherSetupUtil");
                var _local2 = watcherSetupUtilClass;
                _local2["init"](null);
            };
            _watcherSetupUtil.setup(this, function (_arg1:String){
                return (target[_arg1]);
            }, bindings, watchers);
            var i:* = 0;
            while (i < bindings.length) {
                Binding(bindings[i]).execute();
                i = (i + 1);
            };
            mx_internal::_bindings = mx_internal::_bindings.concat(bindings);
            mx_internal::_watchers = mx_internal::_watchers.concat(watchers);
            super.initialize();
        }
        public function get numberRep():Repeater{
            return (this._1649915028numberRep);
        }
        public function set count(_arg1:Number):void{
            var _local2:Object = this._94851343count;
            if (_local2 !== _arg1){
                this._94851343count = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "count", _local2, _arg1));
            };
        }
        private function setScrollPosition():void{
            var _local1:Number = slide.getThumbAt(0).width;
            var _local2:Number = (slide.width - _local1);
            var _local3:Number = ((numbers.width - _local2) / _local2);
            var _local4:Number = (slide.value * (_local2 / (slide.maximum + 1)));
            var _local5:Number = (offset - (_local4 * _local3));
            Tweener.removeTweens(numbers);
            Tweener.addTween(numbers, {x:_local5, time:0.4, transition:"easeoutexpo"});
        }
        public function set numberGap(_arg1:Number):void{
            var _local2:Object = this._1649904333numberGap;
            if (_local2 !== _arg1){
                this._1649904333numberGap = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "numberGap", _local2, _arg1));
            };
        }
        public function set fillColors(_arg1:Array):void{
            _fillColors1 = _arg1;
            _fillColors2 = [_fillColors1[1], _fillColors1[0]];
        }
        public function get sliderValue():Number{
            return (_sliderValue);
        }
        private function set _66679536sliderValue(_arg1:Number):void{
            _sliderValue = _arg1;
            callLater(setScrollPosition);
        }
        public function set offset(_arg1:Number):void{
            var _local2:Object = this._1019779949offset;
            if (_local2 !== _arg1){
                this._1019779949offset = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "offset", _local2, _arg1));
            };
        }
        public function set numberRep(_arg1:Repeater):void{
            var _local2:Object = this._1649915028numberRep;
            if (_local2 !== _arg1){
                this._1649915028numberRep = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "numberRep", _local2, _arg1));
            };
        }
        public function set bgHeight(_arg1:Number):void{
            var _local2:Object = this._1921875508bgHeight;
            if (_local2 !== _arg1){
                this._1921875508bgHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bgHeight", _local2, _arg1));
            };
        }
        public function get slide():HSlider{
            return (this._109526449slide);
        }
        public function __slide_mouseUp(_arg1:MouseEvent):void{
            jumpToNumber(_arg1);
        }
        public function set sliderValue(_arg1:Number):void{
            var _local2:Object = this.sliderValue;
            if (_local2 !== _arg1){
                this._66679536sliderValue = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sliderValue", _local2, _arg1));
            };
        }
        private function get _fillAlphas1():Array{
            return (this._316393670_fillAlphas1);
        }
        private function get _fillColors1():Array{
            return (this._1541031935_fillColors1);
        }
        private function scrollNumbers(_arg1:SliderEvent):void{
            setScrollPosition();
            dispatchEvent(_arg1);
        }
        public function set numbers(_arg1:Canvas):void{
            var _local2:Object = this._2000515510numbers;
            if (_local2 !== _arg1){
                this._2000515510numbers = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "numbers", _local2, _arg1));
            };
        }
        public function get _BalanceSlider_Canvas4():Canvas{
            return (this._703731515_BalanceSlider_Canvas4);
        }
        public function get bg():Canvas{
            return (this._3141bg);
        }
        private function set _fillAlphas1(_arg1:Array):void{
            var _local2:Object = this._316393670_fillAlphas1;
            if (_local2 !== _arg1){
                this._316393670_fillAlphas1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_fillAlphas1", _local2, _arg1));
            };
        }
        private function snapToInt(_arg1:SliderEvent):void{
            var _local2:Number = Math.round(slide.value);
            slide.value = _local2;
            setScrollPosition();
            dispatchEvent(_arg1);
        }
        public function set slider_mask(_arg1:Canvas):void{
            var _local2:Object = this._58020982slider_mask;
            if (_local2 !== _arg1){
                this._58020982slider_mask = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "slider_mask", _local2, _arg1));
            };
        }
        private function _BalanceSlider_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (bgHeight);
            }, function (_arg1:Number):void{
                bg.height = _arg1;
            }, "bg.height");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (((this.height - bg.height) / 2));
            }, function (_arg1:Number):void{
                bg.y = _arg1;
            }, "bg.y");
            result[1] = binding;
            binding = new Binding(this, function ():Array{
                return (_fillColors1);
            }, function (_arg1:Array):void{
                _BalanceSlider_GradientCanvas1.setStyle("fillColors", _arg1);
            }, "_BalanceSlider_GradientCanvas1.fillColors");
            result[2] = binding;
            binding = new Binding(this, function ():Array{
                return (_fillAlphas1);
            }, function (_arg1:Array):void{
                _BalanceSlider_GradientCanvas1.setStyle("fillAlphas", _arg1);
            }, "_BalanceSlider_GradientCanvas1.fillAlphas");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return ((bg.height / 2));
            }, function (_arg1:Number):void{
                _BalanceSlider_GradientCanvas2.y = _arg1;
            }, "_BalanceSlider_GradientCanvas2.y");
            result[4] = binding;
            binding = new Binding(this, function ():Array{
                return (_fillColors2);
            }, function (_arg1:Array):void{
                _BalanceSlider_GradientCanvas2.setStyle("fillColors", _arg1);
            }, "_BalanceSlider_GradientCanvas2.fillColors");
            result[5] = binding;
            binding = new Binding(this, function ():Array{
                return (_fillAlphas2);
            }, function (_arg1:Array):void{
                _BalanceSlider_GradientCanvas2.setStyle("fillAlphas", _arg1);
            }, "_BalanceSlider_GradientCanvas2.fillAlphas");
            result[6] = binding;
            binding = new Binding(this, function ():Number{
                return ((this.width - 10));
            }, function (_arg1:Number):void{
                slider_mask.width = _arg1;
            }, "slider_mask.width");
            result[7] = binding;
            binding = new Binding(this, function ():DisplayObject{
                return (slider_mask);
            }, function (_arg1:DisplayObject):void{
                numbers.mask = _arg1;
            }, "numbers.mask");
            result[8] = binding;
            binding = new Binding(this, function ():Number{
                return (offset);
            }, function (_arg1:Number):void{
                numbers.x = _arg1;
            }, "numbers.x");
            result[9] = binding;
            binding = new Binding(this, function ():Number{
                return (((this.height - numbers.height) / 2));
            }, function (_arg1:Number):void{
                numbers.y = _arg1;
            }, "numbers.y");
            result[10] = binding;
            binding = new Binding(this, function ():Object{
                return (new Array(count));
            }, function (_arg1:Object):void{
                numberRep.dataProvider = _arg1;
            }, "numberRep.dataProvider");
            result[11] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):Number{
                return ((numberGap * _arg2[0]));
            }, function (_arg1:Number, _arg2:Array):void{
                _BalanceSlider_Label1[_arg2[0]].x = _arg1;
            }, "_BalanceSlider_Label1.x");
            result[12] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):String{
                var _local3:* = _arg2[0];
                var _local4:* = ((_local3 == undefined)) ? null : String(_local3);
                return (_local4);
            }, function (_arg1:String, _arg2:Array):void{
                _BalanceSlider_Label1[_arg2[0]].text = _arg1;
            }, "_BalanceSlider_Label1.text");
            result[13] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):uint{
                return (textColor);
            }, function (_arg1:uint, _arg2:Array):void{
                _BalanceSlider_Label1[_arg2[0]].setStyle("color", _arg1);
            }, "_BalanceSlider_Label1.color");
            result[14] = binding;
            binding = new Binding(this, function ():Number{
                return (((this.width - slide.width) / 2));
            }, function (_arg1:Number):void{
                slide.x = _arg1;
            }, "slide.x");
            result[15] = binding;
            binding = new Binding(this, function ():Number{
                return (((this.height - slide.height) / 2));
            }, function (_arg1:Number):void{
                slide.y = _arg1;
            }, "slide.y");
            result[16] = binding;
            binding = new Binding(this, function ():Number{
                return (sliderValue);
            }, function (_arg1:Number):void{
                slide.value = _arg1;
            }, "slide.value");
            result[17] = binding;
            binding = new Binding(this, function ():Class{
                return (CustomSliderThumb);
            }, function (_arg1:Class):void{
                slide.sliderThumbClass = _arg1;
            }, "slide.sliderThumbClass");
            result[18] = binding;
            binding = new Binding(this, function ():Number{
                return ((this.width - 10));
            }, function (_arg1:Number):void{
                slide.width = _arg1;
            }, "slide.width");
            result[19] = binding;
            return (result);
        }
        public function get bgHeight():Number{
            return (this._1921875508bgHeight);
        }
        public function get numberGap():Number{
            return (this._1649904333numberGap);
        }
        public function get textColor():Number{
            return (this._1063571914textColor);
        }
        private function set _fillAlphas2(_arg1:Array):void{
            var _local2:Object = this._316393669_fillAlphas2;
            if (_local2 !== _arg1){
                this._316393669_fillAlphas2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_fillAlphas2", _local2, _arg1));
            };
        }
        private function get _fillAlphas2():Array{
            return (this._316393669_fillAlphas2);
        }
        public function __slide_change(_arg1:SliderEvent):void{
            scrollNumbers(_arg1);
        }
        public function get numbers():Canvas{
            return (this._2000515510numbers);
        }
        public function __numbers_creationComplete(_arg1:FlexEvent):void{
            setScrollPosition();
        }
        public function set fillAlphas(_arg1:Array):void{
            _fillAlphas1 = _arg1;
            _fillAlphas2 = [_fillAlphas1[1], _fillAlphas1[0]];
        }
        public function set slide(_arg1:HSlider):void{
            var _local2:Object = this._109526449slide;
            if (_local2 !== _arg1){
                this._109526449slide = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "slide", _local2, _arg1));
            };
        }
        private function jumpToNumber(_arg1:MouseEvent):void{
            var _local2:Number = _arg1.stageX;
            var _local3:Point = numbers.globalToContent(new Point(_local2, 0));
            slide.value = Math.floor((_local3.x / numberGap));
            snapToInt(new SliderEvent(SliderEvent.THUMB_RELEASE));
        }
        private function _BalanceSlider_bindingExprs():void{
            var _local1:*;
            _local1 = bgHeight;
            _local1 = ((this.height - bg.height) / 2);
            _local1 = _fillColors1;
            _local1 = _fillAlphas1;
            _local1 = (bg.height / 2);
            _local1 = _fillColors2;
            _local1 = _fillAlphas2;
            _local1 = (this.width - 10);
            _local1 = slider_mask;
            _local1 = offset;
            _local1 = ((this.height - numbers.height) / 2);
            _local1 = new Array(count);
            _local1 = (numberGap * numberRep.currentIndex);
            _local1 = numberRep.currentIndex;
            _local1 = textColor;
            _local1 = ((this.width - slide.width) / 2);
            _local1 = ((this.height - slide.height) / 2);
            _local1 = sliderValue;
            _local1 = CustomSliderThumb;
            _local1 = (this.width - 10);
        }
        public function set textColor(_arg1:Number):void{
            var _local2:Object = this._1063571914textColor;
            if (_local2 !== _arg1){
                this._1063571914textColor = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textColor", _local2, _arg1));
            };
        }
        public function set _BalanceSlider_Canvas4(_arg1:Canvas):void{
            var _local2:Object = this._703731515_BalanceSlider_Canvas4;
            if (_local2 !== _arg1){
                this._703731515_BalanceSlider_Canvas4 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_BalanceSlider_Canvas4", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceSlider._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
