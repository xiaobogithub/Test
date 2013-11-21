//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.layout {
    import flash.display.*;
    import flash.geom.*;
    import mx.events.*;
    import com.degrafa.core.*;

    public class LayoutConstraint extends DegrafaObject implements ILayout {

        private var _right:Number
        private var _maxWidth:Number
        private var _minX:Number
        private var _minY:Number
        private var _maxHeight:Number
        private var _layoutRectangle:Rectangle
        private var _maintainAspectRatio:Boolean = false
        private var _height:Number
        private var _maxY:Number
        private var _bottom:Number
        private var _maxX:Number
        private var _percentWidth:Number
        private var container:Rectangle
        private var _verticalCenter:Number
        private var _top:Number
        private var _minHeight:Number
        private var _targetCoordinateSpace:DisplayObject
        private var _width:Number
        private var _invalidated:Boolean
        private var _percentHeight:Number
        private var _left:Number
        private var _minWidth:Number
        private var _horizontalCenter:Number
        private var _x:Number
        private var _y:Number

        public function LayoutConstraint(){
            _layoutRectangle = new Rectangle();
            super();
        }
        public function set y(_arg1:Number):void{
            var _local2:Object = this.y;
            if (_local2 !== _arg1){
                this._121y = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "y", _local2, _arg1));
            };
        }
        private function set _3351623minY(_arg1:Number):void{
            if (_minY != _arg1){
                _minY = _arg1;
                invalidated = true;
            };
        }
        public function get left():Number{
            if (!_left){
                return (NaN);
            };
            return (_left);
        }
        public function get maintainAspectRatio():Boolean{
            return (_maintainAspectRatio);
        }
        private function set _3351622minX(_arg1:Number):void{
            if (_minX != _arg1){
                _minX = _arg1;
                invalidated = true;
            };
        }
        private function set _3344244maxX(_arg1:Number):void{
            if (_maxX != _arg1){
                _maxX = _arg1;
                invalidated = true;
            };
        }
        private function set _120x(_arg1:Number):void{
            if (_x != _arg1){
                _x = _arg1;
                invalidated = true;
            };
        }
        public function set percentWidth(_arg1:Number):void{
            var _local2:Object = this.percentWidth;
            if (_local2 !== _arg1){
                this._1127236479percentWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "percentWidth", _local2, _arg1));
            };
        }
        public function set invalidated(_arg1:Boolean):void{
            _invalidated = _arg1;
        }
        public function set left(_arg1:Number):void{
            var _local2:Object = this.left;
            if (_local2 !== _arg1){
                this._3317767left = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "left", _local2, _arg1));
            };
        }
        public function set maintainAspectRatio(_arg1:Boolean):void{
            var _local2:Object = this.maintainAspectRatio;
            if (_local2 !== _arg1){
                this._574021640maintainAspectRatio = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "maintainAspectRatio", _local2, _arg1));
            };
        }
        private function set _3344245maxY(_arg1:Number):void{
            if (_maxY != _arg1){
                _maxY = _arg1;
                invalidated = true;
            };
        }
        public function get right():Number{
            if (!_right){
                return (NaN);
            };
            return (_right);
        }
        private function calculateLayoutRectangle():void{
            var _local1:Number;
            var _local12:Number;
            var _local13:Number;
            var _local2:Boolean = isNaN(_left);
            var _local3:Boolean = isNaN(_right);
            var _local4:Boolean = isNaN(_horizontalCenter);
            var _local5 = !(Boolean(_local2));
            var _local6 = !(Boolean(_local3));
            if (container){
                if (((!(_local5)) && (!(_local6)))){
                    if (_local4){
                        _layoutRectangle.width = (isNaN(_percentWidth)) ? _width : ((_percentWidth)>1) ? ((_percentWidth / 100) * container.width) : (_percentWidth * container.width);
                        _layoutRectangle.x = (isNaN(_x)) ? 0 : (_x + container.left);
                    } else {
                        _layoutRectangle.width = (isNaN(_percentWidth)) ? _width : ((_percentWidth)>1) ? ((_percentWidth / 100) * container.width) : (_percentWidth * container.width);
                        _layoutRectangle.x = (((_horizontalCenter - (_layoutRectangle.width / 2)) + container.left) + (container.width / 2));
                    };
                } else {
                    if (!_local6){
                        _layoutRectangle.width = (isNaN(_percentWidth)) ? _width : ((_percentWidth)>1) ? ((_percentWidth / 100) * container.width) : (_percentWidth * container.width);
                        _layoutRectangle.x = (container.left + _left);
                    } else {
                        if (!_local5){
                            _layoutRectangle.width = (isNaN(_percentWidth)) ? _width : ((_percentWidth)>1) ? ((_percentWidth / 100) * container.width) : (_percentWidth * container.width);
                            _layoutRectangle.x = ((container.right - _right) - _layoutRectangle.width);
                        } else {
                            _layoutRectangle.right = (container.right - _right);
                            _layoutRectangle.left = (container.left + _left);
                        };
                    };
                };
            };
            if (!isNaN(_minX)){
                _local1 = (container.x + _minX);
                if (_local1 > _layoutRectangle.x){
                    _layoutRectangle.x = _local1;
                };
            };
            if (!isNaN(_maxX)){
                _local1 = (container.x + _maxX);
                if (_local1 < _layoutRectangle.x){
                    _layoutRectangle.x = _local1;
                };
            };
            _local1 = 0;
            if (((!(isNaN(_minWidth))) && ((_minWidth > _layoutRectangle.width)))){
                _local1 = (_layoutRectangle.width - _minWidth);
            } else {
                if (((!(isNaN(_maxWidth))) && ((_maxWidth < _layoutRectangle.width)))){
                    _local1 = (_layoutRectangle.width - _maxWidth);
                };
            };
            if (_local1){
                if (!_local5){
                    if (_local6){
                        _layoutRectangle.x = (_layoutRectangle.x + _local1);
                    } else {
                        if (!_local4){
                            _layoutRectangle.x = (_layoutRectangle.x + (_local1 / 2));
                        };
                    };
                } else {
                    if (((_local5) && (_local6))){
                        _layoutRectangle.x = (_layoutRectangle.x + (_local1 / 2));
                    };
                };
                _layoutRectangle.width = (_layoutRectangle.width - _local1);
            };
            var _local7:Boolean = isNaN(_top);
            var _local8:Boolean = isNaN(_bottom);
            var _local9:Boolean = isNaN(_verticalCenter);
            var _local10 = !(Boolean(_local7));
            var _local11 = !(Boolean(_local8));
            if (container){
                if (((!(_local10)) && (!(_local11)))){
                    if (_local9){
                        _layoutRectangle.height = (isNaN(_percentHeight)) ? _height : ((_percentHeight)>1) ? ((_percentHeight / 100) * container.height) : (_percentHeight * container.height);
                        _layoutRectangle.y = (isNaN(_y)) ? 0 : (_y + container.top);
                    } else {
                        _layoutRectangle.height = (isNaN(_percentHeight)) ? _height : ((_percentHeight)>1) ? ((_percentHeight / 100) * container.height) : (_percentHeight * container.height);
                        _layoutRectangle.y = (((_verticalCenter - (_layoutRectangle.height / 2)) + container.top) + (container.height / 2));
                    };
                } else {
                    if (!_local11){
                        _layoutRectangle.height = (isNaN(_percentHeight)) ? _height : ((_percentHeight)>1) ? ((_percentHeight / 100) * container.height) : (_percentHeight * container.height);
                        _layoutRectangle.y = (container.top + _top);
                    } else {
                        if (!_local10){
                            _layoutRectangle.height = (isNaN(_percentHeight)) ? _height : ((_percentHeight)>1) ? ((_percentHeight / 100) * container.height) : (_percentHeight * container.height);
                            _layoutRectangle.y = ((container.bottom - _bottom) - _layoutRectangle.height);
                        } else {
                            _layoutRectangle.bottom = (container.bottom - _bottom);
                            _layoutRectangle.top = (container.top + _top);
                        };
                    };
                };
            };
            if (!isNaN(_minY)){
                _local1 = (container.y + _minY);
                if (_local1 > _layoutRectangle.y){
                    _layoutRectangle.y = _local1;
                };
            };
            if (!isNaN(_maxY)){
                _local1 = (container.y + _maxY);
                if (_local1 < _layoutRectangle.y){
                    _layoutRectangle.y = _local1;
                };
            };
            _local1 = 0;
            if (((!(isNaN(_minHeight))) && ((_minHeight > _layoutRectangle.height)))){
                _local1 = (_layoutRectangle.height - _minHeight);
            } else {
                if (((!(isNaN(_maxHeight))) && ((_maxHeight < _layoutRectangle.height)))){
                    _local1 = (_layoutRectangle.height - _maxHeight);
                };
            };
            if (_local1){
                if (!_local10){
                    if (_local11){
                        _layoutRectangle.y = (_layoutRectangle.y + _local1);
                    } else {
                        if (!_local9){
                            _layoutRectangle.y = (_layoutRectangle.y + (_local1 / 2));
                        };
                    };
                } else {
                    if (((_local10) && (_local11))){
                        _layoutRectangle.y = (_layoutRectangle.y + (_local1 / 2));
                    };
                };
                _layoutRectangle.height = (_layoutRectangle.height - _local1);
            };
            if (((((_maintainAspectRatio) && (_height))) && (_width))){
                _local12 = (_height / _width);
                _local13 = (_layoutRectangle.height / _layoutRectangle.width);
                if (_local12 > _local13){
                    _local1 = (_layoutRectangle.height / _local12);
                    if (!_local5){
                        if (_local6){
                            _layoutRectangle.x = (_layoutRectangle.x + (_layoutRectangle.width - _local1));
                        } else {
                            if (!_local4){
                                _layoutRectangle.x = (_layoutRectangle.x + ((_layoutRectangle.width - _local1) / 2));
                            };
                        };
                    } else {
                        if (((_local5) && (_local6))){
                            _layoutRectangle.x = (_layoutRectangle.x + ((_layoutRectangle.width - _local1) / 2));
                        };
                    };
                    _layoutRectangle.width = _local1;
                } else {
                    if (_local12 < _local13){
                        _local1 = (_layoutRectangle.width * _local12);
                        if (!_local10){
                            if (_local11){
                                _layoutRectangle.y = (_layoutRectangle.y + (_layoutRectangle.height - _local1));
                            } else {
                                if (!_local9){
                                    _layoutRectangle.y = (_layoutRectangle.y + ((_layoutRectangle.height - _local1) / 2));
                                };
                            };
                        } else {
                            if (((_local10) && (_local11))){
                                _layoutRectangle.y = (_layoutRectangle.y + ((_layoutRectangle.height - _local1) / 2));
                            };
                        };
                        _layoutRectangle.height = _local1;
                    };
                };
            };
        }
        public function get minHeight():Number{
            return (_minHeight);
        }
        private function set _926273685verticalCenter(_arg1:Number):void{
            if (_verticalCenter != _arg1){
                _verticalCenter = _arg1;
                invalidated = true;
            };
        }
        private function set _574021640maintainAspectRatio(_arg1:Boolean):void{
            if (_maintainAspectRatio != _arg1){
                _maintainAspectRatio = _arg1;
                invalidated = true;
            };
        }
        public function get height():Number{
            if (!_height){
                return (0);
            };
            return (_height);
        }
        public function set minWidth(_arg1:Number):void{
            var _local2:Object = this.minWidth;
            if (_local2 !== _arg1){
                this._1375815020minWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minWidth", _local2, _arg1));
            };
        }
        public function get bottom():Number{
            if (!_bottom){
                return (NaN);
            };
            return (_bottom);
        }
        public function computeLayoutRectangle(_arg1:Rectangle, _arg2:Rectangle):Rectangle{
            if (_arg2.isEmpty()){
                return (null);
            };
            _layoutRectangle = _arg1.clone();
            if (!_width){
                _width = (layoutRectangle.width) ? layoutRectangle.width : 1;
            };
            if (!_height){
                _height = (layoutRectangle.height) ? layoutRectangle.height : 1;
            };
            if (isNaN(_x)){
                _x = (layoutRectangle.x) ? layoutRectangle.x : 0;
            };
            if (isNaN(_y)){
                _y = (layoutRectangle.y) ? layoutRectangle.y : 0;
            };
            container = _arg2.clone();
            calculateLayoutRectangle();
            return (layoutRectangle);
        }
        public function set horizontalCenter(_arg1:Number):void{
            var _local2:Object = this.horizontalCenter;
            if (_local2 !== _arg1){
                this._2016110183horizontalCenter = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "horizontalCenter", _local2, _arg1));
            };
        }
        public function set right(_arg1:Number):void{
            var _local2:Object = this.right;
            if (_local2 !== _arg1){
                this._108511772right = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "right", _local2, _arg1));
            };
        }
        public function set minHeight(_arg1:Number):void{
            var _local2:Object = this.minHeight;
            if (_local2 !== _arg1){
                this._133587431minHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minHeight", _local2, _arg1));
            };
        }
        private function set _1854666595targetCoordinateSpace(_arg1:DisplayObject):void{
            if (_targetCoordinateSpace != _arg1){
                _targetCoordinateSpace = _arg1;
                invalidated = true;
            };
        }
        private function set _400381634maxWidth(_arg1:Number):void{
            if (_maxWidth != _arg1){
                _maxWidth = _arg1;
                invalidated = true;
            };
        }
        public function get minX():Number{
            return (_minX);
        }
        public function get minY():Number{
            return (_minY);
        }
        private function set _1383228885bottom(_arg1:Number):void{
            if (_bottom != _arg1){
                _bottom = _arg1;
                invalidated = true;
            };
        }
        public function set derive(_arg1:LayoutConstraint):void{
            if (!_x){
                _x = _arg1.x;
            };
            if (!_minX){
                _minX = _arg1.minX;
            };
            if (!_maxX){
                _maxX = _arg1.maxX;
            };
            if (!_y){
                _y = _arg1.y;
            };
            if (!_minY){
                _minY = _arg1.minY;
            };
            if (!_maxY){
                _maxY = _arg1.maxY;
            };
            if (!_width){
                _width = _arg1.width;
            };
            if (!_minWidth){
                _minWidth = _arg1.minWidth;
            };
            if (!_maxWidth){
                _maxWidth = _arg1.maxWidth;
            };
            if (!_percentWidth){
                _percentWidth = _arg1.percentWidth;
            };
            if (!_height){
                _height = _arg1.height;
            };
            if (!_minHeight){
                _minHeight = _arg1.minHeight;
            };
            if (!_maxHeight){
                _maxHeight = _arg1.maxHeight;
            };
            if (!_percentHeight){
                _percentHeight = _arg1.percentHeight;
            };
            if (!_top){
                _top = _arg1.top;
            };
            if (!_right){
                _right = _arg1.right;
            };
            if (!_bottom){
                _bottom = _arg1.bottom;
            };
            if (!_left){
                _left = _arg1.left;
            };
            if (!_horizontalCenter){
                _horizontalCenter = _arg1.horizontalCenter;
            };
            if (!_verticalCenter){
                _verticalCenter = _arg1.verticalCenter;
            };
            if (!_maintainAspectRatio){
                _maintainAspectRatio = _arg1.maintainAspectRatio;
            };
            if (!_targetCoordinateSpace){
                _targetCoordinateSpace = _arg1.targetCoordinateSpace;
            };
            invalidated = true;
        }
        private function set _121y(_arg1:Number):void{
            if (_y != _arg1){
                _y = _arg1;
                invalidated = true;
            };
        }
        private function set _108511772right(_arg1:Number):void{
            if (_right != _arg1){
                _right = _arg1;
                invalidated = true;
            };
        }
        public function set targetCoordinateSpace(_arg1:DisplayObject):void{
            var _local2:Object = this.targetCoordinateSpace;
            if (_local2 !== _arg1){
                this._1854666595targetCoordinateSpace = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "targetCoordinateSpace", _local2, _arg1));
            };
        }
        public function get top():Number{
            if (!_top){
                return (NaN);
            };
            return (_top);
        }
        public function set maxHeight(_arg1:Number):void{
            var _local2:Object = this.maxHeight;
            if (_local2 !== _arg1){
                this._906066005maxHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "maxHeight", _local2, _arg1));
            };
        }
        private function set _1127236479percentWidth(_arg1:Number):void{
            if (_percentWidth != _arg1){
                _percentWidth = _arg1;
                invalidated = true;
            };
        }
        public function set height(_arg1:Number):void{
            var _local2:Object = this.height;
            if (_local2 !== _arg1){
                this._1221029593height = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "height", _local2, _arg1));
            };
        }
        private function set _113126854width(_arg1:Number):void{
            if (_width != _arg1){
                _width = _arg1;
                _percentWidth = NaN;
                invalidated = true;
            };
        }
        public function get layoutRectangle():Rectangle{
            return (_layoutRectangle.clone());
        }
        public function get verticalCenter():Number{
            if (!_verticalCenter){
                return (NaN);
            };
            return (_verticalCenter);
        }
        private function set _2016110183horizontalCenter(_arg1:Number):void{
            if (_horizontalCenter != _arg1){
                _horizontalCenter = _arg1;
                invalidated = true;
            };
        }
        private function set _3317767left(_arg1:Number):void{
            if (_left != _arg1){
                _left = _arg1;
                invalidated = true;
            };
        }
        public function get maxY():Number{
            return (_maxY);
        }
        public function get x():Number{
            if (!_x){
                return (0);
            };
            return (_x);
        }
        public function get y():Number{
            if (!_y){
                return (0);
            };
            return (_y);
        }
        public function get maxX():Number{
            return (_maxX);
        }
        public function set bottom(_arg1:Number):void{
            var _local2:Object = this.bottom;
            if (_local2 !== _arg1){
                this._1383228885bottom = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "bottom", _local2, _arg1));
            };
        }
        public function set maxWidth(_arg1:Number):void{
            var _local2:Object = this.maxWidth;
            if (_local2 !== _arg1){
                this._400381634maxWidth = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "maxWidth", _local2, _arg1));
            };
        }
        public function get percentWidth():Number{
            if (!_percentWidth){
                return (NaN);
            };
            return (_percentWidth);
        }
        public function get invalidated():Boolean{
            return (_invalidated);
        }
        private function set _1375815020minWidth(_arg1:Number):void{
            if (_minWidth != _arg1){
                _minWidth = _arg1;
                invalidated = true;
            };
        }
        private function set _906066005maxHeight(_arg1:Number):void{
            if (_maxHeight != _arg1){
                _maxHeight = _arg1;
                invalidated = true;
            };
        }
        public function set minY(_arg1:Number):void{
            var _local2:Object = this.minY;
            if (_local2 !== _arg1){
                this._3351623minY = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minY", _local2, _arg1));
            };
        }
        public function set percentHeight(_arg1:Number):void{
            var _local2:Object = this.percentHeight;
            if (_local2 !== _arg1){
                this._1017587252percentHeight = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "percentHeight", _local2, _arg1));
            };
        }
        public function get horizontalCenter():Number{
            if (!_horizontalCenter){
                return (NaN);
            };
            return (_horizontalCenter);
        }
        public function get minWidth():Number{
            return (_minWidth);
        }
        public function get targetCoordinateSpace():DisplayObject{
            if (!_targetCoordinateSpace){
                return (null);
            };
            return (_targetCoordinateSpace);
        }
        public function set width(_arg1:Number):void{
            var _local2:Object = this.width;
            if (_local2 !== _arg1){
                this._113126854width = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "width", _local2, _arg1));
            };
        }
        public function set top(_arg1:Number):void{
            var _local2:Object = this.top;
            if (_local2 !== _arg1){
                this._115029top = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "top", _local2, _arg1));
            };
        }
        public function get maxHeight():Number{
            return (_maxHeight);
        }
        public function set minX(_arg1:Number):void{
            var _local2:Object = this.minX;
            if (_local2 !== _arg1){
                this._3351622minX = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "minX", _local2, _arg1));
            };
        }
        private function set _115029top(_arg1:Number):void{
            if (_top != _arg1){
                _top = _arg1;
                invalidated = true;
            };
        }
        private function set _1221029593height(_arg1:Number):void{
            if (_height != _arg1){
                _height = _arg1;
                _percentHeight = NaN;
                invalidated = true;
            };
        }
        public function get maxWidth():Number{
            return (_maxWidth);
        }
        public function get width():Number{
            if (!_width){
                return (0);
            };
            return (_width);
        }
        public function get percentHeight():Number{
            if (!_percentHeight){
                return (NaN);
            };
            return (_percentHeight);
        }
        private function set _133587431minHeight(_arg1:Number):void{
            if (_minHeight != _arg1){
                _minHeight = _arg1;
                invalidated = true;
            };
        }
        public function set verticalCenter(_arg1:Number):void{
            var _local2:Object = this.verticalCenter;
            if (_local2 !== _arg1){
                this._926273685verticalCenter = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "verticalCenter", _local2, _arg1));
            };
        }
        private function set _1017587252percentHeight(_arg1:Number):void{
            if (_percentHeight != _arg1){
                _percentHeight = _arg1;
                invalidated = true;
            };
        }
        public function set maxX(_arg1:Number):void{
            var _local2:Object = this.maxX;
            if (_local2 !== _arg1){
                this._3344244maxX = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "maxX", _local2, _arg1));
            };
        }
        public function set maxY(_arg1:Number):void{
            var _local2:Object = this.maxY;
            if (_local2 !== _arg1){
                this._3344245maxY = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "maxY", _local2, _arg1));
            };
        }
        public function set x(_arg1:Number):void{
            var _local2:Object = this.x;
            if (_local2 !== _arg1){
                this._120x = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "x", _local2, _arg1));
            };
        }

    }
}//package com.degrafa.geometry.layout 
