//Created by Action Script Viewer - http://www.buraks.com/asv
package assets {
    import flash.events.*;
    import mx.events.*;

    public class Assets implements IEventDispatcher {

        private var _1635531953traffic_red:Class
        private var _1070840842glasses_fg:Class
        private var _1911977268audioPlayerTrack:Class
        private var _1878426061playIcon:Class
        private var _202056257traffic_green:Class
        private var _1231310186info_icon:Class
        private var _3559837tick:Class
        private var _1334831313back_icon:Class
        private var _1936472604tickGrey:Class
        private var _97526796floor:Class
        private var _868044338muteIcon:Class
        private var _829090703pauseIcon:Class
        private var _838440446fat_cross_icon:Class
        private var _1972596093shadowImg:Class
        private var _1878025967continue_icon:Class
        private var _1432297372balance_badge_emotion:Class
        private var _2039841698large_exit:Class
        private var _2006461167fullscreenToggle:Class
        private var _bindingEventDispatcher:EventDispatcher
        private var _1375834781audioPlayerBorder:Class
        private var _1663301934balance_logo:Class
        private var _1070840966glasses_bg:Class
        private var _19964745balance_intro:Class
        private var _1784885173balance_badge_motivation:Class
        private var _1687543948balance_badge_willpower:Class
        private var _1822656266traffic_yellow:Class

        private static var instance:Assets;

        public function Assets(){
            _1070840842glasses_fg = Assets_glasses_fg;
            _2006461167fullscreenToggle = Assets_fullscreenToggle;
            _1070840966glasses_bg = Assets_glasses_bg;
            _1663301934balance_logo = Assets_balance_logo;
            _1784885173balance_badge_motivation = Assets_balance_badge_motivation;
            _1687543948balance_badge_willpower = Assets_balance_badge_willpower;
            _1432297372balance_badge_emotion = Assets_balance_badge_emotion;
            _19964745balance_intro = Assets_balance_intro;
            _1972596093shadowImg = Assets_shadowImg;
            _3559837tick = Assets_tick;
            _1936472604tickGrey = Assets_tickGrey;
            _1635531953traffic_red = Assets_traffic_red;
            _1822656266traffic_yellow = Assets_traffic_yellow;
            _202056257traffic_green = Assets_traffic_green;
            _1878025967continue_icon = Assets_continue_icon;
            _1334831313back_icon = Assets_back_icon;
            _1375834781audioPlayerBorder = Assets_audioPlayerBorder;
            _1878426061playIcon = Assets_playIcon;
            _829090703pauseIcon = Assets_pauseIcon;
            _868044338muteIcon = Assets_muteIcon;
            _1911977268audioPlayerTrack = Assets_audioPlayerTrack;
            _97526796floor = Assets_floor;
            _1231310186info_icon = Assets_info_icon;
            _838440446fat_cross_icon = Assets_fat_cross_icon;
            _2039841698large_exit = Assets_large_exit;
            _bindingEventDispatcher = new EventDispatcher(IEventDispatcher(this));
            super();
            if (instance != null){
                throw (new Error("Error: Singletons can only be instantiated via getInstance() method!"));
            };
            Assets.instance = this;
        }
        public function get fullscreenToggle():Class{
            return (this._2006461167fullscreenToggle);
        }
        public function get floor():Class{
            return (this._97526796floor);
        }
        public function set shadowImg(_arg1:Class):void{
            var _local2:Object = this._1972596093shadowImg;
            if (_local2 !== _arg1){
                this._1972596093shadowImg = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "shadowImg", _local2, _arg1));
            };
        }
        public function get continue_icon():Class{
            return (this._1878025967continue_icon);
        }
        public function set floor(_arg1:Class):void{
            var _local2:Object = this._97526796floor;
            if (_local2 !== _arg1){
                this._97526796floor = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "floor", _local2, _arg1));
            };
        }
        public function get tickGrey():Class{
            return (this._1936472604tickGrey);
        }
        public function set continue_icon(_arg1:Class):void{
            var _local2:Object = this._1878025967continue_icon;
            if (_local2 !== _arg1){
                this._1878025967continue_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "continue_icon", _local2, _arg1));
            };
        }
        public function set pauseIcon(_arg1:Class):void{
            var _local2:Object = this._829090703pauseIcon;
            if (_local2 !== _arg1){
                this._829090703pauseIcon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "pauseIcon", _local2, _arg1));
            };
        }
        public function get balance_logo():Class{
            return (this._1663301934balance_logo);
        }
        public function get muteIcon():Class{
            return (this._868044338muteIcon);
        }
        public function set balance_badge_emotion(_arg1:Class):void{
            var _local2:Object = this._1432297372balance_badge_emotion;
            if (_local2 !== _arg1){
                this._1432297372balance_badge_emotion = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balance_badge_emotion", _local2, _arg1));
            };
        }
        public function get audioPlayerTrack():Class{
            return (this._1911977268audioPlayerTrack);
        }
        public function set tickGrey(_arg1:Class):void{
            var _local2:Object = this._1936472604tickGrey;
            if (_local2 !== _arg1){
                this._1936472604tickGrey = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tickGrey", _local2, _arg1));
            };
        }
        public function dispatchEvent(_arg1:Event):Boolean{
            return (_bindingEventDispatcher.dispatchEvent(_arg1));
        }
        public function set tick(_arg1:Class):void{
            var _local2:Object = this._3559837tick;
            if (_local2 !== _arg1){
                this._3559837tick = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "tick", _local2, _arg1));
            };
        }
        public function set large_exit(_arg1:Class):void{
            var _local2:Object = this._2039841698large_exit;
            if (_local2 !== _arg1){
                this._2039841698large_exit = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "large_exit", _local2, _arg1));
            };
        }
        public function get traffic_green():Class{
            return (this._202056257traffic_green);
        }
        public function get balance_intro():Class{
            return (this._19964745balance_intro);
        }
        public function addEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false, _arg4:int=0, _arg5:Boolean=false):void{
            _bindingEventDispatcher.addEventListener(_arg1, _arg2, _arg3, _arg4, _arg5);
        }
        public function get traffic_red():Class{
            return (this._1635531953traffic_red);
        }
        public function removeEventListener(_arg1:String, _arg2:Function, _arg3:Boolean=false):void{
            _bindingEventDispatcher.removeEventListener(_arg1, _arg2, _arg3);
        }
        public function set balance_logo(_arg1:Class):void{
            var _local2:Object = this._1663301934balance_logo;
            if (_local2 !== _arg1){
                this._1663301934balance_logo = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balance_logo", _local2, _arg1));
            };
        }
        public function set muteIcon(_arg1:Class):void{
            var _local2:Object = this._868044338muteIcon;
            if (_local2 !== _arg1){
                this._868044338muteIcon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "muteIcon", _local2, _arg1));
            };
        }
        public function set audioPlayerTrack(_arg1:Class):void{
            var _local2:Object = this._1911977268audioPlayerTrack;
            if (_local2 !== _arg1){
                this._1911977268audioPlayerTrack = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "audioPlayerTrack", _local2, _arg1));
            };
        }
        public function set balance_badge_motivation(_arg1:Class):void{
            var _local2:Object = this._1784885173balance_badge_motivation;
            if (_local2 !== _arg1){
                this._1784885173balance_badge_motivation = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balance_badge_motivation", _local2, _arg1));
            };
        }
        public function get audioPlayerBorder():Class{
            return (this._1375834781audioPlayerBorder);
        }
        public function get balance_badge_willpower():Class{
            return (this._1687543948balance_badge_willpower);
        }
        public function get info_icon():Class{
            return (this._1231310186info_icon);
        }
        public function get glasses_bg():Class{
            return (this._1070840966glasses_bg);
        }
        public function set traffic_green(_arg1:Class):void{
            var _local2:Object = this._202056257traffic_green;
            if (_local2 !== _arg1){
                this._202056257traffic_green = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "traffic_green", _local2, _arg1));
            };
        }
        public function willTrigger(_arg1:String):Boolean{
            return (_bindingEventDispatcher.willTrigger(_arg1));
        }
        public function get shadowImg():Class{
            return (this._1972596093shadowImg);
        }
        public function get pauseIcon():Class{
            return (this._829090703pauseIcon);
        }
        public function set balance_intro(_arg1:Class):void{
            var _local2:Object = this._19964745balance_intro;
            if (_local2 !== _arg1){
                this._19964745balance_intro = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balance_intro", _local2, _arg1));
            };
        }
        public function get balance_badge_emotion():Class{
            return (this._1432297372balance_badge_emotion);
        }
        public function set traffic_red(_arg1:Class):void{
            var _local2:Object = this._1635531953traffic_red;
            if (_local2 !== _arg1){
                this._1635531953traffic_red = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "traffic_red", _local2, _arg1));
            };
        }
        public function set playIcon(_arg1:Class):void{
            var _local2:Object = this._1878426061playIcon;
            if (_local2 !== _arg1){
                this._1878426061playIcon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "playIcon", _local2, _arg1));
            };
        }
        public function set back_icon(_arg1:Class):void{
            var _local2:Object = this._1334831313back_icon;
            if (_local2 !== _arg1){
                this._1334831313back_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "back_icon", _local2, _arg1));
            };
        }
        public function get tick():Class{
            return (this._3559837tick);
        }
        public function get large_exit():Class{
            return (this._2039841698large_exit);
        }
        public function get balance_badge_motivation():Class{
            return (this._1784885173balance_badge_motivation);
        }
        public function set audioPlayerBorder(_arg1:Class):void{
            var _local2:Object = this._1375834781audioPlayerBorder;
            if (_local2 !== _arg1){
                this._1375834781audioPlayerBorder = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "audioPlayerBorder", _local2, _arg1));
            };
        }
        public function get back_icon():Class{
            return (this._1334831313back_icon);
        }
        public function set info_icon(_arg1:Class):void{
            var _local2:Object = this._1231310186info_icon;
            if (_local2 !== _arg1){
                this._1231310186info_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "info_icon", _local2, _arg1));
            };
        }
        public function set fullscreenToggle(_arg1:Class):void{
            var _local2:Object = this._2006461167fullscreenToggle;
            if (_local2 !== _arg1){
                this._2006461167fullscreenToggle = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fullscreenToggle", _local2, _arg1));
            };
        }
        public function get playIcon():Class{
            return (this._1878426061playIcon);
        }
        public function set balance_badge_willpower(_arg1:Class):void{
            var _local2:Object = this._1687543948balance_badge_willpower;
            if (_local2 !== _arg1){
                this._1687543948balance_badge_willpower = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balance_badge_willpower", _local2, _arg1));
            };
        }
        public function set glasses_fg(_arg1:Class):void{
            var _local2:Object = this._1070840842glasses_fg;
            if (_local2 !== _arg1){
                this._1070840842glasses_fg = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "glasses_fg", _local2, _arg1));
            };
        }
        public function set traffic_yellow(_arg1:Class):void{
            var _local2:Object = this._1822656266traffic_yellow;
            if (_local2 !== _arg1){
                this._1822656266traffic_yellow = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "traffic_yellow", _local2, _arg1));
            };
        }
        public function set glasses_bg(_arg1:Class):void{
            var _local2:Object = this._1070840966glasses_bg;
            if (_local2 !== _arg1){
                this._1070840966glasses_bg = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "glasses_bg", _local2, _arg1));
            };
        }
        public function get glasses_fg():Class{
            return (this._1070840842glasses_fg);
        }
        public function hasEventListener(_arg1:String):Boolean{
            return (_bindingEventDispatcher.hasEventListener(_arg1));
        }
        public function set fat_cross_icon(_arg1:Class):void{
            var _local2:Object = this._838440446fat_cross_icon;
            if (_local2 !== _arg1){
                this._838440446fat_cross_icon = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fat_cross_icon", _local2, _arg1));
            };
        }
        public function get traffic_yellow():Class{
            return (this._1822656266traffic_yellow);
        }
        public function get fat_cross_icon():Class{
            return (this._838440446fat_cross_icon);
        }

        public static function getInstance():Assets{
            if (instance == null){
                instance = new (Assets);
            };
            return (instance);
        }

    }
}//package assets 
