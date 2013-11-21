//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {
    import flash.events.*;
    import mx.events.*;
    import mx.effects.*;

    public class CompositeEffectInstance extends EffectInstance {

        mx_internal var childSets:Array
        mx_internal var activeEffectQueue:Array
        private var _playheadTime:Number = 0
        mx_internal var timerTween:Tween
        mx_internal var endEffectCalled:Boolean

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function CompositeEffectInstance(_arg1:Object){
            activeEffectQueue = [];
            childSets = [];
            super(_arg1);
        }
        override public function get playheadTime():Number{
            return ((_playheadTime + super.playheadTime));
        }
        override mx_internal function get actualDuration():Number{
            var _local1:Number = NaN;
            if (repeatCount > 0){
                _local1 = (((durationWithoutRepeat * repeatCount) + ((repeatDelay * repeatCount) - 1)) + startDelay);
            };
            return (_local1);
        }
        override public function play():void{
            timerTween = new Tween(this, 0, 0, durationWithoutRepeat);
            super.play();
        }
        override public function finishEffect():void{
            activeEffectQueue = null;
            super.finishEffect();
        }
        mx_internal function hasRotateInstance():Boolean{
            var _local1:int;
            var _local2:CompositeEffectInstance;
            if (childSets){
                _local1 = 0;
                while (_local1 < childSets.length) {
                    if (childSets[_local1].length > 0){
                        _local2 = (childSets[_local1][0] as CompositeEffectInstance);
                        if ((((childSets[_local1][0] is RotateInstance)) || (((_local2) && (_local2.hasRotateInstance()))))){
                            return (true);
                        };
                    };
                    _local1++;
                };
            };
            return (false);
        }
        mx_internal function get durationWithoutRepeat():Number{
            return (0);
        }
        override public function initEffect(_arg1:Event):void{
            var _local4:Array;
            var _local5:int;
            var _local6:int;
            super.initEffect(_arg1);
            var _local2:int = childSets.length;
            var _local3:int;
            while (_local3 < _local2) {
                _local4 = childSets[_local3];
                _local5 = _local4.length;
                _local6 = 0;
                while (_local6 < _local5) {
                    _local4[_local6].initEffect(_arg1);
                    _local6++;
                };
                _local3++;
            };
        }
        override public function stop():void{
            super.stop();
            if (timerTween){
                timerTween.stop();
            };
        }
        override public function reverse():void{
            super.reverse();
            super.playReversed = !(playReversed);
            if (timerTween){
                timerTween.reverse();
            };
        }
        public function addChildSet(_arg1:Array):void{
            var _local2:int;
            var _local3:int;
            if (_arg1){
                _local2 = _arg1.length;
                if (_local2 > 0){
                    if (!childSets){
                        childSets = [_arg1];
                    } else {
                        childSets.push(_arg1);
                    };
                    _local3 = 0;
                    while (_local3 < _local2) {
                        _arg1[_local3].addEventListener(EffectEvent.EFFECT_END, effectEndHandler);
                        _arg1[_local3].parentCompositeEffectInstance = this;
                        _local3++;
                    };
                };
            };
        }
        protected function onEffectEnd(_arg1:IEffectInstance):void{
        }
        override mx_internal function playWithNoDuration():void{
            super.playWithNoDuration();
            end();
        }
        public function onTweenUpdate(_arg1:Object):void{
            _playheadTime = (timerTween) ? timerTween.playheadTime : _playheadTime;
        }
        override public function pause():void{
            super.pause();
            if (timerTween){
                timerTween.pause();
            };
        }
        mx_internal function effectEndHandler(_arg1:EffectEvent):void{
            onEffectEnd(_arg1.effectInstance);
        }
        override public function resume():void{
            super.resume();
            if (timerTween){
                timerTween.resume();
            };
        }
        public function onTweenEnd(_arg1:Object):void{
            _playheadTime = (timerTween) ? timerTween.playheadTime : _playheadTime;
        }

    }
}//package mx.effects.effectClasses 
