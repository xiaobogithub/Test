//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.command {
    import mx.controls.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import mx.rpc.*;
    import com.adobe.cairngorm.control.*;
    import com.adobe.cairngorm.commands.*;
    import com.redbox.changetech.business.*;

    public class GetConfigCommand implements ICommand, IResponder {

        private var callBackObject:Object
        private var model:BalanceModelLocator
        private var callBackFunction:Function

        public function GetConfigCommand(){
            model = BalanceModelLocator.getInstance();
            super();
        }
        public function fault(_arg1:Object):void{
            Alert.show("There was an error getting the config xml.");
        }
        public function execute(_arg1:CairngormEvent):void{
            callBackObject = _arg1.data.callBackObject;
            callBackFunction = _arg1.data.callBackFunction;
            var _local2:GetXMLDelegate = new GetXMLDelegate(this, "config.xml");
        }
        public function result(_arg1:Object):void{
            var _local2:XML = XML(_arg1.target.data);
            model.gateway = _local2.AMF_GATEWAY;
            model.culture = _local2.CULTURE;
            if (_local2.debugMode == "true"){
                model.isDebugMode = true;
            } else {
                model.isDebugMode = false;
            };
            if (_local2.alwaysShowLogin == "true"){
                model.alwaysShowLogin = true;
            } else {
                model.alwaysShowLogin = false;
            };
            if (_local2.flashVarOveride == "true"){
                model.flashVars = new Object();
                model.flashVars.mode = String(_local2.flashVars.mode);
                model.flashVars.previewCode = String(_local2.flashVars.previewCode);
                model.flashVars.dayOveride = String(_local2.flashVars.dayOveride);
                model.flashVars.companyId = _local2.flashVars.companyId;
            };
            model.dropDownMenuVO = new DropDownMenuVO(XML(_local2.popup_modules));
            callBackFunction.apply(callBackObject);
        }

    }
}//package com.redbox.changetech.command 
