//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {

    public class TweenListObj {

        public var hasStarted:Boolean
        public var onUpdate:Function
        public var useFrames:Boolean
        public var count:Number
        public var onOverwriteParams:Array
        public var timeStart:Number
        public var auxProperties:Object
        public var timeComplete:Number
        public var onStartParams:Array
        public var rounded:Boolean
        public var updatesSkipped:Number
        public var onUpdateParams:Array
        public var onComplete:Function
        public var properties:Object
        public var onStart:Function
        public var skipUpdates:Number
        public var scope:Object
        public var isCaller:Boolean
        public var timePaused:Number
        public var transition:Function
        public var onCompleteParams:Array
        public var onError:Function
        public var timesCalled:Number
        public var onOverwrite:Function
        public var isPaused:Boolean
        public var waitFrames:Boolean

        public function TweenListObj(_arg1:Object, _arg2:Number, _arg3:Number, _arg4:Boolean, _arg5:Function){
            scope = _arg1;
            timeStart = _arg2;
            timeComplete = _arg3;
            useFrames = _arg4;
            transition = _arg5;
            auxProperties = new Object();
            properties = new Object();
            isPaused = false;
            timePaused = undefined;
            isCaller = false;
            updatesSkipped = 0;
            timesCalled = 0;
            skipUpdates = 0;
            hasStarted = false;
        }
        public function clone(_arg1:Boolean):TweenListObj{
            var _local3:String;
            var _local2:TweenListObj = new TweenListObj(scope, timeStart, timeComplete, useFrames, transition);
            _local2.properties = new Array();
            for (_local3 in properties) {
                _local2.properties[_local3] = properties[_local3].clone();
            };
            _local2.skipUpdates = skipUpdates;
            _local2.updatesSkipped = updatesSkipped;
            if (!_arg1){
                _local2.onStart = onStart;
                _local2.onUpdate = onUpdate;
                _local2.onComplete = onComplete;
                _local2.onOverwrite = onOverwrite;
                _local2.onError = onError;
                _local2.onStartParams = onStartParams;
                _local2.onUpdateParams = onUpdateParams;
                _local2.onCompleteParams = onCompleteParams;
                _local2.onOverwriteParams = onOverwriteParams;
            };
            _local2.rounded = rounded;
            _local2.isPaused = isPaused;
            _local2.timePaused = timePaused;
            _local2.isCaller = isCaller;
            _local2.count = count;
            _local2.timesCalled = timesCalled;
            _local2.waitFrames = waitFrames;
            _local2.hasStarted = hasStarted;
            return (_local2);
        }
        public function toString():String{
            var _local1 = "\n[TweenListObj ";
            _local1 = (_local1 + ("scope:" + String(scope)));
            _local1 = (_local1 + ", properties:");
            var _local2:uint;
            while (_local2 < properties.length) {
                if (_local2 > 0){
                    _local1 = (_local1 + ",");
                };
                _local1 = (_local1 + ("[name:" + properties[_local2].name));
                _local1 = (_local1 + (",valueStart:" + properties[_local2].valueStart));
                _local1 = (_local1 + (",valueComplete:" + properties[_local2].valueComplete));
                _local1 = (_local1 + "]");
                _local2++;
            };
            _local1 = (_local1 + (", timeStart:" + String(timeStart)));
            _local1 = (_local1 + (", timeComplete:" + String(timeComplete)));
            _local1 = (_local1 + (", useFrames:" + String(useFrames)));
            _local1 = (_local1 + (", transition:" + String(transition)));
            if (skipUpdates){
                _local1 = (_local1 + (", skipUpdates:" + String(skipUpdates)));
            };
            if (updatesSkipped){
                _local1 = (_local1 + (", updatesSkipped:" + String(updatesSkipped)));
            };
            if (Boolean(onStart)){
                _local1 = (_local1 + (", onStart:" + String(onStart)));
            };
            if (Boolean(onUpdate)){
                _local1 = (_local1 + (", onUpdate:" + String(onUpdate)));
            };
            if (Boolean(onComplete)){
                _local1 = (_local1 + (", onComplete:" + String(onComplete)));
            };
            if (Boolean(onOverwrite)){
                _local1 = (_local1 + (", onOverwrite:" + String(onOverwrite)));
            };
            if (Boolean(onError)){
                _local1 = (_local1 + (", onError:" + String(onError)));
            };
            if (onStartParams){
                _local1 = (_local1 + (", onStartParams:" + String(onStartParams)));
            };
            if (onUpdateParams){
                _local1 = (_local1 + (", onUpdateParams:" + String(onUpdateParams)));
            };
            if (onCompleteParams){
                _local1 = (_local1 + (", onCompleteParams:" + String(onCompleteParams)));
            };
            if (onOverwriteParams){
                _local1 = (_local1 + (", onOverwriteParams:" + String(onOverwriteParams)));
            };
            if (rounded){
                _local1 = (_local1 + (", rounded:" + String(rounded)));
            };
            if (isPaused){
                _local1 = (_local1 + (", isPaused:" + String(isPaused)));
            };
            if (timePaused){
                _local1 = (_local1 + (", timePaused:" + String(timePaused)));
            };
            if (isCaller){
                _local1 = (_local1 + (", isCaller:" + String(isCaller)));
            };
            if (count){
                _local1 = (_local1 + (", count:" + String(count)));
            };
            if (timesCalled){
                _local1 = (_local1 + (", timesCalled:" + String(timesCalled)));
            };
            if (waitFrames){
                _local1 = (_local1 + (", waitFrames:" + String(waitFrames)));
            };
            if (hasStarted){
                _local1 = (_local1 + (", hasStarted:" + String(hasStarted)));
            };
            _local1 = (_local1 + "]\n");
            return (_local1);
        }

        public static function makePropertiesChain(_arg1:Object):Object{
            var _local3:Object;
            var _local4:Object;
            var _local5:Object;
            var _local6:Number;
            var _local7:Number;
            var _local8:Number;
            var _local2:Object = _arg1.base;
            if (_local2){
                _local3 = {};
                if ((_local2 is Array)){
                    _local4 = [];
                    _local8 = 0;
                    while (_local8 < _local2.length) {
                        _local4.push(_local2[_local8]);
                        _local8++;
                    };
                } else {
                    _local4 = [_local2];
                };
                _local4.push(_arg1);
                _local6 = _local4.length;
                _local7 = 0;
                while (_local7 < _local6) {
                    if (_local4[_local7]["base"]){
                        _local5 = AuxFunctions.concatObjects(makePropertiesChain(_local4[_local7]["base"]), _local4[_local7]);
                    } else {
                        _local5 = _local4[_local7];
                    };
                    _local3 = AuxFunctions.concatObjects(_local3, _local5);
                    _local7++;
                };
                if (_local3["base"]){
                    delete _local3["base"];
                };
                return (_local3);
                //unresolved jump
            };
            return (_arg1);
        }

    }
}//package caurina.transitions 
