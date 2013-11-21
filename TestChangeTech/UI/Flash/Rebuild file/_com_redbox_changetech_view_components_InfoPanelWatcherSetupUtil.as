//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;
    import assets.*;

    public class _com_redbox_changetech_view_components_InfoPanelWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[2] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[1]], null);
            watchers[3] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[1]], null);
            watchers[0] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[0]], null);
            watchers[1] = new PropertyWatcher("fullscreenToggle", {propertyChange:true}, [bindings[0]], null);
            watchers[2].updateParent(BalanceModelLocator);
            watchers[2].addChild(watchers[3]);
            watchers[0].updateParent(Assets);
            watchers[0].addChild(watchers[1]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            InfoPanel.watcherSetupUtil = new (_com_redbox_changetech_view_components_InfoPanelWatcherSetupUtil);
        }

    }
}//package 
