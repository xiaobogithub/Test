//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_BalanceButtonWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[0] = new PropertyWatcher("buttonWidth", {propertyChange:true}, [bindings[9], bindings[0]], propertyGetter);
            watchers[5] = new PropertyWatcher("fillColor_1", {propertyChange:true}, [bindings[7]], propertyGetter);
            watchers[3] = new PropertyWatcher("iconClass", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[6] = new PropertyWatcher("label", {labelChanged:true}, [bindings[8]], propertyGetter);
            watchers[4] = new PropertyWatcher("fillColor_2", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[2] = new FunctionReturnWatcher("getFillColors", target, function ():Array{
                return ([]);
            }, {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[1] = new PropertyWatcher("buttonHeight", {propertyChange:true}, [bindings[4], bindings[6], bindings[1]], propertyGetter);
            watchers[7] = new PropertyWatcher("buttonField", {propertyChange:true}, [bindings[9]], propertyGetter);
            watchers[8] = new PropertyWatcher("x", {xChanged:true}, [bindings[9]], null);
            watchers[0].updateParent(target);
            watchers[5].updateParent(target);
            watchers[3].updateParent(target);
            watchers[6].updateParent(target);
            watchers[4].updateParent(target);
            watchers[2].updateParent(target);
            watchers[1].updateParent(target);
            watchers[7].updateParent(target);
            watchers[7].addChild(watchers[8]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceButton.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceButtonWatcherSetupUtil);
        }

    }
}//package 
