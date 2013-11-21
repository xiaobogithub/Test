//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.text.*;
    import mx.events.*;
    import mx.controls.*;

    public class BalanceTextArea extends TextArea {

        public function BalanceTextArea(){
            addEventListener(FlexEvent.CREATION_COMPLETE, init);
            addEventListener(FlexEvent.VALUE_COMMIT, change);
            selectable = false;
        }
        private function init(_arg1:FlexEvent=null):void{
            removeEventListener(FlexEvent.CREATION_COMPLETE, init);
            textField.autoSize = TextFieldAutoSize.LEFT;
            textField.antiAliasType = AntiAliasType.ADVANCED;
            textField.condenseWhite = true;
            focusEnabled = false;
            verticalScrollPolicy = "off";
            height = textField.height;
        }
        private function change(_arg1:FlexEvent=null):void{
            height = textField.height;
        }

    }
}//package com.redbox.changetech.view.components 
