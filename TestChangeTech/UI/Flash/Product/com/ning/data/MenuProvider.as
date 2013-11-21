package com.ning.data{
	import fl.data.DataProvider;
	
	public class MenuProvider extends DataProvider {
		
		public function addXMLItem(_xmlItem:XML):void {
			trace(_xmlItem.@Name);
			super.addItem({label: _xmlItem.@Name, functionName: _xmlItem.@FunctionName});
		}
		public static function fromXML(_xml:XML):MenuProvider {
			// To be implemented.
			return new MenuProvider();
		}
	}
}