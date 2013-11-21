//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.control.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class LogoutCommand implements ICommand, IResponder {

        private var _viewCallback:Function

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.EXIT_COMMAND);
            _local2.dispatch();
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.Logout();
        }

    }
}//package com.redbox.changetech.command 
