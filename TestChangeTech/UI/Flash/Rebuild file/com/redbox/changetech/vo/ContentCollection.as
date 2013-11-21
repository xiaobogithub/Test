//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;

    public class ContentCollection implements IValueObject, IEventDispatcher {

        private var _bindingEventDispatcher:EventDispatcher
        private var _502677702Contents:Array
        private var _1259292133DayNumber:Number
        private var _2622298Type:String
        private var _77076827Phase:String
        private var _126857307Feedback

        private static var _2068553788RELAPSE_THERAPY:String = "RelapseTherapy";
        private static var _1925346054ACTIVE:String = "Active";
        private static var _staticBindingEventDispatcher:EventDispatcher = new EventDispatcher();
        private static var _609943785LAPSE_THERAPY:String = "LapseTherapy";
        private static var _156146159COUNTDOWN:String = "Countdown";
        private static var _56111140COMPLETION:String = "Completion";

        public function ContentCollection(_arg1:Object=null){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function set Type(_arg1:String):void{
            var _local2:Object = this._2622298Type;
            if (_local2 !== _arg1){
                this._2622298Type = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Type", _local2, _arg1));
            };
        }
        public function set Phase(_arg1:String):void{
            var _local2:Object = this._77076827Phase;
            if (_local2 !== _arg1){
                this._77076827Phase = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Phase", _local2, _arg1));
            };
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function set Feedback(_arg1):void{
            var _local2:Object = this._126857307Feedback;
            if (_local2 !== _arg1){
                this._126857307Feedback = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Feedback", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function set DayNumber(_arg1:Number):void{
            var _local2:Object = this._1259292133DayNumber;
            if (_local2 !== _arg1){
                this._1259292133DayNumber = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "DayNumber", _local2, _arg1));
            };
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function get Type():String{
            return (this._2622298Type);
        }
        public function get Phase():String{
            return (this._77076827Phase);
        }
        public function get Feedback(){
            return (this._126857307Feedback);
        }
        public function get DayNumber():Number{
            return (this._1259292133DayNumber);
        }
        public function set Contents(_arg1:Array):void{
            var _local2:Object = this._502677702Contents;
            if (_local2 !== _arg1){
                this._502677702Contents = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Contents", _local2, _arg1));
            };
        }
        public function get Contents():Array{
            return (this._502677702Contents);
        }

        public static function get COMPLETION():String{
            return (ContentCollection._56111140COMPLETION);
        }
        public static function get RELAPSE_THERAPY():String{
            return (ContentCollection._2068553788RELAPSE_THERAPY);
        }
        public static function get ACTIVE():String{
            return (ContentCollection._1925346054ACTIVE);
        }
        public static function set COMPLETION(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = ContentCollection._56111140COMPLETION;
            if (_local2 !== _arg1){
                ContentCollection._56111140COMPLETION = _arg1;
                _local3 = ContentCollection.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(ContentCollection, "COMPLETION", _local2, _arg1));
                };
            };
        }
        public static function set RELAPSE_THERAPY(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = ContentCollection._2068553788RELAPSE_THERAPY;
            if (_local2 !== _arg1){
                ContentCollection._2068553788RELAPSE_THERAPY = _arg1;
                _local3 = ContentCollection.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(ContentCollection, "RELAPSE_THERAPY", _local2, _arg1));
                };
            };
        }
        public static function set ACTIVE(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = ContentCollection._1925346054ACTIVE;
            if (_local2 !== _arg1){
                ContentCollection._1925346054ACTIVE = _arg1;
                _local3 = ContentCollection.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(ContentCollection, "ACTIVE", _local2, _arg1));
                };
            };
        }
        public static function set LAPSE_THERAPY(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = ContentCollection._609943785LAPSE_THERAPY;
            if (_local2 !== _arg1){
                ContentCollection._609943785LAPSE_THERAPY = _arg1;
                _local3 = ContentCollection.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(ContentCollection, "LAPSE_THERAPY", _local2, _arg1));
                };
            };
        }
        public static function get COUNTDOWN():String{
            return (ContentCollection._156146159COUNTDOWN);
        }
        public static function get staticEventDispatcher():IEventDispatcher{
            return (_staticBindingEventDispatcher);
        }
        public static function get LAPSE_THERAPY():String{
            return (ContentCollection._609943785LAPSE_THERAPY);
        }
        public static function set COUNTDOWN(_arg1:String):void{
            var _local3:IEventDispatcher;
            var _local2:Object = ContentCollection._156146159COUNTDOWN;
            if (_local2 !== _arg1){
                ContentCollection._156146159COUNTDOWN = _arg1;
                _local3 = ContentCollection.staticEventDispatcher;
                if (_local3 != null){
                    _local3.dispatchEvent(PropertyChangeEvent.createUpdateEvent(ContentCollection, "COUNTDOWN", _local2, _arg1));
                };
            };
        }

    }
}//package com.redbox.changetech.vo 
