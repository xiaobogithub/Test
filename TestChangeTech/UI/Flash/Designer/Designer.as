package {
	import fl.controls.*;
	import flash.display.*;
	import flash.errors.IllegalOperationError;
	import flash.events.*;
	import flash.external.ExternalInterface;
	import flash.filters.*;
	import flash.geom.Rectangle;
	import flash.ui.*;
	import com.ethos.changetech.business.*;
	import com.ethos.changetech.controls.*;

	public class Designer extends Sprite {

		private var _canvasX:Number=100;
		private var _canvasY:Number=0;
		private var _propertiesPanelX:Number=900;
		private var _propertiesPanelY:Number=0;
		private var _selectedFilter:GlowFilter;
		private var _designItems:Array;
		private var _selectedItem:DesignElement;
		private var _propertiesPanel:PropertiesPanel;
		private var _submitPanel:SubmitPanel;
		private var _sessionContentGUID:String;
		private var _postURL:String;

		public function Designer() {
			stage.scaleMode = StageScaleMode.NO_SCALE;
			stage.align = StageAlign.TOP_LEFT;

			var newMenu:ContextMenu = new ContextMenu ();
			newMenu.hideBuiltInItems();
			this.contextMenu = newMenu;

			ExternalInterface.addCallback("getParameters", setParameters);
			ExternalInterface.call("sendParameters");

			_designItems = new Array  ;
			setupToolbar();
			setupPropertiesPanels();
			setupButtons();
			setupSelectedFilter();
		}
		private function setParameters(_sessionContentGUIDValue:String, _postURLValue:String):void {
			_sessionContentGUID = _sessionContentGUIDValue;
			_postURL = _postURLValue;
			trace("JS value set successfully!");
		}
		private function setupToolbar():void {
			//refactor these into one.
			textBlockToolbarButton.addEventListener(DesignElementEvent.CREATE,designElementCreated);
			radioButtonToolbarButton.addEventListener(DesignElementEvent.CREATE,designElementCreated);
			checkBoxToolbarButton.addEventListener(DesignElementEvent.CREATE,designElementCreated);
			buttonToolbarButton.addEventListener(DesignElementEvent.CREATE,designElementCreated);
		}
		private function setupPropertiesPanels() {
			//NOTE: The ColorPicker couldn't be set invisible when the PropertiesPanel is dynamicaly added. That's a bug in AS framework.
			// This will be refactored if I found any way to fix it.
			textBlockPropertiesPanel.visible = false;
			radioButtonPropertiesPanel.visible = false;
			checkBoxPropertiesPanel.visible = false;
			buttonPropertiesPanel.visible = false;
		}
		private function setupButtons() {
			submitButton.addEventListener(MouseEvent.CLICK,submitHandler);
		}
		private function setupSelectedFilter():void {
			var _color:Number = 0x0099ff;
			var _angle:Number = 45;
			var _alpha:Number = 0.8;
			var _blurX:Number = 2;
			var _blurY:Number = 2;
			var _distance:Number = 1;
			var _strength:Number = 0.65;
			var _inner:Boolean = false;
			var _knockout:Boolean = false;
			var _quality:Number = BitmapFilterQuality.HIGH;
			_selectedFilter = new GlowFilter(_color,_alpha,_blurX,_blurY,_strength,_quality,_inner,_knockout);
		}
		private function selectedItemChanged(_newItem:DesignElement):void {
			//do something to the old item;
			if (_newItem == _selectedItem) {
				return;
			}
			if (_selectedItem != null) {
				_selectedItem.filters = null;
			}
			_propertiesPanel = null;
			setupPropertiesPanels();
			//
			_selectedItem = _newItem;
			_selectedItem.filters = new Array(_selectedFilter);
			//update properties panel
			_propertiesPanel = getPropertiesPanel(_selectedItem);
		}
		private function getPropertiesPanel(_designElement:DesignElement):PropertiesPanel {
			switch (_designElement.type) {
				case "TextBlock" :
					_propertiesPanel = textBlockPropertiesPanel;
					break;
				case "RadioButton" :
					_propertiesPanel = radioButtonPropertiesPanel;
					break;
				case "CheckBox" :
					_propertiesPanel = checkBoxPropertiesPanel;
					break;
				case "Button" :
					_propertiesPanel = buttonPropertiesPanel;
					break;
				default :
					throw new IllegalOperationError("Invalid designElement type for creating properties");
			}
			_propertiesPanel.target = _designElement;
			_propertiesPanel.visible = true;
			return _propertiesPanel;
		}
		private function getPageXML():String {
			var _pageXML:XML=<Page SessionContentGUID ="" PageGUID="" Name="" Description=""></Page>;
			_pageXML.@SessionContentGUID = _sessionContentGUID;
			var _pageControlsXML:XML =< PageControls></PageControls>;
			var _item:DesignElement;
			for (var _i:int = 0; _i < _designItems.length; _i++) {
				_item = _designItems[_i];
				_pageControlsXML.appendChild(_item.toXML());

			}
			_pageXML.appendChild(_pageControlsXML);
			return _pageXML.toXMLString();
		}
		private function designElementCreated(_event:DesignElementEvent):void {
			var _designElement = new _event.targetClass();
			_designElement.x = _event.x - _designElement.width / 2 - _canvasX;
			_designElement.y = _event.y - _designElement.height / 2 - _canvasY;
			_designElement.addEventListener(MouseEvent.MOUSE_DOWN,itemSelectedHandler);
			_designItems.push(_designElement);
			canvas.addChild(_designElement);
			selectedItemChanged(_designElement);
		}
		private function itemSelectedHandler(_event:MouseEvent):void {
			selectedItemChanged(DesignElement(_event.target));
			_selectedItem.startDrag(false,new Rectangle(0,0,canvas.width - _selectedItem.width,canvas.height - _selectedItem.height));
			_selectedItem.stage.addEventListener(MouseEvent.MOUSE_MOVE,itemMoveHandler);
			_selectedItem.stage.addEventListener(MouseEvent.MOUSE_UP,itemReleaseHandler);
		}
		private function itemReleaseHandler(_event:MouseEvent):void {
			_selectedItem.stopDrag();
			_selectedItem.stage.removeEventListener(MouseEvent.MOUSE_MOVE,itemMoveHandler);
			_selectedItem.stage.removeEventListener(MouseEvent.MOUSE_UP,itemReleaseHandler);
		}
		private function itemMoveHandler(_event:MouseEvent):void {
			_propertiesPanel.updateLocation();
		}
		private function submitHandler(_event:MouseEvent):void {
			trace("submit");
			trace(getPageXML());
			var _xmlLoader = new XmlLoader(_postURL);
			_xmlLoader.showProgressBar=true;
			_xmlLoader.addEventListener(XmlLoaderEvent.COMPELETED,xmlLoadCompeleted);
			_xmlLoader.addEventListener(XmlLoaderEvent.FAILED,xmlLoadFailed);
			_xmlLoader.send(getPageXML());
			_xmlLoader.x = this.width - 300 / 2;
			_xmlLoader.y = this.height - 10 / 2;
			addChild(_xmlLoader);
			_submitPanel = new SubmitPanel();
			_submitPanel.message = "Saving page.";
			addChild(_submitPanel);
		}
		private function xmlLoadCompeleted(_event:XmlLoaderEvent):void {
			_submitPanel.message = "Page saved";
			_submitPanel.addEventListener(PanelEvent.CLOSE,submitPanelCloseHandler);
		}
		private function xmlLoadFailed(_event:XmlLoaderEvent):void {
			_submitPanel.message = "Save Failed: "+_event.data;
			_submitPanel.addEventListener(PanelEvent.CLOSE,submitPanelCloseHandler);
		}
		private function submitPanelCloseHandler(_event:PanelEvent):void {
			removeChild(_submitPanel);
			_submitPanel = null;
			//close the panel here.
		}
	}
}