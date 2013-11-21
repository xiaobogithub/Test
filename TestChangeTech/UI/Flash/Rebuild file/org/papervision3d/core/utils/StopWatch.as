//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.utils {
    import flash.events.*;
    import flash.utils.*;

    public class StopWatch extends EventDispatcher {

        private var startTime:int
        private var elapsedTime:int
        private var isRunning:Boolean
        private var stopTime:int

        public function start():void{
            if (!isRunning){
                startTime = getTimer();
                isRunning = true;
            };
        }
        public function stop():int{
            if (isRunning){
                stopTime = getTimer();
                elapsedTime = (stopTime - startTime);
                isRunning = false;
                return (elapsedTime);
            };
            return (0);
        }
        public function reset():void{
            isRunning = false;
        }

    }
}//package org.papervision3d.core.utils 
