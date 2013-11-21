//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_PlanWeekdayWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[16] = new PropertyWatcher("plan", {propertyChange:true}, [_arg3[8], _arg3[11]], _arg2);
            _arg4[17] = new PropertyWatcher("PlanValue", {propertyChange:true}, [_arg3[8], _arg3[11]], null);
            _arg4[1] = new PropertyWatcher("defaultGlow", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[19] = new PropertyWatcher("isEditMode", {propertyChange:true}, [_arg3[9], _arg3[10], _arg3[12]], _arg2);
            _arg4[5] = new PropertyWatcher("consumption", {propertyChange:true}, [_arg3[4], _arg3[7], _arg3[5]], _arg2);
            _arg4[6] = new PropertyWatcher("DayOfWeek", {propertyChange:true}, [_arg3[4]], null);
            _arg4[8] = new PropertyWatcher("ConsumptionDate", {propertyChange:true}, [_arg3[5]], null);
            _arg4[2] = new PropertyWatcher("model", {propertyChange:true}, [_arg3[4], _arg3[8], _arg3[6], _arg3[7], _arg3[5]], _arg2);
            _arg4[3] = new PropertyWatcher("languageVO", {propertyChange:true}, [_arg3[4], _arg3[8], _arg3[6], _arg3[7], _arg3[5]], null);
            _arg4[10] = new PropertyWatcher("goalTotal", {propertyChange:true}, [_arg3[6]], _arg2);
            _arg4[0] = new PropertyWatcher("selectedGlow", {propertyChange:true}, [_arg3[1]], _arg2);
            _arg4[16].updateParent(_arg1);
            _arg4[16].addChild(_arg4[17]);
            _arg4[1].updateParent(_arg1);
            _arg4[19].updateParent(_arg1);
            _arg4[5].updateParent(_arg1);
            _arg4[5].addChild(_arg4[6]);
            _arg4[5].addChild(_arg4[8]);
            _arg4[2].updateParent(_arg1);
            _arg4[2].addChild(_arg4[3]);
            _arg4[10].updateParent(_arg1);
            _arg4[0].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            PlanWeekday.watcherSetupUtil = new (_com_redbox_changetech_view_components_PlanWeekdayWatcherSetupUtil);
        }

    }
}//package 
