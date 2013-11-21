//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {
    import mx.effects.*;

    public class RotateInstance extends TweenEffectInstance {

        public var originX:Number
        public var originY:Number
        private var centerX:Number
        private var centerY:Number
        public var angleTo:Number = 360
        private var originalOffsetX:Number
        private var originalOffsetY:Number
        private var newX:Number
        private var newY:Number
        public var angleFrom:Number = 0

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function RotateInstance(_arg1:Object){
            super(_arg1);
        }
        override public function onTweenUpdate(_arg1:Object):void{
            if (Math.abs((newX - target.x)) > 0.1){
                centerX = (target.x + originalOffsetX);
            };
            if (Math.abs((newY - target.y)) > 0.1){
                centerY = (target.y + originalOffsetY);
            };
            var _local2:Number = Number(_arg1);
            var _local3:Number = ((Math.PI * _local2) / 180);
            EffectManager.suspendEventHandling();
            target.rotation = _local2;
            newX = ((centerX - (originX * Math.cos(_local3))) + (originY * Math.sin(_local3)));
            newY = ((centerY - (originX * Math.sin(_local3))) - (originY * Math.cos(_local3)));
            newX = Number(newX.toFixed(1));
            newY = Number(newY.toFixed(1));
            target.move(newX, newY);
            EffectManager.resumeEventHandling();
        }
        override public function play():void{
            super.play();
            var _local1:Number = ((Math.PI * target.rotation) / 180);
            if (isNaN(originX)){
                originX = (target.width / 2);
            };
            if (isNaN(originY)){
                originY = (target.height / 2);
            };
            centerX = ((target.x + (originX * Math.cos(_local1))) - (originY * Math.sin(_local1)));
            centerY = ((target.y + (originX * Math.sin(_local1))) + (originY * Math.cos(_local1)));
            if (isNaN(angleFrom)){
                angleFrom = target.rotation;
            };
            if (isNaN(angleTo)){
                angleTo = ((target.rotation)==0) ? ((angleFrom)>180) ? 360 : 0 : target.rotation;
            };
            tween = createTween(this, angleFrom, angleTo, duration);
            target.rotation = angleFrom;
            _local1 = ((Math.PI * angleFrom) / 180);
            EffectManager.suspendEventHandling();
            originalOffsetX = ((originX * Math.cos(_local1)) - (originY * Math.sin(_local1)));
            originalOffsetY = ((originX * Math.sin(_local1)) + (originY * Math.cos(_local1)));
            newX = Number((centerX - originalOffsetX).toFixed(1));
            newY = Number((centerY - originalOffsetY).toFixed(1));
            target.move(newX, newY);
            EffectManager.resumeEventHandling();
        }

    }
}//package mx.effects.effectClasses 
