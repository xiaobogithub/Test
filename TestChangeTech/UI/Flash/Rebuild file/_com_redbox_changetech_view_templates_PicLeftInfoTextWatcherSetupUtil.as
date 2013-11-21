//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_PicLeftInfoTextWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[7] = new PropertyWatcher("info", {propertyChange:true}, [bindings[21], bindings[3]], propertyGetter);
            watchers[31] = new PropertyWatcher("width", {widthChanged:true}, [bindings[21]], null);
            watchers[8] = new PropertyWatcher("height", {heightChanged:true}, [bindings[3]], null);
            watchers[9] = new PropertyWatcher("copyContainer", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[10] = new PropertyWatcher("height", {heightChanged:true}, [bindings[4]], null);
            watchers[11] = new PropertyWatcher("content", {propertyChange:true}, [bindings[17], bindings[15], bindings[8], bindings[11], bindings[18], bindings[10], bindings[7], bindings[12], bindings[5], bindings[22]], propertyGetter);
            watchers[17] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[10]], null);
            watchers[18] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[10]], null);
            watchers[13] = new FunctionReturnWatcher("getSecondaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[7]], null);
            watchers[14] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[7]], null);
            watchers[19] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[11]], null);
            watchers[20] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[11]], null);
            watchers[32] = new PropertyWatcher("PresenterImageUrl", {propertyChange:true}, [bindings[22]], null);
            watchers[23] = new FunctionReturnWatcher("getTertiaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[15]], null);
            watchers[24] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[15]], null);
            watchers[28] = new FunctionReturnWatcher("getTertiaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[18]], null);
            watchers[29] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[18]], null);
            watchers[25] = new PropertyWatcher("cta_btn", {propertyChange:true}, [bindings[16]], propertyGetter);
            watchers[26] = new PropertyWatcher("height", {heightChanged:true}, [bindings[16]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[19], bindings[21], bindings[1], bindings[20], bindings[3], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[19], bindings[21], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[1], bindings[20], bindings[3]], null);
            watchers[16] = new PropertyWatcher("ButtonAction", null, [bindings[9]], propertyGetter);
            watchers[3] = new PropertyWatcher("contentImage", {propertyChange:true}, [bindings[2], bindings[21]], propertyGetter);
            watchers[5] = new PropertyWatcher("width", {widthChanged:true}, [bindings[2], bindings[21]], null);
            watchers[4] = new PropertyWatcher("x", {xChanged:true}, [bindings[2]], null);
            watchers[12] = new PropertyWatcher("module", {propertyChange:true}, [bindings[6], bindings[14]], propertyGetter);
            watchers[22] = new PropertyWatcher("mandatoryQuestionsComplete", {propertyChange:true}, [bindings[14]], null);
            watchers[7].updateParent(target);
            watchers[7].addChild(watchers[31]);
            watchers[7].addChild(watchers[8]);
            watchers[9].updateParent(target);
            watchers[9].addChild(watchers[10]);
            watchers[11].updateParent(target);
            watchers[17].parentWatcher = watchers[11];
            watchers[11].addChild(watchers[17]);
            watchers[17].addChild(watchers[18]);
            watchers[13].parentWatcher = watchers[11];
            watchers[11].addChild(watchers[13]);
            watchers[13].addChild(watchers[14]);
            watchers[19].parentWatcher = watchers[11];
            watchers[11].addChild(watchers[19]);
            watchers[19].addChild(watchers[20]);
            watchers[11].addChild(watchers[32]);
            watchers[23].parentWatcher = watchers[11];
            watchers[11].addChild(watchers[23]);
            watchers[23].addChild(watchers[24]);
            watchers[28].parentWatcher = watchers[11];
            watchers[11].addChild(watchers[28]);
            watchers[28].addChild(watchers[29]);
            watchers[25].updateParent(target);
            watchers[25].addChild(watchers[26]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[16].updateParent(target);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[5]);
            watchers[3].addChild(watchers[4]);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[22]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PicLeftInfoText.watcherSetupUtil = new (_com_redbox_changetech_view_templates_PicLeftInfoTextWatcherSetupUtil);
        }

    }
}//package 
