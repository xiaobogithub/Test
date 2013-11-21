//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_WeekdayDataEntryInfoWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[0] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[2], bindings[3], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("buttonGradColour1", {propertyChange:true}, [bindings[3], bindings[0]], null);
            watchers[2] = new PropertyWatcher("buttonGradColour2", {propertyChange:true}, [bindings[2], bindings[0]], null);
            watchers[3] = new PropertyWatcher("model", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[4] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[1]], null);
            watchers[5] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return (["Today"]);
            }, null, [bindings[1]], null);
            watchers[7] = new PropertyWatcher("currentTarget", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[4]);
            watchers[5].parentWatcher = watchers[4];
            watchers[4].addChild(watchers[5]);
            watchers[7].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            WeekdayDataEntryInfo.watcherSetupUtil = new (_com_redbox_changetech_view_components_WeekdayDataEntryInfoWatcherSetupUtil);
        }

    }
}//package 
