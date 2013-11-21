//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_BalanceValueReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[4] = new PropertyWatcher("value_description", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[5] = new PropertyWatcher("copyContainer", {propertyChange:true}, [_arg3[4], _arg3[5]], _arg2);
            _arg4[6] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[5]], null);
            _arg4[3] = new PropertyWatcher("value_label", {propertyChange:true}, [_arg3[2]], _arg2);
            _arg4[0] = new PropertyWatcher("copyHolder", {propertyChange:true}, [_arg3[0]], _arg2);
            _arg4[1] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[0]], null);
            _arg4[2] = new PropertyWatcher("value_icon", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[4].updateParent(_arg1);
            _arg4[5].updateParent(_arg1);
            _arg4[5].addChild(_arg4[6]);
            _arg4[3].updateParent(_arg1);
            _arg4[0].updateParent(_arg1);
            _arg4[0].addChild(_arg4[1]);
            _arg4[2].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceValueReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceValueReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 
