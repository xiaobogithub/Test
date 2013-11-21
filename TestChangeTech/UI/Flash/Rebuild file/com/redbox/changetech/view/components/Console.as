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
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
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

    public class Console extends Canvas {

        private var _1901137025screenTestGender:TextInput
        private var _96511age:NumericStepper
        private var _1026450970reg_password:TextInput
        private var _265713450username:TextInput
        private var _1216985755password:TextInput
        private var _1088999581currentDay:NumericStepper
        private var _1784863809reg_userName:TextInput
        private var _132835675firstName:TextInput
        private var _1459599807lastName:TextInput
        private var _181110699mobilePhoneNumber:TextInput
        private var _1070931784emailAddress:TextInput
        private var _318184504preview:TextInput
        private var _1249512767gender:TextInput
        private var _documentDescriptor_:UIComponentDescriptor

        public function Console(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({width:800, height:800, childDescriptors:[new UIComponentDescriptor({type:Button, events:{click:"___Console_Button1_click"}, propertiesFactory:function ():Object{
                    return ({label:"LOGIN", width:186, x:10, y:82});
                }}), new UIComponentDescriptor({type:TextInput, id:"username", propertiesFactory:function ():Object{
                    return ({x:320, y:82, text:"login username"});
                }}), new UIComponentDescriptor({type:TextInput, id:"password", propertiesFactory:function ():Object{
                    return ({x:602, y:82, text:"login password"});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button2_click"}, propertiesFactory:function ():Object{
                    return ({label:"COLLECTION_COMPLETE", width:186, x:10, y:112});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button3_click"}, propertiesFactory:function ():Object{
                    return ({label:"COMPLETE_SCREENING", width:186, x:10, y:142});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button4_click"}, propertiesFactory:function ():Object{
                    return ({label:"CREATE_CONSUMER", width:186, x:10, y:172});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button5_click"}, propertiesFactory:function ():Object{
                    return ({label:"GET_PREVIEW", width:186, x:10, y:305});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button6_click"}, propertiesFactory:function ():Object{
                    return ({label:"INITIALISE", width:186, x:10, y:335});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button7_click"}, propertiesFactory:function ():Object{
                    return ({label:"REPORT_USAGE", width:186, x:10, y:365});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button8_click"}, propertiesFactory:function ():Object{
                    return ({label:"SET_PLAN", width:186, x:10, y:395});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button9_click"}, propertiesFactory:function ():Object{
                    return ({label:"SET_SCREENING_GENDER", width:186, x:10, y:425});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button10_click"}, propertiesFactory:function ():Object{
                    return ({label:"START_DAY", width:186, x:10, y:455});
                }}), new UIComponentDescriptor({type:Button, events:{click:"___Console_Button11_click"}, propertiesFactory:function ():Object{
                    return ({label:"START_SCREENING", width:186, x:10, y:485});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:10, y:211, text:"Age", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:10, y:237, text:"CurrentDay", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:10, y:263, text:"EmailAddress", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:280, y:211, text:"FirstName", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:235, y:84, text:"username", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:517, y:84, text:"password", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:280, y:237, text:"Gender", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:280, y:263, text:"LastName", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:515, y:211, text:"MobilePhoneNumber", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:515, y:237, text:"Password", width:77});
                }}), new UIComponentDescriptor({type:Label, propertiesFactory:function ():Object{
                    return ({x:515, y:263, text:"Username", width:77});
                }}), new UIComponentDescriptor({type:NumericStepper, id:"age", propertiesFactory:function ():Object{
                    return ({x:110, y:211, width:41, height:18, value:1});
                }}), new UIComponentDescriptor({type:NumericStepper, id:"currentDay", propertiesFactory:function ():Object{
                    return ({x:110, y:237, width:41, height:18, value:2});
                }}), new UIComponentDescriptor({type:TextInput, id:"emailAddress", propertiesFactory:function ():Object{
                    return ({x:110, y:261, text:"EmailAddress"});
                }}), new UIComponentDescriptor({type:TextInput, id:"preview", propertiesFactory:function ():Object{
                    return ({x:213, y:305, text:"121"});
                }}), new UIComponentDescriptor({type:TextInput, id:"screenTestGender", propertiesFactory:function ():Object{
                    return ({x:213, y:425, text:"Male"});
                }}), new UIComponentDescriptor({type:TextInput, id:"firstName", propertiesFactory:function ():Object{
                    return ({x:347, y:209, text:"FirstName"});
                }}), new UIComponentDescriptor({type:TextInput, id:"mobilePhoneNumber", propertiesFactory:function ():Object{
                    return ({x:600, y:209, text:"MobilePhoneNumber"});
                }}), new UIComponentDescriptor({type:TextInput, id:"reg_password", propertiesFactory:function ():Object{
                    return ({x:600, y:235, text:"reg Password"});
                }}), new UIComponentDescriptor({type:TextInput, id:"reg_userName", propertiesFactory:function ():Object{
                    return ({x:600, y:261, text:"Username"});
                }}), new UIComponentDescriptor({type:TextInput, id:"gender", propertiesFactory:function ():Object{
                    return ({x:347, y:235, text:"Male"});
                }}), new UIComponentDescriptor({type:TextInput, id:"lastName", propertiesFactory:function ():Object{
                    return ({x:347, y:261, text:"LastName"});
                }})]});
            }});
            super();
            mx_internal::_document = this;
            if (!this.styleDeclaration){
                this.styleDeclaration = new CSSStyleDeclaration();
            };
            this.styleDeclaration.defaultFactory = function ():void{
                this.backgroundAlpha = 1;
                this.backgroundColor = 0xFFFFFF;
            };
            this.width = 800;
            this.height = 800;
        }
        public function ___Console_Button10_click(_arg1:MouseEvent):void{
            StartDay();
        }
        public function ___Console_Button2_click(_arg1:MouseEvent):void{
            CollectionComplete();
        }
        public function ___Console_Button4_click(_arg1:MouseEvent):void{
            CreateConsumer();
        }
        public function set gender(_arg1:TextInput):void{
            var _local2:Object = this._1249512767gender;
            if (_local2 !== _arg1){
                this._1249512767gender = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "gender", _local2, _arg1));
            };
        }
        public function ___Console_Button6_click(_arg1:MouseEvent):void{
            Initialise();
        }
        public function get screenTestGender():TextInput{
            return (this._1901137025screenTestGender);
        }
        public function ___Console_Button8_click(_arg1:MouseEvent):void{
            SetPlan();
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }
        public function set lastName(_arg1:TextInput):void{
            var _local2:Object = this._1459599807lastName;
            if (_local2 !== _arg1){
                this._1459599807lastName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "lastName", _local2, _arg1));
            };
        }
        private function SetPlan():void{
            doDispatch("SET_PLAN", null);
        }
        public function set reg_userName(_arg1:TextInput):void{
            var _local2:Object = this._1784863809reg_userName;
            if (_local2 !== _arg1){
                this._1784863809reg_userName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reg_userName", _local2, _arg1));
            };
        }
        public function get password():TextInput{
            return (this._1216985755password);
        }
        public function set preview(_arg1:TextInput):void{
            var _local2:Object = this._318184504preview;
            if (_local2 !== _arg1){
                this._318184504preview = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "preview", _local2, _arg1));
            };
        }
        public function set screenTestGender(_arg1:TextInput):void{
            var _local2:Object = this._1901137025screenTestGender;
            if (_local2 !== _arg1){
                this._1901137025screenTestGender = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "screenTestGender", _local2, _arg1));
            };
        }
        private function CompleteScreening():void{
            var _local1:Object = new Object();
            _local1.response = new Response();
            doDispatch("COMPLETE_SCREENING", _local1);
        }
        private function CollectionComplete():void{
            doDispatch("COLLECTION_COMPLETE", new Response());
        }
        public function set mobilePhoneNumber(_arg1:TextInput):void{
            var _local2:Object = this._181110699mobilePhoneNumber;
            if (_local2 !== _arg1){
                this._181110699mobilePhoneNumber = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mobilePhoneNumber", _local2, _arg1));
            };
        }
        private function GetPreview():void{
            doDispatch("GET_PREVIEW", preview.text);
        }
        private function doDispatch(_arg1:String, _arg2:Object):void{
            var _local3:CairngormEvent = new CairngormEvent(BalanceController[_arg1]);
            _local3.data = _arg2;
            _local3.dispatch();
        }
        private function SetScreeningGender():void{
            doDispatch("SET_SCREENING_GENDER", screenTestGender.text);
        }
        public function set firstName(_arg1:TextInput):void{
            var _local2:Object = this._132835675firstName;
            if (_local2 !== _arg1){
                this._132835675firstName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "firstName", _local2, _arg1));
            };
        }
        public function get emailAddress():TextInput{
            return (this._1070931784emailAddress);
        }
        private function CreateConsumer():void{
            data.age = age.value;
            data.firstName = firstName.text;
            data.gender = gender.text;
            data.lastName = lastName.text;
            data.mobilePhoneNumber = mobilePhoneNumber.text;
            data.password = reg_password.text;
            data.username = reg_userName.text;
            data.email = emailAddress.text;
            doDispatch("CREATE_CONSUMER", data);
        }
        public function set username(_arg1:TextInput):void{
            var _local2:Object = this._265713450username;
            if (_local2 !== _arg1){
                this._265713450username = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "username", _local2, _arg1));
            };
        }
        public function set password(_arg1:TextInput):void{
            var _local2:Object = this._1216985755password;
            if (_local2 !== _arg1){
                this._1216985755password = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "password", _local2, _arg1));
            };
        }
        public function ___Console_Button1_click(_arg1:MouseEvent):void{
            login();
        }
        public function ___Console_Button3_click(_arg1:MouseEvent):void{
            CompleteScreening();
        }
        public function ___Console_Button5_click(_arg1:MouseEvent):void{
            GetPreview();
        }
        public function set currentDay(_arg1:NumericStepper):void{
            var _local2:Object = this._1088999581currentDay;
            if (_local2 !== _arg1){
                this._1088999581currentDay = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "currentDay", _local2, _arg1));
            };
        }
        public function ___Console_Button7_click(_arg1:MouseEvent):void{
            Report();
        }
        public function get gender():TextInput{
            return (this._1249512767gender);
        }
        public function set reg_password(_arg1:TextInput):void{
            var _local2:Object = this._1026450970reg_password;
            if (_local2 !== _arg1){
                this._1026450970reg_password = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reg_password", _local2, _arg1));
            };
        }
        public function get preview():TextInput{
            return (this._318184504preview);
        }
        public function ___Console_Button11_click(_arg1:MouseEvent):void{
            StartScreening();
        }
        private function Report():void{
            doDispatch("REPORT_USAGE", null);
        }
        public function get mobilePhoneNumber():TextInput{
            return (this._181110699mobilePhoneNumber);
        }
        public function get username():TextInput{
            return (this._265713450username);
        }
        private function Initialise():void{
            doDispatch("INITIALISE", null);
        }
        public function ___Console_Button9_click(_arg1:MouseEvent):void{
            SetScreeningGender();
        }
        public function get lastName():TextInput{
            return (this._1459599807lastName);
        }
        private function StartScreening():void{
            doDispatch("START_SCREENING", null);
        }
        public function get currentDay():NumericStepper{
            return (this._1088999581currentDay);
        }
        public function get reg_password():TextInput{
            return (this._1026450970reg_password);
        }
        private function login():void{
            var _local1:Object = new Object();
            _local1.username = username.text;
            _local1.password = username.text;
            doDispatch("LOGIN", _local1);
        }
        public function get firstName():TextInput{
            return (this._132835675firstName);
        }
        public function get reg_userName():TextInput{
            return (this._1784863809reg_userName);
        }
        public function set emailAddress(_arg1:TextInput):void{
            var _local2:Object = this._1070931784emailAddress;
            if (_local2 !== _arg1){
                this._1070931784emailAddress = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "emailAddress", _local2, _arg1));
            };
        }
        private function StartDay():void{
            doDispatch("START_DAY", null);
        }
        public function set age(_arg1:NumericStepper):void{
            var _local2:Object = this._96511age;
            if (_local2 !== _arg1){
                this._96511age = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "age", _local2, _arg1));
            };
        }
        public function get age():NumericStepper{
            return (this._96511age);
        }

    }
}//package com.redbox.changetech.view.components 
