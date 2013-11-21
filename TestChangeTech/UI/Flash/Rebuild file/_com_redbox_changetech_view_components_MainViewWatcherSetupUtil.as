//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_MainViewWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[2], bindings[4], bindings[9], bindings[8], bindings[11], bindings[6], bindings[1], bindings[3], bindings[10], bindings[7], bindings[5], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[2], bindings[0]], null);
            watchers[8] = new PropertyWatcher("loaderSource", {propertyChange:true}, [bindings[9], bindings[8]], null);
            watchers[10] = new PropertyWatcher("showConsole", {propertyChange:true}, [bindings[11]], null);
            watchers[9] = new PropertyWatcher("showLogin", {propertyChange:true}, [bindings[10]], null);
            watchers[3] = new PropertyWatcher("roomContent", {propertyChange:true}, [bindings[4], bindings[6], bindings[7], bindings[5]], null);
            watchers[4] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[4]], null);
            watchers[6] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[6]], null);
            watchers[7] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[7]], null);
            watchers[5] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[5]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[1], bindings[3]], null);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[8]);
            watchers[0].addChild(watchers[10]);
            watchers[0].addChild(watchers[9]);
            watchers[0].addChild(watchers[3]);
            watchers[4].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[4]);
            watchers[6].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[6]);
            watchers[7].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[7]);
            watchers[5].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[5]);
            watchers[0].addChild(watchers[2]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            MainView.watcherSetupUtil = new (_com_redbox_changetech_view_components_MainViewWatcherSetupUtil);
        }

    }
}//package 
