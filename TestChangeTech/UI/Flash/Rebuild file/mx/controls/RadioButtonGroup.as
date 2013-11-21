//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.controls {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;

    public class RadioButtonGroup extends EventDispatcher implements IMXMLObject {

        private var radioButtons:Array
        private var _selection:RadioButton
        private var _selectedValue:Object
        private var document:IFlexDisplayObject
        private var _labelPlacement:String = "right"
        private var indexNumber:int = 0

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function RadioButtonGroup(_arg1:IFlexDisplayObject=null){
            radioButtons = [];
            super();
        }
        public function get enabled():Boolean{
            var _local1:Number = 0;
            var _local2:int = numRadioButtons;
            var _local3:int;
            while (_local3 < _local2) {
                _local1 = (_local1 + getRadioButtonAt(_local3).enabled);
                _local3++;
            };
            if (_local1 == 0){
                return (false);
            };
            if (_local1 == _local2){
                return (true);
            };
            return (false);
        }
        public function set enabled(_arg1:Boolean):void{
            var _local2:int = numRadioButtons;
            var _local3:int;
            while (_local3 < _local2) {
                getRadioButtonAt(_local3).enabled = _arg1;
                _local3++;
            };
            dispatchEvent(new Event("enabledChanged"));
        }
        private function radioButton_removedHandler(_arg1:Event):void{
            var _local2:RadioButton = (_arg1.target as RadioButton);
            if (_local2){
                _local2.removeEventListener(Event.REMOVED, radioButton_removedHandler);
                removeInstance(RadioButton(_arg1.target));
            };
        }
        public function set selectedValue(_arg1:Object):void{
            var _local4:RadioButton;
            _selectedValue = _arg1;
            var _local2:int = numRadioButtons;
            var _local3:int;
            while (_local3 < _local2) {
                _local4 = getRadioButtonAt(_local3);
                if ((((_local4.value == _arg1)) || ((_local4.label == _arg1)))){
                    changeSelection(_local3, false);
                    break;
                };
                _local3++;
            };
            dispatchEvent(new FlexEvent(FlexEvent.VALUE_COMMIT));
        }
        private function getValue():String{
            if (selection){
                return ((((((selection.value) && ((selection.value is String)))) && (!((String(selection.value).length == 0))))) ? String(selection.value) : selection.label);
                //unresolved jump
            };
            return (null);
        }
        public function get labelPlacement():String{
            return (_labelPlacement);
        }
        public function get selection():RadioButton{
            return (_selection);
        }
        public function get selectedValue():Object{
            if (selection){
                return (((selection.value)!=null) ? selection.value : selection.label);
            };
            return (null);
        }
        public function set selection(_arg1:RadioButton):void{
            setSelection(_arg1, false);
        }
        mx_internal function setSelection(_arg1:RadioButton, _arg2:Boolean=true):void{
            var _local3:int;
            var _local4:int;
            if ((((_arg1 == null)) && (!((_selection == null))))){
                _selection.selected = false;
                _selection = null;
                if (_arg2){
                    dispatchEvent(new Event(Event.CHANGE));
                };
            } else {
                _local3 = numRadioButtons;
                _local4 = 0;
                while (_local4 < _local3) {
                    if (_arg1 == getRadioButtonAt(_local4)){
                        changeSelection(_local4, _arg2);
                        break;
                    };
                    _local4++;
                };
            };
            dispatchEvent(new FlexEvent(FlexEvent.VALUE_COMMIT));
        }
        public function initialized(_arg1:Object, _arg2:String):void{
            this.document = (_arg1) ? IFlexDisplayObject(_arg1) : IFlexDisplayObject(Application.application);
        }
        mx_internal function addInstance(_arg1:RadioButton):void{
            _arg1.indexNumber = indexNumber++;
            _arg1.addEventListener(Event.REMOVED, radioButton_removedHandler);
            radioButtons.push(_arg1);
            if (_selectedValue != null){
                selectedValue = _selectedValue;
            };
            dispatchEvent(new Event("numRadioButtonsChanged"));
        }
        public function set labelPlacement(_arg1:String):void{
            _labelPlacement = _arg1;
            var _local2:int = numRadioButtons;
            var _local3:int;
            while (_local3 < _local2) {
                getRadioButtonAt(_local3).labelPlacement = _arg1;
                _local3++;
            };
        }
        public function get numRadioButtons():int{
            return (radioButtons.length);
        }
        public function getRadioButtonAt(_arg1:int):RadioButton{
            return (RadioButton(radioButtons[_arg1]));
        }
        mx_internal function removeInstance(_arg1:RadioButton):void{
            var _local2:Boolean;
            var _local3:int;
            var _local4:RadioButton;
            if (_arg1){
                _local2 = false;
                _local3 = 0;
                while (_local3 < numRadioButtons) {
                    _local4 = getRadioButtonAt(_local3);
                    if (_local2){
                        _local4.indexNumber--;
                    } else {
                        if (_local4 == _arg1){
                            _local4.group = null;
                            if (_arg1 == _selection){
                                _selection = null;
                            };
                            radioButtons.splice(_local3, 1);
                            _local2 = true;
                            indexNumber--;
                            _local3--;
                        };
                    };
                    _local3++;
                };
                if (_local2){
                    dispatchEvent(new Event("numRadioButtonsChanged"));
                };
            };
        }
        private function changeSelection(_arg1:int, _arg2:Boolean=true):void{
            if (getRadioButtonAt(_arg1)){
                if (selection){
                    selection.selected = false;
                };
                _selection = getRadioButtonAt(_arg1);
                _selection.selected = true;
                if (_arg2){
                    dispatchEvent(new Event(Event.CHANGE));
                };
            };
        }

    }
}//package mx.controls 
