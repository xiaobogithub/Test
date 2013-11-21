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
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.Enumerations.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class RadioButtonQuestion extends Canvas implements IBindingClient {

        private var _855831665rb_options:RadioButtonGroup
        private var _2016969570fbk_visible:Boolean = false
        mx_internal var _watchers:Array
        private var _891486451radioButton:Array
        private var _101167fbk:TextArea
        protected var _content:Content
        protected var _question:Question
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        protected var _answer:String = ""
        mx_internal var _bindings:Array
        public var _RadioButtonQuestion_VBox1:VBox
        private var _112797rep:Repeater
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function RadioButtonQuestion(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:VBox, id:"_RadioButtonQuestion_VBox1", stylesFactory:function ():void{
                    this.horizontalAlign = "left";
                    this.paddingTop = 0;
                    this.verticalGap = -4;
                }, propertiesFactory:function ():Object{
                    return ({percentHeight:100, percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:Repeater, id:"rep", propertiesFactory:function ():Object{
                        return ({childDescriptors:[new UIComponentDescriptor({type:BalanceRadioButton, id:"radioButton", events:{click:"__radioButton_click"}, stylesFactory:function ():void{
                            this.paddingLeft = 10;
                            this.leading = -6;
                        }, propertiesFactory:function ():Object{
                            return ({groupName:"rb_options", useHandCursor:true, percentWidth:100});
                        }})]});
                    }}), new UIComponentDescriptor({type:Spacer, propertiesFactory:function ():Object{
                        return ({height:15});
                    }}), new UIComponentDescriptor({type:TextArea, id:"fbk", propertiesFactory:function ():Object{
                        return ({percentWidth:100, verticalScrollPolicy:"off"});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.percentWidth = 100;
            _RadioButtonQuestion_RadioButtonGroup1_i();
            this.addEventListener("creationComplete", ___RadioButtonQuestion_Canvas1_creationComplete);
        }
        public function set answer(_arg1:String):void{
            var _local2:Object = this.answer;
            if (_local2 !== _arg1){
                this._1412808770answer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "answer", _local2, _arg1));
            };
        }
        public function get radioButton():Array{
            return (this._891486451radioButton);
        }
        public function get rb_options():RadioButtonGroup{
            return (this._855831665rb_options);
        }
        private function init():void{
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _RadioButtonQuestion_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_questions_RadioButtonQuestionWatcherSetupUtil");
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
        public function set radioButton(_arg1:Array):void{
            var _local2:Object = this._891486451radioButton;
            if (_local2 !== _arg1){
                this._891486451radioButton = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "radioButton", _local2, _arg1));
            };
        }
        public function get rep():Repeater{
            return (this._112797rep);
        }
        public function set rb_options(_arg1:RadioButtonGroup):void{
            var _local2:Object = this._855831665rb_options;
            if (_local2 !== _arg1){
                this._855831665rb_options = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rb_options", _local2, _arg1));
            };
        }
        public function get fbk():TextArea{
            return (this._101167fbk);
        }
        protected function optionSelected(_arg1:Event=null):void{
            var _local2:AnswerSelectedEvent = new AnswerSelectedEvent(AnswerSelectedEvent.ANSWER_SELECTED, true, false);
            _local2.id = question.Id;
            _local2.score = _arg1.target.score;
            _local2.optionId = _arg1.target.optionID;
            _local2.actionFlag = _arg1.target.actionFlag;
            _local2.label = _arg1.target.label;
            _local2.description = _arg1.target.description;
            if (((!((_arg1.target.description == null))) && (!((_arg1.target.description == ""))))){
                if (content.Template == Templates.Quiz.Text){
                    Application.application.openGenericPopup(_arg1.target.label, _arg1.target.description);
                } else {
                    fbk_visible = true;
                    fbk.text = _arg1.target.description;
                };
            };
            dispatchEvent(_local2);
        }
        public function get answer():String{
            return (_answer);
        }
        private function _RadioButtonQuestion_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (question.Options);
            }, function (_arg1:Object):void{
                rep.dataProvider = _arg1;
            }, "rep.dataProvider");
            result[0] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):String{
                var _local3:* = rep.mx_internal::getItemAt(_arg2[0]).Label;
                var _local4:* = ((_local3 == undefined)) ? null : String(_local3);
                return (_local4);
            }, function (_arg1:String, _arg2:Array):void{
                radioButton[_arg2[0]].label = _arg1;
            }, "radioButton.label");
            result[1] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):Number{
                return (rep.mx_internal::getItemAt(_arg2[0]).Id);
            }, function (_arg1:Number, _arg2:Array):void{
                radioButton[_arg2[0]].optionID = _arg1;
            }, "radioButton.optionID");
            result[2] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):String{
                var _local3:* = rep.mx_internal::getItemAt(_arg2[0]).ActionFlag;
                var _local4:* = ((_local3 == undefined)) ? null : String(_local3);
                return (_local4);
            }, function (_arg1:String, _arg2:Array):void{
                radioButton[_arg2[0]].actionFlag = _arg1;
            }, "radioButton.actionFlag");
            result[3] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):String{
                var _local3:* = rep.mx_internal::getItemAt(_arg2[0]).Description;
                var _local4:* = ((_local3 == undefined)) ? null : String(_local3);
                return (_local4);
            }, function (_arg1:String, _arg2:Array):void{
                radioButton[_arg2[0]].description = _arg1;
            }, "radioButton.description");
            result[4] = binding;
            binding = new RepeatableBinding(this, function (_arg1:Array, _arg2:Array):Number{
                return (rep.mx_internal::getItemAt(_arg2[0]).Score);
            }, function (_arg1:Number, _arg2:Array):void{
                radioButton[_arg2[0]].score = _arg1;
            }, "radioButton.score");
            result[5] = binding;
            binding = new Binding(this, function ():Boolean{
                return (fbk_visible);
            }, function (_arg1:Boolean):void{
                fbk.includeInLayout = _arg1;
            }, "fbk.includeInLayout");
            result[6] = binding;
            binding = new Binding(this, function ():Number{
                return ((fbk.textHeight + 5));
            }, function (_arg1:Number):void{
                fbk.height = _arg1;
            }, "fbk.height");
            result[7] = binding;
            return (result);
        }
        public function ___RadioButtonQuestion_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function _RadioButtonQuestion_bindingExprs():void{
            var _local1:*;
            _local1 = question.Options;
            _local1 = rep.currentItem.Label;
            _local1 = rep.currentItem.Id;
            _local1 = rep.currentItem.ActionFlag;
            _local1 = rep.currentItem.Description;
            _local1 = rep.currentItem.Score;
            _local1 = fbk_visible;
            _local1 = (fbk.textHeight + 5);
        }
        public function set rep(_arg1:Repeater):void{
            var _local2:Object = this._112797rep;
            if (_local2 !== _arg1){
                this._112797rep = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rep", _local2, _arg1));
            };
        }
        private function set fbk_visible(_arg1:Boolean):void{
            var _local2:Object = this._2016969570fbk_visible;
            if (_local2 !== _arg1){
                this._2016969570fbk_visible = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fbk_visible", _local2, _arg1));
            };
        }
        private function set _1165870106question(_arg1:Question):void{
            _question = _arg1;
        }
        public function set question(_arg1:Question):void{
            var _local2:Object = this.question;
            if (_local2 !== _arg1){
                this._1165870106question = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "question", _local2, _arg1));
            };
        }
        public function set fbk(_arg1:TextArea):void{
            var _local2:Object = this._101167fbk;
            if (_local2 !== _arg1){
                this._101167fbk = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fbk", _local2, _arg1));
            };
        }
        public function get question():Question{
            return (_question);
        }
        private function set _1412808770answer(_arg1:String):void{
            _answer = _arg1;
        }
        public function set content(_arg1:Content):void{
            _content = _arg1;
        }
        public function __radioButton_click(_arg1:MouseEvent):void{
            optionSelected(_arg1);
        }
        public function get content():Content{
            return (_content);
        }
        private function get fbk_visible():Boolean{
            return (this._2016969570fbk_visible);
        }
        private function _RadioButtonQuestion_RadioButtonGroup1_i():RadioButtonGroup{
            var _local1:RadioButtonGroup = new RadioButtonGroup();
            rb_options = _local1;
            _local1.initialized(this, "rb_options");
            return (_local1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            RadioButtonQuestion._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components.questions 
