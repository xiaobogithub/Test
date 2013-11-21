//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.geom.renderables {
    import org.papervision3d.core.math.*;

    public class Vertex3DInstance {

        public var y:Number
        public var normal:Number3D
        public var visible:Boolean
        public var extra:Object
        public var x:Number
        public var z:Number

        public function Vertex3DInstance(_arg1:Number=0, _arg2:Number=0, _arg3:Number=0){
            this.x = _arg1;
            this.y = _arg2;
            this.z = _arg3;
            this.visible = false;
            this.normal = new Number3D();
        }
        public function clone():Vertex3DInstance{
            var _local1:Vertex3DInstance = new Vertex3DInstance(x, y, z);
            _local1.visible = visible;
            _local1.extra = extra;
            return (_local1);
        }

        public static function cross(_arg1:Vertex3DInstance, _arg2:Vertex3DInstance):Number{
            return (((_arg1.x * _arg2.y) - (_arg2.x * _arg1.y)));
        }
        public static function dot(_arg1:Vertex3DInstance, _arg2:Vertex3DInstance):Number{
            return (((_arg1.x * _arg2.x) + (_arg1.y * _arg2.y)));
        }
        public static function subTo(_arg1:Vertex3DInstance, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance):void{
            _arg3.x = (_arg2.x - _arg1.x);
            _arg3.y = (_arg2.y - _arg1.y);
        }
        public static function sub(_arg1:Vertex3DInstance, _arg2:Vertex3DInstance):Vertex3DInstance{
            return (new Vertex3DInstance((_arg2.x - _arg1.x), (_arg2.y - _arg1.y)));
        }

    }
}//package org.papervision3d.core.geom.renderables 
