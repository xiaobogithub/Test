/*
 * PageNavigator get page from ChangeTech's NextPageHanlder
 * via page's before and after expression find out the target page
 * return the target page to ChangeTech to display deploy
 * */
package com.ethos.changetech.utils{
	import com.ethos.changetech.events.PageEvent;
	import com.ethos.changetech.events.SubmitEvent;
	import com.ethos.changetech.utils.PageVariableReplacer;
	import com.ethos.changetech.models.MessageObject;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.models.PageSequence;
	import com.ning.data.GlobalValue;
	import com.ethos.changetech.data.PageVariables;
	import flash.errors.IllegalOperationError;
	import flash.events.EventDispatcher;
	import flash.external.ExternalInterface;

	public class PageNavigator extends EventDispatcher {

		private var _pages:Array;
		private var _relapse:Array;
		private var _currentPage:Page;
		private var _targetPageOrder:String;
		private var _pageHistory:Array;
		private var _lastNormalPageIndex:int;
		private var _pagesStack:Array;
		private var _pageBreak:Page;
		private var _lastPageOfSession:Page;

		public function PageNavigator(_pagesArray:Array, _relapseArrary:Array) {
			if (_pagesArray.length==0) {
				throw new IllegalOperationError("Cannot create PageNavigator with 0 pages.");
			}
			_pages=_pagesArray;
			_relapse=_relapseArrary;
			_pageHistory = new Array();
			_lastPageOfSession = null;
		}
		public function get pages():Array {
			return _pages;
		}

		public function initCurrentPage():void {
			_lastNormalPageIndex=0;
			_pagesStack = new Array();
			_currentPage=expressToPage(_pages[0],"before");
		}
		public function expressToPage(_fromPage:Page, _state:String):Page {
			if (_fromPage==null) {
				return null;
			}
			var _targetPage:Page=_fromPage;
			var _targetOrder:String;
			_pageBreak = _fromPage;
			
			switch (_state) {
				case "before" :
					/*
					 * make sure when fromPage's afterExpression is "EndPage" we must ignore it's beforeExpression;
					 * */
					if (_fromPage.afterExpression.length > 0) {
						
						// afterExp contains EndPage and GOTO END
						//var afterExp=PageExpressionParser.parsePageExpression(_fromPage.afterExpression);
						//if (afterExp == "end") {
						
						if (_fromPage.afterExpression == "EndPage") {
							//_targetPage = gotoPage("end");	
							break;
						}
					}
					
					if (_fromPage.beforeExpression.length>0) {
						_targetOrder=PageExpressionParser.parsePageExpression(_fromPage.beforeExpression);
						if (_targetOrder!="0"&&_targetOrder!=_fromPage.order) {
							_targetPage = gotoPage(_targetOrder);							
						}
						// if _targetOrder == 0, keep the _targetPage in before expression.
					}
					break;
				case "after" :
					if (_fromPage.afterExpression!=null&&_fromPage.afterExpression.length>0) {
						_targetOrder=PageExpressionParser.parsePageExpression(_fromPage.afterExpression);
					
					} else {
						_targetOrder="0";
					}
					
					if (_targetOrder=="0") {
						_targetPage=getNextPage(_fromPage);
					} else {						
						_targetPage=gotoPage(_targetOrder);
					}
					_targetPage = expressToPage(_targetPage, "before");
					trace(_targetOrder);
					break;
				default :
					throw new IllegalOperationError("Invalid page expression state.");
			}
			return _targetPage;
		}
		public function gotoPage(_targetOrder:String):Page {
			
			trace("_targetOrder = "+_targetOrder);
			var _targetPage:Page;
			_targetPageOrder=_targetOrder;
			if (_targetPageOrder=="end") {
				this.dispatchEvent(new PageEvent("end", null));
				return null;
			}
			if (_targetPageOrder.toLowerCase()=="nextpagesequence") {
				_targetPage=getNextPageSequence();
				if (_targetPage==null) {
					trace("Cannot find next pageSequence, goto end!");
					this.dispatchEvent(new PageEvent("end", null));
					return null;
				}
				return expressToPage(_targetPage, "before");
			}
			if (_targetPageOrder.indexOf("Relapse")>0) {//GOSUB Relapse
				_targetPageOrder=_targetPageOrder.substr(9,36);
				trace("_targetPageOrder = " + _targetPageOrder);
				var _targetRelapseArrary:Array=_relapse.filter(getRelapse);
				if (_targetRelapseArrary.length!=1) {
					trace("cannot find relpase.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to relapse"+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				_pagesStack.push(_pageBreak);
				// set page sequence order and pageguid before go to relapse
				GlobalValue.setValue("pageSequenceOrder", _pageBreak.pageSequence.order.toString());
				GlobalValue.setValue("pageGUID", _pageBreak.guid);
				_targetPage=_targetRelapseArrary[0].pages[0];
				return expressToPage(_targetPage, "before");
			} else if (_targetPageOrder.length > 32) {
				trace("###########################");
				trace("_targetPageOrder.length > 32");
				trace("_targetPageOrder : " + _targetPageOrder);
				trace("###########################");
				_targetPageOrder=_targetOrder.substr(0,36);
				var _targetRelapseArrary:Array=_relapse.filter(getRelapse);
				if (_targetRelapseArrary.length!=1) {
					trace("cannot find relpase.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to relapse"+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				_targetPageOrder=_targetOrder;
				var _targetPageArray:Array=_targetRelapseArrary[0].pages.filter(getPage);
				if (_targetPageArray.length!=1) {
					trace("cannot find page.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to "+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				_targetPage=_targetPageArray[0];
				_lastNormalPageIndex=_pages.indexOf(_targetPage);
				return expressToPage(_targetPage, "before");
			} else if (_targetOrder.indexOf('0.') == 0 ) {//GOTO 0.x inside a Relapse
				_targetPageOrder=_pageBreak.pageSequence.guid;
				var _targetRelapseArrary:Array=_relapse.filter(getRelapse);
				if (_targetRelapseArrary.length!=1) {
					trace("cannot find relpase.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to relapse"+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				
				_targetPageOrder=_targetOrder.substr(1);//Modified by Niel to change _targetPageOrder to ".x"
				var _targetPageArray:Array=_targetRelapseArrary[0].pages.filter(getPage);
				if (_targetPageArray.length!=1) {
					trace("cannot find page.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to "+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				_targetPage=_targetPageArray[0];
				_lastNormalPageIndex=_pages.indexOf(_targetPage);
				return expressToPage(_targetPage, "before");
			}else {//normal page next and session page GOTO [1.x,x.x]
				trace("goto else _targetPageOrder"+_targetPageOrder);
				var _targetPageArray:Array=_pages.filter(getPage);//filter the pages who's order match the x.x
				if (_targetPageArray.length!=1) {
					trace("cannot find page.");
					var _messageObject:MessageObject=GlobalValue.getValue("messages")["NavigationError"];
					_messageObject.message+="From "+_currentPage.order+" to "+_targetPageOrder;
					this.dispatchEvent(new PageEvent("info", _messageObject));
					return null;
				}
				_targetPage=_targetPageArray[0];
				_lastNormalPageIndex = _pages.indexOf(_targetPage);			
				
				return expressToPage(_targetPage, "before");
				
				
			}
		}
		public function gotoNextPage(_targetPage:Page):Boolean {
			if (_targetPage==null||_targetPage==_currentPage) {
				return false;
			}
			
			//_pageHistory.push(_currentPage.order);
			//******************
			//DTD1586:Directly save the viewed page ,skip the order compare
			_pageHistory.push(_currentPage);
			//******************
			
			if (! _targetPage.isRelapse) {
				_currentPage = gotoPage(_targetPage.order);
			} else {
				_currentPage = expressToPage(_targetPage, "before");
			}
			return true;
		}
		public function gotoPreviousPage():Boolean {
			if (_pageHistory.length==0) {
				return false;
			}
			
			//_currentPage=gotoPage(_pageHistory.pop());
			//*****************
			//DTD1586
			var targetPage:Page = _pageHistory.pop();
			_lastNormalPageIndex = _pages.indexOf(targetPage);
			_currentPage = expressToPage(targetPage, "before");
			//*****************
			
			return true;
		}
		private function getPage(_element:*, _index:int, _array:Array):Boolean {
			return (Page(_element).order == _targetPageOrder);
		}
		private function getRelapse(_element:*, _index:int, _array:Array):Boolean {
			return (PageSequence(_element).guid == _targetPageOrder);
		}
		private function getNextPageSequence():Page {
			// When GOTO nextPageSequence is in the beforeExpression of the first page, _currentPage is null.
			var _thisPage=_currentPage==null?_pages[0]:_currentPage;
			var _nextPage=getNextPage(_thisPage);
			while (_nextPage != null) {
				if (_nextPage.pageSequence.order!=_thisPage.pageSequence.order) {
					return _nextPage;
				}
				_thisPage=_nextPage;
				_nextPage=getNextPage(_thisPage);
			}
			return null;
		}
		
		private function getNextPage(_fromPage:Page):Page {			
			if (! _fromPage.isRelapse) {
				var _fromPageIndex:int=_pages.indexOf(_fromPage);
				if (_fromPageIndex==-1) {
					throw new IllegalOperationError("Error when navigating pages.");
				}
				if (_pages.length==_fromPageIndex+1) {
					this.dispatchEvent(new PageEvent("end", null));
					return null;
				} else {
					_lastNormalPageIndex=_fromPageIndex+1;
					var _nextPage:Page=_pages[_fromPageIndex+1];
					if (_nextPage.type=="SMS") {
						submitSMS(_nextPage);
						return getNextPage(_nextPage);
					} else {
						return _nextPage;
					}
				}
			} else {
				var _fromPageIndex:int=_currentPage.pageSequence.pages.indexOf(_fromPage);
				if (_fromPageIndex==-1) {
					throw new IllegalOperationError("Error when navigating pages.");
				}
				if (_currentPage.pageSequence.pages.length==_fromPageIndex+1) {
					//this.dispatchEvent(new PageEvent("end", null));
					//return null;
					//return _pages[_lastNormalPageIndex+1];
					var _lastBreakPage:Page = _pagesStack.pop();
					
					/*In preview mode ,take back the breakPage to _pagesStack,
					 *becase in preview mode,the last page can't back to breakPage,
					 *once _pagesStack.pop() , when user back to last page twice will access the null value breakPage
					 */
					if (String(GlobalValue.getValue("mode")) == "Preview")
					{
						if (String(GlobalValue.getValue("PageType")) != "Normal"){
							_pagesStack.push(_lastBreakPage);
						}
						
					}
					
					/*
					* DTD1586:HelpButtonInCTPP,Relapse has no breakpage,so end directly.
					* */
					if (_lastBreakPage == null)
					{
						this.dispatchEvent(new PageEvent("end", null));
						return null;
					}
					
					_currentPage=_lastBreakPage;
					
					return getNextPage(_lastBreakPage);
				} else {
					var _nextPage:Page=_currentPage.pageSequence.pages[_fromPageIndex+1];
					if (_nextPage.type=="SMS") {
						submitSMS(_nextPage);
						return getNextPage(_nextPage);
					} else {
						return _nextPage;
					}
				}
			}
		}
		private function submitSMS(_SMSPage:Page) {
			var _SMSXML:XML = <SMS></SMS>;
			_SMSXML.@Text=PageVariableReplacer.replaceAll(_SMSPage.text);
			if(_SMSPage.pageVariableName!=null)
			{
				_SMSXML.@Time=PageVariables.getProperty(_SMSPage.pageVariableName)
			}
			else
			{
				_SMSXML.@Time=_SMSPage.time;
			}
			_SMSXML.@Days=_SMSPage.daysToSend;
			_SMSXML.@MobilePhone = PageVariables.getProperty("MobilePhone");
			var _submitEvent:SubmitEvent=new SubmitEvent("submit",_SMSXML,false);
			_submitEvent.isSMSEvent=true;
			this.dispatchEvent(_submitEvent);
		}
		public function findLastPageOfSession() {
			if (_lastPageOfSession == null){
				var _currentPageIndex:int = _pages.indexOf(_currentPage);
				var _pageCount:int = _pages.length;
				var _index:int = 0;
				for(_index = _currentPageIndex+1; _index < _pageCount; _index++){
					if (_pages[_index].afterExpression == "EndPage"){
						_lastPageOfSession = _pages[_index];
						break;
					}
				}
			}			
		}
		public function get lastPageOfSession():Page{
			return _lastPageOfSession;
		}
		public function set lastPageOfSession(_value:Page):void{
			_lastPageOfSession = _value;
		}
		
		public function get currentPage():Page { return _currentPage; }
		
		public function set currentPage(value:Page):void 
		{
			_currentPage = value;
		}
		
		public function get pageHistory():Array { return _pageHistory; }
		
		public function set pageHistory(value:Array):void 
		{
			_pageHistory = value;
		}
	}
}