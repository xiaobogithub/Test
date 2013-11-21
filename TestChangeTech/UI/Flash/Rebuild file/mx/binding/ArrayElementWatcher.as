//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding {

    public class ArrayElementWatcher extends Watcher {

        private var accessorFunc:Function
        public var arrayWatcher:Watcher
        private var document:Object

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ArrayElementWatcher(_arg1:Object, _arg2:Function, _arg3:Array){
            super(_arg3);
            this.document = _arg1;
            this.accessorFunc = _arg2;
        }
        override public function updateParent(_arg1:Object):void{
            var parent:* = _arg1;
            if (arrayWatcher.value != null){
                wrapUpdate(function ():void{
                    value = arrayWatcher.value[accessorFunc.apply(document)];
                    updateChildren();
                });
            };
        }
        override protected function shallowClone():Watcher{
            return (new ArrayElementWatcher(document, accessorFunc, listeners));
        }

    }
}//package mx.binding 
