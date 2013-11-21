//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import mx.controls.*;
    import com.redbox.changetech.model.*;
    import com.adobe.cairngorm.vo.*;

    public class ConditionCollection implements IValueObject {

        public var Lower:int
        public var Upper:int
        public var Conditions:Array
        public var Type:String

        public static var SPECIFIC_OR:String = "SpecificOr";
        public static var SPECIFIC_AND:String = "SpecificAnd";
        public static var RANGE:String = "Range";

        private function evaluateRange():Boolean{
            var _local1:Boolean;
            var _local2:int = getScore();
            if ((((_local2 >= Lower)) && ((_local2 <= Upper)))){
                _local1 = true;
            } else {
                _local1 = false;
            };
            return (_local1);
        }
        private function evaluateSpecificAnd():Boolean{
            var _local3:Condition;
            if (Conditions == null){
                return (true);
            };
            var _local1:Boolean;
            var _local2:Number = 0;
            while (_local2 < Conditions.length) {
                _local3 = Conditions[_local2];
                if (!_local3.evaluate()){
                    _local1 = false;
                };
                _local2++;
            };
            return (_local1);
        }
        private function evaluateSpecificOr():Boolean{
            var _local3:Condition;
            if (Conditions == null){
                return (true);
            };
            var _local1:Boolean;
            var _local2:Number = 0;
            while (_local2 < Conditions.length) {
                _local3 = Conditions[_local2];
                if (_local3.evaluate()){
                    trace("evaluateSpecificOrRETURNING TRUE");
                    _local1 = true;
                };
                _local2++;
            };
            trace(("evaluateSpecificOrRETURNING " + _local1));
            return (_local1);
        }
        public function evaluate():Boolean{
            trace("EVALUATING::::");
            trace(("Type=" + Type));
            switch (Type){
                case RANGE:
                    return (evaluateRange());
                case SPECIFIC_AND:
                    return (evaluateSpecificAnd());
                case SPECIFIC_OR:
                    return (evaluateSpecificOr());
            };
            return (false);
        }
        private function getScore():int{
            var _local3:Condition;
            var _local4:Answer;
            var _local1:int;
            if (Conditions == null){
                Alert.show("No conditions set for condition collection.");
                return (0);
            };
            if (Conditions.length == 0){
                Alert.show("No conditions set for condition collection.");
                return (0);
            };
            var _local2:Number = 0;
            while (_local2 < Conditions.length) {
                _local3 = Conditions[_local2];
                _local4 = BalanceModelLocator.getInstance().getAnswerById(_local3.QuestionId);
                if (_local3.evaluate()){
                    _local1 = (_local1 + int(_local4.Score));
                } else {
                    return (-1);
                };
                _local2++;
            };
            return (_local1);
        }

    }
}//package com.redbox.changetech.vo 
