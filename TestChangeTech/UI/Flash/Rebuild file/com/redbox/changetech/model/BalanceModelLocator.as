package com.redbox.changetech.model{
	import flash.text.*;
	import mx.core.*;
	import flash.events.*;
	import mx.events.*;
	import com.redbox.changetech.vo.*;
	import flash.utils.*;
	import mx.collections.*;
	import com.adobe.cairngorm.model.*;
	import com.redbox.changetech.util.*;
	import com.redbox.changetech.util.Enumerations.*;

	public class BalanceModelLocator implements IModelLocator,IEventDispatcher {

		private var _currentDay:Number;
		private var _flashVars:Object;
		private var _gateway:String;
		private var _1365206181isFullScreen:Boolean=false;
		private var _323876267happinessScore:Number=0;
		private var _reductionPlanModified:Dictionary;
		private var _currentQuestions:Array;
		private var _1986159062balanceStyleSheet:StyleSheet;
		private var _1512812318roomContent:ArrayCollection;
		private var _reductionPlan:Dictionary;
		private var _1509933079dropDownMenuVO:DropDownMenuVO;
		private var _1339669402showConsole:Boolean=false;
		private var _isOutroActive:Boolean=false;
		private var _culture:String;
		private var _reportedUsageModified:Dictionary;
		private var _currentConsumptionVO:Consumption;
		private var _reportedUsage:Dictionary;
		private var test:Class;
		private var _response:Response;
		private var _1171036467currentContentCode:String="NOT SET";
		private var _1187361573completionUsage:Array;
		private var _1418905389showControls:Boolean=true;
		private var _collectionContentIndex:Number;
		private var _room:Number;
		private var _bindingEventDispatcher:EventDispatcher;
		private var _1089726749alwaysShowLogin:Boolean=false;
		private var _2056683457currentStageWidth:Number;
		private var _510861361isFullLoginRequired:Boolean=false;
		private var _2069404063RPC_OperationInProgress:Boolean=false;
		private var _showLogin:Boolean=false;

		private static  var instance:BalanceModelLocator;

		public function BalanceModelLocator() {
			test=Config;
			_1512812318roomContent=new ArrayCollection(new Array(Config.NUMBER_OF_ROOMS));
			_room=Config.ROOM_ORDER[RoomName.Blank];
			_bindingEventDispatcher=new EventDispatcher(IEventDispatcher(this));
			super();
			if (instance != null) {
				throw new Error("Error: Singletons can only be instantiated via getInstance() method!");
			}
			BalanceModelLocator.instance=this;
			consumer=new Consumer  ;
			completionUsage=[];
		}
		private var _currentContentCollection:ContentCollection;
		public function get currentContentCollection():ContentCollection {
			return this._currentContentCollection;
		}
		public function set currentContentCollection(_arg1:ContentCollection):void {
			var _local2:Object=this._currentContentCollection;
			if (_local2 !== _arg1) {
				this._currentContentCollection=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentContentCollection",_local2,_arg1));
			}
		}
		private var _isTransitionActive:Boolean=false;
		public function get isTransitionActive():Boolean {
			return _isTransitionActive;
		}
		public function set isTransitionActive(_arg1:Boolean):void {
			var _local2:Object=this.isTransitionActive;
			if (_local2 !== _arg1) {
				this._isTransitionActive=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"isTransitionActive",_local2,_arg1));
			}
		}
		private var _assets;
		public function get assets() {
			return this._assets;
		}
		public function set assets(_arg1):void {
			var _local2:Object=this._assets;
			if (_local2 !== _arg1) {
				this._assets=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"assets",_local2,_arg1));
			}
		}
		private var _languageVO:LanguageVo;
		public function get languageVO():LanguageVo {
			return (this._languageVO);
		}
		public function set languageVO(_arg1:LanguageVo):void {
			var _local2:Object=this._languageVO;
			if (_local2 !== _arg1) {
				this._languageVO=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"languageVO",_local2,_arg1));
			}
		}
		private var _contentIndex:Number=0;
		public function get contentIndex():Number {
			return _contentIndex;
		}
		public function set contentIndex(_arg1:Number):void {
			var _local2:Object=this.contentIndex;
			if (_local2 !== _arg1) {
				this._contentIndex=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"contentIndex",_local2,_arg1));
			}
		}
		private var _loaderSource:UIComponent=null;
		public function get loaderSource():UIComponent {
			return _loaderSource;
		}
		public function set loaderSource(_arg1:UIComponent):void {
			var _local2:Object=this.loaderSource;
			if (_local2 !== _arg1) {
				this._loaderSource=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"loaderSource",_local2,_arg1));
			}
		}
		private var _currentStageHeight:Number;
		public function get currentStageHeight():Number {
			return this._currentStageHeight;
		}
		public function set currentStageHeight(_arg1:Number):void {
			var _local2:Object=this._currentStageHeight;
			if (_local2 !== _arg1) {
				this._currentStageHeight=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentStageHeight",_local2,_arg1));
			}
		}
		private var _isDebugMode:Boolean=false;
		public function get isDebugMode():Boolean {
			return this._isDebugMode;
		}
		public function set isDebugMode(_arg1:Boolean):void {
			var _local2:Object=this._isDebugMode;
			if (_local2 !== _arg1) {
				this._isDebugMode=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"isDebugMode",_local2,_arg1));
			}
		}
		private var _consumer:Consumer=null;
		public function get consumer():Consumer {
			return _consumer;
		}
		public function set consumer(_arg1:Consumer):void {
			var _local2:Object=this.consumer;
			if (_local2 !== _arg1) {
				_consumer=_arg1;
				_currentDay=_consumer.CurrentDay;
				currentConsumptionVO=_consumer.ReportedUsage[0];
				trace("currentConsumptionVO=" + currentConsumptionVO);
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"consumer",_local2,_arg1));
			}
		}
		
		public function get reductionPlanModified():Dictionary {
			return _reductionPlanModified;
		}
		public function get balanceStyleSheet():StyleSheet {
			return (this._1986159062balanceStyleSheet);
		}

		public function get alwaysShowLogin():Boolean {
			return (this._1089726749alwaysShowLogin);
		}
		public function get roomContent():ArrayCollection {
			return (this._1512812318roomContent);
		}
		public function set showConsole(_arg1:Boolean):void {
			var _local2:Object=this._1339669402showConsole;
			if (_local2 !== _arg1) {
				this._1339669402showConsole=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"showConsole",_local2,_arg1));
			}
		}
		public function set currentConsumptionVO(_arg1:Consumption):void {
			var _local2:Object=this.currentConsumptionVO;
			if (_local2 !== _arg1) {
				this._1657168539currentConsumptionVO=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentConsumptionVO",_local2,_arg1));
			}
		}
		public function get reductionPlan():Dictionary {
			return _reductionPlan;
		}
		private function set _1921025428showLogin(_arg1:Boolean):void {
			_showLogin=_arg1;
		}
		public function get response():Response {
			return _response;
		}
		public function get flashVars():Object {
			return (this._flashVars);
		}
		public function get gateway():String {
			return _gateway;
		}
		public function set RPC_OperationInProgress(_arg1:Boolean):void {
			var _local2:Object=this._2069404063RPC_OperationInProgress;
			if (_local2 !== _arg1) {
				this._2069404063RPC_OperationInProgress=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"RPC_OperationInProgress",_local2,_arg1));
			}
		}
		public function set collectionContentIndex(_arg1:Number):void {
			var _local2:Object=this.collectionContentIndex;
			if (_local2 !== _arg1) {
				this._1847979415collectionContentIndex=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"collectionContentIndex",_local2,_arg1));
			}
		}
		public function get completionUsage():Array {
			return (this._1187361573completionUsage);
		}
		public function set alwaysShowLogin(_arg1:Boolean):void {
			var _local2:Object=this._1089726749alwaysShowLogin;
			if (_local2 !== _arg1) {
				this._1089726749alwaysShowLogin=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"alwaysShowLogin",_local2,_arg1));
			}
		}
		public function set isFullScreen(_arg1:Boolean):void {
			var _local2:Object=this._1365206181isFullScreen;
			if (_local2 !== _arg1) {
				this._1365206181isFullScreen=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"isFullScreen",_local2,_arg1));
			}
		}
		private function rationaliseUsage():void {
			var _local1:Consumption=_consumer.ReportedUsage[0];
			while (! _local1.DayOfWeek == DayOfWeek.Monday.Text && _consumer.ReportedUsage.length > 0) {
				_consumer.ReportedUsage.shift();
				_local1=_consumer.ReportedUsage[0];
			}
		}
		public function setCurrentConsumptionVO(_arg1:String):void {
			var _local3:Consumption;
			var _local2:Number=0;
			while (_local2 < _consumer.ReportedUsage.length) {
				_local3=_consumer.ReportedUsage[_local2];
				if (_local3.DayOfWeek == _arg1) {
					currentConsumptionVO=_local3;
					break;
				}
				_local2++;
			}
		}


		public function addEventListener(_arg1:String,_arg2:Function,_arg3:Boolean=false,_arg4:int=0,_arg5:Boolean=false):void {
			_bindingEventDispatcher.addEventListener(_arg1,_arg2,_arg3,_arg4,_arg5);
		}
		public function set completionUsage(_arg1:Array):void {
			var _local2:Object=this._1187361573completionUsage;
			if (_local2 !== _arg1) {
				this._1187361573completionUsage=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"completionUsage",_local2,_arg1));
			}
		}
		public function set roomContent(_arg1:ArrayCollection):void {
			var _local2:Object=this._1512812318roomContent;
			if (_local2 !== _arg1) {
				this._1512812318roomContent=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"roomContent",_local2,_arg1));
			}
		}
		private function set _643384364currentQuestions(_arg1:Array):void {
			_currentQuestions=_arg1;
		}
		public function removeEventListener(_arg1:String,_arg2:Function,_arg3:Boolean=false):void {
			_bindingEventDispatcher.removeEventListener(_arg1,_arg2,_arg3);
		}
		public function set balanceStyleSheet(_arg1:StyleSheet):void {
			var _local2:Object=this._1986159062balanceStyleSheet;
			if (_local2 !== _arg1) {
				this._1986159062balanceStyleSheet=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"balanceStyleSheet",_local2,_arg1));
			}
		}
		private function set _1235274393isOutroActive(_arg1:Boolean):void {
			_isOutroActive=_arg1;
		}
		private function set _340323263response(_arg1:Response):void {
			_response=_arg1;
		}
		public function dispatchEvent(_arg1:Event):Boolean {
			return (_bindingEventDispatcher.dispatchEvent(_arg1));
		}
		public function get currentDay():Number {
			return _currentDay;
		}
		public function set happinessScore(_arg1:Number):void {
			var _local2:Object=this._323876267happinessScore;
			if (_local2 !== _arg1) {
				this._323876267happinessScore=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"happinessScore",_local2,_arg1));
			}
		}
		public function get isOutroActive():Boolean {
			return _isOutroActive;
		}
		public function setRoomContent(_arg1:ContentCollection):void {
			var _local2:* =Config.ROOM_MAPPINGS;
			_contentIndex=Config.ROOM_MAPPINGS[_arg1.Type];
			roomContent.setItemAt(_arg1,_contentIndex);
			currentContentCollection=_arg1;
		}
		public function get showLogin():Boolean {
			return _showLogin;
		}
		public function get reportedUsageModified():Dictionary {
			return _reportedUsageModified;
		}


		public function set flashVars(_arg1:Object):void {
			var _local2:Object=this._flashVars;
			if (_local2 !== _arg1) {
				this._flashVars=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"flashVars",_local2,_arg1));
			}
		}
		public function set currentStageWidth(_arg1:Number):void {
			var _local2:Object=this._2056683457currentStageWidth;
			if (_local2 !== _arg1) {
				this._2056683457currentStageWidth=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentStageWidth",_local2,_arg1));
			}
		}

		private function set _3506395room(_arg1:Number):void {
			_room=_arg1;
		}
		public function getQuestionById(_arg1:int):Question {
			var _local3:Question;
			var _local2:Number=0;
			while (_local2 < currentQuestions.length) {
				_local3=currentQuestions[_local2];
				if (_local3.Id == _arg1) {
					return currentQuestions[_local2];
				}
				_local2++;
			}
			return null;
		}
		public function set gateway(_arg1:String):void {
			var _local2:Object=this.gateway;
			if (_local2 !== _arg1) {
				this._189118908gateway=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"gateway",_local2,_arg1));
			}
		}

		public function willTrigger(_arg1:String):Boolean {
			return (_bindingEventDispatcher.willTrigger(_arg1));
		}
		public function get reportedUsage():Dictionary {
			return _reportedUsage;
		}
		public function set response(_arg1:Response):void {
			var _local2:Object=this.response;
			if (_local2 !== _arg1) {
				this._340323263response=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"response",_local2,_arg1));
			}
		}

		public function set culture(_arg1:String):void {
			var _local2:Object=this.culture;
			if (_local2 !== _arg1) {
				this._1121473966culture=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"culture",_local2,_arg1));
			}
		}
		public function get collectionContentIndex():Number {
			return _collectionContentIndex;
		}
		private function set _1657168539currentConsumptionVO(_arg1:Consumption):void {
			_currentConsumptionVO=_arg1;
		}
		public function set currentContentCode(_arg1:String):void {
			var _local2:Object=this._1171036467currentContentCode;
			if (_local2 !== _arg1) {
				this._1171036467currentContentCode=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentContentCode",_local2,_arg1));
			}
		}
		public function get currentConsumptionVO():Consumption {
			return _currentConsumptionVO;
		}
		public function set isFullLoginRequired(_arg1:Boolean):void {
			var _local2:Object=this._510861361isFullLoginRequired;
			if (_local2 !== _arg1) {
				this._510861361isFullLoginRequired=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"isFullLoginRequired",_local2,_arg1));
			}
		}
		public function get RPC_OperationInProgress():Boolean {
			return this._2069404063RPC_OperationInProgress;
		}
		public function get showConsole():Boolean {
			return this._1339669402showConsole;
		}
		public function get isFullScreen():Boolean {
			return this._1365206181isFullScreen;
		}
		public function getAnswerById(_arg1:int):Answer {
			var _local2:Number=0;
			while (_local2 < response.Answers.length) {
				if (response.Answers[_local2].QuestionId == _arg1) {
					return response.Answers[_local2];
				}
				_local2++;
			}
			return null;
		}
		private function set _1847979415collectionContentIndex(_arg1:Number):void {
			_collectionContentIndex=_arg1;
		}
		public function set showControls(_arg1:Boolean):void {
			var _local2:Object=this._1418905389showControls;
			if (_local2 !== _arg1) {
				this._1418905389showControls=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"showControls",_local2,_arg1));
			}
		}
		public function get culture():String {
			return _culture;
		}
		public function set currentQuestions(_arg1:Array):void {
			var _local2:Object=this.currentQuestions;
			if (_local2 !== _arg1) {
				this._643384364currentQuestions=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"currentQuestions",_local2,_arg1));
			}
		}
		public function getAnswersById(_arg1:int):Array {
			var _local2:Array=[];
			var _local3:Number=0;
			while (_local3 < response.Answers.length) {
				if (response.Answers[_local3].QuestionId == _arg1) {
					_local2.push(response.Answers[_local3]);
				}
				_local3++;
			}
			return _local2;
		}

		public function get currentStageWidth():Number {
			return this._2056683457currentStageWidth;
		}
		public function get happinessScore():Number {
			return this._323876267happinessScore;
		}
		public function set room(_arg1:Number):void {
			var _local2:Object=this.room;
			if (_local2 !== _arg1) {
				this._3506395room=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"room",_local2,_arg1));
			}
		}
		public function get currentContentCode():String {
			return this._1171036467currentContentCode;
		}
		private function set _1121473966culture(_arg1:String):void {
			if (flashVars.culture != null) {
				_culture=flashVars.culture;
			} else {
				_culture=_arg1;
			}
		}
		public function get isFullLoginRequired():Boolean {
			return this._510861361isFullLoginRequired;
		}
		private function set _189118908gateway(_arg1:String):void {
			_gateway=_arg1;
		}
		public function set dropDownMenuVO(_arg1:DropDownMenuVO):void {
			var _local2:Object=this._1509933079dropDownMenuVO;
			if (_local2 !== _arg1) {
				this._1509933079dropDownMenuVO=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"dropDownMenuVO",_local2,_arg1));
			}
		}
		public function get showControls():Boolean {
			return this._1418905389showControls;
		}
		public function get currentQuestions():Array {
			return _currentQuestions;
		}
		public function set isOutroActive(_arg1:Boolean):void {
			var _local2:Object=this.isOutroActive;
			if (_local2 !== _arg1) {
				this._1235274393isOutroActive=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"isOutroActive",_local2,_arg1));
			}
		}
		public function set showLogin(_arg1:Boolean):void {
			var _local2:Object=this.showLogin;
			if (_local2 !== _arg1) {
				this._1921025428showLogin=_arg1;
				this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this,"showLogin",_local2,_arg1));
			}
		}

		public function get room():Number {
			return _room;
		}

		public function get dropDownMenuVO():DropDownMenuVO {
			return this._1509933079dropDownMenuVO;
		}
		public function hasEventListener(_arg1:String):Boolean {
			return (_bindingEventDispatcher.hasEventListener(_arg1));
		}


		public static  function getInstance():BalanceModelLocator {
			if (instance == null) {
				instance=new BalanceModelLocator  ;
			}
			return instance;
		}

	}
}//package com.redbox.changetech.model 