//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects {
    import mx.effects.effectClasses.*;

    public class CompositeEffect extends Effect {

        private var _affectedProperties:Array
        private var childTargets:Array
        public var children:Array

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function CompositeEffect(_arg1:Object=null){
            children = [];
            super(_arg1);
            instanceClass = CompositeEffectInstance;
        }
        override public function createInstances(_arg1:Array=null):Array{
            if (!_arg1){
                _arg1 = this.targets;
            };
            childTargets = _arg1;
            var _local2:IEffectInstance = createInstance();
            childTargets = null;
            return ((_local2) ? [_local2] : []);
        }
        override protected function initInstance(_arg1:IEffectInstance):void{
            var _local4:int;
            var _local5:int;
            var _local6:Effect;
            super.initInstance(_arg1);
            var _local2:CompositeEffectInstance = CompositeEffectInstance(_arg1);
            var _local3:Object = childTargets;
            if (!(_local3 is Array)){
                _local3 = [_local3];
            };
            if (children){
                _local4 = children.length;
                _local5 = 0;
                while (_local5 < _local4) {
                    _local6 = children[_local5];
                    if (propertyChangesArray != null){
                        _local6.propertyChangesArray = propertyChangesArray;
                    };
                    if ((((_local6.filterObject == null)) && (filterObject))){
                        _local6.filterObject = filterObject;
                    };
                    if (effectTargetHost){
                        _local6.effectTargetHost = effectTargetHost;
                    };
                    if (_local6.targets.length == 0){
                        _local2.addChildSet(children[_local5].createInstances(_local3));
                    } else {
                        _local2.addChildSet(children[_local5].createInstances(_local6.targets));
                    };
                    _local5++;
                };
            };
        }
        override mx_internal function captureValues(_arg1:Array, _arg2:Boolean):Array{
            var _local5:Effect;
            var _local3:int = children.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local5 = children[_local4];
                _arg1 = _local5.captureValues(_arg1, _arg2);
                _local4++;
            };
            return (_arg1);
        }
        public function addChild(_arg1:IEffect):void{
            children.push(_arg1);
            _affectedProperties = null;
        }
        override mx_internal function applyStartValues(_arg1:Array, _arg2:Array):void{
            var _local5:Effect;
            var _local6:Array;
            var _local3:int = children.length;
            var _local4:int;
            while (_local4 < _local3) {
                _local5 = children[_local4];
                _local6 = ((_local5.targets.length > 0)) ? _local5.targets : _arg2;
                if ((((_local5.filterObject == null)) && (filterObject))){
                    _local5.filterObject = filterObject;
                };
                _local5.applyStartValues(_arg1, _local6);
                _local4++;
            };
        }
        override public function createInstance(_arg1:Object=null):IEffectInstance{
            if (!childTargets){
                childTargets = [_arg1];
            };
            var _local2:IEffectInstance = super.createInstance(_arg1);
            childTargets = null;
            return (_local2);
        }
        override protected function filterInstance(_arg1:Array, _arg2:Object):Boolean{
            var _local3:Array;
            var _local4:int;
            var _local5:int;
            if (filterObject){
                _local3 = targets;
                if (_local3.length == 0){
                    _local3 = childTargets;
                };
                _local4 = _local3.length;
                _local5 = 0;
                while (_local5 < _local4) {
                    if (filterObject.filterInstance(_arg1, effectTargetHost, _local3[_local5])){
                        return (true);
                    };
                    _local5++;
                };
                return (false);
            };
            return (true);
        }
        override public function captureStartValues():void{
            var _local1:Array = getChildrenTargets();
            propertyChangesArray = [];
            var _local2:int = _local1.length;
            var _local3:int;
            while (_local3 < _local2) {
                propertyChangesArray.push(new PropertyChanges(_local1[_local3]));
                _local3++;
            };
            propertyChangesArray = captureValues(propertyChangesArray, true);
            endValuesCaptured = false;
        }
        private function getChildrenTargets():Array{
            var _local3:Array;
            var _local4:Effect;
            var _local5:int;
            var _local6:int;
            var _local7:int;
            var _local8:int;
            var _local9:String;
            var _local1:Array = [];
            var _local2:Object = {};
            _local5 = children.length;
            _local6 = 0;
            while (_local6 < _local5) {
                _local4 = children[_local6];
                if ((_local4 is CompositeEffect)){
                    _local3 = CompositeEffect(_local4).getChildrenTargets();
                    _local7 = _local3.length;
                    _local8 = 0;
                    while (_local8 < _local7) {
                        if (_local3[_local8] != null){
                            _local2[_local3[_local8].toString()] = _local3[_local8];
                        };
                        _local8++;
                    };
                } else {
                    if (_local4.targets != null){
                        _local7 = _local4.targets.length;
                        _local8 = 0;
                        while (_local8 < _local7) {
                            if (_local4.targets[_local8] != null){
                                _local2[_local4.targets[_local8].toString()] = _local4.targets[_local8];
                            };
                            _local8++;
                        };
                    };
                };
                _local6++;
            };
            _local5 = targets.length;
            _local6 = 0;
            while (_local6 < _local5) {
                if (targets[_local6] != null){
                    _local2[targets[_local6].toString()] = targets[_local6];
                };
                _local6++;
            };
            for (_local9 in _local2) {
                _local1.push(_local2[_local9]);
            };
            return (_local1);
        }
        override public function getAffectedProperties():Array{
            var _local1:Array;
            var _local2:int;
            var _local3:int;
            if (!_affectedProperties){
                _local1 = [];
                _local2 = children.length;
                _local3 = 0;
                while (_local3 < _local2) {
                    _local1 = _local1.concat(children[_local3].getAffectedProperties());
                    _local3++;
                };
                _affectedProperties = _local1;
            };
            return (_affectedProperties);
        }

    }
}//package mx.effects 
