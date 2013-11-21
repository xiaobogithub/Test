//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls {
    import mx.core.*;
    import mx.managers.*;
    import flash.events.*;
    import mx.events.*;
    import flash.ui.*;

    public class RadioButton extends Button implements IFocusManagerGroup {

        private var _group:RadioButtonGroup
        mx_internal var _groupName:String
        private var _value:Object
        private var groupChanged:Boolean = false
        mx_internal var indexNumber:int = 0

        mx_internal static const VERSION:String = "3.2.0.3958";

        mx_internal static var createAccessibilityImplementation:Function;

        public function RadioButton(){
            _labelPlacement = "";
            _toggle = true;
            groupName = "radioGroup";
            addEventListener(FlexEvent.ADD, addHandler);
            centerContent = false;
            extraSpacing = 8;
        }
        private function addHandler(_arg1:FlexEvent):void{
            if (((!(_group)) && (initialized))){
                addToGroup();
            };
        }
        private function setNext(_arg1:Boolean=true):void{
            var _local5:RadioButton;
            var _local2:RadioButtonGroup = group;
            var _local3:IFocusManager = focusManager;
            if (_local3){
                _local3.showFocusIndicator = true;
            };
            var _local4:int = (indexNumber + 1);
            while (_local4 < _local2.numRadioButtons) {
                _local5 = _local2.getRadioButtonAt(_local4);
                if (((_local5) && (_local5.enabled))){
                    if (_arg1){
                        _local2.setSelection(_local5);
                    };
                    _local5.setFocus();
                    return;
                };
                _local4++;
            };
            if (((_arg1) && (!((_local2.getRadioButtonAt(indexNumber) == _local2.selection))))){
                _local2.setSelection(this);
            };
            this.drawFocus(true);
        }
        private function addToGroup():Object{
            var _local1:RadioButtonGroup = group;
            if (_local1){
                _local1.addInstance(this);
            };
            return (_local1);
        }
        override protected function commitProperties():void{
            super.commitProperties();
            if (groupChanged){
                addToGroup();
                groupChanged = false;
            };
        }
        override protected function clickHandler(_arg1:MouseEvent):void{
            if (((!(enabled)) || (selected))){
                return;
            };
            if (!_group){
                addToGroup();
            };
            super.clickHandler(_arg1);
            group.setSelection(this);
            var _local2:ItemClickEvent = new ItemClickEvent(ItemClickEvent.ITEM_CLICK);
            _local2.label = label;
            _local2.index = indexNumber;
            _local2.relatedObject = this;
            _local2.item = value;
            group.dispatchEvent(_local2);
        }
        override protected function keyUpHandler(_arg1:KeyboardEvent):void{
            super.keyUpHandler(_arg1);
            if ((((_arg1.keyCode == Keyboard.SPACE)) && (!(_toggle)))){
                _toggle = true;
            };
        }
        override public function get labelPlacement():String{
            var _local1:String = ButtonLabelPlacement.RIGHT;
            if (_labelPlacement != ""){
                _local1 = _labelPlacement;
            } else {
                if (((_group) && (!((_group.labelPlacement == ""))))){
                    _local1 = _group.labelPlacement;
                };
            };
            return (_local1);
        }
        public function set groupName(_arg1:String):void{
            if (((!(_arg1)) || ((_arg1 == "")))){
                return;
            };
            deleteGroup();
            _groupName = _arg1;
            groupChanged = true;
            invalidateProperties();
            invalidateDisplayList();
            dispatchEvent(new Event("groupNameChanged"));
        }
        override protected function initializeAccessibility():void{
            if (RadioButton.createAccessibilityImplementation != null){
                RadioButton.createAccessibilityImplementation(this);
            };
        }
        private function setThis():void{
            if (!_group){
                addToGroup();
            };
            var _local1:RadioButtonGroup = group;
            if (_local1.selection != this){
                _local1.setSelection(this);
            };
        }
        override public function get emphasized():Boolean{
            return (false);
        }
        override public function get toggle():Boolean{
            return (super.toggle);
        }
        override protected function measure():void{
            var _local1:Number;
            var _local2:Number;
            var _local3:Number;
            var _local4:Number;
            super.measure();
            if (FlexVersion.compatibilityVersion < FlexVersion.VERSION_3_0){
                _local1 = measureText(label).height;
                _local2 = (currentIcon) ? currentIcon.height : 0;
                if ((((labelPlacement == ButtonLabelPlacement.LEFT)) || ((labelPlacement == ButtonLabelPlacement.RIGHT)))){
                    _local3 = Math.max(_local1, _local2);
                } else {
                    _local3 = (_local1 + _local2);
                    _local4 = getStyle("verticalGap");
                    if (((!((_local2 == 0))) && (!(isNaN(_local4))))){
                        _local3 = (_local3 + _local4);
                    };
                };
                measuredMinHeight = (measuredHeight = Math.max(_local3, 18));
            };
        }
        override public function set toggle(_arg1:Boolean):void{
        }
        mx_internal function deleteGroup():void{
            try {
                if (document[groupName]){
                    delete document[groupName];
                };
            } catch(e:Error) {
                try {
                    if (document.automaticRadioButtonGroups[groupName]){
                        delete document.automaticRadioButtonGroups[groupName];
                    };
                } catch(e1:Error) {
                };
            };
        }
        override protected function keyDownHandler(_arg1:KeyboardEvent):void{
            if (!enabled){
                return;
            };
            switch (_arg1.keyCode){
                case Keyboard.DOWN:
                    setNext(!(_arg1.ctrlKey));
                    _arg1.stopPropagation();
                    break;
                case Keyboard.UP:
                    setPrev(!(_arg1.ctrlKey));
                    _arg1.stopPropagation();
                    break;
                case Keyboard.LEFT:
                    setPrev(!(_arg1.ctrlKey));
                    _arg1.stopPropagation();
                    break;
                case Keyboard.RIGHT:
                    setNext(!(_arg1.ctrlKey));
                    _arg1.stopPropagation();
                    break;
                case Keyboard.SPACE:
                    setThis();
                    _toggle = false;
                default:
                    super.keyDownHandler(_arg1);
                    break;
            };
        }
        public function get groupName():String{
            return (_groupName);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            if (groupChanged){
                addToGroup();
                groupChanged = false;
            };
            if (((((_group) && (_selected))) && (!((_group.selection == this))))){
                group.setSelection(this, false);
            };
        }
        public function get value():Object{
            return (_value);
        }
        public function set value(_arg1:Object):void{
            _value = _arg1;
            dispatchEvent(new Event("valueChanged"));
            if (((selected) && (group))){
                group.dispatchEvent(new FlexEvent(FlexEvent.VALUE_COMMIT));
            };
        }
        private function setPrev(_arg1:Boolean=true):void{
            var _local5:RadioButton;
            var _local2:RadioButtonGroup = group;
            var _local3:IFocusManager = focusManager;
            if (_local3){
                _local3.showFocusIndicator = true;
            };
            var _local4 = 1;
            while (_local4 <= indexNumber) {
                _local5 = _local2.getRadioButtonAt((indexNumber - _local4));
                if (((_local5) && (_local5.enabled))){
                    if (_arg1){
                        _local2.setSelection(_local5);
                    };
                    _local5.setFocus();
                    return;
                };
                _local4++;
            };
            if (((_arg1) && (!((_local2.getRadioButtonAt(indexNumber) == _local2.selection))))){
                _local2.setSelection(this);
            };
            this.drawFocus(true);
        }
        public function set group(_arg1:RadioButtonGroup):void{
            _group = _arg1;
        }
        public function get group():RadioButtonGroup{
            var g:* = null;
            if (!document){
                return (_group);
            };
            if (!_group){
                if (((groupName) && (!((groupName == ""))))){
                    try {
                        g = RadioButtonGroup(document[groupName]);
                    } catch(e:Error) {
                        if (((document.automaticRadioButtonGroups) && (document.automaticRadioButtonGroups[groupName]))){
                            g = RadioButtonGroup(document.automaticRadioButtonGroups[groupName]);
                        };
                    };
                    if (!g){
                        g = new RadioButtonGroup(IFlexDisplayObject(document));
                        if (!document.automaticRadioButtonGroups){
                            document.automaticRadioButtonGroups = {};
                        };
                        document.automaticRadioButtonGroups[groupName] = g;
                    } else {
                        if (!(g is RadioButtonGroup)){
                            return (null);
                        };
                    };
                    _group = g;
                };
            };
            return (_group);
        }

    }
}//package mx.controls 
