//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import com.redbox.changetech.util.Enumerations.*;

    public class _com_redbox_changetech_view_components_WeekdayDataEntryWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[19] = new PropertyWatcher("Text", {propertyChange:true}, [bindings[9], bindings[8]], null);
            watchers[23] = new PropertyWatcher("spirits_icon_path", {propertyChange:true}, [bindings[11]], propertyGetter);
            watchers[9] = new PropertyWatcher("beer_icon_path", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[16] = new PropertyWatcher("wine_icon_path", {propertyChange:true}, [bindings[7]], propertyGetter);
            watchers[12] = new PropertyWatcher("Text", {propertyChange:true}, [bindings[4], bindings[5]], null);
            watchers[4] = new PropertyWatcher("Text", {propertyChange:true}, [bindings[1], bindings[0]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[4], bindings[8], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[4], bindings[8], bindings[0]], null);
            watchers[17] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return ([DrinkType.Spirit.Text]);
            }, null, [bindings[8]], null);
            watchers[2] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return ([DrinkType.Beer.Text]);
            }, null, [bindings[0]], null);
            watchers[10] = new FunctionReturnWatcher("getLang", target, function ():Array{
                return ([DrinkType.Wine.Text]);
            }, null, [bindings[4]], null);
            watchers[6] = new PropertyWatcher("_reportedValues", {propertyChange:true}, [bindings[2], bindings[6], bindings[10]], propertyGetter);
            watchers[21] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[10]], null);
            watchers[22] = new PropertyWatcher("Value", null, [bindings[10]], null);
            watchers[14] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[6]], null);
            watchers[15] = new PropertyWatcher("Value", null, [bindings[6]], null);
            watchers[7] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[2]], null);
            watchers[8] = new PropertyWatcher("Value", null, [bindings[2]], null);
            watchers[19].updateParent(DrinkType.Spirit);
            watchers[23].updateParent(target);
            watchers[9].updateParent(target);
            watchers[16].updateParent(target);
            watchers[12].updateParent(DrinkType.Wine);
            watchers[4].updateParent(DrinkType.Beer);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[17].parentWatcher = watchers[1];
            watchers[1].addChild(watchers[17]);
            watchers[2].parentWatcher = watchers[1];
            watchers[1].addChild(watchers[2]);
            watchers[10].parentWatcher = watchers[1];
            watchers[1].addChild(watchers[10]);
            watchers[6].updateParent(target);
            watchers[21].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[21]);
            watchers[21].addChild(watchers[22]);
            watchers[14].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[14]);
            watchers[14].addChild(watchers[15]);
            watchers[7].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[7]);
            watchers[7].addChild(watchers[8]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            WeekdayDataEntry.watcherSetupUtil = new (_com_redbox_changetech_view_components_WeekdayDataEntryWatcherSetupUtil);
        }

    }
}//package 
