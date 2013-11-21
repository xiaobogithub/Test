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

    public class BalanceRoomBadge extends Box implements IBindingClient {

        private var timer:Timer
        private var _1783152115_roomVO:RoomVO
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var _1715873934bottomRightBadge:BottomRightBadge
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceRoomBadge(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Box, propertiesFactory:function ():Object{
                return ({width:200, height:200, childDescriptors:[new UIComponentDescriptor({type:BottomRightBadge, id:"bottomRightBadge"})]});
            }});
            _1783152115_roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
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
                this.verticalAlign = "middle";
            };
            this.width = 200;
            this.height = 200;
            this.verticalScrollPolicy = "off";
            this.horizontalScrollPolicy = "off";
            this.addEventListener("creationComplete", ___BalanceRoomBadge_Box1_creationComplete);
        }
        private function _BalanceRoomBadge_bindingExprs():void{
            var _local1:*;
            _local1 = _roomVO;
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceRoomBadge_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceRoomBadgeWatcherSetupUtil");
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
        private function _BalanceRoomBadge_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():RoomVO{
                return (_roomVO);
            }, function (_arg1:RoomVO):void{
                bottomRightBadge.roomVO = _arg1;
            }, "bottomRightBadge.roomVO");
            result[0] = binding;
            return (result);
        }
        public function ___BalanceRoomBadge_Box1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function initChangeRoom(_arg1:int):void{
            timer = new Timer(2000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            timer.start();
        }
        public function set bottomRightBadge(_arg1:BottomRightBadge):void{
            var _local2:Object = this._1715873934bottomRightBadge;
            if (_local2 !== _arg1){
                this._1715873934bottomRightBadge = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bottomRightBadge", _local2, _arg1));
            };
        }
        private function set _roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._1783152115_roomVO;
            if (_local2 !== _arg1){
                this._1783152115_roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_roomVO", _local2, _arg1));
            };
        }
        private function init(_arg1:FlexEvent):void{
            BindingUtils.bindSetter(initChangeRoom, BalanceModelLocator.getInstance(), "room");
        }
        private function changeRoom(_arg1:TimerEvent):void{
            timer.removeEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            _roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            if (_roomVO.roomName == "Blank"){
                bottomRightBadge.visible = false;
            } else {
                bottomRightBadge.visible = true;
            };
        }
        public function get bottomRightBadge():BottomRightBadge{
            return (this._1715873934bottomRightBadge);
        }
        private function get _roomVO():RoomVO{
            return (this._1783152115_roomVO);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceRoomBadge._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
