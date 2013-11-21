package com.ethos.changetech.data{
	import com.ethos.changetech.models.PageVariable;

	public class PageVariables {

		private static  var _instance:PageVariables;
		private var _variables:Array;

		public function PageVariables() {
			_variables = new Array();
		}
		public static function init():void {
			_instance = new PageVariables();
		}
		public static function getInstance():PageVariables {
			if (_instance == null) {
				init();
			}
			return _instance;
		}
		public static function setProperty(_propertyName:String, _propertyValue:Object, _propertyType:String):void {
			if (_instance._variables[_propertyName] == undefined) {
				_instance._variables[_propertyName] = new PageVariable(_propertyName, _propertyValue, _propertyType);
				trace("initing page variables: name = " + _propertyName + " -- value = " + _instance._variables[_propertyName].value);
			} else {
				_instance._variables[_propertyName].value = _propertyValue;
			}
		}
		public static function getProperty(_propertyName:String):Object {
			if (_instance._variables[_propertyName] == undefined) {
				return null;
			}
			return _instance._variables[_propertyName].value;
		}
		public static function getPropertyType(_propertyName:String):String {
			if (_instance._variables[_propertyName] == undefined) {
				return null;
			}
			return _instance._variables[_propertyName].type;
		}
		public static function setPropertiesFromXML(_xml:XML):void {
			var _xmlList:XMLList = _xml.Variable;
			if ( _xmlList.length()>0) {
				for each (var _variableNode:XML in _xmlList) {
					setProperty(_variableNode.@Name, _variableNode.@Value, _variableNode.@Type);
				}
			}
		}
	}
}