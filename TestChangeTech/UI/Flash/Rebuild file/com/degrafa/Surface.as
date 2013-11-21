//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa {
    import flash.display.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import com.degrafa.core.collections.*;

    public class Surface extends UIComponent {

        private var _strokes:StrokeCollection
        private var _fills:FillCollection
        private var _suppressEventProcessing:Boolean = false
        private var _enableEvents:Boolean = true
        private var _graphicsData:GraphicsCollection

        override public function dispatchEvent(_arg1:Event):Boolean{
            if (_suppressEventProcessing){
                return (false);
            };
            return (super.dispatchEvent(_arg1));
        }
        public function get fillCollection():FillCollection{
            initFillsCollection();
            return (_fills);
        }
        public function get strokeCollection():StrokeCollection{
            initSrokesCollection();
            return (_strokes);
        }
        public function dispatchPropertyChange(_arg1:Boolean=false, _arg2:Object=null, _arg3:Object=null, _arg4:Object=null, _arg5:Object=null):Boolean{
            return (dispatchEvent(new PropertyChangeEvent("propertyChange", _arg1, false, PropertyChangeEventKind.UPDATE, _arg2, _arg3, _arg4, _arg5)));
        }
        public function get graphicsCollection():GraphicsCollection{
            initGraphicsCollection();
            return (_graphicsData);
        }
        public function get fills():Array{
            initFillsCollection();
            return (_fills.items);
        }
        public function get graphicsData():Array{
            initGraphicsCollection();
            return (_graphicsData.items);
        }
        private function initSrokesCollection():void{
            if (!_strokes){
                _strokes = new StrokeCollection();
                if (enableEvents){
                    _strokes.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function get suppressEventProcessing():Boolean{
            return (_suppressEventProcessing);
        }
        private function initFillsCollection():void{
            if (!_fills){
                _fills = new FillCollection();
                if (enableEvents){
                    _fills.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function set fills(_arg1:Array):void{
            initFillsCollection();
            _fills.items = _arg1;
        }
        private function propertyChangeHandler(_arg1:PropertyChangeEvent):void{
            dispatchEvent(_arg1);
        }
        public function initChange(_arg1:String, _arg2:Object, _arg3:Object, _arg4:Object):void{
            if (hasEventManager){
                dispatchPropertyChange(false, _arg1, _arg2, _arg3, _arg4);
            };
        }
        public function set graphicsData(_arg1:Array):void{
            var _local2:IGraphic;
            initGraphicsCollection();
            _graphicsData.items = _arg1;
            for each (_local2 in _graphicsData.items) {
                addChild(DisplayObject(_local2));
                if (_local2.target == null){
                    _local2.target = this;
                };
            };
        }
        public function get hasEventManager():Boolean{
            return (true);
        }
        public function set suppressEventProcessing(_arg1:Boolean):void{
            if ((((_suppressEventProcessing == true)) && ((_arg1 == false)))){
                _suppressEventProcessing = _arg1;
                initChange("suppressEventProcessing", false, true, this);
            } else {
                _suppressEventProcessing = _arg1;
            };
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            setActualSize(getExplicitOrMeasuredWidth(), getExplicitOrMeasuredHeight());
        }
        private function initGraphicsCollection():void{
            if (!_graphicsData){
                _graphicsData = new GraphicsCollection();
                if (enableEvents){
                    _graphicsData.addEventListener(PropertyChangeEvent.PROPERTY_CHANGE, propertyChangeHandler);
                };
            };
        }
        public function get strokes():Array{
            initSrokesCollection();
            return (_strokes.items);
        }
        public function get enableEvents():Boolean{
            return (_enableEvents);
        }
        public function set strokes(_arg1:Array):void{
            initSrokesCollection();
            _strokes.items = _arg1;
        }
        public function set enableEvents(_arg1:Boolean):void{
            _enableEvents = _arg1;
        }

    }
}//package com.degrafa 
