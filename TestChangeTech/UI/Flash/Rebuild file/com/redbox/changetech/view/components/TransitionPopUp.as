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
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class TransitionPopUp extends VBox implements IBindingClient {

        public var _TransitionPopUp_Label1:Label
        mx_internal var _watchers:Array
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindings:Array
        mx_internal var _bindingsBeginWithWord:Object
        private var _925318956roomVO:RoomVO
        private var _documentDescriptor_:UIComponentDescriptor
        public var _TransitionPopUp_Image1:Image

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function TransitionPopUp(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({width:200, height:100, childDescriptors:[new UIComponentDescriptor({type:Image, id:"_TransitionPopUp_Image1"}), new UIComponentDescriptor({type:Label, id:"_TransitionPopUp_Label1", stylesFactory:function ():void{
                    this.textAlign = "center";
                    this.fontFamily = "Helvetica Neue";
                    this.fontSize = 32;
                }}), new UIComponentDescriptor({type:HRule, stylesFactory:function ():void{
                    this.strokeColor = 9471879;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:80});
                }})]});
            }});
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
            this.width = 200;
            this.height = 100;
            this.styleName = "badgePopUp";
        }
        private function _TransitionPopUp_bindingExprs():void{
            var _local1:*;
            _local1 = BalanceModelLocator.getInstance().assets[roomVO.badgeAsset];
            _local1 = BalanceModelLocator.getInstance().languageVO.getLang(roomVO.roomName);
            _local1 = roomVO.textColour1;
        }
        public function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        private function _TransitionPopUp_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (BalanceModelLocator.getInstance().assets[roomVO.badgeAsset]);
            }, function (_arg1:Object):void{
                _TransitionPopUp_Image1.source = _arg1;
            }, "_TransitionPopUp_Image1.source");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = BalanceModelLocator.getInstance().languageVO.getLang(roomVO.roomName);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _TransitionPopUp_Label1.text = _arg1;
            }, "_TransitionPopUp_Label1.text");
            result[1] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _TransitionPopUp_Label1.setStyle("color", _arg1);
            }, "_TransitionPopUp_Label1.color");
            result[2] = binding;
            return (result);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _TransitionPopUp_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_TransitionPopUpWatcherSetupUtil");
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
        public function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            TransitionPopUp._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
