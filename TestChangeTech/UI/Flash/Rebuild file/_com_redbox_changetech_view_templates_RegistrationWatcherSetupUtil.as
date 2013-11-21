//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_RegistrationWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[2] = new PropertyWatcher("confirmEmailInput", {propertyChange:true}, [bindings[2]], propertyGetter);
            watchers[1] = new PropertyWatcher("emailInput", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[8] = new PropertyWatcher("content", {propertyChange:true}, [bindings[13], bindings[11], bindings[6], bindings[5]], propertyGetter);
            watchers[11] = new PropertyWatcher("TextLayout", {propertyChange:true}, [bindings[6]], null);
            watchers[22] = new PropertyWatcher("PresenterImageUrl", {propertyChange:true}, [bindings[13]], null);
            watchers[18] = new FunctionReturnWatcher("getCTAButton", target, function ():Array{
                return ([]);
            }, null, [bindings[11]], null);
            watchers[19] = new PropertyWatcher("Label", {propertyChange:true}, [bindings[11]], null);
            watchers[9] = new PropertyWatcher("Title", {propertyChange:true}, [bindings[5]], null);
            watchers[12] = new PropertyWatcher("model", {propertyChange:true}, [bindings[9], bindings[8], bindings[7]], propertyGetter);
            watchers[13] = new PropertyWatcher("languageVO", {propertyChange:true}, [bindings[9], bindings[8], bindings[7]], null);
            watchers[3] = new PropertyWatcher("formContainer", {propertyChange:true}, [bindings[3], bindings[12]], propertyGetter);
            watchers[21] = new PropertyWatcher("width", {widthChanged:true}, [bindings[12]], null);
            watchers[4] = new PropertyWatcher("height", {heightChanged:true}, [bindings[3]], null);
            watchers[5] = new PropertyWatcher("contentImage", {propertyChange:true}, [bindings[4], bindings[12]], propertyGetter);
            watchers[7] = new PropertyWatcher("width", {widthChanged:true}, [bindings[4], bindings[12]], null);
            watchers[6] = new PropertyWatcher("x", {xChanged:true}, [bindings[4]], null);
            watchers[0] = new PropertyWatcher("firstNameInput", {propertyChange:true}, [bindings[0]], propertyGetter);
            watchers[17] = new PropertyWatcher("formIsValid", {propertyChange:true}, [bindings[10]], propertyGetter);
            watchers[2].updateParent(target);
            watchers[1].updateParent(target);
            watchers[8].updateParent(target);
            watchers[8].addChild(watchers[11]);
            watchers[8].addChild(watchers[22]);
            watchers[18].parentWatcher = watchers[8];
            watchers[8].addChild(watchers[18]);
            watchers[18].addChild(watchers[19]);
            watchers[8].addChild(watchers[9]);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[13]);
            watchers[3].updateParent(target);
            watchers[3].addChild(watchers[21]);
            watchers[3].addChild(watchers[4]);
            watchers[5].updateParent(target);
            watchers[5].addChild(watchers[7]);
            watchers[5].addChild(watchers[6]);
            watchers[0].updateParent(target);
            watchers[17].updateParent(target);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            Registration.watcherSetupUtil = new (_com_redbox_changetech_view_templates_RegistrationWatcherSetupUtil);
        }

    }
}//package 
