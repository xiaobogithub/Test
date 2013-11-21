//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects {
    import mx.effects.effectClasses.*;

    public class Resize extends TweenEffect {

        public var hideChildrenTargets:Array
        public var widthTo:Number
        public var heightTo:Number
        public var widthFrom:Number
        public var heightFrom:Number
        public var widthBy:Number
        public var heightBy:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var AFFECTED_PROPERTIES:Array = ["width", "height", "explicitWidth", "explicitHeight", "percentWidth", "percentHeight"];

        public function Resize(_arg1:Object=null){
            super(_arg1);
            instanceClass = ResizeInstance;
        }
        override protected function initInstance(_arg1:IEffectInstance):void{
            super.initInstance(_arg1);
            var _local2:ResizeInstance = ResizeInstance(_arg1);
            if (!isNaN(widthFrom)){
                _local2.widthFrom = widthFrom;
            };
            if (!isNaN(widthTo)){
                _local2.widthTo = widthTo;
            };
            if (!isNaN(widthBy)){
                _local2.widthBy = widthBy;
            };
            if (!isNaN(heightFrom)){
                _local2.heightFrom = heightFrom;
            };
            if (!isNaN(heightTo)){
                _local2.heightTo = heightTo;
            };
            if (!isNaN(heightBy)){
                _local2.heightBy = heightBy;
            };
            _local2.hideChildrenTargets = hideChildrenTargets;
        }
        override public function getAffectedProperties():Array{
            return (AFFECTED_PROPERTIES);
        }

    }
}//package mx.effects 
