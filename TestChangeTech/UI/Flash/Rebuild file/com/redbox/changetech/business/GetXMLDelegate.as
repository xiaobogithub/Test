//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.business {
    import flash.events.*;
    import mx.controls.*;
    import mx.rpc.*;
    import flash.net.*;

    public class GetXMLDelegate {

        public function GetXMLDelegate(_arg1:IResponder, _arg2:String){
            var responder:* = _arg1;
            var aURL:* = _arg2;
            super();
            var request:* = new URLRequest(aURL);
            var loader:* = new URLLoader();
            loader.addEventListener(Event.COMPLETE, responder.result);
            loader.addEventListener(IOErrorEvent.IO_ERROR, responder.result);
            configureListeners(loader);
            try {
                loader.load(request);
            } catch(error:Error) {
                responder.fault(error);
            };
        }
        private function httpStatusHandler(_arg1:HTTPStatusEvent):void{
        }
        private function completeHandler(_arg1:Event):void{
        }
        private function securityErrorHandler(_arg1:SecurityErrorEvent):void{
            (Alert.show(("securityErrorHandler: " + _arg1)) + "\r");
        }
        private function cancelHandler(_arg1:Event):void{
        }
        private function ioErrorHandler(_arg1:IOErrorEvent):void{
            Alert.show(("XML failed: " + _arg1));
        }
        private function configureListeners(_arg1:IEventDispatcher):void{
            _arg1.addEventListener(Event.CANCEL, cancelHandler);
            _arg1.addEventListener(HTTPStatusEvent.HTTP_STATUS, httpStatusHandler);
            _arg1.addEventListener(Event.OPEN, openHandler);
            _arg1.addEventListener(ProgressEvent.PROGRESS, progressHandler);
            _arg1.addEventListener(SecurityErrorEvent.SECURITY_ERROR, securityErrorHandler);
        }
        private function progressHandler(_arg1:ProgressEvent):void{
        }
        private function openHandler(_arg1:Event):void{
        }

    }
}//package com.redbox.changetech.business 
