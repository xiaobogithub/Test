//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls {
    import flash.display.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.styles.*;
    import flash.utils.*;

    public class ProgressBar extends UIComponent implements IFontContextComponent {

        private var _direction:String = "right"
        private var stopPolledMode:Boolean = false
        mx_internal var _labelField:IUITextField
        mx_internal var _determinateBar:IFlexDisplayObject
        private var sourceChanged:Boolean = false
        private var _interval:Number = 30
        private var trackSkinChanged:Boolean = false
        mx_internal var _content:UIComponent
        private var _source:Object
        mx_internal var _track:IFlexDisplayObject
        mx_internal var _bar:UIComponent
        mx_internal var _barMask:IFlexDisplayObject
        private var barSkinChanged:Boolean = false
        private var _stringSource:String
        private var _labelPlacement:String = "bottom"
        private var _value:Number = 0
        private var indeterminateChanged:Boolean = true
        private var _mode:String = "event"
        private var stringSourceChanged:Boolean = false
        private var modeChanged:Boolean = false
        private var _conversion:Number = 1
        mx_internal var _indeterminateBar:IFlexDisplayObject
        private var indeterminateSkinChanged:Boolean = false
        private var _indeterminate:Boolean = false
        private var pollTimer:Timer
        private var _minimum:Number = 0
        private var labelOverride:String
        private var _maximum:Number = 0
        private var visibleChanged:Boolean = false
        private var indeterminatePlaying:Boolean = false
        private var _label:String

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ProgressBar(){
            pollTimer = new Timer(_interval);
            cacheAsBitmap = true;
        }
        public function get minimum():Number{
            return (_minimum);
        }
        public function get conversion():Number{
            return (_conversion);
        }
        private function completeHandler(_arg1:Event):void{
            dispatchEvent(_arg1);
            invalidateDisplayList();
        }
        public function get source():Object{
            return (_source);
        }
        public function set minimum(_arg1:Number):void{
            if (((((!(isNaN(_arg1))) && ((_mode == ProgressBarMode.MANUAL)))) && (!((_arg1 == _minimum))))){
                _minimum = _arg1;
                invalidateDisplayList();
                dispatchEvent(new Event("minimumChanged"));
            };
        }
        public function get maximum():Number{
            return (_maximum);
        }
        override protected function createChildren():void{
            var _local1:Class;
            super.createChildren();
            if (!_content){
                _content = new UIComponent();
                addChild(_content);
            };
            if (!_bar){
                _bar = new UIComponent();
                _content.addChild(_bar);
            };
            if (!_barMask){
                if (FlexVersion.compatibilityVersion >= FlexVersion.VERSION_3_0){
                    _local1 = getStyle("maskSkin");
                    _barMask = new (_local1);
                } else {
                    _barMask = new UIComponent();
                };
                _barMask.visible = true;
                _bar.addChild(DisplayObject(_barMask));
                UIComponent(_bar).mask = DisplayObject(_barMask);
            };
            if (!_labelField){
                _labelField = IUITextField(createInFontContext(UITextField));
                _labelField.styleName = this;
                addChild(DisplayObject(_labelField));
            };
        }
        public function set source(_arg1:Object):void{
            var value:* = _arg1;
            if ((value is String)){
                _stringSource = String(value);
                try {
                    value = document[_stringSource];
                } catch(e:Error) {
                    stringSourceChanged = true;
                };
            };
            if (((_source) && ((_source is IEventDispatcher)))){
                removeSourceListeners();
            };
            if (value){
                _source = value;
                sourceChanged = true;
                modeChanged = true;
                indeterminateChanged = true;
                invalidateProperties();
                invalidateDisplayList();
            } else {
                if (_source != null){
                    _source = null;
                    sourceChanged = true;
                    indeterminateChanged = true;
                    invalidateProperties();
                    invalidateDisplayList();
                    pollTimer.reset();
                };
            };
        }
        public function set conversion(_arg1:Number):void{
            if (((((!(isNaN(_arg1))) && ((Number(_arg1) > 0)))) && (!((_arg1 == _conversion))))){
                _conversion = Number(_arg1);
                invalidateDisplayList();
                dispatchEvent(new Event("conversionChanged"));
            };
        }
        public function set maximum(_arg1:Number):void{
            if (((((!(isNaN(_arg1))) && ((_mode == ProgressBarMode.MANUAL)))) && (!((_arg1 == _maximum))))){
                _maximum = _arg1;
                invalidateDisplayList();
                dispatchEvent(new Event("maximumChanged"));
            };
        }
        public function set mode(_arg1:String):void{
            if (_arg1 != _mode){
                if (_mode == ProgressBarMode.POLLED){
                    stopPolledMode = true;
                };
                _mode = _arg1;
                modeChanged = true;
                indeterminateChanged = true;
                invalidateProperties();
                invalidateDisplayList();
            };
        }
        private function removeSourceListeners():void{
            _source.removeEventListener(ProgressEvent.PROGRESS, progressHandler);
            _source.removeEventListener(Event.COMPLETE, completeHandler);
        }
        private function stopPlayingIndeterminate():void{
            if (indeterminatePlaying){
                indeterminatePlaying = false;
                pollTimer.removeEventListener(TimerEvent.TIMER, updateIndeterminateHandler);
                if (_mode != ProgressBarMode.POLLED){
                    pollTimer.reset();
                };
            };
        }
        public function get labelPlacement():String{
            return (_labelPlacement);
        }
        private function progressHandler(_arg1:ProgressEvent):void{
            _setProgress(_arg1.bytesLoaded, _arg1.bytesTotal);
        }
        override protected function measure():void{
            var _local1:Number;
            var _local2:Number;
            super.measure();
            var _local3:Number = NaN;
            var _local4:Number = NaN;
            var _local5:Number = getStyle("trackHeight");
            var _local6:Number = _track.measuredWidth;
            var _local7:Number = (isNaN(_local5)) ? _track.measuredHeight : _local5;
            var _local8:Number = getStyle("horizontalGap");
            var _local9:Number = getStyle("verticalGap");
            var _local10:Number = getStyle("paddingLeft");
            var _local11:Number = getStyle("paddingRight");
            var _local12:Number = getStyle("paddingTop");
            var _local13:Number = getStyle("paddingBottom");
            var _local14:Number = getStyle("labelWidth");
            var _local15:TextLineMetrics = measureText(predictLabelText());
            var _local16:Number = (isNaN(_local14)) ? (_local15.width + UITextField.TEXT_WIDTH_PADDING) : _local14;
            var _local17:Number = (_local15.height + UITextField.TEXT_HEIGHT_PADDING);
            switch (labelPlacement){
                case ProgressBarLabelPlacement.LEFT:
                case ProgressBarLabelPlacement.RIGHT:
                    _local1 = ((((_local16 + _local6) + _local10) + _local11) + _local8);
                    _local2 = ((Math.max(_local17, _local7) + _local12) + _local13);
                    measuredMinWidth = _local1;
                    break;
                case ProgressBarLabelPlacement.CENTER:
                    _local1 = (((Math.max(_local16, _local6) + _local10) + _local11) + _local8);
                    _local2 = ((Math.max(_local17, _local7) + _local12) + _local13);
                    measuredMinWidth = _local16;
                    break;
                default:
                    _local1 = ((Math.max(_local16, _local6) + _local10) + _local11);
                    _local2 = ((((_local17 + _local7) + _local12) + _local13) + _local9);
                    measuredMinWidth = _local16;
                    break;
            };
            measuredWidth = _local1;
            measuredMinHeight = (measuredHeight = _local2);
            if (!isNaN(_local3)){
                measuredMinWidth = _local3;
            };
            if (!isNaN(_local4)){
                measuredMinHeight = _local4;
            };
        }
        public function get fontContext():IFlexModuleFactory{
            return (moduleFactory);
        }
        private function predictLabelText():String{
            var _local2:Number;
            if (label == null){
                return ("");
            };
            var _local1:String = label;
            if (_maximum != 0){
                _local2 = _maximum;
            } else {
                _local2 = 100000;
            };
            if (_local1){
                if (_indeterminate){
                    _local1 = _local1.replace("%1", String(Math.floor((_local2 / _conversion))));
                    _local1 = _local1.replace("%2", "??");
                    _local1 = _local1.replace("%3", "");
                    _local1 = _local1.replace("%%", "");
                } else {
                    _local1 = _local1.replace("%1", String(Math.floor((_local2 / _conversion))));
                    _local1 = _local1.replace("%2", String(Math.floor((_local2 / _conversion))));
                    _local1 = _local1.replace("%3", "100");
                    _local1 = _local1.replace("%%", "%");
                };
            };
            var _local3:String = getFullLabelText();
            if (_local1.length > _local3.length){
                return (_local1);
            };
            return (_local3);
        }
        public function get value():Number{
            return (_value);
        }
        public function set indeterminate(_arg1:Boolean):void{
            _indeterminate = _arg1;
            indeterminateChanged = true;
            invalidateProperties();
            invalidateDisplayList();
            dispatchEvent(new Event("indeterminateChanged"));
        }
        private function createBar():void{
            if (_determinateBar){
                _bar.removeChild(DisplayObject(_determinateBar));
                _determinateBar = null;
            };
            var _local1:Class = getStyle("barSkin");
            if (_local1){
                _determinateBar = new (_local1);
                if ((_determinateBar is ISimpleStyleClient)){
                    ISimpleStyleClient(_determinateBar).styleName = this;
                };
                _bar.addChild(DisplayObject(_determinateBar));
            };
        }
        private function createIndeterminateBar():void{
            if (_indeterminateBar){
                _bar.removeChild(DisplayObject(_indeterminateBar));
                _indeterminateBar = null;
            };
            var _local1:Class = getStyle("indeterminateSkin");
            if (_local1){
                _indeterminateBar = new (_local1);
                if ((_indeterminateBar is ISimpleStyleClient)){
                    ISimpleStyleClient(_indeterminateBar).styleName = this;
                };
                _indeterminateBar.visible = false;
                _bar.addChild(DisplayObject(_indeterminateBar));
            };
        }
        public function get direction():String{
            return (_direction);
        }
        private function addSourceListeners():void{
            _source.addEventListener(ProgressEvent.PROGRESS, progressHandler);
            _source.addEventListener(Event.COMPLETE, completeHandler);
        }
        private function updateIndeterminateHandler(_arg1:Event):void{
            if (_indeterminateBar.x < 1){
                _indeterminateBar.x = (_indeterminateBar.x + 1);
            } else {
                _indeterminateBar.x = -((getStyle("indeterminateMoveInterval") - 2));
            };
        }
        private function updatePolledHandler(_arg1:Event):void{
            var _local2:Object;
            var _local3:Number;
            var _local4:Number;
            if (_source){
                _local2 = _source;
                _local3 = _local2.bytesLoaded;
                _local4 = _local2.bytesTotal;
                if (((!(isNaN(_local3))) && (!(isNaN(_local4))))){
                    _setProgress(_local3, _local4);
                    if ((((percentComplete >= 100)) && ((_value > 0)))){
                        pollTimer.reset();
                    };
                };
            };
        }
        public function set labelPlacement(_arg1:String):void{
            if (_arg1 != _labelPlacement){
                _labelPlacement = _arg1;
            };
            invalidateSize();
            invalidateDisplayList();
            dispatchEvent(new Event("labelPlacementChanged"));
        }
        public function get mode():String{
            return (_mode);
        }
        public function get percentComplete():Number{
            if ((((_value < _minimum)) || ((_maximum < _minimum)))){
                return (0);
            };
            if ((_maximum - _minimum) == 0){
                return (0);
            };
            var _local1:Number = ((100 * (_value - _minimum)) / (_maximum - _minimum));
            if (((isNaN(_local1)) || ((_local1 < 0)))){
                return (0);
            };
            if (_local1 > 100){
                return (100);
            };
            return (_local1);
        }
        public function setProgress(_arg1:Number, _arg2:Number):void{
            if (_mode == ProgressBarMode.MANUAL){
                _setProgress(_arg1, _arg2);
            };
        }
        private function createTrack():void{
            if (_track){
                _content.removeChild(DisplayObject(_track));
                _track = null;
            };
            var _local1:Class = getStyle("trackSkin");
            if (_local1){
                _track = new (_local1);
                if ((_track is ISimpleStyleClient)){
                    ISimpleStyleClient(_track).styleName = this;
                };
                _content.addChildAt(DisplayObject(_track), 0);
            };
        }
        public function get indeterminate():Boolean{
            return (_indeterminate);
        }
        private function startPlayingIndeterminate():void{
            if (!indeterminatePlaying){
                indeterminatePlaying = true;
                pollTimer.addEventListener(TimerEvent.TIMER, updateIndeterminateHandler, false, 0, true);
                pollTimer.start();
            };
        }
        override public function styleChanged(_arg1:String):void{
            var _local2:Boolean;
            super.styleChanged(_arg1);
            if ((((_arg1 == null)) || ((_arg1 == "styleName")))){
                barSkinChanged = (trackSkinChanged = (indeterminateSkinChanged = true));
                _local2 = true;
            } else {
                if (_arg1 == "barSkin"){
                    barSkinChanged = true;
                    _local2 = true;
                } else {
                    if (_arg1 == "trackSkin"){
                        trackSkinChanged = true;
                        _local2 = true;
                    } else {
                        if (_arg1 == "indeterminateSkin"){
                            indeterminateSkinChanged = true;
                            _local2 = true;
                        };
                    };
                };
            };
            if (_local2){
                invalidateProperties();
                invalidateSize();
                invalidateDisplayList();
            };
        }
        private function getFullLabelText():String{
            var _local1:Number = Math.max(_value, 0);
            var _local2:Number = Math.max(_maximum, 0);
            var _local3:String = label;
            if (_local3){
                if (_indeterminate){
                    _local3 = _local3.replace("%1", String(Math.floor((_local1 / _conversion))));
                    _local3 = _local3.replace("%2", "??");
                    _local3 = _local3.replace("%3", "");
                    _local3 = _local3.replace("%%", "");
                } else {
                    _local3 = _local3.replace("%1", String(Math.floor((_local1 / _conversion))));
                    _local3 = _local3.replace("%2", String(Math.floor((_local2 / _conversion))));
                    _local3 = _local3.replace("%3", String(Math.floor(percentComplete)));
                    _local3 = _local3.replace("%%", "%");
                };
            };
            return (_local3);
        }
        override protected function commitProperties():void{
            var index:* = 0;
            super.commitProperties();
            if (((hasFontContextChanged()) && (!((_labelField == null))))){
                index = getChildIndex(DisplayObject(_labelField));
                removeChild(DisplayObject(_labelField));
                _labelField = IUITextField(createInFontContext(UITextField));
                _labelField.styleName = this;
                addChildAt(DisplayObject(_labelField), index);
            };
            if (trackSkinChanged){
                trackSkinChanged = false;
                createTrack();
            };
            if (barSkinChanged){
                barSkinChanged = false;
                createBar();
            };
            if (indeterminateSkinChanged){
                indeterminateSkinChanged = false;
                createIndeterminateBar();
            };
            if (stringSourceChanged){
                stringSourceChanged = false;
                try {
                    _source = document[_stringSource];
                } catch(e:Error) {
                };
            };
            if (sourceChanged){
                sourceChanged = false;
                dispatchEvent(new Event("sourceChanged"));
            };
            if (modeChanged){
                modeChanged = false;
                if (_source){
                    if (_mode == ProgressBarMode.EVENT){
                        if ((_source is IEventDispatcher)){
                            addSourceListeners();
                        } else {
                            _source = null;
                        };
                    } else {
                        if ((_source is IEventDispatcher)){
                            removeSourceListeners();
                        };
                    };
                };
                if (_mode == ProgressBarMode.POLLED){
                    pollTimer.addEventListener(TimerEvent.TIMER, updatePolledHandler, false, 0, true);
                    pollTimer.start();
                } else {
                    if (stopPolledMode){
                        stopPolledMode = false;
                        pollTimer.removeEventListener(TimerEvent.TIMER, updatePolledHandler);
                        pollTimer.reset();
                    };
                };
                dispatchEvent(new Event("modeChanged"));
            };
        }
        override protected function resourcesChanged():void{
            super.resourcesChanged();
            label = labelOverride;
        }
        public function set fontContext(_arg1:IFlexModuleFactory):void{
            this.moduleFactory = _arg1;
        }
        override public function set visible(_arg1:Boolean):void{
            super.visible = _arg1;
            visibleChanged = true;
            invalidateDisplayList();
        }
        public function set label(_arg1:String):void{
            labelOverride = _arg1;
            _label = ((_arg1)!=null) ? _arg1 : resourceManager.getString("controls", "label");
            invalidateDisplayList();
            dispatchEvent(new Event("labelChanged"));
        }
        override protected function childrenCreated():void{
            super.childrenCreated();
            trackSkinChanged = true;
            barSkinChanged = true;
            indeterminateSkinChanged = true;
        }
        private function layoutContent(_arg1:Number, _arg2:Number):void{
            _track.move(0, 0);
            _track.setActualSize(_arg1, _arg2);
            _bar.move(0, 0);
            _determinateBar.move(0, 0);
            _indeterminateBar.setActualSize((_arg1 + getStyle("indeterminateMoveInterval")), _arg2);
        }
        private function _setProgress(_arg1:Number, _arg2:Number):void{
            var _local3:ProgressEvent;
            if (((((enabled) && (!(isNaN(_arg1))))) && (!(isNaN(_arg2))))){
                _value = _arg1;
                _maximum = _arg2;
                dispatchEvent(new Event(Event.CHANGE));
                _local3 = new ProgressEvent(ProgressEvent.PROGRESS);
                _local3.bytesLoaded = _arg1;
                _local3.bytesTotal = _arg2;
                dispatchEvent(_local3);
                if (_indeterminate){
                    startPlayingIndeterminate();
                };
                if ((((_value == _maximum)) && ((_value > 0)))){
                    if (_indeterminate){
                        stopPlayingIndeterminate();
                    };
                    if (mode != ProgressBarMode.EVENT){
                        dispatchEvent(new Event(Event.COMPLETE));
                    };
                };
                invalidateDisplayList();
            };
        }
        public function get label():String{
            return (_label);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local16:Number;
            var _local17:Graphics;
            var _local18:Number;
            super.updateDisplayList(_arg1, _arg2);
            var _local3:Number = getStyle("horizontalGap");
            var _local4:Number = getStyle("verticalGap");
            var _local5:Number = getStyle("paddingLeft");
            var _local6:Number = getStyle("paddingRight");
            var _local7:Number = getStyle("paddingTop");
            var _local8:Number = getStyle("paddingBottom");
            var _local9:Number = _local5;
            var _local10:Number = _local7;
            var _local11:Number = getStyle("labelWidth");
            var _local12:Number = getStyle("trackHeight");
            _local12 = (isNaN(_local12)) ? _track.measuredHeight : _local12;
            var _local13:TextLineMetrics = measureText(predictLabelText());
            var _local14:Number = (isNaN(_local11)) ? (_local13.width + UITextField.TEXT_WIDTH_PADDING) : _local11;
            var _local15:Number = (_local13.height + UITextField.TEXT_HEIGHT_PADDING);
            switch (labelPlacement){
                case ProgressBarLabelPlacement.TOP:
                    _labelField.move(_local9, _local10);
                    _labelField.setActualSize(_local14, _local15);
                    _content.move(_local9, ((_local10 + _local15) + _local4));
                    layoutContent(((_arg1 - _local9) - _local6), _local12);
                    break;
                case ProgressBarLabelPlacement.RIGHT:
                    _local16 = ((((_arg1 - _local9) - _local6) - _local14) - _local3);
                    _labelField.move(((_local9 + _local16) + _local3), ((_arg2 - _local15) / 2));
                    _labelField.setActualSize(_local14, _local15);
                    _content.move(_local9, (_local10 + ((_local15 - _local12) / 2)));
                    layoutContent(_local16, _local12);
                    break;
                case ProgressBarLabelPlacement.LEFT:
                    _labelField.move(_local9, (_local10 + ((_arg2 - _local15) / 2)));
                    _labelField.setActualSize(_local14, _local15);
                    _content.move(((_local9 + _local14) + _local3), (_local10 + ((_local15 - _local12) / 2)));
                    layoutContent(((((_arg1 - _local9) - _local14) - _local4) - _local6), _local12);
                    break;
                case ProgressBarLabelPlacement.CENTER:
                    _labelField.move(((_arg1 - _local14) / 2), ((_arg2 - _local15) / 2));
                    _labelField.setActualSize(_local14, _local15);
                    _content.move(_local9, _local10);
                    layoutContent((_arg1 - _local6), (_arg2 - _local8));
                    break;
                default:
                    _labelField.move(_local9, ((_local10 + _local12) + _local4));
                    _labelField.setActualSize(_local14, _local15);
                    _content.move(_local9, _local10);
                    layoutContent(((_arg1 - _local9) - _local6), _local12);
                    break;
            };
            if (_barMask){
                _barMask.move(0, 0);
                if (FlexVersion.compatibilityVersion >= FlexVersion.VERSION_3_0){
                    _barMask.setActualSize(_track.width, _track.height);
                } else {
                    _local17 = UIComponent(_barMask).graphics;
                    _local17.clear();
                    _local17.beginFill(0xFFFF00);
                    _local17.drawRect(1, 1, (_track.width - 2), (_track.height - 2));
                    _local17.endFill();
                };
            };
            _labelField.text = getFullLabelText();
            _indeterminateBar.visible = _indeterminate;
            if (((indeterminateChanged) || (visibleChanged))){
                indeterminateChanged = false;
                visibleChanged = false;
                _indeterminateBar.visible = _indeterminate;
                if (((((((_indeterminate) && ((_source == null)))) && ((_mode == ProgressBarMode.EVENT)))) && (visible))){
                    startPlayingIndeterminate();
                } else {
                    stopPlayingIndeterminate();
                };
            };
            if (_indeterminate){
                _determinateBar.setActualSize(_track.width, _track.height);
            } else {
                _local18 = Math.max(0, ((_track.width * percentComplete) / 100));
                _determinateBar.setActualSize(_local18, _track.height);
                _determinateBar.x = ((direction == ProgressBarDirection.RIGHT)) ? 0 : (_track.width - _local18);
            };
        }
        public function set direction(_arg1:String):void{
            if ((((_arg1 == ProgressBarDirection.LEFT)) || ((_arg1 == ProgressBarDirection.RIGHT)))){
                _direction = _arg1;
            };
            invalidateDisplayList();
            dispatchEvent(new Event("directionChanged"));
        }

    }
}//package mx.controls 
