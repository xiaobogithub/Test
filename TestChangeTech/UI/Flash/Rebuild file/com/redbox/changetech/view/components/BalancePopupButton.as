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

    public class BalancePopupButton extends Canvas implements IBindingClient {

        private var timer:Timer
        private var _925318956roomVO:RoomVO
        mx_internal var _watchers:Array
        mx_internal var _bindingsBeginWithWord:Object
        private var _97884btn:BalanceSettingsButton
        private var menu:IFlexDisplayObject
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor
        mx_internal var _bindingsByDestination:Object

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalancePopupButton(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:BalanceSettingsButton, id:"btn", events:{click:"__btn_click"}, propertiesFactory:function ():Object{
                    return ({width:160});
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
            this.addEventListener("creationComplete", ___BalancePopupButton_Canvas1_creationComplete);
        }
        private function openPopupMenu(_arg1:Event):void{
            menu = PopUpManager.createPopUp(this, BalanceSettingsMenu, false);
            menu.x = (x + 54);
            menu.y = 66;
            menu.width = (width - 31);
            BalanceSettingsMenu(menu).room = roomVO;
        }
        public function __btn_click(_arg1:MouseEvent):void{
            openPopupMenu(_arg1);
        }
        public function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalancePopupButton_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalancePopupButtonWatcherSetupUtil");
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
        private function closePopupMenu():void{
            Application.application.stage.removeEventListener(MouseEvent.MOUSE_DOWN, closePopupMenu);
            PopUpManager.removePopUp(menu);
        }
        private function _BalancePopupButton_bindingExprs():void{
            var _local1:*;
            _local1 = model.languageVO.getLang("personal_settings");
        }
        private function _BalancePopupButton_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("personal_settings");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                btn.label = _arg1;
            }, "btn.label");
            result[0] = binding;
            return (result);
        }
        private function initChangeRoom(_arg1:int):void{
            timer = new Timer(2000, 1);
            timer.addEventListener(TimerEvent.TIMER_COMPLETE, changeRoom);
            timer.start();
        }
        public function ___BalancePopupButton_Canvas1_creationComplete(_arg1:FlexEvent):void{
            complete(_arg1);
        }
        public function get btn():BalanceSettingsButton{
            return (this._97884btn);
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
            if (menu){
                BalanceSettingsMenu(menu).room = roomVO;
            };
            visible = BalanceModelLocator.getInstance().showControls;
        }
        public function set btn(_arg1:BalanceSettingsButton):void{
            var _local2:Object = this._97884btn;
            if (_local2 !== _arg1){
                this._97884btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "btn", _local2, _arg1));
            };
        }
        private function complete(_arg1:FlexEvent):void{
            BindingUtils.bindSetter(initChangeRoom, BalanceModelLocator.getInstance(), "room");
            visible = false;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalancePopupButton._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
