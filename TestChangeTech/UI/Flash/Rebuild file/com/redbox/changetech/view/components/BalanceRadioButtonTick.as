//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;

    public class BalanceRadioButtonTick extends Sprite {

        private var MyTick:Class
        private var tick:MovieClip

        public function BalanceRadioButtonTick(){
            MyTick = BalanceRadioButtonTick_MyTick;
            super();
            tick = new MyTick();
            addChild(tick);
        }
    }
}//package com.redbox.changetech.view.components 
