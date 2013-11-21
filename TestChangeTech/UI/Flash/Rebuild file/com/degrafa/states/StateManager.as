//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.states {
    import mx.events.*;

    public class StateManager {

        private var requestedCurrentState:String
        private var _currentState:String
        private var stateClient:IDegrafaStateClient
        private var _currentStateChanged:Boolean

        public function StateManager(_arg1:IDegrafaStateClient){
            this.stateClient = _arg1;
        }
        public function findCommonBaseState(_arg1:String, _arg2:String):String{
            var _local3:State = getState(_arg1);
            var _local4:State = getState(_arg2);
            if (((!(_local3)) || (!(_local4)))){
                return ("");
            };
            if (((isBaseState(_local3.basedOn)) && (isBaseState(_local4.basedOn)))){
                return ("");
            };
            var _local5:Array = getBaseStates(_local3);
            var _local6:Array = getBaseStates(_local4);
            var _local7 = "";
            while (_local5[(_local5.length - 1)] == _local6[(_local6.length - 1)]) {
                _local7 = _local5.pop();
                _local6.pop();
                if (((!(_local5.length)) || (!(_local6.length)))){
                    break;
                };
            };
            if (((_local5.length) && ((_local5[(_local5.length - 1)] == _local4.name)))){
                _local7 = _local4.name;
            } else {
                if (((_local6.length) && ((_local6[(_local6.length - 1)] == _local3.name)))){
                    _local7 = _local3.name;
                };
            };
            return (_local7);
        }
        public function initializeState(_arg1:String):void{
            var _local2:State = getState(_arg1);
            while (_local2) {
                _local2 = getState(_local2.basedOn);
            };
        }
        public function getBaseStates(_arg1:State):Array{
            var _local2:Array = [];
            while (((_arg1) && (_arg1.basedOn))) {
                _local2.push(_arg1.basedOn);
                _arg1 = getState(_arg1.basedOn);
            };
            return (_local2);
        }
        public function applyState(_arg1:String, _arg2:String):void{
            var _local4:Array;
            var _local5:int;
            var _local3:State = getState(_arg1);
            if (_arg1 == _arg2){
                return;
            };
            if (_local3){
                if (_local3.basedOn != _arg2){
                    applyState(_local3.basedOn, _arg2);
                };
                _local4 = _local3.overrides;
                _local5 = 0;
                while (_local5 < _local4.length) {
                    _local4[_local5].apply(stateClient);
                    _local5++;
                };
            };
        }
        public function removeState(_arg1:String, _arg2:String):void{
            var _local4:Array;
            var _local5:int;
            var _local3:State = getState(_arg1);
            if (_arg1 == _arg2){
                return;
            };
            if (_local3){
                _local3.dispatchExitState();
                _local4 = _local3.overrides;
                _local5 = _local4.length;
                while (_local5) {
                    _local4[(_local5 - 1)].remove(stateClient);
                    _local5--;
                };
                if (_local3.basedOn != _arg2){
                    removeState(_local3.basedOn, _arg2);
                };
            };
        }
        public function commitCurrentState():void{
            var _local2:StateChangeEvent;
            var _local1:String = findCommonBaseState(_currentState, requestedCurrentState);
            var _local3:String = (_currentState) ? _currentState : "";
            var _local4:State = getState(requestedCurrentState);
            initializeState(requestedCurrentState);
            _local2 = new StateChangeEvent(StateChangeEvent.CURRENT_STATE_CHANGING);
            _local2.oldState = _local3;
            _local2.newState = (requestedCurrentState) ? requestedCurrentState : "";
            stateClient.dispatchEvent(_local2);
            if (isBaseState(_currentState)){
                stateClient.dispatchEvent(new FlexEvent(FlexEvent.EXIT_STATE));
            };
            removeState(_currentState, _local1);
            _currentState = requestedCurrentState;
            if (isBaseState(currentState)){
                stateClient.dispatchEvent(new FlexEvent(FlexEvent.ENTER_STATE));
            } else {
                applyState(_currentState, _local1);
            };
            _local2 = new StateChangeEvent(StateChangeEvent.CURRENT_STATE_CHANGE);
            _local2.oldState = _local3;
            _local2.newState = (_currentState) ? _currentState : "";
            stateClient.dispatchEvent(_local2);
        }
        public function set currentState(_arg1:String):void{
            setCurrentState(_arg1);
        }
        public function isBaseState(_arg1:String):Boolean{
            return (((!(_arg1)) || ((_arg1 == ""))));
        }
        public function setCurrentState(_arg1:String):void{
            if (((!((_arg1 == currentState))) && (!(((isBaseState(_arg1)) && (isBaseState(currentState))))))){
                requestedCurrentState = _arg1;
                if (stateClient.isInitialized){
                    commitCurrentState();
                } else {
                    _currentStateChanged = true;
                };
            };
        }
        public function getState(_arg1:String):State{
            if (((!(stateClient.states)) || (isBaseState(_arg1)))){
                return (null);
            };
            var _local2:int;
            while (_local2 < stateClient.states.length) {
                if (stateClient.states[_local2].name == _arg1){
                    return (stateClient.states[_local2]);
                };
                _local2++;
            };
            return (null);
        }
        public function get currentState():String{
            return ((_currentStateChanged) ? requestedCurrentState : _currentState);
        }

    }
}//package com.degrafa.states 
