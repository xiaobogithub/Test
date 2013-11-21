//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.utils {
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;

    public class Mouse3D extends DisplayObject3D {

        private var target:Number3D
        private var position:Number3D

        public static var enabled:Boolean = false;
        private static var UP:Number3D = new Number3D(0, 1, 0);

        public function Mouse3D():void{
            position = new Number3D(0, 0, 0);
            target = new Number3D();
            super();
        }
        public function updatePosition(_arg1:RenderHitData):void{
            var _local5:Number3D;
            var _local6:Number3D;
            var _local7:Matrix3D;
            var _local2:Triangle3D = (_arg1.renderable as Triangle3D);
            target.x = _local2.faceNormal.x;
            target.y = _local2.faceNormal.y;
            target.z = _local2.faceNormal.z;
            var _local3:Number3D = Number3D.sub(target, position);
            _local3.normalize();
            if (_local3.modulo > 0.1){
                _local5 = Number3D.cross(_local3, UP);
                _local5.normalize();
                _local6 = Number3D.cross(_local3, _local5);
                _local6.normalize();
                _local7 = this.transform;
                _local7.n11 = _local5.x;
                _local7.n21 = _local5.y;
                _local7.n31 = _local5.z;
                _local7.n12 = -(_local6.x);
                _local7.n22 = -(_local6.y);
                _local7.n32 = -(_local6.z);
                _local7.n13 = _local3.x;
                _local7.n23 = _local3.y;
                _local7.n33 = _local3.z;
            };
            var _local4:Matrix3D = Matrix3D.IDENTITY;
            this.transform = Matrix3D.multiply(_local2.instance.world, _local7);
            x = _arg1.x;
            y = _arg1.y;
            z = _arg1.z;
        }

    }
}//package org.papervision3d.core.utils 
