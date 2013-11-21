//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;
    import assets.*;

    public class _com_redbox_changetech_view_components_BalanceAudioPlayerWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[13] = new PropertyWatcher("mediaSoundLength", {propertyChange:true}, [bindings[8], bindings[7]], propertyGetter);
            watchers[8] = new PropertyWatcher("mediaTitle", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[4] = new PropertyWatcher("playing", {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[2] = new PropertyWatcher("roomVO", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[3] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[1]], null);
            watchers[6] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[2]], null);
            watchers[0] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[0]], null);
            watchers[1] = new PropertyWatcher("audioPlayerBorder", {propertyChange:true}, [bindings[0]], null);
            watchers[11] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[6]], null);
            watchers[12] = new PropertyWatcher("playIcon", {propertyChange:true}, [bindings[6]], null);
            watchers[9] = new FunctionReturnWatcher("getInstance", target, function ():Array{
                return ([]);
            }, null, [bindings[4]], null);
            watchers[10] = new PropertyWatcher("audioPlayerTrack", {propertyChange:true}, [bindings[4]], null);
            watchers[14] = new PropertyWatcher("loadPercent", {propertyChange:true}, [bindings[9]], propertyGetter);
            watchers[13].updateParent(target);
            watchers[8].updateParent(target);
            watchers[4].updateParent(target);
            watchers[2].updateParent(target);
            watchers[2].addChild(watchers[3]);
            watchers[6].updateParent(propertyGetter.apply(target, ["model"]));
            watchers[0].updateParent(Assets);
            watchers[0].addChild(watchers[1]);
            watchers[11].updateParent(Assets);
            watchers[11].addChild(watchers[12]);
            watchers[9].updateParent(Assets);
            watchers[9].addChild(watchers[10]);
            watchers[14].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceAudioPlayer.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceAudioPlayerWatcherSetupUtil);
        }

    }
}//package 
