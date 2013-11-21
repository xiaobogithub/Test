//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class AuxFunctions {

        public static function concatObjects(... _args):Object{
            var _local3:Object;
            var _local5:String;
            var _local2:Object = {};
            var _local4:int;
            while (_local4 < _args.length) {
                _local3 = _args[_local4];
                for (_local5 in _local3) {
                    if (_local3[_local5] == null){
                        delete _local2[_local5];
                    } else {
                        _local2[_local5] = _local3[_local5];
                    };
                };
                _local4++;
            };
            return (_local2);
        }
        public static function numberToG(_arg1:Number):Number{
            return (((_arg1 & 0xFF00) >> 8));
        }
        public static function numberToR(_arg1:Number):Number{
            return (((_arg1 & 0xFF0000) >> 16));
        }
        public static function isInArray(_arg1:String, _arg2:Array):Boolean{
            var _local3:uint = _arg2.length;
            var _local4:uint;
            while (_local4 < _local3) {
                if (_arg2[_local4] == _arg1){
                    return (true);
                };
                _local4++;
            };
            return (false);
        }
        public static function getObjectLength(_arg1:Object):uint{
            var _local3:String;
            var _local2:uint;
            for (_local3 in _arg1) {
                _local2++;
            };
            return (_local2);
        }
        public static function numberToB(_arg1:Number):Number{
            return ((_arg1 & 0xFF));
        }

    }
}//package caurina.transitions 
