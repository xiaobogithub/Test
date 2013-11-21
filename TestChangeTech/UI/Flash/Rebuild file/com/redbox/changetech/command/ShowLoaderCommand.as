//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.core.*;
    import com.redbox.changetech.model.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;

    public class ShowLoaderCommand implements ICommand {

        public function execute(_arg1:CairngormEvent):void{
            BalanceModelLocator.getInstance().loaderSource = (_arg1.data as UIComponent);
        }

    }
}//package com.redbox.changetech.command 
