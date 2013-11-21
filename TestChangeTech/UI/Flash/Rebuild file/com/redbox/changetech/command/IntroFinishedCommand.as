//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.control.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;

    public class IntroFinishedCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.INITIALISE);
            _local2.dispatch();
        }

    }
}//package com.redbox.changetech.command 
