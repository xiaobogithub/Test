//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.geom.renderables.*;

    public class DefaultTriangleCuller implements ITriangleCuller {

        protected static var y2:Number;
        protected static var y1:Number;
        protected static var y0:Number;
        protected static var x0:Number;
        protected static var x1:Number;
        protected static var x2:Number;

        public function testFace(_arg1:Triangle3D, _arg2:Vertex3DInstance, _arg3:Vertex3DInstance, _arg4:Vertex3DInstance):Boolean{
            var _local5:MaterialObject3D;
            if (((((_arg2.visible) && (_arg3.visible))) && (_arg4.visible))){
                _local5 = (_arg1.material) ? _arg1.material : _arg1.instance.material;
                if (_local5.invisible){
                    return (false);
                };
                x0 = _arg2.x;
                y0 = _arg2.y;
                x1 = _arg3.x;
                y1 = _arg3.y;
                x2 = _arg4.x;
                y2 = _arg4.y;
                if (_local5.oneSide){
                    if (_local5.opposite){
                        if ((((x2 - x0) * (y1 - y0)) - ((y2 - y0) * (x1 - x0))) > 0){
                            return (false);
                        };
                    } else {
                        if ((((x2 - x0) * (y1 - y0)) - ((y2 - y0) * (x1 - x0))) < 0){
                            return (false);
                        };
                    };
                };
                return (true);
            };
            return (false);
        }

    }
}//package org.papervision3d.core.culling 
