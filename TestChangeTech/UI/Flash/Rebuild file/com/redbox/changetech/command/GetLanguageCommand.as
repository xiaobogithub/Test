//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetLanguageCommand implements ICommand, IResponder {

        private var callBackObject:Object
        private var model:BalanceModelLocator
        private var callBackFunction:Function

        public function GetLanguageCommand(){
            model = BalanceModelLocator.getInstance();
        }
        public function fault(_arg1:Object):void{
            Alert.show("There was an error getting the config xml.");
        }
        public function execute(_arg1:CairngormEvent):void{
            callBackObject = _arg1.data.callBackObject;
            callBackFunction = _arg1.data.callBackFunction;
            var _local2:String = model.culture;
            var _local3:GetXMLDelegate = new GetXMLDelegate(this, (("locales/" + _local2) + "/lang.xml"));
        }
        public function result(_arg1:Object):void{
            var _local2:XML = XML(_arg1.target.data);
            model.languageVO = new LanguageVo(_local2);
            trace("----------------TESTING LANGUAGE FILE------------------");
            trace(model.languageVO.getLang("MONDAY"));
            trace(model.languageVO.getLang("TUESDAY"));
            trace(model.languageVO.getLang("WEDNESDAY"));
            trace(model.languageVO.getLang("THURSDAY"));
            trace(model.languageVO.getLang("FRIDAY"));
            trace(model.languageVO.getLang("SATURDAY"));
            trace(model.languageVO.getLang("SUNDAY"));
            callBackFunction.apply(callBackObject);
        }

    }
}//package com.redbox.changetech.command 
