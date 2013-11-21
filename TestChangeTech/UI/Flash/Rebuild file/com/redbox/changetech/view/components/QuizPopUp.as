//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import mx.managers.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.binding.*;
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class QuizPopUp extends VBox implements IBindingClient {

        public var _QuizPopUp_BalanceTextArea1:BalanceTextArea
        mx_internal var _bindingsBeginWithWord:Object
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        mx_internal var _bindingsByDestination:Object
        private var _1702149175bodyCopy:String
        public var _QuizPopUp_Label1:Label
        private var _1977022370headerCopy:String
        private var model:BalanceModelLocator
        public var _QuizPopUp_BalanceSlimButton1:BalanceSlimButton
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function QuizPopUp(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({width:300, height:0xFF, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_QuizPopUp_Label1", stylesFactory:function ():void{
                    this.textAlign = "center";
                    this.fontFamily = "Helvetica Neue";
                    this.fontSize = 32;
                }}), new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                    this.strokeColor = 0xE9E9E9;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:80});
                }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_QuizPopUp_BalanceTextArea1", stylesFactory:function ():void{
                    this.fontFamily = "Helvetica Neue";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:80});
                }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                    this.verticalAlign = "bottom";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:90, percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:BalanceSlimButton, id:"_QuizPopUp_BalanceSlimButton1", events:{click:"___QuizPopUp_BalanceSlimButton1_click"}, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }})]});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            if (!this.styleDeclaration){
                this.styleDeclaration = new CSSStyleDeclaration();
            };
            this.styleDeclaration.defaultFactory = function ():void{
                this.borderStyle = "solid";
                this.horizontalAlign = "center";
                this.verticalAlign = "middle";
                this.paddingTop = 10;
                this.paddingBottom = 10;
            };
            this.width = 300;
            this.height = 0xFF;
            this.styleName = "badgePopUp";
        }
        public function set headerCopy(_arg1:String):void{
            var _local2:Object = this._1977022370headerCopy;
            if (_local2 !== _arg1){
                this._1977022370headerCopy = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "headerCopy", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _QuizPopUp_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_QuizPopUpWatcherSetupUtil");
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
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function ___QuizPopUp_BalanceSlimButton1_click(_arg1:MouseEvent):void{
            PopUpManager.removePopUp(this);
        }
        private function _QuizPopUp_bindingExprs():void{
            var _local1:*;
            _local1 = headerCopy;
            _local1 = roomVO.textColour1;
            _local1 = bodyCopy;
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = model.languageVO.getLang("Continue_with_the_programme");
        }
        private function _QuizPopUp_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = headerCopy;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _QuizPopUp_Label1.text = _arg1;
            }, "_QuizPopUp_Label1.text");
            result[0] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _QuizPopUp_Label1.setStyle("color", _arg1);
            }, "_QuizPopUp_Label1.color");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = bodyCopy;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _QuizPopUp_BalanceTextArea1.htmlText = _arg1;
            }, "_QuizPopUp_BalanceTextArea1.htmlText");
            result[2] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _QuizPopUp_BalanceTextArea1.styleSheet = _arg1;
            }, "_QuizPopUp_BalanceTextArea1.styleSheet");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("Continue_with_the_programme");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _QuizPopUp_BalanceSlimButton1.label = _arg1;
            }, "_QuizPopUp_BalanceSlimButton1.label");
            result[4] = binding;
            return (result);
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        public function get headerCopy():String{
            return (this._1977022370headerCopy);
        }
        public function set bodyCopy(_arg1:String):void{
            var _local2:Object = this._1702149175bodyCopy;
            if (_local2 !== _arg1){
                this._1702149175bodyCopy = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bodyCopy", _local2, _arg1));
            };
        }
        public function get bodyCopy():String{
            return (this._1702149175bodyCopy);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            QuizPopUp._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
