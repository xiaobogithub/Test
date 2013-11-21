//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {
    import mx.core.*;
    import flash.events.*;
    import mx.effects.*;
    import flash.utils.*;

    public class ParallelInstance extends CompositeEffectInstance {

        private var timer:Timer
        private var isReversed:Boolean = false
        private var replayEffectQueue:Array
        private var doneEffectQueue:Array

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ParallelInstance(_arg1:Object){
            super(_arg1);
        }
        override public function stop():void{
            var _local1:Array;
            var _local2:int;
            var _local3:int;
            stopTimer();
            if (activeEffectQueue){
                _local1 = activeEffectQueue.concat();
                activeEffectQueue = null;
                _local2 = _local1.length;
                _local3 = 0;
                while (_local3 < _local2) {
                    if (_local1[_local3]){
                        _local1[_local3].stop();
                    };
                    _local3++;
                };
            };
            super.stop();
        }
        private function startTimer():void{
            if (!timer){
                timer = new Timer(10);
                timer.addEventListener(TimerEvent.TIMER, timerHandler);
            };
            timer.start();
        }
        override mx_internal function get durationWithoutRepeat():Number{
            var _local4:Array;
            var _local1:Number = 0;
            var _local2:int = childSets.length;
            var _local3:int;
            while (_local3 < _local2) {
                _local4 = childSets[_local3];
                _local1 = Math.max(_local4[0].actualDuration, _local1);
                _local3++;
            };
            return (_local1);
        }
        private function timerHandler(_arg1:TimerEvent):void{
            var _local5:EffectInstance;
            var _local2:Number = (durationWithoutRepeat - playheadTime);
            var _local3:int = replayEffectQueue.length;
            if (_local3 == 0){
                stopTimer();
                return;
            };
            var _local4:int = (_local3 - 1);
            while (_local4 >= 0) {
                _local5 = replayEffectQueue[_local4];
                if (_local2 <= _local5.actualDuration){
                    activeEffectQueue.push(_local5);
                    replayEffectQueue.splice(_local4, 1);
                    _local5.playReversed = playReversed;
                    _local5.startEffect();
                };
                _local4--;
            };
        }
        private function stopTimer():void{
            if (timer){
                timer.reset();
            };
        }
        override public function addChildSet(_arg1:Array):void{
            var _local2:CompositeEffectInstance;
            super.addChildSet(_arg1);
            if (_arg1.length > 0){
                _local2 = (_arg1[0] as CompositeEffectInstance);
                if ((((_arg1[0] is RotateInstance)) || (((!((_local2 == null))) && (_local2.hasRotateInstance()))))){
                    childSets.pop();
                    childSets.unshift(_arg1);
                };
            };
        }
        override public function reverse():void{
            var _local1:int;
            var _local2:int;
            super.reverse();
            if (isReversed){
                _local1 = activeEffectQueue.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    activeEffectQueue[_local2].reverse();
                    _local2++;
                };
                stopTimer();
            } else {
                replayEffectQueue = doneEffectQueue.splice(0);
                _local1 = activeEffectQueue.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    activeEffectQueue[_local2].reverse();
                    _local2++;
                };
                startTimer();
            };
            isReversed = !(isReversed);
        }
        override public function end():void{
            var _local1:Array;
            var _local2:int;
            var _local3:int;
            endEffectCalled = true;
            stopTimer();
            if (activeEffectQueue){
                _local1 = activeEffectQueue.concat();
                activeEffectQueue = null;
                _local2 = _local1.length;
                _local3 = 0;
                while (_local3 < _local2) {
                    if (_local1[_local3]){
                        _local1[_local3].end();
                    };
                    _local3++;
                };
            };
            super.end();
        }
        override protected function onEffectEnd(_arg1:IEffectInstance):void{
            if (Object(_arg1).suspendBackgroundProcessing){
                UIComponent.resumeBackgroundProcessing();
            };
            if (((endEffectCalled) || ((activeEffectQueue == null)))){
                return;
            };
            var _local2:int = activeEffectQueue.length;
            var _local3:int;
            while (_local3 < _local2) {
                if (_arg1 == activeEffectQueue[_local3]){
                    doneEffectQueue.push(_arg1);
                    activeEffectQueue.splice(_local3, 1);
                    break;
                };
                _local3++;
            };
            if (_local2 == 1){
                finishRepeat();
            };
        }
        override public function resume():void{
            super.resume();
            var _local1:int = activeEffectQueue.length;
            var _local2:int;
            while (_local2 < _local1) {
                activeEffectQueue[_local2].resume();
                _local2++;
            };
        }
        override public function play():void{
            var _local2:int;
            var _local3:int;
            var _local4:Array;
            var _local5:int;
            var _local6:int;
            var _local7:EffectInstance;
            var _local8:Array;
            doneEffectQueue = [];
            activeEffectQueue = [];
            replayEffectQueue = [];
            var _local1:Boolean;
            super.play();
            _local2 = childSets.length;
            _local3 = 0;
            while (_local3 < _local2) {
                _local4 = childSets[_local3];
                _local5 = _local4.length;
                _local6 = 0;
                while ((((_local6 < _local5)) && (!((activeEffectQueue == null))))) {
                    _local7 = _local4[_local6];
                    if (((playReversed) && ((_local7.actualDuration < durationWithoutRepeat)))){
                        replayEffectQueue.push(_local7);
                        startTimer();
                    } else {
                        _local7.playReversed = playReversed;
                        activeEffectQueue.push(_local7);
                    };
                    if (_local7.suspendBackgroundProcessing){
                        UIComponent.suspendBackgroundProcessing();
                    };
                    _local6++;
                };
                _local3++;
            };
            if (activeEffectQueue.length > 0){
                _local8 = activeEffectQueue.slice(0);
                _local3 = 0;
                while (_local3 < _local8.length) {
                    _local8[_local3].startEffect();
                    _local3++;
                };
            };
        }
        override public function pause():void{
            super.pause();
            var _local1:int = activeEffectQueue.length;
            var _local2:int;
            while (_local2 < _local1) {
                activeEffectQueue[_local2].pause();
                _local2++;
            };
        }

    }
}//package mx.effects.effectClasses 
