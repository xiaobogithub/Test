//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.cameras {
    import flash.geom.*;
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.core.culling.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;

    public class Camera3D extends CameraObject3D {

        private var _focusFix:Matrix3D
        private var _prevUseProjection:Boolean
        private var _prevZoom:Number
        private var _prevOrtho:Boolean
        private var _prevWidth:Number
        private var _prevHeight:Number
        private var _prevFocus:Number
        private var _projection:Matrix3D
        private var _prevOrthoProjection:Boolean

        public function Camera3D(_arg1:Number=60, _arg2:Number=10, _arg3:Number=5000, _arg4:Boolean=false, _arg5:Boolean=false){
            super(_arg2, 40);
            _prevFocus = 0;
            _prevZoom = 0;
            _prevOrtho = false;
            _prevUseProjection = false;
            _useCulling = _arg4;
            _useProjectionMatrix = _arg5;
            _far = _arg3;
            _focusFix = Matrix3D.IDENTITY;
        }
        public function update(_arg1:Rectangle):void{
            if (!_arg1){
                throw (new Error(("Camera3D#update: Invalid viewport rectangle! " + _arg1)));
            };
            this.viewport = _arg1;
            _prevFocus = this.focus;
            _prevZoom = this.zoom;
            _prevWidth = this.viewport.width;
            _prevHeight = this.viewport.height;
            if (_prevOrtho != this.ortho){
                if (this.ortho){
                    _prevOrthoProjection = this.useProjectionMatrix;
                    this.useProjectionMatrix = true;
                } else {
                    this.useProjectionMatrix = _prevOrthoProjection;
                };
            } else {
                if (_prevUseProjection != _useProjectionMatrix){
                    this.useProjectionMatrix = this._useProjectionMatrix;
                };
            };
            _prevOrtho = this.ortho;
            _prevUseProjection = _useProjectionMatrix;
            this.useCulling = _useCulling;
        }
        override public function set near(_arg1:Number):void{
            if (_arg1 > 0){
                this.focus = _arg1;
                this.update(this.viewport);
            };
        }
        override public function orbit(_arg1:Number, _arg2:Number, _arg3:Boolean=true, _arg4:DisplayObject3D=null):void{
            var _local10:Number;
            _arg4 = ((_arg4) || (_target));
            _arg4 = ((_arg4) || (DisplayObject3D.ZERO));
            if (_arg3){
                _arg1 = (_arg1 * (Math.PI / 180));
                _arg2 = (_arg2 * (Math.PI / 180));
            };
            var _local5:Number = (_arg4.world.n14 - this.x);
            var _local6:Number = (_arg4.world.n24 - this.y);
            var _local7:Number = (_arg4.world.n34 - this.z);
            var _local8:Number = Math.sqrt((((_local5 * _local5) + (_local6 * _local6)) + (_local7 * _local7)));
            var _local9:Number = (Math.cos(_arg2) * Math.sin(_arg1));
            _local10 = (Math.sin(_arg2) * Math.sin(_arg1));
            var _local11:Number = Math.cos(_arg1);
            this.x = (_arg4.world.n14 + (_local9 * _local8));
            this.y = (_arg4.world.n24 + (_local11 * _local8));
            this.z = (_arg4.world.n34 + (_local10 * _local8));
            this.lookAt(_arg4);
        }
        override public function set useCulling(_arg1:Boolean):void{
            super.useCulling = _arg1;
            if (_useCulling){
                if (!this.culler){
                    this.culler = new FrustumCuller();
                };
                FrustumCuller(this.culler).initialize(this.fov, (this.viewport.width / this.viewport.height), (this.focus / this.zoom), _far);
            } else {
                this.culler = null;
            };
        }
        override public function set orthoScale(_arg1:Number):void{
            super.orthoScale = _arg1;
            this.useProjectionMatrix = this.useProjectionMatrix;
            _prevOrtho = !(this.ortho);
            this.update(this.viewport);
        }
        override public function set far(_arg1:Number):void{
            if (_arg1 > this.focus){
                _far = _arg1;
                this.update(this.viewport);
            };
        }
        override public function transformView(_arg1:Matrix3D=null):void{
            if (((((((((((!((ortho == _prevOrtho))) || (!((_prevUseProjection == _useProjectionMatrix))))) || (!((focus == _prevFocus))))) || (!((zoom == _prevZoom))))) || (!((viewport.width == _prevWidth))))) || (!((viewport.height == _prevHeight))))){
                update(viewport);
            };
            if (_target){
                lookAt(_target);
            } else {
                if (_transformDirty){
                    updateTransform();
                };
            };
            if (_useProjectionMatrix){
                super.transformView();
                this.eye.calculateMultiply4x4(_projection, this.eye);
            } else {
                _focusFix.copy(this.transform);
                _focusFix.n14 = (_focusFix.n14 + (focus * this.transform.n13));
                _focusFix.n24 = (_focusFix.n24 + (focus * this.transform.n23));
                _focusFix.n34 = (_focusFix.n34 + (focus * this.transform.n33));
                super.transformView(_focusFix);
            };
            if ((culler is FrustumCuller)){
                FrustumCuller(culler).transform.copy(this.transform);
            };
        }
        override public function projectVertices(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            var _local17:Number;
            var _local18:Number;
            var _local19:Number;
            var _local20:Number;
            var _local21:Number;
            var _local22:Number;
            var _local23:Number;
            var _local24:Vertex3D;
            var _local25:Vertex3DInstance;
            var _local26:Number;
            if (((!(_arg1.geometry)) || (!(_arg1.geometry.vertices)))){
                return (0);
            };
            var _local3:Matrix3D = _arg1.view;
            var _local4:Array = _arg1.geometry.vertices;
            var _local5:Number = _local3.n11;
            var _local6:Number = _local3.n12;
            var _local7:Number = _local3.n13;
            var _local8:Number = _local3.n21;
            var _local9:Number = _local3.n22;
            var _local10:Number = _local3.n23;
            var _local11:Number = _local3.n31;
            var _local12:Number = _local3.n32;
            var _local13:Number = _local3.n33;
            var _local14:Number = _local3.n41;
            var _local15:Number = _local3.n42;
            var _local16:Number = _local3.n43;
            var _local27:int = _local4.length;
            var _local28:Number = _arg2.camera.focus;
            var _local29:Number = (_local28 * _arg2.camera.zoom);
            var _local30:Number = (viewport.width / 2);
            var _local31:Number = (viewport.height / 2);
            var _local32:Number = _arg2.camera.far;
            var _local33:Number = (_local32 - _local28);
            while ((_local24 = _local4[--_local27])) {
                _local17 = _local24.x;
                _local18 = _local24.y;
                _local19 = _local24.z;
                _local22 = ((((_local17 * _local11) + (_local18 * _local12)) + (_local19 * _local13)) + _local3.n34);
                _local25 = _local24.vertex3DInstance;
                if (_useProjectionMatrix){
                    _local23 = ((((_local17 * _local14) + (_local18 * _local15)) + (_local19 * _local16)) + _local3.n44);
                    _local22 = (_local22 / _local23);
                    if ((_local25.visible = (((_local22 > 0)) && ((_local22 < 1))))){
                        _local20 = (((((_local17 * _local5) + (_local18 * _local6)) + (_local19 * _local7)) + _local3.n14) / _local23);
                        _local21 = (((((_local17 * _local8) + (_local18 * _local9)) + (_local19 * _local10)) + _local3.n24) / _local23);
                        _local25.x = (_local20 * _local30);
                        _local25.y = (_local21 * _local31);
                        _local25.z = (_local22 * _local23);
                    };
                } else {
                    if ((_local25.visible = ((_local28 + _local22) > 0))){
                        _local20 = ((((_local17 * _local5) + (_local18 * _local6)) + (_local19 * _local7)) + _local3.n14);
                        _local21 = ((((_local17 * _local8) + (_local18 * _local9)) + (_local19 * _local10)) + _local3.n24);
                        _local26 = (_local29 / (_local28 + _local22));
                        _local25.x = (_local20 * _local26);
                        _local25.y = (_local21 * _local26);
                        _local25.z = _local22;
                    };
                };
            };
            return (0);
        }
        override public function set useProjectionMatrix(_arg1:Boolean):void{
            var _local2:Number;
            var _local3:Number;
            if (_arg1){
                if (this.ortho){
                    _local2 = (viewport.width / 2);
                    _local3 = (viewport.height / 2);
                    _projection = createOrthoMatrix(-(_local2), _local2, -(_local3), _local3, -(_far), _far);
                    _projection = Matrix3D.multiply(_orthoScaleMatrix, _projection);
                } else {
                    _projection = createPerspectiveMatrix(fov, (viewport.width / viewport.height), this.focus, this.far);
                };
            } else {
                if (this.ortho){
                    _arg1 = true;
                };
            };
            super.useProjectionMatrix = _arg1;
        }

        public static function createPerspectiveMatrix(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number):Matrix3D{
            var _local5:Number = ((_arg1 / 2) * (Math.PI / 180));
            var _local6:Number = Math.tan(_local5);
            var _local7:Number = (1 / _local6);
            return (new Matrix3D([(_local7 / _arg2), 0, 0, 0, 0, _local7, 0, 0, 0, 0, -(((_arg3 + _arg4) / (_arg3 - _arg4))), (((2 * _arg4) * _arg3) / (_arg3 - _arg4)), 0, 0, 1, 0]));
        }
        public static function createOrthoMatrix(_arg1:Number, _arg2:Number, _arg3:Number, _arg4:Number, _arg5:Number, _arg6:Number):Matrix3D{
            var _local7:Number = ((_arg2 + _arg1) / (_arg2 - _arg1));
            var _local8:Number = ((_arg4 + _arg3) / (_arg4 - _arg3));
            var _local9:Number = ((_arg6 + _arg5) / (_arg6 - _arg5));
            var _local10:Matrix3D = new Matrix3D([(2 / (_arg2 - _arg1)), 0, 0, _local7, 0, (2 / (_arg4 - _arg3)), 0, _local8, 0, 0, (-2 / (_arg6 - _arg5)), _local9, 0, 0, 0, 1]);
            _local10.calculateMultiply(Matrix3D.scaleMatrix(1, 1, -1), _local10);
            return (_local10);
        }

    }
}//package org.papervision3d.cameras 
