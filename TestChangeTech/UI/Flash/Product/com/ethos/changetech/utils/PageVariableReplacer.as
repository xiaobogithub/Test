package com.ethos.changetech.utils{
	import com.ethos.changetech.data.PageVariables;
	import com.ethos.changetech.xml.AssignmentXMLNode;
	import flash.errors.IllegalOperationError;

	public class PageVariableReplacer {

		private static const _valueRegExp:RegExp = new RegExp("\\{V: (?P<propertyName>\/?[a-zA-Z][a-zA-Z0-9\\ ]*?) \\}", "x");

		public static function replaceAll(_input:String):String {
			var _output:String = _input;
			//trace(_output);
			var _result:Array = _valueRegExp.exec(_output);
			while (_result != null) {
				_output = _output.replace(_valueRegExp, PageVariables.getProperty(_result.propertyName));
				_result = _valueRegExp.exec(_output);
			}
			
			//trace("&&**@@");
			//trace(_output);
			if(_output!=null)
			{
			_output = replaceRoundFunction(_output);
			}
			//trace(_output);
			return _output;
		}
		
		public static function replaceRoundFunction(_input:String):String{
			var _output:String = _input;				
			while(_output.indexOf("Round(")>0)
			{
				var _tempstring = _output.substr(_output.indexOf("Round(")+6);
					//trace(_tempstring);
				_tempstring = _tempstring.substr(0,_tempstring.indexOf(")"));
					//trace(_tempstring);
				var _result:Array = _tempstring.split(",");	
				if(_result[0].indexOf("."))
				{
					var _replaceValue:String;
					var _valueArray:Array = _result[0].split(".")
					if(_result[1]=="0")
					{
						_replaceValue =_valueArray[0]; 
					}
					else if(_result[1]=="1")
					{
						if (_valueArray.length == 2)
						{
							_replaceValue = _valueArray[0]+"."+_valueArray[1].substring(0,1);							
						}
						else
						{
							_replaceValue = _valueArray[0]+".0";
						}
					}
					else if(_result[1]=="2")
					{
						if (_valueArray.length == 2)
						{
							if (String(_valueArray[1]).length > 1)
							{
								_replaceValue = _valueArray[0]+"."+_valueArray[1].substring(0,2);
							}
							else
							{
								_replaceValue = _valueArray[0]+"."+_valueArray[1]+"0";
							}
						}
						else
						{
							_replaceValue = _valueArray[0]+".00";
						}
					}
						//trace(_replaceValue);
					_output = _output.replace("Round("+_tempstring+")",_replaceValue);
				}																		 
			}
			
			return _output;
		}
		
		public static function replaceFirstIfNumber(_input:String):String {
			var _output:String = _input;
			var _result:Array = _valueRegExp.exec(_output);
			if(_result != null) {
				if(PageVariables.getPropertyType(_result.propertyName) != "String") {
					_output = _output.replace(_valueRegExp, PageVariables.getProperty(_result.propertyName));
				}
			}
			return _output;
		}
		public static function assign(_variableString:String, _value:String):AssignmentXMLNode{
			var _result:Array = _valueRegExp.exec(_variableString);
			if(_result == null){
				throw new IllegalOperationError("Invalid assignment: The format should be {V:variableName}=value");
			}
			var _propertyType:String = PageVariables.getPropertyType(_result.propertyName);
			if(_propertyType ==  null) {
				throw new IllegalOperationError("Invalid assignment variable name: The format should be {V:variableName}=value");
			}			
			// verify type here?
			
			PageVariables.setProperty(_result.propertyName, _value, _propertyType);
			return new AssignmentXMLNode(_result.propertyName, _value);
		}
	}
}