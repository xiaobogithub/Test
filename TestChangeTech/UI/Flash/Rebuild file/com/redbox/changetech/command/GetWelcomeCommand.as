//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetWelcomeCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
            BalanceModelLocator.getInstance().setRoomContent((_arg1.result as ContentCollection));
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.GetWelcome(new Date());
        }

    }
}//package com.redbox.changetech.command 
