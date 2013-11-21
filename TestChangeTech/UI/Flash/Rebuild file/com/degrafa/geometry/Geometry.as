//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry {
    import flash.display.*;
    import flash.geom.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.binding.utils.*;
    import flash.utils.*;
    import com.degrafa.*;
    import com.degrafa.geometry.command.*;
    import com.degrafa.core.*;
    import com.degrafa.core.collections.*;
    import com.degrafa.transform.*;
    import com.degrafa.geometry.layout.*;
    import com.degrafa.states.*;
    import com.degrafa.events.*;
    import com.degrafa.triggers.*;

    public class Geometry extends DegrafaObject implements IDegrafaObject, IGeometryComposition, IDegrafaStateClient, ISimpleStyleClient {

        private var _graphicsTarget:DisplayObjectCollection
        private var methodQue:Array
        public var hasFilters:Boolean
        private var _stateEvent:String
        private var _autoClearGraphicsTarget:Boolean = true
        private var _clippingRectangle:Rectangle = null
        private var _styleName:Object
        private var _fill:IGraphicsFill
        public var hasLayout:Boolean
        public var hasTriggers:Boolean
        private var _currentState:String = ""
        private var _data:String
        private var _filters:FilterCollection
        private var _invalidated:Boolean
        protected var _currentGraphicsTarget:Sprite
        private var _transformContext:Matrix
        private var targetDictionary:Dictionary
        private var _inheritStroke:Boolean = true
        protected var _layoutConstraint:LayoutConstraint
        private var _triggers:Array
        private var _states:Array
        protected var _layoutRectangle:Rectangle
        private var _visible:Boolean = true
        private var _decorators:DecoratorCollection
        public var hasStates:Boolean
        private var initialRenderComplete:Boolean = false
        private var _stroke:IGraphicsStroke
        private var _inheritFill:Boolean = true
        public var _layoutMatrix:Matrix
        private var _state:String
        private var _mask:IGeometryComposition
        private var _geometry:GeometryCollection
        public var hasDecorators:Boolean
        private var stateManager:StateManager
        private var _isRootGeometry:Boolean = false
        private var _commandStack:CommandStack
        private var _transform:ITransform
        private var _stage:Stage

        public function Geometry(){
            methodQue = [];
            targetDictionary = new Dictionary(true);
            _triggers = [];
            _states = [];
            super();
        }
        public function get states():Array{
            return (_states);
        }
        private function onTargetRender(_arg1:Event):void{
            if (!_stage){
                _stage = _arg1.currentTarget.stage;
            };
            if (_stage){
                _arg1.currentTarget.removeEventListener(Event.RENDER, onTargetRender);
                _arg1.currentTarget.removeEventListener(Event.ADDED_TO_STAGE, onTargetRender);
                if ((_arg1.currentTarget is UIComponent)){
                    _arg1.currentTarget.removeEventListener(FlexEvent.UPDATE_COMPLETE, onTargetRender);
                };
            } else {
                return;
            };
            if ((_arg1.currentTarget is UIComponent)){
                initLayoutChangeWatcher((_arg1.currentTarget as UIComponent));
            };
            initDrawQue();
            queDraw(_arg1.currentTarget, _arg1.currentTarget.graphics, null);
        }
        public function get left():Number{
            return ((hasLayout) ? layoutConstraint.left : NaN);
        }
        public function get maintainAspectRatio():Boolean{
            return ((hasLayout) ? layoutConstraint.maintainAspectRatio : false);
        }
        public function set states(_arg1:Array):void{
            var _local2:State;
            _states = _arg1;
            if (_arg1){
                if (!stateManager){
                    stateManager = new StateManager(this);
                    for each (_local2 in _states) {
                        _local2.stateManager = stateManager;
                    };
                };
            } else {
                stateManager = null;
            };
            if (((_arg1) && (!((_arg1.length == 0))))){
                hasStates = true;
            } else {
                hasStates = false;
            };
        }
        public function set maintainAspectRatio(_arg1:Boolean):void{
            layoutConstraint.maintainAspectRatio = _arg1;
        }
        public function set left(_arg1:Number):void{
            layoutConstraint.left = _arg1;
        }
        public function get minHeight():Number{
            return ((hasLayout) ? layoutConstraint.minHeight : NaN);
        }
        public function get right():Number{
            return ((hasLayout) ? layoutConstraint.right : NaN);
        }
        public function set inheritStroke(_arg1:Boolean):void{
            _inheritStroke = _arg1;
        }
        private function onTargetChange(_arg1:Event):void{
            var _local3:Array;
            var _local4:ChangeWatcher;
            var _local5:UIComponent;
            var _local6:Rectangle;
            var _local2:Object = requestTarget((_arg1.currentTarget as UIComponent));
            if (_local2){
                _local3 = _local2.data.watchers;
            };
            if (_local2){
                if (!hasLayout){
                    for each (_local4 in _local3) {
                        _local4.unwatch();
                    };
                    removeTarget((_arg1.currentTarget as UIComponent));
                    return;
                };
            };
            if (_local2){
                _local5 = (_arg1.currentTarget as UIComponent);
                _local6 = new Rectangle(_local5.x, _local5.y, _local5.width, _local5.height);
                if (!_local6.equals(_local2.data.oldbounds)){
                    addUpdateTarget(_local5, {oldbounds:_local6, watchers:_local3});
                    queDraw(_local5, _local5.graphics, null);
                };
            };
        }
        public function get bottom():Number{
            return ((hasLayout) ? layoutConstraint.bottom : NaN);
        }
        public function calculateLayout(_arg1:Rectangle=null):void{
            var _local2:Rectangle;
            if (_layoutConstraint){
                if (!_arg1){
                    _arg1 = new Rectangle(0, 0, 1, 1);
                };
                if (((parent) && ((parent is Geometry)))){
                    _layoutConstraint.computeLayoutRectangle(_arg1, Geometry(parent).layoutRectangle);
                    return;
                };
                if (_currentGraphicsTarget){
                    _local2 = _currentGraphicsTarget.getRect(_currentGraphicsTarget);
                    if (_local2.isEmpty()){
                        if (((!((_currentGraphicsTarget.width == 0))) || (!((_currentGraphicsTarget.height == 0))))){
                            _local2.x = _currentGraphicsTarget.x;
                            _local2.y = _currentGraphicsTarget.y;
                            _local2.width = _currentGraphicsTarget.width;
                            _local2.height = _currentGraphicsTarget.height;
                        };
                    };
                    _layoutConstraint.computeLayoutRectangle(_arg1, _local2);
                    return;
                };
                if (document){
                    _layoutConstraint.computeLayoutRectangle(_arg1, new Rectangle(document.x, document.y, document.width, document.height));
                    return;
                };
            };
        }
        public function set minHeight(_arg1:Number):void{
            layoutConstraint.minHeight = _arg1;
        }
        public function get stroke():IGraphicsStroke{
            return (_stroke);
        }
        public function set inheritFill(_arg1:Boolean):void{
            _inheritFill = _arg1;
        }
        public function get minX():Number{
            return ((hasLayout) ? layoutConstraint.minX : NaN);
        }
        public function get minY():Number{
            return ((hasLayout) ? layoutConstraint.minY : NaN);
        }
        public function set height(_arg1:Number):void{
            layoutConstraint.height = _arg1;
        }
        public function set right(_arg1:Number):void{
            layoutConstraint.right = _arg1;
        }
        public function set bottom(_arg1:Number):void{
            layoutConstraint.bottom = _arg1;
        }
        public function angleAt(_arg1:Number):Number{
            return (commandStack.pathAngleAt(_arg1));
        }
        public function set stroke(_arg1:IGraphicsStroke):void{
            var _local2:Object;
            if (_stroke != _arg1){
                _local2 = _stroke;
                if (_stroke){
                    if (_stroke.hasEventManager){
                        _stroke.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                    };
                };
                _stroke = _arg1;
                if (((enableEvents) && (_stroke))){
                    _stroke.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler, false, 0, true);
                };
                initChange("stroke", _local2, _stroke, this);
            };
        }
        private function processMethodQue(_arg1:Event):void{
            if (methodQue.length == 0){
                return;
            };
            var _local2:Array = methodQue;
            methodQue = [];
            var _local3:int = _local2.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local2[_local4].method.apply(null, [_local2[_local4].args[0]]);
                _local4++;
            };
            _local2.length = 0;
            if ((((methodQue.length == 0)) && (_stage))){
                _stage.removeEventListener(Event.ENTER_FRAME, processMethodQue);
            };
        }
        public function get geometry():Array{
            initGeometryCollection();
            return (_geometry.items);
        }
        public function get x():Number{
            return ((hasLayout) ? layoutConstraint.x : NaN);
        }
        public function get y():Number{
            return ((hasLayout) ? layoutConstraint.y : NaN);
        }
        public function get visible():Boolean{
            return (_visible);
        }
        public function get geometricLength():Number{
            return (commandStack.pathLength);
        }
        public function get percentWidth():Number{
            return ((hasLayout) ? layoutConstraint.percentWidth : NaN);
        }
        public function set minX(_arg1:Number):void{
            layoutConstraint.minX = _arg1;
        }
        public function preDraw():void{
        }
        public function get invalidated():Boolean{
            return (_invalidated);
        }
        public function get state():String{
            return (_state);
        }
        public function get minWidth():Number{
            return ((hasLayout) ? layoutConstraint.minWidth : NaN);
        }
        public function set minY(_arg1:Number):void{
            layoutConstraint.minY = _arg1;
        }
        public function get stateEvent():String{
            return (_stateEvent);
        }
        public function get currentState():String{
            return ((stateManager) ? stateManager.currentState : "");
        }
        public function get transformContext():Matrix{
            return (_transformContext);
        }
        public function get layoutConstraint():LayoutConstraint{
            if (!_layoutConstraint){
                layoutConstraint = new LayoutConstraint();
            };
            return (_layoutConstraint);
        }
        public function set top(_arg1:Number):void{
            layoutConstraint.top = _arg1;
        }
        public function get geometryCollection():GeometryCollection{
            initGeometryCollection();
            return (_geometry);
        }
        public function initFill(_arg1:Graphics, _arg2:Rectangle):void{
            if (parent){
                if (((((inheritFill) && (!(_fill)))) && ((parent is Geometry)))){
                    _fill = Geometry(parent).fill;
                };
            };
            if (_fill){
                if ((_fill is ITransformablePaint)){
                    (_fill as ITransformablePaint).requester = this;
                };
                _fill.begin(_arg1, (_arg2) ? _arg2 : null);
            };
        }
        public function get bounds():Rectangle{
            return (commandStack.bounds);
        }
        public function endDraw(_arg1:Graphics):void{
            var _local2:IGeometryComposition;
            if (fill){
                _arg1.lineStyle.apply(_arg1, null);
                fill.end(_arg1);
            };
            if (((stroke) && (!(fill)))){
                _arg1.moveTo.call(_arg1, null, null);
            };
            if (geometry){
                for each (_local2 in geometry) {
                    _local2.draw(_arg1, null);
                };
            };
            dispatchEvent(new DegrafaEvent(DegrafaEvent.RENDER));
        }
        public function get maxWidth():Number{
            return ((hasLayout) ? layoutConstraint.maxWidth : NaN);
        }
        public function get clippingRectangle():Rectangle{
            return (_clippingRectangle);
        }
        public function set transform(_arg1:ITransform):void{
            var _local2:Object;
            if (((parent) && ((parent as Geometry).transform))){
                _transformContext = (parent as Geometry).transform.getTransformFor((parent as Geometry));
            };
            if (_transform != _arg1){
                _local2 = _transform;
                if (_transform){
                    if (_transform.hasEventManager){
                        _transform.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                    };
                };
                _transform = _arg1;
                if (enableEvents){
                    _transform.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler, false, 0, true);
                };
                initChange("transform", _local2, _transform, this);
            };
        }
        public function set verticalCenter(_arg1:Number):void{
            layoutConstraint.verticalCenter = _arg1;
        }
        public function set visible(_arg1:Boolean):void{
            var _local2:Boolean;
            if (_visible != _arg1){
                _local2 = _visible;
                _visible = _arg1;
                invalidated = true;
                initChange("visible", _local2, _visible, this);
            };
        }
        public function set maxX(_arg1:Number):void{
            layoutConstraint.maxX = _arg1;
        }
        public function set geometry(_arg1:Array):void{
            initGeometryCollection();
            _geometry.items = _arg1;
        }
        public function get mask():IGeometryComposition{
            return (_mask);
        }
        public function set x(_arg1:Number):void{
            layoutConstraint.x = _arg1;
        }
        private function initGraphicsTargetCollection():void{
            if (!_graphicsTarget){
                _graphicsTarget = new DisplayObjectCollection();
                if (enableEvents){
                    _graphicsTarget.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function set y(_arg1:Number):void{
            layoutConstraint.y = _arg1;
        }
        public function set autoClearGraphicsTarget(_arg1:Boolean):void{
            _autoClearGraphicsTarget = _arg1;
        }
        public function set maxY(_arg1:Number):void{
            layoutConstraint.maxY = _arg1;
        }
        public function addUpdateTarget(_arg1:UIComponent, _arg2:Object):void{
            if (!targetDictionary[_arg1]){
                targetDictionary[_arg1] = [];
                targetDictionary[_arg1].data = _arg2;
            } else {
                targetDictionary[_arg1].data = _arg2;
            };
        }
        public function get isInvalidated():Boolean{
            return (_invalidated);
        }
        public function requestTarget(_arg1:UIComponent):Object{
            return (targetDictionary[_arg1]);
        }
        public function set fill(_arg1:IGraphicsFill):void{
            var _local2:Object;
            if (_fill != _arg1){
                _local2 = _fill;
                if (_fill){
                    if (_fill.hasEventManager){
                        _fill.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                    };
                };
                _fill = _arg1;
                if (enableEvents){
                    _fill.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler, false, 0, true);
                };
                initChange("fill", _local2, _fill, this);
            };
        }
        public function set percentWidth(_arg1:Number):void{
            layoutConstraint.percentWidth = _arg1;
        }
        public function get inheritStroke():Boolean{
            return (_inheritStroke);
        }
        public function set invalidated(_arg1:Boolean):void{
            if (_invalidated != _arg1){
                _invalidated = _arg1;
                if (((_invalidated) && (_isRootGeometry))){
                    drawToTargets();
                };
            };
        }
        public function get inheritFill():Boolean{
            return (_inheritFill);
        }
        public function set state(_arg1:String):void{
            _state = _arg1;
        }
        public function drawToTargets():void{
            var _local1:Object;
            if (_graphicsTarget){
                for each (_local1 in _graphicsTarget.items) {
                    queDraw(_local1, _local1.graphics, null);
                };
            };
            _currentGraphicsTarget = null;
        }
        public function get height():Number{
            return ((hasLayout) ? layoutConstraint.height : NaN);
        }
        public function set minWidth(_arg1:Number):void{
            layoutConstraint.minWidth = _arg1;
        }
        public function set stateEvent(_arg1:String):void{
            _stateEvent = _arg1;
        }
        public function set decorators(_arg1:Array):void{
            initDecoratorsCollection();
            _decorators.items = _arg1;
            if (((_arg1) && (!((_arg1.length == 0))))){
                hasDecorators = true;
            } else {
                hasDecorators = false;
            };
        }
        public function set horizontalCenter(_arg1:Number):void{
            layoutConstraint.horizontalCenter = _arg1;
        }
        public function removeTarget(_arg1:UIComponent):void{
            delete targetDictionary[_arg1];
        }
        public function get decoratorCollection():DecoratorCollection{
            initDecoratorsCollection();
            return (_decorators);
        }
        public function set currentState(_arg1:String):void{
            stateManager.currentState = _arg1;
        }
        public function get transformBounds():Rectangle{
            return (commandStack.transformBounds);
        }
        public function draw(_arg1:Graphics, _arg2:Rectangle):void{
            if (!visible){
                return;
            };
            commandStack.draw(_arg1, _arg2);
            endDraw(_arg1);
        }
        public function set transformContext(_arg1:Matrix):void{
            _transformContext = _arg1;
        }
        public function set layoutConstraint(_arg1:LayoutConstraint):void{
            var _local2:Object;
            if (_layoutConstraint != _arg1){
                _local2 = _layoutConstraint;
                if (_layoutConstraint){
                    if (_layoutConstraint.hasEventManager){
                        _layoutConstraint.removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                    };
                };
                _layoutConstraint = _arg1;
                if (enableEvents){
                    _layoutConstraint.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler, false, 0, true);
                };
                initChange("layoutConstraint", _local2, _layoutConstraint, this);
                hasLayout = true;
            };
        }
        private function drawToTarget(_arg1:Object):void{
            if (_arg1){
                if (autoClearGraphicsTarget){
                    _arg1.graphics.clear();
                };
                _currentGraphicsTarget = (_arg1 as Sprite);
                draw(_arg1.graphics, null);
            };
        }
        public function get top():Number{
            return ((hasLayout) ? layoutConstraint.top : NaN);
        }
        public function set maxHeight(_arg1:Number):void{
            layoutConstraint.maxHeight = _arg1;
        }
        public function set data(_arg1:String):void{
            _data = _arg1;
        }
        public function get layoutRectangle():Rectangle{
            return ((_layoutRectangle) ? _layoutRectangle : bounds);
        }
        private function initFilterCollection():void{
            if (!_filters){
                _filters = new FilterCollection();
                _filters.parent = this;
                if (enableEvents){
                    _filters.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function initStroke(_arg1:Graphics, _arg2:Rectangle):void{
            if (parent){
                if (((((inheritStroke) && (!(_stroke)))) && ((parent is Geometry)))){
                    _stroke = Geometry(parent).stroke;
                };
            };
            if (_stroke){
                if ((_stroke is ITransformablePaint)){
                    (_stroke as ITransformablePaint).requester = this;
                };
                _stroke.apply(_arg1, (_arg2) ? _arg2 : null);
            } else {
                _arg1.lineStyle();
            };
        }
        public function get verticalCenter():Number{
            return ((hasLayout) ? layoutConstraint.verticalCenter : NaN);
        }
        public function get transform():ITransform{
            return (_transform);
        }
        public function get maxX():Number{
            return ((hasLayout) ? layoutConstraint.maxX : NaN);
        }
        public function get maxY():Number{
            return ((hasLayout) ? layoutConstraint.maxY : NaN);
        }
        public function set triggers(_arg1:Array):void{
            var _local2:ITrigger;
            _triggers = _arg1;
            if (_triggers){
                for each (_local2 in _triggers) {
                    _local2.triggerParent = this;
                };
            };
            if (((_arg1) && (!((_arg1.length == 0))))){
                hasTriggers = true;
            } else {
                hasTriggers = false;
            };
        }
        public function get autoClearGraphicsTarget():Boolean{
            return (_autoClearGraphicsTarget);
        }
        public function get fill():IGraphicsFill{
            return (_fill);
        }
        public function set maxWidth(_arg1:Number):void{
            layoutConstraint.maxWidth = _arg1;
        }
        private function initDecoratorsCollection():void{
            if (!_decorators){
                _decorators = new DecoratorCollection();
                if (enableEvents){
                    _decorators.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function set width(_arg1:Number):void{
            layoutConstraint.width = _arg1;
        }
        public function get decorators():Array{
            initDecoratorsCollection();
            return (_decorators.items);
        }
        public function get horizontalCenter():Number{
            return ((hasLayout) ? layoutConstraint.horizontalCenter : NaN);
        }
        public function set clippingRectangle(_arg1:Rectangle):void{
            var _local2:Rectangle;
            if (_clippingRectangle != _arg1){
                _local2 = _clippingRectangle;
                _clippingRectangle = _arg1;
                initChange("clippingRectangle", _local2, _clippingRectangle, this);
            };
        }
        public function set percentHeight(_arg1:Number):void{
            layoutConstraint.percentHeight = _arg1;
        }
        private function queDraw(... _args):void{
            var _local2:Object;
            for each (_local2 in methodQue) {
                if (_local2.args[0] == _args[0]){
                    return;
                };
            };
            methodQue.push({method:drawToTarget, args:_args});
            if (_stage){
                _stage.addEventListener(Event.ENTER_FRAME, processMethodQue);
            } else {
                if (graphicsTarget.length){
                    if (graphicsTarget[0].stage){
                        _stage = graphicsTarget[0].stage;
                        _stage.addEventListener(Event.ENTER_FRAME, processMethodQue);
                    };
                };
            };
        }
        public function get data():String{
            return (_data);
        }
        public function get maxHeight():Number{
            return ((hasLayout) ? layoutConstraint.maxHeight : NaN);
        }
        protected function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            if (!parent){
                dispatchEvent(_arg1);
                drawToTargets();
            } else {
                dispatchEvent(_arg1);
            };
        }
        private function initDrawQue():void{
            _stage.addEventListener(Event.ENTER_FRAME, processMethodQue);
        }
        private function initLayoutChangeWatcher(_arg1:UIComponent):void{
            var _local2:Rectangle = new Rectangle(_arg1.x, _arg1.y, _arg1.width, _arg1.height);
            var _local3:Array = [];
            _local3.push(ChangeWatcher.watch(_arg1, "width", onTargetChange, true));
            _local3.push(ChangeWatcher.watch(_arg1, "height", onTargetChange, true));
            _local3.push(ChangeWatcher.watch(_arg1, "x", onTargetChange, true));
            _local3.push(ChangeWatcher.watch(_arg1, "y", onTargetChange, true));
            if (!requestTarget(_arg1)){
                addUpdateTarget(_arg1, {oldbounds:_local2, watchers:_local3});
            };
        }
        public function get triggers():Array{
            return (_triggers);
        }
        public function get isRootGeometry():Boolean{
            return (_isRootGeometry);
        }
        public function get graphicsTargetCollection():DisplayObjectCollection{
            initGraphicsTargetCollection();
            return (_graphicsTarget);
        }
        public function styleChanged(_arg1:String):void{
        }
        public function get width():Number{
            return ((hasLayout) ? _layoutConstraint.width : NaN);
        }
        public function set styleName(_arg1:Object):void{
            _styleName = _arg1;
        }
        public function get percentHeight():Number{
            return ((hasLayout) ? layoutConstraint.percentHeight : NaN);
        }
        public function set mask(_arg1:IGeometryComposition):void{
            var _local2:IGeometryComposition;
            if (((!((_mask == _arg1))) && (!((_arg1 == this))))){
                if (_mask){
                    Geometry(_mask).removeEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
                _local2 = _mask;
                _mask = _arg1;
                Geometry(_mask).addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                initChange("mask", _local2, _mask, this);
            };
        }
        private function initGeometryCollection():void{
            if (!_geometry){
                _geometry = new GeometryCollection();
                _geometry.parent = this;
                if (enableEvents){
                    _geometry.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function pointAt(_arg1:Number):Point{
            return (commandStack.pathPointAt(_arg1));
        }
        public function get styleName():Object{
            return (_styleName);
        }
        public function clearGraphicsTargets():void{
            var _local1:Object;
            if (graphicsTarget){
                for each (_local1 in graphicsTarget) {
                    if (_local1){
                        _local1.graphics.clear();
                    };
                };
            };
        }
        public function set filters(_arg1:Array):void{
            var _local2:Array;
            initFilterCollection();
            if (_filters.items != _arg1){
                _local2 = _filters.items;
                _filters.items = _arg1;
                initChange("filters", _local2, _filters.items, this);
            };
            if (((_arg1) && (!((_arg1.length == 0))))){
                hasFilters = true;
            } else {
                hasFilters = false;
            };
        }
        public function set graphicsTarget(_arg1:Array):void{
            var _local2:Object;
            var _local3:DisplayObject;
            if (!_arg1){
                return;
            };
            for each (_local2 in _arg1) {
                if (!_local2){
                    return;
                };
            };
            initGraphicsTargetCollection();
            _graphicsTarget.items = _arg1;
            for each (_local3 in _graphicsTarget.items) {
                if (!(_local3 is IGraphicSkin)){
                    _local3.addEventListener(Event.RENDER, onTargetRender);
                    _local3.addEventListener(Event.ADDED_TO_STAGE, onTargetRender);
                    if ((_local3 is UIComponent)){
                        _local3.addEventListener(FlexEvent.UPDATE_COMPLETE, onTargetRender);
                    };
                };
            };
            _isRootGeometry = true;
        }
        public function set commandStack(_arg1:CommandStack):void{
            _commandStack = _arg1;
        }
        public function get filters():Array{
            initFilterCollection();
            return (_filters.items);
        }
        public function get graphicsTarget():Array{
            initGraphicsTargetCollection();
            return (_graphicsTarget.items);
        }
        public function get commandStack():CommandStack{
            if (!_commandStack){
                _commandStack = new CommandStack(this);
            };
            return (_commandStack);
        }

    }
}//package com.degrafa.geometry 
