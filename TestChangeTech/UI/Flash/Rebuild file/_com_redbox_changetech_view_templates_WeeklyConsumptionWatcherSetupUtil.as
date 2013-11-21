//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_WeeklyConsumptionWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[13] = new PropertyWatcher("grid", {propertyChange:true}, [bindings[11]], propertyGetter);
            watchers[14] = new PropertyWatcher("x", {xChanged:true}, [bindings[11]], null);
            watchers[3] = new PropertyWatcher("_consumptionValues", {propertyChange:true}, [bindings[2], bindings[4], bindings[8], bindings[6], bindings[3], bindings[7], bindings[5]], propertyGetter);
            watchers[4] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[2]], null);
            watchers[9] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([5]);
            }, {collectionChange:true}, [bindings[7]], null);
            watchers[8] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([4]);
            }, {collectionChange:true}, [bindings[6]], null);
            watchers[6] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[4]], null);
            watchers[10] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([6]);
            }, {collectionChange:true}, [bindings[8]], null);
            watchers[7] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[5]], null);
            watchers[5] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[3]], null);
            watchers[17] = new PropertyWatcher("weekdayGrid", {propertyChange:true}, [bindings[12]], propertyGetter);
            watchers[18] = new PropertyWatcher("y", {yChanged:true}, [bindings[12]], null);
            watchers[15] = new PropertyWatcher("wednesday", {propertyChange:true}, [bindings[11]], propertyGetter);
            watchers[16] = new PropertyWatcher("x", {xChanged:true}, [bindings[11]], null);
            watchers[21] = new PropertyWatcher("dataEntryContainer", {propertyChange:true}, [bindings[13]], propertyGetter);
            watchers[22] = new PropertyWatcher("height", {heightChanged:true}, [bindings[13]], null);
            watchers[19] = new PropertyWatcher("row2", {propertyChange:true}, [bindings[12]], propertyGetter);
            watchers[20] = new PropertyWatcher("y", {yChanged:true}, [bindings[12]], null);
            watchers[23] = new PropertyWatcher("content", {propertyChange:true}, [bindings[15], bindings[16], bindings[14]], propertyGetter);
            watchers[26] = new PropertyWatcher("TextLayout", {propertyChange:true}, [bindings[15]], null);
            watchers[24] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[14]], null);
            watchers[27] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[16]], null);
            watchers[28] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[16]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[9], bindings[1], bindings[10], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[9], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[1], bindings[10]], null);
            watchers[11] = new PropertyWatcher("leftsideContainer", {propertyChange:true}, [bindings[11]], propertyGetter);
            watchers[12] = new PropertyWatcher("x", {xChanged:true}, [bindings[11]], null);
            watchers[13].updateParent(target);
            watchers[13].addChild(watchers[14]);
            watchers[3].updateParent(target);
            watchers[4].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[4]);
            watchers[9].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[9]);
            watchers[8].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[8]);
            watchers[6].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[6]);
            watchers[10].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[10]);
            watchers[7].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[7]);
            watchers[5].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[5]);
            watchers[17].updateParent(target);
            watchers[17].addChild(watchers[18]);
            watchers[15].updateParent(target);
            watchers[15].addChild(watchers[16]);
            watchers[21].updateParent(target);
            watchers[21].addChild(watchers[22]);
            watchers[19].updateParent(target);
            watchers[19].addChild(watchers[20]);
            watchers[23].updateParent(target);
            watchers[23].addChild(watchers[26]);
            watchers[23].addChild(watchers[24]);
            watchers[27].parentWatcher = watchers[23];
            watchers[23].addChild(watchers[27]);
            watchers[27].addChild(watchers[28]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[11].updateParent(target);
            watchers[11].addChild(watchers[12]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            WeeklyConsumption.watcherSetupUtil = new (_com_redbox_changetech_view_templates_WeeklyConsumptionWatcherSetupUtil);
        }

    }
}//package 
