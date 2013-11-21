//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;

    public class Button implements IEventDispatcher {

        private var _73174740Label:String
        private var _2622298Type:String
        private var _1066662835IdButton:Number
        private var _bindingEventDispatcher:EventDispatcher
        private var _583022632ButtonAction:ButtonActionVO

        private static var _919234517EMBED_1:String = "Embed1";
        private static var _919234516EMBED_2:String = "Embed2";
        private static var _403216866PRIMARY:String = "Primary";
        private static var _staticBindingEventDispatcher:EventDispatcher = new EventDispatcher();
        private static var _392169390TERTIARY:String = "Tertiary";
        private static var _1968996692SECONDARY:String = "Secondary";

        public function Button(){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function set Label(_arg1:String):void{
            var _local2:Object = this._73174740Label;
            if (_local2 !== _arg1){
                this._73174740Label = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Label", _local2, _arg1));
            };
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function get IdButton():Number{
            return (this._1066662835IdButton);
        }
        public function set ButtonAction(_arg1:ButtonActionVO):void{
            var _local2:Object = this._583022632ButtonAction;
            if (_local2 !== _arg1){
                this._583022632ButtonAction = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ButtonAction", _local2, _arg1));
            };
        }
        public function get Type():String{
            return (this._2622298Type);
        }
        public function get Label():String{
            return (this._73174740Label);
        }
        public function set IdButton(_arg1:Number):void{
            var _local2:Object = this._1066662835IdButton;
            if (_local2 !== _arg1){
                this._1066662835IdButton = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "IdButton", _local2, _arg1));
            };
        }
        public function get ButtonAction():ButtonActionVO{
            return (this._583022632ButtonAction);
        }
        public function set Type(_arg1:String):void{
            var _local2:Object = this._2622298Type;
            if (_local2 !== _arg1){
                this._2622298Type = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Type", _local2, _arg1));
            };
        }

        public static function get staticEventDispatcher():IEventDispatcher{
            return (_staticBindingEventDispatcher);
        }
        public static function set SECONDARY(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = Button._1968996692SECONDARY;
            if (_local2 !== _arg1){
                Button._1968996692SECONDARY = _arg1;
                _local3 = Button.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(Button, "SECONDARY", _local2, _arg1));
                };
            };
        }
        public static function set PRIMARY(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = Button._403216866PRIMARY;
            if (_local2 !== _arg1){
                Button._403216866PRIMARY = _arg1;
                _local3 = Button.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(Button, "PRIMARY", _local2, _arg1));
                };
            };
        }
        public static function get EMBED_2():String{
            return (Button._919234516EMBED_2);
        }
        public static function get EMBED_1():String{
            return (Button._919234517EMBED_1);
        }
        public static function get PRIMARY():String{
            return (Button._403216866PRIMARY);
        }
        public static function set TERTIARY(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = Button._392169390TERTIARY;
            if (_local2 !== _arg1){
                Button._392169390TERTIARY = _arg1;
                _local3 = Button.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(Button, "TERTIARY", _local2, _arg1));
                };
            };
        }
        public static function get SECONDARY():String{
            return (Button._1968996692SECONDARY);
        }
        public static function set EMBED_1(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = Button._919234517EMBED_1;
            if (_local2 !== _arg1){
                Button._919234517EMBED_1 = _arg1;
                _local3 = Button.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(Button, "EMBED_1", _local2, _arg1));
                };
            };
        }
        public static function set EMBED_2(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = Button._919234516EMBED_2;
            if (_local2 !== _arg1){
                Button._919234516EMBED_2 = _arg1;
                _local3 = Button.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(Button, "EMBED_2", _local2, _arg1));
                };
            };
        }
        public static function get TERTIARY():String{
            return (Button._392169390TERTIARY);
        }

    }
}//package com.redbox.changetech.vo 
