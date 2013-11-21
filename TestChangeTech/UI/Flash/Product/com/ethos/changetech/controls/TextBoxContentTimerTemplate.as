package com.ethos.changetech.controls{
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.xml.FeedbacksXML;
	import com.ethos.changetech.xml.FeedbackXMLNode;
	import com.ning.components.media.skin.*;
	import com.ning.data.GlobalValue;
	import com.ning.text.StaticTextField;
	import flash.display.SimpleButton;
	import flash.events.MouseEvent;
	import flash.events.TimerEvent;
	import flash.text.AntiAliasType;
	import flash.text.StyleSheet;
	import flash.text.TextFieldAutoSize;
	import flash.utils.Timer;

	public class TextBoxContentTimerTemplate extends TextBoxContentStandardBasedTemplate {

		private var _timerButton:SimpleButton;
		private var _startButton:SimpleButton;
		private var _stopButton:SimpleButton;
		private var _timeTextField:StaticTextField;

		// Time
		private var _timer:Timer;
		private var _totalsecond:int=0;
		private var _hh:int=0;
		private var _mm:int=0;
		private var _ss:int=0;

		// Flag
		private var _isTimer:Boolean=true;

		public function TextBoxContentTimerTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			super(_targetPage, _w, _minH, _maxH);
		}
		override public function set primaryThemeColor(_value:uint):void {
			super.primaryThemeColor=_value;
			_timeTextField.textColor=_primaryThemeColor;
		}
		public function get time():String {
			var _hh_str:String=_hh<10?"0"+_hh.toString():_hh.toString();
			var _mm_str:String=_mm<10?"0"+_mm.toString():_mm.toString();
			var _ss_str:String=_ss<10?"0"+_ss.toString():_ss.toString();
			return _hh_str + ":" + _mm_str + ":" + _ss_str;
		}
		public function get timesecond():int {
			return _hh*60*60 + _mm*60 + _ss;
		}
		override public function freeze():void {
			if (_isTimer&&_timer!=null) {
				_timer.stop();
			}
		}
		override public function unfreeze():void {
			if (_isTimer&&_timer!=null) {
				_timer.start();
			}
		}
		override protected function updateInnerContent():void {
			_timeTextField=new StaticTextField(TextFieldAutoSize.LEFT,false,true,AntiAliasType.ADVANCED);
			_timeTextField.textColor=_primaryThemeColor;
			_timeTextField.styleSheet=StyleSheet(GlobalValue.getValue("css"));
			_timeTextField.htmlText="<timer>"+time+"</timer>";
			addChild(_timeTextField);
			if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
			{
				_isTimer=false;
				_totalsecond = parseInt(String(_page.pageVariable));
				updateTime();
			}
			
			_startButton = new SimpleButton(new StartButtonNormal(), new StartButtonOver(), new StartButtonDown(), new StartButtonNormal());
			_stopButton = new SimpleButton(new StopButtonNormal(), new StopButtonOver(), new StopButtonDown(), new StopButtonNormal());
			_timerButton=_startButton;
			_timerButton.addEventListener(MouseEvent.CLICK, startTimerHandler);
			addChild(_timerButton);
			if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
			{
				_timerButton.removeEventListener(MouseEvent.CLICK, startTimerHandler);
				_timerButton.enabled = false;
			}
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			_timeTextField.x = (_contentWidth - _timeTextField.width) / 2 + _horizontalPadding;
			_timeTextField.y=_lastPositionY+2*_textMargin;
			_lastPositionY=_timeTextField.y+_timeTextField.height;

			_timerButton.width=_timeTextField.textHeight;
			_timerButton.height=_timeTextField.textHeight;
			_timerButton.x = (_contentWidth - _timerButton.width) / 2 + _horizontalPadding;
			_timerButton.y=_lastPositionY+2*_textMargin;
			_lastPositionY=_timerButton.y+_timerButton.height;
			_lastPositionY+=_textMargin;
			return _lastPositionY;
		}
		private function updateTime():void {
			var _secondCounter:Number=_totalsecond;
			_hh=Math.floor(_secondCounter/3600);
			_secondCounter=_secondCounter%3600;
			_mm=Math.floor(_secondCounter/60);
			_secondCounter=_secondCounter%60;
			_ss=_secondCounter;
			_timeTextField.htmlText="<timer>"+time+"</timer>";
		}
		private function startTimerHandler(_event:MouseEvent):void {
			_timer=new Timer(1000);
			_timer.addEventListener(TimerEvent.TIMER, timerHandler);
			_timer.start();

			_timerButton.removeEventListener(MouseEvent.CLICK, startTimerHandler);
			removeChild(_timerButton);
			_timerButton=_stopButton;
			_timerButton.addEventListener(MouseEvent.CLICK, stopTimerHandler);
			addChild(_timerButton);
			layout();
		}
		private function stopTimerHandler(_event:MouseEvent):void {
			_timer.stop();
			_isTimer=false;
			updateTime();
			_page.pageVariable=timesecond;
			_timerButton.removeEventListener(MouseEvent.CLICK, stopTimerHandler);
			_timerButton.enabled=false;
		}
		private function timerHandler(_event:TimerEvent):void {
			_totalsecond++;
			updateTime();
		}
		override public function primaryButtonClickedHandler(_event:MouseEvent):void {
			if (checkAndSubmit()) {
				nextPage();
			} else {
				_event.target.enabled=true;
			}
		}
		private function checkAndSubmit():Boolean {
			trace("submit timer.");
			if (_isTimer) {
				infoPage(GlobalValue.getValue("messages")["TimerRequired"]);
				return false;
			} else {
				var _feedbacksXML:FeedbacksXML = new FeedbacksXML();
				var _feedbackXMLNode:FeedbackXMLNode=new FeedbackXMLNode("",timesecond.toString());
				_feedbacksXML.addFeedback(_feedbackXMLNode);
				//trace(_feedbacksXML.toString());
				this.dispatchEvent(new SubmitEvent("submit", _feedbacksXML.xml));
				return true;
			}
		}
	}
}