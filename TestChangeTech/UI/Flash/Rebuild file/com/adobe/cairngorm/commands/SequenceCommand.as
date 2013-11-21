//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.cairngorm.commands {
    import com.adobe.cairngorm.control.*;

    public class SequenceCommand implements ICommand {

        public var nextEvent:CairngormEvent

        public function SequenceCommand(_arg1:CairngormEvent=null):void{
            this.nextEvent = _arg1;
        }
        public function execute(_arg1:CairngormEvent):void{
        }
        public function executeNextCommand():void{
            var _local1:Boolean;
            _local1 = !((nextEvent == null));
            if (_local1){
                CairngormEventDispatcher.getInstance().dispatchEvent(nextEvent);
            };
        }

    }
}//package com.adobe.cairngorm.commands 
