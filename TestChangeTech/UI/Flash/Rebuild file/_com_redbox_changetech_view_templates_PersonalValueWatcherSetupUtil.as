//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_PersonalValueWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[4] = new PropertyWatcher("info", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[5] = new PropertyWatcher("height", {heightChanged:true}, [bindings[3]], null);
            watchers[8] = new PropertyWatcher("_content", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[6] = new PropertyWatcher("copyContainer", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[7] = new PropertyWatcher("height", {heightChanged:true}, [bindings[4]], null);
            watchers[9] = new PropertyWatcher("content", {propertyChange:true}, [bindings[9], bindings[8], bindings[6], bindings[7]], propertyGetter);
            watchers[10] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[6]], null);
            watchers[11] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[6]], null);
            watchers[12] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[7]], null);
            watchers[13] = new PropertyWatcher("ButtonAction", {propertyChange:true}, [bindings[7]], null);
            watchers[0] = new PropertyWatcher("model", {propertyChange:true}, [bindings[2], bindings[11], bindings[1], bindings[10], bindings[0]], propertyGetter);
            watchers[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [bindings[2], bindings[10], bindings[0]], null);
            watchers[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [bindings[11], bindings[1]], null);
            watchers[4].updateParent(target);
            watchers[4].addChild(watchers[5]);
            watchers[8].updateParent(target);
            watchers[6].updateParent(target);
            watchers[6].addChild(watchers[7]);
            watchers[9].updateParent(target);
            watchers[10].parentWatcher = watchers[9];
            watchers[9].addChild(watchers[10]);
            watchers[10].addChild(watchers[11]);
            watchers[12].parentWatcher = watchers[9];
            watchers[9].addChild(watchers[12]);
            watchers[12].addChild(watchers[13]);
            watchers[0].updateParent(target);
            watchers[0].addChild(watchers[1]);
            watchers[0].addChild(watchers[2]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PersonalValue.watcherSetupUtil = new (_com_redbox_changetech_view_templates_PersonalValueWatcherSetupUtil);
        }

    }
}//package 
