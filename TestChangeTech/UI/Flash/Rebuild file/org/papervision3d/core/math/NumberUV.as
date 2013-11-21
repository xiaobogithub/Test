//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.math {

    public class NumberUV {

        public var u:Number
        public var v:Number

        public function NumberUV(_arg1:Number=0, _arg2:Number=0){
            this.u = _arg1;
            this.v = _arg2;
        }
        public function toString():String{
            return (((("u:" + u) + " v:") + v));
        }
        public function clone():NumberUV{
            return (new NumberUV(this.u, this.v));
        }

        public static function get ZERO():NumberUV{
            return (new NumberUV(0, 0));
        }

    }
}//package org.papervision3d.core.math 
