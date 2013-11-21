//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.command {
    import flash.geom.*;
    import flash.net.*;
    import com.degrafa.geometry.utilities.*;

    public class CommandStackItem {

        private var _y:Number
        private var _y1:Number
        private var _cy:Number
        public var id:String
        private var _segmentLength:Number = 0
        var indexInParent:uint
        private var _cx:Number
        var parent:CommandStack
        private var _renderDelegateStart:Array
        private var _renderDelegateEnd:Array
        public var type:int
        private var _lengthInvalidated:Boolean = true
        private var _invalidated:Boolean = true
        public var reference:String
        private var _next:CommandStackItem
        private var _bounds:Rectangle
        private var _previous:CommandStackItem
        private var _x1:Number
        private var _commandStack:CommandStack
        private var _delegate:Function
        private var _x:Number
        public var skip:Boolean

        public static const LINE_TO:int = 1;
        public static const MOVE_TO:int = 0;
        public static const CURVE_TO:int = 2;
        public static const COMMAND_STACK:int = 4;
        public static const DELEGATE_TO:int = 3;
        public static const IS_REGISTERED:Boolean = !(((registerClassAlias("com.degrafa.geometry.command.CommandStackItem", CommandStackItem)) || (registerClassAlias("flash.geom.Point", Point))));

        public function CommandStackItem(_arg1:int=0, _arg2:Number=NaN, _arg3:Number=NaN, _arg4:Number=NaN, _arg5:Number=NaN, _arg6:Number=NaN, _arg7:Number=NaN, _arg8:CommandStack=null){
            _renderDelegateStart = [];
            _renderDelegateEnd = [];
            super();
            invalidated = true;
            this.type = _arg1;
            _x = _arg2;
            _y = _arg3;
            _x1 = _arg4;
            _y1 = _arg5;
            _cx = _arg6;
            _cy = _arg7;
            if (_arg8){
                this.commandStack = _arg8;
            };
        }
        public function get y():Number{
            return (_y);
        }
        public function get commandStack():CommandStack{
            return (_commandStack);
        }
        public function get delegate():Function{
            return (_delegate);
        }
        public function set lengthInvalidated(_arg1:Boolean):void{
            _lengthInvalidated = _arg1;
            if (_lengthInvalidated != _arg1){
                _lengthInvalidated = _arg1;
                if (_lengthInvalidated){
                    parent.lengthInvalidated = _arg1;
                };
            };
        }
        public function get invalidated():Boolean{
            return (_invalidated);
        }
        public function set delegate(_arg1:Function):void{
            if (_delegate != _arg1){
                _delegate = _arg1;
                invalidated = true;
            };
        }
        private function curvePointAt(_arg1:Number, _arg2:Point=null, _arg3:Point=null):Point{
            var _local4:Point = Point.interpolate(control, start, _arg1);
            var _local5:Point = Point.interpolate(end, control, _arg1);
            return (Point.interpolate(_local5, _local4, _arg1));
        }
        public function get segmentLength():Number{
            if (((!(_segmentLength)) || (lengthInvalidated))){
                switch (type){
                    case CommandStackItem.MOVE_TO:
                        _segmentLength = 0;
                        break;
                    case CommandStackItem.LINE_TO:
                        _segmentLength = lineLength(start, end);
                        break;
                    case CommandStackItem.CURVE_TO:
                        _segmentLength = curveLength();
                        break;
                    case CommandStackItem.COMMAND_STACK:
                        _segmentLength = commandStack.pathLength;
                        break;
                    default:
                        _segmentLength = 0;
                        break;
                };
            };
            return (_segmentLength);
        }
        public function set invalidated(_arg1:Boolean):void{
            _invalidated = _arg1;
            if (_invalidated != _arg1){
                _invalidated = _arg1;
                if (_invalidated){
                    parent.invalidated = _invalidated;
                    lengthInvalidated = _invalidated;
                };
            };
        }
        private function curveAngleAt(_arg1:Number, _arg2:Point=null, _arg3:Point=null):Number{
            _arg2 = linePointAt(_arg1, start, control);
            _arg3 = linePointAt(_arg1, control, end);
            return (lineAngleAt(_arg1, _arg2, _arg3));
        }
        public function set y(_arg1:Number):void{
            if (_y != _arg1){
                _y = _arg1;
                invalidated = true;
            };
        }
        public function get renderDelegateEnd():Array{
            return (_renderDelegateEnd);
        }
        private function lineLength(_arg1:Point=null, _arg2:Point=null):Number{
            if (!_arg1){
                _arg1 = start;
            };
            if (!_arg2){
                _arg2 = end;
            };
            var _local3:Number = (_arg2.x - _arg1.x);
            var _local4:Number = (_arg2.y - _arg1.y);
            return (Math.sqrt(((_local3 * _local3) + (_local4 * _local4))));
        }
        public function set x1(_arg1:Number):void{
            if (_x1 != _arg1){
                _x1 = _arg1;
                invalidated = true;
            };
        }
        public function derive(_arg1:CommandStackItem):void{
            if (!type){
                type = _arg1.type;
            };
            if (!x){
                _x = _arg1.x;
            };
            if (!y){
                _y = _arg1.y;
            };
            if (!x1){
                _x1 = _arg1.x1;
            };
            if (!y1){
                _y1 = _arg1.y1;
            };
            if (!cx){
                _cx = _arg1.cx;
            };
            if (!cy){
                _cy = _arg1.cy;
            };
            if (!reference){
                reference = _arg1.reference;
            };
            invalidated = true;
        }
        public function get bounds():Rectangle{
            if (invalidated){
                calcBounds();
            };
            return (_bounds);
        }
        public function set end(_arg1:Point):void{
            if ((((type == 1)) || ((type == 0)))){
                x = _arg1.x;
                y = _arg1.y;
            } else {
                x1 = _arg1.x;
                y1 = _arg1.y;
            };
        }
        public function get x1():Number{
            return (_x1);
        }
        private function curveLength(_arg1:int=5, _arg2:Point=null, _arg3:Point=null, _arg4:Point=null):Number{
            var _local9:Number;
            var _local10:Number;
            var _local11:Number;
            var _local14:Point;
            var _local15:int;
            if (!_arg2){
                _arg2 = start;
            };
            if (!_arg3){
                _arg3 = control;
            };
            if (!_arg4){
                _arg4 = end;
            };
            var _local5:Number = (_arg4.x - _arg2.x);
            var _local6:Number = (_arg4.y - _arg2.y);
            var _local7:Number = ((_local5)==0) ? 0 : ((_arg3.x - _arg2.x) / _local5);
            var _local8:Number = ((_local6)==0) ? 0 : ((_arg3.y - _arg2.y) / _local6);
            var _local12:Number = 0;
            var _local13:Point = _arg2;
            _local15 = 1;
            while (_local15 < _arg1) {
                _local11 = (_local15 / _arg1);
                _local9 = ((2 * _local11) * (1 - _local11));
                _local10 = (_local11 * _local11);
                _local14 = new Point((_arg2.x + (_local5 * ((_local9 * _local7) + _local10))), (_arg2.y + (_local6 * ((_local9 * _local8) + _local10))));
                _local12 = (_local12 + lineLength(_local13, _local14));
                _local13 = _local14;
                _local15++;
            };
            return ((_local12 + lineLength(_local13, _arg4)));
        }
        public function set y1(_arg1:Number):void{
            if (_y1 != _arg1){
                _y1 = _arg1;
                invalidated = true;
            };
        }
        public function set renderDelegateEnd(_arg1:Array):void{
            if (_renderDelegateEnd != _arg1){
                _renderDelegateEnd = _arg1;
                invalidated = true;
            };
        }
        public function set control(_arg1:Point):void{
            cx = _arg1.x;
            cy = _arg1.y;
        }
        public function get lengthInvalidated():Boolean{
            return (_lengthInvalidated);
        }
        public function set renderDelegateStart(_arg1:Array):void{
            if (_renderDelegateStart != _arg1){
                _renderDelegateStart = _arg1;
                invalidated = true;
            };
        }
        private function linePointAt(_arg1:Number, _arg2:Point=null, _arg3:Point=null):Point{
            if (!_arg2){
                _arg2 = start;
            };
            if (!_arg3){
                _arg3 = end;
            };
            var _local4:Number = (_arg3.x - _arg2.x);
            var _local5:Number = (_arg3.y - _arg2.y);
            return (new Point((_arg2.x + (_local4 * _arg1)), (_arg2.y + (_local5 * _arg1))));
        }
        private function lineAngleAt(_arg1:Number, _arg2:Point=null, _arg3:Point=null):Number{
            if (!_arg2){
                _arg2 = start;
            };
            if (!_arg3){
                _arg3 = end;
            };
            return (Math.atan2((_arg3.y - _arg2.y), (_arg3.x - _arg2.x)));
        }
        public function get control():Point{
            return (new Point(cx, cy));
        }
        public function get y1():Number{
            return (_y1);
        }
        public function segmentAngleAt(_arg1:Number):Number{
            switch (type){
                case CommandStackItem.MOVE_TO:
                    return (0);
                case CommandStackItem.LINE_TO:
                    return (lineAngleAt(_arg1));
                case CommandStackItem.CURVE_TO:
                    return (curveAngleAt(_arg1));
                case CommandStackItem.COMMAND_STACK:
                    return (commandStack.pathAngleAt(_arg1));
                default:
                    return (0);
            };
        }
        public function get start():Point{
            if (_previous){
                if (_previous.skip){
                    return (_previous.start);
                };
                return (_previous.end);
                //unresolved jump
            };
            return (new Point(0, 0));
        }
        public function set next(_arg1:CommandStackItem):void{
            if (_next != _arg1){
                _next = _arg1;
                if (type == CommandStackItem.COMMAND_STACK){
                    commandStack.lastNonCommandStackItem.next = _arg1;
                };
            };
        }
        public function get renderDelegateStart():Array{
            return (_renderDelegateStart);
        }
        public function segmentPointAt(_arg1:Number):Point{
            switch (type){
                case CommandStackItem.MOVE_TO:
                    return (start.clone());
                case CommandStackItem.LINE_TO:
                    return (linePointAt(_arg1, start, end));
                case CommandStackItem.CURVE_TO:
                    return (curvePointAt(_arg1));
                case CommandStackItem.COMMAND_STACK:
                    return (commandStack.pathPointAt(_arg1));
                default:
                    return (null);
            };
        }
        public function calcBounds():void{
            var _local1:Point;
            if (!invalidated){
                return;
            };
            switch (type){
                case CommandStackItem.MOVE_TO:
                    if (((isNaN(_x)) || (isNaN(_y)))){
                        _bounds = new Rectangle();
                        skip = true;
                        break;
                    };
                    _bounds = new Rectangle(x, y, 0, 0);
                    break;
                case CommandStackItem.LINE_TO:
                    if (((isNaN(_x)) || (isNaN(_y)))){
                        _bounds = new Rectangle();
                        skip = true;
                        break;
                    };
                    _local1 = this.start;
                    _bounds = new Rectangle(Math.min(x, _local1.x), Math.min(y, _local1.y), Math.abs((x - _local1.x)), Math.abs((y - _local1.y)));
                    if (!_bounds.width){
                        _bounds.width = 0.0001;
                    };
                    if (!_bounds.height){
                        _bounds.height = 0.0001;
                    };
                    break;
                case CommandStackItem.CURVE_TO:
                    if (((((((isNaN(_cx)) || (isNaN(_cy)))) || (isNaN(_x1)))) || (isNaN(_y1)))){
                        _bounds = new Rectangle();
                        skip = true;
                        break;
                    };
                    _local1 = this.start;
                    _bounds = GeometryUtils.bezierBounds(_local1.x, _local1.y, cx, cy, x1, y1).clone();
                    break;
                case CommandStackItem.COMMAND_STACK:
                    _bounds = commandStack.bounds;
                    break;
            };
            invalidated = false;
        }
        public function set cy(_arg1:Number):void{
            if (_cy != _arg1){
                _cy = _arg1;
                invalidated = true;
            };
        }
        public function set previous(_arg1:CommandStackItem):void{
            if (_previous != _arg1){
                _previous = _arg1;
                if (_arg1.type == CommandStackItem.COMMAND_STACK){
                    _previous = _arg1.commandStack.lastNonCommandStackItem;
                };
                if ((((type == CommandStackItem.COMMAND_STACK)) && (commandStack.length))){
                    CommandStackItem(commandStack.source[0]).previous = _previous;
                };
            };
        }
        public function set x(_arg1:Number):void{
            if (_x != _arg1){
                _x = _arg1;
                invalidated = true;
            };
        }
        public function set cx(_arg1:Number):void{
            if (_cx != _arg1){
                _cx = _arg1;
                invalidated = true;
            };
        }
        public function get next():CommandStackItem{
            return (_next);
        }
        public function get cy():Number{
            return (_cy);
        }
        public function get previous():CommandStackItem{
            return (_previous);
        }
        public function get x():Number{
            return (_x);
        }
        public function set commandStack(_arg1:CommandStack):void{
            if (_commandStack != _arg1){
                _commandStack = _arg1;
                _arg1.parent = this;
                invalidated = true;
            };
        }
        public function get cx():Number{
            return (_cx);
        }
        public function get end():Point{
            return (new Point(((((type == 1)) || ((type == 0)))) ? _x : _x1, ((((type == 1)) || ((type == 0)))) ? _y : _y1));
        }

    }
}//package com.degrafa.geometry.command 
