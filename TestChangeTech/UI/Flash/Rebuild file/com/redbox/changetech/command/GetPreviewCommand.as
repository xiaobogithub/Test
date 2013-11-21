﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetPreviewCommand implements ICommand, IResponder {

        public function fault(_arg1:Object):void{
        }
        public function result(_arg1:Object):void{
            var _local2:ContentCollection = (_arg1.result as ContentCollection);
            BalanceModelLocator.getInstance().setRoomContent(_local2);
        }
        public function execute(_arg1:CairngormEvent):void{
            var _local2:ServiceDelegate = new ServiceDelegate(this);
            _local2.GetPreview(_arg1.data);
        }

    }
}//package com.redbox.changetech.command 
