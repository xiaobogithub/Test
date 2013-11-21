package com.ethos.changetech.utils{
	import com.ethos.changetech.data.PageVariables;
	import com.ethos.changetech.xml.AssignmentsXML;
	import com.ethos.changetech.xml.AssignmentXMLNode;
	import com.ning.controls.StringLoader;
	import com.ning.data.GlobalValue;
	import flash.errors.IllegalOperationError;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	
	public class PageExpressionParser {

		public static  const ifElseRegExp:RegExp = new RegExp("^[ ]* (?:IF [ ]+ (?P<ifCondition>.+?) [ ]+){0,1}  (?P<ifExpression>(?:SET|GOTO|GOSUB|GOWEB|EndPage) [ ]+ (?:.(?![ ]ELSE[ ]|[ ]IF[ ]))* [^\\ ])  (?: [ ]+ ELSE [ ]+ (?P<elseExpression>(?:SET|GOTO|GOSUB|GOWEB|EndPage) [ ]+ (?:.(?![ ]ELSE[ ]|[ ]IF[ ]))* [^\\ ])){0,1}", "x");
		
		//public static  const ifElseRegExp:RegExp =new RegExp("^[ ]* (?:IF [ ]+ (?P<ifCondition>.+?) [ ]+){0,1}  (?P<ifExpression>(?:SET|GOTO|GOSUB|GOWEB|EndPage) [ ]+ (?:.(?![ ]ELSE[ ]|[ ]IF[ ]))* [^\\ ])  (?: [ ]+ ELSE [ ]+){0,1} (?P<elseExpression>.*){0,1}", "xi");
		
		public static  const conditionRegExp:RegExp = new RegExp("[ ]* (?P<expressionA>.+?) [ ]* (?P<operator><(?!=)|<=|>(?!=)|>=|==) [ ]* (?P<expressionB>.+?) [ ]* $", "x");
		public static  const executionRegExp:RegExp = new RegExp("^[ ]* (?P<keyword>SET|GOTO|GOSUB|GOWEB|EndPage) [ ]+ (?P<action>(:?.(?!SET|GOTO|GOSUB|GOWEB|EndPage))* [^\\ ])", "x");
		public static  const assingmentRegExp:RegExp = new RegExp("^[ ]* (?P<variable>.+?) [ ]* = [ ]* (?P<value>.+?) [ ]* $", "xi");
		public static  const badEqualFormatRegExp:RegExp = new RegExp("[^><=!] = [^><=]", "x");
		public static  const bracketRegExp:RegExp = new RegExp("(?P<functionName>GetIndex|Round){0,1}\\( (?P<expression>[^\\(\\)]*)\\)", "x");
		public static  const bracketUnmatchRegExp:RegExp = new RegExp("[\\)\\(]");
		public static  const multiDivideRegExp:RegExp = new RegExp("(?P<expressionA> [^\\*\\+-\\/]+?) [ ]* (?P<operator>[\\*\\/]) [ ]* (?P<expressionB>-{0,1}[^\\*\\+-\\/]+)", "x");
		public static  const multiDivideUnmatchRegExp:RegExp = new RegExp("[\\*\\/]");
		public static  const addMinusRegExp:RegExp = new RegExp("^ [ ]* (?P<expressionA> -{0,1} [^\\+-]+?) [ ]* (?P<operator>[\\+-]) [ ]* (?P<expressionB> -{0,1} [^\\+-]+)", "x");

		static public function parsePageExpression(_expression:String):String {
			trace("parsePageExpression " + _expression);
			if (_expression == "EndPage"){
				return "end";
			}
			var _result:Array = ifElseRegExp.exec(_expression);
			if (_result == null) {
				throw new IllegalOperationError("Invalid page expression. Please check format.");
			}
			while (_result != null) {				
				trace("Test1" + _result.ifCondition);
				if (CompareSubExpression(_result.ifCondition)) {
					trace("condition is true");
					trace(_result.ifExpression);
					
					return parseExecutionExpression(_result.ifExpression);
				} else if (_result.elseExpression != "") {
					trace("find else statement");
					return parseExecutionExpression(_result.elseExpression);
				} else {
					trace("remove if when false");
					_expression = _expression.replace(_result[0], "");
					trace("the new expression = " + _expression);
					_result = ifElseRegExp.exec(_expression);
				}
			}
			// When IF is false and no else, default.
			return "0";
		}
		
	    static public function CompareSubExpression(_condition:String):Boolean{
			var _conditionArray:Array = _condition.split(" AND ");
			var flag:Boolean = true;
			for(var i:int=0;i<_conditionArray.length;i++){
				trace(_conditionArray[i]);
				if(parseCompareExpression(_conditionArray[i])==false){
					flag=false;
					break;
					}					
				}
				return flag;
			}			
		
		static public function parseCompareExpression(_condition:String):Boolean {
			if (_condition == "") {
				return true;
			}
			var _result:Array = conditionRegExp.exec(_condition);
			if (_result == null) {
				if (badEqualFormatRegExp.exec(_condition) == null) {
					throw new IllegalOperationError("Invalid page expression. The IF condition is in bad format.");
				} else {
					throw new IllegalOperationError("Invalid page expression. Please use \"==\" instead of \"=\" for equaling.");
				}
			}
			var _valueA = parseMathExpression(_result.expressionA);
			var _valueB = parseMathExpression(_result.expressionB);
			switch (_result.operator) {
				case ">" :
					return Number(_valueA) > Number(_valueB);
				case ">=" :
					return Number(_valueA) >= Number(_valueB);
				case "<" :
					return Number(_valueA) < Number(_valueB);
				case "<=" :
					return Number(_valueA) <= Number(_valueB);
				case "==" :
					return _valueA == _valueB;
				case "!=" :
					return _valueA != _valueB;
				default :
					throw new IllegalOperationError("Invalid comparation operator(internal error, please connect to the developer).");
			}
		}
		static public function parseExecutionExpression(_expression:String):String {
			var _result:Array = executionRegExp.exec(_expression);
			if (_result == null) {
				throw new IllegalOperationError("Invalid execution expression. Please check format.");
			}
			var _isAssigned:Boolean = false;
			var _assignmentsXML:AssignmentsXML = new AssignmentsXML();
			var _isGoTo:Boolean = false;
			var _goToTarget:String = "0";
			while (_result != null) {
				switch (_result.keyword.toLowerCase()) {
					case "set" :
						_assignmentsXML.addAssignment(parseAssignmentExpression(_result.action));
						_isAssigned = true;
						trace("remove set");
						_expression = _expression.replace(_result[0], "");
						trace("the new expression = " + _expression);
						_result = executionRegExp.exec(_expression);
						break;
					case "goto" :
						_isGoTo = true;
						if (_result.action.toLowerCase() == "end" ) {							
							_goToTarget = "end";
						} else if (_result.action.toLowerCase() == "nextpagesequence " ) {	
							_goToTarget = "nextpagesequence";
						} else {
							_goToTarget = parseMathExpression(_result.action)
						}
						break;
					// Chen Pu: 2010-07-07 Support relapse GOSUB {Relapse:}
					case "gosub":
						_isGoTo = true;
						_goToTarget = _result.action;
						break;
					// Di fujie: go to a web page
					case "goweb":		
						_isGoTo = true;
//						var _submitXML:XML = <XMLModel></XMLModel>;
//						_submitXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
//						_submitXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
//						_submitXML.@SessionGUID = String(GlobalValue.getValue("sessionGUID"));
//						_submitXML.@IsRetake = String(GlobalValue.getValue("IsRetake"));
//						_submitXML.appendChild(<SessionEnding/>);
//						var _submitLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
//						_submitLoader.showProgressBar=false;
//						_submitLoader.target = _result.action;
//						_submitLoader.addEventListener(Event.COMPLETE, goWebSubmitCompeletHandler);
//						_submitLoader.addEventListener(IOErrorEvent.IO_ERROR, goWebSubmitFailedHandler);
//						_submitLoader.send(_submitXML.toXMLString());
	
						var str:String = _result.action;

						var tmp:String=str.substr(str.indexOf("?")+1);

						var vars:Array=tmp.split("&");

						var url:String = str.substring(0, str.indexOf("?")+1);

						for(var i:uint=0;i<vars.length;i++)
						{
							var obj:Object=new Object();
							
							obj.name=vars[i].substr(0,3);
							
							obj.varName=vars[i].split(":")[1].substring(0,vars[i].split(":")[1].length-1);
							
							obj.varValue = PageVariables.getProperty(obj.varName);
							
							url += obj.name + "=" + obj.varValue+"&";
							
							
						}
						
						url = url.substr(0, url.length - 1);

						trace("GoWebURL = "+url);

						GlobalValue.setValue("GoWebURL", url);
						_goToTarget = "end";	
						break;
					case "endpage":
						_isGoTo = true;
						_goToTarget = "end";
						break;
					default :
						throw new IllegalOperationError("Invalid execution keyword (GOTO or SET) in expression.");
				}
				if(_isGoTo){
					break;
				}
			}
			if (_isAssigned) {
				var _submitXML:XML = <XMLModel></XMLModel>;
				_submitXML.@UserGUID = String(GlobalValue.getValue("userGUID"));
				_submitXML.@ProgramGUID = String(GlobalValue.getValue("programGUID"));
				_submitXML.@SessionGUID = String(GlobalValue.getValue("sessionGUID"));
				_submitXML.appendChild(_assignmentsXML.xml);
				trace("assignments xml:");
				trace(_submitXML);
				
				var _submitLoader = new StringLoader(String(GlobalValue.getValue("submitURL")));
				_submitLoader.showProgressBar = false;				
				
				PostSorter.addToPostArray(_submitLoader, _submitXML.toXMLString());				
			}
			
			// When there is no GOTO, return 0.
			return _goToTarget;
		}
		static private function goWebSubmitCompeletHandler(_event:Event):void {
			trace("submit completed.");
			//So far, link to go web location
			if (_event.target.target!=null) {
				WindowLocation.href = _event.target.target;	
			}
		}
		static private function goWebSubmitFailedHandler(_event:IOErrorEvent):void {
			trace("submit failed.");
		}
		static public function parseAssignmentExpression(_expression:String):AssignmentXMLNode {
			trace(_expression);
			var _result:Array = assingmentRegExp.exec(_expression);
			 return PageVariableReplacer.assign(_result.variable, parseMathExpression(_result.value));
		}
		static public function parseMathExpression(_expression:String):String {
			trace("input expression = " + _expression);
			//_expression = PageVariableReplacer.replaceAll(_expression);
			// Calculate Parenthesis.
			var _bracketResult:Array = bracketRegExp.exec(_expression);
			while (_bracketResult != null) {
				trace("bracketResult : " + _bracketResult);
				switch (_bracketResult.functionName) {
					case "GetIndex" :
						// GetIndex function parenthesis.
						_expression = _expression.replace(_bracketResult[0], PageExpressionFunctions.getIndex(_bracketResult.expression));
						break;
					case "Round":
						_expression = _expression.replace(_bracketResult[0], PageExpressionFunctions.round(_bracketResult.expression));											  
						break;
					case "" :
						// Math parenthesis.
						_expression = _expression.replace(_bracketResult[0], parseMathExpression(_bracketResult.expression));
						break;
					default :
						throw new IllegalOperationError("Invalid function name in expression.");
				}
				trace("new expression after bracket = " + _expression);
				_bracketResult = bracketRegExp.exec(_expression);
			}
			if (bracketUnmatchRegExp.exec(_expression) != null) {
				throw new IllegalOperationError("Invalid page expression. Parenthesis are not match.");
			}
			// Calculate multiplication and division.
			var _multiDivideResult:Array = multiDivideRegExp.exec(_expression);
			while (_multiDivideResult != null) {
				trace("multiDivideResult : " + _multiDivideResult);
				_multiDivideResult.expressionA = PageVariableReplacer.replaceFirstIfNumber(_multiDivideResult.expressionA);
				_multiDivideResult.expressionB = PageVariableReplacer.replaceFirstIfNumber(_multiDivideResult.expressionB);
				if (isNaN(Number(_multiDivideResult.expressionA)) || isNaN(Number(_multiDivideResult.expressionA))) {
					//throw new IllegalOperationError("Invalid page expression. Can not do multiplication or division on String type value.");
					return _expression;
				}
				switch (_multiDivideResult.operator) {
					case "*" :
						_expression = _expression.replace(_multiDivideResult[0], Number(_multiDivideResult.expressionA) * Number(_multiDivideResult.expressionB));
						break;
					case "/" :
						_expression = _expression.replace(_multiDivideResult[0], Number(_multiDivideResult.expressionA) / Number(_multiDivideResult.expressionB));
						break;
					default :
						throw new IllegalOperationError("Invalid operator(internal error, please connect to the developer).");
				}
				trace("new expression after mulit/divide = " + _expression);
				_multiDivideResult = multiDivideRegExp.exec(_expression);
			}
			if (multiDivideUnmatchRegExp.exec(_expression) != null) {
				throw new IllegalOperationError("Invalid page expression. multiplication or division is not match.");
			}
			
			
			//Calculate addition and subtraction.
			var _addMinusResult:Array = addMinusRegExp.exec(_expression);
			while (_addMinusResult != null) {
				trace("_addMinusResult = " + _addMinusResult);
				_addMinusResult.expressionA = PageVariableReplacer.replaceFirstIfNumber(_addMinusResult.expressionA);
				_addMinusResult.expressionB = PageVariableReplacer.replaceFirstIfNumber(_addMinusResult.expressionB);
				trace("_addMinusResult.expressionA = " + _addMinusResult.expressionA);
				trace("_addMinusResult.expressionB = " + _addMinusResult.expressionB);
				
				// 08312012-niel  if the right or left at operator IS NOT A Number ,return _expression
				if (isNaN(Number(_addMinusResult.expressionA)) || isNaN(Number(_addMinusResult.expressionA))) {
							//throw new IllegalOperationError("Invalid page expression. Can not do subtraction on String type value.");
							return _expression;
					}
				
				switch (_addMinusResult.operator) {
					case "+" :
						if (isNaN(Number(_addMinusResult.expressionA))||isNaN(Number(_addMinusResult.expressionB))) {
							_expression = _expression.replace(_addMinusResult[0], PageVariableReplacer.replaceAll(_addMinusResult.expressionA) + PageVariableReplacer.replaceAll(_addMinusResult.expressionB));
						} else {
							_expression = _expression.replace(_addMinusResult[0], Number(_addMinusResult.expressionA) + Number(_addMinusResult.expressionB));
						}
						break;
					case "-" :
						if (isNaN(Number(_addMinusResult.expressionA)) || isNaN(Number(_addMinusResult.expressionA))) {
							//throw new IllegalOperationError("Invalid page expression. Can not do subtraction on String type value.");
							return _expression;
						}
						_expression = _expression.replace(_addMinusResult[0], Number(_addMinusResult.expressionA) - Number(_addMinusResult.expressionB));
						break;
					default :
						throw new IllegalOperationError("Invalid operator(internal error, please connect to the developer).");
				}
				trace("new expression after add/minus = " + _expression);
				_addMinusResult = addMinusRegExp.exec(_expression);
			}
			_expression = PageVariableReplacer.replaceAll(_expression);
			trace("result : " + _expression);
			return _expression;
		}
	}
}