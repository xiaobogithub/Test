//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.modules {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.binding.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.redbox.changetech.event.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.Enumerations.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class ScreeningTest extends BasicModule implements IBindingClient {

        mx_internal var _bindingsBeginWithWord:Object
        public var firstQuestionScoreIsZero:Boolean
        mx_internal var _bindingsByDestination:Object
        mx_internal var _watchers:Array
        private var model:BalanceModelLocator
        mx_internal var _bindings:Array
        public var totalScore:Number
        private var callCollectionComplete:Boolean = false
        private var _documentDescriptor_:UIComponentDescriptor
        public var genderString:String

        public static var GREEN:String = "Green";
        private static var _watcherSetupUtil:IWatcherSetupUtil;
        private static var PHASE_1:String = "Phase1";
        private static var SCREENING_PATH:String = "ScreeningPath";
        private static var CONFIRM_FIRST_QUESTION_ACTION_STRING:String = "ConfirmFirstQuestion";
        private static var FIRST_QUESTION_ACTION_STRING:String = "FirstQuestion";
        public static var RED:String = "Red";
        private static var DECIDE_TRAFFIC_LIGHT_STRING:String = "DecideTrafficLights";
        public static var YELLOW:String = "Yellow";
        private static var IMMEDIATE_PHASE_1:String = "ImmediatePhase1";

        public function ScreeningTest(){
            _documentDescriptor_ = new UIComponentDescriptor({type:BasicModule});
            model = BalanceModelLocator.getInstance();
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.percentWidth = 100;
            this.percentHeight = 100;
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
            this.layout = "absolute";
            this.addEventListener("creationComplete", ___ScreeningTest_BasicModule1_creationComplete);
        }
        override public function next(_arg1:Event=null):Boolean{
            var _local2:Question;
            var _local3:Number;
            var _local4:Content;
            if (response.NextType == null){
                response.NextType = ContentType.ScreeningPart2b.Text;
            };
            if (content.Questions != null){
                if (content.Questions.length > 0){
                    _local2 = content.Questions[0];
                    trace(("question.Action=" + _local2.Action));
                    if (_local2.Action == SCREENING_PATH){
                        trace(("response.NextType=" + response.NextType));
                        if (response.NextType != ContentType.ScreeningPart2a.Text){
                            forceCollectionComplete = true;
                            trace(("collectionComplete=" + collectionComplete));
                            collectionComplete();
                            return (true);
                        };
                    };
                };
            };
            if (content.Tag == DECIDE_TRAFFIC_LIGHT_STRING){
                decideTrafficLights();
            };
            if (content != null){
                if (content.Questions != null){
                    if (content.Questions[0].Action == "Gender"){
                        setScreeningGender();
                        return (false);
                    };
                };
            };
            if (contentIndex < (contentCollection.Contents.length - 1)){
                _local3 = (contentIndex + 1);
                while (_local3 != contentIndex) {
                    _local4 = contentCollection.Contents[_local3];
                    if (_local4.Conditions == null){
                        if (_local4.Tag == null){
                            contentIndex = _local3;
                        } else {
                            if ((((_local4.Tag == currentTag)) || ((_local4.Tag == DECIDE_TRAFFIC_LIGHT_STRING)))){
                                contentIndex = _local3;
                            } else {
                                _local3++;
                            };
                        };
                    } else {
                        if (_local4.Conditions.evaluate()){
                            trace(("nextContent.Code=" + _local4));
                            contentIndex = _local3;
                        } else {
                            _local3++;
                        };
                    };
                };
                return (true);
                //unresolved jump
            };
            endScreening();
            return (false);
        }
        public function ___ScreeningTest_BasicModule1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        private function _ScreeningTest_bindingExprs():void{
            var _local1:*;
            _local1 = (content.Template) ? content.Template : "defaultState";
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _ScreeningTest_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_modules_ScreeningTestWatcherSetupUtil");
                var _local2 = watcherSetupUtilClass;
                _local2["init"](null);
            };
            _watcherSetupUtil.setup(this, function (_arg1:String){
                return (target[_arg1]);
            }, bindings, watchers);
            var i:* = 0;
            while (i < bindings.length) {
                Binding(bindings[i]).execute();
                i = (i + 1);
            };
            mx_internal::_bindings = mx_internal::_bindings.concat(bindings);
            mx_internal::_watchers = mx_internal::_watchers.concat(watchers);
            super.initialize();
        }
        private function setScreeningGender():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.SET_SCREENING_GENDER);
            _local1.data = genderString;
            _local1.dispatch();
        }
        private function _ScreeningTest_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():String{
                var _local1:* = (content.Template) ? content.Template : "defaultState";
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                this.currentState = _arg1;
            }, "this.currentState");
            result[0] = binding;
            return (result);
        }
        private function endScreening():void{
            var _local1:CairngormEvent = new CairngormEvent(BalanceController.COLLECTION_COMPLETE);
            _local1.data = new Object();
            _local1.data.response = response;
            _local1.dispatch();
        }
        private function getScoreFromCompletionUsage():Number{
            var _local9:Consumption;
            var _local10:Number;
            var _local1:Number = 0;
            var _local2:Number = 0;
            var _local3:Number = 0;
            while (_local3 < model.consumer.ReportedUsage.length) {
                _local9 = model.consumer.ReportedUsage[_local3];
                _local10 = Number(_local9.total(false));
                if (_local10 > 0){
                    _local2++;
                };
                _local1 = (_local1 + _local10);
                _local3++;
            };
            var _local4:Number = 0;
            var _local5:Number = 13;
            var _local6:Number = 14;
            var _local7:Number = 20;
            var _local8:Number = 21;
            switch (true){
                case (((_local1 >= 0)) && ((_local1 <= _local5))):
                    _local4 = (_local4 + 0);
                    break;
                case (((_local1 >= _local6)) && ((_local1 <= _local7))):
                    _local4 = (_local4 + 3);
                    break;
                case (_local1 > _local8):
                    _local4 = (_local4 + 7);
                    break;
            };
            if (_local2 > 6){
                _local4 = (_local4 + 3);
            };
            return (_local4);
        }
        private function init():void{
        }
        private function getFirstQuestionScoreisZero():Boolean{
            var _local3:Answer;
            var _local4:Question;
            var _local1:Boolean;
            var _local2:Number = 0;
            while (_local2 < response.Answers.length) {
                _local3 = response.Answers[_local2];
                _local4 = model.getQuestionById(_local3.QuestionId);
                if (_local4.Action == FIRST_QUESTION_ACTION_STRING){
                    if (_local3.Score == 0){
                        _local1 = true;
                    } else {
                        _local1 = false;
                    };
                };
                _local2++;
            };
            return (_local1);
        }
        private function decideTrafficLights():void{
            firstQuestionScoreIsZero = getFirstQuestionScoreisZero();
            totalScore = getTotalScore();
            var _local1:Number = model.consumer.getWeeksUsage();
            if (genderString == "Male"){
                switch (true){
                    case (_local1 <= 13):
                        totalScore = (totalScore + 0);
                        break;
                    case (((_local1 > 13)) && ((_local1 <= 20))):
                        totalScore = (totalScore + 3);
                        break;
                    case (_local1 >= 21):
                        totalScore = (totalScore + 7);
                        break;
                };
            } else {
                switch (true){
                    case (_local1 <= 8):
                        totalScore = (totalScore + 0);
                        break;
                    case (((_local1 > 8)) && ((_local1 <= 13))):
                        totalScore = (totalScore + 3);
                        break;
                    case (_local1 >= 14):
                        totalScore = (totalScore + 7);
                        break;
                };
            };
            var _local2:Number = model.consumer.getDaysDrinkingInWeek();
            if (_local2 > 5){
                totalScore = (totalScore + 3);
            };
            trace(("firstQuestionScoreIsZero=" + firstQuestionScoreIsZero));
            trace(("totalScore=" + totalScore));
            if (firstQuestionScoreIsZero){
                switch (true){
                    case (((totalScore >= 0)) && ((totalScore <= 2))):
                        currentTag = GREEN;
                        break;
                    case (((totalScore >= 3)) && ((totalScore <= 6))):
                        currentTag = YELLOW;
                        break;
                    case (((totalScore >= 7)) && ((totalScore <= 10))):
                        currentTag = RED;
                        break;
                };
            } else {
                switch (true){
                    case (((totalScore >= 0)) && ((totalScore <= 2))):
                        currentTag = GREEN;
                        break;
                    case (((totalScore >= 3)) && ((totalScore <= 9))):
                        currentTag = YELLOW;
                        break;
                    case (((totalScore >= 10)) && ((totalScore <= 26))):
                        currentTag = RED;
                        break;
                };
            };
            trace(("currentTag=" + currentTag));
            setScreeningScore(totalScore, currentTag);
        }
        private function setScreeningScore(_arg1:Number, _arg2:String):void{
            var _local3:CairngormEvent = new CairngormEvent(BalanceController.SET_SCREENINGSCORE_COMMAND);
            _local3.data = new Object();
            _local3.data.score = _arg1;
            _local3.data.color = _arg2;
            _local3.dispatch();
        }
        override protected function storeAnswer(_arg1:AnswerSelectedEvent):void{
            trace(("event.actionFlag+" + _arg1.actionFlag));
            switch (_arg1.actionFlag){
                case IMMEDIATE_PHASE_1:
                    response.NextType = ContentType.ScreeningPart2a.Text;
                    callCollectionComplete = true;
                    super.storeAnswer(_arg1);
                    return;
                case PHASE_1:
                    response.NextType = ContentType.ScreeningPart2a.Text;
                    super.storeAnswer(_arg1);
                    return;
            };
            if (content != null){
                if (content.Questions != null){
                    if (content.Questions[0].Action == "Gender"){
                        genderString = _arg1.actionFlag;
                    };
                };
            };
            super.storeAnswer(_arg1);
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            ScreeningTest._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.modules 
