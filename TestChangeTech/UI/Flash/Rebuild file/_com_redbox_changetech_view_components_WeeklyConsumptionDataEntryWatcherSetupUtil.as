//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class _com_redbox_changetech_view_components_WeeklyConsumptionDataEntryWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[7] = new PropertyWatcher("beer_icon_path", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[6] = new PropertyWatcher("_reportedTotal", {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[5] = new PropertyWatcher("Text", {propertyChange:true}, [bindings[1]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[0]], null);
            watchers[2] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return (["Drinks"]);
            }, null, [bindings[0]], null);
            watchers[7].updateParent(target);
            watchers[6].updateParent(target);
            watchers[5].updateParent(DrinkType.Beer);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[2].parentWatcher = watchers[1];
            watchers[1].addChild(watchers[2]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            WeeklyConsumptionDataEntry.watcherSetupUtil = new (_com_redbox_changetech_view_components_WeeklyConsumptionDataEntryWatcherSetupUtil);
        }

    }
}//package 
