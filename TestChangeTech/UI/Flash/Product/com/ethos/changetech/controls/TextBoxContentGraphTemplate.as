package com.ethos.changetech.controls{
	import com.ethos.changetech.models.Graph;
	import com.ethos.changetech.models.GraphItem;
	import com.ethos.changetech.models.Page;
	import com.ethos.changetech.utils.PageExpressionParser;
	import com.ning.data.GlobalValue;
	import com.ning.graphs.GraphBase;
	import com.ning.graphs.StaticLineGraph;
	import flash.errors.IllegalOperationError;
	import flash.text.StyleSheet;
	
	public class TextBoxContentGraphTemplate extends TextBoxContentStandardBasedTemplate {
		
		private var _graphs:Array;
		
		public function TextBoxContentGraphTemplate(_targetPage:Page, _w:Number, _minH:Number, _maxH:Number) {
			_graphs = new Array();
			super(_targetPage, _w, _minH, _maxH);
		}
		override protected function updateInnerContent():void {
			if (_page.graphs.length == 0) {
				return;
			}
			for (var _i:int = 0; _i < _page.graphs.length; _i++) {
				var _graph:GraphBase;
				switch (_page.graphs[_i].type) {
					case "Line graph" :
						_graph = new StaticLineGraph();
						//_graph.hasSymbol = false;
						_graph.styleSheet = StyleSheet(GlobalValue.getValue("css"));
						_graph.caption = _page.graphs[_i].caption;
						_graph.horizontalAxisRange = _page.graphs[_i].scoreRange;
						_graph.horizontalAxisUnit = "";
						_graph.horizontalAxisStep = 1;
						_graph.verticalAxisRange = _page.graphs[_i].timeRange;
						_graph.verticalAxisUnit = _page.graphs[_i].timeUnit;
						_graph.verticalAxisBaseUnit = _page.graphs[_i].timeBaselineUnit;
						_graph.verticalAxisStep = 1;
						for (var _j:int = 0; _j < _page.graphs[_i].badScoreRanges.length; _j++) {
							_graph.setHorizontalBackground(_page.graphs[_i].badScoreRanges[_j], 0xFF0000);
						}

						for (_j = 0; _j < _page.graphs[_i].mediumScoreRanges.length; _j++) {
							_graph.setHorizontalBackground(_page.graphs[_i].mediumScoreRanges[_j], 0xFFFF00);
						}
						for (_j = 0; _j < _page.graphs[_i].goodScoreRanges.length; _j++) {
							_graph.setHorizontalBackground(_page.graphs[_i].goodScoreRanges[_j], 0x00FF00);
						}
						for (_j = 0; _j < _page.graphs[_i].graphItems.length; _j++) {
							trace("old values = " + _page.graphs[_i].graphItems[_j].values);
							for (var _m:int = 0; _m < _page.graphs[_i].graphItems[_j].values.length; _m++) {
								if (_page.graphs[_i].graphItems[_j].values[_m] == "") {
									continue;
								}
								var _value:Number = Number(PageExpressionParser.parseMathExpression(_page.graphs[_i].graphItems[_j].values[_m]));
								if (isNaN(_value)) {
									throw new IllegalOperationError("Invalid value in line " + _page.graphs[_i].graphItems[_j].name + " , value should be numeric type or expression can be valued as numeric type.");
								}
								_page.graphs[_i].graphItems[_j].values[_m] = _value;
							}
							trace("new values = " + _page.graphs[_i].graphItems[_j].values);
							_graph.addRecord({name:_page.graphs[_i].graphItems[_j].name, color:_page.graphs[_i].graphItems[_j].color, pointType:_page.graphs[_i].graphItems[_j].pointType, values:_page.graphs[_i].graphItems[_j].values});
						}
						_graph.draw();
						break;
					default :
						throw new IllegalOperationError("Invalid graph type.");
				}
				_graphs.push(_graph);
				addChild(_graph);
			}
		}
		override protected function innerContainerLayout(_lastPositionY:Number):Number {
			var _maxGraphWidth:Number;
			var _maxGraphHeight:Number;
			var _graphsAmount = _page.graphs.length;
			if (_graphsAmount < 3 ) {
				_maxGraphWidth = (_contentWidth - (_graphsAmount - 1) * _textMargin) / _graphsAmount;
				_maxGraphHeight = _maxHeight - _lastPositionY - 2 * _textMargin;
			} else {
				_maxGraphWidth = (_contentWidth - 2 * _textMargin) / 3;
				var _lineNumber:Number = Math.ceil(_graphsAmount / 3);
				_maxGraphHeight = (_maxHeight - 2 * _textMargin - (_lineNumber - 1) * _textMargin) / _lineNumber;
			}
			var _startPostionY:Number = _lastPositionY + _textMargin;
			var _graph:GraphBase;
			for (var _i:int = 0; _i<_graphs.length; _i++) {
				_graph = _graphs[_i];
				_graph.maxWidth = _maxGraphWidth;
				_graph.maxHeight = Math.max(Number(GlobalValue.getValue("layout")["GraphMinHeight"]), _maxGraphHeight);
				_graph.draw();
				_graph.x = _horizontalPadding + (_i % 3) * (_maxGraphWidth + _textMargin);
				_graph.y = _startPostionY + Math.floor(_i / 3) * (_maxGraphHeight + _textMargin);
				_lastPositionY = Math.max(_lastPositionY, _graph.y + _graph.actualHeight);
			}
			_lastPositionY += _textMargin;
			return _lastPositionY;
		}
		
	}
}