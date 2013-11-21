package com.ethos.changetech.controls 
{
	import com.ning.data.GlobalValue;
	import fl.controls.ComboBox;
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.filters.BlurFilter;
	
	/**
	 * ...
	 * @author ...
	 */
	public class ComboBoxTimeZone extends MovieClip
	{
		private var _comboBox:ComboBox;
		private var _enable:Boolean;
		private var _selectedItem:Object;
		private var _itemWidth:Number = 404;
		public function ComboBoxTimeZone() 
		{
			init();
		}
		
		private function init():void
		{
			_comboBox = new ComboBox();
			_comboBox.width = _itemWidth;
			
			var timeZoneData:XML = XML(GlobalValue.getValue("TimeZoneOptions"));
			var timeZoneLength:uint = timeZoneData.option.length();
			
			for (var i:uint = 0; i < timeZoneLength; i++)
			{
				_comboBox.addItem({label:timeZoneData.option[i],value:timeZoneData.option[i].@value});
			}
			
			//default timeZone
			if (GlobalValue.getValue("CurrentTimeZone") != null)
			{
				_comboBox.selectedIndex = timeZoneData.option.(@value == GlobalValue.getValue("CurrentTimeZone")).childIndex();
			}
			else if (GlobalValue.getValue("UserTimeZone") != null && GlobalValue.getValue("UserTimeZone") != "")
			{
				_comboBox.selectedIndex = timeZoneData.option.(@value == GlobalValue.getValue("UserTimeZone")).childIndex();
			}
			else 
			{
				_comboBox.selectedIndex = timeZoneData.option.(@value == GlobalValue.getValue("ProgramTimeZone")).childIndex();
			}
			
			_comboBox.textField.filters = [new BlurFilter(0, 0, 0)];
			
			//_comboBox.addEventListener(Event.CHANGE, change);
			
			this.addChild(_comboBox);
		}
		
		private function change(e:Event):void 
		{
			GlobalValue.setValue("CurrentTimeZone",String(_comboBox.selectedItem.value));
		}
		
		public function set enable(value:Boolean):void 
		{
			_enable = value;
			_comboBox.enabled = value;
		}
		
		public function get selectedItem():Object { return _comboBox.selectedItem; }
		
		public function set selectedItem(value:Object):void 
		{
			_selectedItem = value;
		}
		
	}

}