//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.transitions {
    import flash.display.*;
    import mx.core.*;
    import flash.events.*;
    import com.redbox.changetech.control.*;
    import mx.containers.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import assets.*;
    import flash.utils.*;
    import com.redbox.changetech.view.modules.*;
    import org.papervision3d.materials.*;
    import org.papervision3d.objects.*;
    import org.papervision3d.view.*;
    import caurina.transitions.*;
    import org.papervision3d.objects.primitives.*;

    public class RoomTransition extends BasicView {

        protected var _viewStackChildren:Array
        protected var _spinTime:Number = 1
        protected var _nextIndex:int
        protected var _plane1_1:Plane
        private var model:BalanceModelLocator
        protected var _zoomOutTime:Number = 2
        protected var paralax:Number = 1
        protected var _zoomDistance:Number = 500
        protected var _timer:Timer
        protected var _targetRotation:Number
        protected var _spinDirection:Number
        protected var _ViewStackWidth:Number
        protected var _rootNode:DisplayObject3D
        protected var _viewStack:ViewStack
        protected var _plane0_1:Plane
        protected var _zoomInTime:Number = 2
        private var yOffset:Number
        protected var _plane0:Plane
        protected var _plane1:Plane
        protected var _plane2:Plane
        protected var _selectedIndex:int = 3
        private var myHeight:Number

        public function RoomTransition(_arg1:ViewStack, _arg2:Number, _arg3:Number, _arg4:Boolean=false, _arg5:Boolean=false, _arg6:String="Target"){
            model = BalanceModelLocator.getInstance();
            super(_arg2, _arg3, _arg4, _arg5, _arg6);
            myHeight = (model.currentStageHeight + yOffset);
            _viewStack = _arg1;
            _viewStackChildren = _viewStack.getChildren();
            visible = false;
            setupScene();
        }
        protected function zoomOut():void{
            Tweener.addTween(camera, {z:(camera.z - _zoomDistance), time:_zoomOutTime, transition:"easeoutcubic", onComplete:spin});
            Tweener.addTween(_plane0, {z:(_plane0.z + (_ViewStackWidth * 2)), time:_zoomOutTime, transition:"easeoutcubic"});
        }
        protected function deferStartTransition(_arg1:TimerEvent):void{
            _timer.removeEventListener(TimerEvent.TIMER_COMPLETE, deferStartTransition);
            var _local2:AbstractBalanceModule = (BalanceRoom(_viewStackChildren[selectedIndex]).module as AbstractBalanceModule);
            trace(("mod=" + _local2));
            if (_local2 == null){
                _selectedIndex = _nextIndex;
                transitionFinished();
                return;
            };
            trace(((("mods=" + _local2.transitionContainer1) + ":") + _local2.transitionContainer2));
            if ((((_local2.transitionContainer1 == null)) || ((_local2.transitionContainer2 == null)))){
                _selectedIndex = _nextIndex;
                transitionFinished();
                return;
            };
            updatePlaneMaterials(_nextIndex);
            updatePlanePosition(_spinDirection);
            _selectedIndex = _nextIndex;
            startTransition();
        }
        protected function setupScene():void{
            camera.z = -1;
            camera.zoom = 100;
            _ViewStackWidth = 1000;
            _plane0 = new Plane(null, (_viewStack.width * paralax), (_viewStack.height * paralax), 4, 4);
            _plane0.z = ((_ViewStackWidth * paralax) + 1);
            _plane0_1 = new Plane(null, _viewStack.width, _viewStack.height, 4, 4);
            _plane0_1.z = _ViewStackWidth;
            _plane1 = new Plane(null, (_viewStack.width * paralax), (_viewStack.height * paralax), 4, 4);
            _plane1_1 = new Plane(null, _viewStack.width, _viewStack.height, 4, 4);
            _plane2 = new Plane(null, (model.currentStageWidth * 2), (model.currentStageWidth * 2), 4, 4);
            _plane2.y = -((_ViewStackWidth / 3));
            _plane2.rotationX = 90;
            _rootNode = scene.addChild(new DisplayObject3D(), "rootNode");
            _rootNode.addChild(_plane0, "plane0");
            _rootNode.addChild(_plane0_1, "plane0_1");
            _rootNode.addChild(_plane1, "plane1");
            _rootNode.addChild(_plane1_1, "plane1_1");
            _rootNode.addChild(_plane2, "plane2");
            _plane2.material = getFloorBMP();
            updatePlaneMaterials();
        }
        protected function showTransition(_arg1:Event=null):void{
            this.removeEventListener(Event.ENTER_FRAME, showTransition);
            visible = true;
            _viewStack.visible = false;
        }
        protected function hideTransition():void{
            visible = false;
            _viewStack.visible = true;
        }
        protected function spin():void{
            Tweener.addTween(_rootNode, {rotationY:_targetRotation, time:_spinTime, transition:"easeinoutcubic", onComplete:zoomIn});
        }
        protected function generateMaterial(_arg1:IContainer):BitmapMaterial{
            var _local2:BitmapData = (_arg1) ? new BitmapData(_arg1.width, _arg1.height, true, 0xFFFFFF) : null;
            if (_arg1){
                _local2.draw(_arg1);
            };
            return ((_arg1) ? new BitmapMaterial(_local2) : null);
        }
        protected function getFloorBMP():BitmapMaterial{
            var _local1:Class = Assets.getInstance().floor;
            var _local2:BitmapAsset = BitmapAsset(new (_local1));
            return (new BitmapMaterial(_local2.bitmapData));
        }
        protected function startTransition():void{
            startRendering();
            zoomOut();
            this.addEventListener(Event.ENTER_FRAME, showTransition);
        }
        protected function updatePlanePosition(_arg1:int):void{
            var _local2:Number = (-(_ViewStackWidth) * _arg1);
            _plane1.x = (_local2 * 2);
            _plane1_1.x = _local2;
            _targetRotation = (_arg1 * 90);
            _plane1.rotationY = -(_targetRotation);
            _plane1_1.rotationY = -(_targetRotation);
        }
        protected function zoomIn():void{
            Tweener.addTween(camera, {z:(camera.z + _zoomDistance), time:_zoomInTime, transition:"easeoutcubic", onComplete:transitionFinished});
            Tweener.addTween(_plane1, {x:(-(_ViewStackWidth) * _spinDirection), time:_zoomOutTime, transition:"easeoutcubic"});
        }
        protected function updatePlaneMaterials(_arg1:Number=1):void{
            var _local2:AbstractBalanceModule;
            var _local3:*;
            var _local4:*;
            var _local5:*;
            var _local6:*;
            var _local7:*;
            if (_viewStackChildren[selectedIndex].module != null){
                _local2 = (BalanceRoom(_viewStackChildren[selectedIndex]).module as AbstractBalanceModule);
                _local3 = _local2.transitionContainer1;
                _plane0.material = generateMaterial(_local3);
                _local4 = _viewStackChildren[selectedIndex];
                _local5 = _local2.transitionContainer2;
                _plane0_1.material = generateMaterial(_local5);
            };
            if (_viewStackChildren[_arg1].module != null){
                _local2 = (BalanceRoom(_viewStackChildren[_arg1]).module as AbstractBalanceModule);
                _local6 = _local2.transitionContainer1;
                _plane1.material = generateMaterial(_local6);
                _local7 = _local2.transitionContainer2;
                _plane1_1.material = generateMaterial(_local7);
            };
        }
        protected function transitionFinished():void{
            stopRendering();
            _viewStack.selectedIndex = _selectedIndex;
            hideTransition();
            resetRotation();
            var _local1:Event = new Event(BalanceController.TRANSITION_COMPLETE);
            dispatchEvent(_local1);
            BalanceModelLocator.getInstance().isTransitionActive = false;
            var _local2:AbstractBalanceModule = (BalanceRoom(_viewStackChildren[_selectedIndex]).module as AbstractBalanceModule);
            _local2.removeEventListener(AbstractBalanceModule.BALANCE_MODULE_READY, initialiseTransition);
        }
        public function set selectedIndex(_arg1:int):void{
            var _local2:int;
            var _local3:AbstractBalanceModule;
            if (_selectedIndex != _arg1){
                _nextIndex = _arg1;
                _local2 = (_nextIndex - _selectedIndex);
                if ((_nextIndex - _selectedIndex) > 0){
                    _local2 = -1;
                } else {
                    _local2 = 1;
                };
                _spinDirection = _local2;
                _local3 = (BalanceRoom(_viewStackChildren[_arg1]).module as AbstractBalanceModule);
                if (_local3){
                    _local3.addEventListener(AbstractBalanceModule.BALANCE_MODULE_READY, initialiseTransition);
                };
            };
        }
        public function get selectedIndex():int{
            return (_selectedIndex);
        }
        protected function initialiseTransition(_arg1:Event):void{
            trace("initialiseTransition");
            BalanceModelLocator.getInstance().isTransitionActive = true;
            _timer = new Timer(300, 1);
            _timer.addEventListener(TimerEvent.TIMER_COMPLETE, deferStartTransition);
            _timer.start();
        }
        protected function resetRotation():void{
            _rootNode.rotationY = 0;
            _plane0.z = (_ViewStackWidth * paralax);
        }

    }
}//package com.redbox.changetech.transitions 
