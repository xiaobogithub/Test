//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.objects {
    import org.papervision3d.core.proto.*;
    import org.papervision3d.core.render.data.*;
    import org.papervision3d.view.layer.*;
    import org.papervision3d.view.*;
    import org.papervision3d.core.data.*;
    import org.papervision3d.core.math.*;
    import org.papervision3d.core.geom.renderables.*;
    import org.papervision3d.core.log.*;
    import org.papervision3d.materials.utils.*;
    import org.papervision3d.materials.shaders.*;
    import org.papervision3d.core.material.*;
    import org.papervision3d.*;

    public class DisplayObject3D extends DisplayObjectContainer3D {

        public var extra:Object
        public var frustumTestMethod:int = 0
        private var _rot:Quaternion
        public var id:int
        private var _rotationY:Number
        private var _rotationZ:Number
        public var meshSort:uint = 1
        public var materials:MaterialsList
        private var _rotationX:Number
        private var _qYaw:Quaternion
        private var _zAxis:Number3D
        private var _xAxis:Number3D
        private var _scaleDirty:Boolean = false
        private var _autoCalcScreenCoords:Boolean = false
        private var _tempScale:Number3D
        private var _numClones:uint = 0
        public var alpha:Number = 1
        public var screen:Number3D
        private var _scaleX:Number
        private var _scaleY:Number
        private var _scaleZ:Number
        public var geometry:GeometryObject3D
        private var _qPitch:Quaternion
        public var visible:Boolean
        protected var _userData:UserData
        public var screenZ:Number
        public var container:ViewportLayer
        protected var _useOwnContainer:Boolean = false
        public var transform:Matrix3D
        private var _material:MaterialObject3D
        private var _position:Number3D
        public var name:String
        protected var _scene:SceneObject3D = null
        private var _qRoll:Quaternion
        private var _localRotationZ:Number = 0
        public var culled:Boolean
        public var world:Matrix3D
        public var blendMode:String = "normal"
        private var _localRotationX:Number = 0
        private var _localRotationY:Number = 0
        public var view:Matrix3D
        public var parent:DisplayObjectContainer3D
        private var _target:Number3D
        public var faces:Array
        private var _yAxis:Number3D
        public var flipLightDirection:Boolean = false
        private var _rotation:Number3D
        protected var _transformDirty:Boolean = false
        protected var _sorted:Array
        private var _rotationDirty:Boolean = false
        public var parentContainer:DisplayObject3D
        public var filters:Array

        public static const MESH_SORT_CENTER:uint = 1;
        private static const LEFT:Number3D = new Number3D(-1, 0, 0);
        public static const MESH_SORT_CLOSE:uint = 3;
        private static const BACKWARD:Number3D = new Number3D(0, 0, -1);
        private static const FORWARD:Number3D = new Number3D(0, 0, 1);
        public static const MESH_SORT_FAR:uint = 2;
        private static const DOWN:Number3D = new Number3D(0, -1, 0);
        private static const UP:Number3D = new Number3D(0, 1, 0);
        private static const RIGHT:Number3D = new Number3D(1, 0, 0);

        private static var entry_count:uint = 0;
        private static var _newID:int = 0;
        private static var _tempMatrix:Matrix3D = Matrix3D.IDENTITY;
        public static var sortedArray:Array = new Array();
        public static var faceLevelMode:Boolean;
        private static var _tempQuat:Quaternion = new Quaternion();
        private static var toRADIANS:Number = 0.0174532925199433;
        private static var toDEGREES:Number = 57.2957795130823;

        public function DisplayObject3D(_arg1:String=null, _arg2:GeometryObject3D=null):void{
            faces = new Array();
            filters = [];
            screen = new Number3D();
            _position = Number3D.ZERO;
            _target = Number3D.ZERO;
            _zAxis = Number3D.ZERO;
            _xAxis = Number3D.ZERO;
            _yAxis = Number3D.ZERO;
            _rotation = Number3D.ZERO;
            _rot = new Quaternion();
            _qPitch = new Quaternion();
            _qYaw = new Quaternion();
            _qRoll = new Quaternion();
            super();
            if (_arg1 != null){
                PaperLogger.info(("DisplayObject3D: " + _arg1));
            };
            this.culled = false;
            this.transform = Matrix3D.IDENTITY;
            this.world = Matrix3D.IDENTITY;
            this.view = Matrix3D.IDENTITY;
            this.x = 0;
            this.y = 0;
            this.z = 0;
            rotationX = 0;
            rotationY = 0;
            rotationZ = 0;
            _localRotationX = (_localRotationY = (_localRotationZ = 0));
            var _local3:Number = (Papervision3D.usePERCENT) ? 100 : 1;
            scaleX = _local3;
            scaleY = _local3;
            scaleZ = _local3;
            _tempScale = new Number3D();
            this.visible = true;
            this.id = _newID++;
            this.name = ((_arg1) || (String(this.id)));
            _numClones = 0;
            if (_arg2){
                addGeometry(_arg2);
            };
        }
        public function set z(_arg1:Number):void{
            this.transform.n34 = _arg1;
        }
        override public function addChild(_arg1:DisplayObject3D, _arg2:String=null):DisplayObject3D{
            _arg1 = super.addChild(_arg1, _arg2);
            if (_arg1.scene == null){
                _arg1.scene = scene;
            };
            if (this.useOwnContainer){
                _arg1.parentContainer = this;
            };
            return (_arg1);
        }
        public function setChildMaterialByName(_arg1:String, _arg2:MaterialObject3D):void{
            setChildMaterial(getChildByName(_arg1, true), _arg2);
        }
        public function moveDown(_arg1:Number):void{
            translate(_arg1, DOWN);
        }
        public function project(_arg1:DisplayObject3D, _arg2:RenderSessionData):Number{
            var _local5:DisplayObject3D;
            if (this._transformDirty){
                updateTransform();
            };
            this.world.calculateMultiply(_arg1.world, this.transform);
            if (_arg2.camera.culler){
                if (this === _arg2.camera){
                    this.culled = true;
                } else {
                    this.culled = (_arg2.camera.culler.testObject(this) < 0);
                };
                if (this.culled){
                    _arg2.renderStatistics.culledObjects++;
                    return (0);
                };
            };
            if (_arg1 !== _arg2.camera){
                if (_arg2.camera.useProjectionMatrix){
                    this.view.calculateMultiply4x4(_arg1.view, this.transform);
                } else {
                    this.view.calculateMultiply(_arg1.view, this.transform);
                };
            } else {
                if (_arg2.camera.useProjectionMatrix){
                    this.view.calculateMultiply4x4(_arg2.camera.eye, this.transform);
                } else {
                    this.view.calculateMultiply(_arg2.camera.eye, this.transform);
                };
            };
            if (_autoCalcScreenCoords){
                calculateScreenCoords(_arg2.camera);
            };
            var _local3:Number = 0;
            var _local4:Number = 0;
            for each (_local5 in this._childrenByName) {
                if (_local5.visible){
                    _local3 = (_local3 + _local5.project(this, _arg2));
                    _local4++;
                };
            };
            return ((this.screenZ = (_local3 / _local4)));
        }
        public function set scene(_arg1:SceneObject3D):void{
            var _local2:DisplayObject3D;
            _scene = _arg1;
            for each (_local2 in this._childrenByName) {
                if (_local2.scene == null){
                    _local2.scene = _scene;
                };
            };
        }
        public function setChildMaterial(_arg1:DisplayObject3D, _arg2:MaterialObject3D, _arg3:MaterialObject3D=null):void{
            var _local4:Triangle3D;
            if (!_arg1){
                return;
            };
            if (((!(_arg3)) || ((_arg1.material === _arg3)))){
                _arg1.material = _arg2;
            };
            if (((_arg1.geometry) && (_arg1.geometry.faces))){
                for each (_local4 in _arg1.geometry.faces) {
                    if (((!(_arg3)) || ((_local4.material === _arg3)))){
                        _local4.material = _arg2;
                    };
                };
            };
        }
        public function get userData():UserData{
            return (_userData);
        }
        public function get material():MaterialObject3D{
            return (_material);
        }
        public function set userData(_arg1:UserData):void{
            _userData = _arg1;
        }
        public function lookAt(_arg1:DisplayObject3D, _arg2:Number3D=null):void{
            var _local3:DisplayObject3D;
            var _local4:Matrix3D;
            if ((this is CameraObject3D)){
                _position.reset(this.x, this.y, this.z);
            } else {
                _local3 = (this.parent as DisplayObject3D);
                if (_local3){
                    world.calculateMultiply(_local3.world, transform);
                } else {
                    world.copy(transform);
                };
                _position.reset(world.n14, world.n24, world.n34);
            };
            if ((_arg1 is CameraObject3D)){
                _target.reset(_arg1.x, _arg1.y, _arg1.z);
            } else {
                _local3 = (_arg1.parent as DisplayObject3D);
                if (_local3){
                    _arg1.world.calculateMultiply(_local3.world, _arg1.transform);
                } else {
                    _arg1.world.copy(_arg1.transform);
                };
                _target.reset(_arg1.world.n14, _arg1.world.n24, _arg1.world.n34);
            };
            _zAxis.copyFrom(_target);
            _zAxis.minusEq(_position);
            _zAxis.normalize();
            if (_zAxis.modulo > 0.1){
                _xAxis = Number3D.cross(_zAxis, ((_arg2) || (UP)), _xAxis);
                _xAxis.normalize();
                _yAxis = Number3D.cross(_zAxis, _xAxis, _yAxis);
                _yAxis.normalize();
                _local4 = this.transform;
                _local4.n11 = (_xAxis.x * _scaleX);
                _local4.n21 = (_xAxis.y * _scaleX);
                _local4.n31 = (_xAxis.z * _scaleX);
                _local4.n12 = (-(_yAxis.x) * _scaleY);
                _local4.n22 = (-(_yAxis.y) * _scaleY);
                _local4.n32 = (-(_yAxis.z) * _scaleY);
                _local4.n13 = (_zAxis.x * _scaleZ);
                _local4.n23 = (_zAxis.y * _scaleZ);
                _local4.n33 = (_zAxis.z * _scaleZ);
                _localRotationX = (_localRotationY = (_localRotationZ = 0));
                this._transformDirty = false;
                this._rotationDirty = true;
            } else {
                PaperLogger.error("lookAt error");
            };
        }
        public function set rotationX(_arg1:Number):void{
            this._rotationX = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            this._transformDirty = true;
        }
        public function calculateScreenCoords(_arg1:CameraObject3D):void{
            var _local2:Number;
            var _local3:Number;
            var _local4:Number;
            var _local5:Number;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            if (_arg1.useProjectionMatrix){
                _local2 = 0;
                _local3 = 0;
                _local4 = 0;
                _local5 = ((((_local2 * view.n41) + (_local3 * view.n42)) + (_local4 * view.n43)) + view.n44);
                _local6 = (_arg1.viewport.width / 2);
                _local7 = (_arg1.viewport.height / 2);
                screen.x = (((((_local2 * view.n11) + (_local3 * view.n12)) + (_local4 * view.n13)) + view.n14) / _local5);
                screen.y = (((((_local2 * view.n21) + (_local3 * view.n22)) + (_local4 * view.n23)) + view.n24) / _local5);
                screen.z = ((((_local2 * view.n31) + (_local3 * view.n32)) + (_local4 * view.n33)) + view.n34);
                screen.x = (screen.x * _local6);
                screen.y = (screen.y * _local7);
            } else {
                _local8 = ((_arg1.focus * _arg1.zoom) / (_arg1.focus + view.n34));
                screen.x = (view.n14 * _local8);
                screen.y = (view.n24 * _local8);
                screen.z = view.n34;
            };
        }
        public function set rotationZ(_arg1:Number):void{
            this._rotationZ = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            this._transformDirty = true;
        }
        public function pitch(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (this._transformDirty){
                updateTransform();
            };
            _qPitch.setFromAxisAngle(transform.n11, transform.n21, transform.n31, _arg1);
            this.transform.calculateMultiply3x3(_qPitch.matrix, transform);
            _localRotationX = (_localRotationX + _arg1);
            _rotationDirty = true;
        }
        protected function setParentContainer(_arg1:DisplayObject3D, _arg2:Boolean=true):void{
            var _local3:DisplayObject3D;
            if (((_arg2) && (!((_arg1 == this))))){
                parentContainer = _arg1;
            };
            for each (_local3 in children) {
                _local3.setParentContainer(_arg1, _arg2);
            };
        }
        public function set rotationY(_arg1:Number):void{
            this._rotationY = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            this._transformDirty = true;
        }
        public function addGeometry(_arg1:GeometryObject3D=null):void{
            if (_arg1){
                this.geometry = _arg1;
            };
        }
        public function get sceneX():Number{
            return (this.world.n14);
        }
        public function get scaleX():Number{
            if (Papervision3D.usePERCENT){
                return ((this._scaleX * 100));
            };
            return (this._scaleX);
        }
        public function get scaleY():Number{
            if (Papervision3D.usePERCENT){
                return ((this._scaleY * 100));
            };
            return (this._scaleY);
        }
        public function get scaleZ():Number{
            if (Papervision3D.usePERCENT){
                return ((this._scaleZ * 100));
            };
            return (this._scaleZ);
        }
        public function get scale():Number{
            if ((((this._scaleX == this._scaleY)) && ((this._scaleX == this._scaleZ)))){
                if (Papervision3D.usePERCENT){
                    return ((this._scaleX * 100));
                };
                return (this._scaleX);
                //unresolved jump
            };
            return (NaN);
        }
        public function moveUp(_arg1:Number):void{
            translate(_arg1, UP);
        }
        public function get sceneZ():Number{
            return (this.world.n34);
        }
        public function get sceneY():Number{
            return (this.world.n24);
        }
        public function distanceTo(_arg1:DisplayObject3D):Number{
            var _local2:Number = (this.x - _arg1.x);
            var _local3:Number = (this.y - _arg1.y);
            var _local4:Number = (this.z - _arg1.z);
            return (Math.sqrt((((_local2 * _local2) + (_local3 * _local3)) + (_local4 * _local4))));
        }
        private function updateMaterials(_arg1:DisplayObject3D, _arg2:MaterialObject3D, _arg3:MaterialObject3D):void{
            var _local4:DisplayObject3D;
            var _local5:Triangle3D;
            _arg2.unregisterObject(_arg1);
            if ((((_arg3 is AbstractLightShadeMaterial)) || ((_arg3 is ShadedMaterial)))){
                _arg3.registerObject(_arg1);
            };
            if (_arg1.material === _arg2){
                _arg1.material = _arg3;
            };
            if (((((_arg1.geometry) && (_arg1.geometry.faces))) && (_arg1.geometry.faces.length))){
                for each (_local5 in _arg1.geometry.faces) {
                    if (_local5.material === _arg2){
                        _local5.material = _arg3;
                    };
                };
            };
            for each (_local4 in _arg1.children) {
                updateMaterials(_local4, _arg2, _arg3);
            };
        }
        public function clone():DisplayObject3D{
            var _local3:DisplayObject3D;
            var _local1:String = ((this.name + "_") + _numClones++);
            var _local2:DisplayObject3D = new DisplayObject3D(_local1);
            if (this.material){
                _local2.material = this.material;
            };
            if (this.materials){
                _local2.materials = this.materials.clone();
            };
            if (this.geometry){
                _local2.geometry = this.geometry.clone(_local2);
                _local2.geometry.ready = true;
            };
            _local2.copyTransform(this.transform);
            for each (_local3 in this.children) {
                _local2.addChild(_local3.clone());
            };
            return (_local2);
        }
        public function set material(_arg1:MaterialObject3D):void{
            if (_material){
                _material.unregisterObject(this);
            };
            _material = _arg1;
            _material.registerObject(this);
        }
        private function updateRotation():void{
            _tempScale.x = (Papervision3D.usePERCENT) ? (_scaleX * 100) : _scaleX;
            _tempScale.y = (Papervision3D.usePERCENT) ? (_scaleY * 100) : _scaleY;
            _tempScale.z = (Papervision3D.usePERCENT) ? (_scaleZ * 100) : _scaleZ;
            _rotation = Matrix3D.matrix2euler(this.transform, _rotation, _tempScale);
            this._rotationX = (_rotation.x * toRADIANS);
            this._rotationY = (_rotation.y * toRADIANS);
            this._rotationZ = (_rotation.z * toRADIANS);
            this._rotationDirty = false;
        }
        public function hitTestObject(_arg1:DisplayObject3D, _arg2:Number=1):Boolean{
            var _local3:Number = (this.x - _arg1.x);
            var _local4:Number = (this.y - _arg1.y);
            var _local5:Number = (this.z - _arg1.z);
            var _local6:Number = (((_local3 * _local3) + (_local4 * _local4)) + (_local5 * _local5));
            var _local7:Number = (this.geometry) ? this.geometry.boundingSphere.maxDistance : 0;
            var _local8:Number = (_arg1.geometry) ? _arg1.geometry.boundingSphere.maxDistance : 0;
            _local7 = (_local7 * _arg2);
            return (((_local7 + _local8) > _local6));
        }
        public function translate(_arg1:Number, _arg2:Number3D):void{
            var _local3:Number3D = _arg2.clone();
            if (this._transformDirty){
                updateTransform();
            };
            Matrix3D.rotateAxis(transform, _local3);
            this.x = (this.x + (_arg1 * _local3.x));
            this.y = (this.y + (_arg1 * _local3.y));
            this.z = (this.z + (_arg1 * _local3.z));
        }
        public function get localRotationZ():Number{
            return ((Papervision3D.useDEGREES) ? (_localRotationZ * toDEGREES) : _localRotationZ);
        }
        public function get localRotationY():Number{
            return ((Papervision3D.useDEGREES) ? (_localRotationY * toDEGREES) : _localRotationY);
        }
        public function get z():Number{
            return (this.transform.n34);
        }
        public function get localRotationX():Number{
            return ((Papervision3D.useDEGREES) ? (_localRotationX * toDEGREES) : _localRotationX);
        }
        public function get x():Number{
            return (this.transform.n14);
        }
        public function get y():Number{
            return (this.transform.n24);
        }
        public function moveLeft(_arg1:Number):void{
            translate(_arg1, LEFT);
        }
        public function replaceMaterialByName(_arg1:MaterialObject3D, _arg2:String):void{
            if (!this.materials){
                return;
            };
            var _local3:MaterialObject3D = this.materials.getMaterialByName(_arg2);
            if (!_local3){
                return;
            };
            if (this.material === _local3){
                this.material = _arg1;
            };
            _local3 = this.materials.removeMaterial(_local3);
            _arg1 = this.materials.addMaterial(_arg1, _arg2);
            updateMaterials(this, _local3, _arg1);
        }
        public function get scene():SceneObject3D{
            return (_scene);
        }
        public function set useOwnContainer(_arg1:Boolean):void{
            _useOwnContainer = _arg1;
            setParentContainer(this, true);
        }
        public function getMaterialByName(_arg1:String):MaterialObject3D{
            var _local3:DisplayObject3D;
            var _local2:MaterialObject3D = (this.materials) ? this.materials.getMaterialByName(_arg1) : null;
            if (_local2){
                return (_local2);
            };
            for each (_local3 in this._childrenByName) {
                _local2 = _local3.getMaterialByName(_arg1);
                if (_local2){
                    return (_local2);
                };
            };
            return (null);
        }
        public function copyTransform(_arg1):void{
            var _local2:Matrix3D;
            var _local3:Matrix3D;
            if ((_arg1 is DisplayObject3D)){
                DisplayObject3D(_arg1).updateTransform();
            };
            _local2 = this.transform;
            _local3 = ((_arg1 is DisplayObject3D)) ? _arg1.transform : _arg1;
            _local2.n11 = _local3.n11;
            _local2.n12 = _local3.n12;
            _local2.n13 = _local3.n13;
            _local2.n14 = _local3.n14;
            _local2.n21 = _local3.n21;
            _local2.n22 = _local3.n22;
            _local2.n23 = _local3.n23;
            _local2.n24 = _local3.n24;
            _local2.n31 = _local3.n31;
            _local2.n32 = _local3.n32;
            _local2.n33 = _local3.n33;
            _local2.n34 = _local3.n34;
            this._transformDirty = false;
            this._rotationDirty = true;
        }
        public function get rotationY():Number{
            if (this._rotationDirty){
                updateRotation();
            };
            return ((Papervision3D.useDEGREES) ? (this._rotationY * toDEGREES) : this._rotationY);
        }
        public function get rotationZ():Number{
            if (this._rotationDirty){
                updateRotation();
            };
            return ((Papervision3D.useDEGREES) ? (this._rotationZ * toDEGREES) : this._rotationZ);
        }
        public function set scaleY(_arg1:Number):void{
            if (Papervision3D.usePERCENT){
                this._scaleY = (_arg1 / 100);
            } else {
                this._scaleY = _arg1;
            };
            this._transformDirty = true;
        }
        public function roll(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (_transformDirty){
                updateTransform();
            };
            _qRoll.setFromAxisAngle(transform.n13, transform.n23, transform.n33, _arg1);
            transform.calculateMultiply3x3(_qRoll.matrix, transform);
            _localRotationZ = (_localRotationZ + _arg1);
            _rotationDirty = true;
        }
        public function set scaleZ(_arg1:Number):void{
            if (Papervision3D.usePERCENT){
                this._scaleZ = (_arg1 / 100);
            } else {
                this._scaleZ = _arg1;
            };
            this._transformDirty = true;
        }
        public function get rotationX():Number{
            if (this._rotationDirty){
                updateRotation();
            };
            return ((Papervision3D.useDEGREES) ? (this._rotationX * toDEGREES) : this._rotationX);
        }
        public function set scale(_arg1:Number):void{
            if (Papervision3D.usePERCENT){
                _arg1 = (_arg1 / 100);
            };
            this._scaleX = (this._scaleY = (this._scaleZ = _arg1));
            this._transformDirty = true;
        }
        public function get autoCalcScreenCoords():Boolean{
            return (_autoCalcScreenCoords);
        }
        public function yaw(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (_transformDirty){
                updateTransform();
            };
            _qYaw.setFromAxisAngle(transform.n12, transform.n22, transform.n32, _arg1);
            transform.calculateMultiply3x3(_qYaw.matrix, transform);
            _localRotationY = (_localRotationY + _arg1);
            _rotationDirty = true;
        }
        public function set scaleX(_arg1:Number):void{
            if (Papervision3D.usePERCENT){
                this._scaleX = (_arg1 / 100);
            } else {
                this._scaleX = _arg1;
            };
            this._transformDirty = true;
        }
        public function createViewportLayer(_arg1:Viewport3D, _arg2:Boolean=true):ViewportLayer{
            var _local3:ViewportLayer = _arg1.getChildLayer(this, true);
            if (_arg2){
                addChildrenToLayer(this, _local3);
            };
            return (_local3);
        }
        override public function toString():String{
            return (((((((this.name + ": x:") + Math.round(this.x)) + " y:") + Math.round(this.y)) + " z:") + Math.round(this.z)));
        }
        public function moveForward(_arg1:Number):void{
            translate(_arg1, FORWARD);
        }
        public function addChildrenToLayer(_arg1:DisplayObject3D, _arg2:ViewportLayer):void{
            var _local3:DisplayObject3D;
            for each (_local3 in _arg1.children) {
                _arg2.addDisplayObject3D(_local3);
                _local3.addChildrenToLayer(_local3, _arg2);
            };
        }
        public function copyPosition(_arg1):void{
            var _local2:Matrix3D = this.transform;
            var _local3:Matrix3D = ((_arg1 is DisplayObject3D)) ? _arg1.transform : _arg1;
            _local2.n14 = _local3.n14;
            _local2.n24 = _local3.n24;
            _local2.n34 = _local3.n34;
        }
        public function get useOwnContainer():Boolean{
            return (_useOwnContainer);
        }
        public function updateTransform():void{
            _rot.setFromEuler(_rotationY, _rotationZ, _rotationX);
            this.transform.copy3x3(_rot.matrix);
            _tempMatrix.reset();
            _tempMatrix.n11 = this._scaleX;
            _tempMatrix.n22 = this._scaleY;
            _tempMatrix.n33 = this._scaleZ;
            this.transform.calculateMultiply(this.transform, _tempMatrix);
            _transformDirty = false;
        }
        public function hitTestPoint(_arg1:Number, _arg2:Number, _arg3:Number):Boolean{
            var _local4:Number = (this.x - _arg1);
            var _local5:Number = (this.y - _arg2);
            var _local6:Number = (this.z - _arg3);
            var _local7:Number = (((_local4 * _local4) + (_local5 * _local5)) + (_local6 * _local6));
            var _local8:Number = (this.geometry) ? this.geometry.boundingSphere.maxDistance : 0;
            return ((_local8 > _local7));
        }
        public function moveRight(_arg1:Number):void{
            translate(_arg1, RIGHT);
        }
        public function moveBackward(_arg1:Number):void{
            translate(_arg1, BACKWARD);
        }
        public function set localRotationY(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (_transformDirty){
                updateTransform();
            };
            _qYaw.setFromAxisAngle(transform.n12, transform.n22, transform.n32, (_localRotationY - _arg1));
            transform.calculateMultiply3x3(_qYaw.matrix, transform);
            _localRotationY = _arg1;
            _rotationDirty = true;
        }
        public function set localRotationZ(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (_transformDirty){
                updateTransform();
            };
            _qRoll.setFromAxisAngle(transform.n13, transform.n23, transform.n33, (_localRotationZ - _arg1));
            transform.calculateMultiply3x3(_qRoll.matrix, transform);
            _localRotationZ = _arg1;
            _rotationDirty = true;
        }
        public function set localRotationX(_arg1:Number):void{
            _arg1 = (Papervision3D.useDEGREES) ? (_arg1 * toRADIANS) : _arg1;
            if (this._transformDirty){
                updateTransform();
            };
            _qPitch.setFromAxisAngle(transform.n11, transform.n21, transform.n31, (_localRotationX - _arg1));
            this.transform.calculateMultiply3x3(_qPitch.matrix, transform);
            _localRotationX = _arg1;
            _rotationDirty = true;
        }
        public function set x(_arg1:Number):void{
            this.transform.n14 = _arg1;
        }
        public function materialsList():String{
            var _local2:String;
            var _local3:DisplayObject3D;
            var _local1 = "";
            for (_local2 in this.materials) {
                _local1 = (_local1 + (_local2 + "\n"));
            };
            for each (_local3 in this._childrenByName) {
                for (_local2 in _local3.materials.materialsByName) {
                    _local1 = (_local1 + (("+ " + _local2) + "\n"));
                };
            };
            return (_local1);
        }
        public function set y(_arg1:Number):void{
            this.transform.n24 = _arg1;
        }
        public function set autoCalcScreenCoords(_arg1:Boolean):void{
            _autoCalcScreenCoords = _arg1;
        }

        public static function get ZERO():DisplayObject3D{
            return (new (DisplayObject3D));
        }

    }
}//package org.papervision3d.objects 
