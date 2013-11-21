//Created by Action Script Viewer - http://www.buraks.com/asv
package com.adobe.cairngorm.control {
    import flash.utils.*;
    import com.adobe.cairngorm.commands.*;
    import com.adobe.cairngorm.*;

    public class FrontController {

        protected var commands:Dictionary

        public function FrontController(){
            commands = new Dictionary();
            super();
        }
        protected function getCommand(_arg1:String):Class{
            var _local2:Class;
            _local2 = commands[_arg1];
            if (_local2 == null){
                throw (new CairngormError(CairngormMessageCodes.COMMAND_NOT_FOUND, _arg1));
            };
            return (_local2);
        }
        protected function executeCommand(_arg1:CairngormEvent):void{
            var _local2:Class;
            var _local3:ICommand;
            _local2 = getCommand(_arg1.type);
            _local3 = new (_local2);
            _local3.execute(_arg1);
        }
        public function addCommand(_arg1:String, _arg2:Class, _arg3:Boolean=true):void{
            if (commands[_arg1] != null){
                throw (new CairngormError(CairngormMessageCodes.COMMAND_ALREADY_REGISTERED, _arg1));
            };
            commands[_arg1] = _arg2;
            CairngormEventDispatcher.getInstance().addEventListener(_arg1, executeCommand, false, 0, _arg3);
        }
        public function removeCommand(_arg1:String):void{
            if (commands[_arg1] === null){
                throw (new CairngormError(CairngormMessageCodes.COMMAND_NOT_REGISTERED, _arg1));
            };
            CairngormEventDispatcher.getInstance().removeEventListener(_arg1, executeCommand);
            commands[_arg1] = null;
            delete commands[_arg1];
        }

    }
}//package com.adobe.cairngorm.control 
