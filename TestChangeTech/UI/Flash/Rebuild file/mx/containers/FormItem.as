//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers {
    import flash.display.*;
    import mx.core.*;
    import flash.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.containers.utilityClasses.*;

    public class FormItem extends Container {

        private var _direction:String = "vertical"
        private var guessedNumColumns:int
        private var _required:Boolean = false
        mx_internal var verticalLayoutObject:BoxLayout
        private var labelChanged:Boolean = false
        private var guessedRowWidth:Number
        private var labelObj:Label
        private var numberOfGuesses:int = 0
        private var indicatorObj:IFlexDisplayObject
        private var _label:String = ""

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function FormItem(){
            verticalLayoutObject = new BoxLayout();
            super();
            _horizontalScrollPolicy = ScrollPolicy.OFF;
            _verticalScrollPolicy = ScrollPolicy.OFF;
            verticalLayoutObject.target = this;
            verticalLayoutObject.direction = BoxDirection.VERTICAL;
        }
        override public function styleChanged(_arg1:String):void{
            var _local3:String;
            var _local4:CSSStyleDeclaration;
            super.styleChanged(_arg1);
            var _local2:Boolean = (((_arg1 == null)) || ((_arg1 == "styleName")));
            if (((_local2) || ((_arg1 == "labelStyleName")))){
                if (labelObj){
                    _local3 = getStyle("labelStyleName");
                    if (_local3){
                        _local4 = StyleManager.getStyleDeclaration(("." + _local3));
                        if (_local4){
                            labelObj.styleDeclaration = _local4;
                            labelObj.regenerateStyleCache(true);
                        };
                    };
                };
            };
        }
        mx_internal function getHorizontalAlignValue():Number{
            var _local1:String = getStyle("horizontalAlign");
            if (_local1 == "center"){
                return (0.5);
            };
            if (_local1 == "right"){
                return (1);
            };
            return (0);
        }
        private function calcNumColumns(_arg1:Number):int{
            var _local7:IUIComponent;
            var _local8:Number;
            var _local2:Number = 0;
            var _local3:Number = 0;
            var _local4:Number = getStyle("horizontalGap");
            if (direction != FormItemDirection.HORIZONTAL){
                return (1);
            };
            var _local5:Number = numChildren;
            var _local6:int;
            while (_local6 < numChildren) {
                _local7 = IUIComponent(getChildAt(_local6));
                if (!_local7.includeInLayout){
                    _local5--;
                } else {
                    _local8 = _local7.getExplicitOrMeasuredWidth();
                    _local3 = Math.max(_local3, _local8);
                    _local2 = (_local2 + _local8);
                    if (_local6 > 0){
                        _local2 = (_local2 + _local4);
                    };
                };
                _local6++;
            };
            if (((isNaN(_arg1)) || ((_local2 <= _arg1)))){
                return (_local5);
            };
            if ((_local3 * 2) <= _arg1){
                return (2);
            };
            return (1);
        }
        override protected function commitProperties():void{
            super.commitProperties();
            if (labelChanged){
                labelObj.text = label;
                labelObj.validateSize();
                labelChanged = false;
            };
        }
        mx_internal function get labelObject():Object{
            return (labelObj);
        }
        private function previousUpdateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local10:int;
            var _local11:IUIComponent;
            var _local12:Number;
            var _local13:Number;
            var _local14:Number;
            var _local15:Number;
            var _local16:Number;
            var _local19:Number;
            var _local20:int;
            var _local21:Number;
            var _local22:int;
            var _local23:Number;
            var _local24:Number;
            var _local25:Number;
            var _local26:Number;
            super.updateDisplayList(_arg1, _arg2);
            var _local3:EdgeMetrics = viewMetricsAndPadding;
            var _local4:Number = _local3.left;
            var _local5:Number = _local3.top;
            var _local6:Number = _local5;
            var _local7:Number = calculateLabelWidth();
            var _local8:Number = getStyle("indicatorGap");
            var _local9:String = getStyle("horizontalAlign");
            if (_local9 == "right"){
                _local16 = 1;
            } else {
                if (_local9 == "center"){
                    _local16 = 0.5;
                } else {
                    _local16 = 0;
                };
            };
            var _local17:int = numChildren;
            if (_local17 > 0){
                _local11 = IUIComponent(getChildAt(0));
                _local12 = _local11.baselinePosition;
                if (!isNaN(_local12)){
                    _local6 = (_local6 + (_local12 - labelObj.baselinePosition));
                };
            };
            labelObj.setActualSize(_local7, labelObj.getExplicitOrMeasuredHeight());
            labelObj.move(_local4, _local6);
            _local4 = (_local4 + _local7);
            displayIndicator(_local4, _local6);
            _local4 = (_local4 + _local8);
            var _local18:Number = ((_arg1 - _local3.right) - _local4);
            if (_local18 < 0){
                _local18 = 0;
            };
            if (direction == FormItemDirection.HORIZONTAL){
                _local19 = 0;
                _local20 = calcNumColumns(_local18);
                _local22 = 0;
                _local14 = getStyle("horizontalGap");
                _local15 = getStyle("verticalGap");
                if (((!((_local20 == guessedNumColumns))) && ((numberOfGuesses == 0)))){
                    guessedRowWidth = _local18;
                    numberOfGuesses = 1;
                    invalidateSize();
                } else {
                    numberOfGuesses = 0;
                };
                if (_local20 == _local17){
                    _local23 = (height - (_local5 + _local3.bottom));
                    _local24 = Flex.flexChildWidthsProportionally(this, (_local18 - ((_local17 - 1) * _local14)), _local23);
                    _local4 = (_local4 + (_local24 * _local16));
                    _local10 = 0;
                    while (_local10 < _local17) {
                        _local11 = IUIComponent(getChildAt(_local10));
                        _local11.move(Math.floor(_local4), _local5);
                        _local4 = (_local4 + (_local11.width + _local14));
                        _local10++;
                    };
                } else {
                    _local10 = 0;
                    while (_local10 < _local17) {
                        _local11 = IUIComponent(getChildAt(_local10));
                        _local19 = Math.max(_local19, _local11.getExplicitOrMeasuredWidth());
                        _local10++;
                    };
                    _local25 = (_local18 - ((_local20 * _local19) + ((_local20 - 1) * _local14)));
                    if (_local25 < 0){
                        _local25 = 0;
                    };
                    _local4 = (_local4 + (_local25 * _local16));
                    _local21 = _local4;
                    _local10 = 0;
                    while (_local10 < _local17) {
                        _local11 = IUIComponent(getChildAt(_local10));
                        _local13 = Math.min(_local19, _local11.getExplicitOrMeasuredWidth());
                        _local11.setActualSize(_local13, _local11.getExplicitOrMeasuredHeight());
                        _local11.move(_local21, _local5);
                        ++_local22;
                        if (_local22 >= _local20){
                            _local21 = _local4;
                            _local22 = 0;
                            _local5 = (_local5 + (_local11.height + _local15));
                        } else {
                            _local21 = (_local21 + (_local19 + _local14));
                        };
                        _local10++;
                    };
                };
            } else {
                _local15 = getStyle("verticalGap");
                _local10 = 0;
                while (_local10 < _local17) {
                    _local11 = IUIComponent(getChildAt(_local10));
                    if (!isNaN(_local11.percentWidth)){
                        _local13 = Math.floor(((_local18 * Math.min(_local11.percentWidth, 100)) / 100));
                    } else {
                        _local13 = _local11.getExplicitOrMeasuredWidth();
                        if (isNaN(_local11.explicitWidth)){
                            if (_local13 < Math.floor((_local18 * 0.25))){
                                _local13 = Math.floor((_local18 * 0.25));
                            } else {
                                if (_local13 < Math.floor((_local18 * 0.5))){
                                    _local13 = Math.floor((_local18 * 0.5));
                                } else {
                                    if (_local13 < Math.floor((_local18 * 0.75))){
                                        _local13 = Math.floor((_local18 * 0.75));
                                    } else {
                                        if (_local13 < Math.floor(_local18)){
                                            _local13 = Math.floor(_local18);
                                        };
                                    };
                                };
                            };
                        };
                    };
                    _local11.setActualSize(_local13, _local11.getExplicitOrMeasuredHeight());
                    _local26 = ((_local18 - _local13) * _local16);
                    _local11.move((_local4 + _local26), _local5);
                    _local5 = (_local5 + _local11.height);
                    _local5 = (_local5 + _local15);
                    _local10++;
                };
            };
            _local6 = _local3.top;
            if (_local17 > 0){
                _local11 = IUIComponent(getChildAt(0));
                _local12 = _local11.baselinePosition;
                if (!isNaN(_local12)){
                    _local6 = (_local6 + (_local12 - labelObj.baselinePosition));
                };
            };
            labelObj.move(labelObj.x, _local6);
        }
        override protected function createChildren():void{
            var _local1:String;
            var _local2:CSSStyleDeclaration;
            super.createChildren();
            if (!labelObj){
                labelObj = new FormItemLabel();
                _local1 = getStyle("labelStyleName");
                if (_local1){
                    _local2 = StyleManager.getStyleDeclaration(("." + _local1));
                    if (_local2){
                        labelObj.styleDeclaration = _local2;
                    };
                };
                rawChildren.addChild(labelObj);
                dispatchEvent(new Event("itemLabelChanged"));
            };
        }
        function getPreferredLabelWidth():Number{
            if (((!(label)) || ((label == "")))){
                return (0);
            };
            if (isNaN(labelObj.measuredWidth)){
                labelObj.validateSize();
            };
            var _local1:Number = labelObj.measuredWidth;
            if (isNaN(_local1)){
                return (0);
            };
            return (_local1);
        }
        override protected function measure():void{
            if (FlexVersion.compatibilityVersion < FlexVersion.VERSION_3_0){
                previousMeasure();
                return;
            };
            super.measure();
            if (direction == FormItemDirection.VERTICAL){
                measureVertical();
            } else {
                measureHorizontal();
            };
        }
        public function get itemLabel():Label{
            return (labelObj);
        }
        public function get required():Boolean{
            return (_required);
        }
        private function previousMeasure():void{
            var _local12:Number;
            var _local13:Number;
            var _local16:int;
            var _local17:IUIComponent;
            super.measure();
            var _local1:int = (guessedNumColumns = calcNumColumns(guessedRowWidth));
            var _local2:Number = getStyle("horizontalGap");
            var _local3:Number = getStyle("verticalGap");
            var _local4:Number = getStyle("indicatorGap");
            var _local5:int;
            var _local6:Number = 0;
            var _local7:Number = 0;
            var _local8:Number = 0;
            var _local9:Number = 0;
            var _local10:Number = 0;
            var _local11:Number = 0;
            _local12 = 0;
            _local13 = 0;
            var _local14:Number = 0;
            var _local15:int = numChildren;
            if ((((direction == FormItemDirection.HORIZONTAL)) && ((_local1 < _local15)))){
                _local16 = 0;
                while (_local16 < _local15) {
                    _local17 = IUIComponent(getChildAt(_local16));
                    _local14 = Math.max(_local14, _local17.getExplicitOrMeasuredWidth());
                    _local16++;
                };
            };
            _local16 = 0;
            while (_local16 < _local15) {
                _local17 = IUIComponent(getChildAt(_local16));
                if (_local5 < _local1){
                    _local6 = (_local6 + (isNaN(_local17.percentWidth)) ? _local17.getExplicitOrMeasuredWidth() : _local17.minWidth);
                    _local7 = (_local7 + ((_local14)>0) ? _local14 : _local17.getExplicitOrMeasuredWidth());
                    if (_local5 > 0){
                        _local6 = (_local6 + _local2);
                        _local7 = (_local7 + _local2);
                    };
                    _local8 = Math.max(_local8, (isNaN(_local17.percentHeight)) ? _local17.getExplicitOrMeasuredHeight() : _local17.minHeight);
                    _local9 = Math.max(_local9, _local17.getExplicitOrMeasuredHeight());
                };
                _local5++;
                if ((((_local5 >= _local1)) || ((_local16 == (_local15 - 1))))){
                    _local10 = Math.max(_local10, _local6);
                    _local12 = Math.max(_local12, _local7);
                    _local11 = (_local11 + _local8);
                    _local13 = (_local13 + _local9);
                    if (_local16 > 0){
                        _local11 = (_local11 + _local3);
                        _local13 = (_local13 + _local3);
                    };
                    _local5 = 0;
                    _local6 = 0;
                    _local7 = 0;
                    _local8 = 0;
                    _local9 = 0;
                };
                _local16++;
            };
            var _local18:Number = (calculateLabelWidth() + _local4);
            _local10 = (_local10 + _local18);
            _local12 = (_local12 + _local18);
            if (((!((label == null))) && (!((label == ""))))){
                _local11 = Math.max(_local11, labelObj.getExplicitOrMeasuredHeight());
                _local13 = Math.max(_local13, labelObj.getExplicitOrMeasuredHeight());
            };
            var _local19:EdgeMetrics = viewMetricsAndPadding;
            _local11 = (_local11 + (_local19.top + _local19.bottom));
            _local10 = (_local10 + (_local19.left + _local19.right));
            _local13 = (_local13 + (_local19.top + _local19.bottom));
            _local12 = (_local12 + (_local19.left + _local19.right));
            measuredMinWidth = _local10;
            measuredMinHeight = _local11;
            measuredWidth = _local12;
            measuredHeight = _local13;
        }
        private function measureHorizontal():void{
            var _local10:Number;
            var _local12:Number;
            var _local14:int;
            var _local16:IUIComponent;
            var _local1:int = (guessedNumColumns = calcNumColumns(guessedRowWidth));
            var _local2:Number = getStyle("horizontalGap");
            var _local3:Number = getStyle("verticalGap");
            var _local4:Number = getStyle("indicatorGap");
            var _local5:Number = 0;
            var _local6:Number = 0;
            var _local7:Number = 0;
            var _local8:Number = 0;
            var _local9:Number = 0;
            _local10 = 0;
            var _local11:Number = 0;
            _local12 = 0;
            var _local13:Number = 0;
            var _local15:int;
            if (_local1 < numChildren){
                _local14 = 0;
                while (_local14 < numChildren) {
                    _local16 = IUIComponent(getChildAt(_local14));
                    if (!_local16.includeInLayout){
                    } else {
                        _local13 = Math.max(_local13, _local16.getExplicitOrMeasuredWidth());
                    };
                    _local14++;
                };
            };
            var _local17:int;
            _local14 = 0;
            while (_local14 < numChildren) {
                _local16 = IUIComponent(getChildAt(_local14));
                if (!_local16.includeInLayout){
                } else {
                    _local5 = (_local5 + (isNaN(_local16.percentWidth)) ? _local16.getExplicitOrMeasuredWidth() : _local16.minWidth);
                    _local6 = (_local6 + ((_local13)>0) ? _local13 : _local16.getExplicitOrMeasuredWidth());
                    if (_local15 > 0){
                        _local5 = (_local5 + _local2);
                        _local6 = (_local6 + _local2);
                    };
                    _local7 = Math.max(_local7, (isNaN(_local16.percentHeight)) ? _local16.getExplicitOrMeasuredHeight() : _local16.minHeight);
                    _local8 = Math.max(_local8, _local16.getExplicitOrMeasuredHeight());
                    _local15++;
                    if ((((_local15 >= _local1)) || ((_local14 == (numChildren - 1))))){
                        _local9 = Math.max(_local9, _local5);
                        _local11 = Math.max(_local11, _local6);
                        _local10 = (_local10 + _local7);
                        _local12 = (_local12 + _local8);
                        if (_local17 > 0){
                            _local10 = (_local10 + _local3);
                            _local12 = (_local12 + _local3);
                        };
                        _local15 = 0;
                        _local17++;
                        _local5 = 0;
                        _local6 = 0;
                        _local7 = 0;
                        _local8 = 0;
                    };
                };
                _local14++;
            };
            var _local18:Number = (calculateLabelWidth() + _local4);
            _local9 = (_local9 + _local18);
            _local11 = (_local11 + _local18);
            if (((!((label == null))) && (!((label == ""))))){
                _local10 = Math.max(_local10, labelObj.getExplicitOrMeasuredHeight());
                _local12 = Math.max(_local12, labelObj.getExplicitOrMeasuredHeight());
            };
            var _local19:EdgeMetrics = viewMetricsAndPadding;
            _local10 = (_local10 + (_local19.top + _local19.bottom));
            _local9 = (_local9 + (_local19.left + _local19.right));
            _local12 = (_local12 + (_local19.top + _local19.bottom));
            _local11 = (_local11 + (_local19.left + _local19.right));
            measuredMinWidth = _local9;
            measuredMinHeight = _local10;
            measuredWidth = _local11;
            measuredHeight = _local12;
        }
        public function set required(_arg1:Boolean):void{
            if (_arg1 != _required){
                _required = _arg1;
                invalidateDisplayList();
                dispatchEvent(new Event("requiredChanged"));
            };
        }
        private function updateDisplayListVerticalChildren(_arg1:Number, _arg2:Number):void{
            var _local5:IUIComponent;
            var _local3:Number = (calculateLabelWidth() + getStyle("indicatorGap"));
            if (!isNaN(explicitMinWidth)){
                _explicitMinWidth = (_explicitMinWidth - _local3);
            } else {
                if (!isNaN(measuredMinWidth)){
                    measuredMinWidth = (measuredMinWidth - _local3);
                };
            };
            verticalLayoutObject.updateDisplayList((_arg1 - _local3), _arg2);
            if (!isNaN(explicitMinWidth)){
                _explicitMinWidth = (_explicitMinWidth + _local3);
            } else {
                if (!isNaN(measuredMinWidth)){
                    measuredMinWidth = (measuredMinWidth + _local3);
                };
            };
            var _local4:Number = numChildren;
            var _local6:Number = 0;
            while (_local6 < _local4) {
                _local5 = IUIComponent(getChildAt(_local6));
                IUIComponent(getChildAt(_local6)).move((_local5.x + _local3), _local5.y);
                _local6++;
            };
        }
        private function displayIndicator(_arg1:Number, _arg2:Number):void{
            var _local3:Class;
            if (required){
                if (!indicatorObj){
                    _local3 = (getStyle("indicatorSkin") as Class);
                    indicatorObj = IFlexDisplayObject(new (_local3));
                    rawChildren.addChild(DisplayObject(indicatorObj));
                };
                indicatorObj.x = (_arg1 + ((getStyle("indicatorGap") - indicatorObj.width) / 2));
                if (labelObj){
                    indicatorObj.y = (_arg2 + ((labelObj.getExplicitOrMeasuredHeight() - indicatorObj.measuredHeight) / 2));
                };
            } else {
                if (indicatorObj){
                    rawChildren.removeChild(DisplayObject(indicatorObj));
                    indicatorObj = null;
                };
            };
        }
        override public function set label(_arg1:String):void{
            _label = _arg1;
            labelChanged = true;
            invalidateProperties();
            invalidateSize();
            invalidateDisplayList();
            if ((parent is Form)){
                Form(parent).invalidateLabelWidth();
            };
            dispatchEvent(new Event("labelChanged"));
        }
        private function updateDisplayListHorizontalChildren(_arg1:Number, _arg2:Number):void{
            var _local17:int;
            var _local18:IUIComponent;
            var _local19:Number;
            var _local20:Number;
            var _local27:Number;
            var _local28:Number;
            var _local29:Number;
            var _local30:Number;
            var _local32:Number;
            var _local33:Number;
            var _local34:Number;
            var _local35:Number;
            var _local36:Number;
            var _local37:Number;
            var _local38:Number;
            var _local39:Boolean;
            var _local40:Array;
            var _local41:Number;
            var _local42:*;
            var _local43:*;
            var _local44:*;
            var _local3:EdgeMetrics = viewMetricsAndPadding;
            var _local4:Number = calculateLabelWidth();
            var _local5:Number = getStyle("indicatorGap");
            var _local6:Number = getStyle("horizontalGap");
            var _local7:Number = getStyle("verticalGap");
            var _local8:Number = getStyle("paddingLeft");
            var _local9:Number = getStyle("paddingTop");
            var _local10:Number = getHorizontalAlignValue();
            var _local11:Number = ((((scaleX > 0)) && (!((scaleX == 1))))) ? (minWidth / Math.abs(scaleX)) : minWidth;
            var _local12:Number = ((((scaleY > 0)) && (!((scaleY == 1))))) ? (minHeight / Math.abs(scaleY)) : minHeight;
            var _local13:Number = ((Math.max(_arg1, _local11) - _local3.left) - _local3.right);
            var _local14:Number = ((Math.max(_arg2, _local12) - _local3.top) - _local3.bottom);
            var _local15:Number = 0;
            var _local16:Number = ((_local13 - _local4) - _local5);
            if (_local16 < 0){
                _local16 = 0;
            };
            var _local21:int = calcNumColumns(_local16);
            var _local22:int;
            if (_local21 != guessedNumColumns){
                if (numberOfGuesses < 2){
                    guessedRowWidth = _local16;
                    numberOfGuesses++;
                    invalidateSize();
                    return;
                };
                _local21 = guessedNumColumns;
                numberOfGuesses = 0;
            } else {
                numberOfGuesses = 0;
            };
            var _local23:Number = ((_local8 + _local4) + _local5);
            var _local24:Number = _local9;
            var _local25:Number = _local23;
            var _local26:Number = _local24;
            var _local31:int = numChildren;
            _local17 = 0;
            while (_local17 < numChildren) {
                if (!IUIComponent(getChildAt(_local17)).includeInLayout){
                    _local31--;
                };
                _local17++;
            };
            if (_local21 == _local31){
                _local32 = Flex.flexChildWidthsProportionally(this, (_local16 - ((_local31 - 1) * _local6)), _local14);
                _local23 = (_local23 + (_local32 * _local10));
                _local17 = 0;
                while (_local17 < numChildren) {
                    _local18 = IUIComponent(getChildAt(_local17));
                    if (!_local18.includeInLayout){
                    } else {
                        _local18.move(Math.floor(_local23), _local24);
                        _local23 = (_local23 + (_local18.width + _local6));
                    };
                    _local17++;
                };
            } else {
                _local17 = 0;
                while (_local17 < numChildren) {
                    _local18 = IUIComponent(getChildAt(_local17));
                    if (!_local18.includeInLayout){
                    } else {
                        _local27 = _local18.getExplicitOrMeasuredWidth();
                        _local29 = _local18.percentWidth;
                        _local19 = (isNaN(_local29)) ? _local27 : ((_local29 * _local16) / 100);
                        _local19 = Math.max(_local18.minWidth, Math.min(_local18.maxWidth, _local19));
                        _local15 = Math.max(_local15, _local19);
                    };
                    _local17++;
                };
                _local15 = Math.min(_local15, Math.floor(((_local16 - ((_local21 - 1) * _local6)) / _local21)));
                _local33 = (_local16 - ((_local21 * _local15) + ((_local21 - 1) * _local6)));
                if (_local33 < 0){
                    _local33 = 0;
                };
                _local23 = (_local23 + (_local33 * _local10));
                _local34 = 0;
                _local35 = 0;
                _local36 = 0;
                _local37 = _local14;
                _local38 = _local37;
                _local22 = 0;
                _local17 = 0;
                while (_local17 < numChildren) {
                    _local18 = IUIComponent(getChildAt(_local17));
                    if (!_local18.includeInLayout){
                        if (_local17 == (numChildren - 1)){
                            _local38 = (_local38 - _local35);
                            if (_local17 != (numChildren - 1)){
                                _local38 = (_local38 - _local7);
                            };
                            if ((((_local35 > 0)) && ((_local34 > 0)))){
                                _local34 = Math.max(0, (_local34 - ((100 * _local35) / _local37)));
                            };
                            _local36 = (_local36 + _local34);
                            _local35 = 0;
                            _local34 = 0;
                            _local22 = 0;
                        };
                    } else {
                        if (!isNaN(_local18.percentHeight)){
                            _local34 = Math.max(_local34, _local18.percentHeight);
                        } else {
                            _local35 = Math.max(_local35, _local18.getExplicitOrMeasuredHeight());
                        };
                        ++_local22;
                        if ((((_local22 >= _local21)) || ((_local17 == (numChildren - 1))))){
                            _local38 = (_local38 - _local35);
                            if (_local17 != (numChildren - 1)){
                                _local38 = (_local38 - _local7);
                            };
                            if ((((_local35 > 0)) && ((_local34 > 0)))){
                                _local34 = Math.max(0, (_local34 - ((100 * _local35) / _local37)));
                            };
                            _local36 = (_local36 + _local34);
                            _local35 = 0;
                            _local34 = 0;
                            _local22 = 0;
                        };
                    };
                    _local17++;
                };
                _local39 = false;
                _local40 = new Array(numChildren);
                _local17 = 0;
                while (_local17 < numChildren) {
                    _local40[_local17] = -1;
                    _local17++;
                };
                _local41 = (_local38 - ((_local37 * _local36) / 100));
                if (_local41 > 0){
                    _local38 = (_local38 - _local41);
                };
                do  {
                    _local39 = true;
                    _local42 = (_local38 / _local36);
                    _local22 = 0;
                    _local25 = _local23;
                    _local26 = _local24;
                    _local43 = 0;
                    _local17 = 0;
                    while (_local17 < numChildren) {
                        _local18 = IUIComponent(getChildAt(_local17));
                        if (!_local18.includeInLayout){
                        } else {
                            _local27 = _local18.getExplicitOrMeasuredWidth();
                            _local28 = _local18.getExplicitOrMeasuredHeight();
                            _local29 = _local18.percentWidth;
                            _local30 = _local18.percentHeight;
                            _local19 = Math.min(_local15, (isNaN(_local29)) ? _local27 : ((_local29 * _local16) / 100));
                            _local19 = Math.max(_local18.minWidth, Math.min(_local18.maxWidth, _local19));
                            if (_local40[_local17] != -1){
                                _local20 = _local40[_local17];
                            } else {
                                _local20 = (isNaN(_local30)) ? _local28 : (_local30 * _local42);
                                if (_local20 < _local18.minHeight){
                                    _local20 = _local18.minHeight;
                                    _local36 = (_local36 - _local18.percentHeight);
                                    _local38 = (_local38 - _local20);
                                    _local40[_local17] = _local20;
                                    _local39 = false;
                                    break;
                                } else {
                                    if (_local20 > _local18.maxHeight){
                                        _local20 = _local18.maxHeight;
                                        _local36 = (_local36 - _local18.percentHeight);
                                        _local38 = (_local38 - _local20);
                                        _local40[_local17] = _local20;
                                        _local39 = false;
                                        break;
                                    };
                                };
                            };
                            if ((((_local18.scaleX == 1)) && ((_local18.scaleY == 1)))){
                                _local18.setActualSize(Math.floor(_local19), Math.floor(_local20));
                            } else {
                                _local18.setActualSize(_local19, _local20);
                            };
                            _local44 = ((_local15 - _local18.width) * _local10);
                            _local18.move(Math.floor((_local25 + _local44)), Math.floor(_local26));
                            _local43 = Math.max(_local43, _local18.height);
                            ++_local22;
                            if (_local22 >= _local21){
                                _local25 = _local23;
                                _local22 = 0;
                                _local26 = (_local26 + (_local43 + _local7));
                                _local43 = 0;
                            } else {
                                _local25 = (_local25 + (_local15 + _local6));
                            };
                        };
                        _local17++;
                    };
                } while (!(_local39));
            };
        }
        override public function get label():String{
            return (_label);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            var _local7:IUIComponent;
            var _local8:Number;
            if (FlexVersion.compatibilityVersion < FlexVersion.VERSION_3_0){
                previousUpdateDisplayList(_arg1, _arg2);
                return;
            };
            super.updateDisplayList(_arg1, _arg2);
            if (direction == FormItemDirection.VERTICAL){
                updateDisplayListVerticalChildren(_arg1, _arg2);
            } else {
                updateDisplayListHorizontalChildren(_arg1, _arg2);
            };
            var _local3:EdgeMetrics = viewMetricsAndPadding;
            var _local4:Number = _local3.left;
            var _local5:Number = _local3.top;
            var _local6:Number = calculateLabelWidth();
            if (numChildren > 0){
                _local7 = IUIComponent(getChildAt(0));
                _local8 = _local7.baselinePosition;
                if (!isNaN(_local8)){
                    _local5 = (_local5 + (_local8 - labelObj.baselinePosition));
                };
            };
            labelObj.setActualSize(_local6, labelObj.getExplicitOrMeasuredHeight());
            labelObj.move(_local4, _local5);
            _local4 = (_local4 + _local6);
            displayIndicator(_local4, _local5);
        }
        private function calculateLabelWidth():Number{
            var _local1:Number = getStyle("labelWidth");
            if (_local1 == 0){
                _local1 = NaN;
            };
            if (((isNaN(_local1)) && ((parent is Form)))){
                _local1 = Form(parent).calculateLabelWidth();
            };
            if (isNaN(_local1)){
                _local1 = getPreferredLabelWidth();
            };
            return (_local1);
        }
        private function measureVertical():void{
            var _local2:Number;
            verticalLayoutObject.measure();
            var _local1:Number = (calculateLabelWidth() + getStyle("indicatorGap"));
            measuredMinWidth = (measuredMinWidth + _local1);
            measuredWidth = (measuredWidth + _local1);
            _local2 = labelObj.getExplicitOrMeasuredHeight();
            measuredMinHeight = Math.max(measuredMinHeight, _local2);
            measuredHeight = Math.max(measuredHeight, _local2);
        }
        public function set direction(_arg1:String):void{
            _direction = _arg1;
            invalidateSize();
            invalidateDisplayList();
            dispatchEvent(new Event("directionChanged"));
        }
        public function get direction():String{
            return (_direction);
        }

    }
}//package mx.containers 
