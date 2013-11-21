//Created by Action Script Viewer - http://www.buraks.com/asv
package caurina.transitions {
    import flash.display.*;
    import flash.events.*;
    import flash.utils.*;

    public class Tweener {

        private static var _timeScale:Number = 1;
        private static var _specialPropertySplitterList:Object;
        private static var _engineExists:Boolean = false;
        private static var _specialPropertyModifierList:Object;
        private static var _currentTime:Number;
        private static var _tweenList:Array;
        private static var _specialPropertyList:Object;
        private static var _transitionList:Object;
        private static var _inited:Boolean = false;
        private static var __tweener_controller__:MovieClip;

        public function Tweener(){
            trace("Tweener is a static class and should not be instantiated.");
        }
        public static function registerSpecialPropertyModifier(_arg1:String, _arg2:Function, _arg3:Function):void{
            if (!_inited){
                init();
            };
            var _local4:SpecialPropertyModifier = new SpecialPropertyModifier(_arg2, _arg3);
            _specialPropertyModifierList[_arg1] = _local4;
        }
        public static function registerSpecialProperty(_arg1:String, _arg2:Function, _arg3:Function, _arg4:Array=null):void{
            if (!_inited){
                init();
            };
            var _local5:SpecialProperty = new SpecialProperty(_arg2, _arg3, _arg4);
            _specialPropertyList[_arg1] = _local5;
        }
        public static function addCaller(_arg1:Object=null, _arg2:Object=null):Boolean{
            var _local5:Number;
            var _local6:Number;
            var _local10:Function;
            var _local11:TweenListObj;
            var _local12:Number;
            var _local13:String;
            if ((((arguments.length < 2)) || ((arguments[0] == undefined)))){
                return (false);
            };
            var _local4:Array = new Array();
            if ((arguments[0] is Array)){
                _local5 = 0;
                while (_local5 < arguments[0].length) {
                    _local4.push(arguments[0][_local5]);
                    _local5++;
                };
            } else {
                _local5 = 0;
                while (_local5 < (arguments.length - 1)) {
                    _local4.push(arguments[_local5]);
                    _local5++;
                };
            };
            var _local7:Object = arguments[(arguments.length - 1)];
            if (!_inited){
                init();
            };
            if (((!(_engineExists)) || (!(Boolean(__tweener_controller__))))){
                startEngine();
            };
            var _local8:Number = (isNaN(_local7.time)) ? 0 : _local7.time;
            var _local9:Number = (isNaN(_local7.delay)) ? 0 : _local7.delay;
            if (typeof(_local7.transition) == "string"){
                _local13 = _local7.transition.toLowerCase();
                _local10 = _transitionList[_local13];
            } else {
                _local10 = _local7.transition;
            };
            if (!Boolean(_local10)){
                _local10 = _transitionList["easeoutexpo"];
            };
            _local5 = 0;
            while (_local5 < _local4.length) {
                _local11 = new TweenListObj(_local4[_local5], (_currentTime + ((_local9 * 1000) / _timeScale)), (_currentTime + (((_local9 * 1000) + (_local8 * 1000)) / _timeScale)), (_local7.useFrames == true), _local10);
                _local11.properties = null;
                _local11.onStart = _local7.onStart;
                _local11.onUpdate = _local7.onUpdate;
                _local11.onComplete = _local7.onComplete;
                _local11.onOverwrite = _local7.onOverwrite;
                _local11.onStartParams = _local7.onStartParams;
                _local11.onUpdateParams = _local7.onUpdateParams;
                _local11.onCompleteParams = _local7.onCompleteParams;
                _local11.onOverwriteParams = _local7.onOverwriteParams;
                _local11.isCaller = true;
                _local11.count = _local7.count;
                _local11.waitFrames = _local7.waitFrames;
                _tweenList.push(_local11);
                if ((((_local8 == 0)) && ((_local9 == 0)))){
                    _local12 = (_tweenList.length - 1);
                    updateTweenByIndex(_local12);
                    removeTweenByIndex(_local12);
                };
                _local5++;
            };
            return (true);
        }
        public static function init(_arg1=null):void{
            _inited = true;
            _transitionList = new Object();
            Equations.init();
            _specialPropertyList = new Object();
            _specialPropertyModifierList = new Object();
            _specialPropertySplitterList = new Object();
            SpecialPropertiesDefault.init();
        }
        private static function updateTweens():Boolean{
            var _local1:int;
            if (_tweenList.length == 0){
                return (false);
            };
            _local1 = 0;
            while (_local1 < _tweenList.length) {
                if ((((_tweenList[_local1] == undefined)) || (!(_tweenList[_local1].isPaused)))){
                    if (!updateTweenByIndex(_local1)){
                        removeTweenByIndex(_local1);
                    };
                    if (_tweenList[_local1] == null){
                        removeTweenByIndex(_local1, true);
                        _local1--;
                    };
                };
                _local1++;
            };
            return (true);
        }
        public static function removeTweens(_arg1:Object, ... _args):Boolean{
            var _local4:uint;
            var _local3:Array = new Array();
            _local4 = 0;
            while (_local4 < _args.length) {
                if ((((typeof(_args[_local4]) == "string")) && (!(AuxFunctions.isInArray(_args[_local4], _local3))))){
                    _local3.push(_args[_local4]);
                };
                _local4++;
            };
            return (affectTweens(removeTweenByIndex, _arg1, _local3));
        }
        public static function pauseAllTweens():Boolean{
            var _local2:uint;
            if (!Boolean(_tweenList)){
                return (false);
            };
            var _local1:Boolean;
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                pauseTweenByIndex(_local2);
                _local1 = true;
                _local2++;
            };
            return (_local1);
        }
        public static function splitTweens(_arg1:Number, _arg2:Array):uint{
            var _local5:uint;
            var _local6:String;
            var _local7:Boolean;
            var _local3:TweenListObj = _tweenList[_arg1];
            var _local4:TweenListObj = _local3.clone(false);
            _local5 = 0;
            while (_local5 < _arg2.length) {
                _local6 = _arg2[_local5];
                if (Boolean(_local3.properties[_local6])){
                    _local3.properties[_local6] = undefined;
                    delete _local3.properties[_local6];
                };
                _local5++;
            };
            for (_local6 in _local4.properties) {
                _local7 = false;
                _local5 = 0;
                while (_local5 < _arg2.length) {
                    if (_arg2[_local5] == _local6){
                        _local7 = true;
                        break;
                    };
                    _local5++;
                };
                if (!_local7){
                    _local4.properties[_local6] = undefined;
                    delete _local4.properties[_local6];
                };
            };
            _tweenList.push(_local4);
            return ((_tweenList.length - 1));
        }
        public static function resumeTweenByIndex(_arg1:Number):Boolean{
            var _local2:TweenListObj = _tweenList[_arg1];
            if ((((_local2 == null)) || (!(_local2.isPaused)))){
                return (false);
            };
            _local2.timeStart = (_local2.timeStart + (_currentTime - _local2.timePaused));
            _local2.timeComplete = (_local2.timeComplete + (_currentTime - _local2.timePaused));
            _local2.timePaused = undefined;
            _local2.isPaused = false;
            return (true);
        }
        public static function debug_getList():String{
            var _local2:uint;
            var _local3:uint;
            var _local1 = "";
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                _local1 = (_local1 + (("[" + _local2) + "] ::\n"));
                _local3 = 0;
                while (_local3 < _tweenList[_local2].properties.length) {
                    _local1 = (_local1 + (((("  " + _tweenList[_local2].properties[_local3].name) + " -> ") + _tweenList[_local2].properties[_local3].valueComplete) + "\n"));
                    _local3++;
                };
                _local2++;
            };
            return (_local1);
        }
        public static function getVersion():String{
            return ("AS3 1.26.62");
        }
        public static function onEnterFrame(_arg1:Event):void{
            updateTime();
            var _local2:Boolean;
            _local2 = updateTweens();
            if (!_local2){
                stopEngine();
            };
        }
        public static function updateTime():void{
            _currentTime = getTimer();
        }
        private static function updateTweenByIndex(_arg1:Number):Boolean{
            var tTweening:* = null;
            var mustUpdate:* = false;
            var nv:* = NaN;
            var t:* = NaN;
            var b:* = NaN;
            var c:* = NaN;
            var d:* = NaN;
            var pName:* = null;
            var tScope:* = null;
            var tProperty:* = null;
            var pv:* = NaN;
            var i:* = _arg1;
            tTweening = _tweenList[i];
            if ((((tTweening == null)) || (!(Boolean(tTweening.scope))))){
                return (false);
            };
            var isOver:* = false;
            if (_currentTime >= tTweening.timeStart){
                tScope = tTweening.scope;
                if (tTweening.isCaller){
                    do  {
                        t = (((tTweening.timeComplete - tTweening.timeStart) / tTweening.count) * (tTweening.timesCalled + 1));
                        b = tTweening.timeStart;
                        c = (tTweening.timeComplete - tTweening.timeStart);
                        d = (tTweening.timeComplete - tTweening.timeStart);
                        nv = tTweening.transition(t, b, c, d);
                    } while (!(_currentTime >= nv));
                } else {
                    mustUpdate = (((((tTweening.skipUpdates < 1)) || (!(tTweening.skipUpdates)))) || ((tTweening.updatesSkipped >= tTweening.skipUpdates)));
                    if (_currentTime >= tTweening.timeComplete){
                        isOver = true;
                        mustUpdate = true;
                    };
                    if (!tTweening.hasStarted){
                        if (Boolean(tTweening.onStart)){
                            try {
                                tTweening.onStart.apply(tScope, tTweening.onStartParams);
                            } catch(e:Error) {
                                handleError(tTweening, e, "onStart");
                            };
                        };
                        for (pName in tTweening.properties) {
                            pv = getPropertyValue(tScope, pName);
                            tTweening.properties[pName].valueStart = (isNaN(pv)) ? tTweening.properties[pName].valueComplete : pv;
                        };
                        mustUpdate = true;
                        tTweening.hasStarted = true;
                    };
                    if (mustUpdate){
                        for (pName in tTweening.properties) {
                            tProperty = tTweening.properties[pName];
                            if (isOver){
                                nv = tProperty.valueComplete;
                            } else {
                                if (tProperty.hasModifier){
                                    t = (_currentTime - tTweening.timeStart);
                                    d = (tTweening.timeComplete - tTweening.timeStart);
                                    nv = tTweening.transition(t, 0, 1, d);
                                    nv = tProperty.modifierFunction(tProperty.valueStart, tProperty.valueComplete, nv, tProperty.modifierParameters);
                                } else {
                                    t = (_currentTime - tTweening.timeStart);
                                    b = tProperty.valueStart;
                                    c = (tProperty.valueComplete - tProperty.valueStart);
                                    d = (tTweening.timeComplete - tTweening.timeStart);
                                    nv = tTweening.transition(t, b, c, d);
                                };
                            };
                            if (tTweening.rounded){
                                nv = Math.round(nv);
                            };
                            setPropertyValue(tScope, pName, nv);
                        };
                        tTweening.updatesSkipped = 0;
                        if (Boolean(tTweening.onUpdate)){
                            try {
                                tTweening.onUpdate.apply(tScope, tTweening.onUpdateParams);
                            } catch(e:Error) {
                                handleError(tTweening, e, "onUpdate");
                            };
                        };
                    } else {
                        tTweening.updatesSkipped++;
                    };
                };
                if (((isOver) && (Boolean(tTweening.onComplete)))){
                    try {
                        tTweening.onComplete.apply(tScope, tTweening.onCompleteParams);
                    } catch(e:Error) {
                        handleError(tTweening, e, "onComplete");
                    };
                };
                return (!(isOver));
            };
            return (true);
        }
        public static function setTimeScale(_arg1:Number):void{
            var _local2:Number;
            if (isNaN(_arg1)){
                _arg1 = 1;
            };
            if (_arg1 < 1E-5){
                _arg1 = 1E-5;
            };
            if (_arg1 != _timeScale){
                if (_tweenList != null){
                    _local2 = 0;
                    while (_local2 < _tweenList.length) {
                        _tweenList[_local2].timeStart = (_currentTime - (((_currentTime - _tweenList[_local2].timeStart) * _timeScale) / _arg1));
                        _tweenList[_local2].timeComplete = (_currentTime - (((_currentTime - _tweenList[_local2].timeComplete) * _timeScale) / _arg1));
                        if (_tweenList[_local2].timePaused != undefined){
                            _tweenList[_local2].timePaused = (_currentTime - (((_currentTime - _tweenList[_local2].timePaused) * _timeScale) / _arg1));
                        };
                        _local2++;
                    };
                };
                _timeScale = _arg1;
            };
        }
        public static function resumeAllTweens():Boolean{
            var _local2:uint;
            if (!Boolean(_tweenList)){
                return (false);
            };
            var _local1:Boolean;
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                resumeTweenByIndex(_local2);
                _local1 = true;
                _local2++;
            };
            return (_local1);
        }
        private static function handleError(_arg1:TweenListObj, _arg2:Error, _arg3:String):void{
            var pTweening:* = _arg1;
            var pError:* = _arg2;
            var pCallBackName:* = _arg3;
            if (((Boolean(pTweening.onError)) && ((pTweening.onError is Function)))){
                try {
                    pTweening.onError.apply(pTweening.scope, [pTweening.scope, pError]);
                } catch(metaError:Error) {
                    trace("## [Tweener] Error:", pTweening.scope, "raised an error while executing the 'onError' handler. Original error:\n", pError.getStackTrace(), "\nonError error:", metaError.getStackTrace());
                };
            } else {
                if (!Boolean(pTweening.onError)){
                    trace("## [Tweener] Error: :", pTweening.scope, (("raised an error while executing the'" + pCallBackName) + "'handler. \n"), pError.getStackTrace());
                };
            };
        }
        private static function startEngine():void{
            _engineExists = true;
            _tweenList = new Array();
            __tweener_controller__ = new MovieClip();
            __tweener_controller__.addEventListener(Event.ENTER_FRAME, Tweener.onEnterFrame);
            updateTime();
        }
        public static function removeAllTweens():Boolean{
            var _local2:uint;
            if (!Boolean(_tweenList)){
                return (false);
            };
            var _local1:Boolean;
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                removeTweenByIndex(_local2);
                _local1 = true;
                _local2++;
            };
            return (_local1);
        }
        public static function addTween(_arg1:Object=null, _arg2:Object=null):Boolean{
            var _local5:Number;
            var _local6:Number;
            var _local7:String;
            var _local8:String;
            var _local15:Function;
            var _local16:Object;
            var _local17:TweenListObj;
            var _local18:Number;
            var _local19:Array;
            var _local20:Array;
            var _local21:String;
            if ((((arguments.length < 2)) || ((arguments[0] == undefined)))){
                return (false);
            };
            var _local4:Array = new Array();
            if ((arguments[0] is Array)){
                _local5 = 0;
                while (_local5 < arguments[0].length) {
                    _local4.push(arguments[0][_local5]);
                    _local5++;
                };
            } else {
                _local5 = 0;
                while (_local5 < (arguments.length - 1)) {
                    _local4.push(arguments[_local5]);
                    _local5++;
                };
            };
            var _local9:Object = TweenListObj.makePropertiesChain(arguments[(arguments.length - 1)]);
            if (!_inited){
                init();
            };
            if (((!(_engineExists)) || (!(Boolean(__tweener_controller__))))){
                startEngine();
            };
            var _local10:Number = (isNaN(_local9.time)) ? 0 : _local9.time;
            var _local11:Number = (isNaN(_local9.delay)) ? 0 : _local9.delay;
            var _local12:Array = new Array();
            var _local13:Object = {time:true, delay:true, useFrames:true, skipUpdates:true, transition:true, onStart:true, onUpdate:true, onComplete:true, onOverwrite:true, rounded:true, onStartParams:true, onUpdateParams:true, onCompleteParams:true, onOverwriteParams:true};
            var _local14:Object = new Object();
            for (_local7 in _local9) {
                if (!_local13[_local7]){
                    if (_specialPropertySplitterList[_local7]){
                        _local19 = _specialPropertySplitterList[_local7].splitValues(_local9[_local7], _specialPropertySplitterList[_local7].parameters);
                        _local5 = 0;
                        while (_local5 < _local19.length) {
                            _local12[_local19[_local5].name] = {valueStart:undefined, valueComplete:_local19[_local5].value};
                            _local5++;
                        };
                    } else {
                        if (_specialPropertyModifierList[_local7] != undefined){
                            _local20 = _specialPropertyModifierList[_local7].modifyValues(_local9[_local7]);
                            _local5 = 0;
                            while (_local5 < _local20.length) {
                                _local14[_local20[_local5].name] = {modifierParameters:_local20[_local5].parameters, modifierFunction:_specialPropertyModifierList[_local7].getValue};
                                _local5++;
                            };
                        } else {
                            _local12[_local7] = {valueStart:undefined, valueComplete:_local9[_local7]};
                        };
                    };
                };
            };
            for (_local7 in _local14) {
                if (_local12[_local7] != undefined){
                    _local12[_local7].modifierParameters = _local14[_local7].modifierParameters;
                    _local12[_local7].modifierFunction = _local14[_local7].modifierFunction;
                };
            };
            if (typeof(_local9.transition) == "string"){
                _local21 = _local9.transition.toLowerCase();
                _local15 = _transitionList[_local21];
            } else {
                _local15 = _local9.transition;
            };
            if (!Boolean(_local15)){
                _local15 = _transitionList["easeoutexpo"];
            };
            _local5 = 0;
            while (_local5 < _local4.length) {
                _local16 = new Object();
                for (_local7 in _local12) {
                    _local16[_local7] = new PropertyInfoObj(_local12[_local7].valueStart, _local12[_local7].valueComplete, _local12[_local7].modifierFunction, _local12[_local7].modifierParameters);
                };
                _local17 = new TweenListObj(_local4[_local5], (_currentTime + ((_local11 * 1000) / _timeScale)), (_currentTime + (((_local11 * 1000) + (_local10 * 1000)) / _timeScale)), (_local9.useFrames == true), _local15);
                _local17.properties = _local16;
                _local17.onStart = _local9.onStart;
                _local17.onUpdate = _local9.onUpdate;
                _local17.onComplete = _local9.onComplete;
                _local17.onOverwrite = _local9.onOverwrite;
                _local17.onError = _local9.onError;
                _local17.onStartParams = _local9.onStartParams;
                _local17.onUpdateParams = _local9.onUpdateParams;
                _local17.onCompleteParams = _local9.onCompleteParams;
                _local17.onOverwriteParams = _local9.onOverwriteParams;
                _local17.rounded = _local9.rounded;
                _local17.skipUpdates = _local9.skipUpdates;
                removeTweensByTime(_local17.scope, _local17.properties, _local17.timeStart, _local17.timeComplete);
                _tweenList.push(_local17);
                if ((((_local10 == 0)) && ((_local11 == 0)))){
                    _local18 = (_tweenList.length - 1);
                    updateTweenByIndex(_local18);
                    removeTweenByIndex(_local18);
                };
                _local5++;
            };
            return (true);
        }
        public static function registerTransition(_arg1:String, _arg2:Function):void{
            if (!_inited){
                init();
            };
            _transitionList[_arg1] = _arg2;
        }
        private static function affectTweens(_arg1:Function, _arg2:Object, _arg3:Array):Boolean{
            var _local5:uint;
            var _local6:Array;
            var _local7:uint;
            var _local8:uint;
            var _local9:uint;
            var _local4:Boolean;
            if (!Boolean(_tweenList)){
                return (false);
            };
            _local5 = 0;
            while (_local5 < _tweenList.length) {
                if (((_tweenList[_local5]) && ((_tweenList[_local5].scope == _arg2)))){
                    if (_arg3.length == 0){
                        _arg1(_local5);
                        _local4 = true;
                    } else {
                        _local6 = new Array();
                        _local7 = 0;
                        while (_local7 < _arg3.length) {
                            if (Boolean(_tweenList[_local5].properties[_arg3[_local7]])){
                                _local6.push(_arg3[_local7]);
                            };
                            _local7++;
                        };
                        if (_local6.length > 0){
                            _local8 = AuxFunctions.getObjectLength(_tweenList[_local5].properties);
                            if (_local8 == _local6.length){
                                _arg1(_local5);
                                _local4 = true;
                            } else {
                                _local9 = splitTweens(_local5, _local6);
                                _arg1(_local9);
                                _local4 = true;
                            };
                        };
                    };
                };
                _local5++;
            };
            return (_local4);
        }
        public static function getTweens(_arg1:Object):Array{
            var _local2:uint;
            var _local3:String;
            if (!Boolean(_tweenList)){
                return ([]);
            };
            var _local4:Array = new Array();
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                if (_tweenList[_local2].scope == _arg1){
                    for (_local3 in _tweenList[_local2].properties) {
                        _local4.push(_local3);
                    };
                };
                _local2++;
            };
            return (_local4);
        }
        private static function setPropertyValue(_arg1:Object, _arg2:String, _arg3:Number):void{
            if (_specialPropertyList[_arg2] != undefined){
                if (Boolean(_specialPropertyList[_arg2].parameters)){
                    _specialPropertyList[_arg2].setValue(_arg1, _arg3, _specialPropertyList[_arg2].parameters);
                } else {
                    _specialPropertyList[_arg2].setValue(_arg1, _arg3);
                };
            } else {
                _arg1[_arg2] = _arg3;
            };
        }
        private static function getPropertyValue(_arg1:Object, _arg2:String):Number{
            if (_specialPropertyList[_arg2] != undefined){
                if (Boolean(_specialPropertyList[_arg2].parameters)){
                    return (_specialPropertyList[_arg2].getValue(_arg1, _specialPropertyList[_arg2].parameters));
                };
                return (_specialPropertyList[_arg2].getValue(_arg1));
                //unresolved jump
            };
            return (_arg1[_arg2]);
        }
        public static function isTweening(_arg1:Object):Boolean{
            var _local2:uint;
            if (!Boolean(_tweenList)){
                return (false);
            };
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                if (_tweenList[_local2].scope == _arg1){
                    return (true);
                };
                _local2++;
            };
            return (false);
        }
        public static function getTweenCount(_arg1:Object):Number{
            var _local2:uint;
            if (!Boolean(_tweenList)){
                return (0);
            };
            var _local3:Number = 0;
            _local2 = 0;
            while (_local2 < _tweenList.length) {
                if (_tweenList[_local2].scope == _arg1){
                    _local3 = (_local3 + AuxFunctions.getObjectLength(_tweenList[_local2].properties));
                };
                _local2++;
            };
            return (_local3);
        }
        private static function stopEngine():void{
            _engineExists = false;
            _tweenList = null;
            _currentTime = 0;
            __tweener_controller__.removeEventListener(Event.ENTER_FRAME, Tweener.onEnterFrame);
            __tweener_controller__ = null;
        }
        public static function pauseTweenByIndex(_arg1:Number):Boolean{
            var _local2:TweenListObj = _tweenList[_arg1];
            if ((((_local2 == null)) || (_local2.isPaused))){
                return (false);
            };
            _local2.timePaused = _currentTime;
            _local2.isPaused = true;
            return (true);
        }
        public static function removeTweensByTime(_arg1:Object, _arg2:Object, _arg3:Number, _arg4:Number):Boolean{
            var removedLocally:* = false;
            var i:* = 0;
            var pName:* = null;
            var p_scope:* = _arg1;
            var p_properties:* = _arg2;
            var p_timeStart:* = _arg3;
            var p_timeComplete:* = _arg4;
            var removed:* = false;
            var tl:* = _tweenList.length;
            i = 0;
            while (i < tl) {
                if (((Boolean(_tweenList[i])) && ((p_scope == _tweenList[i].scope)))){
                    if ((((p_timeComplete > _tweenList[i].timeStart)) && ((p_timeStart < _tweenList[i].timeComplete)))){
                        removedLocally = false;
                        for (pName in _tweenList[i].properties) {
                            if (Boolean(p_properties[pName])){
                                if (Boolean(_tweenList[i].onOverwrite)){
                                    try {
                                        _tweenList[i].onOverwrite.apply(_tweenList[i].scope, _tweenList[i].onOverwriteParams);
                                    } catch(e:Error) {
                                        handleError(_tweenList[i], e, "onOverwrite");
                                    };
                                };
                                _tweenList[i].properties[pName] = undefined;
                                delete _tweenList[i].properties[pName];
                                removedLocally = true;
                                removed = true;
                            };
                        };
                        if (removedLocally){
                            if (AuxFunctions.getObjectLength(_tweenList[i].properties) == 0){
                                removeTweenByIndex(i);
                            };
                        };
                    };
                };
                i = (i + 1);
            };
            return (removed);
        }
        public static function registerSpecialPropertySplitter(_arg1:String, _arg2:Function, _arg3:Array=null):void{
            if (!_inited){
                init();
            };
            var _local4:SpecialPropertySplitter = new SpecialPropertySplitter(_arg2, _arg3);
            _specialPropertySplitterList[_arg1] = _local4;
        }
        public static function removeTweenByIndex(_arg1:Number, _arg2:Boolean=false):Boolean{
            _tweenList[_arg1] = null;
            if (_arg2){
                _tweenList.splice(_arg1, 1);
            };
            return (true);
        }
        public static function resumeTweens(_arg1:Object, ... _args):Boolean{
            var _local4:uint;
            var _local3:Array = new Array();
            _local4 = 0;
            while (_local4 < _args.length) {
                if ((((typeof(_args[_local4]) == "string")) && (!(AuxFunctions.isInArray(_args[_local4], _local3))))){
                    _local3.push(_args[_local4]);
                };
                _local4++;
            };
            return (affectTweens(resumeTweenByIndex, _arg1, _local3));
        }
        public static function pauseTweens(_arg1:Object, ... _args):Boolean{
            var _local4:uint;
            var _local3:Array = new Array();
            _local4 = 0;
            while (_local4 < _args.length) {
                if ((((typeof(_args[_local4]) == "string")) && (!(AuxFunctions.isInArray(_args[_local4], _local3))))){
                    _local3.push(_args[_local4]);
                };
                _local4++;
            };
            return (affectTweens(pauseTweenByIndex, _arg1, _local3));
        }

    }
}//package caurina.transitions 
