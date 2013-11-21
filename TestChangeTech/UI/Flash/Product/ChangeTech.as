package
{
	import com.ethos.changetech.controls.*;
	import com.ethos.changetech.data.PageVariables;
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.models.*;
	import com.ethos.changetech.models.PageSequence;
	import com.ethos.changetech.utils.PageNavigator;
	import com.ethos.changetech.utils.PostSorter;
	import com.ethos.changetech.utils.WindowLocation;
	import com.ning.controls.CSSLoader;
	import com.ning.controls.StringLoader;
	import com.ning.data.GlobalValue;
	import com.ning.data.MenuProvider;
	import com.ning.display.ColorableSprite;
	import com.ning.events.LoginEvent;
	import fl.transitions.easing.*;
	import fl.transitions.Tween;
	import fl.transitions.TweenEvent;
	import flash.display.BlendMode;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.TimerEvent;
	import flash.external.ExternalInterface;
	import flash.filters.BitmapFilterQuality;
	import flash.filters.BlurFilter;
	import flash.text.TextField;
	import flash.ui.ContextMenu;
	import flash.utils.Timer;
	
	public class ChangeTech extends ColorableSprite
	{
		// System.security.loadPolicyFile("http://changetechstorage.blob.core.windows.net/crossdomain.xml");
		private var _globalValue:GlobalValue;
		private var _pageVariables:PageVariables;
		private var _pageNavigator:PageNavigator;
		private var _postSorter:PostSorter;
		
		private var _background:Background;
		private var _loader:Sprite;
		private var _session:Session;
		private var _sessionContainer:SessionContainer;
		private var _page:Page;
		private var _relapse:Array;
		private var _pageContainer:PageContainer;
		private var _messageBox:MessageBox;
		private var _roomLoader:RoomLoader;
		private var _roomTransition:RoomTransition;
		private var _settingMenuProvider:MenuProvider;
		private var _loadingAni:LoadingAni;
		
		private var _messageBoxAlphaTween:Tween;
		private var _timeWaitTween:Tween;
		private var _isMissedDay:Boolean = false;
		
		private var _topBodyBarColor:uint;
		private var _primary
		
		public var testText:TextField = new TextField();
		public static var _instanse:ChangeTech;
		
		/*
		 * init stage ,events,variables,layouts and parameters call from javascript before parse XML data.
		 * */
		public function ChangeTech()
		{
			initStage();
			initEvent();
			initGlobalValue();
			initPostSorter();
			initPageVariables();
			initJS();
		}
		
		/*
		 * init stage mode and align way
		 * hide the unnecessary menu in right-hand button
		 * */
		private function initStage():void
		{
			stage.scaleMode = StageScaleMode.NO_SCALE;
			stage.align = StageAlign.TOP_LEFT;
			
			var newMenu:ContextMenu = new ContextMenu;
			newMenu.hideBuiltInItems();
			this.contextMenu = newMenu;
		}
		
		/*
		 * add stage resize event,it give response when window resize
		 * */
		private function initEvent():void
		{
			stage.addEventListener(Event.RESIZE, resizeHandler);
		}
		
		/*
		 * decare a global variable container ,
		 * it can been set and get variables any where in this program
		 * */
		private function initGlobalValue():void
		{
			_globalValue = GlobalValue.getInstance();
		}
		
		/*
		 * PostSorter make sure send data to .net one by one,by Niel
		 * */
		private function initPostSorter():void
		{
			_postSorter = PostSorter.getInstance();
		}
		
		private function initPageVariables():void
		{
			_pageVariables = PageVariables.getInstance();
		}
		
		/*
		 * check javascript is ready
		 * */
		private function initJS():void
		{
			if (ExternalInterface.available)
			{
				if (checkJavaScriptReady())
				{
					initJSValue();
				}
				else
				{
					var _readyTimer:Timer = new Timer(100, 0);
					_readyTimer.addEventListener(TimerEvent.TIMER, readyTimerHandler);
					_readyTimer.start();
				}
			}
			else
			{
				trace("ExternalInterface is not available!");
				//var _messageObject:MessageObject = new MessageObject("Error", "OK");
				//_messageObject.message = "Cannot communicate with Java Script!";
				//showMessageBox(_messageObject);
			}
		}
		
		private function checkJavaScriptReady():Boolean
		{
			var _isReady:Boolean = ExternalInterface.call("checkJSReady");
			return _isReady;
		}
		
		private function readyTimerHandler(_event:TimerEvent):void
		{
			trace("Checking JavaScript status...");
			var _isReady:Boolean = checkJavaScriptReady();
			if (_isReady)
			{
				initJSValue();
				Timer(_event.target).stop();
			}
		}
		
		/*
		 * store global variables from js in GlobalValue
		 * */
		private function initJSValue():void
		{
			GlobalValue.setValue("mode", ExternalInterface.call("getMode"));
			GlobalValue.setValue("dataURL", ExternalInterface.call("getURL", "data"));
			GlobalValue.setValue("mediaRootURL", ExternalInterface.call("getURL", "mediaRoot"));
			
			//refactor media url 
			GlobalValue.setValue("originalimagecontainerRoot", ExternalInterface.call("getURL", "originalimagecontainerRoot"));
			GlobalValue.setValue("videocontainerRoot", ExternalInterface.call("getURL", "videocontainerRoot"));
			GlobalValue.setValue("audiocontainerRoot", ExternalInterface.call("getURL", "audiocontainerRoot"));
			GlobalValue.setValue("logocontainerRoot", ExternalInterface.call("getURL", "logocontainerRoot"));
			
			GlobalValue.setValue("submitURL", ExternalInterface.call("getURL", "submit"));
			GlobalValue.setValue("crossdomainURL", ExternalInterface.call("getURL", "crossdomain"));
			GlobalValue.setValue("paymentURL", ExternalInterface.call("getURL", "payment"));
			GlobalValue.setValue("ctppURL", ExternalInterface.call("getURL", "ctpp"));
			loadCSS(ExternalInterface.call("getURL", "css"));
		}
		
		private function loadCSS(_cssURL:String):void
		{
			var _cssLoader:CSSLoader = new CSSLoader();
			_cssLoader.addEventListener(Event.COMPLETE, cssLoadCompleteHandler);
			_cssLoader.addEventListener(IOErrorEvent.IO_ERROR, cssLoadErrorHandler);
			_cssLoader.load(_cssURL);
			_cssLoader.x = (stage.stageWidth - _cssLoader.width) / 2;
			_cssLoader.y = (stage.stageHeight - _cssLoader.height) / 2;
			addChild(_cssLoader);
			_loader = _cssLoader;
		}
		
		private function cssLoadCompleteHandler(_event:Event):void
		{
			GlobalValue.setValue("css", _event.target.styleSheet);
			_event.target.removeEventListener(Event.COMPLETE, cssLoadCompleteHandler);
			_event.target.removeEventListener(IOErrorEvent.IO_ERROR, cssLoadErrorHandler);
			removeChild(CSSLoader(_event.target));
			loadLayoutSetting(ExternalInterface.call("getURL", "layout"));
		}
		
		private function cssLoadErrorHandler(_event:IOErrorEvent):void
		{
			trace("load css error, try again");
			removeChild(CSSLoader(_event.target));
			throw new IllegalOperationError("Cannot download CSS file, please refresh page. If it still doesn't work, please connect your application provider.");
		}
		
		private function loadLayoutSetting(_settingURL:String):void
		{
			var _layoutLoader:StringLoader = new StringLoader(_settingURL);
			trace(_settingURL);
			_layoutLoader.showProgressBar = true;
			_layoutLoader.addEventListener(Event.COMPLETE, layoutLoadCompleted);
			_layoutLoader.load();
			_layoutLoader.x = (stage.stageWidth - _layoutLoader.width) / 2;
			_layoutLoader.y = (stage.stageHeight - _layoutLoader.height) / 2;
			addChild(_layoutLoader);
			_loader = _layoutLoader;
		}
		
		private function layoutLoadCompleted(_event:Event):void
		{
			trace("layout data load completed.");
			GlobalValue.setValue("layout", new Object());
			//trace(_event.target.data);
			var _layoutXML:XML = XML(_event.target.data);
			var _settingList:XMLList = _layoutXML.Setting;
			if (_settingList.length() > 0)
			{
				for each (var _settingNode:XML in _settingList)
				{
					GlobalValue.getValue("layout")[_settingNode.@Name] = _settingNode.@Value;
						//trace("initing layout: name = " + _settingNode.@Name + " -- value = " + GlobalValue.getValue("layout")[_settingNode.@Name]);
				}
			}
			_event.target.removeEventListener(Event.COMPLETE, layoutLoadCompleted);
			removeChild(StringLoader(_event.target));
			
			initContainer();
			loadData();
		}
		
		private function initContainer():void
		{
			//_settingMenuProvider = new MenuProvider();
			_background = new Background();
			_background.draw(stage.stageWidth, stage.stageHeight);
			addChild(_background);
			_pageContainer = new PageContainer();
			_pageContainer.setStageSize(stage.stageWidth, stage.stageHeight);
			_pageContainer.addEventListener(PageEvent.INFO, infoPageHandler);
			_pageContainer.addEventListener(PageEvent.NEXTPAGE, nextPageHandler);
			_pageContainer.addEventListener(PageEvent.PREVIOUSPAGE, previousPageHandler);
			_pageContainer.addEventListener(SubmitEvent.SUBMIT, submitPageHandler);
			_pageContainer.addEventListener(PageEvent.STARTLOADING, pageStartLoadingHandler);
			_pageContainer.addEventListener(PageEvent.STOPLOADING, pageStopLoadingHandler);
			_pageContainer.addEventListener(LoginEvent.COMPLETE, loginCompleteHandler);
			addChild(_pageContainer);
			_sessionContainer = new SessionContainer();
			_sessionContainer.setStageSize(stage.stageWidth, stage.stageHeight);
			_sessionContainer.addEventListener(PageEvent.CATEGORY, showCategoryDescriptionHandler);
			_sessionContainer.addEventListener(PageEvent.INFO, sessionContainerInfoHandler);
			addChild(_sessionContainer);
		}
		
		/*
		 * TODO:load data
		 * */
		private function loadData():void
		{
			if (String(GlobalValue.getValue("dataURL")).length <= 0)
			{
				var _messageObject:MessageObject = new MessageObject("Error", "OK");
				_messageObject.message = "Invalid data URL.";
				showMessageBox(_messageObject);
				return;
			}
			
			/*
			   //aviod cache data
			   var tempDataURL:String = String(GlobalValue.getValue("dataURL"));
			   if (tempDataURL.indexOf("?") == -1)
			   {
			   tempDataURL += String("?r=" + Math.random());
			   }else {
			   tempDataURL += String("&r=" + Math.random());
			   }
			 */
			
			var _dataLoader:StringLoader = new StringLoader(String(GlobalValue.getValue("dataURL")));
			
			_dataLoader.showProgressBar = true;
			_dataLoader.addEventListener(Event.COMPLETE, dataLoadCompleted);
			_dataLoader.load();
			_dataLoader.x = (stage.stageWidth - _dataLoader.width) / 2;
			_dataLoader.y = (stage.stageHeight - _dataLoader.height) / 2;
			addChild(_dataLoader);
			_loader = _dataLoader;
		}
		
		private function dataLoadCompleted(_event:Event):void
		{
			trace("XML data load completed.");
			_event.target.removeEventListener(Event.COMPLETE, dataLoadCompleted);
			removeChild(StringLoader(_event.target));
			_loader = null;
			var _message:String = _event.target.data;
			var _flag:String = _message.charAt(0);
			
			switch (_flag)
			{
				case "0": 
						var _messageObject:MessageObject = new MessageObject("Error", "OK");
						_messageObject.message=_message.substr(2);
						showMessageBox(_messageObject);
						_pageContainer.textBoxContent.dispatchEvent(new PageEvent("setbutton", {enabled: false}));
					break;
				case "1": 
					parseData(_message.substr(2));
					break;
				case "2":
					var ctppURL = GlobalValue.getValue("ctppURL");
					WindowLocation.href = ctppURL + "?CTPP=" + _message.substr(2);
					break;
			}
		}
		
		/*
		 * TODO:parse data
		 * */
		private function parseData(_data:String):void
		{
			var _xmlModel:XML = XML(_data);
			GlobalValue.setValue("userGUID", _xmlModel.@UserGUID);
			GlobalValue.setValue("programGUID", _xmlModel.@ProgramGUID);
			GlobalValue.setValue("MissedDay", _xmlModel.@MissedDay);
			GlobalValue.setValue("ShouldDoDay", _xmlModel.@ShouldDoDay);
			GlobalValue.setValue("IsNeedPinCode", _xmlModel.@IsNeedPinCode);
			GlobalValue.setValue("IsContainTwoParts", _xmlModel.@IsContainTwoParts);
			GlobalValue.setValue("IsNeedSerialNumber", _xmlModel.@IsNeedSerialNumber);
			GlobalValue.setValue("IsRetake", _xmlModel.@IsRetake);
			GlobalValue.setValue("IsCTPPEnable", _xmlModel.@IsCTPPEnable);
			GlobalValue.setValue("IsNoCatchUp", _xmlModel.@IsNoCatchUp);
			GlobalValue.setValue("SupportTimeZone", _xmlModel.@IsSupportTimeZone);
			GlobalValue.setValue("ProgramTimeZone", _xmlModel.@ProgramTimeZone);
			GlobalValue.setValue("UserTimeZone", _xmlModel.@UserTimeZone);
			GlobalValue.setValue("TimeZoneOptions", _xmlModel.TimeZoneList.select);
			GlobalValue.setValue("IsSMSToEmail", _xmlModel.@IsSMSToEmail);
			GlobalValue.setValue("IsNotShowDayAndSetMenu", _xmlModel.@IsNotShowDayAndSetMenu);
			
			/*
			 * DDT:1587: No Catching Up
			 * add a globalValu IsNoCatchUp , accoding to it's value to re-assign "ShouldDoDay" & "MissedDay"
			 * */
			if (GlobalValue.getValue("IsNoCatchUp") == "1")
			{
				GlobalValue.setValue("MissedDay", "false"); //decide the value of isMissedDay in "initSessionContainer()"
				GlobalValue.setValue("ShouldDoDay", "0");
			}
			else
			{
				//keep the value of "ShouldDoDay" & "MissedDay" 
			}
			
			//set page type IsHelpInCTPP/IsReportInCTPP/Normal
			if (_xmlModel.@IsHelpInCTPP == true)
			{
				GlobalValue.setValue("PageType", "HelpInCTPP");
			}
			else if (_xmlModel.@IsReportInCTPP == true)
			{
				GlobalValue.setValue("PageType", "ReportInCTPP");
			}
			else if (_xmlModel.hasOwnProperty("Session"))
			{
				GlobalValue.setValue("PageType", "Normal");
			}
			
			if (_xmlModel.ProgramVariables.length() > 0)
			{
				PageVariables.setPropertiesFromXML(_xmlModel.ProgramVariables[0]);
			}
			if (_xmlModel.GeneralVariables.length() > 0)
			{
				PageVariables.setPropertiesFromXML(_xmlModel.GeneralVariables[0]);
			}
			if (_xmlModel.TipMessages.length() > 0)
			{
				GlobalValue.setValue("messages", new Object());
				var _messageNodeList:XMLList = _xmlModel.TipMessages[0].Message;
				if (_messageNodeList.length() > 0)
				{
					for each (var _messageNode:XML in _messageNodeList)
					{
						var _messageObject:MessageObject = new MessageObject(_messageNode.@Title, _messageNode.@BackButtonName);
						_messageObject.message = _messageNode.@Message;
						GlobalValue.getValue("messages")[_messageNode.@Name] = _messageObject;
							//trace("initing message: name = " + _messageNode.@Name + " -- title = " + 						GlobalValue.getValue("messages")[_messageNode.@Name].title + " -- message = " + GlobalValue.getValue("messages")[_messageNode.@Name].message + " -- backButtonName = " + GlobalValue.getValue("messages")[_messageNode.@Name].backButtonName);
					}
				}
			}
			_settingMenuProvider = new MenuProvider();
			if (_xmlModel.SettingMenu.length() > 0)
			{
				GlobalValue.setValue("settingMenuTitle", _xmlModel.SettingMenu[0].@Title);
				var _settingMenuItemList:XMLList = _xmlModel.SettingMenu[0].MenuItem;
				if (_settingMenuItemList.length() > 0)
				{
					for each (var _menuItemNode:XML in _settingMenuItemList)
					{
						if (_menuItemNode.@FunctionName == "TimeZoneFunction")
						{
							if (GlobalValue.getValue("SupportTimeZone") == "1")
							_settingMenuProvider.addXMLItem(_menuItemNode);
						}else {
							_settingMenuProvider.addXMLItem(_menuItemNode);
						}
					}
				}
			}
			if (_xmlModel.SmsToEmail.length() > 0)
			{
				GlobalValue.setValue("smsToEmailModel", _xmlModel.SmsToEmail[0]);
			}
			if (_xmlModel.TimeZone.length() > 0)
			{
				GlobalValue.setValue("timeZoneModel", _xmlModel.TimeZone[0]);
			}
			if (_xmlModel.Help.length() > 0)
			{
				var _helpModel:Help = new Help(_xmlModel.Help[0]);
				GlobalValue.setValue("helpModel", _helpModel);
			}
			if (_xmlModel.TipFriend.length() > 0)
			{
				var _tipFriendModel:TipFriend = new TipFriend(_xmlModel.TipFriend[0]);
				GlobalValue.setValue("tipFriendModel", _tipFriendModel);
			}
			if (_xmlModel.Profile.length() > 0)
			{
				var _profileModel:Profile = new Profile(_xmlModel.Profile[0]);
				GlobalValue.setValue("profileModel", _profileModel);
			}
			if (_xmlModel.ProgramStatus.length() > 0)
			{
				var _programStatusModel:ProgramStatus = new ProgramStatus(_xmlModel.ProgramStatus[0]);
				GlobalValue.setValue("programStatusModel", _programStatusModel);
			}
			if (_xmlModel.ExitProgram.length() > 0)
			{
				var _exitProgramModel:ProgramStatus = new ProgramStatus(_xmlModel.ExitProgram[0]);
				GlobalValue.setValue("exitProgramModel", _exitProgramModel);
			}
			if (_xmlModel.SpecialString.length() > 0)
			{
				GlobalValue.setValue("specialString", new Object());
				var _stringItemList:XMLList = _xmlModel.SpecialString[0].StringItem;
				trace(_stringItemList.length());
				if (_stringItemList.length() > 0)
				{
					for each (var _stringItem:XML in _stringItemList)
					{
						trace(_stringItem.@Name);
						GlobalValue.getValue("specialString")[_stringItem.@Name] = _stringItem.@Value;
					}
				}
			}
			if (_xmlModel.Session.length() > 0)
			{
				_session = new Session(_xmlModel.Session[0]);
				GlobalValue.setValue("sessionGUID", _session.guid);
				initSessionContainer();
				
				// load relapse
				if (_xmlModel.Relapse.length() > 0)
				{
					var _pageSequenceXMLList:XMLList = _xmlModel.Relapse[0].PageSequence;
					if (_pageSequenceXMLList.length() > 0)
					{
						var i = 0;
						_relapse = new Array();
						for each (var _pageSequenceNode:XML in _pageSequenceXMLList)
						{
							_relapse.push(new PageSequence(_session, _pageSequenceNode, i, true));
							i++;
						}
							//trace("relapse amount = " + _relapse.length.toString());
					}
				}
				if (_session.pages.length > 0)
				{
					initPageNavigator();
				}
			}
			
			//----judge if the data is a independent relapse from CTPP----
			if (GlobalValue.getValue("PageType") == "ReportInCTPP" || GlobalValue.getValue("PageType") == "HelpInCTPP")
			{
				//pack the assigned relapse into a session.
				var relapses:XML =
					<Relapse></Relapse>;
				relapses.insertChildBefore(null, _xmlModel.Relapse[0].PageSequence.(@GUID == _xmlModel.@PageSequenceGUID));
				_session = new Session(relapses);
				
				GlobalValue.setValue("sessionGUID", _session.guid);
				initSessionContainer();
				
				// load relapse
				if (_xmlModel.Relapse.length() > 0)
				{
					var _pageSequenceXMLList:XMLList = _xmlModel.Relapse[0].PageSequence;
					if (_pageSequenceXMLList.length() > 0)
					{
						var i = 0;
						_relapse = new Array();
						for each (var _pageSequenceNode:XML in _pageSequenceXMLList)
						{
							_relapse.push(new PageSequence(_session, _pageSequenceNode, i, true));
							i++;
						}
							//trace("relapse amount = " + _relapse.length.toString());
					}
				}
				if (_session.pages.length > 0)
				{
					initPageNavigator();
				}
			}
			//--------
		}
		
		private function initSessionContainer():void
		{
			_sessionContainer.logo = _session.logoURL;
			var dayLabel:String = "Dag";
			if (GlobalValue.getValue("specialString") != null && GlobalValue.getValue("specialString")["Day"] != null)
			{
				dayLabel = String(GlobalValue.getValue("specialString")["Day"]);
			}
			_sessionContainer.setDay(_session.day, dayLabel);
			_sessionContainer.settingMenuTitle = String(GlobalValue.getValue("settingMenuTitle"));
			_sessionContainer.settingMenuProvider = _settingMenuProvider;
			
			trace("Before tip message **********************");
			trace("********: " + GlobalValue.getValue("MissedDay"));
			if (GlobalValue.getValue("MissedDay") == "true")
			{
				_isMissedDay = true;
			}
			else
			{
				_isMissedDay = false;
			}
		
		}
		
		private function initPageNavigator():void
		{
			_pageNavigator = new PageNavigator(_session.pages, _relapse);
			_pageNavigator.addEventListener(PageEvent.INFO, infoPageHandler);
			_pageNavigator.addEventListener(PageEvent.END, endPageHandler);
			_pageNavigator.addEventListener(SubmitEvent.SUBMIT, submitPageHandler);
			_pageNavigator.initCurrentPage();
			_page = _pageNavigator.currentPage;
			
			//judge if need missded day
			var catchingUpToggle:Boolean = GlobalValue.getValue("PageType") == "Normal" ? true : false;
			catchingUpToggle = catchingUpToggle ? (_isMissedDay == true && GlobalValue.getValue("IsRetake").toLowerCase() == "false") : false;
			
			if (catchingUpToggle)
			{
				//String(GlobalValue.getValue("messages")["CatchingUpEarlyDay"])
				var _messageObject:MessageObject = GlobalValue.getValue("messages")["CatchingUpEarlyDay"];
				//_messageObject.title = String(GlobalValue.getValue("IsRetake"));
				trace("Before show message box!");
				trace(_pageContainer);
				showMessageBox(_messageObject);
				return;
				trace("after tip message catch up early day");
			}
			else
			{
				updatePageContainer();
				sendPageStartMessage();
				sendPageSequenceStartMessage();
			}
		}
		
		/*
		 *page change's layout delpoy
		 * */
		private function updatePageContainer():void
		{
			if (_page == null)
			{
				return;
			}
			trace("Opening page: Page sequence " + _page.pageSequence.order.toString() + " Page " + _page.order.toString());
			_pageContainer.page = _page; //[Important] connector of current page data and layout 
			if (_page.pageSequence.programRoomName != _sessionContainer.programRoomName)
			{
				if (_pageContainer.textBoxContent != null)
				{
					_sessionContainer.exitRoom();
					_pageContainer.exitRoom();
					_timeWaitTween = new Tween(_pageContainer, "x", null, _pageContainer.x, _pageContainer.x, 20, false);
					_timeWaitTween.addEventListener(TweenEvent.MOTION_FINISH, exitRoomBeforeTransitionMotionFinishHandler);
				}
				else
				{
					initRoomTranstion();
				}
			}
			else
			{
				//TODO:Page Switch
				_pageContainer.switchPage();
			}
		}
		
		private function exitRoomBeforeTransitionMotionFinishHandler(_event:TweenEvent):void
		{
			initRoomTranstion();
		}
		
		private function initRoomTranstion():void
		{
			primaryThemeColor = _page.pageSequence.primaryThemeColor;
			secondaryThemeColor = _page.pageSequence.secondaryThemeColor;
			_roomTransition = new RoomTransition();
			_roomTransition.addEventListener(Event.ENTER_FRAME, roomTransitionLoadRoomHandler);
			_roomTransition.addEventListener(Event.ENTER_FRAME, roomTransitionLoadPageHandler);
			_roomTransition.addEventListener(Event.ENTER_FRAME, roomTransitionFinishHandler);
			_roomTransition.x = stage.stageWidth / 2;
			_roomTransition.y = stage.stageHeight / 2;
			addChild(_roomTransition);
		}
		
		private function roomTransitionLoadRoomHandler(_event:Event):void
		{
			if (MovieClip(_event.target).currentFrame >= 40)
			{
				_event.target.removeEventListener(Event.ENTER_FRAME, roomTransitionLoadRoomHandler);
				_roomLoader = new RoomLoader();
				_roomLoader.label = _page.pageSequence.programRoomName;
				//_roomLoader.primaryThemeColor=_secondaryThemeColor;
				_roomLoader.primaryThemeColor = _page.pageSequence.TopBarColor;
				_roomLoader.x = (stage.stageWidth - _roomLoader.width) / 2;
				_roomLoader.y = (stage.stageHeight - _roomLoader.height) / 2;
				addChild(_roomLoader);
			}
		}
		
		private function roomTransitionLoadPageHandler(_event:Event):void
		{
			if (MovieClip(_event.target).currentFrame >= 80)
			{
				_event.target.removeEventListener(Event.ENTER_FRAME, roomTransitionLoadPageHandler);
				if (contains(_roomLoader))
				{
					removeChild(_roomLoader);
				}
				//_background.topBarColor=_primaryThemeColor;
				_background.topBarColor = _page.pageSequence.TopBarColor;
				_background.draw(stage.stageWidth, stage.stageHeight);
				//_sessionContainer.setCategory(_page.pageSequence.programRoomName, _secondaryThemeColor);
				//_pageContainer.primaryThemeColor=_primaryThemeColor;
				_sessionContainer.setCategory(_page.pageSequence.programRoomName, _page.pageSequence.TopBarColor);
				_pageContainer.enterRoom();
			}
		}
		
		private function roomTransitionFinishHandler(_event:Event):void
		{
			if (MovieClip(_event.target).currentFrame >= 116)
			{
				_event.target.removeEventListener(Event.ENTER_FRAME, roomTransitionFinishHandler);
				removeChild(MovieClip(_event.target));
			}
		}
		
		/*
		 * DTD1586:Judge PageType and sent end page message
		 * */
		private function getEndPage():void
		{
			if (GlobalValue.getValue("GoWebURL") != null)
			{
				sendUnsecureWebURL();
			}
			else
			{
				if (GlobalValue.getValue("PageType") == "ReportInCTPP" || GlobalValue.getValue("PageType") == "HelpInCTPP")
				{
					getRelapseFromCTPPEndPage();
				}
				else if (GlobalValue.getValue("PageType") == "Normal")
				{
					getNormalEndPage();
				}
			}
			
		}
		
		private function sendUnsecureWebURL():void
		{
			trace("send Unsecure WebURL.");
			if (String(GlobalValue.getValue("mode")) != "Preview")
			{
				_pageContainer.dispatchEvent(new PageEvent("startloading", null));
				var _endingXML:XML =   <XMLModel></XMLModel>;
				_endingXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
				_endingXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
				_endingXML.@SessionGUID = "";
				_endingXML.@URL = GlobalValue.getValue("GoWebURL");
				_endingXML.appendChild(<GoWeb/>);					
				trace(_endingXML.toXMLString());
				var _endingLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
				_endingLoader.showProgressBar = false;
				_endingLoader.addEventListener(Event.COMPLETE, webURLEncryptCompeletHandler);
				_endingLoader.addEventListener(IOErrorEvent.IO_ERROR, endingLoaderFailedHandler);
				_endingLoader.send(_endingXML.toXMLString());
			}
		}
		
		//get end page after webURL has been encrypt
		private function webURLEncryptCompeletHandler(e:Event):void 
		{
			
				var encryptedURL:String = e.target.data.substr(e.target.data.indexOf(";")+1);
				GlobalValue.setValue("GoWebURL", encryptedURL)
				
				if (GlobalValue.getValue("PageType") == "ReportInCTPP" || GlobalValue.getValue("PageType") == "HelpInCTPP")
				{
					getRelapseFromCTPPEndPage();
				}
				else if (GlobalValue.getValue("PageType") == "Normal")
				{
					getNormalEndPage();
				}
		}
		
		private function getRelapseFromCTPPEndPage():void
		{
			trace("get end page.");
			if (String(GlobalValue.getValue("mode")) != "Preview")
			{
				_pageContainer.dispatchEvent(new PageEvent("startloading", null));
				var _endingXML:XML =   <XMLModel></XMLModel>;
				
				_endingXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
				_endingXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
				_endingXML.@SessionGUID = "";
				_endingXML.@PageSequenceGUID = _page.pageSequence.guid;
				_endingXML.@IsHelpInCTPP = String(GlobalValue.getValue("PageType")) == "HelpInCTPP" ? "true" : "false";
				_endingXML.@IsReportInCTPP = String(GlobalValue.getValue("PageType")) == "ReportInCTPP" ? "true" : "false";
				//lastPageOfSession == null decide whether display the last page in CTPP
				if (_pageNavigator.lastPageOfSession != null)
				{
					_endingXML.@PageGUID = _pageNavigator.lastPageOfSession.guid;
				}
				else
				{
					_endingXML.@PageGUID = "";
				}
				//_endingXML.@IsRetake = String(GlobalValue.getValue("IsRetake"));						
				_endingXML.appendChild(<RelapseEnd/>);					
				trace(_endingXML.toXMLString());
				var _endingLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
				_endingLoader.showProgressBar = false;
				_endingLoader.addEventListener(Event.COMPLETE, endingLoaderCompeletHandler);
				_endingLoader.addEventListener(IOErrorEvent.IO_ERROR, endingLoaderFailedHandler);
				_endingLoader.send(_endingXML.toXMLString());
			}
		}
		
		private function getNormalEndPage():void
		{
			trace("get end page.");
			if (String(GlobalValue.getValue("mode")) != "Preview")
			{
				_pageContainer.dispatchEvent(new PageEvent("startloading", null));
				var _endingXML:XML =   <XMLModel></XMLModel>;
				_endingXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
				_endingXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
				_endingXML.@SessionGUID = _session.guid;
				if (_pageNavigator.lastPageOfSession != null)
				{
					_endingXML.@PageGUID = _pageNavigator.lastPageOfSession.guid;
				}
				_endingXML.@IsRetake = String(GlobalValue.getValue("IsRetake"));
				_endingXML.appendChild(<SessionEnding/>);					
				trace(_endingXML.toXMLString());
				var _endingLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
				_endingLoader.showProgressBar = false;
				_endingLoader.addEventListener(Event.COMPLETE, endingLoaderCompeletHandler);
				_endingLoader.addEventListener(IOErrorEvent.IO_ERROR, endingLoaderFailedHandler);
				_endingLoader.send(_endingXML.toXMLString());
			}
		}
		
		private function endingLoaderCompeletHandler(_event:Event):void
		{
			trace("ending page successful.");
			_pageContainer.dispatchEvent(new PageEvent("stoploading", null));
			trace("ending message:");
			if (GlobalValue.getValue("GoWebURL") != null)
			{ //GOWeb execute
				WindowLocation.href = String(GlobalValue.getValue("GoWebURL"));
			}
			else
			{
				var _message:String = _event.target.data;
				trace(_message);
				var _flag:String = _message.charAt(0);
				switch (_flag)
				{
					case "0": 
						var _messageObject:MessageObject = GlobalValue.getValue("messages")["LoginFailed"];
						if (_messageObject != null)
						{
							_messageObject.message = _message.substr(2);
						}
						else
						{
							_messageObject = new MessageObject("Error", "OK");
							_messageObject.message = _message.substr(2);
						}
						showMessageBox(_messageObject);
						_pageContainer.textBoxContent.dispatchEvent(new PageEvent("setbutton", {enabled: true}));
						break;
					case "1": 
						_pageContainer.dispatchEvent(new LoginEvent("complete", _message.substr(2)));
						break;
					case "3": //2012/9/19 15:54
						_pageContainer.dispatchEvent(new LoginEvent("complete", _message.substr(2)));
						break;
					case "2": 
						//TODO:DTD1586,feedback ctpp url
						var ctppURL = GlobalValue.getValue("ctppURL");
						WindowLocation.href = ctppURL + "?CTPP=" + _message.substr(2);
						break;
					default: 
						throw new IllegalOperationError("Invalid ending page server state.");
				}
			}
		}
		
		private function endingLoaderFailedHandler(_event:IOErrorEvent):void
		{
			trace("ending page failed.");
			_pageContainer.dispatchEvent(new PageEvent("stoploading", null));
			showMessageBox(GlobalValue.getValue("messages")["NetworkError"]);
			_pageContainer.textBoxContent.dispatchEvent(new PageEvent("setbutton", {enabled: true}));
		}
		
		private function showMessageBox(_messageObject:MessageObject):void
		{
			_messageBox = new MessageBox(_messageObject);
			_messageBox.primaryThemeColor = _primaryThemeColor;
			_messageBox.x = (stage.stageWidth - _messageBox.width) / 2;
			_messageBox.y = (stage.stageHeight - _messageBox.height) / 2;
			_messageBox.addEventListener(PageEvent.CATEGORY, hideMessageBoxHandler);
			_messageBox.addEventListener(PageEvent.ARRANGED, arrangeMessageBoxHandler);
			_messageBox.addEventListener(SubmitEvent.SUBMIT, submitSettingHandler);
			var _blurFilter:BlurFilter = new BlurFilter(5, 5, BitmapFilterQuality.HIGH);
			if (_sessionContainer != null)
			{
				_sessionContainer.filters = new Array(_blurFilter);
				_sessionContainer.mouseEnabled = false;
				_sessionContainer.mouseChildren = false;
			}
			if (_pageContainer != null)
			{
				_pageContainer.filters = new Array(_blurFilter);
				_pageContainer.blendMode = BlendMode.MULTIPLY;
				_pageContainer.mouseEnabled = false;
				_pageContainer.mouseChildren = false;
				if (_pageContainer.textBoxContent != null)
				{
					_pageContainer.textBoxContent.freeze();
				}
			}
			addChild(_messageBox);
			_messageBoxAlphaTween = new Tween(_messageBox, "alpha", Regular.easeOut, 0, 1, 1, true);
		}
		
		private function arrangeMessageBoxHandler(_event:PageEvent):void
		{
			_messageBox.x = (stage.stageWidth - _messageBox.width) / 2;
			_messageBox.y = (stage.stageHeight - _messageBox.height) / 2;
		}
		
		private function hideMessageBoxHandler(_event:PageEvent):void
		{
			if (_sessionContainer != null)
			{
				_sessionContainer.revertMenu();
				_sessionContainer.filters = null;
				_sessionContainer.mouseEnabled = true;
				_sessionContainer.mouseChildren = true;
			}
			if (_pageContainer != null)
			{
				_pageContainer.filters = null;
				_pageContainer.blendMode = BlendMode.NORMAL;
				_pageContainer.mouseEnabled = true;
				_pageContainer.mouseChildren = true;
				if (_pageContainer.textBoxContent != null)
				{
					_pageContainer.textBoxContent.unfreeze();
				}
			}
			if (contains(_messageBox))
			{
				removeChild(_messageBox);
			}
			if (_isMissedDay == true)
			{
				//_isMissedDay=false;
				updatePageContainer();
			}
		}
		
		private function layout():void
		{
			if (_background != null && contains(_background))
			{
				_background.draw(stage.stageWidth, stage.stageHeight);
			}
			if (_loader != null && contains(_loader))
			{
				_loader.x = (stage.stageWidth - _loader.width) / 2;
				_loader.y = (stage.stageHeight - _loader.height) / 2;
			}
			if (_messageBox != null && contains(_messageBox))
			{
				_messageBox.x = (stage.stageWidth - _messageBox.width) / 2;
				_messageBox.y = (stage.stageHeight - _messageBox.height) / 2;
			}
			if (_sessionContainer != null && contains(_sessionContainer))
			{
				_sessionContainer.setStageSize(stage.stageWidth, stage.stageHeight);
			}
			if (_pageContainer != null && contains(_pageContainer))
			{
				_pageContainer.setStageSize(stage.stageWidth, stage.stageHeight);
			}
			if (_roomTransition != null && contains(_roomTransition))
			{
				_roomTransition.x = stage.stageWidth / 2;
				_roomTransition.y = stage.stageHeight / 2;
			}
			if (_roomLoader != null && contains(_roomLoader))
			{
				_roomLoader.x = (stage.stageWidth - _roomLoader.width) / 2;
				_roomLoader.y = (stage.stageHeight - _roomLoader.height) / 2;
			}
			if (_loadingAni != null && contains(_loadingAni))
			{
				_loadingAni.x = (stage.stageWidth - _loadingAni.width) / 2;
				_loadingAni.y = (stage.stageHeight - _loadingAni.height) / 2;
			}
		}
		
		private function resizeHandler(_event:Event):void
		{
			layout();
		}
		
		private function pageStartLoadingHandler(_event:PageEvent):void
		{
			_loadingAni = new LoadingAni();
			_loadingAni.width = 25;
			_loadingAni.height = 25;
			_loadingAni.x = (stage.stageWidth - _loadingAni.width) / 2;
			_loadingAni.y = (stage.stageHeight - _loadingAni.height) / 2;
			addChild(_loadingAni);
			var _blurFilter:BlurFilter = new BlurFilter(5, 5, BitmapFilterQuality.HIGH);
			_sessionContainer.filters = new Array(_blurFilter);
			_sessionContainer.mouseEnabled = false;
			_sessionContainer.mouseChildren = false;
			_pageContainer.filters = new Array(_blurFilter);
			_pageContainer.blendMode = BlendMode.MULTIPLY;
			_pageContainer.mouseEnabled = false;
			_pageContainer.mouseChildren = false;
			_pageContainer.textBoxContent.freeze();
			if (_messageBox != null && contains(_messageBox))
			{
				_messageBox.filters = new Array(_blurFilter);
				_messageBox.mouseEnabled = false;
				_messageBox.mouseChildren = false;
			}
		}
		
		private function pageStopLoadingHandler(_event:PageEvent):void
		{
			if (_messageBox != null && contains(_messageBox))
			{
				_messageBox.filters = null;
				_messageBox.mouseEnabled = true;
				_messageBox.mouseChildren = true;
			}
			else
			{
				_sessionContainer.filters = null;
				_sessionContainer.mouseEnabled = true;
				_sessionContainer.mouseChildren = true;
				_pageContainer.filters = null;
				_pageContainer.blendMode = BlendMode.NORMAL;
				_pageContainer.mouseEnabled = true;
				_pageContainer.mouseChildren = true;
				_pageContainer.textBoxContent.unfreeze();
			}
			if (_loadingAni != null && contains(_loadingAni))
			{
				removeChild(_loadingAni);
			}
		}
		
		private function infoPageHandler(_event:PageEvent):void
		{
			trace("info");
			if (!_event.data)
			{
				showMessageBox(new MessageObject("Lack Tip Message in programs","ok"));
			}else {
				showMessageBox(MessageObject(_event.data));
			}
		}
		
		private function showCategoryDescriptionHandler(_event:PageEvent):void
		{
			if (_page != null)
			{
				var _messageObject:MessageObject = GlobalValue.getValue("messages")["ProgramRoomDescription"];
				_messageObject.title = _page.pageSequence.programRoomName;
				_messageObject.message = _page.pageSequence.programRoomDescription;
				showMessageBox(_messageObject);
			}
		}
		
		private function sessionContainerInfoHandler(_event:PageEvent):void
		{
			showMessageBox(MessageObject(_event.data));
		}
		
		/*
		 * NextPage Handler ,respond the primary button click event.
		 * Contains all the page change function's entrance like GOTO,Next Page,Next Sequence...
		 * */
		private function nextPageHandler(_event:PageEvent):void
		{
			sendPageEndMessage();
			
			//find the actual target page
			var _targetPage:Page = _pageNavigator.expressToPage(_page, "after");
			
			//judge if need shoudDoDay
			var catchingUpToggle:Boolean = (GlobalValue.getValue("PageType") == "Normal")&&GlobalValue.getValue("IsNoCatchUp")=="1" ? false : true;
			catchingUpToggle = catchingUpToggle ? (int(GlobalValue.getValue("ShouldDoDay")) <= 1) : true;
			trace("catchingUpToggle : " + catchingUpToggle);
			
			if (String(GlobalValue.getValue("IsCTPPEnable")).toLowerCase() == "true" || String(GlobalValue.getValue("IsCTPPEnable")).toLowerCase() == "1")
			{
				//TODO: Send page end event
				if (_targetPage == null)
				{
					_pageNavigator.lastPageOfSession = null;
					sendPageSequenceEndMessage();
					getEndPage();
				}
				else if (_targetPage.afterExpression == "EndPage" && catchingUpToggle)
				{
					_pageNavigator.lastPageOfSession = _targetPage;
					sendPageSequenceEndMessage();
					getEndPage();
				}
				else if (_targetPage.type == "Account creation" && String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true")
				{
					_page = _targetPage;
					nextPageHandler(new PageEvent("nextpage", null));
				}
				else if (_pageNavigator.currentPage != _targetPage)
				{
					// When NextPage's BeforeExpression goto end, _pageNavigator.gotoNextPage will return true, but _currentPage is null. 
					if (_pageNavigator.currentPage != null)
					{
						var _pageSequenceChanged:Boolean = false;
						if (_page.pageSequence.order != _pageNavigator.currentPage.pageSequence.order)
						{
							sendPageSequenceEndMessage();
							_pageSequenceChanged = true;
						}
						
						//_page = _pageNavigator.currentPage;	
						_pageNavigator.pageHistory.push(_pageNavigator.currentPage);
						_page = _targetPage;
						_pageNavigator.currentPage = _targetPage;
						
						updatePageContainer();
						if (_pageSequenceChanged)
						{
							sendPageSequenceStartMessage();
						}
						sendPageStartMessage();
					}
					else
					{
						trace("next page is null");
						_pageNavigator.lastPageOfSession = null;
						sendPageSequenceEndMessage();
						getEndPage();
					}
				}
				else
				{
					sendPageSequenceEndMessage();
				}
			}
			else
			{
				if (_targetPage == null)
				{
					_pageNavigator.lastPageOfSession = null;
					sendPageSequenceEndMessage();
					getEndPage();
				}
				else if (_pageNavigator.currentPage != _targetPage)
				{
					if (_pageNavigator.currentPage != null)
					{
						var _pageSequenceChanged:Boolean = false;
						if (_page.pageSequence.order != _pageNavigator.currentPage.pageSequence.order)
						{
							sendPageSequenceEndMessage();
							_pageSequenceChanged = true;
						}
						
						//_page = _pageNavigator.currentPage;	
						_pageNavigator.pageHistory.push(_pageNavigator.currentPage);
						_page = _targetPage;
						_pageNavigator.currentPage = _targetPage;
						
						updatePageContainer();
						if (_pageSequenceChanged)
						{
							sendPageSequenceStartMessage();
						}
						sendPageStartMessage();
					}
					else
					{
						trace("next page is null");
						_pageNavigator.lastPageOfSession = null;
						sendPageSequenceEndMessage();
						getEndPage();
					}
				}
				else
				{
					sendPageSequenceEndMessage();
				}
			}
		}
		
		private function previousPageHandler(_event:PageEvent):void
		{
			//TODO: Send page end event
			if (_pageNavigator.gotoPreviousPage())
			{
				_page = _pageNavigator.currentPage;
				updatePageContainer();
			}
		}
		
		private function endPageHandler(_event:PageEvent):void
		{
			trace("end");
			_pageNavigator.findLastPageOfSession();
			// Will call getEndPage later
			//getEndPage();
		}
		
		private function loginCompleteHandler(_event:LoginEvent):void
		{
			trace(_event.message);
			parseData(_event.message);
		}
		
		private function submitPage(_submitTarget:Object, _message:String, _isFeedback:Boolean = true):void
		{
			var _submitLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
			_submitLoader.showProgressBar = false;
			
			//trace("sendOut message :***  "+_message);
			
			if (_submitTarget != null)
			{
				_submitLoader.target = PageContainer(_submitTarget).textBoxContent;
			}
			
			if (String(GlobalValue.getValue("IsRetake")).toLowerCase() == "true" && _isFeedback)
			{
			}
			else
			{
				if (_isFeedback)
				{
					_submitLoader.addEventListener(Event.COMPLETE, submitPageLoaderCompeletHandler);
					_submitLoader.addEventListener(IOErrorEvent.IO_ERROR, submitPageLoaderFailedHandler);
				}
				
				if (!_isFeedback)
				{
					_submitLoader.send(_message);
				}
				else
				{
					PostSorter.addToPostArray(_submitLoader, _message);
				}
			}
		}
		
		/*
		 * TODO:Send page massage before enter the next.
		 * */
		private function sendPageEndMessage():void
		{
			var _msgXML:XML =   <XMLModel></XMLModel>;
			_msgXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_msgXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			_msgXML.@SessionGUID = _session.guid;
			_msgXML.@PageGUID = _page.guid;
			_msgXML.@PageSequenceOrder = _page.pageSequence.order.toString();
			_msgXML.@PageSequenceGUID = _page.pageSequence.guid;
			_msgXML.appendChild(<PageEnd/>);
			submitPage(null, _msgXML.toString(), false);
		}
		
		private function sendPageStartMessage():void
		{
			var _msgXML:XML =   <XMLModel></XMLModel>;
			_msgXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_msgXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			_msgXML.@SessionGUID = _session.guid;
			_msgXML.@PageGUID = _page.guid;
			_msgXML.@PageSequenceOrder = _page.pageSequence.order.toString();
			_msgXML.@PageSequenceGUID = _page.pageSequence.guid;
			_msgXML.appendChild(<PageStart/>);
			submitPage(null, _msgXML.toString(), false);
		}
		
		private function sendPageSequenceStartMessage():void
		{
			var _msgXML:XML =   <XMLModel></XMLModel>;
			_msgXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_msgXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			_msgXML.@SessionGUID = _session.guid;
			_msgXML.@PageGUID = _page.guid;
			_msgXML.@PageSequenceOrder = _page.pageSequence.order.toString();
			_msgXML.@PageSequenceGUID = _page.pageSequence.guid;
			_msgXML.appendChild(<PageSequenceStart/>);
			submitPage(null, _msgXML.toString(), false);
		}
		
		private function sendPageSequenceEndMessage():void
		{
			var _msgXML:XML =   <XMLModel></XMLModel>;
			_msgXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_msgXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			_msgXML.@SessionGUID = _session.guid;
			_msgXML.@PageGUID = _page.guid;
			_msgXML.@PageSequenceOrder = _page.pageSequence.order.toString();
			_msgXML.@PageSequenceGUID = _page.pageSequence.guid;
			_msgXML.appendChild(<PageSequenceEnd/>);
			submitPage(null, _msgXML.toString(), false);
		}
		
		private function submitPageHandler(_event:SubmitEvent):void
		{
			trace("submit page " + _event.isFeedback);
			var _submitXML:XML =   <XMLModel></XMLModel>;
			_submitXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_submitXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			_submitXML.@SessionGUID = _session.guid;
			if (_page.pageSequence.order.toString() == "")
			{
				//Relapse
				_submitXML.@PageSequenceOrder = String(GlobalValue.getValue("pageSequenceOrder")) ? String(GlobalValue.getValue("pageSequenceOrder")) : "";
				_submitXML.@PageGUID = String(GlobalValue.getValue("pageGUID")) ? String(GlobalValue.getValue("pageGUID")) : "";
				_submitXML.@RelapsePageSequenceGUID = _page.pageSequence.guid;
				_submitXML.@RelapsePageGUID = _page.guid;
			}
			else
			{
				_submitXML.@PageSequenceOrder = _page.pageSequence.order.toString();
				_submitXML.@PageGUID = _page.guid;
			}
			_submitXML.appendChild(_event.xml);
			trace(_submitXML.toXMLString());
			var submitTarget:Object;
			if (_event.isSMSEvent)
			{
				submitTarget = null;
			}
			else
			{
				submitTarget = _event.target;
			}
			submitPage(submitTarget, _submitXML.toXMLString(), _event.isFeedback);
		}
		
		private function submitPageLoaderCompeletHandler(_event:Event):void
		{
			trace("submit page completed.");
			if (_event.target.target != null)
			{
				TextBoxContentTemplate(_event.target.target).submitSuccessfulCallBack(_event.target.data);
			}
		}
		
		private function submitPageLoaderFailedHandler(_event:IOErrorEvent):void
		{
			trace("submit page failed.");
			if (_event.target.target != null)
			{
				TextBoxContentTemplate(_event.target.target).submitFailedCallBack(_event.target.data);
			}
		}
		
		private function submitSetting(_submitTarget:Object, _message:String):void
		{
			var _submitLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
			_submitLoader.showProgressBar = false;
			if (_submitTarget != null)
			{
				_submitLoader.target = MessageBox(_submitTarget).textBoxContent;
			}
			_submitLoader.addEventListener(Event.COMPLETE, submitSettingLoaderCompeletHandler);
			_submitLoader.addEventListener(IOErrorEvent.IO_ERROR, submitSettingLoaderFailedHandler);
			_submitLoader.send(_message);
		}
		
		private function submitSettingHandler(_event:SubmitEvent):void
		{
			trace("submit setting");
			var _settingXML:XML = _event.xml;
			_settingXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
			_settingXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
			trace(_settingXML.toXMLString());
			submitSetting(_event.target, _settingXML.toXMLString());
		}
		
		private function submitSettingLoaderCompeletHandler(_event:Event):void
		{
			trace("submit page completed.");
			if (_event.target.target != null)
			{
				MessageBoxContentTemplate(_event.target.target).submitSuccessfulCallBack(_event.target.data);
			}
		}
		
		private function submitSettingLoaderFailedHandler(_event:IOErrorEvent):void
		{
			trace("submit page failed.");
			if (_event.target.target != null)
			{
				MessageBoxContentTemplate(_event.target.target).submitFailedCallBack(_event.target.data);
			}
		}
	}
}