//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import com.redbox.changetech.model.*;
    import com.adobe.cairngorm.vo.*;

    public class Condition implements IValueObject {

        public var QuestionId:Number
        public var Operator:String
        public var Lower:int
        public var Upper:int
        public var OptionId:Number

        public static var IS:String = "Is";
        public static var NOT:String = "Not";
        public static var RANGE:String = "Range";

        private function evaluateIs():Boolean{
            var _local4:Answer;
            trace("evaluate IS-------------------");
            var _local1:Array = BalanceModelLocator.getInstance().getAnswersById(QuestionId);
            trace(("answers.length=" + _local1.length));
            if (_local1.length == 0){
                return (false);
            };
            var _local2:Boolean;
            var _local3:Number = 0;
            while (_local3 < _local1.length) {
                _local4 = _local1[_local3];
                trace(("answer.OptionId=" + _local4.OptionId));
                trace(("OptionId=" + OptionId));
                if (_local4.OptionId == OptionId){
                    trace("evaluateIsRETURNING TRUE");
                    return (true);
                };
                _local3++;
            };
            trace("evaluateIsRETURNING FALSE");
            return (_local2);
        }
        private function evaluateIsNot():Boolean{
            var _local4:Answer;
            trace("evaluate is NOT-------------------");
            var _local1:Array = BalanceModelLocator.getInstance().getAnswersById(QuestionId);
            trace(("answers.length=" + _local1.length));
            if (_local1.length == 0){
                return (true);
            };
            var _local2:Boolean;
            var _local3:Number = 0;
            while (_local3 < _local1.length) {
                _local4 = _local1[_local3];
                trace(("answer.OptionId=" + _local4.OptionId));
                trace(("OptionId=" + OptionId));
                if (_local4.OptionId == OptionId){
                    _local2 = false;
                    break;
                };
                _local3++;
            };
            return (_local2);
        }
        public function evaluate():Boolean{
            if (OptionId < 1){
                return (true);
            };
            switch (Operator){
                case IS:
                    return (evaluateIs());
                case NOT:
                    return (evaluateIsNot());
                case RANGE:
                    return (evaluateRange());
            };
            return (false);
        }
        private function evaluateRange():Boolean{
            var _local1:Answer = BalanceModelLocator.getInstance().getAnswerById(QuestionId);
            if (_local1 == null){
                return (true);
            };
            if ((((_local1.Score <= Upper)) && ((_local1.Score >= Lower)))){
                return (true);
            };
            return (false);
        }

    }
}//package com.redbox.changetech.vo 
