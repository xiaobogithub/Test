//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_BalanceImageReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[2] = new PropertyWatcher("source", {propertyChange:true}, [_arg3[4]], _arg2);
            _arg4[0] = new PropertyWatcher("contentImage", {propertyChange:true}, [_arg3[2], _arg3[6], _arg3[5], _arg3[0]], _arg2);
            _arg4[5] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[6]], null);
            _arg4[4] = new PropertyWatcher("y", {yChanged:true}, [_arg3[6]], null);
            _arg4[3] = new PropertyWatcher("source", {sourceChanged:true}, [_arg3[5]], null);
            _arg4[2].updateParent(_arg1);
            _arg4[0].updateParent(_arg1);
            _arg4[0].addChild(_arg4[5]);
            _arg4[0].addChild(_arg4[4]);
            _arg4[0].addChild(_arg4[3]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceImageReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceImageReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 
