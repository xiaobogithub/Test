package com.ethos.changetech.utils{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.models.*;
	import com.ning.data.GlobalValue;
	import flash.events.EventDispatcher;
	public class MenuFunctions extends EventDispatcher {
		
		public static function SMSToEmailFunction(_targetObject:EventDispatcher):void
		{
			var _messageObject:MessageObject;
			
			var title:String ;
			var buttonLabel:String ;
			
			var smsToEmailMode:XML = XML(GlobalValue.getValue("smsToEmailModel"));
			title = smsToEmailMode.@Title;
			buttonLabel = smsToEmailMode.@BackButtonName;
			
			if (smsToEmailMode.@Title == undefined || smsToEmailMode.@Title == "" || smsToEmailMode.@Title == null)
			title = "SMSToEmail";
			
			if (smsToEmailMode.@BackButtonName == undefined || smsToEmailMode.@BackButtonName == "" || smsToEmailMode.@BackButtonName == null)
			buttonLabel = "OK";
			
			
			_messageObject = new MessageObject(title,buttonLabel);
			_messageObject.type = "smsToEmail";
			//_messageObject.model = _helpModel;
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		
		/*
		 * DTD1582:TimeZone
		*/
		public static function TimeZoneFunction(_targetObject:EventDispatcher):void
		{
			var _messageObject:MessageObject;
			
			var title:String ;
			var buttonLabel:String ;
			
			var timeZoneMode:XML = XML(GlobalValue.getValue("timeZoneModel"));
			title = timeZoneMode.@Title;
			buttonLabel = timeZoneMode.@BackButtonName;
			
			if (timeZoneMode.@Title == undefined || timeZoneMode.@Title == "" || timeZoneMode.@Title == null)
			title = "TimeZone";
			
			if (timeZoneMode.@BackButtonName == undefined || timeZoneMode.@BackButtonName == "" || timeZoneMode.@BackButtonName == null)
			buttonLabel = "OK";
			
			
			_messageObject = new MessageObject(title,buttonLabel);
			_messageObject.type = "timeZone";
			//_messageObject.model = _helpModel;
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		
		public static function HelpFunction(_targetObject:EventDispatcher):void {
			var _helpModel:Help = Help(GlobalValue.getValue("helpModel"));
			var _messageObject:MessageObject;
			if (_helpModel == null) {
				_messageObject = new MessageObject("error", "back");
				_messageObject.message = "Help Error: Cannot get help information from xml.";
			} else {
				_messageObject = new MessageObject(_helpModel.title, _helpModel.backButtonName);
				_messageObject.type = "help";
				_messageObject.model = _helpModel;
			}
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		public static function TipFriendFunction(_targetObject:EventDispatcher):void {
			var _tipFriendModel:TipFriend = TipFriend(GlobalValue.getValue("tipFriendModel"));
			var _messageObject:MessageObject;
			if (_tipFriendModel == null) {
				_messageObject = new MessageObject("error", "back");
				_messageObject.message = "Help Error: Cannot get tip friend information from xml.";
			} else {
				_messageObject = new MessageObject(_tipFriendModel.title, _tipFriendModel.backButtonName);
				_messageObject.type = "tipFriend";
				_messageObject.model = _tipFriendModel;
			}
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		public static function ProfileFunction(_targetObject:EventDispatcher):void {
			var _profileModel:Profile = Profile(GlobalValue.getValue("profileModel"));
			var _messageObject:MessageObject;
			if (_profileModel == null) {
				_messageObject = new MessageObject("error", "back");
				_messageObject.message = "Help Error: Cannot get profile information from xml.";
			} else {
				_messageObject = new MessageObject(_profileModel.title, _profileModel.backButtonName);
				_messageObject.type = "profile";
				_messageObject.model = _profileModel;
			}
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		public static function PauseProgramFunction(_targetObject:EventDispatcher):void {
			var _programStatusModel:ProgramStatus = ProgramStatus(GlobalValue.getValue("programStatusModel"));
			var _messageObject:MessageObject;
			if (_programStatusModel == null) {
				_messageObject = new MessageObject("error", "back");
				_messageObject.message = "Help Error: Cannot get program status information from xml.";
			} else {
				_messageObject = new MessageObject(_programStatusModel.title, _programStatusModel.backButtonName);
				_messageObject.type = "pause";
				_messageObject.model = _programStatusModel;
			}
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
		public static function ExitProgramFunction(_targetObject:EventDispatcher):void {
			var _exitProgramModel:ProgramStatus = ProgramStatus(GlobalValue.getValue("exitProgramModel"));
			var _messageObject:MessageObject;
			if (_exitProgramModel == null) {
				_messageObject = new MessageObject("error", "back");
				_messageObject.message = "Help Error: Cannot get exit program information from xml.";
			} else {
				_messageObject = new MessageObject(_exitProgramModel.title, _exitProgramModel.backButtonName);
				_messageObject.type = "exit";
				_messageObject.model = _exitProgramModel;
			}
			_targetObject.dispatchEvent(new PageEvent("info", _messageObject));
		}
	}
}