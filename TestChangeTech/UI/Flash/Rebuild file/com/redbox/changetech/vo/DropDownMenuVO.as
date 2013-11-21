//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.vo {
    import com.redbox.changetech.model.*;

    public class DropDownMenuVO {

        public var menuIDs:Array
        private var path:String
        private var model:BalanceModelLocator
        private var menuData:Array = null

        public function DropDownMenuVO(_arg1:XML){
            var _local2:XML;
            menuIDs = [];
            model = BalanceModelLocator.getInstance();
            super();
            for each (_local2 in _arg1.module) {
                menuIDs.push(_local2);
            };
            path = _arg1.path;
        }
        public function getModuleURL(_arg1:String):String{
            return (((path + _arg1) + ".swf"));
        }
        public function getDataProvider():Array{
            var _local1:Number;
            var _local2:Object;
            if (menuData == null){
                menuData = [];
                _local1 = 0;
                while (_local1 < menuIDs.length) {
                    _local2 = new Object();
                    _local2.label = model.languageVO.getLang(menuIDs[_local1]);
                    _local2.icon = (("assets/icons/" + menuIDs[_local1]) + ".png");
                    _local2.value = menuIDs[_local1];
                    _local2.hasLine = true;
                    menuData.push(_local2);
                    _local1++;
                };
            };
            menuData[0].hasLine = false;
            return (menuData);
        }

    }
}//package com.redbox.changetech.vo 
