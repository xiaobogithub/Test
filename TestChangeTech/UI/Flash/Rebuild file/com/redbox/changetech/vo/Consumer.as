//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import flash.events.*;
    import mx.events.*;
    import com.redbox.changetech.util.*;
    import com.adobe.cairngorm.vo.*;

    public class Consumer implements IValueObject, IEventDispatcher {

        private var _537348043MobilePhoneNumber:String
        private var _1280996730DateOfBirth:Date
        private var _904465283CurrentDay:Number
        private var _1159285896CompanyName:String
        private var _478179356ReductionPlan:Array
        private var _591149656CompanyId:String
        private var _906611496EmailAddress:String
        private var _1394955679LastName:String
        private var _2129321697Gender:Object
        private var _2136803643FirstName:String
        private var _125810928StartDate:Date
        private var _1019501267ScreeningColor:String
        private var _2363Id:Number
        private var _2026347353CurrentDate:Date
        private var _634643566ReportedUsage:Array
        private var _bindingEventDispatcher:EventDispatcher
        private var _1005079460ScreeningScore:Number
        private var _2230953Guid:Object
        private var _65759Age:Number
        private var _201069322Username:String
        private var _1281629883Password:String
        private var _822106797Postcode:String
        private var _1719226422InitialReportedUsage:Array

        public function Consumer(){
            var _local2:Consumption;
            var _local3:Consumption;
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
            ReductionPlan = [];
            ReportedUsage = [];
            var _local1:Number = 0;
            while (_local1 < Config.WEEKDAYS.length) {
                _local2 = new Consumption(Config.WEEKDAYS[_local1], new Date());
                _local2.ConsumptionDate = new Date();
                ReportedUsage.push(_local2);
                _local3 = new Consumption(Config.WEEKDAYS[_local1], new Date());
                ReductionPlan.push(_local3);
                _local1++;
            };
        }
        public function set CurrentDay(_arg1:Number):void{
            var _local2:Object = this._904465283CurrentDay;
            if (_local2 !== _arg1){
                this._904465283CurrentDay = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "CurrentDay", _local2, _arg1));
            };
        }
        public function get StartDate():Date{
            return (this._125810928StartDate);
        }
        public function set CompanyName(_arg1:String):void{
            var _local2:Object = this._1159285896CompanyName;
            if (_local2 !== _arg1){
                this._1159285896CompanyName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "CompanyName", _local2, _arg1));
            };
        }
        public function get ReductionPlan():Array{
            return (this._478179356ReductionPlan);
        }
        public function set StartDate(_arg1:Date):void{
            var _local2:Object = this._125810928StartDate;
            if (_local2 !== _arg1){
                this._125810928StartDate = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "StartDate", _local2, _arg1));
            };
        }
        public function get ScreeningColor():String{
            return (this._1019501267ScreeningColor);
        }
        public function get MobilePhoneNumber():String{
            return (this._537348043MobilePhoneNumber);
        }
        public function set ReductionPlan(_arg1:Array):void{
            var _local2:Object = this._478179356ReductionPlan;
            if (_local2 !== _arg1){
                this._478179356ReductionPlan = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ReductionPlan", _local2, _arg1));
            };
        }
        public function get Id():Number{
            return (this._2363Id);
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function get FirstName():String{
            return (this._2136803643FirstName);
        }
        public function getCurrentDayTarget():String{
            var _local3:Consumption;
            var _local4:String;
            if (CurrentDate == null){
                return ("00");
            };
            var _local1:String = WeekDayUtils.getDayOfWeekString(CurrentDate.day);
            var _local2:Number = 0;
            while (_local2 < ReductionPlan.length) {
                _local3 = ReductionPlan[_local2];
                if (_local3.DayOfWeek.toLowerCase() == _local1.toLowerCase()){
                    _local4 = String(_local3.PlanValue);
                    if (_local4.length == 1){
                        _local4 = ("0" + _local4);
                    };
                    return (_local4);
                };
                _local2++;
            };
            return ("00");
        }
        public function getYesterdayDayConsumption():Number{
            var _local1:Number;
            var _local4:Consumption;
            if (CurrentDate == null){
                return (0);
            };
            var _local2:String = WeekDayUtils.getDayOfWeekString((CurrentDate.day - 1));
            var _local3:Number = 0;
            while (_local3 < ReportedUsage.length) {
                _local4 = ReportedUsage[_local3];
                trace(((_local4.DayOfWeek.toLowerCase() + ":") + _local2.toLowerCase()));
                if (_local4.DayOfWeek.toLowerCase() == _local2.toLowerCase()){
                    _local1 = _local4.total(false);
                };
                _local3++;
            };
            return (_local1);
        }
        public function set CurrentDate(_arg1:Date):void{
            var _local2:Object = this._2026347353CurrentDate;
            if (_local2 !== _arg1){
                this._2026347353CurrentDate = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "CurrentDate", _local2, _arg1));
            };
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set Guid(_arg1:Object):void{
            var _local2:Object = this._2230953Guid;
            if (_local2 !== _arg1){
                this._2230953Guid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Guid", _local2, _arg1));
            };
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get CompanyId():String{
            return (this._591149656CompanyId);
        }
        public function set ScreeningColor(_arg1:String):void{
            var _local2:Object = this._1019501267ScreeningColor;
            if (_local2 !== _arg1){
                this._1019501267ScreeningColor = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ScreeningColor", _local2, _arg1));
            };
        }
        public function get LastName():String{
            return (this._1394955679LastName);
        }
        public function get ScreeningScore():Number{
            return (this._1005079460ScreeningScore);
        }
        public function set Postcode(_arg1:String):void{
            var _local2:Object = this._822106797Postcode;
            if (_local2 !== _arg1){
                this._822106797Postcode = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Postcode", _local2, _arg1));
            };
        }
        public function set MobilePhoneNumber(_arg1:String):void{
            var _local2:Object = this._537348043MobilePhoneNumber;
            if (_local2 !== _arg1){
                this._537348043MobilePhoneNumber = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "MobilePhoneNumber", _local2, _arg1));
            };
        }
        public function get ReportedUsage():Array{
            return (this._634643566ReportedUsage);
        }
        public function getWeeksReductionPlan():Number{
            var _local3:Consumption;
            var _local1:Number = 0;
            var _local2:Number = 0;
            while (_local2 < ReductionPlan.length) {
                _local3 = ReductionPlan[_local2];
                _local1 = (_local1 + _local3.total(false));
                _local2++;
            };
            return (_local1);
        }
        public function set Id(_arg1:Number):void{
            var _local2:Object = this._2363Id;
            if (_local2 !== _arg1){
                this._2363Id = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Id", _local2, _arg1));
            };
        }
        public function get DateOfBirth():Date{
            return (this._1280996730DateOfBirth);
        }
        public function set Password(_arg1:String):void{
            var _local2:Object = this._1281629883Password;
            if (_local2 !== _arg1){
                this._1281629883Password = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Password", _local2, _arg1));
            };
        }
        public function getCurrentDayConsumption():Number{
            var _local1:Number;
            var _local4:Consumption;
            if (CurrentDate == null){
                return (0);
            };
            var _local2:String = WeekDayUtils.getDayOfWeekString(CurrentDate.day);
            var _local3:Number = 0;
            while (_local3 < ReportedUsage.length) {
                _local4 = ReportedUsage[_local3];
                trace(((_local4.DayOfWeek.toLowerCase() + ":") + _local2.toLowerCase()));
                if (_local4.DayOfWeek.toLowerCase() == _local2.toLowerCase()){
                    _local1 = _local4.total(false);
                };
                _local3++;
            };
            return (_local1);
        }
        public function get CurrentDay():Number{
            return (this._904465283CurrentDay);
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get CompanyName():String{
            return (this._1159285896CompanyName);
        }
        public function set InitialReportedUsage(_arg1:Array):void{
            var _local2:Object = this._1719226422InitialReportedUsage;
            if (_local2 !== _arg1){
                this._1719226422InitialReportedUsage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "InitialReportedUsage", _local2, _arg1));
            };
        }
        public function get CurrentDate():Date{
            return (this._2026347353CurrentDate);
        }
        public function get Guid():Object{
            return (this._2230953Guid);
        }
        public function get Postcode():String{
            return (this._822106797Postcode);
        }
        public function set FirstName(_arg1:String):void{
            var _local2:Object = this._2136803643FirstName;
            if (_local2 !== _arg1){
                this._2136803643FirstName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "FirstName", _local2, _arg1));
            };
        }
        public function set CompanyId(_arg1:String):void{
            var _local2:Object = this._591149656CompanyId;
            if (_local2 !== _arg1){
                this._591149656CompanyId = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "CompanyId", _local2, _arg1));
            };
        }
        public function set EmailAddress(_arg1:String):void{
            var _local2:Object = this._906611496EmailAddress;
            if (_local2 !== _arg1){
                this._906611496EmailAddress = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "EmailAddress", _local2, _arg1));
            };
        }
        public function set Age(_arg1:Number):void{
            var _local2:Object = this._65759Age;
            if (_local2 !== _arg1){
                this._65759Age = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Age", _local2, _arg1));
            };
        }
        public function set ScreeningScore(_arg1:Number):void{
            var _local2:Object = this._1005079460ScreeningScore;
            if (_local2 !== _arg1){
                this._1005079460ScreeningScore = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ScreeningScore", _local2, _arg1));
            };
        }
        public function get Password():String{
            return (this._1281629883Password);
        }
        public function getDaysDrinkingInWeek():Number{
            var _local3:Consumption;
            var _local1:Number = 0;
            var _local2:Number = 0;
            while (_local2 < ReportedUsage.length) {
                _local3 = ReportedUsage[_local2];
                if (_local3.total(false) > 0){
                    _local1 = (_local1 + 1);
                };
                _local2++;
            };
            return (_local1);
        }
        public function getWeeksUsage():Number{
            var _local3:Consumption;
            var _local1:Number = 0;
            var _local2:Number = 0;
            while (_local2 < ReportedUsage.length) {
                _local3 = ReportedUsage[_local2];
                _local1 = (_local1 + _local3.total(false));
                _local2++;
            };
            return (_local1);
        }
        public function get InitialReportedUsage():Array{
            return (this._1719226422InitialReportedUsage);
        }
        public function set LastName(_arg1:String):void{
            var _local2:Object = this._1394955679LastName;
            if (_local2 !== _arg1){
                this._1394955679LastName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "LastName", _local2, _arg1));
            };
        }
        public function set ReportedUsage(_arg1:Array):void{
            var _local2:Object = this._634643566ReportedUsage;
            if (_local2 !== _arg1){
                this._634643566ReportedUsage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ReportedUsage", _local2, _arg1));
            };
        }
        public function get EmailAddress():String{
            return (this._906611496EmailAddress);
        }
        public function get Gender():Object{
            return (this._2129321697Gender);
        }
        public function set Username(_arg1:String):void{
            var _local2:Object = this._201069322Username;
            if (_local2 !== _arg1){
                this._201069322Username = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Username", _local2, _arg1));
            };
        }
        public function get Age():Number{
            return (this._65759Age);
        }
        public function set Gender(_arg1:Object):void{
            var _local2:Object = this._2129321697Gender;
            if (_local2 !== _arg1){
                this._2129321697Gender = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "Gender", _local2, _arg1));
            };
        }
        public function set DateOfBirth(_arg1:Date):void{
            var _local2:Object = this._1280996730DateOfBirth;
            if (_local2 !== _arg1){
                this._1280996730DateOfBirth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "DateOfBirth", _local2, _arg1));
            };
        }
        public function get Username():String{
            return (this._201069322Username);
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function updateReportedValues():void{
            var _local2:Consumption;
            var _local1:Number = 0;
            while (_local1 < ReportedUsage.length) {
                _local2 = ReportedUsage[_local1];
                _local1++;
            };
        }

    }
}//package com.redbox.changetech.vo 
