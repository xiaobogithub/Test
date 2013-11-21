//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_PersonalPlanWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[3] = new PropertyWatcher("_consumptionValues", {propertyChange:true}, [bindings[2], bindings[4], bindings[8], bindings[6], bindings[14], bindings[10], bindings[12]], propertyGetter);
            watchers[17] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([6]);
            }, {collectionChange:true}, [bindings[14]], null);
            watchers[15] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([5]);
            }, {collectionChange:true}, [bindings[12]], null);
            watchers[13] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([4]);
            }, {collectionChange:true}, [bindings[10]], null);
            watchers[4] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[2]], null);
            watchers[9] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[6]], null);
            watchers[11] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[8]], null);
            watchers[7] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[4]], null);
            watchers[25] = new PropertyWatcher("content", {propertyChange:true}, [bindings[23], bindings[21], bindings[22]], propertyGetter);
            watchers[28] = new PropertyWatcher("TextLayout", {propertyChange:true}, [bindings[22]], null);
            watchers[29] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[23]], null);
            watchers[30] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[23]], null);
            watchers[26] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[21]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[17], bindings[16], bindings[1], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[16], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[17], bindings[1]], null);
            watchers[23] = new PropertyWatcher("infoContainer", {propertyChange:true}, [bindings[20]], propertyGetter);
            watchers[24] = new PropertyWatcher("height", {heightChanged:true}, [bindings[20]], null);
            watchers[5] = new PropertyWatcher("_planValues", {propertyChange:true}, [bindings[15], bindings[13], bindings[9], bindings[11], bindings[3], bindings[7], bindings[5]], propertyGetter);
            watchers[8] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[5]], null);
            watchers[6] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[3]], null);
            watchers[16] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([5]);
            }, {collectionChange:true}, [bindings[13]], null);
            watchers[18] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([6]);
            }, {collectionChange:true}, [bindings[15]], null);
            watchers[14] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([4]);
            }, {collectionChange:true}, [bindings[11]], null);
            watchers[10] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[7]], null);
            watchers[12] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[9]], null);
            watchers[21] = new PropertyWatcher("bottomRow", {propertyChange:true}, [bindings[19]], propertyGetter);
            watchers[22] = new PropertyWatcher("y", {yChanged:true}, [bindings[19]], null);
            watchers[19] = new PropertyWatcher("sunday", {propertyChange:true}, [bindings[18]], propertyGetter);
            watchers[20] = new PropertyWatcher("x", {xChanged:true}, [bindings[18]], null);
            watchers[3].updateParent(target);
            watchers[17].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[17]);
            watchers[15].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[15]);
            watchers[13].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[13]);
            watchers[4].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[4]);
            watchers[9].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[9]);
            watchers[11].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[11]);
            watchers[7].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[7]);
            watchers[25].updateParent(target);
            watchers[25].addChild(watchers[28]);
            watchers[29].parentWatcher = watchers[25];
            watchers[25].addChild(watchers[29]);
            watchers[29].addChild(watchers[30]);
            watchers[25].addChild(watchers[26]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[23].updateParent(target);
            watchers[23].addChild(watchers[24]);
            watchers[5].updateParent(target);
            watchers[8].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[8]);
            watchers[6].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[6]);
            watchers[16].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[16]);
            watchers[18].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[18]);
            watchers[14].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[14]);
            watchers[10].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[10]);
            watchers[12].parentWatcher = watchers[5];
            watchers[5].addChild(watchers[12]);
            watchers[21].updateParent(target);
            watchers[21].addChild(watchers[22]);
            watchers[19].updateParent(target);
            watchers[19].addChild(watchers[20]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PersonalPlan.watcherSetupUtil = new (_com_redbox_changetech_view_templates_PersonalPlanWatcherSetupUtil);
        }

    }
}//package 
