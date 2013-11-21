//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.utils.virtualmouse {
    import flash.display.*;
    import flash.geom.*;
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.core.log.*;

    public class VirtualMouse extends EventDispatcher {

        private var _container:Sprite
        private var _stage:Stage
        private var lastDownTarget:DisplayObject
        private var target:InteractiveObject
        private var updateMouseDown:Boolean = false
        private var eventEvent:Class
        private var _lastEvent:Event
        private var mouseEventEvent:Class
        private var location:Point
        private var delta:int = 0
        private var disabledEvents:Object
        private var ignoredInstances:Dictionary
        private var isLocked:Boolean = false
        private var lastWithinStage:Boolean = true
        private var lastLocation:Point
        private var isDoubleClickEvent:Boolean = false
        private var lastMouseDown:Boolean = false
        private var ctrlKey:Boolean = false
        private var altKey:Boolean = false
        private var _useNativeEvents:Boolean = false
        private var shiftKey:Boolean = false

        public static const UPDATE:String = "update";

        private static var _mouseIsDown:Boolean = false;

        public function VirtualMouse(_arg1:Stage=null, _arg2:Sprite=null, _arg3:Number=0, _arg4:Number=0){
            disabledEvents = new Object();
            ignoredInstances = new Dictionary(true);
            eventEvent = VirtualMouseEvent;
            mouseEventEvent = VirtualMouseMouseEvent;
            super();
            this.stage = _arg1;
            this.container = _arg2;
            location = new Point(_arg3, _arg4);
            lastLocation = location.clone();
            addEventListener(UPDATE, handleUpdate);
            update();
        }
        public function get mouseIsDown():Boolean{
            return (_mouseIsDown);
        }
        public function get container():Sprite{
            return (_container);
        }
        public function exitContainer():void{
            if (!container){
                return;
            };
            var _local1:Point = target.globalToLocal(location);
            if (!disabledEvents[MouseEvent.MOUSE_OUT]){
                _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                container.dispatchEvent(new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                dispatchEvent(new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
            };
            if (!disabledEvents[MouseEvent.ROLL_OUT]){
                _lastEvent = new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                container.dispatchEvent(new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                dispatchEvent(new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
            };
            if (target != container){
                if (!disabledEvents[MouseEvent.MOUSE_OUT]){
                    _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    target.dispatchEvent(new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                    dispatchEvent(new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                };
                if (!disabledEvents[MouseEvent.ROLL_OUT]){
                    _lastEvent = new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    target.dispatchEvent(new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                    dispatchEvent(new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local1.x, _local1.y, container, ctrlKey, altKey, shiftKey, _mouseIsDown, delta));
                };
            };
            target = _stage;
        }
        public function release():void{
            updateMouseDown = true;
            _mouseIsDown = false;
            if (!isLocked){
                update();
            };
        }
        private function keyHandler(_arg1:KeyboardEvent):void{
            altKey = _arg1.altKey;
            ctrlKey = _arg1.ctrlKey;
            shiftKey = _arg1.shiftKey;
        }
        public function click():void{
            press();
            release();
        }
        public function disableEvent(_arg1:String):void{
            disabledEvents[_arg1] = true;
        }
        public function set container(_arg1:Sprite):void{
            _container = _arg1;
        }
        public function get lastEvent():Event{
            return (_lastEvent);
        }
        private function handleUpdate(_arg1:Event):void{
            var _local4:InteractiveObject;
            var _local5:DisplayObject;
            var _local9:Boolean;
            if (!container){
                return;
            };
            if (container.scrollRect){
                PaperLogger.warning("The container that virtualMouse is trying to test against has a scrollRect defined, and may cause an issue with finding objects under a defined point.  Use MovieMaterial.rect to set a rectangle area instead");
            };
            var _local2:Point = new Point();
            _local2.x = container.x;
            _local2.y = container.y;
            container.x = (container.y = 0);
            var _local3:Array = container.getObjectsUnderPoint(location);
            container.x = _local2.x;
            container.y = _local2.y;
            var _local6:int = _local3.length;
            while (_local6--) {
                _local5 = _local3[_local6];
                while (_local5) {
                    if (ignoredInstances[_local5]){
                        _local4 = null;
                        break;
                    };
                    if (((_local4) && ((_local5 is SimpleButton)))){
                        _local4 = null;
                    } else {
                        if (((_local4) && (!(DisplayObjectContainer(_local5).mouseChildren)))){
                            _local4 = null;
                        };
                    };
                    if (((((!(_local4)) && ((_local5 is InteractiveObject)))) && (InteractiveObject(_local5).mouseEnabled))){
                        _local4 = InteractiveObject(_local5);
                    };
                    _local5 = _local5.parent;
                };
                if (_local4){
                    break;
                };
            };
            if (!_local4){
                _local4 = _stage;
            };
            var _local7:Point = target.globalToLocal(location);
            var _local8:Point = _local4.globalToLocal(location);
            if (((!((lastLocation.x == location.x))) || (!((lastLocation.y == location.y))))){
                _local9 = false;
                if (stage){
                    _local9 = (((((((location.x >= 0)) && ((location.y >= 0)))) && ((location.x <= stage.stageWidth)))) && ((location.y <= stage.stageHeight)));
                };
                if (((((!(_local9)) && (lastWithinStage))) && (!(disabledEvents[Event.MOUSE_LEAVE])))){
                    _lastEvent = new eventEvent(Event.MOUSE_LEAVE, false, false);
                    stage.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
                if (((_local9) && (!(disabledEvents[MouseEvent.MOUSE_MOVE])))){
                    _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_MOVE, true, false, _local8.x, _local8.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    _local4.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
                lastWithinStage = _local9;
            };
            if (_local4 != target){
                if (!disabledEvents[MouseEvent.MOUSE_OUT]){
                    _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_OUT, true, false, _local7.x, _local7.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    target.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
                if (!disabledEvents[MouseEvent.ROLL_OUT]){
                    _lastEvent = new mouseEventEvent(MouseEvent.ROLL_OUT, false, false, _local7.x, _local7.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    target.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
                if (!disabledEvents[MouseEvent.MOUSE_OVER]){
                    _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_OVER, true, false, _local8.x, _local8.y, target, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    _local4.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
                if (!disabledEvents[MouseEvent.ROLL_OVER]){
                    _lastEvent = new mouseEventEvent(MouseEvent.ROLL_OVER, false, false, _local8.x, _local8.y, target, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                    _local4.dispatchEvent(_lastEvent);
                    dispatchEvent(_lastEvent);
                };
            };
            if (updateMouseDown){
                if (_mouseIsDown){
                    if (!disabledEvents[MouseEvent.MOUSE_DOWN]){
                        _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_DOWN, true, false, _local8.x, _local8.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                        _local4.dispatchEvent(_lastEvent);
                        dispatchEvent(_lastEvent);
                    };
                    lastDownTarget = _local4;
                    updateMouseDown = false;
                } else {
                    if (!disabledEvents[MouseEvent.MOUSE_UP]){
                        _lastEvent = new mouseEventEvent(MouseEvent.MOUSE_UP, true, false, _local8.x, _local8.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                        _local4.dispatchEvent(_lastEvent);
                        dispatchEvent(_lastEvent);
                    };
                    if (((!(disabledEvents[MouseEvent.CLICK])) && ((_local4 == lastDownTarget)))){
                        _lastEvent = new mouseEventEvent(MouseEvent.CLICK, true, false, _local8.x, _local8.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                        _local4.dispatchEvent(_lastEvent);
                        dispatchEvent(_lastEvent);
                    };
                    lastDownTarget = null;
                    updateMouseDown = false;
                };
            };
            if (((((isDoubleClickEvent) && (!(disabledEvents[MouseEvent.DOUBLE_CLICK])))) && (_local4.doubleClickEnabled))){
                _lastEvent = new mouseEventEvent(MouseEvent.DOUBLE_CLICK, true, false, _local8.x, _local8.y, _local4, ctrlKey, altKey, shiftKey, _mouseIsDown, delta);
                _local4.dispatchEvent(_lastEvent);
                dispatchEvent(_lastEvent);
            };
            lastLocation = location.clone();
            lastMouseDown = _mouseIsDown;
            target = _local4;
        }
        public function getLocation():Point{
            return (location.clone());
        }
        public function lock():void{
            isLocked = true;
        }
        public function get useNativeEvents():Boolean{
            return (_useNativeEvents);
        }
        public function setLocation(_arg1, _arg2=null):void{
            var _local3:Point;
            if ((_arg1 is Point)){
                _local3 = (_arg1 as Point);
                location.x = _local3.x;
                location.y = _local3.y;
            } else {
                location.x = Number(_arg1);
                location.y = Number(_arg2);
            };
            if (!isLocked){
                update();
            };
        }
        public function unignore(_arg1:DisplayObject):void{
            if ((_arg1 in ignoredInstances)){
                delete ignoredInstances[_arg1];
            };
        }
        public function doubleClick():void{
            if (isLocked){
                release();
            } else {
                click();
                press();
                isDoubleClickEvent = true;
                release();
                isDoubleClickEvent = false;
            };
        }
        public function update():void{
            dispatchEvent(new Event(UPDATE, false, false));
        }
        public function unlock():void{
            isLocked = false;
            update();
        }
        public function ignore(_arg1:DisplayObject):void{
            ignoredInstances[_arg1] = true;
        }
        public function enableEvent(_arg1:String):void{
            if ((_arg1 in disabledEvents)){
                delete disabledEvents[_arg1];
            };
        }
        public function press():void{
            updateMouseDown = true;
            _mouseIsDown = true;
            if (!isLocked){
                update();
            };
        }
        public function set useNativeEvents(_arg1:Boolean):void{
            if (_arg1 == _useNativeEvents){
                return;
            };
            _useNativeEvents = _arg1;
            if (_useNativeEvents){
                eventEvent = VirtualMouseEvent;
                mouseEventEvent = VirtualMouseMouseEvent;
            } else {
                eventEvent = Event;
                mouseEventEvent = MouseEvent;
            };
        }
        public function set x(_arg1:Number):void{
            location.x = _arg1;
            if (!isLocked){
                update();
            };
        }
        public function set y(_arg1:Number):void{
            location.y = _arg1;
            if (!isLocked){
                update();
            };
        }
        public function get y():Number{
            return (location.y);
        }
        public function set stage(_arg1:Stage):void{
            var _local2:Boolean;
            if (_stage){
                _local2 = true;
                _stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyHandler);
                _stage.removeEventListener(KeyboardEvent.KEY_UP, keyHandler);
            } else {
                _local2 = false;
            };
            _stage = _arg1;
            if (_stage){
                _stage.addEventListener(KeyboardEvent.KEY_DOWN, keyHandler);
                _stage.addEventListener(KeyboardEvent.KEY_UP, keyHandler);
                target = _stage;
                if (!_local2){
                    update();
                };
            };
        }
        public function get stage():Stage{
            return (_stage);
        }
        public function get x():Number{
            return (location.x);
        }

    }
}//package org.papervision3d.core.utils.virtualmouse 
