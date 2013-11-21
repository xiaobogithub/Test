//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.proto {
    import flash.geom.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.*;

    public class CameraObject3D extends DisplayObject3D {

        protected var _orthoScale:Number = 1
        public var culler:IObjectCuller
        public var sort:Boolean
        public var viewport:Rectangle
        protected var _target:DisplayObject3D
        protected var _orthoScaleMatrix:Matrix3D
        public var eye:Matrix3D
        protected var _ortho:Boolean
        protected var _useCulling:Boolean
        public var zoom:Number
        public var yUP:Boolean
        protected var _far:Number
        public var focus:Number
        protected var _useProjectionMatrix:Boolean

        public static var DEFAULT_VIEWPORT:Rectangle = new Rectangle(0, 0, 550, 400);
        public static var DEFAULT_POS:Number3D = new Number3D(0, 0, -1000);
        public static var DEFAULT_UP:Number3D = new Number3D(0, 1, 0);
        private static var _flipY:Matrix3D = Matrix3D.scaleMatrix(1, -1, 1);

        public function CameraObject3D(_arg1:Number=500, _arg2:Number=3){
            this.x = DEFAULT_POS.x;
            this.y = DEFAULT_POS.y;
            this.z = DEFAULT_POS.z;
            this.zoom = _arg2;
            this.focus = _arg1;
            this.eye = Matrix3D.IDENTITY;
            this.viewport = DEFAULT_VIEWPORT;
            this.sort = true;
            _ortho = false;
            _orthoScaleMatrix = Matrix3D.scaleMatrix(1, 1, 1);
            if (Papervision3D.useRIGHTHANDED){
                DEFAULT_UP.y = -1;
                this.yUP = false;
                this.lookAt(DisplayObject3D.ZERO);
            } else {
                this.yUP = true;
            };
        }
        public function get target():DisplayObject3D{
            return (_target);
        }
        public function get useProjectionMatrix():Boolean{
            return (_useProjectionMatrix);
        }
        public function set fov(_arg1:Number):void{
            if (((!(viewport)) || (viewport.isEmpty()))){
                PaperLogger.warning("CameraObject3D#viewport not set, can't set fov!");
                return;
            };
            var _local2:Number = 0;
            var _local3:Number = 0;
            var _local4:Number = 0;
            if (_target){
                _local2 = _target.world.n14;
                _local3 = _target.world.n24;
                _local4 = _target.world.n34;
            };
            var _local5:Number = (viewport.height / 2);
            var _local6:Number = ((_arg1 / 2) * (Math.PI / 180));
            this.focus = ((_local5 / Math.tan(_local6)) / this.zoom);
        }
        public function pan(_arg1:Number):void{
        }
        public function get far():Number{
            return (_far);
        }
        public function set target(_arg1:DisplayObject3D):void{
            _target = _arg1;
        }
        public function get useCulling():Boolean{
            return (_useCulling);
        }
        public function set far(_arg1:Number):void{
            if (_arg1 > this.focus){
                _far = _arg1;
            };
        }
        public function get near():Number{
            return (this.focus);
        }
        public function transformView(_arg1:Matrix3D=null):void{
            if (this.yUP){
                eye.calculateMultiply(((_arg1) || (this.transform)), _flipY);
                eye.invert();
            } else {
                eye.calculateInverse(((_arg1) || (this.transform)));
            };
        }
        public function set useProjectionMatrix(_arg1:Boolean):void{
            _useProjectionMatrix = _arg1;
        }
        public function tilt(_arg1:Number):void{
        }
        override public function lookAt(_arg1:DisplayObject3D, _arg2:Number3D=null):void{
            if (this.yUP){
                super.lookAt(_arg1, _arg2);
            } else {
                super.lookAt(_arg1, ((_arg2) || (DEFAULT_UP)));
            };
        }
        public function get ortho():Boolean{
            return (_ortho);
        }
        public function orbit(_arg1:Number, _arg2:Number, _arg3:Boolean=true, _arg4:DisplayObject3D=null):void{
        }
        public function get fov():Number{
            if (((!(viewport)) || (viewport.isEmpty()))){
                PaperLogger.warning("CameraObject3D#viewport not set, can't calculate fov!");
                return (NaN);
            };
            var _local1:Number = 0;
            var _local2:Number = 0;
            var _local3:Number = 0;
            if (_target){
                _local1 = _target.world.n14;
                _local2 = _target.world.n24;
                _local3 = _target.world.n34;
            };
            var _local4:Number = (this.x - _local1);
            var _local5:Number = (this.y - _local2);
            var _local6:Number = (this.z - _local3);
            var _local7:Number = this.focus;
            var _local8:Number = this.zoom;
            var _local9:Number = (Math.sqrt((((_local4 * _local4) + (_local5 * _local5)) + (_local6 * _local6))) + _local7);
            var _local10:Number = (viewport.height / 2);
            var _local11:Number = (180 / Math.PI);
            return (((Math.atan(((((_local9 / _local7) / _local8) * _local10) / _local9)) * _local11) * 2));
        }
        public function set near(_arg1:Number):void{
            if (_arg1 > 0){
                this.focus = _arg1;
            };
        }
        public function set useCulling(_arg1:Boolean):void{
            _useCulling = _arg1;
        }
        public function set orthoScale(_arg1:Number):void{
            _orthoScale = ((_arg1 > 0)) ? _arg1 : 0.0001;
            _orthoScaleMatrix.n11 = _orthoScale;
            _orthoScaleMatrix.n22 = _orthoScale;
            _orthoScaleMatrix.n33 = _orthoScale;
        }
        public function unproject(_arg1:Number, _arg2:Number):Number3D{
            var _local3:Number = ((focus * zoom) / focus);
            var _local4:Number3D = new Number3D((_arg1 / _local3), (-(_arg2) / _local3), focus);
            Matrix3D.multiplyVector3x3(transform, _local4);
            return (_local4);
        }
        public function set ortho(_arg1:Boolean):void{
            _ortho = _arg1;
        }
        public function projectVertices(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            return (0);
        }
        public function get orthoScale():Number{
            return (_orthoScale);
        }

    }
}//package org.papervision3d.core.proto 
