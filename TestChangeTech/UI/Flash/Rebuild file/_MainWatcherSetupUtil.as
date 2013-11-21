//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import assets.*;

    public class _MainWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[11] = new PropertyWatcher("footerX", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[9] = new PropertyWatcher("footer", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[10] = new PropertyWatcher("height", {heightChanged:true}, [bindings[4]], null);
            watchers[7] = new PropertyWatcher("appCanvas", {propertyChange:true}, [bindings[4]], propertyGetter);
            watchers[8] = new PropertyWatcher("height", {heightChanged:true}, [bindings[4]], null);
            watchers[2] = new PropertyWatcher("header", {propertyChange:true}, [bindings[2], bindings[1]], propertyGetter);
            watchers[3] = new PropertyWatcher("height", {heightChanged:true}, [bindings[2], bindings[1]], null);
            watchers[4] = new PropertyWatcher("model", {propertyChange:true}, [bindings[8], bindings[3], bindings[7]], propertyGetter);
            watchers[12] = new PropertyWatcher("currentContentCode", {propertyChange:true}, [bindings[7]], null);
            watchers[5] = new PropertyWatcher("showControls", {propertyChange:true}, [bindings[3]], null);
            watchers[13] = new PropertyWatcher("isDebugMode", {propertyChange:true}, [bindings[8]], null);
            watchers[0] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[0]], null);
            watchers[1] = new PropertyWatcher("balance_logo", {propertyChange:true}, [bindings[0]], null);
            watchers[11].updateParent(target);
            watchers[9].updateParent(target);
            watchers[9].addChild(watchers[10]);
            watchers[7].updateParent(target);
            watchers[7].addChild(watchers[8]);
            watchers[2].updateParent(target);
            watchers[2].addChild(watchers[3]);
            watchers[4].updateParent(target);
            watchers[4].addChild(watchers[12]);
            watchers[4].addChild(watchers[5]);
            watchers[4].addChild(watchers[13]);
            watchers[0].updateParent(Assets);
            watchers[0].addChild(watchers[1]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            Main.watcherSetupUtil = new (_MainWatcherSetupUtil);
        }

    }
}//package 
