//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.questions.*;

    public class _com_redbox_changetech_view_components_questions_RadioButtonQuestionWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[2] = new PropertyWatcher("rep", {propertyChange:true}, [_arg3[2], _arg3[4], _arg3[1], _arg3[3], _arg3[5]], _arg2);
            _arg4[3] = new PropertyWatcher("dataProvider", {collectionChange:true}, [_arg3[2], _arg3[4], _arg3[1], _arg3[3], _arg3[5]], null);
            _arg4[4] = new RepeaterItemWatcher(_arg4[3]);
            _arg4[7] = new PropertyWatcher("ActionFlag", null, [_arg3[3]], null);
            _arg4[8] = new PropertyWatcher("Description", null, [_arg3[4]], null);
            _arg4[9] = new PropertyWatcher("Score", null, [_arg3[5]], null);
            _arg4[5] = new PropertyWatcher("Label", null, [_arg3[1]], null);
            _arg4[6] = new PropertyWatcher("Id", null, [_arg3[2]], null);
            _arg4[11] = new PropertyWatcher("fbk", {propertyChange:true}, [_arg3[7]], _arg2);
            _arg4[12] = new PropertyWatcher("textHeight", null, [_arg3[7]], null);
            _arg4[0] = new PropertyWatcher("question", {propertyChange:true}, [_arg3[0]], _arg2);
            _arg4[1] = new PropertyWatcher("Options", {propertyChange:true}, [_arg3[0]], null);
            _arg4[10] = new PropertyWatcher("fbk_visible", {propertyChange:true}, [_arg3[6]], _arg2);
            _arg4[2].updateParent(_arg1);
            _arg4[2].addChild(_arg4[3]);
            _arg4[3].addChild(_arg4[4]);
            _arg4[4].addChild(_arg4[7]);
            _arg4[4].addChild(_arg4[8]);
            _arg4[4].addChild(_arg4[9]);
            _arg4[4].addChild(_arg4[5]);
            _arg4[4].addChild(_arg4[6]);
            _arg4[11].updateParent(_arg1);
            _arg4[11].addChild(_arg4[12]);
            _arg4[0].updateParent(_arg1);
            _arg4[0].addChild(_arg4[1]);
            _arg4[10].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            RadioButtonQuestion.watcherSetupUtil = new (_com_redbox_changetech_view_components_questions_RadioButtonQuestionWatcherSetupUtil);
        }

    }
}//package 
