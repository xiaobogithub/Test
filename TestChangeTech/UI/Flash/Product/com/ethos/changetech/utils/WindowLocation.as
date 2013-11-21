
// START PACKAGE
package com.ethos.changetech.utils {
	
	import flash.external.ExternalInterface;

	// START CLASS
	public class WindowLocation {
		
		/*
		 * STATIC
		 */
		
		private static var _cache:Boolean = false;
		private static var _cached:Array = [];
		
		private static function call(functionName:String, ... arguments):* {
			if(_cache) {
				if(_cached[functionName+'-'+arguments.join('-')] != undefined) {
					return _cached[functionName+'-'+arguments.join('-')];
				}
			}  
			if(ExternalInterface.available) {
				return _cached[functionName+'-'+arguments.join('-')] = ExternalInterface.call(functionName, arguments);
			}
			return null;
		}
		
		/*
		 * METHODS
		 */
		
		static public function assign(url:String):void {
			ExternalInterface.call("window.location.assign", url);
		}
		
		static public function reload(forceGet:Boolean = false):void {
			ExternalInterface.call("window.location.reload", forceGet);
		}
		
		static public function replace(url:String):void {
			ExternalInterface.call("window.location.replace", url);
		}
		
		/*
		 * PROPERTIES
		 */
		
		// href
		static public function get href():String {
			return call("window.location.href.toString");
		}
		
		static public function set href(v:String):void {
			assign(v);
		}
		
		// protocol read-only
		static public function get protocol():String {
			return call("window.location.protocol.toString");
		}
		
		// host read-only
		static public function get host():String {
			return call("window.location.host.toString");
		}
		
		// hostname read-only
		static public function get hostname():String {
			return call("window.location.hostname.toString");
		}
		
		// port read-only
		static public function get port():String {
			return call("window.location.port.toString");
		}
		
		// pathname read-only
		static public function get pathname():String {
			return call("window.location.pathname.toString");
		}
		
		// search read-only
		static public function get search():String {
			return call("window.location.search.toString");
		}
		
		// hash read-only
		static public function get hash():String {
			return call("window.location.hash.toString");
		}
		
		/* get the query variables into a dynamic Parameters object */
		// parameters read-only (but with dynamic descendants)
		static public function get parameters():QueryParameters {
			return new QueryParameters(search.substring(1));
		}
		
		/* setting cache to true means that ExternalInterface calls only need to be called once */
		// cache
		static public function get cache():Boolean {
			return _cache;
		}
		static public function set cache(v:Boolean):void {
			_cache = v;
		}
	}
	// END CLASS
}
// END PACKAGE