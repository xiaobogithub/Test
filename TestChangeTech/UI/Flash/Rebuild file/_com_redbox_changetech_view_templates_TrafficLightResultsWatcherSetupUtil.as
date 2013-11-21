//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.templates.*;

    public class _com_redbox_changetech_view_templates_TrafficLightResultsWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[21] = new PropertyWatcher("greenPointString", {propertyChange:true}, [_arg3[18]], _arg2);
            _arg4[7] = new PropertyWatcher("contentContainer", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[8] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[3]], null);
            _arg4[14] = new PropertyWatcher("content", {propertyChange:true}, [_arg3[8]], _arg2);
            _arg4[4] = new PropertyWatcher("info", {propertyChange:true}, [_arg3[2]], _arg2);
            _arg4[5] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[2]], null);
            _arg4[19] = new PropertyWatcher("redPointString", {propertyChange:true}, [_arg3[16]], _arg2);
            _arg4[16] = new PropertyWatcher("score", {propertyChange:true}, [_arg3[12]], _arg2);
            _arg4[17] = new PropertyWatcher("trafficlightImage", {propertyChange:true}, [_arg3[15]], _arg2);
            _arg4[18] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[15]], null);
            _arg4[9] = new PropertyWatcher("copyContainer", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[10] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[3]], null);
            _arg4[0] = new PropertyWatcher("model", {propertyChange:true}, [_arg3[13], _arg3[4], _arg3[1], _arg3[14], _arg3[5], _arg3[0]], _arg2);
            _arg4[1] = new PropertyWatcher("currentStageWidth", {propertyChange:true}, [_arg3[13], _arg3[0]], null);
            _arg4[11] = new PropertyWatcher("languageVO", {propertyChange:true}, [_arg3[4], _arg3[5]], null);
            _arg4[2] = new PropertyWatcher("currentStageHeight", {propertyChange:true}, [_arg3[1], _arg3[14]], null);
            _arg4[20] = new PropertyWatcher("yellowPointString", {propertyChange:true}, [_arg3[17]], _arg2);
            _arg4[6] = new PropertyWatcher("forwardEnabled", {propertyChange:true}, [_arg3[11], _arg3[6], _arg3[3], _arg3[10], _arg3[7]], _arg2);
            _arg4[15] = new PropertyWatcher("module", {propertyChange:true}, [_arg3[9]], _arg2);
            _arg4[21].updateParent(_arg1);
            _arg4[7].updateParent(_arg1);
            _arg4[7].addChild(_arg4[8]);
            _arg4[14].updateParent(_arg1);
            _arg4[4].updateParent(_arg1);
            _arg4[4].addChild(_arg4[5]);
            _arg4[19].updateParent(_arg1);
            _arg4[16].updateParent(_arg1);
            _arg4[17].updateParent(_arg1);
            _arg4[17].addChild(_arg4[18]);
            _arg4[9].updateParent(_arg1);
            _arg4[9].addChild(_arg4[10]);
            _arg4[0].updateParent(_arg1);
            _arg4[0].addChild(_arg4[1]);
            _arg4[0].addChild(_arg4[11]);
            _arg4[0].addChild(_arg4[2]);
            _arg4[20].updateParent(_arg1);
            _arg4[6].updateParent(_arg1);
            _arg4[15].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            TrafficLightResults.watcherSetupUtil = new (_com_redbox_changetech_view_templates_TrafficLightResultsWatcherSetupUtil);
        }

    }
}//package 
