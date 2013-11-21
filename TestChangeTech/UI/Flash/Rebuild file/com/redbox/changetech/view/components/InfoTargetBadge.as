﻿//Created by Action Script Viewer - http://www.buraks.com/asv
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
    import com.redbox.changetech.model.*;
    import mx.binding.utils.*;
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

    public class InfoTargetBadge extends VBox implements IBindingClient {

        private var timer:Timer
        mx_internal var _bindingsBeginWithWord:Object
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        mx_internal var _bindingsByDestination:Object
        private var _591217587dayTarget:String = ""
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        public var _InfoTargetBadge_Label1:Label
        public var _InfoTargetBadge_Label2:Label

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function InfoTargetBadge(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({height:50, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_InfoTargetBadge_Label1", propertiesFactory:function ():Object{
                    return ({width:50, height:15, styleName:"infoDayBadgeDay"});
                }}), new UIComponentDescriptor({type:Label, id:"_InfoTargetBadge_Label2", stylesFactory:function ():void{
                    this.textAlign = "center";
                    this.fontSize = 22;
                    this.fontFamily = "Helvetica Neue";
                    this.fontWeight = "bold";
                }, propertiesFactory:function ():Object{
                    return ({width:50, height:25});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            model = BalanceModelLocator.getInstance();
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
                this.horizontalAlign = "center";
                this.verticalGap = -3;
            };
            this.height = 50;
            this.addEventListener("creationComplete", ___InfoTargetBadge_VBox1_creationComplete);
        }
        public function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        private function get dayTarget():String{
            return (this._591217587dayTarget);
        }
        public function ___InfoTargetBadge_VBox1_creationComplete(_arg1:FlexEvent):void{
            complete(_arg1);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _InfoTargetBadge_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_InfoTargetBadgeWatcherSetupUtil");
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
        private function initChangeRoom(_arg1:int):void{
            timer = new Timer(2000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            timer.start();
        }
        private function set dayTarget(_arg1:String):void{
            var _local2:Object = this._591217587dayTarget;
            if (_local2 !== _arg1){
                this._591217587dayTarget = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dayTarget", _local2, _arg1));
            };
        }
        public function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function changeRoom(_arg1:TimerEvent):void{
            timer.removeEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            visible = BalanceModelLocator.getInstance().showControls;
            dayTarget = BalanceModelLocator.getInstance().consumer.getCurrentDayTarget();
        }
        private function _InfoTargetBadge_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("target");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _InfoTargetBadge_Label1.text = _arg1;
            }, "_InfoTargetBadge_Label1.text");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = dayTarget;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _InfoTargetBadge_Label2.text = _arg1;
            }, "_InfoTargetBadge_Label2.text");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _InfoTargetBadge_Label2.setStyle("color", _arg1);
            }, "_InfoTargetBadge_Label2.color");
            result[2] = binding;
            return (result);
        }
        private function complete(_arg1:FlexEvent):void{
            BindingUtils.bindSetter(initChangeRoom, BalanceModelLocator.getInstance(), "room");
            visible = false;
            dayTarget = BalanceModelLocator.getInstance().consumer.getCurrentDayTarget();
        }
        private function _InfoTargetBadge_bindingExprs():void{
            var _local1:*;
            _local1 = model.languageVO.getLang("target");
            _local1 = dayTarget;
            _local1 = roomVO.textColour1;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            InfoTargetBadge._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
