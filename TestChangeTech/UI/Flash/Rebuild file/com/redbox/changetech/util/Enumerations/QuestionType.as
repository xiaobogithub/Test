//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.util.Enumerations {

    public class QuestionType {

        public var Text:String

        public static const Slider:QuestionType = new (QuestionType);
;
        public static const SingleLine:QuestionType = new (QuestionType);
;
        public static const RadioButton:QuestionType = new (QuestionType);
;
        public static const TimeInputQuestion:QuestionType = new (QuestionType);
;
        public static const Numeric:QuestionType = new (QuestionType);
;
        public static const Stopwatch:QuestionType = new (QuestionType);
;
        public static const EditableList:QuestionType = new (QuestionType);
;
        public static const MultiLineText:QuestionType = new (QuestionType);
;
        public static const Select:QuestionType = new (QuestionType);
;

        CStringUtils.InitEnumConstants(QuestionType);
    }
}//package com.redbox.changetech.util.Enumerations 
