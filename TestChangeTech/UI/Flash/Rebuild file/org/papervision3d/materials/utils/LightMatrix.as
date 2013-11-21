//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.materials.utils {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.*;
    import org.papervision3d.lights.*;

    public class LightMatrix {

        private static var _targetPos:Number3D = new Number3D();
        private static var _lightUp:Number3D = new Number3D();
        private static var _lightSide:Number3D = new Number3D();
        private static var _lightDir:Number3D = new Number3D();
        private static var lightMatrix:Matrix3D = Matrix3D.IDENTITY;
        private static var invMatrix:Matrix3D = Matrix3D.IDENTITY;
        protected static var UP:Number3D = new Number3D(0, 1, 0);
        private static var _lightPos:Number3D = new Number3D();

        public static function getLightMatrix(_arg1:LightObject3D, _arg2:DisplayObject3D, _arg3:RenderSessionData, _arg4:Matrix3D):Matrix3D{
            var _local6:Matrix3D;
            var _local7:Matrix3D;
            var _local5:Matrix3D = (_arg4) ? _arg4 : Matrix3D.IDENTITY;
            if (_arg1 == null){
                _arg1 = new PointLight3D();
                _arg1.copyPosition(_arg3.camera);
            };
            _targetPos.reset();
            _lightPos.reset();
            _lightDir.reset();
            _lightUp.reset();
            _lightSide.reset();
            _local6 = _arg1.transform;
            _local7 = _arg2.world;
            _lightPos.x = -(_local6.n14);
            _lightPos.y = -(_local6.n24);
            _lightPos.z = -(_local6.n34);
            _targetPos.x = -(_local7.n14);
            _targetPos.y = -(_local7.n24);
            _targetPos.z = -(_local7.n34);
            _lightDir.x = (_targetPos.x - _lightPos.x);
            _lightDir.y = (_targetPos.y - _lightPos.y);
            _lightDir.z = (_targetPos.z - _lightPos.z);
            invMatrix.calculateInverse(_arg2.world);
            Matrix3D.multiplyVector3x3(invMatrix, _lightDir);
            _lightDir.normalize();
            _lightSide.x = ((_lightDir.y * UP.z) - (_lightDir.z * UP.y));
            _lightSide.y = ((_lightDir.z * UP.x) - (_lightDir.x * UP.z));
            _lightSide.z = ((_lightDir.x * UP.y) - (_lightDir.y * UP.x));
            _lightSide.normalize();
            _lightUp.x = ((_lightSide.y * _lightDir.z) - (_lightSide.z * _lightDir.y));
            _lightUp.y = ((_lightSide.z * _lightDir.x) - (_lightSide.x * _lightDir.z));
            _lightUp.z = ((_lightSide.x * _lightDir.y) - (_lightSide.y * _lightDir.x));
            _lightUp.normalize();
            if (((Papervision3D.useRIGHTHANDED) || (_arg2.flipLightDirection))){
                _lightDir.x = -(_lightDir.x);
                _lightDir.y = -(_lightDir.y);
                _lightDir.z = -(_lightDir.z);
            };
            _local5.n11 = _lightSide.x;
            _local5.n12 = _lightSide.y;
            _local5.n13 = _lightSide.z;
            _local5.n21 = _lightUp.x;
            _local5.n22 = _lightUp.y;
            _local5.n23 = _lightUp.z;
            _local5.n31 = _lightDir.x;
            _local5.n32 = _lightDir.y;
            _local5.n33 = _lightDir.z;
            return (_local5);
        }

    }
}//package org.papervision3d.materials.utils 
