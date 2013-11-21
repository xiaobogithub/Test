//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.states {
    import flash.events.*;
    import mx.events.*;

    public class State extends EventDispatcher {

        public var overrides:Array
        public var name:String
        public var basedOn:String
        private var initialized:Boolean = false
        public var stateManager:StateManager

        public function State(){
            overrides = [];
            super();
        }
        public function initialize():void{
            var _local1:int;
            if (!initialized){
                initialized = true;
                _local1 = 0;
                while (_local1 < overrides.length) {
                    overrides[_local1].initialize();
                    _local1++;
                };
            };
        }
        public function dispatchExitState():void{
            dispatchEvent(new FlexEvent(FlexEvent.EXIT_STATE));
        }
        public function dispatchEnterState():void{
            dispatchEvent(new FlexEvent(FlexEvent.ENTER_STATE));
        }

    }
}//package com.degrafa.states 
