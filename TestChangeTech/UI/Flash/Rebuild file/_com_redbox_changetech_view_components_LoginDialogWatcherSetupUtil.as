//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_LoginDialogWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[6] = new PropertyWatcher("_defaultUserName", {propertyChange:true}, [_arg3[4]], _arg2);
            _arg4[8] = new PropertyWatcher("_defaultPassword", {propertyChange:true}, [_arg3[6], _arg3[12]], _arg2);
            _arg4[18] = new PropertyWatcher("_reset_login_error", {propertyChange:true}, [_arg3[17]], _arg2);
            _arg4[1] = new PropertyWatcher("languageVO", {propertyChange:true}, [_arg3[15], _arg3[19], _arg3[8], _arg3[11], _arg3[16], _arg3[18], _arg3[3], _arg3[7], _arg3[13], _arg3[9], _arg3[1], _arg3[14], _arg3[10], _arg3[5], _arg3[0]], null);
            _arg4[4] = new PropertyWatcher("_login_error", {propertyChange:true}, [_arg3[2]], _arg2);
            _arg4[6].updateParent(_arg1);
            _arg4[8].updateParent(_arg1);
            _arg4[18].updateParent(_arg1);
            _arg4[1].updateParent(_arg2.apply(_arg1, ["model"]));
            _arg4[4].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            LoginDialog.watcherSetupUtil = new (_com_redbox_changetech_view_components_LoginDialogWatcherSetupUtil);
        }

    }
}//package 
