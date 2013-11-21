//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_PersonalValuesWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[14] = new PropertyWatcher("grid", {propertyChange:true}, [bindings[15], bindings[14]], propertyGetter);
            watchers[16] = new PropertyWatcher("width", {widthChanged:true}, [bindings[14]], null);
            watchers[18] = new PropertyWatcher("height", {heightChanged:true}, [bindings[15]], null);
            watchers[17] = new PropertyWatcher("y", {yChanged:true}, [bindings[15]], null);
            watchers[15] = new PropertyWatcher("x", {xChanged:true}, [bindings[14]], null);
            watchers[21] = new PropertyWatcher("content", {propertyChange:true}, [bindings[17], bindings[19], bindings[16], bindings[18]], propertyGetter);
            watchers[24] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[17]], null);
            watchers[25] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[17]], null);
            watchers[22] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[16]], null);
            watchers[23] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[16]], null);
            watchers[19] = new PropertyWatcher("cta_btn", {propertyChange:true}, [bindings[15]], propertyGetter);
            watchers[20] = new PropertyWatcher("height", {heightChanged:true}, [bindings[15]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[13], bindings[1], bindings[12], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[12], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[13], bindings[1]], null);
            watchers[3] = new PropertyWatcher("options", {propertyChange:true}, [bindings[2], bindings[4], bindings[9], bindings[8], bindings[11], bindings[6], bindings[3], bindings[10], bindings[7], bindings[5]], propertyGetter);
            watchers[13] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[11]], null);
            watchers[4] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([0]);
            }, {collectionChange:true}, [bindings[2]], null);
            watchers[9] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([5]);
            }, {collectionChange:true}, [bindings[7]], null);
            watchers[8] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([4]);
            }, {collectionChange:true}, [bindings[6]], null);
            watchers[11] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([7]);
            }, {collectionChange:true}, [bindings[9]], null);
            watchers[6] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([2]);
            }, {collectionChange:true}, [bindings[4]], null);
            watchers[10] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([6]);
            }, {collectionChange:true}, [bindings[8]], null);
            watchers[7] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([3]);
            }, {collectionChange:true}, [bindings[5]], null);
            watchers[12] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([8]);
            }, {collectionChange:true}, [bindings[10]], null);
            watchers[5] = new FunctionReturnWatcher("getItemAt", target, function ():Array{
                return ([1]);
            }, {collectionChange:true}, [bindings[3]], null);
            watchers[14].updateParent(target);
            watchers[14].addChild(watchers[16]);
            watchers[14].addChild(watchers[18]);
            watchers[14].addChild(watchers[17]);
            watchers[14].addChild(watchers[15]);
            watchers[21].updateParent(target);
            watchers[24].parentWatcher = watchers[21];
            watchers[21].addChild(watchers[24]);
            watchers[24].addChild(watchers[25]);
            watchers[22].parentWatcher = watchers[21];
            watchers[21].addChild(watchers[22]);
            watchers[22].addChild(watchers[23]);
            watchers[19].updateParent(target);
            watchers[19].addChild(watchers[20]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[3].updateParent(target);
            watchers[13].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[13]);
            watchers[4].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[4]);
            watchers[9].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[9]);
            watchers[8].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[8]);
            watchers[11].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[11]);
            watchers[6].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[6]);
            watchers[10].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[10]);
            watchers[7].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[7]);
            watchers[12].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[12]);
            watchers[5].parentWatcher = watchers[3];
            watchers[3].addChild(watchers[5]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PersonalValues.watcherSetupUtil = new (_com_redbox_changetech_view_templates_PersonalValuesWatcherSetupUtil);
        }

    }
}//package 
