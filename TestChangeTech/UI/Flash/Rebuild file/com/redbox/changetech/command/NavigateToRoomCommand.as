//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.adobe.cairngorm.control.*;
    import com.redbox.changetech.util.*;
    import com.redbox.changetech.util.Enumerations.*;
    import com.adobe.cairngorm.commands.*;

    public class NavigateToRoomCommand implements ICommand {

        private var model:BalanceModelLocator

        public function NavigateToRoomCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().room = Config.ROOM_MAPPINGS[(_arg1.data as String)];
            if (BalanceModelLocator.getInstance().room == Config.ROOM_ORDER[RoomName.Blank]){
                BalanceModelLocator.getInstance().showControls = false;
            } else {
                BalanceModelLocator.getInstance().showControls = true;
            };
        }

    }
}//package com.redbox.changetech.command 
