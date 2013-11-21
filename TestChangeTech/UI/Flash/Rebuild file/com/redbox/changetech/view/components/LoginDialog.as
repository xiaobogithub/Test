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
    import com.redbox.changetech.model.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.adobe.crypto.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class LoginDialog extends Canvas implements IBindingClient {

        private var _96619420email:TextInput
        private var _1082042285cta_btn:BalanceButtonReflectionCanvas
        private var model:BalanceModelLocator
        public var _LoginDialog_TextArea1:TextArea
        public var _LoginDialog_TextArea2:TextArea
        public var _LoginDialog_TextArea4:TextArea
        public var _LoginDialog_FormItem2:FormItem
        public var _LoginDialog_TextArea6:TextArea
        public var _LoginDialog_FormItem4:FormItem
        public var _LoginDialog_FormItem1:FormItem
        public var _LoginDialog_TextArea5:TextArea
        public var _LoginDialog_FormItem3:FormItem
        public var _LoginDialog_TextArea7:TextArea
        private var _1926385031screens:ViewStack
        private var _2000301742resetLoginCanvas:Canvas
        private var _475315224_defaultUserName:String = ""
        mx_internal var _bindingsByDestination:Object
        private var _816427483cta_btn2:BalanceButtonReflectionCanvas
        private var _3440957pin2:TextInput
        private var _436537223reset_login:BalanceButtonReflectionCanvas
        private var _1022529523_login_error:String = ""
        private var _1497950141_reset_login_error
        public var _LoginDialog_Label1:Label
        public var _LoginDialog_Label2:Label
        mx_internal var _watchers:Array
        private var _1644573106login_error:TextArea
        private var _243754545loginCanvas2:Canvas
        private var _839147041loginCanvas:Canvas
        mx_internal var _bindingsBeginWithWord:Object
        private var _1265357118reset_login_error:TextArea
        private var _3440956pin1:TextInput
        mx_internal var _bindings:Array
        private var _1958967741_defaultPassword:String = ""
        private var _documentDescriptor_:UIComponentDescriptor
        private var _1265522602reset_login_email:TextInput

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function LoginDialog(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:ViewStack, id:"screens", propertiesFactory:function ():Object{
                    return ({resizeToContent:true, childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                        this.verticalGap = -30;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"loginCanvas", propertiesFactory:function ():Object{
                            return ({width:330, styleName:"roundedGradCanvas", verticalScrollPolicy:"off", horizontalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalGap = 3;
                                this.paddingLeft = 20;
                                this.paddingRight = 20;
                                this.paddingTop = 20;
                                this.paddingBottom = 50;
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea1", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 18;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea2", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 14;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:TextArea, id:"login_error", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 10;
                                    this.color = 0x9900;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100, height:0});
                                }}), new UIComponentDescriptor({type:Form, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:FormItem, id:"_LoginDialog_FormItem1", stylesFactory:function ():void{
                                        this.labelStyleName = "registrationInputLabel";
                                    }, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:TextInput, id:"email"})]});
                                    }}), new UIComponentDescriptor({type:FormItem, id:"_LoginDialog_FormItem2", stylesFactory:function ():void{
                                        this.labelStyleName = "registrationInputLabel";
                                    }, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:TextInput, id:"pin1", propertiesFactory:function ():Object{
                                            return ({displayAsPassword:true});
                                        }})]});
                                    }})]});
                                }}), new UIComponentDescriptor({type:Label, id:"_LoginDialog_Label1", events:{click:"___LoginDialog_Label1_click"}, stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 10;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100, buttonMode:true, useHandCursor:true, mouseChildren:false});
                                }})]});
                            }})]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.horizontalAlign = "center";
                        }, propertiesFactory:function ():Object{
                            return ({width:350, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn", events:{click:"__cta_btn_click"}, propertiesFactory:function ():Object{
                                return ({reflectionIsOn:true});
                            }})]});
                        }})]});
                    }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                        this.verticalGap = -30;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"loginCanvas2", propertiesFactory:function ():Object{
                            return ({width:330, styleName:"roundedGradCanvas", verticalScrollPolicy:"off", horizontalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalGap = 3;
                                this.paddingLeft = 20;
                                this.paddingRight = 20;
                                this.paddingTop = 20;
                                this.paddingBottom = 50;
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea4", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 18;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea5", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 14;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:FormItem, id:"_LoginDialog_FormItem3", stylesFactory:function ():void{
                                    this.labelStyleName = "registrationInputLabel";
                                }, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:TextInput, id:"pin2", propertiesFactory:function ():Object{
                                        return ({displayAsPassword:true});
                                    }})]});
                                }})]});
                            }})]});
                        }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                            this.horizontalAlign = "center";
                        }, propertiesFactory:function ():Object{
                            return ({width:350, childDescriptors:[new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"cta_btn2", events:{click:"__cta_btn2_click"}, propertiesFactory:function ():Object{
                                return ({reflectionIsOn:true});
                            }})]});
                        }})]});
                    }}), new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                        this.verticalGap = -30;
                        this.horizontalAlign = "center";
                    }, propertiesFactory:function ():Object{
                        return ({verticalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:Canvas, id:"resetLoginCanvas", propertiesFactory:function ():Object{
                            return ({width:330, styleName:"roundedGradCanvas", verticalScrollPolicy:"off", horizontalScrollPolicy:"off", childDescriptors:[new UIComponentDescriptor({type:VBox, stylesFactory:function ():void{
                                this.verticalGap = 3;
                                this.paddingLeft = 20;
                                this.paddingRight = 20;
                                this.paddingTop = 20;
                                this.paddingBottom = 50;
                            }, propertiesFactory:function ():Object{
                                return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                                    this.horizontalAlign = "center";
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100, childDescriptors:[new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea6", stylesFactory:function ():void{
                                        this.fontFamily = "Helvetica Neue";
                                        this.fontSize = 18;
                                    }, propertiesFactory:function ():Object{
                                        return ({percentWidth:100});
                                    }})]});
                                }}), new UIComponentDescriptor({type:TextArea, id:"_LoginDialog_TextArea7", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 14;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100});
                                }}), new UIComponentDescriptor({type:Form, propertiesFactory:function ():Object{
                                    return ({childDescriptors:[new UIComponentDescriptor({type:FormItem, id:"_LoginDialog_FormItem4", stylesFactory:function ():void{
                                        this.labelStyleName = "registrationInputLabel";
                                    }, propertiesFactory:function ():Object{
                                        return ({childDescriptors:[new UIComponentDescriptor({type:TextInput, id:"reset_login_email", propertiesFactory:function ():Object{
                                            return ({text:""});
                                        }})]});
                                    }})]});
                                }}), new UIComponentDescriptor({type:TextArea, id:"reset_login_error", stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 10;
                                    this.color = 0x990000;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100, height:0, visible:true});
                                }}), new UIComponentDescriptor({type:Label, id:"_LoginDialog_Label2", events:{click:"___LoginDialog_Label2_click"}, stylesFactory:function ():void{
                                    this.fontFamily = "Helvetica Neue";
                                    this.fontSize = 10;
                                }, propertiesFactory:function ():Object{
                                    return ({percentWidth:100, buttonMode:true, useHandCursor:true, mouseChildren:false});
                                }})]});
                            }})]});
                        }}), new UIComponentDescriptor({type:BalanceButtonReflectionCanvas, id:"reset_login", events:{click:"__reset_login_click"}, propertiesFactory:function ():Object{
                            return ({reflectionIsOn:true});
                        }})]});
                    }})]});
                }})]});
            }});
            model = BalanceModelLocator.getInstance();
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.addEventListener("creationComplete", ___LoginDialog_Canvas1_creationComplete);
        }
        private function loginOK(_arg1:Boolean):void{
            var _local2:SharedObject = SharedObject.getLocal("Balance");
            if (_local2 != null){
                if (_local2.data != null){
                    if (email != null){
                        _local2.data.username = email.text;
                    };
                    if (pin1 != null){
                        _local2.data.password = pin1.text;
                    } else {
                        _local2.data.password = pin2.text;
                    };
                };
            };
        }
        public function set screens(_arg1:ViewStack):void{
            var _local2:Object = this._1926385031screens;
            if (_local2 !== _arg1){
                this._1926385031screens = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "screens", _local2, _arg1));
            };
        }
        private function init():void{
        }
        private function resetLoginResult(_arg1:Object):void{
            if (_arg1.result){
                _login_error = model.languageVO.getLang("login_reset_sent");
                showLogin();
                if (login_error){
                    login_error.height = 14;
                };
            } else {
                _reset_login_error = model.languageVO.getLang("login_reset_notfound");
                if (reset_login_error){
                    reset_login_error.height = 14;
                };
            };
        }
        private function get _defaultUserName():String{
            return (this._475315224_defaultUserName);
        }
        public function get loginCanvas():Canvas{
            return (this._839147041loginCanvas);
        }
        public function get cta_btn2():BalanceButtonReflectionCanvas{
            return (this._816427483cta_btn2);
        }
        public function get loginCanvas2():Canvas{
            return (this._243754545loginCanvas2);
        }
        public function set email(_arg1:TextInput):void{
            var _local2:Object = this._96619420email;
            if (_local2 !== _arg1){
                this._96619420email = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "email", _local2, _arg1));
            };
        }
        public function get pin1():TextInput{
            return (this._3440956pin1);
        }
        public function get pin2():TextInput{
            return (this._3440957pin2);
        }
        public function set loginCanvas(_arg1:Canvas):void{
            var _local2:Object = this._839147041loginCanvas;
            if (_local2 !== _arg1){
                this._839147041loginCanvas = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "loginCanvas", _local2, _arg1));
            };
        }
        private function get _reset_login_error(){
            return (this._1497950141_reset_login_error);
        }
        private function showResetLogin():void{
            screens.selectedIndex = 2;
            _reset_login_error = "";
            if (reset_login_error){
                reset_login_error.height = 0;
            };
            _login_error = "";
            if (login_error){
                login_error.height = 0;
            };
        }
        private function set _defaultUserName(_arg1:String):void{
            var _local2:Object = this._475315224_defaultUserName;
            if (_local2 !== _arg1){
                this._475315224_defaultUserName = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_defaultUserName", _local2, _arg1));
            };
        }
        private function _LoginDialog_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_welcome");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea1.text = _arg1;
            }, "_LoginDialog_TextArea1.text");
            result[0] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_instructions");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea2.text = _arg1;
            }, "_LoginDialog_TextArea2.text");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _login_error;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                login_error.text = _arg1;
            }, "login_error.text");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("enter_email");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_FormItem1.label = _arg1;
            }, "_LoginDialog_FormItem1.label");
            result[3] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _defaultUserName;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                email.text = _arg1;
            }, "email.text");
            result[4] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("pincode");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_FormItem2.label = _arg1;
            }, "_LoginDialog_FormItem2.label");
            result[5] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _defaultPassword;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                pin1.text = _arg1;
            }, "pin1.text");
            result[6] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_reset_click_to");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_Label1.text = _arg1;
            }, "_LoginDialog_Label1.text");
            result[7] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn.buttonLabel = _arg1;
            }, "cta_btn.buttonLabel");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_welcome");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea4.text = _arg1;
            }, "_LoginDialog_TextArea4.text");
            result[9] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_instructions_pin_only");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea5.text = _arg1;
            }, "_LoginDialog_TextArea5.text");
            result[10] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("pincode");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_FormItem3.label = _arg1;
            }, "_LoginDialog_FormItem3.label");
            result[11] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _defaultPassword;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                pin2.text = _arg1;
            }, "pin2.text");
            result[12] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                cta_btn2.buttonLabel = _arg1;
            }, "cta_btn2.buttonLabel");
            result[13] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_reset_title");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea6.text = _arg1;
            }, "_LoginDialog_TextArea6.text");
            result[14] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_reset_intruction");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_TextArea7.text = _arg1;
            }, "_LoginDialog_TextArea7.text");
            result[15] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("enter_email");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_FormItem4.label = _arg1;
            }, "_LoginDialog_FormItem4.label");
            result[16] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = _reset_login_error;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                reset_login_error.text = _arg1;
            }, "reset_login_error.text");
            result[17] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_click_to");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _LoginDialog_Label2.text = _arg1;
            }, "_LoginDialog_Label2.text");
            result[18] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = model.languageVO.getLang("login_reset_button_label");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                reset_login.buttonLabel = _arg1;
            }, "reset_login.buttonLabel");
            result[19] = binding;
            return (result);
        }
        public function set cta_btn2(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._816427483cta_btn2;
            if (_local2 !== _arg1){
                this._816427483cta_btn2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn2", _local2, _arg1));
            };
        }
        private function resetLogin():void{
            _reset_login_error = "";
            _login_error = "";
            if (((!(reset_login_email.text)) || ((reset_login_email.text.length == 0)))){
                _reset_login_error = model.languageVO.getLang("login_reset_error_blank");
                if (reset_login_error){
                    reset_login_error.height = 14;
                };
                return;
            };
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.RESET_LOGIN);
            _local1.data = new Object();
            _local1.data.username = reset_login_email.text;
            _local1.data.callback = resetLoginResult;
            _local1.dispatch();
        }
        public function get reset_login_email():TextInput{
            return (this._1265522602reset_login_email);
        }
        public function set cta_btn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._1082042285cta_btn;
            if (_local2 !== _arg1){
                this._1082042285cta_btn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "cta_btn", _local2, _arg1));
            };
        }
        public function set loginCanvas2(_arg1:Canvas):void{
            var _local2:Object = this._243754545loginCanvas2;
            if (_local2 !== _arg1){
                this._243754545loginCanvas2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "loginCanvas2", _local2, _arg1));
            };
        }
        public function set login_error(_arg1:TextArea):void{
            var _local2:Object = this._1644573106login_error;
            if (_local2 !== _arg1){
                this._1644573106login_error = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "login_error", _local2, _arg1));
            };
        }
        private function login():void{
            _login_error = "";
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.LOGIN);
            _local1.data = new Object();
            _local1.data.username = email.text;
            _local1.data.password = pin1.text;
            _local1.data.callback = loginOK;
            _local1.dispatch();
        }
        public function get resetLoginCanvas():Canvas{
            return (this._2000301742resetLoginCanvas);
        }
        private function showLogin():void{
            this.visible = true;
        }
        private function comparePIN():void{
            var _local1:CairngormEvent;
            if (MD5.hash(pin2.text) == model.consumer.Password){
                _local1 = new CairngormEvent(BalanceController.LOGIN);
                _local1.data = new Object();
                _local1.data.username = model.consumer.EmailAddress;
                _local1.data.password = pin2.text;
                _local1.data.callback = loginOK;
                _local1.dispatch();
            } else {
                problem();
            };
        }
        public function __reset_login_click(_arg1:MouseEvent):void{
            resetLogin();
        }
        public function get reset_login():BalanceButtonReflectionCanvas{
            return (this._436537223reset_login);
        }
        public function set pin1(_arg1:TextInput):void{
            var _local2:Object = this._3440956pin1;
            if (_local2 !== _arg1){
                this._3440956pin1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "pin1", _local2, _arg1));
            };
        }
        public function set pin2(_arg1:TextInput):void{
            var _local2:Object = this._3440957pin2;
            if (_local2 !== _arg1){
                this._3440957pin2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "pin2", _local2, _arg1));
            };
        }
        public function set reset_login_error(_arg1:TextArea):void{
            var _local2:Object = this._1265357118reset_login_error;
            if (_local2 !== _arg1){
                this._1265357118reset_login_error = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reset_login_error", _local2, _arg1));
            };
        }
        public function get screens():ViewStack{
            return (this._1926385031screens);
        }
        private function set _defaultPassword(_arg1:String):void{
            var _local2:Object = this._1958967741_defaultPassword;
            if (_local2 !== _arg1){
                this._1958967741_defaultPassword = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_defaultPassword", _local2, _arg1));
            };
        }
        private function _LoginDialog_bindingExprs():void{
            var _local1:*;
            _local1 = model.languageVO.getLang("login_welcome");
            _local1 = model.languageVO.getLang("login_instructions");
            _local1 = _login_error;
            _local1 = model.languageVO.getLang("enter_email");
            _local1 = _defaultUserName;
            _local1 = model.languageVO.getLang("pincode");
            _local1 = _defaultPassword;
            _local1 = model.languageVO.getLang("login_reset_click_to");
            _local1 = model.languageVO.getLang("login");
            _local1 = model.languageVO.getLang("login_welcome");
            _local1 = model.languageVO.getLang("login_instructions_pin_only");
            _local1 = model.languageVO.getLang("pincode");
            _local1 = _defaultPassword;
            _local1 = model.languageVO.getLang("login");
            _local1 = model.languageVO.getLang("login_reset_title");
            _local1 = model.languageVO.getLang("login_reset_intruction");
            _local1 = model.languageVO.getLang("enter_email");
            _local1 = _reset_login_error;
            _local1 = model.languageVO.getLang("login_click_to");
            _local1 = model.languageVO.getLang("login_reset_button_label");
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _LoginDialog_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_LoginDialogWatcherSetupUtil");
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
        private function set _reset_login_error(_arg1):void{
            var _local2:Object = this._1497950141_reset_login_error;
            if (_local2 !== _arg1){
                this._1497950141_reset_login_error = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_reset_login_error", _local2, _arg1));
            };
        }
        public function __cta_btn_click(_arg1:MouseEvent):void{
            login();
        }
        public function ___LoginDialog_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        public function get email():TextInput{
            return (this._96619420email);
        }
        private function problem():void{
            Alert.show(model.languageVO.getLang("login_error_problem"));
        }
        public function set resetLoginCanvas(_arg1:Canvas):void{
            var _local2:Object = this._2000301742resetLoginCanvas;
            if (_local2 !== _arg1){
                this._2000301742resetLoginCanvas = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "resetLoginCanvas", _local2, _arg1));
            };
        }
        public function set reset_login_email(_arg1:TextInput):void{
            var _local2:Object = this._1265522602reset_login_email;
            if (_local2 !== _arg1){
                this._1265522602reset_login_email = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reset_login_email", _local2, _arg1));
            };
        }
        public function get cta_btn():BalanceButtonReflectionCanvas{
            return (this._1082042285cta_btn);
        }
        public function ___LoginDialog_Label1_click(_arg1:MouseEvent):void{
            showResetLogin();
        }
        public function get login_error():TextArea{
            return (this._1644573106login_error);
        }
        private function get _defaultPassword():String{
            return (this._1958967741_defaultPassword);
        }
        public function get reset_login_error():TextArea{
            return (this._1265357118reset_login_error);
        }
        public function __cta_btn2_click(_arg1:MouseEvent):void{
            comparePIN();
        }
        override public function set visible(_arg1:Boolean):void{
            super.visible = _arg1;
            trace(("BalanceModelLocator.getInstance().isFullLoginRequired=" + BalanceModelLocator.getInstance().isFullLoginRequired));
            if (((BalanceModelLocator.getInstance().isFullLoginRequired) || (model.alwaysShowLogin))){
                screens.selectedIndex = 0;
            } else {
                screens.selectedIndex = 1;
            };
        }
        private function set _login_error(_arg1:String):void{
            var _local2:Object = this._1022529523_login_error;
            if (_local2 !== _arg1){
                this._1022529523_login_error = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "_login_error", _local2, _arg1));
            };
        }
        public function set reset_login(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this._436537223reset_login;
            if (_local2 !== _arg1){
                this._436537223reset_login = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "reset_login", _local2, _arg1));
            };
        }
        private function get _login_error():String{
            return (this._1022529523_login_error);
        }
        public function ___LoginDialog_Label2_click(_arg1:MouseEvent):void{
            showLogin();
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            LoginDialog._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
