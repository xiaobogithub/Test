//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.culling {
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;

    public class FrustumCuller implements IObjectCuller {

        private var _tang:Number
        private var _near:Number
        private var _ratio:Number
        private var _fov:Number
        private var _far:Number
        private var _nh:Number
        private var _fh:Number
        private var _nw:Number
        public var transform:Matrix3D
        private var _sphereY:Number
        private var _sphereX:Number
        private var _fw:Number

        public static const OUTSIDE:int = -1;
        public static const INSIDE:int = 1;
        public static const INTERSECT:int = 0;

        public function FrustumCuller(){
            this.transform = Matrix3D.IDENTITY;
            this.initialize();
        }
        public function get ratio():Number{
            return (_ratio);
        }
        public function pointInFrustum(_arg1:Number, _arg2:Number, _arg3:Number):int{
            var _local4:Matrix3D = this.transform;
            var _local5:Number = (_arg1 - _local4.n14);
            var _local6:Number = (_arg2 - _local4.n24);
            var _local7:Number = (_arg3 - _local4.n34);
            var _local8:Number = (((_local5 * _local4.n13) + (_local6 * _local4.n23)) + (_local7 * _local4.n33));
            if ((((_local8 > _far)) || ((_local8 < _near)))){
                return (OUTSIDE);
            };
            var _local9:Number = (((_local5 * _local4.n12) + (_local6 * _local4.n22)) + (_local7 * _local4.n32));
            var _local10:Number = (_local8 * _tang);
            if ((((_local9 > _local10)) || ((_local9 < -(_local10))))){
                return (OUTSIDE);
            };
            var _local11:Number = (((_local5 * _local4.n11) + (_local6 * _local4.n21)) + (_local7 * _local4.n31));
            _local10 = (_local10 * _ratio);
            if ((((_local11 > _local10)) || ((_local11 < -(_local10))))){
                return (OUTSIDE);
            };
            return (INSIDE);
        }
        public function get fov():Number{
            return (_fov);
        }
        public function set ratio(_arg1:Number):void{
            this.initialize(_fov, _arg1, _near, _far);
        }
        public function set near(_arg1:Number):void{
            this.initialize(_fov, _ratio, _arg1, _far);
        }
        public function set fov(_arg1:Number):void{
            this.initialize(_arg1, _ratio, _near, _far);
        }
        public function get far():Number{
            return (_far);
        }
        public function initialize(_arg1:Number=60, _arg2:Number=1.333, _arg3:Number=1, _arg4:Number=5000):void{
            _fov = _arg1;
            _ratio = _arg2;
            _near = _arg3;
            _far = _arg4;
            var _local5:Number = (((Math.PI / 180) * _fov) * 0.5);
            _tang = Math.tan(_local5);
            _nh = (_near * _tang);
            _nw = (_nh * _ratio);
            _fh = (_far * _tang);
            _fw = (_fh * _ratio);
            var _local6:Number = Math.atan((_tang * _ratio));
            _sphereX = (1 / Math.cos(_local6));
            _sphereY = (1 / Math.cos(_local5));
        }
        public function set far(_arg1:Number):void{
            this.initialize(_fov, _ratio, _near, _arg1);
        }
        public function get near():Number{
            return (_near);
        }
        public function sphereInFrustum(_arg1:DisplayObject3D, _arg2:BoundingSphere):int{
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local3:Number = _arg2.radius;
            var _local8:int = INSIDE;
            var _local9:Matrix3D = this.transform;
            var _local10:Number = (_arg1.world.n14 - _local9.n14);
            var _local11:Number = (_arg1.world.n24 - _local9.n24);
            var _local12:Number = (_arg1.world.n34 - _local9.n34);
            _local7 = (((_local10 * _local9.n13) + (_local11 * _local9.n23)) + (_local12 * _local9.n33));
            if ((((_local7 > (_far + _local3))) || ((_local7 < (_near - _local3))))){
                return (OUTSIDE);
            };
            if ((((_local7 > (_far - _local3))) || ((_local7 < (_near + _local3))))){
                _local8 = INTERSECT;
            };
            _local6 = (((_local10 * _local9.n12) + (_local11 * _local9.n22)) + (_local12 * _local9.n32));
            _local4 = (_sphereY * _local3);
            _local7 = (_local7 * _tang);
            if ((((_local6 > (_local7 + _local4))) || ((_local6 < (-(_local7) - _local4))))){
                return (OUTSIDE);
            };
            if ((((_local6 > (_local7 - _local4))) || ((_local6 < (-(_local7) + _local4))))){
                _local8 = INTERSECT;
            };
            _local5 = (((_local10 * _local9.n11) + (_local11 * _local9.n21)) + (_local12 * _local9.n31));
            _local7 = (_local7 * _ratio);
            _local4 = (_sphereX * _local3);
            if ((((_local5 > (_local7 + _local4))) || ((_local5 < (-(_local7) - _local4))))){
                return (OUTSIDE);
            };
            if ((((_local5 > (_local7 - _local4))) || ((_local5 < (-(_local7) + _local4))))){
                _local8 = INTERSECT;
            };
            return (_local8);
        }
        public function testObject(_arg1:DisplayObject3D):int{
            var _local2:int = INSIDE;
            if (((((!(_arg1.geometry)) || (!(_arg1.geometry.vertices)))) || (!(_arg1.geometry.vertices.length)))){
                return (_local2);
            };
            switch (_arg1.frustumTestMethod){
                case FrustumTestMethod.BOUNDING_SPHERE:
                    _local2 = sphereInFrustum(_arg1, _arg1.geometry.boundingSphere);
                    break;
                case FrustumTestMethod.BOUNDING_BOX:
                    _local2 = aabbInFrustum(_arg1, _arg1.geometry.aabb);
                    break;
                case FrustumTestMethod.NO_TESTING:
                    break;
                default:
                    break;
            };
            return (_local2);
        }
        public function aabbInFrustum(_arg1:DisplayObject3D, _arg2:AxisAlignedBoundingBox, _arg3:Boolean=true):int{
            var _local4:Vertex3D;
            var _local5:Number3D;
            var _local6:int;
            var _local7:int;
            var _local8:Array = _arg2.getBoxVertices();
            for each (_local4 in _local8) {
                _local5 = _local4.toNumber3D();
                Matrix3D.multiplyVector(_arg1.world, _local5);
                if (pointInFrustum(_local5.x, _local5.y, _local5.z) == INSIDE){
                    _local6++;
                    if (_arg3){
                        return (INSIDE);
                    };
                } else {
                    _local7++;
                };
                if (((_local6) && (_local7))){
                    return (INTERSECT);
                };
            };
            if (_local6){
                return (((_local6 < 8)) ? INTERSECT : INSIDE);
                //unresolved jump
            };
            return (OUTSIDE);
        }

    }
}//package org.papervision3d.core.culling 
