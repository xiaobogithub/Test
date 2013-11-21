//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import mx.core.*;
    import flash.utils.*;

    public class BalanceRadioButtonTick_MyTick extends MovieClipLoaderAsset {

        public var dataClass:Class

        private static var bytes:ByteArray = null;

        public function BalanceRadioButtonTick_MyTick(){
            dataClass = BalanceRadioButtonTick_MyTick_dataClass;
            super();
            initialWidth = (700 / 20);
            initialHeight = (700 / 20);
        }
        override public function get movieClipData():ByteArray{
            if (bytes == null){
                bytes = ByteArray(new dataClass());
            };
            return (bytes);
        }

    }
}//package com.redbox.changetech.view.components 
