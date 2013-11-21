//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import flash.geom.*;
    import mx.events.*;
    import com.degrafa.core.collections.*;

    public class GeometryGroup extends Graphic implements IGraphic, IGeometry {

        private var _geometry:GeometryCollection

        private function initGeometryCollection():void{
            if (!_geometry){
                _geometry = new GeometryCollection();
                if (enableEvents){
                    _geometry.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function set data(_arg1:String):void{
        }
        public function get geometryCollection():GeometryCollection{
            initGeometryCollection();
            return (_geometry);
        }
        public function get geometry():Array{
            initGeometryCollection();
            return (_geometry.items);
        }
        public function get data():String{
            return ("");
        }
        public function set geometry(_arg1:Array):void{
            var _local2:IGeometry;
            initGeometryCollection();
            _geometry.items = _arg1;
            for each (_local2 in _geometry.items) {
                if ((_local2 is IGraphic)){
                    addChild(DisplayObject(_local2));
                };
            };
        }
        private function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            draw(null, null);
        }
        override public function draw(_arg1:Graphics, _arg2:Rectangle):void{
            var _local3:IGeometry;
            if (!parent){
                return;
            };
            super.draw(null, null);
            if (_geometry){
                for each (_local3 in _geometry.items) {
                    if ((_local3 is IGraphic)){
                        _local3.draw(null, null);
                    } else {
                        _local3.draw(this.graphics, null);
                    };
                };
            };
            super.endDraw(null);
        }

    }
}//package com.degrafa 
