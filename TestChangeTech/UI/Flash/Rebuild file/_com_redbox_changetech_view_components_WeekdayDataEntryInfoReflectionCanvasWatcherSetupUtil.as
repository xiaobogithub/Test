﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_WeekdayDataEntryInfoReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[0] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[0]], _arg2);
            _arg4[1] = new PropertyWatcher("dataEntryInfo", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[0].updateParent(_arg1);
            _arg4[1].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            WeekdayDataEntryInfoReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_WeekdayDataEntryInfoReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 