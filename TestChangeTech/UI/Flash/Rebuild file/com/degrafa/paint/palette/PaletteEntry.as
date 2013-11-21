//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.paint.palette {
    import flash.utils.*;
    import com.degrafa.core.*;

    public class PaletteEntry extends DegrafaObject {

        protected var typeName:String
        private var _value:Object
        private var _name:String
        protected var type:Class

        public function PaletteEntry(_arg1:String=undefined, _arg2:Object=null){
            this.name = _arg1;
            this.value = _arg2;
        }
        public function set value(_arg1:Object):void{
            var _local2:Object;
            if (_value != _arg1){
                _local2 = _value;
                _value = _arg1;
                typeName = getQualifiedClassName(_value);
                typeName = typeName.replace("::", ".");
                type = Class(getDefinitionByName(typeName));
                initChange("value", _local2, _value, this);
            };
        }
        override public function get name():String{
            return (_name);
        }
        public function get value():Object{
            return (_value);
        }
        public function set name(_arg1:String):void{
            _name = _arg1;
        }

    }
}//package com.degrafa.paint.palette 
