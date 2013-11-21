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
    import mx.states.*;
    import mx.effects.*;
    import mx.binding.*;
    import assets.*;
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

    public class IconOption extends GradientCanvas implements IBindingClient {

        private var _1231310186info_icon:Image
        private var _1282089978_selected:Boolean = false
        public var _IconOption_Transition1:Transition
        private var _3559837tick:Image
        mx_internal var _bindingsByDestination:Object
        public var iconOption:IconOption = null
        private var _1698334100_option:Option
        private var _1724546052description:TextArea
        private var _3645t1:Sequence
        private var _2025208835valueLabel:Label
        private var _738054082iconPath:String
        mx_internal var _watchers:Array
        public var _IconOption_Image2:Image
        public var _IconOption_SetProperty2:SetProperty
        public var _IconOption_SetProperty4:SetProperty
        public var _IconOption_SetProperty5:SetProperty
        public var _IconOption_SetProperty6:SetProperty
        public var _IconOption_SetProperty7:SetProperty
        public var _IconOption_SetProperty1:SetProperty
        public var _IconOption_SetProperty3:SetProperty
        public var _IconOption_SetProperty8:SetProperty
        public var _IconOption_SetProperty9:SetProperty
        public var _IconOption_SetProperty10:SetProperty
        public var _IconOption_SetProperty11:SetProperty
        public var _IconOption_SetProperty12:SetProperty
        public var _IconOption_SetProperty13:SetProperty
        mx_internal var _bindings:Array
        mx_internal var _bindingsBeginWithWord:Object
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function IconOption(){
            _documentDescriptor_ = new UIComponentDescriptor({type:GradientCanvas, propertiesFactory:function ():Object{
                return ({width:147, height:127, childDescriptors:[new UIComponentDescriptor({type:Image, id:"tick", propertiesFactory:function ():Object{
                    return ({x:117, y:10, width:30, height:30});
                }}), new UIComponentDescriptor({type:Image, id:"_IconOption_Image2", propertiesFactory:function ():Object{
                    return ({x:25, y:0, width:90, height:90});
                }}), new UIComponentDescriptor({type:Image, id:"info_icon", events:{click:"__info_icon_click"}, propertiesFactory:function ():Object{
                    return ({x:120});
                }}), new UIComponentDescriptor({type:Label, id:"valueLabel", propertiesFactory:function ():Object{
                    return ({x:5, y:94, styleName:"weekday"});
                }}), new UIComponentDescriptor({type:TextArea, id:"description", propertiesFactory:function ():Object{
                    return ({x:10, y:94, styleName:"description"});
                }}), new UIComponentDescriptor({type:Image, propertiesFactory:function ():Object{
                    return ({x:105, y:10});
                }}), new UIComponentDescriptor({type:Image, propertiesFactory:function ():Object{
                    return ({x:105, y:10, height:32});
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
                this.fillColors = [0xFFFFFF, 0xCFCFCF];
                this.fillAlphas = [1, 1];
                this.gradientRatio = [0, 0xFF];
                this.angle = [90];
                this.borderAlphas = [0.5];
                this.cornerRadius = 5;
            };
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.width = 147;
            this.height = 127;
            this.colorsConfiguration = [2];
            this.currentState = "default";
            this.states = [_IconOption_State1_c(), _IconOption_State2_c()];
            _IconOption_Transition1_i();
            this.addEventListener("creationComplete", ___IconOption_GradientCanvas1_creationComplete);
        }
        public function get valueLabel():Label{
            return (this._2025208835valueLabel);
        }
        public function set valueLabel(_arg1:Label):void{
            var _local2:Object = this._2025208835valueLabel;
            if (_local2 !== _arg1){
                this._2025208835valueLabel = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "valueLabel", _local2, _arg1));
            };
        }
        private function init():void{
        }
        private function _IconOption_SetProperty13_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty13 = _local1;
            _local1.name = "y";
            _local1.value = 92;
            BindingManager.executeBindings(this, "_IconOption_SetProperty13", _IconOption_SetProperty13);
            return (_local1);
        }
        private function _IconOption_Move1_c():Move{
            var _local1:Move = new Move();
            _local1.duration = 1000;
            return (_local1);
        }
        private function _IconOption_Parallel1_c():Parallel{
            var _local1:Parallel = new Parallel();
            _local1.children = [_IconOption_Move1_c(), _IconOption_Resize1_c()];
            return (_local1);
        }
        public function ___IconOption_GradientCanvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function toggleInfo():void{
            dispatchEvent(new Event("toggleInfo", true));
        }
        private function _IconOption_Sequence1_i():Sequence{
            var _local1:Sequence = new Sequence();
            t1 = _local1;
            _local1.children = [_IconOption_Parallel1_c()];
            BindingManager.executeBindings(this, "t1", t1);
            return (_local1);
        }
        private function set _option(_arg1:Option):void{
            var _local2:Object = this._1698334100_option;
            if (_local2 !== _arg1){
                this._1698334100_option = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_option", _local2, _arg1));
            };
        }
        public function set selected(_arg1:Boolean):void{
            if (iconOption != null){
                iconOption.selected = _arg1;
            };
            _selected = _arg1;
        }
        private function _IconOption_SetProperty3_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty3 = _local1;
            _local1.name = "y";
            _local1.value = 125;
            BindingManager.executeBindings(this, "_IconOption_SetProperty3", _IconOption_SetProperty3);
            return (_local1);
        }
        public function set tick(_arg1:Image):void{
            var _local2:Object = this._3559837tick;
            if (_local2 !== _arg1){
                this._3559837tick = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tick", _local2, _arg1));
            };
        }
        private function _IconOption_SetProperty7_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty7 = _local1;
            _local1.name = "y";
            _local1.value = 92;
            BindingManager.executeBindings(this, "_IconOption_SetProperty7", _IconOption_SetProperty7);
            return (_local1);
        }
        public function get t1():Sequence{
            return (this._3645t1);
        }
        private function get iconPath():String{
            return (this._738054082iconPath);
        }
        private function _IconOption_SetProperty12_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty12 = _local1;
            _local1.name = "x";
            _local1.value = 17;
            BindingManager.executeBindings(this, "_IconOption_SetProperty12", _IconOption_SetProperty12);
            return (_local1);
        }
        public function get info_icon():Image{
            return (this._1231310186info_icon);
        }
        public function get option():Option{
            return (_option);
        }
        private function _IconOption_SetProperty2_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty2 = _local1;
            _local1.name = "y";
            BindingManager.executeBindings(this, "_IconOption_SetProperty2", _IconOption_SetProperty2);
            return (_local1);
        }
        public function set t1(_arg1:Sequence):void{
            var _local2:Object = this._3645t1;
            if (_local2 !== _arg1){
                this._3645t1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "t1", _local2, _arg1));
            };
        }
        private function _IconOption_SetProperty6_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty6 = _local1;
            _local1.name = "x";
            _local1.value = 17;
            BindingManager.executeBindings(this, "_IconOption_SetProperty6", _IconOption_SetProperty6);
            return (_local1);
        }
        private function set iconPath(_arg1:String):void{
            var _local2:Object = this._738054082iconPath;
            if (_local2 !== _arg1){
                this._738054082iconPath = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconPath", _local2, _arg1));
            };
        }
        private function _IconOption_State2_c():State{
            var _local1:State = new State();
            _local1.name = "default";
            _local1.overrides = [_IconOption_SetProperty9_i(), _IconOption_SetProperty10_i(), _IconOption_SetProperty11_i(), _IconOption_SetProperty12_i(), _IconOption_SetProperty13_i()];
            return (_local1);
        }
        private function _IconOption_bindingExprs():void{
            var _local1:*;
            _local1 = description;
            _local1 = info_icon;
            _local1 = ((203 - info_icon.height) - 8);
            _local1 = description;
            _local1 = description;
            _local1 = this;
            _local1 = valueLabel;
            _local1 = valueLabel;
            _local1 = description;
            _local1 = this;
            _local1 = description;
            _local1 = info_icon;
            _local1 = ((127 - info_icon.height) - 8);
            _local1 = valueLabel;
            _local1 = valueLabel;
            _local1 = _selected;
            _local1 = Assets.getInstance().tick;
            _local1 = iconPath;
            _local1 = Assets.getInstance().info_icon;
            _local1 = _option.Label;
            _local1 = _option.Description;
            _local1 = [this, info_icon];
        }
        private function _IconOption_SetProperty11_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty11 = _local1;
            _local1.name = "y";
            BindingManager.executeBindings(this, "_IconOption_SetProperty11", _IconOption_SetProperty11);
            return (_local1);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _IconOption_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_IconOptionWatcherSetupUtil");
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
        public function __info_icon_click(_arg1:MouseEvent):void{
            toggleInfo();
        }
        private function _IconOption_Transition1_i():Transition{
            var _local1:Transition = new Transition();
            _IconOption_Transition1 = _local1;
            _local1.fromState = "*";
            _local1.toState = "*";
            _local1.effect = _IconOption_Sequence1_i();
            return (_local1);
        }
        public function get selected():Boolean{
            return (_selected);
        }
        public function get tick():Image{
            return (this._3559837tick);
        }
        private function _IconOption_State1_c():State{
            var _local1:State = new State();
            _local1.name = "info";
            _local1.overrides = [_IconOption_SetProperty1_i(), _IconOption_SetProperty2_i(), _IconOption_SetProperty3_i(), _IconOption_SetProperty4_i(), _IconOption_SetProperty5_i(), _IconOption_SetProperty6_i(), _IconOption_SetProperty7_i(), _IconOption_SetProperty8_i()];
            return (_local1);
        }
        private function _IconOption_SetProperty5_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty5 = _local1;
            _local1.name = "height";
            _local1.value = 210;
            BindingManager.executeBindings(this, "_IconOption_SetProperty5", _IconOption_SetProperty5);
            return (_local1);
        }
        private function _IconOption_SetProperty1_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty1 = _local1;
            _local1.name = "visible";
            _local1.value = true;
            BindingManager.executeBindings(this, "_IconOption_SetProperty1", _IconOption_SetProperty1);
            return (_local1);
        }
        private function _IconOption_SetProperty9_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty9 = _local1;
            _local1.name = "height";
            _local1.value = 127;
            BindingManager.executeBindings(this, "_IconOption_SetProperty9", _IconOption_SetProperty9);
            return (_local1);
        }
        public function set info_icon(_arg1:Image):void{
            var _local2:Object = this._1231310186info_icon;
            if (_local2 !== _arg1){
                this._1231310186info_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info_icon", _local2, _arg1));
            };
        }
        private function _IconOption_Resize1_c():Resize{
            var _local1:Resize = new Resize();
            _local1.duration = 1000;
            return (_local1);
        }
        private function _IconOption_SetProperty10_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty10 = _local1;
            _local1.name = "visible";
            _local1.value = false;
            BindingManager.executeBindings(this, "_IconOption_SetProperty10", _IconOption_SetProperty10);
            return (_local1);
        }
        private function get _option():Option{
            return (this._1698334100_option);
        }
        public function set option(_arg1:Option):void{
            _option = _arg1;
            iconPath = (("assets/icons/" + option.Icon) + ".png");
            iconPath = option.Icon;
        }
        private function set _selected(_arg1:Boolean):void{
            var _local2:Object = this._1282089978_selected;
            if (_local2 !== _arg1){
                this._1282089978_selected = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_selected", _local2, _arg1));
            };
        }
        private function _IconOption_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (description);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty1.target = _arg1;
            }, "_IconOption_SetProperty1.target");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (info_icon);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty2.target = _arg1;
            }, "_IconOption_SetProperty2.target");
            result[1] = binding;
            binding = new Binding(this, function (){
                return (((203 - info_icon.height) - 8));
            }, function (_arg1):void{
                _IconOption_SetProperty2.value = _arg1;
            }, "_IconOption_SetProperty2.value");
            result[2] = binding;
            binding = new Binding(this, function ():Object{
                return (description);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty3.target = _arg1;
            }, "_IconOption_SetProperty3.target");
            result[3] = binding;
            binding = new Binding(this, function ():Object{
                return (description);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty4.target = _arg1;
            }, "_IconOption_SetProperty4.target");
            result[4] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty5.target = _arg1;
            }, "_IconOption_SetProperty5.target");
            result[5] = binding;
            binding = new Binding(this, function ():Object{
                return (valueLabel);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty6.target = _arg1;
            }, "_IconOption_SetProperty6.target");
            result[6] = binding;
            binding = new Binding(this, function ():Object{
                return (valueLabel);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty7.target = _arg1;
            }, "_IconOption_SetProperty7.target");
            result[7] = binding;
            binding = new Binding(this, function ():Object{
                return (description);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty8.target = _arg1;
            }, "_IconOption_SetProperty8.target");
            result[8] = binding;
            binding = new Binding(this, function ():Object{
                return (this);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty9.target = _arg1;
            }, "_IconOption_SetProperty9.target");
            result[9] = binding;
            binding = new Binding(this, function ():Object{
                return (description);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty10.target = _arg1;
            }, "_IconOption_SetProperty10.target");
            result[10] = binding;
            binding = new Binding(this, function ():Object{
                return (info_icon);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty11.target = _arg1;
            }, "_IconOption_SetProperty11.target");
            result[11] = binding;
            binding = new Binding(this, function (){
                return (((127 - info_icon.height) - 8));
            }, function (_arg1):void{
                _IconOption_SetProperty11.value = _arg1;
            }, "_IconOption_SetProperty11.value");
            result[12] = binding;
            binding = new Binding(this, function ():Object{
                return (valueLabel);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty12.target = _arg1;
            }, "_IconOption_SetProperty12.target");
            result[13] = binding;
            binding = new Binding(this, function ():Object{
                return (valueLabel);
            }, function (_arg1:Object):void{
                _IconOption_SetProperty13.target = _arg1;
            }, "_IconOption_SetProperty13.target");
            result[14] = binding;
            binding = new Binding(this, function ():Boolean{
                return (_selected);
            }, function (_arg1:Boolean):void{
                tick.visible = _arg1;
            }, "tick.visible");
            result[15] = binding;
            binding = new Binding(this, function ():Object{
                return (Assets.getInstance().tick);
            }, function (_arg1:Object):void{
                tick.source = _arg1;
            }, "tick.source");
            result[16] = binding;
            binding = new Binding(this, function ():Object{
                return (iconPath);
            }, function (_arg1:Object):void{
                _IconOption_Image2.source = _arg1;
            }, "_IconOption_Image2.source");
            result[17] = binding;
            binding = new Binding(this, function ():Object{
                return (Assets.getInstance().info_icon);
            }, function (_arg1:Object):void{
                info_icon.source = _arg1;
            }, "info_icon.source");
            result[18] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _option.Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                valueLabel.text = _arg1;
            }, "valueLabel.text");
            result[19] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _option.Description;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                description.text = _arg1;
            }, "description.text");
            result[20] = binding;
            binding = new Binding(this, function ():Array{
                return ([this, info_icon]);
            }, function (_arg1:Array):void{
                t1.targets = _arg1;
            }, "t1.targets");
            result[21] = binding;
            return (result);
        }
        public function set description(_arg1:TextArea):void{
            var _local2:Object = this._1724546052description;
            if (_local2 !== _arg1){
                this._1724546052description = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "description", _local2, _arg1));
            };
        }
        private function _IconOption_SetProperty4_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty4 = _local1;
            _local1.name = "width";
            _local1.value = 127;
            BindingManager.executeBindings(this, "_IconOption_SetProperty4", _IconOption_SetProperty4);
            return (_local1);
        }
        private function get _selected():Boolean{
            return (this._1282089978_selected);
        }
        public function get description():TextArea{
            return (this._1724546052description);
        }
        private function _IconOption_SetProperty8_i():SetProperty{
            var _local1:SetProperty = new SetProperty();
            _IconOption_SetProperty8 = _local1;
            _local1.name = "x";
            _local1.value = 20;
            BindingManager.executeBindings(this, "_IconOption_SetProperty8", _IconOption_SetProperty8);
            return (_local1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            IconOption._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
