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
    import mx.binding.utils.*;
    import assets.*;
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
    import com.redbox.changetech.util.Enumerations.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class InfoPanel extends HBox implements IBindingClient {

        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var _112487538vrule:VRule
        private var _1071912318balancePopupButton:BalancePopupButton
        private var _739161860infoTargetBadge:InfoTargetBadge
        private var _912737333infoDayBadge:InfoDayBadge
        mx_internal var _bindings:Array
        private var _97692276fsBut:Image
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function InfoPanel(){
            _documentDescriptor_ = new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:InfoDayBadge, id:"infoDayBadge", propertiesFactory:function ():Object{
                    return ({visible:false});
                }}), new UIComponentDescriptor({type:VRule, id:"vrule", propertiesFactory:function ():Object{
                    return ({height:40, visible:false});
                }}), new UIComponentDescriptor({type:InfoTargetBadge, id:"infoTargetBadge", propertiesFactory:function ():Object{
                    return ({visible:false});
                }}), new UIComponentDescriptor({type:BalancePopupButton, id:"balancePopupButton", propertiesFactory:function ():Object{
                    return ({visible:false});
                }}), new UIComponentDescriptor({type:Image, id:"fsBut", events:{click:"__fsBut_click"}, propertiesFactory:function ():Object{
                    return ({buttonMode:true});
                }})]});
            }});
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
                this.horizontalGap = 8;
                this.verticalAlign = "middle";
            };
            this.addEventListener("creationComplete", ___InfoPanel_HBox1_creationComplete);
        }
        private function _InfoPanel_bindingExprs():void{
            var _local1:*;
            _local1 = Assets.getInstance().fullscreenToggle;
            _local1 = BalanceModelLocator.getInstance().languageVO.getLang("toggle_full_screen");
        }
        public function get balancePopupButton():BalancePopupButton{
            return (this._1071912318balancePopupButton);
        }
        public function get fsBut():Image{
            return (this._97692276fsBut);
        }
        public function set balancePopupButton(_arg1:BalancePopupButton):void{
            var _local2:Object = this._1071912318balancePopupButton;
            if (_local2 !== _arg1){
                this._1071912318balancePopupButton = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balancePopupButton", _local2, _arg1));
            };
        }
        public function set infoTargetBadge(_arg1:InfoTargetBadge):void{
            var _local2:Object = this._739161860infoTargetBadge;
            if (_local2 !== _arg1){
                this._739161860infoTargetBadge = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "infoTargetBadge", _local2, _arg1));
            };
        }
        private function update(_arg1:ContentCollection):void{
            var _local2:Array;
            var _local3:Number;
            if (_arg1 != null){
                switch (true){
                    case (((((((_arg1.Type == ContentType.ScreeningTest.Text)) || ((_arg1.Type == ContentType.ScreeningPart2a.Text)))) || ((_arg1.Type == ContentType.ScreeningPart2b.Text)))) || ((_arg1.Type == ContentType.Intro.Text))):
                        infoDayBadge.visible = false;
                        infoDayBadge.alpha = 0;
                        vrule.visible = false;
                        vrule.alpha = 0;
                        infoTargetBadge.visible = false;
                        infoTargetBadge.alpha = 0;
                        balancePopupButton.visible = false;
                        balancePopupButton.alpha = 0;
                        break;
                    case (_arg1.Phase == ContentCollection.COUNTDOWN):
                        infoDayBadge.visible = false;
                        infoDayBadge.alpha = 0;
                        vrule.visible = false;
                        vrule.alpha = 0;
                        infoTargetBadge.visible = false;
                        infoTargetBadge.alpha = 0;
                        balancePopupButton.visible = true;
                        balancePopupButton.alpha = 1;
                        break;
                    case (_arg1.DayNumber == 1):
                        infoDayBadge.visible = true;
                        infoDayBadge.alpha = 1;
                        vrule.visible = true;
                        vrule.alpha = 1;
                        infoTargetBadge.visible = false;
                        infoTargetBadge.alpha = 0;
                        balancePopupButton.visible = true;
                        balancePopupButton.alpha = 1;
                        break;
                    default:
                        infoDayBadge.visible = true;
                        infoDayBadge.alpha = 1;
                        vrule.visible = true;
                        vrule.alpha = 1;
                        infoTargetBadge.visible = true;
                        infoTargetBadge.alpha = 1;
                        balancePopupButton.visible = true;
                        balancePopupButton.alpha = 1;
                };
                _local2 = [infoDayBadge, vrule, infoTargetBadge, balancePopupButton, fsBut];
                _local3 = 0;
                while (_local3 < _local2.length) {
                    _local2[_local3].includeInLayout = _local2[_local3].visible;
                    _local3++;
                };
            };
        }
        public function get infoTargetBadge():InfoTargetBadge{
            return (this._739161860infoTargetBadge);
        }
        private function toggleFullScreen():void{
            Application.application.toggleFullScreen();
        }
        public function __fsBut_click(_arg1:MouseEvent):void{
            toggleFullScreen();
        }
        private function init():void{
            BindingUtils.bindSetter(update, BalanceModelLocator.getInstance(), "currentContentCollection");
            Application.application.addEventListener(ScreenToggleEvent.SCREEN_TOGGLE, updateToggle);
            var _local1:JavaScriptUtils = new JavaScriptUtils();
            _local1.setUpToggle();
            ToolTipManager.enabled = true;
        }
        public function set vrule(_arg1:VRule):void{
            var _local2:Object = this._112487538vrule;
            if (_local2 !== _arg1){
                this._112487538vrule = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "vrule", _local2, _arg1));
            };
        }
        private function _InfoPanel_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (Assets.getInstance().fullscreenToggle);
            }, function (_arg1:Object):void{
                fsBut.source = _arg1;
            }, "fsBut.source");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = BalanceModelLocator.getInstance().languageVO.getLang("toggle_full_screen");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                fsBut.toolTip = _arg1;
            }, "fsBut.toolTip");
            result[1] = binding;
            return (result);
        }
        public function get vrule():VRule{
            return (this._112487538vrule);
        }
        public function set fsBut(_arg1:Image):void{
            var _local2:Object = this._97692276fsBut;
            if (_local2 !== _arg1){
                this._97692276fsBut = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fsBut", _local2, _arg1));
            };
        }
        public function ___InfoPanel_HBox1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _InfoPanel_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_InfoPanelWatcherSetupUtil");
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
        public function set infoDayBadge(_arg1:InfoDayBadge):void{
            var _local2:Object = this._912737333infoDayBadge;
            if (_local2 !== _arg1){
                this._912737333infoDayBadge = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "infoDayBadge", _local2, _arg1));
            };
        }
        public function get infoDayBadge():InfoDayBadge{
            return (this._912737333infoDayBadge);
        }
        private function updateToggle(_arg1:Event):void{
            if (BalanceModelLocator.getInstance().isFullScreen){
                fsBut.toolTip = BalanceModelLocator.getInstance().languageVO.getLang("toggle_normal");
            } else {
                fsBut.toolTip = BalanceModelLocator.getInstance().languageVO.getLang("toggle_full_screen");
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            InfoPanel._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
