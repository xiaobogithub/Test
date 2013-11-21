//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.templates {
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
    import com.redbox.changetech.view.components.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.view.modules.*;
    import flash.filters.*;
    import mx.validators.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class Registration extends ModuleViewTemplate implements IBindingClient {

        private var _811296866contentImage:BalanceImageReflectionCanvas
        private var _582129719firstNameValidator:StringValidator
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var _2041319926confirmEmailValidator:EmailValidator
        private var focussedFormControl:DisplayObject
        private var _357617898emailValidator:EmailValidator
        private var _emailErrorTip:ToolTip
        public var _Registration_FormItem2:FormItem
        public var _Registration_FormItem3:FormItem
        public var _Registration_FormItem1:FormItem
        mx_internal var _bindingsByDestination:Object
        private var _1580505311formIsEmpty:Boolean = true
        private var _1298893550cta_container:HBox
        private var _1595843470formIsValid:Boolean = false
        mx_internal var _watchers:Array
        private var _1298201998emailInput:TextInput
        private var _84037742confirmEmailInput:TextInput
        private var _1549852825transContainer2:Canvas
        private var _1828708561firstNameInput:TextInput
        mx_internal var _bindingsBeginWithWord:Object
        public var _Registration_Spacer1:Spacer
        private var _94397725registrationForm:Form
        private var _239827997formContainer:BalanceCustomContentReflectionCanvas
        private var _1549852824transContainer1:Canvas
        mx_internal var _bindings:Array
        public var _Registration_VBox1:VBox
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function Registration(){
            _documentDescriptor_ = new UIComponentDescriptor({type:ModuleViewTemplate, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"transContainer1", propertiesFactory:function ():Object{
                    return ({width:1000, height:650, horizontalScrollPolicy:"off", verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, id:"_Registration_VBox1", propertiesFactory:function ():Object{
                        return ({width:480, verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:BalanceCustomContentReflectionCanvas, id:"formContainer", propertiesFactory:function ():Object{
                            return ({percentWidth:100, percentHeight:100, content_arr:[_Registration_Form1_i()]});
                        }}), new UIComponentDescriptor({type:HBox, id:"cta_container", stylesFactory:function ():void{
                            this.horizontalAlign = "right";
                        }, propertiesFactory:function ():Object{
                            return ({width:460, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn", events:{click:"__cta_btn_click"}})]});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:Canvas, id:"transContainer2", propertiesFactory:function ():Object{
                    return ({width:1000, height:650, verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                        return ({childDescriptors:[new UIComponentDescriptor({type:Spacer, id:"_Registration_Spacer1"}), new UIComponentDescriptor({type:BalanceImageReflectionCanvas, id:"contentImage"})]});
                    }})]});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            _Registration_EmailValidator2_i();
            _Registration_EmailValidator1_i();
            _Registration_StringValidator1_i();
            this.addEventListener("creationComplete", ___Registration_ModuleViewTemplate1_creationComplete);
        }
        private function _Registration_StringValidator1_i():StringValidator{
            var _local1:StringValidator = new StringValidator();
            firstNameValidator = _local1;
            _local1.property = "text";
            _local1.minLength = 2;
            BindingManager.executeBindings(this, "firstNameValidator", firstNameValidator);
            _local1.initialized(this, "firstNameValidator");
            return (_local1);
        }
        public function __emailInput_focusOut(_arg1:FocusEvent):void{
            validateEmails();
        }
        private function validateEmails():void{
            if (emailInput.text != confirmEmailInput.text){
                confirmEmailInput.errorString = model.languageVO.getLang("emails_dont_match");
            } else {
                confirmEmailInput.errorString = "";
            };
        }
        private function init(_arg1:FlexEvent):void{
        }
        public function get cta_container():HBox{
            return (this._1298893550cta_container);
        }
        private function validate(_arg1:Validator):Boolean{
            var _local2:DisplayObject = (_arg1.source as DisplayObject);
            var _local3 = !((_local2 == focussedFormControl));
            var _local4:ValidationResultEvent = _arg1.validate(null, _local3);
            var _local5 = (_local4.type == ValidationResultEvent.VALID);
            formIsValid = ((formIsValid) && (_local5));
            return (_local5);
        }
        public function get emailValidator():EmailValidator{
            return (this._357617898emailValidator);
        }
        private function _Registration_EmailValidator1_i():EmailValidator{
            var _local1:EmailValidator = new EmailValidator();
            emailValidator = _local1;
            _local1.property = "text";
            BindingManager.executeBindings(this, "emailValidator", emailValidator);
            _local1.initialized(this, "emailValidator");
            return (_local1);
        }
        public function set emailInput(_arg1:TextInput):void{
            var _local2:Object = this._1298201998emailInput;
            if (_local2 !== _arg1){
                this._1298201998emailInput = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "emailInput", _local2, _arg1));
            };
        }
        public function set cta_container(_arg1:HBox):void{
            var _local2:Object = this._1298893550cta_container;
            if (_local2 !== _arg1){
                this._1298893550cta_container = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_container", _local2, _arg1));
            };
        }
        private function _Registration_FormItem2_i():FormItem{
            var _local1:FormItem = new FormItem();
            _Registration_FormItem2 = _local1;
            _local1.styleName = "registrationLabel";
            _local1.id = "_Registration_FormItem2";
            BindingManager.executeBindings(this, "_Registration_FormItem2", _Registration_FormItem2);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Registration_TextInput2_i());
            return (_local1);
        }
        private function _Registration_TextInput2_i():TextInput{
            var _local1:TextInput = new TextInput();
            emailInput = _local1;
            _local1.styleName = "registrationInputLabel";
            _local1.addEventListener("change", __emailInput_change);
            _local1.addEventListener("focusOut", __emailInput_focusOut);
            _local1.id = "emailInput";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function set formIsValid(_arg1:Boolean):void{
            var _local2:Object = this._1595843470formIsValid;
            if (_local2 !== _arg1){
                this._1595843470formIsValid = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "formIsValid", _local2, _arg1));
            };
        }
        public function set emailValidator(_arg1:EmailValidator):void{
            var _local2:Object = this._357617898emailValidator;
            if (_local2 !== _arg1){
                this._357617898emailValidator = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "emailValidator", _local2, _arg1));
            };
        }
        public function get firstNameInput():TextInput{
            return (this._1828708561firstNameInput);
        }
        public function get firstNameValidator():StringValidator{
            return (this._582129719firstNameValidator);
        }
        public function set transContainer2(_arg1:Canvas):void{
            var _local2:Object = this._1549852825transContainer2;
            if (_local2 !== _arg1){
                this._1549852825transContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer2", _local2, _arg1));
            };
        }
        public function get formContainer():BalanceCustomContentReflectionCanvas{
            return (this._239827997formContainer);
        }
        public function set formContainer(_arg1:BalanceCustomContentReflectionCanvas):void{
            var _local2:Object = this._239827997formContainer;
            if (_local2 !== _arg1){
                this._239827997formContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "formContainer", _local2, _arg1));
            };
        }
        private function callback(_arg1:Boolean):void{
            if (_arg1){
                BasicModule(module).outro();
                dispatchEvent(new Event(Event.COMPLETE));
            };
        }
        private function _Registration_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (firstNameInput);
            }, function (_arg1:Object):void{
                firstNameValidator.source = _arg1;
            }, "firstNameValidator.source");
            result[0] = binding;
            binding = new Binding(this, function ():Object{
                return (emailInput);
            }, function (_arg1:Object):void{
                emailValidator.source = _arg1;
            }, "emailValidator.source");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (confirmEmailInput);
            }, function (_arg1:Object):void{
                confirmEmailValidator.source = _arg1;
            }, "confirmEmailValidator.source");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return ((((formContainer.height / 2) + 40) * -1));
            }, function (_arg1:Number):void{
                _Registration_VBox1.setStyle("verticalGap", _arg1);
            }, "_Registration_VBox1.verticalGap");
            result[3] = binding;
            binding = new Binding(this, function ():Number{
                return ((contentImage.x + contentImage.width));
            }, function (_arg1:Number):void{
                _Registration_VBox1.x = _arg1;
            }, "_Registration_VBox1.x");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.Title;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                formContainer.title_str = _arg1;
            }, "formContainer.title_str");
            result[5] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = SerializeContentXML.convertToHTMLText(content.TextLayout);
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                formContainer.copy_str = _arg1;
            }, "formContainer.copy_str");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("first_name");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _Registration_FormItem1.label = _arg1;
            }, "_Registration_FormItem1.label");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("email");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _Registration_FormItem2.label = _arg1;
            }, "_Registration_FormItem2.label");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("confirm_email");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _Registration_FormItem3.label = _arg1;
            }, "_Registration_FormItem3.label");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return (formIsValid);
            }, function (_arg1:Boolean):void{
                cta_btn.buttonEnabled = _arg1;
            }, "cta_btn.buttonEnabled");
            result[10] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.getCTAButton().Label;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[11] = binding;
            binding = new Binding(this, function ():Number{
                return (Math.max(((1000 - (contentImage.width + formContainer.width)) / 3), 0));
            }, function (_arg1:Number):void{
                _Registration_Spacer1.width = _arg1;
            }, "_Registration_Spacer1.width");
            result[12] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = content.PresenterImageUrl;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                contentImage.source = _arg1;
            }, "contentImage.source");
            result[13] = binding;
            return (result);
        }
        public function set registrationForm(_arg1:Form):void{
            var _local2:Object = this._94397725registrationForm;
            if (_local2 !== _arg1){
                this._94397725registrationForm = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "registrationForm", _local2, _arg1));
            };
        }
        public function set transContainer1(_arg1:Canvas):void{
            var _local2:Object = this._1549852824transContainer1;
            if (_local2 !== _arg1){
                this._1549852824transContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transContainer1", _local2, _arg1));
            };
        }
        public function get registrationForm():Form{
            return (this._94397725registrationForm);
        }
        private function clearFormHandler():void{
            firstNameInput.text = "";
            emailInput.text = "";
            confirmEmailInput.text = "";
            firstNameInput.errorString = "";
            emailInput.errorString = "";
            confirmEmailInput.errorString = "";
            validateEmails();
            formIsEmpty = true;
            resetFocus();
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        private function _Registration_FormItem1_i():FormItem{
            var _local1:FormItem = new FormItem();
            _Registration_FormItem1 = _local1;
            _local1.styleName = "registrationLabel";
            _local1.id = "_Registration_FormItem1";
            BindingManager.executeBindings(this, "_Registration_FormItem1", _Registration_FormItem1);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Registration_TextInput1_i());
            return (_local1);
        }
        public function set firstNameInput(_arg1:TextInput):void{
            var _local2:Object = this._1828708561firstNameInput;
            if (_local2 !== _arg1){
                this._1828708561firstNameInput = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "firstNameInput", _local2, _arg1));
            };
        }
        public function get contentImage():BalanceImageReflectionCanvas{
            return (this._811296866contentImage);
        }
        private function _Registration_TextInput1_i():TextInput{
            var _local1:TextInput = new TextInput();
            firstNameInput = _local1;
            _local1.styleName = "registrationInputLabel";
            _local1.addEventListener("change", __firstNameInput_change);
            _local1.id = "firstNameInput";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function get confirmEmailInput():TextInput{
            return (this._84037742confirmEmailInput);
        }
        public function set firstNameValidator(_arg1:StringValidator):void{
            var _local2:Object = this._582129719firstNameValidator;
            if (_local2 !== _arg1){
                this._582129719firstNameValidator = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "firstNameValidator", _local2, _arg1));
            };
        }
        public function __confirmEmailInput_change(_arg1:Event):void{
            validateForm(_arg1);
        }
        public function get emailInput():TextInput{
            return (this._1298201998emailInput);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _Registration_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_templates_RegistrationWatcherSetupUtil");
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
        public function get transContainer2():Canvas{
            return (this._1549852825transContainer2);
        }
        public function __emailInput_change(_arg1:Event):void{
            validateForm(_arg1);
        }
        public function __cta_btn_click(_arg1:MouseEvent):void{
            submitForm();
        }
        public function get formIsValid():Boolean{
            return (this._1595843470formIsValid);
        }
        public function get transContainer1():Canvas{
            return (this._1549852824transContainer1);
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function ___Registration_ModuleViewTemplate1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        public function set formIsEmpty(_arg1:Boolean):void{
            var _local2:Object = this._1580505311formIsEmpty;
            if (_local2 !== _arg1){
                this._1580505311formIsEmpty = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "formIsEmpty", _local2, _arg1));
            };
        }
        public function set contentImage(_arg1:BalanceImageReflectionCanvas):void{
            var _local2:Object = this._811296866contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        private function _Registration_bindingExprs():void{
            var _local1:*;
            _local1 = firstNameInput;
            _local1 = emailInput;
            _local1 = confirmEmailInput;
            _local1 = (((formContainer.height / 2) + 40) * -1);
            _local1 = (contentImage.x + contentImage.width);
            _local1 = content.Title;
            _local1 = SerializeContentXML.convertToHTMLText(content.TextLayout);
            _local1 = model.languageVO.getLang("first_name");
            _local1 = model.languageVO.getLang("email");
            _local1 = model.languageVO.getLang("confirm_email");
            _local1 = formIsValid;
            _local1 = content.getCTAButton().Label;
            _local1 = Math.max(((1000 - (contentImage.width + formContainer.width)) / 3), 0);
            _local1 = content.PresenterImageUrl;
        }
        private function resetFocus():void{
            focusManager.setFocus(firstNameInput);
        }
        public function set confirmEmailInput(_arg1:TextInput):void{
            var _local2:Object = this._84037742confirmEmailInput;
            if (_local2 !== _arg1){
                this._84037742confirmEmailInput = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "confirmEmailInput", _local2, _arg1));
            };
        }
        private function _Registration_EmailValidator2_i():EmailValidator{
            var _local1:EmailValidator = new EmailValidator();
            confirmEmailValidator = _local1;
            _local1.property = "text";
            BindingManager.executeBindings(this, "confirmEmailValidator", confirmEmailValidator);
            _local1.initialized(this, "confirmEmailValidator");
            return (_local1);
        }
        private function _Registration_FormItem3_i():FormItem{
            var _local1:FormItem = new FormItem();
            _Registration_FormItem3 = _local1;
            _local1.styleName = "registrationLabel";
            _local1.id = "_Registration_FormItem3";
            BindingManager.executeBindings(this, "_Registration_FormItem3", _Registration_FormItem3);
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Registration_TextInput3_i());
            return (_local1);
        }
        public function __confirmEmailInput_focusOut(_arg1:FocusEvent):void{
            validateEmails();
        }
        private function _Registration_TextInput3_i():TextInput{
            var _local1:TextInput = new TextInput();
            confirmEmailInput = _local1;
            _local1.styleName = "registrationInputLabel";
            _local1.addEventListener("change", __confirmEmailInput_change);
            _local1.addEventListener("focusOut", __confirmEmailInput_focusOut);
            _local1.id = "confirmEmailInput";
            if (!_local1.document){
                _local1.document = this;
            };
            return (_local1);
        }
        public function __firstNameInput_change(_arg1:Event):void{
            validateForm(_arg1);
        }
        private function _Registration_Form1_i():Form{
            var _local1:Form = new Form();
            registrationForm = _local1;
            _local1.percentHeight = 100;
            _local1.id = "registrationForm";
            if (!_local1.document){
                _local1.document = this;
            };
            _local1.addChild(_Registration_FormItem1_i());
            _local1.addChild(_Registration_FormItem2_i());
            _local1.addChild(_Registration_FormItem3_i());
            return (_local1);
        }
        private function creationCompleteHandler():void{
            resetFocus();
        }
        public function set confirmEmailValidator(_arg1:EmailValidator):void{
            var _local2:Object = this._2041319926confirmEmailValidator;
            if (_local2 !== _arg1){
                this._2041319926confirmEmailValidator = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "confirmEmailValidator", _local2, _arg1));
            };
        }
        public function get confirmEmailValidator():EmailValidator{
            return (this._2041319926confirmEmailValidator);
        }
        public function get formIsEmpty():Boolean{
            return (this._1580505311formIsEmpty);
        }
        private function validateForm(_arg1:Event):void{
            focussedFormControl = (_arg1.target as DisplayObject);
            validateEmails();
            formIsValid = (emailInput.text == confirmEmailInput.text);
            formIsEmpty = (((((firstNameInput.text == "")) && ((emailInput.text == "")))) && ((confirmEmailInput.text == "")));
            validate(firstNameValidator);
            validate(emailValidator);
            validate(confirmEmailValidator);
            trace(("formIsValid=" + formIsValid));
        }
        private function submitForm():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.CREATE_CONSUMER);
            _local1.data = new Object();
            _local1.data.callback = callback;
            _local1.data.FirstName = firstNameInput.text;
            _local1.data.EmailAddress = emailInput.text;
            _local1.dispatch();
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            Registration._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.templates 
