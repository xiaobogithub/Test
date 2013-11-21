//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.display.*;
    import flash.geom.*;
    import flash.media.*;
    import flash.text.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.binding.*;
    import mx.containers.*;
    import com.redbox.changetech.model.*;
    import assets.*;
    import com.redbox.changetech.vo.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import flash.filters.*;
    import flash.ui.*;
    import com.redbox.changetech.util.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceAudioPlayer extends VBox implements IBindingClient {

        private var _132495564loadPercentBox:Box
        mx_internal var _bindings:Array
        private var _493563858playing:Boolean = false
        private var _678927291percent:Number
        public var _BalanceAudioPlayer_Label1:Label
        public var _BalanceAudioPlayer_Label2:Label
        private var _1086638673mediaSoundLength:Number = 0
        private var model:BalanceModelLocator
        private var _mediaSound:Sound
        private var _1191704449loadPercent:Number
        private var _bytesTotal:int
        private var _mediaURL:String
        private var pausePosition:Number = 0
        mx_internal var _bindingsByDestination:Object
        private var _mediaTitle:String
        private var _902124208seekBarBox:Box
        private var _572854234playPauseBtn:Button
        private var _bytesLoaded:int
        mx_internal var _watchers:Array
        private var _1971782267seekBar:HSlider
        private var positionTimer:Timer
        private var _925318956roomVO:RoomVO
        private var _channel:SoundChannel
        mx_internal var _bindingsBeginWithWord:Object
        private var _1180563074isDrag:Boolean = false
        private var _1002217129label_load_percent:Label
        private var sndinit:Boolean = false
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceAudioPlayer(){
            _documentDescriptor_ = new UIComponentDescriptor({type:VBox, propertiesFactory:function ():Object{
                return ({width:316, height:100, childDescriptors:[new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                    return ({percentWidth:90, childDescriptors:[new UIComponentDescriptor({type:Label, id:"_BalanceAudioPlayer_Label1", stylesFactory:function ():void{
                        this.fontSize = 14;
                        this.fontFamily = "Helvetica Neue";
                    }}), new UIComponentDescriptor({type:Label, id:"_BalanceAudioPlayer_Label2", stylesFactory:function ():void{
                        this.fontSize = 14;
                        this.fontFamily = "Helvetica Neue";
                    }})]});
                }}), new UIComponentDescriptor({type:Box, id:"seekBarBox", stylesFactory:function ():void{
                    this.horizontalAlign = "center";
                    this.verticalAlign = "middle";
                }, propertiesFactory:function ():Object{
                    return ({width:286, height:11, childDescriptors:[new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                        return ({x:0, y:0, percentWidth:100, percentHeight:100, childDescriptors:[new UIComponentDescriptor({type:Box, id:"loadPercentBox", stylesFactory:function ():void{
                            this.horizontalAlign = "left";
                            this.verticalAlign = "middle";
                            this.backgroundColor = 0xDEDEDE;
                            this.backgroundAlpha = 0.9;
                        }, propertiesFactory:function ():Object{
                            return ({x:0, y:0, width:0, percentHeight:80});
                        }}), new UIComponentDescriptor({type:HSlider, id:"seekBar", events:{change:"__seekBar_change", thumbPress:"__seekBar_thumbPress", thumbRelease:"__seekBar_thumbRelease"}, propertiesFactory:function ():Object{
                            return ({width:1, showDataTip:false, styleName:"audioGreenSlider", enabled:true});
                        }})]});
                    }})]});
                }}), new UIComponentDescriptor({type:HBox, propertiesFactory:function ():Object{
                    return ({percentWidth:90, childDescriptors:[new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                        this.horizontalAlign = "left";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:50, childDescriptors:[new UIComponentDescriptor({type:Button, id:"playPauseBtn", events:{click:"__playPauseBtn_click"}, propertiesFactory:function ():Object{
                            return ({toggle:true, selected:false});
                        }})]});
                    }}), new UIComponentDescriptor({type:HBox, stylesFactory:function ():void{
                        this.horizontalAlign = "right";
                    }, propertiesFactory:function ():Object{
                        return ({percentWidth:50, childDescriptors:[new UIComponentDescriptor({type:Label, id:"label_load_percent", stylesFactory:function ():void{
                            this.fontSize = 12;
                            this.fontFamily = "Helvetica Neue";
                        }})]});
                    }})]});
                }})]});
            }});
            _925318956roomVO = RoomVO(Config.ROOM_CONFIGS.getItemAt(BalanceModelLocator.getInstance().room));
            model = BalanceModelLocator.getInstance();
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            if (!this.styleDeclaration){
                this.styleDeclaration = new CSSStyleDeclaration();
            };
            this.styleDeclaration.defaultFactory = function ():void{
                this.horizontalAlign = "center";
                this.verticalAlign = "middle";
            };
            this.width = 316;
            this.height = 100;
            this.addEventListener("creationComplete", ___BalanceAudioPlayer_VBox1_creationComplete);
        }
        public function ___BalanceAudioPlayer_VBox1_creationComplete(_arg1:FlexEvent):void{
            init(_arg1);
        }
        private function init(_arg1:FlexEvent):void{
            if (mediaURL){
                loadMedia();
            };
        }
        private function updatePosition(_arg1:TimerEvent):void{
            pausePosition = _channel.position;
            if (!isDrag){
                updateSeekBar();
            };
        }
        private function set _900784405mediaURL(_arg1:String):void{
            _mediaURL = _arg1;
        }
        private function playMedia(_arg1):void{
            trace(("playMedia " + _arg1));
            _channel = _mediaSound.play(_arg1);
            _channel.addEventListener(Event.SOUND_COMPLETE, complete);
            if (!sndinit){
                positionTimer = new Timer(50);
                positionTimer.addEventListener(TimerEvent.TIMER, updatePosition);
                sndinit = true;
            };
            positionTimer.start();
            playing = true;
        }
        private function _BalanceAudioPlayer_bindingExprs():void{
            var _local1:*;
            _local1 = Assets.getInstance().audioPlayerBorder;
            _local1 = roomVO.textColour1;
            _local1 = (playing) ? model.languageVO.getLang("now_playing") : "";
            _local1 = mediaTitle;
            _local1 = Assets.getInstance().audioPlayerTrack;
            _local1 = CustomSliderThumb;
            _local1 = Assets.getInstance().playIcon;
            _local1 = !((mediaSoundLength == 0));
            _local1 = ((mediaSoundLength)!=0) ? 100 : 10;
            _local1 = (((loadPercent >= 0)) ? Math.floor(loadPercent) : 0 + "%");
        }
        private function loadingError(_arg1:IOErrorEvent):void{
            Alert.show("No media specified.");
        }
        private function progress(_arg1:ProgressEvent){
            if (isDrag){
                return;
            };
            _bytesLoaded = _arg1.bytesLoaded;
            _bytesTotal = _arg1.bytesTotal;
            loadPercent = Math.round(((100 * _bytesLoaded) / _bytesTotal));
            mediaSoundLength = ((_mediaSound.length * _bytesTotal) / _bytesLoaded);
            seekBar.width = (seekBar.parent.width * (loadPercent / 100));
            loadPercentBox.width = seekBar.width;
            updateSeekBar();
        }
        private function get playing():Boolean{
            return (this._493563858playing);
        }
        private function complete(_arg1:Event=null):void{
            if (_channel){
                _channel.removeEventListener(Event.SOUND_COMPLETE, complete);
            };
            if (positionTimer){
                positionTimer.removeEventListener(TimerEvent.TIMER, updatePosition);
            };
            seekBar.value = 0;
            sndinit = false;
            playPause(_arg1);
            playing = false;
            playPauseBtn.setStyle("skin", Assets.getInstance().playIcon);
            pausePosition = 0;
        }
        private function get percent():Number{
            return (this._678927291percent);
        }
        public function __seekBar_thumbPress(_arg1:SliderEvent):void{
            isDrag = true;
        }
        private function playPause(_arg1:Event=null):void{
            if (playing){
                pausePosition = _channel.position;
                _channel.stop();
                playing = false;
                playPauseBtn.setStyle("skin", Assets.getInstance().playIcon);
                if (((positionTimer) && (positionTimer.running))){
                    positionTimer.stop();
                };
            } else {
                playPauseBtn.setStyle("skin", Assets.getInstance().pauseIcon);
                playMedia(pausePosition);
            };
        }
        public function get seekBarBox():Box{
            return (this._902124208seekBarBox);
        }
        public function get label_load_percent():Label{
            return (this._1002217129label_load_percent);
        }
        private function get mediaSoundLength():Number{
            return (this._1086638673mediaSoundLength);
        }
        private function set playing(_arg1:Boolean):void{
            var _local2:Object = this._493563858playing;
            if (_local2 !== _arg1){
                this._493563858playing = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "playing", _local2, _arg1));
            };
        }
        private function initSoundDetails(_arg1:Event):void{
            trace(("ID3 : " + _mediaSound.id3.toString()));
        }
        private function updateSeekBar(){
            if (((!(isDrag)) && ((pausePosition > 0)))){
                seekBar.value = ((pausePosition / _mediaSound.length) * 10);
            };
        }
        public function __seekBar_thumbRelease(_arg1:SliderEvent):void{
            isDrag = false;
        }
        public function get seekBar():HSlider{
            return (this._1971782267seekBar);
        }
        private function get isDrag():Boolean{
            return (this._1180563074isDrag);
        }
        private function get loadPercent():Number{
            return (this._1191704449loadPercent);
        }
        private function set percent(_arg1:Number):void{
            var _local2:Object = this._678927291percent;
            if (_local2 !== _arg1){
                this._678927291percent = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "percent", _local2, _arg1));
            };
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceAudioPlayer_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceAudioPlayerWatcherSetupUtil");
                var _local2 = watcherSetupUtilClass;
                _local2["init"](null);
            };
            _watcherSetupUtil.setup(this, function (_arg1:String){
                return (target[_arg1]);
            }, bindings, watchers);
            var i:* = 0;
            while (i < bindings.length) {
                Binding(bindings[i]).execute();
                i = (i + 1);
            };
            mx_internal::_bindings = mx_internal::_bindings.concat(bindings);
            mx_internal::_watchers = mx_internal::_watchers.concat(watchers);
            super.initialize();
        }
        private function loadMedia():void{
            mediaSoundLength = 0;
            _mediaSound = new Sound();
            _mediaSound.addEventListener(Event.ID3, initSoundDetails);
            _mediaSound.addEventListener(Event.COMPLETE, soundLoaded);
            _mediaSound.addEventListener(IOErrorEvent.IO_ERROR, loadingError);
            _mediaSound.addEventListener(ProgressEvent.PROGRESS, progress);
            _mediaSound.load(new URLRequest(mediaURL));
        }
        private function set _1929384148mediaTitle(_arg1:String):void{
            _mediaTitle = _arg1;
        }
        private function set mediaSoundLength(_arg1:Number):void{
            var _local2:Object = this._1086638673mediaSoundLength;
            if (_local2 !== _arg1){
                this._1086638673mediaSoundLength = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mediaSoundLength", _local2, _arg1));
            };
        }
        private function set roomVO(_arg1:RoomVO):void{
            var _local2:Object = this._925318956roomVO;
            if (_local2 !== _arg1){
                this._925318956roomVO = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "roomVO", _local2, _arg1));
            };
        }
        private function _BalanceAudioPlayer_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Class{
                return (Assets.getInstance().audioPlayerBorder);
            }, function (_arg1:Class):void{
                this.setStyle("borderSkin", _arg1);
            }, "this.borderSkin");
            result[0] = binding;
            binding = new Binding(this, function ():uint{
                return (roomVO.textColour1);
            }, function (_arg1:uint):void{
                _BalanceAudioPlayer_Label1.setStyle("color", _arg1);
            }, "_BalanceAudioPlayer_Label1.color");
            result[1] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = (playing) ? model.languageVO.getLang("now_playing") : "";
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceAudioPlayer_Label1.text = _arg1;
            }, "_BalanceAudioPlayer_Label1.text");
            result[2] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = mediaTitle;
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                _BalanceAudioPlayer_Label2.text = _arg1;
            }, "_BalanceAudioPlayer_Label2.text");
            result[3] = binding;
            binding = new Binding(this, function ():Class{
                return (Assets.getInstance().audioPlayerTrack);
            }, function (_arg1:Class):void{
                seekBarBox.setStyle("borderSkin", _arg1);
            }, "seekBarBox.borderSkin");
            result[4] = binding;
            binding = new Binding(this, function ():Class{
                return (CustomSliderThumb);
            }, function (_arg1:Class):void{
                seekBar.sliderThumbClass = _arg1;
            }, "seekBar.sliderThumbClass");
            result[5] = binding;
            binding = new Binding(this, function ():Class{
                return (Assets.getInstance().playIcon);
            }, function (_arg1:Class):void{
                playPauseBtn.setStyle("skin", _arg1);
            }, "playPauseBtn.skin");
            result[6] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((mediaSoundLength == 0)));
            }, function (_arg1:Boolean):void{
                playPauseBtn.enabled = _arg1;
            }, "playPauseBtn.enabled");
            result[7] = binding;
            binding = new Binding(this, function ():Number{
                return (((mediaSoundLength)!=0) ? 100 : 10);
            }, function (_arg1:Number):void{
                playPauseBtn.alpha = _arg1;
            }, "playPauseBtn.alpha");
            result[8] = binding;
            binding = new Binding(this, function ():String{
                var _local1:* = (((loadPercent >= 0)) ? Math.floor(loadPercent) : 0 + "%");
                var _local2:* = ((_local1 == undefined)) ? null : String(_local1);
                return (_local2);
            }, function (_arg1:String):void{
                label_load_percent.text = _arg1;
            }, "label_load_percent.text");
            result[9] = binding;
            return (result);
        }
        public function set label_load_percent(_arg1:Label):void{
            var _local2:Object = this._1002217129label_load_percent;
            if (_local2 !== _arg1){
                this._1002217129label_load_percent = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "label_load_percent", _local2, _arg1));
            };
        }
        public function set seekBarBox(_arg1:Box):void{
            var _local2:Object = this._902124208seekBarBox;
            if (_local2 !== _arg1){
                this._902124208seekBarBox = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "seekBarBox", _local2, _arg1));
            };
        }
        public function set playPauseBtn(_arg1:Button):void{
            var _local2:Object = this._572854234playPauseBtn;
            if (_local2 !== _arg1){
                this._572854234playPauseBtn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "playPauseBtn", _local2, _arg1));
            };
        }
        public function set mediaTitle(_arg1:String):void{
            var _local2:Object = this.mediaTitle;
            if (_local2 !== _arg1){
                this._1929384148mediaTitle = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mediaTitle", _local2, _arg1));
            };
        }
        public function set loadPercentBox(_arg1:Box):void{
            var _local2:Object = this._132495564loadPercentBox;
            if (_local2 !== _arg1){
                this._132495564loadPercentBox = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "loadPercentBox", _local2, _arg1));
            };
        }
        public function __playPauseBtn_click(_arg1:MouseEvent):void{
            playPause(_arg1);
        }
        private function scanPosition(_arg1:Event):void{
            isDrag = true;
            if (((positionTimer) && (positionTimer.running))){
                positionTimer.stop();
            };
            pausePosition = ((seekBar.value / 10) * _mediaSound.length);
            if (playing){
                _channel.stop();
                playMedia(pausePosition);
            };
            isDrag = false;
        }
        public function get loadPercentBox():Box{
            return (this._132495564loadPercentBox);
        }
        public function __seekBar_change(_arg1:SliderEvent):void{
            scanPosition(_arg1);
        }
        public function set mediaURL(_arg1:String):void{
            var _local2:Object = this.mediaURL;
            if (_local2 !== _arg1){
                this._900784405mediaURL = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "mediaURL", _local2, _arg1));
            };
        }
        public function get mediaTitle():String{
            return (_mediaTitle);
        }
        public function get playPauseBtn():Button{
            return (this._572854234playPauseBtn);
        }
        private function get roomVO():RoomVO{
            return (this._925318956roomVO);
        }
        public function get mediaURL():String{
            return (_mediaURL);
        }
        private function soundLoaded(_arg1:Event):void{
            mediaSoundLength = _mediaSound.length;
            trace(("soundLoaded " + mediaSoundLength));
            label_load_percent.visible = false;
        }
        public function set seekBar(_arg1:HSlider):void{
            var _local2:Object = this._1971782267seekBar;
            if (_local2 !== _arg1){
                this._1971782267seekBar = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "seekBar", _local2, _arg1));
            };
        }
        public function destroy():void{
            if (_channel){
                _channel.stop();
            };
            complete();
            _mediaSound = null;
            _channel = null;
        }
        private function set isDrag(_arg1:Boolean):void{
            var _local2:Object = this._1180563074isDrag;
            if (_local2 !== _arg1){
                this._1180563074isDrag = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "isDrag", _local2, _arg1));
            };
        }
        private function set loadPercent(_arg1:Number):void{
            var _local2:Object = this._1191704449loadPercent;
            if (_local2 !== _arg1){
                this._1191704449loadPercent = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "loadPercent", _local2, _arg1));
            };
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceAudioPlayer._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
