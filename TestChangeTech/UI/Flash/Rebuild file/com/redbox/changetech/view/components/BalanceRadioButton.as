//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import mx.controls.*;

    public class BalanceRadioButton extends RadioButton {

        public var actionFlag:String
        private var balanceRadioButtonTick:BalanceRadioButtonTick
        public var optionID:Number
        public var score:Number
        public var description:String

        override public function set selected(_arg1:Boolean):void{
            super.selected = _arg1;
            if (_arg1){
            };
        }
        override protected function createChildren():void{
            super.createChildren();
            balanceRadioButtonTick = new BalanceRadioButtonTick();
            trace(("createChildren optionID=" + optionID));
        }

    }
}//package com.redbox.changetech.view.components 
