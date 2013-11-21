//Created by Action Script Viewer - http://www.buraks.com/asv
package assets {
    import mx.core.*;
    import flash.utils.*;

    public class Assets_tick extends MovieClipLoaderAsset {

        public var dataClass:Class

        private static var bytes:ByteArray = null;

        public function Assets_tick(){
            dataClass = Assets_tick_dataClass;
            super();
            initialWidth = (2140 / 20);
            initialHeight = (1620 / 20);
        }
        override public function get movieClipData():ByteArray{
            if (bytes == null){
                bytes = ByteArray(new dataClass());
            };
            return (bytes);
        }

    }
}//package assets 
