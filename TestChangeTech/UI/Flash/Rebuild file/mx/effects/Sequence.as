//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects {
    import mx.effects.effectClasses.*;

    public class Sequence extends CompositeEffect {

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function Sequence(_arg1:Object=null){
            super(_arg1);
            instanceClass = SequenceInstance;
        }
        override protected function initInstance(_arg1:IEffectInstance):void{
            super.initInstance(_arg1);
        }

    }
}//package mx.effects 
