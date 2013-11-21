//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import com.redbox.changetech.model.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;

    public class HideLoaderCommand implements ICommand {

        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().loaderSource = null;
        }

    }
}//package com.redbox.changetech.command 
