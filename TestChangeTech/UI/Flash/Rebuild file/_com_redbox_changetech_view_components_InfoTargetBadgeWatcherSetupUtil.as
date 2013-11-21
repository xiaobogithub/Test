//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_InfoTargetBadgeWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[3] = new PropertyWatcher("dayTarget", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[4] = new PropertyWatcher("roomVO", {propertyChange:true}, [_arg3[2]], _arg2);
            _arg4[5] = new PropertyWatcher("textColour1", {propertyChange:true}, [_arg3[2]], null);
            _arg4[1] = new PropertyWatcher("languageVO", {propertyChange:true}, [_arg3[0]], null);
            _arg4[3].updateParent(_arg1);
            _arg4[4].updateParent(_arg1);
            _arg4[4].addChild(_arg4[5]);
            _arg4[1].updateParent(_arg2.apply(_arg1, ["model"]));
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            InfoTargetBadge.watcherSetupUtil = new (_com_redbox_changetech_view_components_InfoTargetBadgeWatcherSetupUtil);
        }

    }
}//package 
