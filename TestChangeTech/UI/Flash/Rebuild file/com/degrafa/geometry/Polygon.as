//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry {
    import flash.display.*;
    import flash.geom.*;
    import mx.events.*;
    import com.degrafa.*;
    import com.degrafa.core.collections.*;

    public class Polygon extends Geometry implements IGeometry {

        private var _x:Number
        private var _points:GraphicPointCollection
        private var _y:Number

        public function Polygon(_arg1:Array=null){
            if (_arg1){
                this.points = _arg1;
            };
        }
        override public function set y(_arg1:Number):void{
            var _local2:Object = this.y;
            if (_local2 !== _arg1){
                this._121y = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "y", _local2, _arg1));
            };
        }
        public function set points(_arg1:Array):void{
            var _local2:Object = this.points;
            if (_local2 !== _arg1){
                this._982754077points = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "points", _local2, _arg1));
            };
        }
        override public function preDraw():void{
            var _local1:int;
            var _local2:int;
            if (invalidated){
                if (((((!(_points)) || (!(_points.items)))) || ((_points.items.length < 1)))){
                    return;
                };
                commandStack.length = 0;
                commandStack.addMoveTo((_points.items[0].x + x), (_points.items[0].y + y));
                _local1 = 0;
                _local2 = _points.items.length;
                while (_local1 < _local2) {
                    commandStack.addLineTo((_points.items[_local1].x + x), (_points.items[_local1].y + y));
                    _local1++;
                };
                if (((!(((_points.items[(_points.items.length - 1)].x + x) == (_points.items[0].x + x)))) || (!(((_points.items[(_points.items.length - 1)].y + y) == (_points.items[0].y + y)))))){
                    commandStack.addLineTo((_points.items[0].x + x), (_points.items[0].y + y));
                };
                invalidated = false;
            };
        }
        public function get pointCollection():GraphicPointCollection{
            initPointsCollection();
            return (_points);
        }
        public function set _120x(_arg1:Number):void{
            if (_x != _arg1){
                _x = _arg1;
                invalidated = true;
            };
        }
        private function initPointsCollection():void{
            if (!_points){
                _points = new GraphicPointCollection();
                if (enableEvents){
                    _points.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        override public function draw(_arg1:Graphics, _arg2:Rectangle):void{
            if (invalidated){
                preDraw();
            };
            if (_layoutConstraint){
                calculateLayout();
            };
            super.draw(_arg1, (_arg2) ? _arg2 : bounds);
        }
        override public function set data(_arg1:String):void{
            var _local2:Object = this.data;
            if (_local2 !== _arg1){
                this._3076010data = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "data", _local2, _arg1));
            };
        }
        public function set derive(_arg1:Polygon):void{
            if (!fill){
                fill = _arg1.fill;
            };
            if (!stroke){
                stroke = _arg1.stroke;
            };
            if (!_x){
                _x = _arg1.x;
            };
            if (!_y){
                _y = _arg1.y;
            };
            if (((!(_points)) && (!((_arg1.points.length == 0))))){
                points = _arg1.points;
            };
        }
        public function set _121y(_arg1:Number):void{
            if (_y != _arg1){
                _y = _arg1;
                invalidated = true;
            };
        }
        private function set _982754077points(_arg1:Array):void{
            initPointsCollection();
            _points.items = _arg1;
            invalidated = true;
        }
        public function get points():Array{
            initPointsCollection();
            return (_points.items);
        }
        override protected function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            invalidated = true;
            super.propertyChangeHandler(_arg1);
        }
        override public function calculateLayout(_arg1:Rectangle=null):void{
            if (_layoutConstraint){
                if (_layoutConstraint.invalidated){
                    super.calculateLayout(new Rectangle((_x) ? _x : 0, (_y) ? _y : 0, 1, 1));
                    _layoutRectangle = _layoutConstraint.layoutRectangle;
                };
            };
        }
        override public function set x(_arg1:Number):void{
            var _local2:Object = this.x;
            if (_local2 !== _arg1){
                this._120x = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "x", _local2, _arg1));
            };
        }
        override public function get x():Number{
            if (!_x){
                return (0);
            };
            return (_x);
        }
        override public function get y():Number{
            if (!_y){
                return (0);
            };
            return (_y);
        }
        public function set _3076010data(_arg1:String):void{
            var _local2:Array;
            var _local3:Array;
            var _local4:Array;
            var _local5:int;
            var _local6:int;
            if (super.data != _arg1){
                super.data = _arg1;
                _local2 = _arg1.split(" ");
                _local3 = [];
                _local5 = 0;
                _local6 = _local2.length;
                while (_local5 < _local6) {
                    _local4 = String(_local2[_local5]).split(",");
                    if (_local4.length == 2){
                        _local3.push(new GraphicPoint(_local4[0], _local4[1]));
                    };
                    _local5++;
                };
                points = _local3;
                invalidated = true;
            };
        }

    }
}//package com.degrafa.geometry 
