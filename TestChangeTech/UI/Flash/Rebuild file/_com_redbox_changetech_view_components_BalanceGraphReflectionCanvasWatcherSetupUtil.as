//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;

    public class _com_redbox_changetech_view_components_BalanceGraphReflectionCanvasWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[19] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[18]], null);
            watchers[20] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[18]], null);
            watchers[21] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[22]], null);
            watchers[22] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[22]], null);
            watchers[13] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[10]], null);
            watchers[14] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[10]], null);
            watchers[11] = new PropertyWatcher("_completionScore", {propertyChange:true}, [bindings[9]], propertyGetter);
            watchers[8] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[6]], null);
            watchers[9] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[6]], null);
            watchers[1] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[2] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[1]], null);
            watchers[10] = new PropertyWatcher("_copy_str", {propertyChange:true}, [bindings[15], bindings[19], bindings[8], bindings[23], bindings[21], bindings[11], bindings[16], bindings[20], bindings[24], bindings[7], bindings[12]], propertyGetter);
            watchers[6] = new PropertyWatcher("_screeningScore", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[4] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[17], bindings[13], bindings[4], bindings[9], bindings[5]], null);
            watchers[16] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[14]], null);
            watchers[17] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[14]], null);
            watchers[0] = new PropertyWatcher("title_str", {propertyChange:true}, [bindings[2], bindings[3], bindings[0]], propertyGetter);
            watchers[19].updateParent(BalanceModelLocator);
            watchers[19].addChild(watchers[20]);
            watchers[21].updateParent(BalanceModelLocator);
            watchers[21].addChild(watchers[22]);
            watchers[13].updateParent(BalanceModelLocator);
            watchers[13].addChild(watchers[14]);
            watchers[11].updateParent(target);
            watchers[8].updateParent(BalanceModelLocator);
            watchers[8].addChild(watchers[9]);
            watchers[1].updateParent(target);
            watchers[1].addChild(watchers[2]);
            watchers[10].updateParent(target);
            watchers[6].updateParent(target);
            watchers[4].updateParent(propertyGetter.apply(target, ["model"]));
            watchers[16].updateParent(BalanceModelLocator);
            watchers[16].addChild(watchers[17]);
            watchers[0].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceGraphReflectionCanvas.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceGraphReflectionCanvasWatcherSetupUtil);
        }

    }
}//package 
