package com.ethos.changetech.models{
	import com.ethos.changetech.data.PageVariables;
	import com.hexagonstar.util.debug.Debug;
	import flash.errors.IllegalOperationError;

	public class Page {
		private var _pageSequence:PageSequence;
		private var _guid:String;
		private var _type:String;
		private var _order:String;
		private var _name:String;
		private var _pageVariableName:String;

		// View properties.
		private var _title:String;
		private var _text:String;
		private var _footerText:String;
		private var _primaryButtonName:String;
		private var _secondaryButtonName:String;
		private var _beforeExpression:String;
		private var _afterExpression:String;
		private var _presenterImage:String;
		private var _presenterImageMode:String;
		private var _presenterImagePosition:String;
		private var _backgroundImage:String;
		private var _singleMode:Boolean = false;
		private var _hideTextMode:Boolean = false;
		private var _hasVideo:Boolean = false;
		// Standare page properties.
		private var _medias:Array;

		// GetInfo page properties.
		private var _questions:Array;
		private var _isReadOnly:Boolean = false;

		// IntervalPicture page properties.
		private var _pushPicture:String;
		private var _interval:int;

		// Preference page properties.
		private var _preferences:Array;
		private var _maxPreferences:int;

		// Graph page properties.
		private var _graphs:Array;
		private var _index:Number;
		
		//SMS page properties
		private var _time:String;
		private var _daysToSend:String;
		
		private var _xmlData:XML;
		public function Page(_parentPageSequence:PageSequence, _xml:XML,index:Number) {
			_index = index;
			_pageSequence = _parentPageSequence;
			_medias = new Array();
			_questions = new Array();
			_preferences = new Array();
			_graphs = new Array();
			fromXML(_xml);
			_xmlData = _xml;
		}
		public function get pageSequence():PageSequence {
			return _pageSequence;
		}
		public function get guid():String {
			return _guid;
		}
		public function get type():String {
			return _type;
		}
		public function get order():String {
			return _order;
		}
		public function get name():String {
			return _name;
		}
		public function get pageVariableName():String {
			return _pageVariableName;
		}
		public function get pageVariable():Object {
			return PageVariables.getProperty(_pageVariableName);
		}
		public function set pageVariable(_value:Object):void {
			if (_pageVariableName != null && _pageVariableName.length > 0) {
				PageVariables.setProperty(_pageVariableName, _value, PageVariables.getPropertyType(_pageVariableName));
			}
		}
		public function get presenterImageMode():String {
			return _presenterImageMode;
		}
		public function get title():String {
			return _title;
		}
		public function get text():String {
			return _text;
		}
		public function get footerText():String {
			return _footerText;
		}
		public function get primaryButtonName():String {
			return _primaryButtonName;
		}
		public function get secondaryButtonName():String {
			return _secondaryButtonName;
		}
		public function get beforeExpression():String {
			return _beforeExpression;
		}
		public function get afterExpression():String {
			return _afterExpression;
		}
		public function get presenterImage():String {
			return _presenterImage;
		}
		public function get presenterImagePosition():String {
			return _presenterImagePosition;
		}
		public function get backgroundImage():String {
			return _backgroundImage;
		}
		public function get medias():Array {
			return _medias;
		}
		public function get questions():Array {
			return _questions;
		}
		public function get isReadOnly():Boolean {
			return _isReadOnly;
		}
		public function set isReadOnly(_value:Boolean):void {
			if (_value == _isReadOnly) {
				return;
			}
			_isReadOnly = _value;
		}
		public function get interval():int {
			return _interval;
		}
		public function get pushPicture():String {
			return _pushPicture;
		}
		public function get preferences():Array {
			return _preferences;
		}
		public function get maxPreferences():int {
			return _maxPreferences;
		}
		public function get graphs():Array {
			return _graphs;
		}
		public function get isRelapse():Boolean{
			return _pageSequence.isRelapse;
		}
		public function get time():String{
			return _time;
		}
		public function get daysToSend():String{
			return _daysToSend;
		}		
		public function get singleMode():Boolean{ 
			return _singleMode; 
		}		
		public function get hideTextMode():Boolean{ 
			return _hideTextMode; 
		}
		public function get hasVideo():Boolean{
			return _hasVideo;
		}
		
		public function get xmlData():XML 
		{
			return _xmlData;
		}
		public function fromXML(_data:XML):void {
			_guid = _data.@GUID;
			_type = _data.@Type;
			_order = _pageSequence.order.toString() + "." + Number(_data.@Order).toString();
			_name = _data.@Name;
			_pageVariableName = _data.@ProgramVariable;
			_title = _data.@Title;
			_text = _data.@Text;
			_footerText = _data.@FooterText;
			_primaryButtonName = _data.@ButtonPrimaryName;
			_secondaryButtonName = _data.@ButtonSecondaryName;
			_beforeExpression = _data.@BeforeExpression;
			_afterExpression = _data.@AfterExpression;
			_presenterImageMode = _data.@PresenterMode;
			
			if(_type == "Push pictures") {
				_pushPicture = _data.@PresenterImage;
				_presenterImage = "";
			} else {
				_presenterImage = _data.@PresenterImage;
			}
			if (_title.length > 0 && _text.length > 0) {
				_singleMode = false;
			}else if (_title.length <=0 && _text.length <= 0) {
				_singleMode = false;
				if (_type == "Standard") {
					_hideTextMode = true;
				}
			}else {
				if (_presenterImage.length == 0 && _type=="Standard") {
					_singleMode = true;
				}
			}
			_presenterImagePosition = _data.@PresenterImagePosition;
			_backgroundImage = _data.@BackgroundImage;
			_interval = _data.@Interval;
			_maxPreferences = _data.@MaxPreferences;
			_time = _data.@Time;
			_daysToSend = _data.@DaysToSend;
			if (_order == "1.1") {
				validation();
			}
			
			switch (_type) {
				case "Standard" :
					var _mediasXMLList:XMLList = _data.Media;
					if (_mediasXMLList.length() > 1) {
						throw new IllegalOperationError("Invalid page media setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + " : Standard page can only contain one or no media.");
					}
					if (_mediasXMLList.length() == 1) {
						var _mediaNode = _mediasXMLList[0];
						var _mediaXmlStr:String = _mediasXMLList.toXMLString();
						trace(_mediaXmlStr);
						_mediaXmlStr = _mediaXmlStr.substr(_mediaXmlStr.indexOf("Type=")+6);
						_mediaXmlStr = _mediaXmlStr.substr(0,_mediaXmlStr.indexOf('"'));

						if (_mediaXmlStr== "Video")
						{
							_hasVideo = true;
						}
						
						trace(_hasVideo);
						addMedia(new Media(this, _mediaNode));
					} else {
						//Debug.trace("      No media in this page.");
					}
					break;
				case "Push pictures" :
					var _mediasXMLList:XMLList = _data.Media;
					if (_mediasXMLList.length() > 1) {
						throw new IllegalOperationError("Invalid page media setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + " : Standard page can only contain one or no media.");
					}
					if (_mediasXMLList.length() > 0) {
						var _mediaNode = _mediasXMLList[0];
						addMedia(new Media(this, _mediaNode));
					} else {
						//Debug.trace("      No media in this page.");
					}
					break;
				case "Get information" :
					var _questionsXMLList:XMLList = _data.Questions;
					if (_questionsXMLList.length() != 1 ) {
						throw new IllegalOperationError("Invalid page question setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ".");
					}
					var _questionNodeList:XMLList = _questionsXMLList[0].Question;
					if (_questionNodeList.length() < 1 || _questionNodeList.length() > 5) {
						throw new IllegalOperationError("Invalid page question setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ": Get information page should contain 1 to 5 questions.");
					}
					for each (var _questionNode:XML in _questionNodeList) {
						addQuestion(new Question(this, _questionNode));
					}

					trace("      question amount = " + _questions.length.toString());
					break;
				case "Choose preferences" :
					var _preferencesXMLList:XMLList = _data.Preferences;
					if (_preferencesXMLList.length() != 1) {
						throw new IllegalOperationError("Invalid page preferences setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ".");
					}
					var _preferenceNodeList:XMLList = _preferencesXMLList[0].Preference;
					if (_preferenceNodeList.length() < 1 || _preferenceNodeList.length() >9) {
						throw new IllegalOperationError("Invalid page preferences setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ": Choose preferences page should contain 1 to 9 preferences.");
					}
					if (_preferenceNodeList.length() < _maxPreferences) {
						throw new IllegalOperationError("Invalid page preferences setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ": The max amount of choosable preferences should not be bigger than the total amount of preferences.");
					}
					for each (var _preferenceNode:XML in _preferenceNodeList) {
						addPreference(new Preference(this, _preferenceNode));
					}


					trace("      preference amount = " + _preferences.length.toString());
					break;
				case "Graph" :
					var _graphsXMLList:XMLList = _data.Graphs;
					if (_graphsXMLList.length() != 1) {
						throw new IllegalOperationError("Invalid page graphs setting in page sequence No." + _pageSequence.order.toString() + " page No." + _order.toString() + ".");
					}
					var _graphNodeList:XMLList = _graphsXMLList[0].Graph;
					for each (var _graphNode:XML in _graphNodeList) {
						addGraph(new Graph(this, _graphNode));
					}


					trace("      graph amount = " + _graphs.length.toString());
					break;
				default :
			}
		}
		private function validation():void {
			Debug.trace("    ******************************");
			Debug.trace("    --page order No. " + _order);
			Debug.trace("      page GUID = " + _guid);
			Debug.trace("      page type = " + _type);
			Debug.trace("      page name = " + _name);
			Debug.trace("      page pageVariableName = " + _pageVariableName);
			Debug.trace("      page body title = " + _title);
			//Debug.trace("      page body text = " + _text);
			Debug.trace("      page body footer text = " + _footerText);
			Debug.trace("      page button primary name = " + _primaryButtonName);
			Debug.trace("      page button secondary name = " + _secondaryButtonName);
			Debug.trace("      page button before expression = " + _beforeExpression);
			Debug.trace("      page button after expression = " + _afterExpression);
			Debug.trace("      page presenter image = " + _presenterImage);
			Debug.trace("      page presenter image position = " + _presenterImagePosition);
			Debug.trace("      page background image = " + _backgroundImage);
			Debug.trace("      page interval = " + _interval);
			Debug.trace("      page maxPreferences = " + _maxPreferences);
			Debug.trace("    ******************************");
		}
		private function addMedia(_media:Media):void {
			_medias.push(_media);
		}
		private function addQuestion(_question:Question):void {
			_questions.push(_question);
		}
		private function addPreference(_preference:Preference):void {
			_preferences.push(_preference);
		}
		private function addGraph(_graph:Graph):void {
			_graphs.push(_graph);
		}
	}
}