//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.binding.utils {

    public class BindingUtils {

        mx_internal static const VERSION:String = "3.2.0.3958";

        public static function bindProperty(_arg1:Object, _arg2:String, _arg3:Object, _arg4:Object, _arg5:Boolean=false):ChangeWatcher{
            var w:* = null;
            var assign:* = null;
            var site:* = _arg1;
            var prop:* = _arg2;
            var host:* = _arg3;
            var chain:* = _arg4;
            var commitOnly:Boolean = _arg5;
            w = ChangeWatcher.watch(host, chain, null, commitOnly);
            if (w != null){
                assign = function (_arg1):void{
                    site[prop] = w.getValue();
                };
                w.setHandler(assign);
                assign(null);
            };
            return (w);
        }
        public static function bindSetter(_arg1:Function, _arg2:Object, _arg3:Object, _arg4:Boolean=false):ChangeWatcher{
            var w:* = null;
            var invoke:* = null;
            var setter:* = _arg1;
            var host:* = _arg2;
            var chain:* = _arg3;
            var commitOnly:Boolean = _arg4;
            w = ChangeWatcher.watch(host, chain, null, commitOnly);
            if (w != null){
                invoke = function (_arg1):void{
                    setter(w.getValue());
                };
                w.setHandler(invoke);
                invoke(null);
            };
            return (w);
        }

    }
}//package mx.binding.utils 
