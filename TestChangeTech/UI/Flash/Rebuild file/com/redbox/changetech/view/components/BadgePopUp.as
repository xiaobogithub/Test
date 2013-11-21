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

    public class BadgePopUp extends VBox implements IBindingClient {

        private var _documentDescriptor_:UIComponentDescriptor
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        public var _BadgePopUp_Image1:Image
        public var _BadgePopUp_Label1:Label
        mx_internal var _watchers:Array
        private var roomVO:RoomVO
        public var _BadgePopUp_BalanceSlimButton1:BalanceSlimButton
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        public var _BadgePopUp_BalanceTextArea1:BalanceTextArea
        private var _110371416title:String
        private var _954925063message:String

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BadgePopUp(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({width:300, height:0xFF, childDescriptors:[new UIComponentDescriptor({type:Image, id:"_BadgePopUp_Image1"}), new UIComponentDescriptor({type:Label, id:"_BadgePopUp_Label1", stylesFactory:function ():void{
                    this.textAlign = "center";
                    this.fontFamily = "Helvetica Neue";
                    this.fontSize = 32;
                }}), new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                    this.strokeColor = 0xE9E9E9;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:80});
                }}), new UIComponentDescriptor({type:BalanceTextArea, id:"_BadgePopUp_BalanceTextArea1", stylesFactory:function ():void{
                    this.fontFamily = "Helvetica Neue";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:80});
                }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                    this.verticalAlign = "bottom";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:90, percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:BalanceSlimButton, id:"_BadgePopUp_BalanceSlimButton1", events:{click:"___BadgePopUp_BalanceSlimButton1_click"}, propertiesFactory:function ():Object{
                        return ({percentWidth:100});
                    }})]});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
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
            this.addEventListener("creationComplete", ___BadgePopUp_VBox1_creationComplete);
        }
        public function ___BadgePopUp_BalanceSlimButton1_click(_arg1:MouseEvent):void{
            PopUpManager.removePopUp(this);
        }
        private function get message():String{
            return (this._954925063message);
        }
        private function _BadgePopUp_bindingExprs():void{
            var _local1:*;
            _local1 = BalanceModelLocator.getInstance().assets[roomVO.badgeAsset];
            _local1 = title;
            _local1 = roomVO.textColour1;
            _local1 = message;
            _local1 = BalanceModelLocator.getInstance().balanceStyleSheet;
            _local1 = model.languageVO.getLang("Continue_with_the_programme");
        }
        private function set message(_arg1:String):void{
            var _local2:Object = this._954925063message;
            if (_local2 !== _arg1){
                this._954925063message = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "message", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BadgePopUp_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BadgePopUpWatcherSetupUtil");
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
        private function init():void{
            title = model.languageVO.getLang(roomVO.roomName);
            message = model.languageVO.getLang(roomVO.demoPopUpCopy);
            trace(("roomVO.demoPopUpCopy=" + roomVO.demoPopUpCopy));
        }
        private function get title():String{
            return (this._110371416title);
        }
        private function set title(_arg1:String):void{
            var _local2:Object = this._110371416title;
            if (_local2 !== _arg1){
                this._110371416title = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "title", _local2, _arg1));
            };
        }
        public function ___BadgePopUp_VBox1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function _BadgePopUp_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (BalanceModelLocator.getInstance().assets[roomVO.badgeAsset]);
            }, function (_arg1:Object):void{
                _BadgePopUp_Image1.source = _arg1;
            }, "_BadgePopUp_Image1.source");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BadgePopUp_Label1.text = _arg1;
            }, "_BadgePopUp_Label1.text");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BadgePopUp_Label1.setStyle("color", _arg1);
            }, "_BadgePopUp_Label1.color");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = message;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BadgePopUp_BalanceTextArea1.htmlText = _arg1;
            }, "_BadgePopUp_BalanceTextArea1.htmlText");
            result[3] = binding;
            binding = new Binding(this, function ():StyleSheet{
                return (BalanceModelLocator.getInstance().balanceStyleSheet);
            }, function (_arg1:StyleSheet):void{
                _BadgePopUp_BalanceTextArea1.styleSheet = _arg1;
            }, "_BadgePopUp_BalanceTextArea1.styleSheet");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("Continue_with_the_programme");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BadgePopUp_BalanceSlimButton1.label = _arg1;
            }, "_BadgePopUp_BalanceSlimButton1.label");
            result[5] = binding;
            return (result);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BadgePopUp._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
