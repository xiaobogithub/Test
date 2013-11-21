//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import com.adobe.cairngorm.control.*;
    import com.redbox.changetech.util.Enumerations.*;
    import com.adobe.cairngorm.commands.*;

    public class StartUpCommand extends SequenceCommand implements ICommand {

        public function StartUpCommand(){
            this.nextEvent = new CairngormEvent(BalanceController.SHOW_LOADER);
        }
        override public function execute(_arg1:CairngormEvent):void{
            var _local2:ContentCollection = new ContentCollection();
            _local2.Type = ContentType.Intro.Text;
            _local2.Contents = [new Content()];
            BalanceModelLocator.getInstance().setRoomContent(_local2);
        }

    }
}//package com.redbox.changetech.command 
