//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_CompletionGraphWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[30] = new PropertyWatcher("info", {propertyChange:true}, [bindings[21]], propertyGetter);
            watchers[31] = new PropertyWatcher("width", {widthChanged:true}, [bindings[21]], null);
            watchers[9] = new PropertyWatcher("screeningScore", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[6] = new PropertyWatcher("content", {propertyChange:true}, [bindings[17], bindings[15], bindings[4], bindings[8], bindings[11], bindings[18], bindings[3], bindings[10], bindings[7], bindings[12], bindings[22]], propertyGetter);
            watchers[17] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[11]], null);
            watchers[18] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[11]], null);
            watchers[15] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[10]], null);
            watchers[16] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[10]], null);
            watchers[32] = new PropertyWatcher("PresenterImageUrl", {propertyChange:true}, [bindings[22]], null);
            watchers[11] = new FunctionReturnWatcher("getSecondaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[7]], null);
            watchers[12] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[7]], null);
            watchers[7] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[3]], null);
            watchers[27] = new FunctionReturnWatcher("getTertiaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[18]], null);
            watchers[28] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[18]], null);
            watchers[22] = new FunctionReturnWatcher("getTertiaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[15]], null);
            watchers[23] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[15]], null);
            watchers[24] = new PropertyWatcher("cta_btn", {propertyChange:true}, [bindings[16]], propertyGetter);
            watchers[25] = new PropertyWatcher("height", {heightChanged:true}, [bindings[16]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[19], bindings[21], bindings[1], bindings[20], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[19], bindings[21], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[1], bindings[20]], null);
            watchers[14] = new PropertyWatcher("ButtonAction", null, [bindings[9]], propertyGetter);
            watchers[10] = new PropertyWatcher("completionScore", {propertyChange:true}, [bindings[6]], propertyGetter);
            watchers[3] = new PropertyWatcher("contentImage", {propertyChange:true}, [bindings[2], bindings[21]], propertyGetter);
            watchers[5] = new PropertyWatcher("width", {widthChanged:true}, [bindings[2], bindings[21]], null);
            watchers[4] = new PropertyWatcher("x", {xChanged:true}, [bindings[2]], null);
            watchers[20] = new PropertyWatcher("module", {propertyChange:true}, [bindings[14]], propertyGetter);
            watchers[21] = new PropertyWatcher("mandatoryQuestionsComplete", {propertyChange:true}, [bindings[14]], null);
            watchers[30].updateParent(target);
            watchers[30].addChild(watchers[31]);
            watchers[9].updateParent(target);
            watchers[6].updateParent(target);
            watchers[17].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[17]);
            watchers[17].addChild(watchers[18]);
            watchers[15].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[15]);
            watchers[15].addChild(watchers[16]);
            watchers[6].addChild(watchers[32]);
            watchers[11].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[11]);
            watchers[11].addChild(watchers[12]);
            watchers[6].addChild(watchers[7]);
            watchers[27].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[27]);
            watchers[27].addChild(watchers[28]);
            watchers[22].parentWatcher = watchers[6];
            watchers[6].addChild(watchers[22]);
            watchers[22].addChild(watchers[23]);
            watchers[24].updateParent(target);
            watchers[24].addChild(watchers[25]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[14].updateParent(target);
            watchers[10].updateParent(target);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[5]);
            watchers[3].addChild(watchers[4]);
            watchers[20].updateParent(target);
            watchers[20].addChild(watchers[21]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            CompletionGraph.watcherSetupUtil = new (_com_redbox_changetech_view_templates_CompletionGraphWatcherSetupUtil);
        }

    }
}//package 
