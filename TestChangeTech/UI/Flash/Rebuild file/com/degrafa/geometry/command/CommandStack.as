//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.command {
    import flash.display.*;
    import flash.geom.*;
    import com.degrafa.geometry.*;
    import flash.net.*;
    import com.degrafa.core.collections.*;
    import com.degrafa.transform.*;
    import flash.filters.*;
    import com.degrafa.decorators.*;
    import com.degrafa.geometry.display.*;
    import com.degrafa.geometry.utilities.*;

    public class CommandStack {

        private var _pathLength:Number = 0
        public var source:Array
        private var hasRenderDecoration:Boolean
        public var parent:CommandStackItem
        private var _fxShape:Shape
        public var owner:Geometry
        private var _maskRender:Shape
        private var _globalRenderDelegateStart:Array
        private var _invalidated:Boolean = true
        private var _lengthInvalidated:Boolean = true
        private var _bounds:Rectangle
        private var _cursor:DegrafaCursor
        private var _globalRenderDelegateEnd:Array

        public static const IS_REGISTERED:Boolean = !(registerClassAlias("com.degrafa.geometry.command.CommandStack", CommandStack));

        public static var currentLayoutMatrix:Matrix = new Matrix();
        private static var transXY:Point = new Point();
        public static var transMatrix:Matrix = new Matrix();
        public static var currentTransformMatrix:Matrix = new Matrix();
        private static var transCP:Point = new Point();

        public function CommandStack(_arg1:Geometry=null){
            source = [];
            _globalRenderDelegateStart = [];
            _globalRenderDelegateEnd = [];
            _bounds = new Rectangle();
            super();
            this.owner = _arg1;
        }
        public function pathAngleAt(_arg1:Number):Number{
            var curLength:* = NaN;
            var item:* = null;
            var firstSegment:* = null;
            var t:* = _arg1;
            if (!source.length){
                return (0);
            };
            t = cleant(t);
            curLength = 0;
            if (t == 0){
                firstSegment = firstSegmentWithLength;
                curLength = firstSegment.segmentLength;
                return (firstSegment.segmentAngleAt(t));
            };
            if (t == 1){
                return (lastSegmentWithLength.segmentAngleAt(t));
            };
            var tLength:* = (t * pathLength);
            var lastLength:* = 0;
            var n:* = source.length;
            for each (item in source) {
                var _local5 = item;
                with (_local5) {
                    if (type != 0){
                        curLength = (curLength + segmentLength);
                        //unresolved jump
                    };
                };
                continue;
                if (tLength <= curLength){
                    return (segmentAngleAt(((tLength - lastLength) / segmentLength)));
                };
                lastLength = curLength;
            };
            return (0);
        }
        public function addCubicBezierTo(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number, _arg7:Number, _arg8:Number, _arg9:int=1):Array{
            return (GeometryUtils.cubicToQuadratic(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, 1, this));
        }
        public function pathPointAt(_arg1:Number):Point{
            var curLength:* = NaN;
            var item:* = null;
            var firstSegment:* = null;
            var t:* = _arg1;
            if (!source.length){
                return (new Point(0, 0));
            };
            t = cleant(t);
            curLength = 0;
            if (t == 0){
                firstSegment = firstSegmentWithLength;
                curLength = firstSegment.segmentLength;
                return (adjustPointToLayoutAndTransform(firstSegment.segmentPointAt(t)));
            };
            if (t == 1){
                return (adjustPointToLayoutAndTransform(lastSegmentWithLength.segmentPointAt(t)));
            };
            var tLength:* = (t * pathLength);
            var lastLength:* = 0;
            var n:* = source.length;
            for each (item in source) {
                var _local5 = item;
                with (_local5) {
                    if (type != 0){
                        curLength = (curLength + segmentLength);
                        //unresolved jump
                    };
                };
                continue;
                if (tLength <= curLength){
                    return (adjustPointToLayoutAndTransform(segmentPointAt(((tLength - lastLength) / segmentLength))));
                };
                lastLength = curLength;
            };
            return (new Point(0, 0));
        }
        public function set lengthInvalidated(_arg1:Boolean):void{
            if (_lengthInvalidated != _arg1){
                _lengthInvalidated = _arg1;
            };
        }
        public function set globalRenderDelegateStart(_arg1:Array):void{
            if (_globalRenderDelegateStart != _arg1){
                _globalRenderDelegateStart = _arg1;
                invalidated = true;
            };
        }
        public function get invalidated():Boolean{
            return (_invalidated);
        }
        public function addLineTo(_arg1:Number, _arg2:Number):CommandStackItem{
            var _local3:int = (source.push(new CommandStackItem(CommandStackItem.LINE_TO, _arg1, _arg2, NaN, NaN, NaN, NaN)) - 1);
            updateItemRelations(source[_local3], _local3);
            source[_local3].indexInParent = _local3;
            source[_local3].parent = this;
            invalidated = true;
            return (source[_local3]);
        }
        public function set invalidated(_arg1:Boolean):void{
            if (_invalidated != _arg1){
                _invalidated = _arg1;
                if (_invalidated){
                    lengthInvalidated = true;
                };
            };
        }
        private function updateToFilterRectangle(_arg1:Rectangle, _arg2:DisplayObject):Rectangle{
            var _local4:BitmapFilter;
            var _local3:BitmapData = new BitmapData(_arg1.width, _arg1.height, true, 0);
            for each (_local4 in owner.filters) {
                _arg1 = _arg1.union(_local3.generateFilterRect(_arg1, _local4));
            };
            return (_arg1);
        }
        public function get firstSegmentWithLength():CommandStackItem{
            var _local1:CommandStackItem;
            for each (_local1 in source) {
                switch (_local1.type){
                    case 1:
                    case 2:
                        return (_local1);
                    case 4:
                        return (_local1.commandStack.firstSegmentWithLength);
                };
            };
            return (source[0]);
        }
        public function getItem(_arg1:int):CommandStackItem{
            return (source[_arg1]);
        }
        private function processDelegateArray(_arg1:Array, _arg2:CommandStackItem, _arg3:Graphics, _arg4:int):CommandStackItem{
            var _local5:Function;
            for each (_local5 in _arg1) {
                _arg2 = _local5(this, _arg2, _arg3, _arg4);
            };
            return (_arg2);
        }
        public function get lastNonCommandStackItem():CommandStackItem{
            var _local1:int = (source.length - 1);
            while (_local1 > 0) {
                if (source[_local1].type != CommandStackItem.COMMAND_STACK){
                    return (source[_local1]);
                };
                return (CommandStackItem(source[_local1]).commandStack.lastNonCommandStackItem);
            };
            return (source[0]);
        }
        public function addMoveTo(_arg1:Number, _arg2:Number):CommandStackItem{
            var _local3:int = (source.push(new CommandStackItem(CommandStackItem.MOVE_TO, _arg1, _arg2, NaN, NaN, NaN, NaN)) - 1);
            updateItemRelations(source[_local3], _local3);
            source[_local3].indexInParent = _local3;
            return (source[_local3]);
        }
        private function delegateGraphicsCall(_arg1:String, _arg2:Graphics, _arg3:Number=0, _arg4:Number=0, _arg5:Number=0, _arg6:Number=0, _arg7:Number=0, _arg8:Number=0){
            var _local9:IRenderDecorator;
            for each (_local9 in owner.decorators) {
                switch (_arg1){
                    case "moveTo":
                        return (_local9.moveTo(_arg3, _arg4, _arg2));
                    case "lineTo":
                        return (_local9.lineTo(_arg3, _arg4, _arg2));
                    case "curveTo":
                        return (_local9.curveTo(_arg5, _arg6, _arg7, _arg8, _arg2));
                };
            };
        }
        private function renderCommandStack(_arg1:Graphics, _arg2:Rectangle, _arg3:DegrafaCursor=null):void{
            var item:* = null;
            var graphics:* = _arg1;
            var rc:* = _arg2;
            var cursor = _arg3;
            while (cursor.moveNext()) {
                item = cursor.current;
                if (item.renderDelegateStart.length != 0){
                    item = processDelegateArray(item.renderDelegateStart, item, graphics, cursor.currentIndex);
                };
                if (_globalRenderDelegateStart.length != 0){
                    item = processDelegateArray(_globalRenderDelegateStart, item, graphics, cursor.currentIndex);
                };
                if (item.skip){
                } else {
                    var _local5 = item;
                    with (_local5) {
                        switch (type){
                            case CommandStackItem.MOVE_TO:
                                if (transMatrix){
                                    transXY.x = x;
                                    transXY.y = y;
                                    transXY = transMatrix.transformPoint(transXY);
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("moveTo", graphics, transXY.x, transXY.y);
                                    } else {
                                        graphics.moveTo(transXY.x, transXY.y);
                                    };
                                } else {
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("moveTo", graphics, x, y);
                                    } else {
                                        graphics.moveTo(x, y);
                                    };
                                };
                                break;
                            case CommandStackItem.LINE_TO:
                                if (transMatrix){
                                    transXY.x = x;
                                    transXY.y = y;
                                    transXY = transMatrix.transformPoint(transXY);
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("lineTo", graphics, transXY.x, transXY.y);
                                    } else {
                                        graphics.lineTo(transXY.x, transXY.y);
                                    };
                                } else {
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("lineTo", graphics, x, y);
                                    } else {
                                        graphics.lineTo(x, y);
                                    };
                                };
                                break;
                            case CommandStackItem.CURVE_TO:
                                if (transMatrix){
                                    transXY.x = x1;
                                    transXY.y = y1;
                                    transCP.x = cx;
                                    transCP.y = cy;
                                    transXY = transMatrix.transformPoint(transXY);
                                    transCP = transMatrix.transformPoint(transCP);
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("curveTo", graphics, 0, 0, transCP.x, transCP.y, transXY.x, transXY.y);
                                    } else {
                                        graphics.curveTo(transCP.x, transCP.y, transXY.x, transXY.y);
                                    };
                                } else {
                                    if (hasRenderDecoration){
                                        delegateGraphicsCall("curveTo", graphics, 0, 0, cx, cy, x1, y1);
                                    } else {
                                        graphics.curveTo(cx, cy, x1, y1);
                                    };
                                };
                                break;
                            case CommandStackItem.DELEGATE_TO:
                                item.delegate(this, item, graphics, cursor.currentIndex);
                                break;
                            case CommandStackItem.COMMAND_STACK:
                                renderCommandStack(graphics, rc, new DegrafaCursor(commandStack.source));
                                break;
                        };
                    };
                    if (item.renderDelegateEnd.length != 0){
                        item = processDelegateArray(item.renderDelegateEnd, item, graphics, cursor.currentIndex);
                    };
                    if (_globalRenderDelegateEnd.length != 0){
                        item = processDelegateArray(_globalRenderDelegateEnd, item, graphics, cursor.currentIndex);
                    };
                };
            };
        }
        public function get bounds():Rectangle{
            var _local1:CommandStackItem;
            if (!invalidated){
                return (_bounds);
            };
            _bounds.setEmpty();
            for each (_local1 in source) {
                if (_local1.bounds){
                    _bounds = _bounds.union(_local1.bounds);
                };
            };
            invalidated = false;
            if (_bounds.height != 0.0001){
                _bounds.height = Number(_bounds.height.toPrecision(3));
            };
            if (_bounds.width != 0.0001){
                _bounds.width = Number(_bounds.width.toPrecision(3));
            };
            if (_bounds.isEmpty()){
                invalidated = true;
            };
            return (_bounds);
        }
        public function get globalRenderDelegateEnd():Array{
            return (_globalRenderDelegateEnd);
        }
        public function addCommandStack(_arg1:CommandStack):CommandStackItem{
            var _local2:int = (source.push(new CommandStackItem(CommandStackItem.COMMAND_STACK, NaN, NaN, NaN, NaN, NaN, NaN, _arg1)) - 1);
            updateItemRelations(source[_local2], _local2);
            source[_local2].indexInParent = _local2;
            source[_local2].parent = this;
            invalidated = true;
            return (source[_local2]);
        }
        public function addItemAt(_arg1:CommandStackItem, _arg2:int=-1):CommandStackItem{
            var _local3:int = _arg2;
            if (_local3 == -1){
                _local3 = (source.push(_arg1) - 1);
            } else {
                source.splice(_local3, 0, _arg1);
                _local3 = (_local3 + -1);
            };
            updateItemRelations(source[_local3], _local3);
            _arg1.indexInParent = _local3;
            _arg1.parent = this;
            invalidated = true;
            return (source[_local3]);
        }
        public function get lengthInvalidated():Boolean{
            return (_lengthInvalidated);
        }
        public function draw(_arg1:Graphics, _arg2:Rectangle):void{
            var _local3:IDecorator;
            var _local4:DisplayObject;
            var _local5:DisplayObject;
            var _local6:Matrix;
            var _local7:Boolean;
            var _local8:Matrix;
            var _local9:Matrix;
            var _local10:Matrix;
            if ((((source.length == 0)) && (!((owner is IDisplayObjectProxy))))){
                return;
            };
            if (owner.hasDecorators){
                for each (_local3 in owner.decorators) {
                    _local3.initialize(this);
                    if ((_local3 is IRenderDecorator)){
                        hasRenderDecoration = true;
                    };
                };
            };
            predraw();
            if ((owner is IDisplayObjectProxy)){
                if (!IDisplayObjectProxy(owner).displayObject){
                    return;
                };
                _local4 = IDisplayObjectProxy(owner).displayObject;
                if (owner.hasFilters){
                    _local4.filters = owner.filters;
                };
                if (((transMatrix) && (((IDisplayObjectProxy(owner).transformBeforeRender) || (((owner._layoutMatrix) && ((IDisplayObjectProxy(owner).layoutMode == "scale")))))))){
                    if (Sprite(_local4).numChildren != 0){
                        _local5 = Sprite(_local4).getChildAt(0);
                        if (!IDisplayObjectProxy(owner).transformBeforeRender){
                            _local5.transform.matrix = CommandStack.currentLayoutMatrix;
                        } else {
                            if (IDisplayObjectProxy(owner).layoutMode == "scale"){
                                _local5.transform.matrix = CommandStack.transMatrix;
                            } else {
                                if (owner._layoutMatrix){
                                    _local6 = owner._layoutMatrix.clone();
                                    _local6.a = 1;
                                    _local6.d = 1;
                                    _local6.concat(CommandStack.currentTransformMatrix);
                                    _local5.transform.matrix = _local6;
                                } else {
                                    _local5.transform.matrix = CommandStack.currentTransformMatrix;
                                };
                            };
                        };
                    };
                };
                owner.initStroke(_arg1, _arg2);
                renderBitmapDatatoContext(IDisplayObjectProxy(owner).displayObject, _arg1, !(IDisplayObjectProxy(owner).transformBeforeRender), _arg2);
            } else {
                _cursor = new DegrafaCursor(source);
                _local7 = !((owner.mask == null));
                if (((owner.hasFilters) || (_local7))){
                    if (!_fxShape){
                        _fxShape = new Shape();
                    } else {
                        _fxShape.graphics.clear();
                    };
                    if (_local7){
                        if (!_maskRender){
                            _maskRender = new Shape();
                        };
                        _maskRender.graphics.clear();
                        _local8 = currentLayoutMatrix.clone();
                        _local9 = currentTransformMatrix.clone();
                        _local10 = transMatrix.clone();
                        owner.mask.draw(_maskRender.graphics, owner.mask.bounds);
                        currentLayoutMatrix = _local8;
                        currentTransformMatrix = _local9;
                        transMatrix = _local10;
                        _fxShape.mask = _maskRender;
                    } else {
                        if (_fxShape.mask){
                            _fxShape.mask = null;
                        };
                    };
                    owner.initStroke(_fxShape.graphics, _arg2);
                    owner.initFill(_fxShape.graphics, _arg2);
                    renderCommandStack(_fxShape.graphics, _arg2, _cursor);
                    renderBitmapDatatoContext(_fxShape, _arg1);
                } else {
                    owner.initStroke(_arg1, _arg2);
                    owner.initFill(_arg1, _arg2);
                    renderCommandStack(_arg1, _arg2, _cursor);
                };
            };
        }
        public function addDelegate(_arg1:Function):CommandStackItem{
            var _local2:int = (source.push(new CommandStackItem(CommandStackItem.DELEGATE_TO)) - 1);
            source[_local2].delegate = _arg1;
            updateItemRelations(source[_local2], _local2);
            source[_local2].indexInParent = _local2;
            source[_local2].parent = this;
            return (source[_local2]);
        }
        public function get transformBounds():Rectangle{
            if (transMatrix){
                return (TransformBase.transformBounds(_bounds.clone(), transMatrix));
            };
            return (_bounds);
        }
        public function get globalRenderDelegateStart():Array{
            return (_globalRenderDelegateStart);
        }
        public function set length(_arg1:int):void{
            source.length = _arg1;
            invalidated = true;
        }
        public function get firstNonCommandStackItem():CommandStackItem{
            var _local1:int = (source.length - 1);
            while (_local1 < (source.length - 1)) {
                if (source[_local1].type != CommandStackItem.COMMAND_STACK){
                    return (source[_local1]);
                };
                return (CommandStackItem(source[_local1]).commandStack.firstNonCommandStackItem);
            };
            return (null);
        }
        private function cleant(_arg1:Number, _arg2:Number=NaN):Number{
            if (isNaN(_arg1)){
                _arg1 = _arg2;
            } else {
                if ((((_arg1 < 0)) || ((_arg1 > 1)))){
                    _arg1 = (_arg1 % 1);
                    if (_arg1 == 0){
                        _arg1 = _arg2;
                    } else {
                        if (_arg1 < 0){
                            _arg1 = (_arg1 + 1);
                        };
                    };
                };
            };
            return (_arg1);
        }
        public function get cursor():DegrafaCursor{
            if (!_cursor){
                _cursor = new DegrafaCursor(source);
            };
            return (_cursor);
        }
        private function updateItemRelations(_arg1:CommandStackItem, _arg2:int):void{
            _arg1.previous = ((_arg2)>0) ? source[(_arg2 - 1)] : null;
            if (_arg1.previous){
                if (_arg1.previous.type == CommandStackItem.COMMAND_STACK){
                    _arg1.previous = _arg1.previous.commandStack.lastNonCommandStackItem;
                };
                _arg1.previous.next = ((_arg1.type)==CommandStackItem.COMMAND_STACK) ? _arg1.commandStack.firstNonCommandStackItem : _arg1;
            };
        }
        public function addItem(_arg1:CommandStackItem):CommandStackItem{
            var _local2:int = (source.push(_arg1) - 1);
            updateItemRelations(source[_local2], _local2);
            _arg1.indexInParent = _local2;
            _arg1.parent = this;
            invalidated = true;
            return (source[_local2]);
        }
        public function resetBounds():void{
            if (_bounds){
                _bounds.setEmpty();
                invalidated = true;
            };
        }
        private function renderBitmapDatatoContext(_arg1:DisplayObject, _arg2:Graphics, _arg3:Boolean=false, _arg4:Rectangle=null):void{
            var _local7:BitmapData;
            var _local9:Matrix;
            var _local10:Matrix;
            var _local11:Matrix;
            if (!_arg1){
                return;
            };
            var _local5:Rectangle = _arg1.getBounds(_arg1);
            if (owner.mask){
                _local5 = _local5.intersection(_maskRender.getBounds(_maskRender));
            };
            if (_local5.isEmpty()){
                return;
            };
            var _local6:Rectangle = _local5.clone();
            if (owner.hasFilters){
                _arg1.filters = owner.filters;
                _local6.x = (_local6.y = 0);
                _local6.width = Math.ceil(_local6.width);
                _local6.height = Math.ceil(_local6.height);
                if (((!(_local6.width)) || (!(_local6.height)))){
                    return;
                };
                _local6 = updateToFilterRectangle(_local6, _arg1);
                _local6.offset(_local5.x, _local5.y);
            };
            var _local8:Rectangle = (owner.clippingRectangle) ? owner.clippingRectangle : null;
            if ((((_local6.width < 1)) || ((_local6.height < 1)))){
                return;
            };
            _local6.y = Math.floor(_local6.y);
            _local6.width = Math.ceil((_local6.width + (_local6.x - (_local6.x = Math.floor(_local6.x)))));
            _local6.height = Math.ceil((_local6.height + (_local6.y - (_local6.y = Math.floor(_local6.y)))));
            if ((((_local6.width > 2880)) || ((_local6.height > 2880)))){
                return;
            };
            if ((owner is IDisplayObjectProxy)){
                _local7 = new BitmapData((_local6.width + 4), (_local6.height + 4), true, 0);
                _local9 = new Matrix(1, 0, 0, 1, (2 - _local6.x), (2 - _local6.y));
            } else {
                _local7 = new BitmapData(_local6.width, _local6.height, true, 0);
                _local9 = new Matrix(1, 0, 0, 1, -(_local6.x), -(_local6.y));
            };
            _local7.draw(_arg1, _local9, null, null, _local8, true);
            _local9.invert();
            if (!_arg3){
                if (((((owner.hasFilters) && (!(_local5.equals(_local6))))) && ((owner is IDisplayObjectProxy)))){
                    _local9 = new Matrix((_local5.width / _local6.width), 0, 0, (_local5.height / _local6.height), _local9.tx, _local9.ty);
                    _arg2.beginBitmapFill(_local7, _local9, false, true);
                    _arg2.drawRect(Math.floor(_local5.x), Math.floor(_local5.y), Math.ceil(_local5.width), Math.ceil(_local5.height));
                    _arg2.endFill();
                } else {
                    _arg2.beginBitmapFill(_local7, _local9, false, true);
                    _arg2.drawRect(_local6.x, _local6.y, _local6.width, _local6.height);
                    _arg2.endFill();
                };
            } else {
                if (transMatrix){
                    if ((owner is IDisplayObjectProxy)){
                        if (((owner._layoutMatrix) && ((IDisplayObjectProxy(owner).layoutMode == "scale")))){
                            _local9.concat(CommandStack.currentTransformMatrix);
                        } else {
                            _local9.concat(currentTransformMatrix);
                            transMatrix = currentTransformMatrix;
                        };
                    } else {
                        _local9.concat(transMatrix);
                    };
                };
                _arg2.beginBitmapFill(_local7, _local9, false, true);
                renderCommandStack(_arg2, _arg4, new DegrafaCursor(this.source));
            };
        }
        private function predraw():void{
            var _local4:Rectangle;
            var _local1:Geometry = owner;
            while (_local1.parent) {
                _local1 = (_local1.parent as Geometry);
                if (_local1.transform){
                    owner.transformContext = _local1.transform.getTransformFor(_local1);
                    break;
                };
            };
            var _local2:Boolean = owner.hasLayout;
            transMatrix = null;
            currentLayoutMatrix.identity();
            if (_local2){
                _local4 = ((owner is IDisplayObjectProxy)) ? owner.bounds : bounds;
                if (!_local4.equals(owner.layoutRectangle)){
                    currentLayoutMatrix.translate(-(_local4.x), -(_local4.y));
                    currentLayoutMatrix.scale((owner.layoutRectangle.width / _local4.width), (owner.layoutRectangle.height / _local4.height));
                    currentLayoutMatrix.translate(owner.layoutRectangle.x, owner.layoutRectangle.y);
                    owner._layoutMatrix = currentLayoutMatrix.clone();
                    transMatrix = currentLayoutMatrix.clone();
                } else {
                    _local2 = false;
                    owner._layoutMatrix = null;
                    currentLayoutMatrix.identity();
                };
            } else {
                if (owner._layoutMatrix){
                    owner._layoutMatrix = null;
                };
            };
            var _local3:Boolean = ((owner.transformContext) || (((owner.transform) && (!(owner.transform.isIdentity)))));
            if (_local3){
                currentTransformMatrix = (owner.transform) ? owner.transform.getTransformFor(owner) : owner.transformContext;
                if (!_local2){
                    transMatrix = (owner.transform) ? owner.transform.getTransformFor(owner) : owner.transformContext;
                } else {
                    transMatrix.concat((owner.transform) ? owner.transform.getTransformFor(owner) : owner.transformContext);
                };
            } else {
                currentTransformMatrix.identity();
                if (!_local2){
                    transMatrix = null;
                };
            };
        }
        public function addCurveTo(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):CommandStackItem{
            var _local5:int = (source.push(new CommandStackItem(CommandStackItem.CURVE_TO, NaN, NaN, _arg3, _arg4, _arg1, _arg2)) - 1);
            updateItemRelations(source[_local5], _local5);
            source[_local5].indexInParent = _local5;
            source[_local5].parent = this;
            invalidated = true;
            return (source[_local5]);
        }
        public function get pathLength():Number{
            var _local1:CommandStackItem;
            if (lengthInvalidated){
                lengthInvalidated = false;
                _pathLength = 0;
                for each (_local1 in source) {
                    _pathLength = (_pathLength + _local1.segmentLength);
                };
            };
            return (_pathLength);
        }
        public function get length():int{
            return (source.length);
        }
        public function get lastSegmentWithLength():CommandStackItem{
            var _local1:int = (source.length - 1);
            while (_local1 > 0) {
                if ((((source[_local1].type == 1)) || ((source[_local1].type == 2)))){
                    return (source[_local1]);
                };
                if (source[_local1].type == 4){
                    return (source[_local1].commandStack.lastSegmentWithLength);
                };
                _local1--;
            };
            return (source[(length - 1)]);
        }
        public function adjustPointToLayoutAndTransform(_arg1:Point):Point{
            if (!owner){
                return (_arg1);
            };
            if (transMatrix){
                return (transMatrix.transformPoint(_arg1));
            };
            return (_arg1);
        }
        public function set globalRenderDelegateEnd(_arg1:Array):void{
            if (_globalRenderDelegateEnd != _arg1){
                _globalRenderDelegateEnd = _arg1;
                invalidated = true;
            };
        }

    }
}//package com.degrafa.geometry.command 
