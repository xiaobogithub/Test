//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;

    public class _com_redbox_changetech_view_components_BalanceCopyReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[0] = new PropertyWatcher("textFieldContainer", {propertyChange:true}, [bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("height", {heightChanged:true}, [bindings[0]], null);
            watchers[6] = new PropertyWatcher("copy_str", {propertyChange:true}, [bindings[9], bindings[11], bindings[12]], propertyGetter);
            watchers[3] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[2], bindings[6]], propertyGetter);
            watchers[4] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[2], bindings[6]], null);
            watchers[9] = new PropertyWatcher("copyContainer", {propertyChange:true}, [bindings[13], bindings[14]], propertyGetter);
            watchers[10] = new PropertyWatcher("height", {heightChanged:true}, [bindings[14]], null);
            watchers[5] = new PropertyWatcher("intro_str", {propertyChange:true}, [bindings[8], bindings[7], bindings[5]], propertyGetter);
            watchers[7] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[10]], null);
            watchers[8] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[10]], null);
            watchers[2] = new PropertyWatcher("title_str", {propertyChange:true}, [bindings[4], bindings[1], bindings[3]], propertyGetter);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[6].updateParent(target);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[4]);
            watchers[9].updateParent(target);
            watchers[9].addChild(watchers[10]);
            watchers[5].updateParent(target);
            watchers[7].updateParent(BalanceModelLocator);
            watchers[7].addChild(watchers[8]);
            watchers[2].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceCopyReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceCopyReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 
