//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.questions.*;

    public class _com_redbox_changetech_view_components_questions_SliderQuestionWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            var target:* = _arg1;
            var propertyGetter:* = _arg2;
            var bindings:* = _arg3;
            var watchers:* = _arg4;
            watchers[8] = new PropertyWatcher("sliderStyleName", {propertyChange:true}, [bindings[5]], propertyGetter);
            watchers[0] = new PropertyWatcher("_direction", {propertyChange:true}, [bindings[7], bindings[0]], propertyGetter);
            watchers[6] = new PropertyWatcher("sliderMinLabel", {propertyChange:true}, [bindings[4], bindings[10]], propertyGetter);
            watchers[14] = new PropertyWatcher("text", {textChanged:true, change:false}, [bindings[10]], null);
            watchers[7] = new PropertyWatcher("y", {yChanged:true}, [bindings[4]], null);
            watchers[17] = new PropertyWatcher("sliderMaxLabel", {propertyChange:true}, [bindings[13]], propertyGetter);
            watchers[18] = new PropertyWatcher("text", {textChanged:true, change:false}, [bindings[13]], null);
            watchers[5] = new PropertyWatcher("_labels", {propertyChange:true}, [bindings[3]], propertyGetter);
            watchers[1] = new PropertyWatcher("answer", {propertyChange:true}, [bindings[1]], propertyGetter);
            watchers[12] = new PropertyWatcher("_roomVO", {propertyChange:true}, [bindings[9], bindings[12]], propertyGetter);
            watchers[13] = new PropertyWatcher("textColour1", {propertyChange:true}, [bindings[9], bindings[12]], null);
            watchers[2] = new PropertyWatcher("question", {propertyChange:true}, [bindings[2], bindings[8], bindings[11]], propertyGetter);
            watchers[3] = new PropertyWatcher("Options", {propertyChange:true}, [bindings[2], bindings[8], bindings[11]], null);
            watchers[15] = new ArrayElementWatcher(target, function (){
                return ((target.question.Options.length - 1));
            }, [bindings[11]]);
            watchers[16] = new PropertyWatcher("Description", null, [bindings[11]], null);
            watchers[9] = new ArrayElementWatcher(target, function (){
                return (0);
            }, [bindings[8]]);
            watchers[11] = new PropertyWatcher("Description", null, [bindings[8]], null);
            watchers[8].updateParent(target);
            watchers[0].updateParent(target);
            watchers[6].updateParent(target);
            watchers[6].addChild(watchers[14]);
            watchers[6].addChild(watchers[7]);
            watchers[17].updateParent(target);
            watchers[17].addChild(watchers[18]);
            watchers[5].updateParent(target);
            watchers[1].updateParent(target);
            watchers[12].updateParent(target);
            watchers[12].addChild(watchers[13]);
            watchers[2].updateParent(target);
            watchers[2].addChild(watchers[3]);
            watchers[15].arrayWatcher = watchers[3];
            watchers[3].addChild(watchers[15]);
            watchers[15].addChild(watchers[16]);
            watchers[9].arrayWatcher = watchers[3];
            watchers[3].addChild(watchers[9]);
            watchers[9].addChild(watchers[11]);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            SliderQuestion.watcherSetupUtil = new (_com_redbox_changetech_view_components_questions_SliderQuestionWatcherSetupUtil);
        }

    }
}//package 
