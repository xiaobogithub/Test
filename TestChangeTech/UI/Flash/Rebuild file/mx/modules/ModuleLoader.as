//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.modules {
    import flash.display.*;
    import mx.core.*;
    import mx.events.*;
    import mx.containers.*;
    import flash.utils.*;
    import flash.system.*;

    public class ModuleLoader extends VBox implements IDeferredInstantiationUIComponent {

        private var loadRequested:Boolean = false
        private var module:IModuleInfo
        public var applicationDomain:ApplicationDomain
        private var _url:String = null
        public var child:DisplayObject

        mx_internal static const VERSION:String = "3.2.0.3958";

        private function moduleUnloadHandler(_arg1:ModuleEvent):void{
            dispatchEvent(_arg1);
        }
        public function unloadModule():void{
            if (child){
                removeChild(child);
                child = null;
            };
            if (module){
                module.removeEventListener(ModuleEvent.PROGRESS, moduleProgressHandler);
                module.removeEventListener(ModuleEvent.SETUP, moduleSetupHandler);
                module.removeEventListener(ModuleEvent.READY, moduleReadyHandler);
                module.removeEventListener(ModuleEvent.ERROR, moduleErrorHandler);
                module.unload();
                module.removeEventListener(ModuleEvent.UNLOAD, moduleUnloadHandler);
                module = null;
            };
        }
        private function moduleErrorHandler(_arg1:ModuleEvent):void{
            unloadModule();
            dispatchEvent(_arg1);
        }
        public function get url():String{
            return (_url);
        }
        private function moduleProgressHandler(_arg1:ModuleEvent):void{
            dispatchEvent(_arg1);
        }
        public function loadModule(_arg1:String=null, _arg2:ByteArray=null):void{
            if (_arg1 != null){
                _url = _arg1;
            };
            if (_url == null){
                return;
            };
            if (child){
                return;
            };
            if (module){
                return;
            };
            dispatchEvent(new FlexEvent(FlexEvent.LOADING));
            module = ModuleManager.getModule(_url);
            module.addEventListener(ModuleEvent.PROGRESS, moduleProgressHandler);
            module.addEventListener(ModuleEvent.SETUP, moduleSetupHandler);
            module.addEventListener(ModuleEvent.READY, moduleReadyHandler);
            module.addEventListener(ModuleEvent.ERROR, moduleErrorHandler);
            module.addEventListener(ModuleEvent.UNLOAD, moduleUnloadHandler);
            module.load(applicationDomain, null, _arg2);
        }
        override public function createComponentsFromDescriptors(_arg1:Boolean=true):void{
            super.createComponentsFromDescriptors(_arg1);
            loadRequested = true;
            loadModule();
        }
        private function moduleSetupHandler(_arg1:ModuleEvent):void{
            dispatchEvent(_arg1);
        }
        private function moduleReadyHandler(_arg1:ModuleEvent):void{
            var _local2:DisplayObjectContainer;
            child = (module.factory.create() as DisplayObject);
            dispatchEvent(_arg1);
            if (child){
                _local2 = parent;
                addChild(child);
            };
        }
        public function set url(_arg1:String):void{
            if (_arg1 == _url){
                return;
            };
            var _local2:Boolean;
            if (module){
                module.removeEventListener(ModuleEvent.PROGRESS, moduleProgressHandler);
                module.removeEventListener(ModuleEvent.SETUP, moduleSetupHandler);
                module.removeEventListener(ModuleEvent.READY, moduleReadyHandler);
                module.removeEventListener(ModuleEvent.ERROR, moduleErrorHandler);
                module.removeEventListener(ModuleEvent.UNLOAD, moduleUnloadHandler);
                module.release();
                module = null;
                if (child){
                    removeChild(child);
                    child = null;
                };
            };
            _url = _arg1;
            dispatchEvent(new FlexEvent(FlexEvent.URL_CHANGED));
            if (((!((_url == null))) && (loadRequested))){
                loadModule();
            };
        }

    }
}//package mx.modules 
