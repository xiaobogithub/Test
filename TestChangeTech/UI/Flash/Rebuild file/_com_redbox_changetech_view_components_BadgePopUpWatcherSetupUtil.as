//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.model.*;

    public class _com_redbox_changetech_view_components_BadgePopUpWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[5] = new PropertyWatcher("title", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[8] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[4]], null);
            watchers[9] = new PropertyWatcher("balanceStyleSheet", {propertyChange:true}, [bindings[4]], null);
            watchers[7] = new PropertyWatcher("message", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[4] = new PropertyWatcher("badgeAsset", {propertyChange:true}, [bindings[0]], null);
            watchers[6] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[2]], null);
            watchers[11] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[5]], null);
            watchers[0] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[0]], null);
            watchers[1] = new PropertyWatcher("assets", {propertyChange:true}, [bindings[0]], null);
            watchers[2] = new ArrayElementWatcher(target, function (){
                return (target.roomVO.badgeAsset);
            }, [bindings[0]]);
            watchers[5].updateParent(target);
            watchers[8].updateParent(BalanceModelLocator);
            watchers[8].addChild(watchers[9]);
            watchers[7].updateParent(target);
            watchers[4].updateParent(propertyGetter.apply(target, ["roomVO"]));
            watchers[6].updateParent(propertyGetter.apply(target, ["roomVO"]));
            watchers[11].updateParent(propertyGetter.apply(target, ["model"]));
            watchers[0].updateParent(BalanceModelLocator);
            watchers[0].addChild(watchers[1]);
            watchers[2].arrayWatcher = watchers[1];
            watchers[1].addChild(watchers[2]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BadgePopUp.watcherSetupUtil = new (_com_redbox_changetech_view_components_BadgePopUpWatcherSetupUtil);
        }

    }
}//package 
