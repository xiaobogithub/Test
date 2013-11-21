//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import assets.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class _com_redbox_changetech_view_components_WeekdayWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[3] = new PropertyWatcher("day", {propertyChange:true}, [bindings[2], bindings[9], bindings[16]], propertyGetter);
            watchers[9] = new PropertyWatcher("defaultGlow", {propertyChange:true}, [bindings[13], bindings[20]], propertyGetter);
            watchers[5] = new PropertyWatcher("drinks_label", {propertyChange:true}, [bindings[4], bindings[11], bindings[18]], propertyGetter);
            watchers[7] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[8]], null);
            watchers[8] = new PropertyWatcher("tickGrey", {propertyChange:true}, [bindings[8]], null);
            watchers[1] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[1]], null);
            watchers[2] = new PropertyWatcher("tick", {propertyChange:true}, [bindings[1]], null);
            watchers[4] = new PropertyWatcher("drinks", {propertyChange:true}, [bindings[17], bindings[28], bindings[3], bindings[10]], propertyGetter);
            watchers[28] = new PropertyWatcher("visible", {show:true, hide:true}, [bindings[28]], null);
            watchers[10] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[15]], null);
            watchers[11] = new PropertyWatcher("tickGrey", {propertyChange:true}, [bindings[15]], null);
            watchers[27] = new PropertyWatcher("drinks_string", {propertyChange:true}, [bindings[27]], propertyGetter);
            watchers[12] = new PropertyWatcher("consumption", {propertyChange:true}, [bindings[23], bindings[24], bindings[22]], propertyGetter);
            watchers[13] = new PropertyWatcher("Modified", {propertyChange:true}, [bindings[24], bindings[22]], null);
            watchers[14] = new PropertyWatcher("Closed", {propertyChange:true}, [bindings[24], bindings[22]], null);
            watchers[18] = new PropertyWatcher("DayOfWeek", {propertyChange:true}, [bindings[23]], null);
            watchers[15] = new PropertyWatcher("model", {propertyChange:true}, [bindings[23]], propertyGetter);
            watchers[16] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[23]], null);
            watchers[17] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return ([target.consumption.DayOfWeek]);
            }, null, [bindings[23]], null);
            watchers[20] = new PropertyWatcher("_reportedValues", {propertyChange:true}, [bindings[25]], propertyGetter);
            watchers[23] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[25]], null);
            watchers[24] = new PropertyWatcher("Value", null, [bindings[25]], null);
            watchers[21] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[25]], null);
            watchers[22] = new PropertyWatcher("Value", null, [bindings[25]], null);
            watchers[25] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[25]], null);
            watchers[26] = new PropertyWatcher("Value", null, [bindings[25]], null);
            watchers[0] = new PropertyWatcher("tickImage", {propertyChange:true}, [bindings[14], bindings[7], bindings[0]], propertyGetter);
            watchers[6] = new PropertyWatcher("selectedGlow", {propertyChange:true}, [bindings[6]], propertyGetter);
            watchers[3].updateParent(target);
            watchers[9].updateParent(target);
            watchers[5].updateParent(target);
            watchers[7].updateParent(Assets);
            watchers[7].addChild(watchers[8]);
            watchers[1].updateParent(Assets);
            watchers[1].addChild(watchers[2]);
            watchers[4].updateParent(target);
            watchers[4].addChild(watchers[28]);
            watchers[10].updateParent(Assets);
            watchers[10].addChild(watchers[11]);
            watchers[27].updateParent(target);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[13]);
            watchers[12].addChild(watchers[14]);
            watchers[12].addChild(watchers[18]);
            watchers[15].updateParent(target);
            watchers[15].addChild(watchers[16]);
            watchers[17].parentWatcher = watchers[16];
            watchers[16].addChild(watchers[17]);
            watchers[20].updateParent(target);
            watchers[23].parentWatcher = watchers[20];
            watchers[20].addChild(watchers[23]);
            watchers[23].addChild(watchers[24]);
            watchers[21].parentWatcher = watchers[20];
            watchers[20].addChild(watchers[21]);
            watchers[21].addChild(watchers[22]);
            watchers[25].parentWatcher = watchers[20];
            watchers[20].addChild(watchers[25]);
            watchers[25].addChild(watchers[26]);
            watchers[0].updateParent(target);
            watchers[6].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            Weekday.watcherSetupUtil = new (_com_redbox_changetech_view_components_WeekdayWatcherSetupUtil);
        }

    }
}//package 
