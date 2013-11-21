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

    public class EditableListElement extends Canvas implements IBindingClient {

        mx_internal var _bindingsByDestination:Object
        mx_internal var _bindingsBeginWithWord:Object
        mx_internal var _watchers:Array
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        private var _1058056547textInput:TextInput
        private var _documentDescriptor_:UIComponentDescriptor
        private var _619469534_listElementObject:Object
        private var _1678958759close_button:Button

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function EditableListElement(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:370, childDescriptors:[new UIComponentDescriptor({type:Button, id:"close_button", propertiesFactory:function ():Object{
                    return ({x:313, y:2});
                }}), new UIComponentDescriptor({type:TextInput, id:"textInput", stylesFactory:function ():void{
                    this.backgroundColor = 0xFFFFFF;
                    this.backgroundAlpha = 1;
                }, propertiesFactory:function ():Object{
                    return ({editable:true, x:5, y:2, width:300, height:23});
                }})]});
            }});
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
                this.backgroundAlpha = 0;
            };
            this.width = 370;
        }
        private function set _listElementObject(_arg1:Object):void{
            var _local2:Object = this._619469534_listElementObject;
            if (_local2 !== _arg1){
                this._619469534_listElementObject = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_listElementObject", _local2, _arg1));
            };
        }
        private function _EditableListElement_bindingExprs():void{
            var _local1:*;
            _local1 = model.languageVO.getLang("remove");
            _local1 = _listElementObject.text;
        }
        public function set textInput(_arg1:TextInput):void{
            var _local2:Object = this._1058056547textInput;
            if (_local2 !== _arg1){
                this._1058056547textInput = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "textInput", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _EditableListElement_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_EditableListElementWatcherSetupUtil");
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
        public function get close_button():Button{
            return (this._1678958759close_button);
        }
        public function get listElementObject():Object{
            return (_listElementObject);
        }
        private function get _listElementObject():Object{
            return (this._619469534_listElementObject);
        }
        public function get textInput():TextInput{
            return (this._1058056547textInput);
        }
        private function _EditableListElement_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("remove");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                close_button.label = _arg1;
            }, "close_button.label");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _listElementObject.text;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                textInput.text = _arg1;
            }, "textInput.text");
            result[1] = binding;
            return (result);
        }
        public function set close_button(_arg1:Button):void{
            var _local2:Object = this._1678958759close_button;
            if (_local2 !== _arg1){
                this._1678958759close_button = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "close_button", _local2, _arg1));
            };
        }
        public function set listElementObject(_arg1:Object):void{
            _listElementObject = _arg1;
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            EditableListElement._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
