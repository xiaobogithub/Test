﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.vo.*;

    public class _com_redbox_changetech_view_components_BalanceSecondaryButtonReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[3] = new PropertyWatcher("action", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[2] = new StaticPropertyWatcher("SECONDARY", {propertyChange:true}, [_arg3[2]], null);
            _arg4[1] = new PropertyWatcher("buttonLabel", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[4] = new PropertyWatcher("cta_btn", {propertyChange:true}, [_arg3[4], _arg3[5]], _arg2);
            _arg4[5] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[5]], null);
            _arg4[0] = new PropertyWatcher("buttonEnabled", {propertyChange:true}, [_arg3[0]], _arg2);
            _arg4[3].updateParent(_arg1);
            _arg4[2].updateParent(Button);
            _arg4[1].updateParent(_arg1);
            _arg4[4].updateParent(_arg1);
            _arg4[4].addChild(_arg4[5]);
            _arg4[0].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceSecondaryButtonReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceSecondaryButtonReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 