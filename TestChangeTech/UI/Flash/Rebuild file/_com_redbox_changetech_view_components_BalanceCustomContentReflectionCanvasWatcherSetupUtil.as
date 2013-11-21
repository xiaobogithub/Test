//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;

    public class _com_redbox_changetech_view_components_BalanceCustomContentReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[7] = new PropertyWatcher("contentContainer", {propertyChange:true}, [bindings[13], bindings[12]], propertyGetter);
            watchers[9] = new PropertyWatcher("height", {heightChanged:true}, [bindings[13]], null);
            watchers[8] = new PropertyWatcher("y", {yChanged:true}, [bindings[13]], null);
            watchers[4] = new PropertyWatcher("copy_str", {propertyChange:true}, [bindings[8], bindings[11], bindings[10]], propertyGetter);
            watchers[1] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[1], bindings[5]], propertyGetter);
            watchers[2] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[1], bindings[5]], null);
            watchers[3] = new PropertyWatcher("intro_str", {propertyChange:true}, [bindings[4], bindings[6], bindings[7]], propertyGetter);
            watchers[5] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[9]], null);
            watchers[6] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[9]], null);
            watchers[0] = new PropertyWatcher("title_str", {propertyChange:true}, [bindings[2], bindings[3], bindings[0]], propertyGetter);
            watchers[7].updateParent(target);
            watchers[7].addChild(watchers[9]);
            watchers[7].addChild(watchers[8]);
            watchers[4].updateParent(target);
            watchers[1].updateParent(target);
            watchers[1].addChild(watchers[2]);
            watchers[3].updateParent(target);
            watchers[5].updateParent(BalanceModelLocator);
            watchers[5].addChild(watchers[6]);
            watchers[0].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceCustomContentReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceCustomContentReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 
