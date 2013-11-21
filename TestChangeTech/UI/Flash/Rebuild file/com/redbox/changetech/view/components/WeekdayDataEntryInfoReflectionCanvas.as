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
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class WeekdayDataEntryInfoReflectionCanvas extends Canvas implements IBindingClient {

        mx_internal var _bindingsBeginWithWord:Object
        private var _736810656reflector:Reflector
        mx_internal var _bindings:Array
        private var _2009958326dataEntryInfo:WeekdayDataEntryInfo
        private var _documentDescriptor_:UIComponentDescriptor
        mx_internal var _bindingsByDestination:Object
        mx_internal var _watchers:Array

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function WeekdayDataEntryInfoReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:WeekdayDataEntryInfo, id:"dataEntryInfo", propertiesFactory:function ():Object{
                    return ({height:130});
                }}), new UIComponentDescriptor({type:Reflector, id:"reflector", propertiesFactory:function ():Object{
                    return ({falloff:0.4, alpha:0.4, y:130});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
        }
        private function _WeekdayDataEntryInfoReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (this.width);
            }, function (_arg1:Number):void{
                dataEntryInfo.width = _arg1;
            }, "dataEntryInfo.width");
            result[0] = binding;
            binding = new Binding(this, function ():UIComponent{
                return (dataEntryInfo);
            }, function (_arg1:UIComponent):void{
                reflector.target = _arg1;
            }, "reflector.target");
            result[1] = binding;
            return (result);
        }
        public function get reflector():Reflector{
            return (this._736810656reflector);
        }
        private function _WeekdayDataEntryInfoReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = this.width;
            _local1 = dataEntryInfo;
        }
        public function get dataEntryInfo():WeekdayDataEntryInfo{
            return (this._2009958326dataEntryInfo);
        }
        public function set reflector(_arg1:Reflector):void{
            var _local2:Object = this._736810656reflector;
            if (_local2 !== _arg1){
                this._736810656reflector = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reflector", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _WeekdayDataEntryInfoReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_WeekdayDataEntryInfoReflectionCanvasWatcherSetupUtil");
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
        public function set dataEntryInfo(_arg1:WeekdayDataEntryInfo):void{
            var _local2:Object = this._2009958326dataEntryInfo;
            if (_local2 !== _arg1){
                this._2009958326dataEntryInfo = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "dataEntryInfo", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            WeekdayDataEntryInfoReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
