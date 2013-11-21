//Created by Action Script Viewer - http://www.buraks.com/asv
package flexlib.controls {
    import mx.core.*;
    import mx.controls.*;
    import mx.containers.*;

    public class CanvasButton extends Button implements IContainer {

        private var _childDescriptors:Array
        private var _childrenCreated:Boolean = false
        protected var _viewMetrics:EdgeMetrics
        private var _horizontalScrollPolicy:String = "auto"
        protected var _horizontalScrollPosition:Number
        protected var _creatingContentPane:Boolean
        protected var _verticalScrollPosition:Number
        private var canvas:Canvas
        private var _verticalScrollPolicy:String = "auto"
        protected var _defaultButton:IFlexDisplayObject

        public function CanvasButton():void{
        }
        override public function get buttonMode():Boolean{
            return (super.buttonMode);
        }
        public function set creatingContentPane(_arg1:Boolean):void{
            this._creatingContentPane = _arg1;
        }
        override public function set mouseChildren(_arg1:Boolean):void{
            super.mouseChildren = _arg1;
            if (canvas){
                canvas.mouseChildren = _arg1;
            };
        }
        override protected function createChildren():void{
            super.createChildren();
            canvas = new Canvas();
            canvas.verticalScrollPolicy = _verticalScrollPolicy;
            canvas.horizontalScrollPolicy = _horizontalScrollPolicy;
            canvas.mouseChildren = super.mouseChildren;
            canvas.buttonMode = super.buttonMode;
            super.addChild(canvas);
            canvas.initializeRepeaterArrays(this);
            createComponents();
            _childrenCreated = true;
        }
        public function get creatingContentPane():Boolean{
            return (this._creatingContentPane);
        }
        public function get horizontalScrollPolicy():String{
            return (_horizontalScrollPolicy);
        }
        public function set defaultButton(_arg1:IFlexDisplayObject):void{
            this._defaultButton = _arg1;
        }
        public function get verticalScrollPosition():Number{
            return (this._verticalScrollPosition);
        }
        public function set horizontalScrollPosition(_arg1:Number):void{
            this._horizontalScrollPosition = _arg1;
        }
        public function set viewMetrics(_arg1:EdgeMetrics):void{
            this._viewMetrics = _arg1;
        }
        private function createComponents():void{
            var _local1:UIComponentDescriptor;
            for each (_local1 in _childDescriptors) {
                canvas.createComponentFromDescriptor(_local1, true);
            };
        }
        public function set horizontalScrollPolicy(_arg1:String):void{
            _horizontalScrollPolicy = _arg1;
            if (canvas){
                canvas.horizontalScrollPolicy = _arg1;
            };
        }
        public function set childDescriptors(_arg1:Array):void{
            _childDescriptors = _arg1;
        }
        override public function get mouseChildren():Boolean{
            return (super.mouseChildren);
        }
        public function get defaultButton():IFlexDisplayObject{
            return (this._defaultButton);
        }
        override protected function measure():void{
            super.measure();
            measuredHeight = canvas.getExplicitOrMeasuredHeight();
            measuredWidth = canvas.getExplicitOrMeasuredWidth();
        }
        public function get horizontalScrollPosition():Number{
            return (this._horizontalScrollPosition);
        }
        override mx_internal function layoutContents(_arg1:Number, _arg2:Number, _arg3:Boolean):void{
            super.layoutContents(_arg1, _arg2, _arg3);
            setChildIndex(canvas, (numChildren - 1));
        }
        public function get viewMetrics():EdgeMetrics{
            return (this._viewMetrics);
        }
        public function set verticalScrollPosition(_arg1:Number):void{
            this._verticalScrollPosition = _arg1;
        }
        public function set verticalScrollPolicy(_arg1:String):void{
            _verticalScrollPolicy = _arg1;
            if (canvas){
                canvas.verticalScrollPolicy = _arg1;
            };
        }
        mx_internal function setDocumentDescriptor(_arg1:UIComponentDescriptor):void{
            if (((_documentDescriptor) && (_documentDescriptor.properties.childDescriptors))){
                if (_arg1.properties.childDescriptors){
                    throw (new Error("Multiple sets of visual children have been specified for this component (base component definition and derived component definition)."));
                };
            } else {
                _documentDescriptor = _arg1;
                _documentDescriptor.document = this;
            };
            if (_arg1.properties.childDescriptors){
                this.childDescriptors = _arg1.properties.childDescriptors;
            };
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            canvas.setActualSize(_arg1, _arg2);
        }
        override public function set buttonMode(_arg1:Boolean):void{
            super.buttonMode = _arg1;
            if (canvas){
                canvas.buttonMode = _arg1;
            };
        }
        public function get verticalScrollPolicy():String{
            return (_verticalScrollPolicy);
        }

    }
}//package flexlib.controls 
