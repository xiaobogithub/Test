//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_ProgressMonitorWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[3] = new PropertyWatcher("isEditMode", {propertyChange:true}, [bindings[4], bindings[9], bindings[8], bindings[6], bindings[18], bindings[3], bindings[7], bindings[5]], propertyGetter);
            watchers[12] = new PropertyWatcher("content", {propertyChange:true}, [bindings[15], bindings[19], bindings[16]], propertyGetter);
            watchers[15] = new PropertyWatcher("TextLayout", {propertyChange:true}, [bindings[16]], null);
            watchers[18] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[19]], null);
            watchers[19] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[19]], null);
            watchers[13] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[15]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[17], bindings[2], bindings[11], bindings[1], bindings[10], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[2], bindings[10], bindings[0]], null);
            watchers[16] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[17]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[11], bindings[1]], null);
            watchers[10] = new PropertyWatcher("infoContainer", {propertyChange:true}, [bindings[14]], propertyGetter);
            watchers[11] = new PropertyWatcher("height", {heightChanged:true}, [bindings[14]], null);
            watchers[6] = new PropertyWatcher("bottomRow", {propertyChange:true}, [bindings[13], bindings[12]], propertyGetter);
            watchers[8] = new PropertyWatcher("width", {widthChanged:true}, [bindings[12]], null);
            watchers[9] = new PropertyWatcher("y", {yChanged:true}, [bindings[13]], null);
            watchers[7] = new PropertyWatcher("x", {xChanged:true}, [bindings[12]], null);
            watchers[4] = new PropertyWatcher("leftsideContainer", {propertyChange:true}, [bindings[12]], propertyGetter);
            watchers[5] = new PropertyWatcher("x", {xChanged:true}, [bindings[12]], null);
            watchers[3].updateParent(target);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[15]);
            watchers[18].parentWatcher = watchers[12];
            watchers[12].addChild(watchers[18]);
            watchers[18].addChild(watchers[19]);
            watchers[12].addChild(watchers[13]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[16]);
            watchers[0].addChild(watchers[2]);
            watchers[10].updateParent(target);
            watchers[10].addChild(watchers[11]);
            watchers[6].updateParent(target);
            watchers[6].addChild(watchers[8]);
            watchers[6].addChild(watchers[9]);
            watchers[6].addChild(watchers[7]);
            watchers[4].updateParent(target);
            watchers[4].addChild(watchers[5]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            ProgressMonitor.watcherSetupUtil = new (_com_redbox_changetech_view_templates_ProgressMonitorWatcherSetupUtil);
        }

    }
}//package 
