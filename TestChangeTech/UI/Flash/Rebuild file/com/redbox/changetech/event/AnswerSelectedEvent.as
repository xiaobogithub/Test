//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.event {
    import flash.events.*;

    public class AnswerSelectedEvent extends Event {

        public var actionFlag:String
        public var label:String
        public var nextType:String
        public var optionId:Number
        public var callNext:Boolean
        public var score:Number
        public var text:String
        public var id:int
        public var description:String

        public static const ANSWER_SELECTED:String = "answerSelected";
        public static const ANSWER_DELETED:String = "answerDeleted";

        public function AnswerSelectedEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:int=-1, _arg5:Number=-1, _arg6:Number=-1, _arg7:String="", _arg8:Boolean=false){
            super(_arg1, _arg2, _arg3);
            this.id = _arg4;
            this.optionId = _arg5;
            this.score = _arg6;
            this.text = _arg7;
            this.callNext = _arg8;
        }
    }
}//package com.redbox.changetech.event 
