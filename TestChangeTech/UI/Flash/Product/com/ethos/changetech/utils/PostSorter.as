package com.ethos.changetech.utils{
	import com.ning.controls.StringLoader;
	import flash.display.Loader;
	import flash.events.Event;
	import flash.events.EventDispatcher;
/*
 * sort the post of feedback & assignment
 * */
	public class PostSorter extends EventDispatcher{
		private var _postArray:Array = new Array();
		private static var _instance:PostSorter;
		public function PostSorter():void {
		}
		public static function init():void {
			_instance = new PostSorter();
		}
		public static function getInstance():PostSorter {
			if (_instance == null) {
				init();
			}
			return _instance;
		}
		/*
		 * postArray set to storing the loader and it's data
		 * when add data to the array ,if it's length is one imply it don't need to line up
		 * */
		public static function addToPostArray(_loader:StringLoader, _data:String):void
		{
			_instance._postArray.push( { "loader":_loader, "data":_data } );
			
			if (_instance._postArray.length == 1)
			{
				startSend();
			}
		}
		
		public static function startSend():void
		{
			_instance._postArray[0].loader.addEventListener(Event.COMPLETE, sendCompleteHandler);
			_instance._postArray[0].loader.send(_instance._postArray[0].data);
		}
		/*
		 * when a data return back successfully,remove the loader from array.
		 * */
		public static function sendCompleteHandler(e:Event):void 
		{
			_instance._postArray.shift()
			if (_instance._postArray.length > 0)
			startSend();
		}
	}
}