//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.adobe.cairngorm.vo.*;
    import com.redbox.changetech.util.Enumerations.*;

    public dynamic class Consumption implements IValueObject, IEventDispatcher {

        private var _1273415943DayOfWeek:String
        private var _2021313932Closed:Boolean = false
        private var _629421623ConsumptionDate:Date
        private var _550869271Modified:Boolean = false
        private var _1788538891ReportedValues:Array
        private var _197343320PlanValue:int
        private var _bindingEventDispatcher:EventDispatcher

        public function Consumption(_arg1:String=null, _arg2:Date=null, _arg3:int=0){
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
            this.DayOfWeek = _arg1;
            this.DateTime = _arg2;
            this.ReportedValues = [new ConsumptionValue(DrinkType.Beer.Text), new ConsumptionValue(DrinkType.Wine.Text), new ConsumptionValue(DrinkType.Spirit.Text), new ConsumptionValue(DrinkType.Total.Text)];
            this.PlanValue = _arg3;
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function total(_arg1:Boolean){
            var total:* = undefined;
            var returnString:* = _arg1;
            try {
                total = ((ReportedValues[0].Value + ReportedValues[1].Value) + ReportedValues[2].Value);
                if (returnString){
                    total = (String(total) + " Glass");
                };
            } catch(error:Error) {
                if (returnString){
                    total = "-";
                } else {
                    total = 0;
                };
            };
            return (total);
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get DayOfWeek():String{
            return (this._1273415943DayOfWeek);
        }
        public function get ConsumptionDate():Date{
            return (this._629421623ConsumptionDate);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function get Closed():Boolean{
            return (this._2021313932Closed);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function set PlanValue(_arg1:int):void{
            var _local2:Object = this._197343320PlanValue;
            if (_local2 !== _arg1){
                this._197343320PlanValue = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "PlanValue", _local2, _arg1));
            };
        }
        public function set ConsumptionDate(_arg1:Date):void{
            var _local2:Object = this._629421623ConsumptionDate;
            if (_local2 !== _arg1){
                this._629421623ConsumptionDate = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ConsumptionDate", _local2, _arg1));
            };
        }
        public function set DayOfWeek(_arg1:String):void{
            var _local2:Object = this._1273415943DayOfWeek;
            if (_local2 !== _arg1){
                this._1273415943DayOfWeek = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "DayOfWeek", _local2, _arg1));
            };
        }
        public function set Closed(_arg1:Boolean):void{
            var _local2:Object = this._2021313932Closed;
            if (_local2 !== _arg1){
                this._2021313932Closed = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Closed", _local2, _arg1));
            };
        }
        public function set Modified(_arg1:Boolean):void{
            var _local2:Object = this._550869271Modified;
            if (_local2 !== _arg1){
                this._550869271Modified = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Modified", _local2, _arg1));
            };
        }
        public function get PlanValue():int{
            return (this._197343320PlanValue);
        }
        public function get Modified():Boolean{
            return (this._550869271Modified);
        }
        public function set ReportedValues(_arg1:Array):void{
            var _local2:Object = this._1788538891ReportedValues;
            if (_local2 !== _arg1){
                this._1788538891ReportedValues = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ReportedValues", _local2, _arg1));
            };
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function get ReportedValues():Array{
            return (this._1788538891ReportedValues);
        }

    }
}//package com.redbox.changetech.vo 
