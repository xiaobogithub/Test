//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;

    public class _com_redbox_changetech_view_components_QuizPopUpWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[4] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[3]], null);
            watchers[5] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[3]], null);
            watchers[3] = new PropertyWatcher("bodyCopy", {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[1] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[2] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[1]], null);
            watchers[7] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[4]], null);
            watchers[0] = new PropertyWatcher("headerCopy", {propertyChange:true}, [bindings[0]], propertyGetter);
            watchers[4].updateParent(BalanceModelLocator);
            watchers[4].addChild(watchers[5]);
            watchers[3].updateParent(target);
            watchers[1].updateParent(target);
            watchers[1].addChild(watchers[2]);
            watchers[7].updateParent(propertyGetter.apply(target, ["model"]));
            watchers[0].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            QuizPopUp.watcherSetupUtil = new (_com_redbox_changetech_view_components_QuizPopUpWatcherSetupUtil);
        }

    }
}//package 
