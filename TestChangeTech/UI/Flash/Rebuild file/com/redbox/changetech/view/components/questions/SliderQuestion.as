//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components.questions {
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
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import org.osflash.thunderbolt.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class SliderQuestion extends Canvas implements IBindingClient {

        mx_internal var _watchers:Array
        private var _2143844291sliderMinLabel:TextArea
        private var _374926351sliderMaxLabel:TextArea
        private var _1206422345hslider:HSlider
        private var _question:Question
        private var _432218117sliderStyleName:String
        mx_internal var _bindingsByDestination:Object
        private var _1783152115_roomVO:RoomVO
        mx_internal var _bindingsBeginWithWord:Object
        public var _SliderQuestion_HBox1:HBox
        public var _SliderQuestion_HBox2:HBox
        private var _1598053662_labels:Array
        private var _answer:String
        mx_internal var _bindings:Array
        private var _1843462656_direction:String = "vertical"
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function SliderQuestion(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                    this.verticalGap = -20;
                    this.paddingTop = 0;
                }, propertiesFactory:function ():Object{
                    return ({childDescriptors:[new UIComponentDescriptor({type:HBox, id:"_SliderQuestion_HBox1", stylesFactory:function ():void{
                        this.verticalAlign = "middle";
                        this.horizontalAlign = "left";
                        this.paddingTop = 0;
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:HSlider, id:"hslider", events:{change:"__hslider_change"}, stylesFactory:function ():void{
                            this.thumbOffset = 3;
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:100, minimum:1, snapInterval:1, tickInterval:1, showDataTip:false});
                        }})]});
                    }}), new UIComponentDescriptor({type:HBox, id:"_SliderQuestion_HBox2", stylesFactory:function ():void{
                        this.verticalAlign = "top";
                        this.horizontalAlign = "left";
                        this.paddingTop = 0;
                    }, propertiesFactory:function ():Object{
                        return ({height:20, percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"sliderMinLabel", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 12;
                            this.textAlign = "left";
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:40, selectable:false, tabEnabled:false});
                        }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                            return ({percentWidth:20});
                        }}), new UIComponentDescriptor({type:TextArea, id:"sliderMaxLabel", stylesFactory:function ():void{
                            this.fontFamily = "Helvetica Neue";
                            this.fontSize = 12;
                            this.textAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({percentWidth:40, selectable:false, tabEnabled:false});
                        }})]});
                    }})]});
                }})]});
            }});
            _1783152115_roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___SliderQuestion_Canvas1_creationComplete);
        }
        private function get _direction():String{
            return (this._1843462656_direction);
        }
        private function set _direction(_arg1:String):void{
            var _local2:Object = this._1843462656_direction;
            if (_local2 !== _arg1){
                this._1843462656_direction = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_direction", _local2, _arg1));
            };
        }
        public function set sliderMinLabel(_arg1:TextArea):void{
            var _local2:Object = this._2143844291sliderMinLabel;
            if (_local2 !== _arg1){
                this._2143844291sliderMinLabel = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sliderMinLabel", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _SliderQuestion_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_questions_SliderQuestionWatcherSetupUtil");
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
        private function _SliderQuestion_bindingExprs():void{
            var _local1:*;
            _local1 = _direction;
            _local1 = int(answer);
            _local1 = question.Options.length;
            _local1 = _labels;
            _local1 = (sliderMinLabel.y + 10);
            _local1 = sliderStyleName;
            _local1 = CustomSliderThumb;
            _local1 = _direction;
            _local1 = question.Options[0].Description;
            _local1 = _roomVO.textColour1;
            _local1 = !((sliderMinLabel.text == ""));
            _local1 = question.Options[(question.Options.length - 1)].Description;
            _local1 = _roomVO.textColour1;
            _local1 = !((sliderMaxLabel.text == ""));
        }
        private function get _labels():Array{
            return (this._1598053662_labels);
        }
        public function set hslider(_arg1:HSlider):void{
            var _local2:Object = this._1206422345hslider;
            if (_local2 !== _arg1){
                this._1206422345hslider = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "hslider", _local2, _arg1));
            };
        }
        private function init():void{
            trace("SLIDER QUESTION INIT ");
        }
        private function optionSelected(_arg1:Event):void{
            trace(" slider option selected");
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            var _local3:Number = (HSlider(_arg1.target).value - 1);
            _local2.optionId = _question.Options[_local3].Id;
            _local2.score = _question.Options[_local3].Score;
            trace(("sliderValue=" + _local3));
            trace(("_question.Options[sliderValue].Score=" + _question.Options[_local3].Score));
            dispatchEvent(_local2);
        }
        private function _SliderQuestion_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = _direction;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _SliderQuestion_HBox1.direction = _arg1;
            }, "_SliderQuestion_HBox1.direction");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (int(answer));
            }, function (_arg1:Number):void{
                hslider.value = _arg1;
            }, "hslider.value");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (question.Options.length);
            }, function (_arg1:Number):void{
                hslider.maximum = _arg1;
            }, "hslider.maximum");
            result[2] = binding;
            binding = new Binding(this, function ():Array{
                return (_labels);
            }, function (_arg1:Array):void{
                hslider.labels = _arg1;
            }, "hslider.labels");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return ((sliderMinLabel.y + 10));
            }, function (_arg1:Number):void{
                hslider.y = _arg1;
            }, "hslider.y");
            result[4] = binding;
            binding = new Binding(this, function ():Object{
                return (sliderStyleName);
            }, function (_arg1:Object):void{
                hslider.styleName = _arg1;
            }, "hslider.styleName");
            result[5] = binding;
            binding = new Binding(this, function ():Class{
                return (CustomSliderThumb);
            }, function (_arg1:Class):void{
                hslider.sliderThumbClass = _arg1;
            }, "hslider.sliderThumbClass");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _direction;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _SliderQuestion_HBox2.direction = _arg1;
            }, "_SliderQuestion_HBox2.direction");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = question.Options[0].Description;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                sliderMinLabel.text = _arg1;
            }, "sliderMinLabel.text");
            result[8] = binding;
            binding = new Binding(this, function ():uint{
                return (_roomVO.textColour1);
            }, function (_arg1:uint):void{
                sliderMinLabel.setStyle("color", _arg1);
            }, "sliderMinLabel.color");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((sliderMinLabel.text == "")));
            }, function (_arg1:Boolean):void{
                sliderMinLabel.includeInLayout = _arg1;
            }, "sliderMinLabel.includeInLayout");
            result[10] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = question.Options[(question.Options.length - 1)].Description;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                sliderMaxLabel.text = _arg1;
            }, "sliderMaxLabel.text");
            result[11] = binding;
            binding = new Binding(this, function ():uint{
                return (_roomVO.textColour1);
            }, function (_arg1:uint):void{
                sliderMaxLabel.setStyle("color", _arg1);
            }, "sliderMaxLabel.color");
            result[12] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((sliderMaxLabel.text == "")));
            }, function (_arg1:Boolean):void{
                sliderMaxLabel.includeInLayout = _arg1;
            }, "sliderMaxLabel.includeInLayout");
            result[13] = binding;
            return (result);
        }
        private function set _labels(_arg1:Array):void{
            var _local2:Object = this._1598053662_labels;
            if (_local2 !== _arg1){
                this._1598053662_labels = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_labels", _local2, _arg1));
            };
        }
        public function get answer():String{
            return (_answer);
        }
        public function __hslider_change(_arg1:SliderEvent):void{
            optionSelected(_arg1);
        }
        public function get hslider():HSlider{
            return (this._1206422345hslider);
        }
        public function get sliderMinLabel():TextArea{
            return (this._2143844291sliderMinLabel);
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
            _labels = new Array();
            var _local2:StringUtils = new StringUtils();
            var _local3:Number = 0;
            while (_local3 < _question.Options.length) {
                Logger.debug(("Label : " + _question.Options[_local3].Label));
                _labels.push(_local2.trim(_question.Options[_local3].Label, " "));
                _local3++;
            };
        }
        public function ___SliderQuestion_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = ((_arg1)==null) ? "0" : _arg1;
        }
        public function get question():Question{
            return (_question);
        }
        public function set sliderMaxLabel(_arg1:TextArea):void{
            var _local2:Object = this._374926351sliderMaxLabel;
            if (_local2 !== _arg1){
                this._374926351sliderMaxLabel = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sliderMaxLabel", _local2, _arg1));
            };
        }
        private function set _962590849direction(_arg1:String):void{
            _direction = ((((_arg1 == "horizontal")) || ((_arg1 == "vertical")))) ? _arg1 : "vertical";
        }
        public function get sliderMaxLabel():TextArea{
            return (this._374926351sliderMaxLabel);
        }
        private function set _roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._1783152115_roomVO;
            if (_local2 !== _arg1){
                this._1783152115_roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_roomVO", _local2, _arg1));
            };
        }
        private function get _roomVO():RoomVO{
            return (this._1783152115_roomVO);
        }
        public function get sliderStyleName():String{
            return (this._432218117sliderStyleName);
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        public function set sliderStyleName(_arg1:String):void{
            var _local2:Object = this._432218117sliderStyleName;
            if (_local2 !== _arg1){
                this._432218117sliderStyleName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "sliderStyleName", _local2, _arg1));
            };
        }
        public function set direction(_arg1:String):void{
            var _local2:Object = this.direction;
            if (_local2 !== _arg1){
                this._962590849direction = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "direction", _local2, _arg1));
            };
        }
        public function get direction():String{
            return (_direction);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            SliderQuestion._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components.questions 
