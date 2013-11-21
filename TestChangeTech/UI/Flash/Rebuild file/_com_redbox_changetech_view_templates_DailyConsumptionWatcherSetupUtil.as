//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_DailyConsumptionWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[12] = new PropertyWatcher("grid", {propertyChange:true}, [bindings[15], bindings[12]], propertyGetter);
            watchers[13] = new PropertyWatcher("width", {widthChanged:true}, [bindings[15], bindings[12]], null);
            watchers[14] = new PropertyWatcher("x", {xChanged:true}, [bindings[15]], null);
            watchers[7] = new PropertyWatcher("_consumptionValues", {propertyChange:true}, [bindings[9], bindings[8], bindings[11], bindings[10]], propertyGetter);
            watchers[9] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([4]);
            }, {collectionChange:true}, [bindings[9]], null);
            watchers[8] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[8]], null);
            watchers[11] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([6]);
            }, {collectionChange:true}, [bindings[11]], null);
            watchers[10] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([5]);
            }, {collectionChange:true}, [bindings[10]], null);
            watchers[0] = new PropertyWatcher("questions", {propertyChange:true}, [bindings[1], bindings[0]], propertyGetter);
            watchers[17] = new PropertyWatcher("weekdayGrid", {propertyChange:true}, [bindings[17]], propertyGetter);
            watchers[18] = new PropertyWatcher("y", {yChanged:true}, [bindings[17]], null);
            watchers[2] = new PropertyWatcher("dataEntryContainer", {propertyChange:true}, [bindings[4], bindings[18]], propertyGetter);
            watchers[19] = new PropertyWatcher("height", {heightChanged:true}, [bindings[18]], null);
            watchers[1] = new PropertyWatcher("dataEntry", {propertyChange:true}, [bindings[2], bindings[3]], propertyGetter);
            watchers[20] = new PropertyWatcher("content", {propertyChange:true}, [bindings[19], bindings[21], bindings[20]], propertyGetter);
            watchers[23] = new PropertyWatcher("TextLayout", {propertyChange:true}, [bindings[20]], null);
            watchers[21] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[19]], null);
            watchers[24] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[21]], null);
            watchers[25] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[21]], null);
            watchers[4] = new PropertyWatcher("model", {propertyChange:true}, [bindings[13], bindings[6], bindings[14], bindings[7]], propertyGetter);
            watchers[5] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[13], bindings[6]], null);
            watchers[6] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[14], bindings[7]], null);
            watchers[15] = new PropertyWatcher("leftsideContainer", {propertyChange:true}, [bindings[16]], propertyGetter);
            watchers[16] = new PropertyWatcher("y", {yChanged:true}, [bindings[16]], null);
            watchers[3] = new PropertyWatcher("copy1_str", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[13]);
            watchers[12].addChild(watchers[14]);
            watchers[7].updateParent(target);
            watchers[9].parentWatcher = watchers[7];
            watchers[7].addChild(watchers[9]);
            watchers[8].parentWatcher = watchers[7];
            watchers[7].addChild(watchers[8]);
            watchers[11].parentWatcher = watchers[7];
            watchers[7].addChild(watchers[11]);
            watchers[10].parentWatcher = watchers[7];
            watchers[7].addChild(watchers[10]);
            watchers[0].updateParent(target);
            watchers[17].updateParent(target);
            watchers[17].addChild(watchers[18]);
            watchers[2].updateParent(target);
            watchers[2].addChild(watchers[19]);
            watchers[1].updateParent(target);
            watchers[20].updateParent(target);
            watchers[20].addChild(watchers[23]);
            watchers[20].addChild(watchers[21]);
            watchers[24].parentWatcher = watchers[20];
            watchers[20].addChild(watchers[24]);
            watchers[24].addChild(watchers[25]);
            watchers[4].updateParent(target);
            watchers[4].addChild(watchers[5]);
            watchers[4].addChild(watchers[6]);
            watchers[15].updateParent(target);
            watchers[15].addChild(watchers[16]);
            watchers[3].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            DailyConsumption.watcherSetupUtil = new (_com_redbox_changetech_view_templates_DailyConsumptionWatcherSetupUtil);
        }

    }
}//package 
