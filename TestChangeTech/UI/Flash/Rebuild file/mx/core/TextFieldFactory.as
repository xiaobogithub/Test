//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.core {
    import flash.text.*;
    import flash.utils.*;

    public class TextFieldFactory implements ITextFieldFactory {

        private var textFields:Dictionary

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var instance:ITextFieldFactory;

        public function TextFieldFactory(){
            textFields = new Dictionary(true);
            super();
        }
        public function createTextField(_arg1:IFlexModuleFactory):TextField{
            var _local4:Object;
            var _local2:TextField;
            var _local3:Dictionary = textFields[_arg1];
            if (_local3){
                for (_local4 in _local3) {
                    _local2 = TextField(_local4);
                    break;
                };
            };
            if (!_local2){
                if (_arg1){
                    _local2 = TextField(_arg1.create("flash.text.TextField"));
                } else {
                    _local2 = new TextField();
                };
                if (!_local3){
                    _local3 = new Dictionary(true);
                };
                _local3[_local2] = 1;
                textFields[_arg1] = _local3;
            };
            return (_local2);
        }

        public static function getInstance():ITextFieldFactory{
            if (!instance){
                instance = new (TextFieldFactory);
            };
            return (instance);
        }

    }
}//package mx.core 
