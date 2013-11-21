//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.cameras {
    import flash.display.*;
    import flash.geom.*;
    import flash.text.*;
    import flash.events.*;
    import org.papervision3d.view.*;
    import flash.ui.*;

    public class DebugCamera3D extends Camera3D {

        protected var keyLeft:Boolean = false
        protected var targetRotationX:Number = 0
        protected var targetRotationY:Number = 0
        protected var sideFactor:Number = 0
        protected var _propertiesDisplay:Sprite
        protected var viewport3D:Viewport3D
        protected var fovText:TextField
        protected var xText:TextField
        protected var yText:TextField
        protected var zText:TextField
        protected var startPoint:Point
        protected var startRotationX:Number
        protected var startRotationY:Number
        protected var keyBackward:Boolean = false
        protected var farText:TextField
        protected var keyForward:Boolean = false
        protected var rotationXText:TextField
        protected var rotationZText:TextField
        protected var rotationYText:TextField
        protected var forwardFactor:Number = 0
        protected var nearText:TextField
        protected var viewportStage:Stage
        protected var _inertia:Number = 3
        protected var keyRight:Boolean = false

        public function DebugCamera3D(_arg1:Viewport3D, _arg2:Number=90, _arg3:Number=10, _arg4:Number=5000){
            super(_arg2, _arg3, _arg4, true);
            this.viewport3D = _arg1;
            this.viewport = _arg1.sizeRectangle;
            this.focus = ((this.viewport.height / 2) / Math.tan(((_arg2 / 2) * (Math.PI / 180))));
            this.zoom = (this.focus / _arg3);
            this.focus = _arg3;
            this.far = _arg4;
            displayProperties();
            checkStageReady();
        }
        protected function keyDownHandler(_arg1:KeyboardEvent):void{
            switch (_arg1.keyCode){
                case "W".charCodeAt():
                case Keyboard.UP:
                    keyForward = true;
                    keyBackward = false;
                    break;
                case "S".charCodeAt():
                case Keyboard.DOWN:
                    keyBackward = true;
                    keyForward = false;
                    break;
                case "A".charCodeAt():
                case Keyboard.LEFT:
                    keyLeft = true;
                    keyRight = false;
                    break;
                case "D".charCodeAt():
                case Keyboard.RIGHT:
                    keyRight = true;
                    keyLeft = false;
                    break;
                case "Q".charCodeAt():
                    rotationZ--;
                    break;
                case "E".charCodeAt():
                    rotationZ++;
                    break;
                case "F".charCodeAt():
                    fov--;
                    break;
                case "R".charCodeAt():
                    fov++;
                    break;
                case "G".charCodeAt():
                    near = (near - 10);
                    break;
                case "T".charCodeAt():
                    near = (near + 10);
                    break;
                case "H".charCodeAt():
                    far = (far - 10);
                    break;
                case "Y".charCodeAt():
                    far = (far + 10);
                    break;
            };
        }
        public function set inertia(_arg1:Number):void{
            _inertia = _arg1;
        }
        protected function setupEvents():void{
            viewportStage = viewport3D.containerSprite.stage;
            viewportStage.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
            viewportStage.addEventListener(MouseEvent.MOUSE_UP, mouseUpHandler);
            viewportStage.addEventListener(KeyboardEvent.KEY_DOWN, keyDownHandler);
            viewportStage.addEventListener(KeyboardEvent.KEY_UP, keyUpHandler);
            viewportStage.addEventListener(Event.ENTER_FRAME, onEnterFrameHandler);
        }
        protected function displayProperties():void{
            _propertiesDisplay = new Sprite();
            _propertiesDisplay.graphics.beginFill(0);
            _propertiesDisplay.graphics.drawRect(0, 0, 100, 100);
            _propertiesDisplay.graphics.endFill();
            _propertiesDisplay.x = 0;
            _propertiesDisplay.y = 0;
            var _local1:TextFormat = new TextFormat("_sans", 9);
            xText = new TextField();
            yText = new TextField();
            zText = new TextField();
            rotationXText = new TextField();
            rotationYText = new TextField();
            rotationZText = new TextField();
            fovText = new TextField();
            nearText = new TextField();
            farText = new TextField();
            var _local2:Array = [xText, yText, zText, rotationXText, rotationYText, rotationZText, fovText, nearText, farText];
            var _local3 = 10;
            var _local4:Number = 0;
            while (_local4 < _local2.length) {
                _local2[_local4].width = 100;
                _local2[_local4].selectable = false;
                _local2[_local4].textColor = 0xFFFF00;
                _local2[_local4].text = "";
                _local2[_local4].defaultTextFormat = _local1;
                _local2[_local4].y = (_local3 * _local4);
                _propertiesDisplay.addChild(_local2[_local4]);
                _local4++;
            };
            viewport3D.addChild(_propertiesDisplay);
        }
        protected function onEnterFrameHandler(_arg1:Event):void{
            if (keyForward){
                forwardFactor = (forwardFactor + 50);
            };
            if (keyBackward){
                forwardFactor = (forwardFactor + -50);
            };
            if (keyLeft){
                sideFactor = (sideFactor + -50);
            };
            if (keyRight){
                sideFactor = (sideFactor + 50);
            };
            var _local2:Number = (this.rotationX + ((targetRotationX - this.rotationX) / _inertia));
            var _local3:Number = (this.rotationY + ((targetRotationY - this.rotationY) / _inertia));
            this.rotationX = (Math.round((_local2 * 10)) / 10);
            this.rotationY = (Math.round((_local3 * 10)) / 10);
            forwardFactor = (forwardFactor + ((0 - forwardFactor) / _inertia));
            sideFactor = (sideFactor + ((0 - sideFactor) / _inertia));
            if (forwardFactor > 0){
                this.moveForward(forwardFactor);
            } else {
                this.moveBackward(-(forwardFactor));
            };
            if (sideFactor > 0){
                this.moveRight(sideFactor);
            } else {
                this.moveLeft(-(sideFactor));
            };
            xText.text = ("x:" + int(x));
            yText.text = ("y:" + int(y));
            zText.text = ("z:" + int(z));
            rotationXText.text = ("rotationX:" + int(_local2));
            rotationYText.text = ("rotationY:" + int(_local3));
            rotationZText.text = ("rotationZ:" + int(rotationZ));
            fovText.text = ("fov:" + Math.round(fov));
            nearText.text = ("near:" + Math.round(near));
            farText.text = ("far:" + Math.round(far));
        }
        protected function mouseUpHandler(_arg1:MouseEvent):void{
            viewportStage.removeEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
        }
        protected function keyUpHandler(_arg1:KeyboardEvent):void{
            switch (_arg1.keyCode){
                case "W".charCodeAt():
                case Keyboard.UP:
                    keyForward = false;
                    break;
                case "S".charCodeAt():
                case Keyboard.DOWN:
                    keyBackward = false;
                    break;
                case "A".charCodeAt():
                case Keyboard.LEFT:
                    keyLeft = false;
                    break;
                case "D".charCodeAt():
                case Keyboard.RIGHT:
                    keyRight = false;
                    break;
            };
        }
        public function get propsDisplay():Sprite{
            return (_propertiesDisplay);
        }
        public function get inertia():Number{
            return (_inertia);
        }
        protected function onAddedToStageHandler(_arg1:Event):void{
            setupEvents();
        }
        protected function mouseMoveHandler(_arg1:MouseEvent):void{
            targetRotationY = (startRotationY - ((startPoint.x - viewportStage.mouseX) / 2));
            targetRotationX = (startRotationX + ((startPoint.y - viewportStage.mouseY) / 2));
        }
        protected function mouseDownHandler(_arg1:MouseEvent):void{
            viewportStage.addEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
            startPoint = new Point(viewportStage.mouseX, viewportStage.mouseY);
            startRotationY = this.rotationY;
            startRotationX = this.rotationX;
        }
        public function set propsDisplay(_arg1:Sprite):void{
            _propertiesDisplay = _arg1;
        }
        private function checkStageReady():void{
            if (viewport3D.containerSprite.stage == null){
                viewport3D.containerSprite.addEventListener(Event.ADDED_TO_STAGE, onAddedToStageHandler);
            } else {
                setupEvents();
            };
        }

    }
}//package org.papervision3d.cameras 
