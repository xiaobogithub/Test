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

    public class DropDownItemRenderer extends Canvas implements IBindingClient {

        private var _1672910234labelField:Label
        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var _816711803iconHolder:Image
        mx_internal var _bindings:Array
        private var _3321844line:HRule
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function DropDownItemRenderer(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:HRule, id:"line", stylesFactory:function ():void{
                    this.strokeColor = 0xE2E2E2;
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:95, y:0});
                }}), new UIComponentDescriptor({type:Image, id:"iconHolder", propertiesFactory:function ():Object{
                    return ({x:5, y:9});
                }}), new UIComponentDescriptor({type:Label, id:"labelField", propertiesFactory:function ():Object{
                    return ({y:9});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.horizontalScrollPolicy = "off";
        }
        public function get iconHolder():Image{
            return (this._816711803iconHolder);
        }
        public function set iconHolder(_arg1:Image):void{
            var _local2:Object = this._816711803iconHolder;
            if (_local2 !== _arg1){
                this._816711803iconHolder = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "iconHolder", _local2, _arg1));
            };
        }
        public function set labelField(_arg1:Label):void{
            var _local2:Object = this._1672910234labelField;
            if (_local2 !== _arg1){
                this._1672910234labelField = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "labelField", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _DropDownItemRenderer_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_DropDownItemRendererWatcherSetupUtil");
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
        private function _DropDownItemRenderer_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Boolean{
                return (data.hasLine);
            }, function (_arg1:Boolean):void{
                line.visible = _arg1;
            }, "line.visible");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (((width - line.width) / 2));
            }, function (_arg1:Number):void{
                line.x = _arg1;
            }, "line.x");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (data.icon);
            }, function (_arg1:Object):void{
                iconHolder.source = _arg1;
            }, "iconHolder.source");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = data.label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                labelField.text = _arg1;
            }, "labelField.text");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return (((iconHolder.x + iconHolder.width) + 3));
            }, function (_arg1:Number):void{
                labelField.x = _arg1;
            }, "labelField.x");
            result[4] = binding;
            return (result);
        }
        public function get labelField():Label{
            return (this._1672910234labelField);
        }
        public function set line(_arg1:HRule):void{
            var _local2:Object = this._3321844line;
            if (_local2 !== _arg1){
                this._3321844line = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "line", _local2, _arg1));
            };
        }
        private function _DropDownItemRenderer_bindingExprs():void{
            var _local1:*;
            _local1 = data.hasLine;
            _local1 = ((width - line.width) / 2);
            _local1 = data.icon;
            _local1 = data.label;
            _local1 = ((iconHolder.x + iconHolder.width) + 3);
        }
        public function get line():HRule{
            return (this._3321844line);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            DropDownItemRenderer._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
