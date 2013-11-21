//Created by Action Script Viewer - http://www.buraks.com/asv
package assets {
    import mx.core.*;
    import flash.utils.*;

    public class Assets_balance_intro extends MovieClipLoaderAsset {

        public var dataClass:Class

        private static var bytes:ByteArray = null;

        public function Assets_balance_intro(){
            dataClass = Assets_balance_intro_dataClass;
            super();
            initialWidth = (16000 / 20);
            initialHeight = (8000 / 20);
        }
        override public function get movieClipData():ByteArray{
            if (bytes == null){
                bytes = ByteArray(new dataClass());
            };
            return (bytes);
        }

    }
}//package assets 
