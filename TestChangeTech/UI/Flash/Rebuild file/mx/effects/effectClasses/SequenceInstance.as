//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {
    import mx.core.*;
    import mx.effects.*;

    public class SequenceInstance extends CompositeEffectInstance {

        private var startTime:Number = 0
        private var currentInstanceDuration:Number = 0
        private var currentSetIndex:int = -1
        private var currentSet:Array
        private var activeChildCount:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function SequenceInstance(_arg1:Object){
            super(_arg1);
        }
        override public function stop():void{
            var _local1:Array;
            var _local2:Array;
            var _local3:int;
            var _local4:int;
            var _local5:int;
            var _local6:int;
            var _local7:Array;
            var _local8:int;
            var _local9:int;
            var _local10:IEffectInstance;
            if (((activeEffectQueue) && ((activeEffectQueue.length > 0)))){
                _local1 = activeEffectQueue.concat();
                activeEffectQueue = null;
                _local2 = _local1[currentSetIndex];
                _local3 = _local2.length;
                _local4 = 0;
                while (_local4 < _local3) {
                    _local2[_local4].stop();
                    _local4++;
                };
                _local5 = _local1.length;
                _local6 = (currentSetIndex + 1);
                while (_local6 < _local5) {
                    _local7 = _local1[_local6];
                    _local8 = _local7.length;
                    _local9 = 0;
                    while (_local9 < _local8) {
                        _local10 = _local7[_local9];
                        _local10.effect.deleteInstance(_local10);
                        _local9++;
                    };
                    _local6++;
                };
            };
            super.stop();
        }
        private function playNextChildSet(_arg1:Number=0):Boolean{
            var _local2:EffectInstance;
            if (!playReversed){
                if (((!(activeEffectQueue)) || ((currentSetIndex++ >= (activeEffectQueue.length - 1))))){
                    return (false);
                };
            } else {
                if (currentSetIndex-- <= 0){
                    return (false);
                };
            };
            var _local3:Array = activeEffectQueue[currentSetIndex];
            currentSet = [];
            var _local4:int;
            while (_local4 < _local3.length) {
                _local2 = _local3[_local4];
                currentSet.push(_local2);
                _local2.playReversed = playReversed;
                if (_local2.suspendBackgroundProcessing){
                    UIComponent.suspendBackgroundProcessing();
                };
                _local2.startEffect();
                _local4++;
            };
            currentInstanceDuration = (currentInstanceDuration + _local2.actualDuration);
            return (true);
        }
        override mx_internal function get durationWithoutRepeat():Number{
            var _local4:Array;
            var _local1:Number = 0;
            var _local2:int = childSets.length;
            var _local3:int;
            while (_local3 < _local2) {
                _local4 = childSets[_local3];
                _local1 = (_local1 + _local4[0].actualDuration);
                _local3++;
            };
            return (_local1);
        }
        override public function end():void{
            var _local1:Array;
            var _local2:Array;
            var _local3:int;
            var _local4:int;
            var _local5:int;
            var _local6:int;
            var _local7:Array;
            var _local8:int;
            var _local9:int;
            endEffectCalled = true;
            if (((activeEffectQueue) && ((activeEffectQueue.length > 0)))){
                _local1 = activeEffectQueue.concat();
                activeEffectQueue = null;
                _local2 = _local1[currentSetIndex];
                _local3 = _local2.length;
                _local4 = 0;
                while (_local4 < _local3) {
                    _local2[_local4].end();
                    _local4++;
                };
                _local5 = _local1.length;
                _local6 = (currentSetIndex + 1);
                while (_local6 < _local5) {
                    _local7 = _local1[_local6];
                    _local8 = _local7.length;
                    _local9 = 0;
                    while (_local9 < _local8) {
                        EffectInstance(_local7[_local9]).playWithNoDuration();
                        _local9++;
                    };
                    _local6++;
                };
            };
            super.end();
        }
        override public function reverse():void{
            var _local1:int;
            var _local2:int;
            super.reverse();
            if (((currentSet) && ((currentSet.length > 0)))){
                _local1 = currentSet.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    currentSet[_local2].reverse();
                    _local2++;
                };
            };
        }
        override protected function onEffectEnd(_arg1:IEffectInstance):void{
            if (Object(_arg1).suspendBackgroundProcessing){
                UIComponent.resumeBackgroundProcessing();
            };
            if (endEffectCalled){
                return;
            };
            var _local2:int;
            while (_local2 < currentSet.length) {
                if (_arg1 == currentSet[_local2]){
                    currentSet.splice(_local2, 1);
                    break;
                };
                _local2++;
            };
            if (currentSet.length == 0){
                if (false == playNextChildSet()){
                    finishRepeat();
                };
            };
        }
        override public function resume():void{
            var _local1:int;
            var _local2:int;
            super.resume();
            if (((currentSet) && ((currentSet.length > 0)))){
                _local1 = currentSet.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    currentSet[_local2].resume();
                    _local2++;
                };
            };
        }
        override public function play():void{
            var _local1:int;
            var _local2:int;
            var _local3:int;
            var _local4:int;
            var _local5:Array;
            activeEffectQueue = [];
            currentSetIndex = (playReversed) ? childSets.length : -1;
            _local1 = childSets.length;
            _local2 = 0;
            while (_local2 < _local1) {
                _local5 = childSets[_local2];
                activeEffectQueue.push(_local5);
                _local2++;
            };
            super.play();
            startTime = Tween.intervalTime;
            if (activeEffectQueue.length == 0){
                finishRepeat();
                return;
            };
            playNextChildSet();
        }
        override public function pause():void{
            var _local1:int;
            var _local2:int;
            super.pause();
            if (((currentSet) && ((currentSet.length > 0)))){
                _local1 = currentSet.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    currentSet[_local2].pause();
                    _local2++;
                };
            };
        }

    }
}//package mx.effects.effectClasses 
