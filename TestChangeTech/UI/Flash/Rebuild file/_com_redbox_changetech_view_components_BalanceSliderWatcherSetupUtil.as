//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import flash.display.*;
    import mx.core.*;
    import mx.binding.*;
    import com.redbox.changetech.view.components.*;

    public class _com_redbox_changetech_view_components_BalanceSliderWatcherSetupUtil extends Sprite implements IWatcherSetupUtil {

        public function setup(_arg1:Object, _arg2:Function, _arg3:Array, _arg4:Array):void{
            _arg4[13] = new PropertyWatcher("numberGap", {propertyChange:true}, [_arg3[12]], _arg2);
            _arg4[8] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[15], _arg3[19], _arg3[7]], _arg2);
            _arg4[7] = new PropertyWatcher("_fillAlphas2", {propertyChange:true}, [_arg3[6]], _arg2);
            _arg4[9] = new PropertyWatcher("slider_mask", {propertyChange:true}, [_arg3[8]], _arg2);
            _arg4[14] = new PropertyWatcher("numberRep", {propertyChange:true}, [_arg3[13], _arg3[12]], _arg2);
            _arg4[15] = new PropertyWatcher("dataProvider", {collectionChange:true}, [_arg3[13], _arg3[12]], null);
            _arg4[6] = new PropertyWatcher("_fillColors2", {propertyChange:true}, [_arg3[5]], _arg2);
            _arg4[17] = new PropertyWatcher("slide", {propertyChange:true}, [_arg3[15], _arg3[16]], _arg2);
            _arg4[18] = new PropertyWatcher("width", {widthChanged:true}, [_arg3[15]], null);
            _arg4[19] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[16]], null);
            _arg4[5] = new PropertyWatcher("_fillAlphas1", {propertyChange:true}, [_arg3[3]], _arg2);
            _arg4[10] = new PropertyWatcher("offset", {propertyChange:true}, [_arg3[9]], _arg2);
            _arg4[4] = new PropertyWatcher("_fillColors1", {propertyChange:true}, [_arg3[2]], _arg2);
            _arg4[20] = new PropertyWatcher("sliderValue", {propertyChange:true}, [_arg3[17]], _arg2);
            _arg4[11] = new PropertyWatcher("numbers", {propertyChange:true}, [_arg3[10]], _arg2);
            _arg4[12] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[10]], null);
            _arg4[2] = new PropertyWatcher("bg", {propertyChange:true}, [_arg3[4], _arg3[1]], _arg2);
            _arg4[3] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[4], _arg3[1]], null);
            _arg4[16] = new PropertyWatcher("textColor", {propertyChange:true}, [_arg3[14]], _arg2);
            _arg4[1] = new PropertyWatcher("height", {heightChanged:true}, [_arg3[16], _arg3[1], _arg3[10]], _arg2);
            _arg4[0] = new PropertyWatcher("bgHeight", {propertyChange:true}, [_arg3[0]], _arg2);
            _arg4[13].updateParent(_arg1);
            _arg4[8].updateParent(_arg1);
            _arg4[7].updateParent(_arg1);
            _arg4[9].updateParent(_arg1);
            _arg4[14].updateParent(_arg1);
            _arg4[14].addChild(_arg4[15]);
            _arg4[6].updateParent(_arg1);
            _arg4[17].updateParent(_arg1);
            _arg4[17].addChild(_arg4[18]);
            _arg4[17].addChild(_arg4[19]);
            _arg4[5].updateParent(_arg1);
            _arg4[10].updateParent(_arg1);
            _arg4[4].updateParent(_arg1);
            _arg4[20].updateParent(_arg1);
            _arg4[11].updateParent(_arg1);
            _arg4[11].addChild(_arg4[12]);
            _arg4[2].updateParent(_arg1);
            _arg4[2].addChild(_arg4[3]);
            _arg4[16].updateParent(_arg1);
            _arg4[1].updateParent(_arg1);
            _arg4[0].updateParent(_arg1);
        }

        public static function init(_arg1:IFlexModuleFactory):void{
            BalanceSlider.watcherSetupUtil = new (_com_redbox_changetech_view_components_BalanceSliderWatcherSetupUtil);
        }

    }
}//package 
