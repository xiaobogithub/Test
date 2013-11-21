//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_CueReactivityPromptWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[5] = new PropertyWatcher("displayNumberCanvas", {propertyChange:true}, [_arg3[4]], _arg2);
            _arg4[6] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[4]], null);
            _arg4[7] = new PropertyWatcher("displayNumberText", {propertyChange:true}, [_arg3[5]], _arg2);
            _arg4[9] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[5]], null);
            _arg4[8] = new PropertyWatcher("y", {yChanged:true}, [_arg3[5]], null);
            _arg4[4] = new PropertyWatcher("currentDisplayNum", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[14] = new PropertyWatcher("_presenterImageUrl", {propertyChange:true}, [_arg3[11]], _arg2);
            _arg4[15] = new PropertyWatcher("content", {propertyChange:true}, [_arg3[12]], _arg2);
            _arg4[16] = new PropertyWatcher("Title", {propertyChange:true}, [_arg3[12]], null);
            _arg4[1] = new PropertyWatcher("model", {propertyChange:true}, [_arg3[2], _arg3[9], _arg3[8], _arg3[1], _arg3[10], _arg3[7]], _arg2);
            _arg4[2] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [_arg3[9], _arg3[1], _arg3[7]], null);
            _arg4[3] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [_arg3[2], _arg3[8], _arg3[10]], null);
            _arg4[11] = new PropertyWatcher("contentImage", {propertyChange:true}, [_arg3[9], _arg3[10]], _arg2);
            _arg4[12] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[9]], null);
            _arg4[13] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[10]], null);
            _arg4[5].updateParent(_arg1);
            _arg4[5].addChild(_arg4[6]);
            _arg4[7].updateParent(_arg1);
            _arg4[7].addChild(_arg4[9]);
            _arg4[7].addChild(_arg4[8]);
            _arg4[4].updateParent(_arg1);
            _arg4[14].updateParent(_arg1);
            _arg4[15].updateParent(_arg1);
            _arg4[15].addChild(_arg4[16]);
            _arg4[1].updateParent(_arg1);
            _arg4[1].addChild(_arg4[2]);
            _arg4[1].addChild(_arg4[3]);
            _arg4[11].updateParent(_arg1);
            _arg4[11].addChild(_arg4[12]);
            _arg4[11].addChild(_arg4[13]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            CueReactivityPrompt.watcherSetupUtil = new (_com_redbox_changetech_view_templates_CueReactivityPromptWatcherSetupUtil);
        }

    }
}//package 
