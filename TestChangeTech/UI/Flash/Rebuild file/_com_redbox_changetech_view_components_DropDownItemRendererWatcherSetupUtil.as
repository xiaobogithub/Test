//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_DropDownItemRendererWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[7] = new PropertyWatcher("iconHolder", {propertyChange:true}, [_arg3[4]], _arg2);
            _arg4[9] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[4]], null);
            _arg4[8] = new PropertyWatcher("x", {xChanged:true}, [_arg3[4]], null);
            _arg4[2] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[1]], _arg2);
            _arg4[0] = new PropertyWatcher("data", {dataChange:true}, [_arg3[2], _arg3[3], _arg3[0]], _arg2);
            _arg4[6] = new PropertyWatcher("label", null, [_arg3[3]], null);
            _arg4[5] = new PropertyWatcher("icon", null, [_arg3[2]], null);
            _arg4[1] = new PropertyWatcher("hasLine", null, [_arg3[0]], null);
            _arg4[3] = new PropertyWatcher("line", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[4] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[1]], null);
            _arg4[7].updateParent(_arg1);
            _arg4[7].addChild(_arg4[9]);
            _arg4[7].addChild(_arg4[8]);
            _arg4[2].updateParent(_arg1);
            _arg4[0].updateParent(_arg1);
            _arg4[0].addChild(_arg4[6]);
            _arg4[0].addChild(_arg4[5]);
            _arg4[0].addChild(_arg4[1]);
            _arg4[3].updateParent(_arg1);
            _arg4[3].addChild(_arg4[4]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            DropDownItemRenderer.watcherSetupUtil = new (_com_redbox_changetech_view_components_DropDownItemRendererWatcherSetupUtil);
        }

    }
}//package 
