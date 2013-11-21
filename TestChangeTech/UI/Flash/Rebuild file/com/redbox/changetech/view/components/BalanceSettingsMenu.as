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
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceSettingsMenu extends Canvas implements IBindingClient {

        private var _room:RoomVO
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var _3322014list:List
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceSettingsMenu(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:List, id:"list", stylesFactory:function ():void{
                    this.paddingTop = 0;
                    this.borderStyle = "none";
                    this.backgroundAlpha = 0;
                    this.fontFamily = "Helvetica Neue";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "off";
            this.horizontalScrollPolicy = "off";
            this.addEventListener("creationComplete", ___BalanceSettingsMenu_Canvas1_creationComplete);
        }
        private function set _3506395room(_arg1:RoomVO):void{
            _room = _arg1;
        }
        private function _BalanceSettingsMenu_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():uint{
                return (room.textColour1);
            }, function (_arg1:uint):void{
                list.setStyle("textRollOverColor", _arg1);
            }, "list.textRollOverColor");
            result[0] = binding;
            return (result);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceSettingsMenu_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceSettingsMenuWatcherSetupUtil");
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
        private function _BalanceSettingsMenu_bindingExprs():void{
            var _local1:*;
            _local1 = room.textColour1;
        }
        public function get room():RoomVO{
            return (_room);
        }
        public function ___BalanceSettingsMenu_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function init():void{
            list.dataProvider = model.dropDownMenuVO.getDataProvider();
            list.itemRenderer = new ClassFactory(DropDownItemRenderer);
            list.rowCount = list.dataProvider.length;
            list.rowHeight = 30;
            list.addEventListener(FlexEvent.UPDATE_COMPLETE, refreshMenuBackground);
            list.addEventListener(ListEvent.CHANGE, launchPopup);
        }
        public function get list():List{
            return (this._3322014list);
        }
        private function refreshMenuBackground(_arg1:Number=-1):void{
            height = (list.height + 5);
            var _local2:Number = 20;
            var _local3:Number = 8;
            var _local4:Number = 10;
            var _local5:Number = 0xE4E4E4;
            var _local6:Array = [0xFFFFFF, 15132648];
            graphics.clear();
            graphics.lineStyle(1, _local5);
            var _local7:Matrix = new Matrix();
            _local7.createGradientBox(width, height, ((Math.PI / 180) * 90), 0, 0);
            graphics.beginGradientFill(GradientType.LINEAR, _local6, [1, 1], [60, 0xFF], _local7);
            graphics.moveTo(0, 0);
            if (_arg1 > -1){
                graphics.lineTo(0, (_arg1 - (_local2 / 2)));
                graphics.lineTo(-(_local3), _arg1);
                graphics.lineTo(0, (_arg1 + (_local2 / 2)));
            };
            graphics.lineTo(0, (height - _local4));
            graphics.curveTo(0, height, _local4, height);
            graphics.lineTo((width - _local4), height);
            graphics.curveTo(width, height, width, (height - _local4));
            graphics.lineTo(width, 0);
            graphics.lineTo(0, 0);
        }
        public function launchPopup(_arg1:ListEvent):void{
            trace("launchPopup");
            var _local2:Object = _arg1.itemRenderer.data.value;
            trace(_local2);
            Application.application.launchModulePopup(_local2);
        }
        public function set list(_arg1:List):void{
            var _local2:Object = this._3322014list;
            if (_local2 !== _arg1){
                this._3322014list = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "list", _local2, _arg1));
            };
        }
        public function set room(_arg1:RoomVO):void{
            var _local2:Object = this.room;
            if (_local2 !== _arg1){
                this._3506395room = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "room", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceSettingsMenu._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
