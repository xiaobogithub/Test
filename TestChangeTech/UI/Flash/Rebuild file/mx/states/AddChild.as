//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.states {
    import flash.display.*;
    import mx.core.*;
    import mx.resources.*;
    import mx.containers.*;

    public class AddChild implements IOverride {

        mx_internal var added:Boolean = false
        mx_internal var instanceCreated:Boolean = false
        private var _creationPolicy:String = "auto"
        public var relativeTo:UIComponent
        public var position:String
        private var _target:DisplayObject
        private var _targetFactory:IDeferredInstance
        private var resourceManager:IResourceManager

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function AddChild(_arg1:UIComponent=null, _arg2:DisplayObject=null, _arg3:String="lastChild"){
            resourceManager = ResourceManager.getInstance();
            super();
            this.relativeTo = _arg1;
            this.target = _arg2;
            this.position = _arg3;
        }
        public function remove(_arg1:UIComponent):void{
            var _local2:UIComponent = (relativeTo) ? relativeTo : _arg1;
            if (!added){
                return;
            };
            switch (position){
                case "before":
                case "after":
                    _local2.parent.removeChild(target);
                    break;
                case "firstChild":
                case "lastChild":
                default:
                    if ((((target is ControlBar)) && ((_local2 is Panel)))){
                        Panel(_local2).rawChildren.removeChild(target);
                        Panel(_local2).createComponentsFromDescriptors();
                    } else {
                        if ((((target is ApplicationControlBar)) && (ApplicationControlBar(target).dock))){
                            Application(_local2).dockControlBar(ApplicationControlBar(target), false);
                            Application(_local2).removeChild(target);
                        } else {
                            if (_local2 == target.parent){
                                _local2.removeChild(target);
                            };
                        };
                    };
                    break;
            };
            added = false;
        }
        public function initialize():void{
            if (creationPolicy == ContainerCreationPolicy.AUTO){
                createInstance();
            };
        }
        public function get target():DisplayObject{
            if (((!(_target)) && (!((creationPolicy == ContainerCreationPolicy.NONE))))){
                createInstance();
            };
            return (_target);
        }
        public function set creationPolicy(_arg1:String):void{
            _creationPolicy = _arg1;
            if (_creationPolicy == ContainerCreationPolicy.ALL){
                createInstance();
            };
        }
        public function set target(_arg1:DisplayObject):void{
            _target = _arg1;
        }
        public function apply(_arg1:UIComponent):void{
            var _local3:String;
            var _local2:UIComponent = (relativeTo) ? relativeTo : _arg1;
            added = false;
            if (!target){
                return;
            };
            if (target.parent){
                _local3 = resourceManager.getString("states", "alreadyParented");
                throw (new Error(_local3));
            };
            switch (position){
                case "before":
                    _local2.parent.addChildAt(target, _local2.parent.getChildIndex(_local2));
                    break;
                case "after":
                    _local2.parent.addChildAt(target, (_local2.parent.getChildIndex(_local2) + 1));
                    break;
                case "firstChild":
                    _local2.addChildAt(target, 0);
                    break;
                case "lastChild":
                default:
                    _local2.addChild(target);
                    if ((((target is ControlBar)) && ((_local2 is Panel)))){
                        Panel(_local2).createComponentsFromDescriptors();
                    } else {
                        if ((((((target is ApplicationControlBar)) && (ApplicationControlBar(target).dock))) && ((_local2 is Application)))){
                            ApplicationControlBar(target).resetDock(true);
                        };
                    };
                    break;
            };
            added = true;
        }
        public function createInstance():void{
            var _local1:Object;
            if (((((!(instanceCreated)) && (!(_target)))) && (targetFactory))){
                instanceCreated = true;
                _local1 = targetFactory.getInstance();
                if ((_local1 is DisplayObject)){
                    _target = DisplayObject(_local1);
                };
            };
        }
        public function set targetFactory(_arg1:IDeferredInstance):void{
            _targetFactory = _arg1;
            if (creationPolicy == ContainerCreationPolicy.ALL){
                createInstance();
            };
        }
        public function get creationPolicy():String{
            return (_creationPolicy);
        }
        public function get targetFactory():IDeferredInstance{
            return (_targetFactory);
        }

    }
}//package mx.states 
