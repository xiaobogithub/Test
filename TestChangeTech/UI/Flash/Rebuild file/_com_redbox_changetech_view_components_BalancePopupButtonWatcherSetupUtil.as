//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_BalancePopupButtonWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[1] = new PropertyWatcher("languageVO", {propertyChange:true}, [_arg3[0]], null);
            _arg4[1].updateParent(_arg2.apply(_arg1, ["model"]));
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalancePopupButton.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalancePopupButtonWatcherSetupUtil);
        }

    }
}//package 
