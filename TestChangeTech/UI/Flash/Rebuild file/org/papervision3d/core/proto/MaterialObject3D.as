//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import flash.display.*;
    import flash.geom.*;
    import flash.events.*;
    import flash.utils.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.render.material.*;
    import org.papervision3d.core.render.draw.*;

    public class MaterialObject3D extends EventDispatcher implements ITriangleDrawer {

        public var interactive:Boolean = false
        public var lineAlpha:Number = 0
        public var name:String
        public var lineColor:Number
        public var id:Number
        public var oneSide:Boolean = true
        public var lineThickness:Number = 1
        public var heightOffset:Number = 0
        public var fillAlpha:Number = 0
        protected var objects:Dictionary
        public var fillColor:Number
        public var invisible:Boolean = false
        public var smooth:Boolean = false
        public var bitmap:BitmapData
        public var maxU:Number
        public var maxV:Number
        public var tiled:Boolean = false
        public var widthOffset:Number = 0
        public var opposite:Boolean = false

        public static var DEFAULT_COLOR:int = 0;
        public static var DEBUG_COLOR:int = 0xFF00FF;
        private static var _totalMaterialObjects:Number = 0;

        public function MaterialObject3D(){
            lineColor = DEFAULT_COLOR;
            fillColor = DEFAULT_COLOR;
            super();
            this.id = _totalMaterialObjects++;
            MaterialManager.registerMaterial(this);
            objects = new Dictionary(true);
        }
        public function registerObject(_arg1:DisplayObject3D):void{
            objects[_arg1] = _arg1;
        }
        public function destroy():void{
            objects = null;
            bitmap = null;
            MaterialManager.unRegisterMaterial(this);
        }
        public function clone():MaterialObject3D{
            var _local1:MaterialObject3D = new MaterialObject3D();
            _local1.copy(this);
            return (_local1);
        }
        public function updateBitmap():void{
        }
        override public function toString():String{
            return (((((("[MaterialObject3D] bitmap:" + this.bitmap) + " lineColor:") + this.lineColor) + " fillColor:") + fillColor));
        }
        public function unregisterObject(_arg1:DisplayObject3D):void{
            if (((objects) && (!((objects[_arg1] == null))))){
                delete objects[_arg1];
            };
        }
        public function get doubleSided():Boolean{
            return (!(this.oneSide));
        }
        public function copy(_arg1:MaterialObject3D):void{
            this.bitmap = _arg1.bitmap;
            this.smooth = _arg1.smooth;
            this.lineColor = _arg1.lineColor;
            this.lineAlpha = _arg1.lineAlpha;
            this.fillColor = _arg1.fillColor;
            this.fillAlpha = _arg1.fillAlpha;
            this.oneSide = _arg1.oneSide;
            this.opposite = _arg1.opposite;
            this.invisible = _arg1.invisible;
            this.name = _arg1.name;
            this.maxU = _arg1.maxU;
            this.maxV = _arg1.maxV;
        }
        public function set doubleSided(_arg1:Boolean):void{
            this.oneSide = !(_arg1);
        }
        public function drawTriangle(_arg1:Triangle3D, _arg2:Graphics, _arg3:RenderSessionData, _arg4:BitmapData=null, _arg5:Matrix=null):void{
        }

        public static function get DEFAULT():MaterialObject3D{
            var _local1:MaterialObject3D = new WireframeMaterial();
            _local1.lineColor = (0xFFFFFF * Math.random());
            _local1.lineAlpha = 1;
            _local1.fillColor = DEFAULT_COLOR;
            _local1.fillAlpha = 1;
            _local1.doubleSided = false;
            return (_local1);
        }
        public static function get DEBUG():MaterialObject3D{
            var _local1:MaterialObject3D = new (MaterialObject3D);
            _local1.lineColor = (0xFFFFFF * Math.random());
            _local1.lineAlpha = 1;
            _local1.fillColor = DEBUG_COLOR;
            _local1.fillAlpha = 0.37;
            _local1.doubleSided = true;
            return (_local1);
        }

    }
}//package org.papervision3d.core.proto 
