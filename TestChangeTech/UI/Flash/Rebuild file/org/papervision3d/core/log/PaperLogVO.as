//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.log {

    public class PaperLogVO {

        public var msg:String
        public var level:int
        public var arg:Array
        public var object:Object

        public function PaperLogVO(_arg1:int, _arg2:String, _arg3:Object, _arg4:Array){
            this.level = _arg1;
            this.msg = _arg2;
            this.object = _arg3;
            this.arg = _arg4;
        }
    }
}//package org.papervision3d.core.log 
