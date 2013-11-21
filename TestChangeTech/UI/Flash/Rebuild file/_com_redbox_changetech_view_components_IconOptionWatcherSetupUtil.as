//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import assets.*;

    public class _com_redbox_changetech_view_components_IconOptionWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[4] = new PropertyWatcher("_selected", {propertyChange:true}, [bindings[15]], propertyGetter);
            watchers[1] = new PropertyWatcher("info_icon", {propertyChange:true}, [bindings[2], bindings[21], bindings[11], bindings[1], bindings[12]], propertyGetter);
            watchers[2] = new PropertyWatcher("height", {heightChanged:true}, [bindings[2], bindings[12]], null);
            watchers[8] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[18]], null);
            watchers[9] = new PropertyWatcher("info_icon", {propertyChange:true}, [bindings[18]], null);
            watchers[3] = new PropertyWatcher("valueLabel", {propertyChange:true}, [bindings[13], bindings[6], bindings[14], bindings[7]], propertyGetter);
            watchers[0] = new PropertyWatcher("description", {propertyChange:true}, [bindings[4], bindings[8], bindings[3], bindings[10], bindings[0]], propertyGetter);
            watchers[7] = new PropertyWatcher("iconPath", {propertyChange:true}, [bindings[17]], propertyGetter);
            watchers[5] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[16]], null);
            watchers[6] = new PropertyWatcher("tick", {propertyChange:true}, [bindings[16]], null);
            watchers[10] = new PropertyWatcher("_option", {propertyChange:true}, [bindings[19], bindings[20]], propertyGetter);
            watchers[12] = new PropertyWatcher("Description", {propertyChange:true}, [bindings[20]], null);
            watchers[11] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[19]], null);
            watchers[4].updateParent(target);
            watchers[1].updateParent(target);
            watchers[1].addChild(watchers[2]);
            watchers[8].updateParent(Assets);
            watchers[8].addChild(watchers[9]);
            watchers[3].updateParent(target);
            watchers[0].updateParent(target);
            watchers[7].updateParent(target);
            watchers[5].updateParent(Assets);
            watchers[5].addChild(watchers[6]);
            watchers[10].updateParent(target);
            watchers[10].addChild(watchers[12]);
            watchers[10].addChild(watchers[11]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            IconOption.watcherSetupUtil = new (_com_redbox_changetech_view_components_IconOptionWatcherSetupUtil);
        }

    }
}//package 
