//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.modules {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.controls.*;
    import com.redbox.changetech.control.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import com.adobe.cairngorm.control.*;
    import mx.modules.*;
    import com.redbox.changetech.event.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class AbstractBalanceModule extends Module implements IBalanceModule {

        protected var t:Timer
        protected var _isModuleActive:Boolean = false
        private var _2075616051mandatoryQuestionsComplete:Boolean
        protected var _collectionComplete:Boolean
        protected var _response:Response
        protected var _contentIndex:Number
        protected var _content:Content
        protected var _ctaBtn:BalanceButtonReflectionCanvas
        protected var _contentImage:BalanceImageReflectionCanvas
        private var currentQuestions:Array
        public var navigationMode:NavigationMode
        public var currentTag:String
        protected var _transitionContainer1:IContainer
        protected var _transitionContainer2:IContainer
        protected var _contentCollection:ContentCollection

        public static const BALANCE_MODULE_READY:String = "balanceModuleReady";
        public static const OUTRO_START:String = "outro_start";

        public function AbstractBalanceModule(){
            navigationMode = NavigationMode.Free;
            super();
            this.addEventListener(AnswerSelectedEvent.ANSWER_SELECTED, storeAnswer);
            this.addEventListener(AnswerSelectedEvent.ANSWER_DELETED, removeAnswer);
            this.addEventListener(BalanceController.MODULE_COLLECTION_COMPLETE, collectionComplete);
            if (getQualifiedClassName(this) == "com.redbox.changetech.view.modules::AbstractBalanceModule"){
                throw (new Error("AbstractClass must be extended"));
            };
        }
        public function get transitionContainer1():IContainer{
            return (_transitionContainer1);
        }
        public function get transitionContainer2():IContainer{
            return (_transitionContainer2);
        }
        private function set _811296866contentImage(_arg1:BalanceImageReflectionCanvas):void{
            _contentImage = _arg1;
        }
        private function addResponse(_arg1:Number, _arg2:Number=-1, _arg3:Number=-1, _arg4:String=""):void{
            var _local5:Answer = new Answer();
            _local5.QuestionId = _arg1;
            _local5.OptionId = _arg2;
            _local5.Score = _arg3;
            _local5.Text = _arg4;
            response.Answers.push(_local5);
            trace(("response.Answers=" + response.Answers));
        }
        protected function storeAnswer(_arg1:AnswerSelectedEvent):void{
            trace("-------------------------------storeAnswer");
            var _local2:Question = BalanceModelLocator.getInstance().getQuestionById(_arg1.id);
            var _local3:Answer;
            var _local4:Number = 0;
            while (_local4 < response.Answers.length) {
                if (response.Answers[_local4].QuestionId == _arg1.id){
                    _local3 = response.Answers[_local4];
                    break;
                };
                _local4++;
            };
            if (_local3 == null){
                trace("adding response-----------------");
                trace(("event.score=" + _arg1.score));
                addResponse(_arg1.id, _arg1.optionId, _arg1.score, _arg1.text);
            } else {
                if (_local2.AllowMultiple){
                    if (_local3.OptionId != _arg1.optionId){
                        addResponse(_arg1.id, _arg1.optionId, _arg1.score, _arg1.text);
                    };
                } else {
                    trace("setting score of previous answer");
                    _local3.OptionId = _arg1.optionId;
                    _local3.Score = _arg1.score;
                    _local3.Text = _arg1.text;
                };
            };
            mandatoryQuestionsComplete = checkMandatoryQuestions();
            trace(("mandatoryQuestionsComplete=" + mandatoryQuestionsComplete));
            if (_arg1.callNext){
                next();
            };
        }
        public function previous(_arg1:Event=null):Boolean{
            var _local2:Number;
            var _local3:Content;
            if (navigationMode == NavigationMode.ForwardOnly){
                return (false);
            };
            if (contentIndex > 0){
                _local2 = (contentIndex - 1);
                while (_local2 != contentIndex) {
                    _local3 = contentCollection.Contents[_local2];
                    if (_local3.Conditions == null){
                        contentIndex = _local2;
                    } else {
                        if (_local3.Conditions.evaluate()){
                            contentIndex = _local2;
                        } else {
                            _local2--;
                        };
                    };
                };
            } else {
                return (false);
            };
            return (true);
        }
        public function set transitionContainer2(_arg1:IContainer):void{
            var _local2:Object = this.transitionContainer2;
            if (_local2 !== _arg1){
                this._620714234transitionContainer2 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transitionContainer2", _local2, _arg1));
            };
        }
        private function set _811329497contentIndex(_arg1:Number):void{
            _contentIndex = _arg1;
            trace("SETTING CONTENT INDEX");
            content = contentCollection.Contents[contentIndex];
            mandatoryQuestionsComplete = checkMandatoryQuestions();
            BalanceModelLocator.getInstance().collectionContentIndex = _contentIndex;
        }
        private function set _1350596052ctaBtn(_arg1:BalanceButtonReflectionCanvas):void{
            _ctaBtn = _arg1;
        }
        public function moduleReady(_arg1:Event=null):void{
            if (t){
                t.removeEventListener(TimerEvent.TIMER_COMPLETE, moduleReady);
            };
            if (!_isModuleActive){
                dispatchEvent(new Event(BALANCE_MODULE_READY, true));
            };
            _isModuleActive = true;
        }
        public function set mandatoryQuestionsComplete(_arg1:Boolean):void{
            var _local2:Object = this._2075616051mandatoryQuestionsComplete;
            if (_local2 !== _arg1){
                this._2075616051mandatoryQuestionsComplete = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mandatoryQuestionsComplete", _local2, _arg1));
            };
        }
        private function setMobileNumber(){
            var _local2:Answer;
            var _local3:Question;
            var _local4:String;
            var _local5:CairngormEvent;
            var _local1:Number = 0;
            while (_local1 < response.Answers.length) {
                _local2 = (response.Answers[_local1] as Answer);
                _local3 = BalanceModelLocator.getInstance().getQuestionById(_local2.QuestionId);
                if (_local3.Action == "MobileNumber"){
                    _local4 = _local2.Text;
                    _local5 = new CairngormEvent(BalanceController.SET_MOBILENUMBER_COMMAND);
                    _local5.data = new Object();
                    _local5.data.mobileNumber = _local4;
                    _local5.data.callBack = this.collectionComplete;
                    _local5.data.callBackObject = this;
                    _local5.dispatch();
                };
                _local1++;
            };
        }
        public function set transitionContainer1(_arg1:IContainer):void{
            var _local2:Object = this.transitionContainer1;
            if (_local2 !== _arg1){
                this._620714235transitionContainer1 = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "transitionContainer1", _local2, _arg1));
            };
        }
        protected function checkMandatoryQuestions():Boolean{
            var _local1:Number;
            var _local2:Answer;
            var _local3:Question;
            if (content == null){
                Alert.show("error: contents is null");
                return (false);
            };
            if (content.Questions != null){
                _local1 = 0;
                while (_local1 < content.Questions.length) {
                    if (content.Questions[_local1].Mandatory){
                        _local2 = getAnswerById(content.Questions[_local1].Id);
                        trace(("answer=" + _local2));
                        if (_local2 == null){
                            _local3 = content.Questions[_local1];
                            if (((!((_local3.PreviousAnswer == null))) && (!((_local3.PreviousAnswer == ""))))){
                                addResponse(_local3.Id, -1, -1, _local3.PreviousAnswer);
                                return (true);
                            };
                            return (false);
                        };
                    };
                    _local1++;
                };
            };
            return (true);
        }
        public function getAnswerById(_arg1:int):Answer{
            return (BalanceModelLocator.getInstance().getAnswerById(_arg1));
        }
        public function getTotalScore():Number{
            var _local3:Answer;
            var _local1:Number = 0;
            var _local2:Number = 0;
            while (_local2 < response.Answers.length) {
                _local3 = response.Answers[_local2];
                if (_local3.Score > -1){
                    _local1 = (_local1 + _local3.Score);
                };
                _local2++;
            };
            return (_local1);
        }
        public function set response(_arg1:Response):void{
            var _local2:Object = this.response;
            if (_local2 !== _arg1){
                this._340323263response = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "response", _local2, _arg1));
            };
        }
        public function collectionComplete(_arg1:Event=null):void{
            var _local2:CairngormEvent = new CairngormEvent(BalanceController.COLLECTION_COMPLETE);
            _local2.data = new Object();
            _local2.data.response = response;
            _local2.dispatch();
        }
        public function get ctaBtn():BalanceButtonReflectionCanvas{
            return (_ctaBtn);
        }
        private function generateResponseObject():void{
            var _local1:Number;
            var _local2:Content;
            var _local3:Array;
            var _local4:Number;
            var _local5:Question;
            response = new Response();
            response.Answers = [];
            response.Type = contentCollection.Type;
            currentQuestions = [];
            if (contentCollection.Contents){
                _local1 = 0;
                while (_local1 < contentCollection.Contents.length) {
                    _local2 = contentCollection.Contents[_local1];
                    _local3 = _local2.Questions;
                    if (_local3 != null){
                        _local4 = 0;
                        while (_local4 < _local3.length) {
                            _local5 = _local3[_local4];
                            currentQuestions.push(_local5);
                            _local4++;
                        };
                    };
                    _local1++;
                };
            };
            BalanceModelLocator.getInstance().currentQuestions = currentQuestions;
            BalanceModelLocator.getInstance().response = response;
        }
        private function set _951530617content(_arg1:Content):void{
            _content = _arg1;
            dispatchEvent(new Event("contentChange", true));
            trace("CALLING CONTENT CHANGED");
            contentChanged();
            if (_content.Code != null){
                BalanceModelLocator.getInstance().currentContentCode = _content.Code;
            };
        }
        public function creationComplete(_arg1:Event):void{
        }
        public function get response():Response{
            return (_response);
        }
        public function set contentImage(_arg1:BalanceImageReflectionCanvas):void{
            var _local2:Object = this.contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        protected function resetModuleReady(_arg1:Number):void{
            _isModuleActive = false;
        }
        public function get mandatoryQuestionsComplete():Boolean{
            return (this._2075616051mandatoryQuestionsComplete);
        }
        public function flushAnswers(_arg1:Number):void{
            var _local2:Number = 0;
            while (_local2 < response.Answers.length) {
                if (response.Answers[_local2].QuestionId == _arg1){
                    response.Answers.splice(_local2, 1);
                    _local2--;
                };
                _local2++;
            };
        }
        public function delayCallModuleReady(_arg1:int):void{
            t = new Timer(_arg1, 1);
            t.addEventListener(TimerEvent.TIMER_COMPLETE, moduleReady);
            t.start();
        }
        private function set _340323263response(_arg1:Response):void{
            _response = _arg1;
        }
        public function get contentIndex():Number{
            return (_contentIndex);
        }
        protected function collectionInitialized(_arg1:Event=null):void{
        }
        protected function endOfCollection():void{
            setMobileNumber();
            collectionComplete();
        }
        public function get contentImage():BalanceImageReflectionCanvas{
            return (_contentImage);
        }
        public function set ctaBtn(_arg1:BalanceButtonReflectionCanvas):void{
            var _local2:Object = this.ctaBtn;
            if (_local2 !== _arg1){
                this._1350596052ctaBtn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "ctaBtn", _local2, _arg1));
            };
        }
        private function set _620714234transitionContainer2(_arg1:IContainer):void{
            _transitionContainer2 = _arg1;
        }
        public function set contentIndex(_arg1:Number):void{
            var _local2:Object = this.contentIndex;
            if (_local2 !== _arg1){
                this._811329497contentIndex = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentIndex", _local2, _arg1));
            };
        }
        public function set contentCollection(_arg1:ContentCollection):void{
            var _local2:Boolean;
            _contentCollection = _arg1;
            generateResponseObject();
            if (_arg1.Contents){
                _contentIndex = -1;
                contentIndex = 0;
            };
            collectionInitialized();
        }
        private function set _620714235transitionContainer1(_arg1:IContainer):void{
            _transitionContainer1 = _arg1;
        }
        private function removeAnswer(_arg1:AnswerSelectedEvent):void{
            var _local2:Number;
            var _local3:Number = 0;
            while (_local3 < response.Answers.length) {
                if ((((response.Answers[_local3].QuestionId == _arg1.id)) && ((response.Answers[_local3].optionId == _arg1.optionId)))){
                    _local2 = _local3;
                    break;
                };
                _local3++;
            };
            response.Answers.splice(_local2, 1);
        }
        public function set content(_arg1:Content):void{
            var _local2:Object = this.content;
            if (_local2 !== _arg1){
                this._951530617content = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "content", _local2, _arg1));
            };
        }
        public function next(_arg1:Event=null):Boolean{
            var _local2:Number;
            var _local3:Content;
            trace(("s c o r e = " + getTotalScore()));
            if (BalanceModelLocator.getInstance().flashVars.mode == "PREVIEW"){
                return (false);
            };
            if (contentIndex < (contentCollection.Contents.length - 1)){
                _local2 = (contentIndex + 1);
                while (_local2 != contentIndex) {
                    _local3 = contentCollection.Contents[_local2];
                    trace(("currentTag=" + currentTag));
                    trace(("nextContent=" + _local3));
                    if (_local3 == null){
                        Alert.show("problem due to invalid conditions and or tags");
                        return (false);
                    };
                    if (_local3.Conditions == null){
                        if (_local3.Tag == null){
                            contentIndex = _local2;
                        } else {
                            if (currentTag == null){
                                contentIndex = _local2;
                            } else {
                                if (_local3.Tag == currentTag){
                                    contentIndex = _local2;
                                } else {
                                    _local2++;
                                };
                            };
                        };
                    } else {
                        if (_local3.Conditions.evaluate()){
                            trace(("NEW CONTENT INDEX BEING SET::::: PREVIOUS CONTENTInex= " + contentIndex));
                            contentIndex = _local2;
                        } else {
                            _local2++;
                            if (_local2 >= contentCollection.Contents.length){
                                endOfCollection();
                                return (false);
                            };
                        };
                    };
                };
            } else {
                endOfCollection();
                return (false);
            };
            return (true);
        }
        public function get contentCollection():ContentCollection{
            return (_contentCollection);
        }
        public function get content():Content{
            return (_content);
        }
        protected function contentChanged(){
        }

    }
}//package com.redbox.changetech.view.modules 
