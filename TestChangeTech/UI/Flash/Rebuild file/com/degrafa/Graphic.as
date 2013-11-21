//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import flash.geom.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.utils.*;
    import com.degrafa.core.*;
    import com.degrafa.core.collections.*;

    public class Graphic extends Sprite implements IMXMLObject {

        private var _suppressEventProcessing:Boolean = false
        private var _target:DisplayObjectContainer
        private var _id:String
        private var _width:Number = 0
        private var _height:Number = 0
        private var _fills:FillCollection
        private var _percentHeight:Number
        private var _stroke:IGraphicsStroke
        private var _enableEvents:Boolean = true
        private var _fill:IGraphicsFill
        private var _percentWidth:Number
        private var _document:Object
        private var _strokes:StrokeCollection

        public function get percentWidth():Number{
            return (_percentWidth);
        }
        public function set percentWidth(_arg1:Number):void{
            if (_percentWidth == _arg1){
                return;
            };
            _percentWidth = _arg1;
        }
        public function get fill():IGraphicsFill{
            return (_fill);
        }
        public function set fill(_arg1:IGraphicsFill):void{
            _fill = _arg1;
        }
        public function set enableEvents(_arg1:Boolean):void{
            _enableEvents = _arg1;
        }
        public function get fillCollection():FillCollection{
            initFillsCollection();
            return (_fills);
        }
        public function get enableEvents():Boolean{
            return (_enableEvents);
        }
        public function get measuredHeight():Number{
            return (_height);
        }
        public function get target():DisplayObjectContainer{
            return (_target);
        }
        override public function set width(_arg1:Number):void{
            _width = _arg1;
            draw(null, null);
            dispatchEvent(new Event("change"));
        }
        private function initSrokesCollection():void{
            if (!_strokes){
                _strokes = new StrokeCollection();
                if (enableEvents){
                    _strokes.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function set percentHeight(_arg1:Number):void{
            if (_percentHeight == _arg1){
                return;
            };
            _percentHeight = _arg1;
        }
        public function get id():String{
            if (_id){
                return (_id);
            };
            _id = NameUtil.createUniqueName(this);
            name = _id;
            return (_id);
        }
        public function set fills(_arg1:Array):void{
            initFillsCollection();
            _fills.items = _arg1;
        }
        public function dispatchPropertyChange(_arg1:Boolean=false, _arg2:Object=null, _arg3:Object=null, _arg4:Object=null, _arg5:Object=null):Boolean{
            return (dispatchEvent(new PropertyChangeEvent("propertyChange", _arg1, false, PropertyChangeEventKind.UPDATE, _arg2, _arg3, _arg4, _arg5)));
        }
        public function set target(_arg1:DisplayObjectContainer):void{
            if (!_arg1){
                return;
            };
            if (((!((_target == _arg1))) && (!((_target == null))))){
                _target.removeChild(this);
            };
            _target = _arg1;
            _target.addChild(this);
            draw(null, null);
            endDraw(null);
        }
        private function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            draw(null, null);
        }
        public function get measuredWidth():Number{
            return (_width);
        }
        public function get document():Object{
            return (_document);
        }
        override public function get height():Number{
            return (_height);
        }
        public function set suppressEventProcessing(_arg1:Boolean):void{
            if ((((_suppressEventProcessing == true)) && ((_arg1 == false)))){
                _suppressEventProcessing = _arg1;
                initChange("suppressEventProcessing", false, true, this);
            } else {
                _suppressEventProcessing = _arg1;
            };
        }
        public function get stroke():IGraphicsStroke{
            return (_stroke);
        }
        public function get strokes():Array{
            initSrokesCollection();
            return (_strokes.items);
        }
        public function set id(_arg1:String):void{
            _id = _arg1;
            name = _id;
        }
        public function endDraw(_arg1:Graphics):void{
            if (fill){
                fill.end(this.graphics);
            };
        }
        override public function get width():Number{
            return (_width);
        }
        public function draw(_arg1:Graphics, _arg2:Rectangle):void{
            var _local3:Rectangle;
            if (!parent){
                return;
            };
            if (((percentWidth) || (percentHeight))){
                _width = ((parent.width / 100) * _percentHeight);
                _height = ((parent.height / 100) * _percentHeight);
            };
            this.graphics.clear();
            if (stroke){
                if (!_arg2){
                    stroke.apply(this.graphics, null);
                } else {
                    stroke.apply(this.graphics, _arg2);
                };
            } else {
                this.graphics.lineStyle(0, 0xFFFFFF, 0);
            };
            if (fill){
                if (!_arg2){
                    _local3 = new Rectangle(0, 0, width, height);
                    fill.begin(this.graphics, _local3);
                } else {
                    fill.begin(this.graphics, _arg2);
                };
            };
        }
        public function get percentHeight():Number{
            return (_percentHeight);
        }
        override public function set height(_arg1:Number):void{
            _height = _arg1;
            draw(null, null);
            dispatchEvent(new Event("change"));
        }
        override public function dispatchEvent(_arg1:Event):Boolean{
            if (_suppressEventProcessing){
                return (false);
            };
            return (super.dispatchEvent(_arg1));
        }
        public function get fills():Array{
            initFillsCollection();
            return (_fills.items);
        }
        public function get suppressEventProcessing():Boolean{
            return (_suppressEventProcessing);
        }
        private function initFillsCollection():void{
            if (!_fills){
                _fills = new FillCollection();
                if (enableEvents){
                    _fills.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function initialized(_arg1:Object, _arg2:String):void{
            if (!_id){
                if (_arg2){
                    _id = _arg2;
                } else {
                    _id = NameUtil.createUniqueName(this);
                };
            };
            name = _id;
            _document = _arg1;
            if (((enableEvents) && (!(suppressEventProcessing)))){
                dispatchEvent(new FlexEvent(FlexEvent.INITIALIZE));
            };
        }
        public function initChange(_arg1:String, _arg2:Object, _arg3:Object, _arg4:Object):void{
            if (hasEventManager){
                dispatchPropertyChange(false, _arg1, _arg2, _arg3, _arg4);
            };
        }
        public function set stroke(_arg1:IGraphicsStroke):void{
            _stroke = _arg1;
        }
        override public function set y(_arg1:Number):void{
            super.y = _arg1;
        }
        public function get hasEventManager():Boolean{
            return (true);
        }
        public function set strokes(_arg1:Array):void{
            initSrokesCollection();
            _strokes.items = _arg1;
        }
        override public function set x(_arg1:Number):void{
            super.x = _arg1;
        }
        override public function get x():Number{
            return (super.x);
        }
        override public function get y():Number{
            return (super.y);
        }
        public function get strokeCollection():StrokeCollection{
            initSrokesCollection();
            return (_strokes);
        }

    }
}//package com.degrafa 
