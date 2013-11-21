//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_PicRightInfoTextWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[6] = new PropertyWatcher("info", {propertyChange:true}, [bindings[17], bindings[3]], propertyGetter);
            watchers[25] = new PropertyWatcher("width", {widthChanged:true}, [bindings[17]], null);
            watchers[7] = new PropertyWatcher("height", {heightChanged:true}, [bindings[3]], null);
            watchers[24] = new PropertyWatcher("x", {xChanged:true}, [bindings[17]], null);
            watchers[3] = new PropertyWatcher("transContainer1", {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[4] = new PropertyWatcher("width", {widthChanged:true}, [bindings[2]], null);
            watchers[8] = new PropertyWatcher("copyContainer", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[9] = new PropertyWatcher("height", {heightChanged:true}, [bindings[4]], null);
            watchers[10] = new PropertyWatcher("content", {propertyChange:true}, [bindings[9], bindings[8], bindings[11], bindings[18], bindings[14], bindings[10], bindings[7], bindings[12], bindings[5]], propertyGetter);
            watchers[15] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[9]], null);
            watchers[16] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[9]], null);
            watchers[19] = new FunctionReturnWatcher("getTertiaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[12]], null);
            watchers[20] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[12]], null);
            watchers[26] = new PropertyWatcher("PresenterImageUrl", {propertyChange:true}, [bindings[18]], null);
            watchers[12] = new FunctionReturnWatcher("getSecondaryButton", target, function ():Array{
                return ([]);
            }, null, [bindings[7]], null);
            watchers[13] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[7]], null);
            watchers[21] = new PropertyWatcher("cta_btn", {propertyChange:true}, [bindings[13]], propertyGetter);
            watchers[22] = new PropertyWatcher("height", {heightChanged:true}, [bindings[13]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[15], bindings[16], bindings[1], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[15], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[16], bindings[1]], null);
            watchers[11] = new PropertyWatcher("module", {propertyChange:true}, [bindings[6]], propertyGetter);
            watchers[6].updateParent(target);
            watchers[6].addChild(watchers[25]);
            watchers[6].addChild(watchers[7]);
            watchers[6].addChild(watchers[24]);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[4]);
            watchers[8].updateParent(target);
            watchers[8].addChild(watchers[9]);
            watchers[10].updateParent(target);
            watchers[15].parentWatcher = watchers[10];
            watchers[10].addChild(watchers[15]);
            watchers[15].addChild(watchers[16]);
            watchers[19].parentWatcher = watchers[10];
            watchers[10].addChild(watchers[19]);
            watchers[19].addChild(watchers[20]);
            watchers[10].addChild(watchers[26]);
            watchers[12].parentWatcher = watchers[10];
            watchers[10].addChild(watchers[12]);
            watchers[12].addChild(watchers[13]);
            watchers[21].updateParent(target);
            watchers[21].addChild(watchers[22]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
            watchers[11].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PicRightInfoText.watcherSetupUtil = new (_com_redbox_changetech_view_templates_PicRightInfoTextWatcherSetupUtil);
        }

    }
}//package 
