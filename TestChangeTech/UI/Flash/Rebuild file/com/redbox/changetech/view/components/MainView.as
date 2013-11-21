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
    import mx.binding.utils.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.transitions.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class MainView extends Canvas implements IBindingClient {

        private var _108698295room2:BalanceRoom
        mx_internal var _watchers:Array
        private var _413522385roomTransitionContainer:UIComponent
        private var _108698296room3:BalanceRoom
        private var _roomTransition:RoomTransition
        private var _108698360rooms:ViewStack
        mx_internal var _bindingsByDestination:Object
        private var _108698297room4:BalanceRoom
        mx_internal var _bindingsBeginWithWord:Object
        public var _MainView_Console1:Console
        public var _MainView_LoginDialog1:LoginDialog
        private var _108698294room1:BalanceRoom
        private var _104069929model:BalanceModelLocator
        public var _MainView_Box1:Box
        private var _922330056mainViewProgressBar:ProgressBar
        private var _documentDescriptor_:UIComponentDescriptor
        public var yOffset:Number
        mx_internal var _bindings:Array

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function MainView(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:UIComponent, id:"roomTransitionContainer"}), new UIComponentDescriptor({type:ViewStack, id:"rooms", propertiesFactory:function ():Object{
                    return ({creationPolicy:"all", selectedIndex:3, resizeToContent:true, childDescriptors:[new UIComponentDescriptor({type:BalanceRoom, id:"room1", propertiesFactory:function ():Object{
                        return ({roomName:"Emotion"});
                    }}), new UIComponentDescriptor({type:BalanceRoom, id:"room2", propertiesFactory:function ():Object{
                        return ({roomName:"Motivation"});
                    }}), new UIComponentDescriptor({type:BalanceRoom, id:"room3", propertiesFactory:function ():Object{
                        return ({roomName:"Willpower"});
                    }}), new UIComponentDescriptor({type:BalanceRoom, id:"room4", propertiesFactory:function ():Object{
                        return ({roomName:"Blank"});
                    }})]});
                }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "middle";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, percentHeight:100, styleName:"alignMiddleCenterContainer", childDescriptors:[new UIComponentDescriptor({type:Box, id:"_MainView_Box1", stylesFactory:function ():void{
                        this.backgroundAlpha = 0.8;
                        this.backgroundColor = 0xFFFFFF;
                        this.cornerRadius = 10;
                        this.horizontalAlign = "center";
                        this.verticalAlign = "middle";
                    }, propertiesFactory:function ():Object{
                        return ({width:240, height:100, childDescriptors:[new UIComponentDescriptor({type:ProgressBar, id:"mainViewProgressBar", stylesFactory:function ():void{
                            this.barColor = 0x666666;
                            this.borderColor = 0x666666;
                            this.color = 0x666666;
                            this.fontFamily = "Helvetica Neue";
                            this.fontWeight = "normal";
                            this.fontSize = 13;
                        }, propertiesFactory:function ():Object{
                            return ({label:"Loading module...%3%%", labelPlacement:"top", width:200});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "middle";
                }, propertiesFactory:function ():Object{
                    return ({percentWidth:100, percentHeight:100, styleName:"alignMiddleCenterContainer", childDescriptors:[new UIComponentDescriptor({type:LoginDialog, id:"_MainView_LoginDialog1"})]});
                }}), new UIComponentDescriptor({type:Console, id:"_MainView_Console1"})]});
            }});
            _104069929model = BalanceModelLocator.getInstance();
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
                this.paddingTop = 0;
                this.paddingBottom = 0;
                this.paddingLeft = 0;
                this.paddingRight = 0;
            };
            this.percentWidth = 100;
            this.percentHeight = 100;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.addEventListener("creationComplete", ___MainView_Canvas1_creationComplete);
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _MainView_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_MainViewWatcherSetupUtil");
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
        private function _MainView_bindingExprs():void{
            var _local1:*;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = model.currentStageWidth;
            _local1 = model.currentStageHeight;
            _local1 = (model.roomContent.getItemAt(0) as ContentCollection);
            _local1 = (model.roomContent.getItemAt(1) as ContentCollection);
            _local1 = (model.roomContent.getItemAt(2) as ContentCollection);
            _local1 = (model.roomContent.getItemAt(3) as ContentCollection);
            _local1 = !((model.loaderSource == null));
            _local1 = model.loaderSource;
            _local1 = model.showLogin;
            _local1 = model.showConsole;
        }
        public function set roomTransitionContainer(_arg1:UIComponent):void{
            var _local2:Object = this._413522385roomTransitionContainer;
            if (_local2 !== _arg1){
                this._413522385roomTransitionContainer = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomTransitionContainer", _local2, _arg1));
            };
        }
        public function set mainViewProgressBar(_arg1:ProgressBar):void{
            var _local2:Object = this._922330056mainViewProgressBar;
            if (_local2 !== _arg1){
                this._922330056mainViewProgressBar = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mainViewProgressBar", _local2, _arg1));
            };
        }
        private function get model():BalanceModelLocator{
            return (this._104069929model);
        }
        private function transitionComplete(_arg1:Event):void{
            BalanceModelLocator.getInstance().RPC_OperationInProgress = false;
        }
        public function get room1():BalanceRoom{
            return (this._108698294room1);
        }
        public function get room2():BalanceRoom{
            return (this._108698295room2);
        }
        public function get room4():BalanceRoom{
            return (this._108698297room4);
        }
        private function setTransitionIndex(_arg1:Number):void{
            _roomTransition.selectedIndex = _arg1;
        }
        public function get room3():BalanceRoom{
            return (this._108698296room3);
        }
        public function get mainViewProgressBar():ProgressBar{
            return (this._922330056mainViewProgressBar);
        }
        private function reportKeyDown(_arg1:KeyboardEvent):void{
            if (_arg1.charCode == 99){
                BalanceModelLocator.getInstance().showConsole = !(BalanceModelLocator.getInstance().showConsole);
            };
        }
        public function get roomTransitionContainer():UIComponent{
            return (this._413522385roomTransitionContainer);
        }
        private function set model(_arg1:BalanceModelLocator):void{
            var _local2:Object = this._104069929model;
            if (_local2 !== _arg1){
                this._104069929model = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "model", _local2, _arg1));
            };
        }
        private function _MainView_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                roomTransitionContainer.width = _arg1;
            }, "roomTransitionContainer.width");
            result[0] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                roomTransitionContainer.height = _arg1;
            }, "roomTransitionContainer.height");
            result[1] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageWidth);
            }, function (_arg1:Number):void{
                rooms.width = _arg1;
            }, "rooms.width");
            result[2] = binding;
            binding = new Binding(this, function ():Number{
                return (model.currentStageHeight);
            }, function (_arg1:Number):void{
                rooms.height = _arg1;
            }, "rooms.height");
            result[3] = binding;
            binding = new Binding(this, function ():ContentCollection{
                return ((model.roomContent.getItemAt(0) as ContentCollection));
            }, function (_arg1:ContentCollection):void{
                room1.contentCollection = _arg1;
            }, "room1.contentCollection");
            result[4] = binding;
            binding = new Binding(this, function ():ContentCollection{
                return ((model.roomContent.getItemAt(1) as ContentCollection));
            }, function (_arg1:ContentCollection):void{
                room2.contentCollection = _arg1;
            }, "room2.contentCollection");
            result[5] = binding;
            binding = new Binding(this, function ():ContentCollection{
                return ((model.roomContent.getItemAt(2) as ContentCollection));
            }, function (_arg1:ContentCollection):void{
                room3.contentCollection = _arg1;
            }, "room3.contentCollection");
            result[6] = binding;
            binding = new Binding(this, function ():ContentCollection{
                return ((model.roomContent.getItemAt(3) as ContentCollection));
            }, function (_arg1:ContentCollection):void{
                room4.contentCollection = _arg1;
            }, "room4.contentCollection");
            result[7] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((model.loaderSource == null)));
            }, function (_arg1:Boolean):void{
                _MainView_Box1.visible = _arg1;
            }, "_MainView_Box1.visible");
            result[8] = binding;
            binding = new Binding(this, function ():Object{
                return (model.loaderSource);
            }, function (_arg1:Object):void{
                mainViewProgressBar.source = _arg1;
            }, "mainViewProgressBar.source");
            result[9] = binding;
            binding = new Binding(this, function ():Boolean{
                return (model.showLogin);
            }, function (_arg1:Boolean):void{
                _MainView_LoginDialog1.visible = _arg1;
            }, "_MainView_LoginDialog1.visible");
            result[10] = binding;
            binding = new Binding(this, function ():Boolean{
                return (model.showConsole);
            }, function (_arg1:Boolean):void{
                _MainView_Console1.visible = _arg1;
            }, "_MainView_Console1.visible");
            result[11] = binding;
            return (result);
        }
        public function ___MainView_Canvas1_creationComplete(_arg1:FlexEvent):void{
            creationCompleteHandler();
        }
        public function set room1(_arg1:BalanceRoom):void{
            var _local2:Object = this._108698294room1;
            if (_local2 !== _arg1){
                this._108698294room1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "room1", _local2, _arg1));
            };
        }
        public function set room2(_arg1:BalanceRoom):void{
            var _local2:Object = this._108698295room2;
            if (_local2 !== _arg1){
                this._108698295room2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "room2", _local2, _arg1));
            };
        }
        public function set room3(_arg1:BalanceRoom):void{
            var _local2:Object = this._108698296room3;
            if (_local2 !== _arg1){
                this._108698296room3 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "room3", _local2, _arg1));
            };
        }
        private function creationCompleteHandler():void{
            rooms.selectedIndex = model.room;
            setupRoomTransition();
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.STARTUP);
            _local1.dispatch();
        }
        private function setupRoomTransition(_arg1:Event=null):void{
            if (!_roomTransition){
                _roomTransition = new RoomTransition(rooms, model.currentStageWidth, model.currentStageHeight);
                roomTransitionContainer.addChild(_roomTransition);
                _roomTransition.addEventListener(BalanceController.TRANSITION_COMPLETE, transitionComplete);
                BindingUtils.bindSetter(setTransitionIndex, model, "room");
            };
        }
        public function set rooms(_arg1:ViewStack):void{
            var _local2:Object = this._108698360rooms;
            if (_local2 !== _arg1){
                this._108698360rooms = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "rooms", _local2, _arg1));
            };
        }
        public function get rooms():ViewStack{
            return (this._108698360rooms);
        }
        public function set room4(_arg1:BalanceRoom):void{
            var _local2:Object = this._108698297room4;
            if (_local2 !== _arg1){
                this._108698297room4 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "room4", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            MainView._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
