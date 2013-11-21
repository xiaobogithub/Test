package com.ning.components.media{
	import com.ning.components.media.skin.*;
	import fl.core.UIComponent;
	import flash.display.InteractiveObject;
	import flash.display.MovieClip;
	import flash.display.SimpleButton;
	import flash.errors.IllegalOperationError;
	import flash.errors.IOError;
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	import flash.events.MouseEvent;
	import flash.events.ProgressEvent;
	import flash.events.TimerEvent;
	import flash.geom.ColorTransform;
	import flash.geom.Rectangle;
	import flash.media.Sound;
	import flash.media.SoundChannel;
	import flash.media.SoundLoaderContext;
	import flash.net.URLRequest;
	import flash.system.ApplicationDomain;
	import flash.utils.Timer;

	public class SoundPlayer extends UIComponent {
		private var _minWidth:Number = 155;
		private var _minHeight:Number = 60;
		private var _playerWidth:Number;
		private var _playPauseAncherLeft:Boolean = true;
		private var _playPauseAncherRight:Boolean = false;
		private var _stopAncherLeft:Boolean = false;
		private var _stopAncherRight:Boolean = true;
		private var _seekBarAncherLeft:Boolean = true;
		private var _seekBarAncherRight:Boolean = true;
		private var _skinBackgroundAlpha:Number = 0.85;
		private var _skinBackgroundColor:uint = 0x47ABCB;

		private var _playPauseInitPositionLeft:Number;
		private var _playPauseInitPositionRight:Number;
		private var _stopInitPositionLeft:Number;
		private var _stopInitPositionRight:Number;
		private var _seekBarInitPositionLeft:Number;
		private var _seekBarInitPositionRight:Number;
		private var _seekBarProgressWidth:Number;

		private var _playPausePositionX:Number;
		private var _playPausePositionY:Number;
		private var _playPausePositionWidth:Number;
		private var _playPausePositionHeight:Number;
		private var _stopPositionX:Number;
		private var _stopPositionY:Number;
		private var _stopPositionWidth:Number;
		private var _stopPositionHeight:Number;

		private var _source:String;
		private var _sound:Sound;
		private var _autoPlay:Boolean = false;
		private var _soundChannel:SoundChannel;
		private var _soundLoadedPer:Number = 0;
		private var _pausePosition:Number = 0;
		private var _isDownloading:Boolean = false;
		private var _isPlaying:Boolean = false;
		private var _isPlayingBeforeDrag:Boolean;

		private var _progressTimer:Timer;

		private var _playButton:SimpleButton;
		private var _pauseButton:SimpleButton;
		private var _stopButton:SimpleButton;

		public var outline_mc:MovieClip = new MovieClip();
		public var outlineFill_mc:MovieClip = new MovieClip();
		public var outlineShadow_mc:MovieClip = new MovieClip();
		public var playPause_mc:InteractiveObject  = new MovieClip();
		public var stop_mc:InteractiveObject = new MovieClip();
		public var seekBarProgress_mc:MovieClip = new MovieClip();
		public var seekBar_mc:MovieClip = new MovieClip();
		public var seekBarHandle_mc:MovieClip = new MovieClip();
		public var seekBarHit_mc:MovieClip = new MovieClip();

		public function SoundPlayer() {
			init();
			layout();
			setUpButtons();
		}
		public function get source():String {
			return _source;
		}
		public function set source(_value:String):void {
			if (_value == _source) {
				return;
			}
			_source = _value;
			_sound = new Sound();
			_sound.addEventListener(Event.COMPLETE, soundLoadCompleteHandler);
			//_sound.addEventListener(Event.ID3, id3Handler);
			_sound.addEventListener(IOErrorEvent.IO_ERROR, soundLoadIOErrorHandler);
			_sound.addEventListener(ProgressEvent.PROGRESS, soundLoadProgressHandler);
			var _request:URLRequest = new URLRequest(_source);
			_sound.load(_request, new SoundLoaderContext(8000, true));
			_isDownloading = true;
			//
			getPlayButton();
			getStopButton();
			seekBarHandle_mc.addEventListener(MouseEvent.MOUSE_DOWN, seekBarHandleStartDragHandler);
			if (_autoPlay) {
				playClickHandler();
			}
		}
		public function get minWidth():Number {
			return _minWidth;
		}
		public function set minWidth(_value:Number):void {
			if (_value == _minWidth) {
				return;
			}
			_minWidth = _value;
			layout();
		}
		public function get minHeight():Number {
			return _minHeight;
		}
		public function set minHeight(_value:Number):void {
			if (_value == _minHeight) {
				return;
			}
			_minHeight = _value;
			layout();
		}
		public function get playPauseAncherLeft():Boolean {
			return _playPauseAncherLeft;
		}
		public function set playPauseAncherLeft(_value:Boolean):void {
			if (_value == _playPauseAncherLeft) {
				return;
			}
			_playPauseAncherLeft = _value;
			layout();
		}
		public function get playPauseAncherRight():Boolean {
			return _playPauseAncherRight;
		}
		public function set playPauseAncherRight(_value:Boolean):void {
			if (_value == _playPauseAncherRight) {
				return;
			}
			_playPauseAncherRight = _value;
			layout();
		}
		public function get stopAncherLeft():Boolean {
			return _stopAncherLeft;
		}
		public function set stopAncherLeft(_value:Boolean):void {
			if (_value == _stopAncherLeft) {
				return;
			}
			_stopAncherLeft = _value;
			layout();
		}
		public function get stopAncherRight():Boolean {
			return _stopAncherRight;
		}
		public function set stopAncherRight(_value:Boolean):void {
			if (_value == _stopAncherRight) {
				return;
			}
			_stopAncherRight = _value;
			layout();
		}
		public function get seekBarAncherLeft():Boolean {
			return _seekBarAncherLeft;
		}
		public function set seekBarAncherLeft(_value:Boolean):void {
			if (_value == _seekBarAncherLeft) {
				return;
			}
			_seekBarAncherLeft = _value;
			layout();
		}
		public function get seekBarAncherRight():Boolean {
			return _seekBarAncherRight;
		}
		public function set seekBarAncherRight(_value:Boolean):void {
			if (_value == _seekBarAncherRight) {
				return;
			}
			_seekBarAncherRight = _value;
			layout();
		}
		override public function get width():Number {
			return _playerWidth;
		}
		override public function set width(_value:Number):void {
			if (_value == _playerWidth) {
				return;
			}
			if (_value <= _minWidth) {
				_playerWidth = _minWidth;
			} else {
				_playerWidth = _value;
			}
			layout();
		}
		public function get skinBackgroundAlpha():Number {
			return _skinBackgroundAlpha;
		}
		public function set skinBackgroundAlpha(_value:Number) {
			if (_value == _skinBackgroundAlpha) {
				return;
			}
			if (_value<0 || _value>1) {
				throw new IllegalOperationError("Invalid alpha setting for SoundPlayer skinBackgroundAlpha: value should be between 0 and 1.");
			}
			_skinBackgroundAlpha = _value;
			var _colorTransform = new ColorTransform(1, 1, 1, 0, 0, 0, 0, 0xff * _skinBackgroundAlpha);
			outlineFill_mc.transform.colorTransform = _colorTransform;
		}
		public function get skinBackgroundColor():uint {
			return _skinBackgroundColor;
		}
		public function set skinBackgroundColor(_value:uint):void {
			if (_value == _skinBackgroundColor) {
				return;
			}
			_skinBackgroundColor = _value;
			var _colorTransform = new ColorTransform();
			_colorTransform.color = _skinBackgroundColor;
			outlineFill_mc.transform.colorTransform = _colorTransform;
		}
		
		public function get autoPlay():Boolean { return _autoPlay; }
		
		public function set autoPlay(value:Boolean):void 
		{
			_autoPlay = value;
		}
		private function init():void {
			addChild(outlineShadow_mc);
			addChild(outlineFill_mc);
			addChild(outline_mc);
			addChild(playPause_mc);
			addChild(stop_mc);
			addChild(seekBarHit_mc);
			addChild(seekBarHandle_mc);
			addChild(seekBar_mc);
			addChild(seekBarProgress_mc);

			if (outline_mc.width <= _minWidth) {
				_playerWidth = _minWidth;
			} else {
				_playerWidth = outline_mc.width;
			}
			_playPauseInitPositionLeft = playPause_mc.x - outline_mc.x;
			_playPauseInitPositionRight = _playerWidth - _playPauseInitPositionLeft - playPause_mc.width;
			_stopInitPositionLeft = stop_mc.x - outline_mc.x;
			_stopInitPositionRight = _playerWidth - _stopInitPositionLeft - stop_mc.width;
			_seekBarInitPositionLeft = seekBar_mc.x - outline_mc.x;
			_seekBarInitPositionRight = _playerWidth - _seekBarInitPositionLeft - seekBar_mc.width;
			_seekBarProgressWidth = seekBar_mc.width - 2;
			getButtonProperties();

			_progressTimer = new Timer(50);
			_progressTimer.addEventListener(TimerEvent.TIMER, progressTimerHandler);
			this.addEventListener(Event.REMOVED_FROM_STAGE, removeFromStageHandler);
		}
		private function layout():void {
			if (_playerWidth < _minWidth) {
				_playerWidth = _minWidth;
			}
			outline_mc.width = _playerWidth;
			outlineFill_mc.x = outline_mc.x + 1;
			outlineFill_mc.width = _playerWidth - 2;
			outlineShadow_mc.x = outline_mc.x;
			outlineShadow_mc.width = _playerWidth;

			// play/pause button layout
			if (_playPauseAncherLeft) {
				// Keep the x position for playPause_mc.
				playPause_mc.x = _playPauseInitPositionLeft;
				if (_playPauseAncherRight) {
					playPause_mc.width = _playerWidth - _playPauseInitPositionLeft - _playPauseInitPositionRight;
				} else {
					// Keep the width for playPause_mc.
				}
			} else {
				if (_playPauseAncherRight) {
					playPause_mc.x = outline_mc.x + _playerWidth - _playPauseInitPositionRight - playPause_mc.width;
				} else {
					throw new IllegalOperationError("Invalid play/pause button ancher setting.");
				}
			}
			// stop button layout
			if (_stopAncherLeft) {
				// Keep the x position for stop_mc.
				stop_mc.x = _stopInitPositionLeft;
				if (_stopAncherRight) {
					stop_mc.width = _playerWidth - _stopInitPositionLeft - _stopInitPositionRight;
				} else {
					// Keep the width for stop_mc.
				}
			} else {
				if (_stopAncherRight) {
					stop_mc.x = outline_mc.x + _playerWidth - _stopInitPositionRight - stop_mc.width;
				} else {
					throw new IllegalOperationError("Invalid stop button ancher setting.");
				}
			}
			//seek bar layout
			if (_seekBarAncherLeft) {
				seekBar_mc.x = _seekBarInitPositionLeft;
				seekBarProgress_mc.x = _seekBarInitPositionLeft + 1;
				seekBarHit_mc.x = _seekBarInitPositionLeft + 1;
				seekBarHandle_mc.x = _seekBarInitPositionLeft + 1.5;
				if (_seekBarAncherRight) {
					seekBar_mc.width = _playerWidth - _seekBarInitPositionLeft - _seekBarInitPositionRight;
					seekBarHit_mc.width = seekBar_mc.width - 2;
					_seekBarProgressWidth = seekBar_mc.width - 2;
				} else {
					// Keep the width for seek bar.
				}
			} else {
				if (_seekBarAncherRight) {
					seekBar_mc.x = outline_mc.x + _playerWidth - _seekBarInitPositionRight - seekBar_mc.width;
					seekBarProgress_mc.x = seekBar_mc.x + 1;
					seekBarHit_mc.x = seekBar_mc.x + 1;
					seekBarHandle_mc.x = seekBar_mc.x + 1.5;
				} else {
					throw new IllegalOperationError("Invalid seek bar ancher setting.");
				}
			}
			seekBarProgress_mc.width = 0;
			getButtonProperties();
		}
		private function setUpButtons():void {
			_playButton = new SimpleButton(new PlayButtonNormal(), new PlayButtonOver(), new PlayButtonDown(), new PlayButtonNormal());
			_pauseButton = new SimpleButton(new PauseButtonNormal(), new PauseButtonOver(), new PauseButtonDown(), new PauseButtonNormal());
			_stopButton = new SimpleButton(new StopButtonNormal(), new StopButtonOver(), new StopButtonDown(), new StopButtonNormal());
		}
		private function getButtonProperties():void {
			_playPausePositionX = playPause_mc.x;
			_playPausePositionY = playPause_mc.y;
			_playPausePositionWidth = playPause_mc.width;
			_playPausePositionHeight = playPause_mc.height;
			_stopPositionX = stop_mc.x;
			_stopPositionY = stop_mc.y;
			_stopPositionWidth = stop_mc.width;
			_stopPositionHeight = stop_mc.height;
		}
		private function setPlayPauseProperties():void {
			playPause_mc.x = _playPausePositionX;
			playPause_mc.y = _playPausePositionY;
			playPause_mc.width = _playPausePositionWidth;
			playPause_mc.height = _playPausePositionHeight;
		}
		private function setStopProperties():void {
			stop_mc.x = _stopPositionX;
			stop_mc.y = _stopPositionY;
			stop_mc.width = _stopPositionWidth;
			stop_mc.height = _stopPositionHeight;
		}
		private function getPauseButton():void {
			removeChild(playPause_mc);
			playPause_mc = _pauseButton;
			playPause_mc.addEventListener(MouseEvent.CLICK, pauseClickHandler);
			setPlayPauseProperties();
			addChild(playPause_mc);
		}
		private function getPlayButton():void {
			removeChild(playPause_mc);
			playPause_mc = _playButton;
			playPause_mc.addEventListener(MouseEvent.CLICK, playClickHandler);
			setPlayPauseProperties();
			addChild(playPause_mc);
		}
		private function getStopButton():void {
			removeChild(stop_mc);
			stop_mc = _stopButton;
			stop_mc.addEventListener(MouseEvent.CLICK, stopClickHandler);
			setStopProperties();
			addChild(stop_mc);
		}
		private function soundLoadCompleteHandler(_event:Event):void {
			trace("Sound loaded completed!");
			_isDownloading = false;
			_sound.removeEventListener(Event.COMPLETE, soundLoadCompleteHandler);
			//_sound.removeEventListener(Event.ID3, id3Handler);
			_sound.removeEventListener(IOErrorEvent.IO_ERROR, soundLoadIOErrorHandler);
			_sound.removeEventListener(ProgressEvent.PROGRESS, soundLoadProgressHandler);
			//getPauseButton();
			//getPlayButton();
			//getStopButton();
			// Start to play.
			//_soundChannel = _sound.play();
			//_isPlaying = false;
			//_soundChannel.addEventListener(Event.SOUND_COMPLETE, soundCompleteHandler);
			//_progressTimer.start();
			//seekBarHandle_mc.addEventListener(MouseEvent.MOUSE_DOWN, seekBarHandleStartDragHandler);
		}
		private function soundLoadIOErrorHandler(_event:IOErrorEvent):void {
			_isDownloading = false;
			_sound.removeEventListener(Event.COMPLETE, soundLoadCompleteHandler);
			//_sound.removeEventListener(Event.ID3, id3Handler);
			_sound.removeEventListener(IOErrorEvent.IO_ERROR, soundLoadIOErrorHandler);
			_sound.removeEventListener(ProgressEvent.PROGRESS, soundLoadProgressHandler);
			throw new IOError("Cannot load sound file: " + _event.text);
		}
		private function soundLoadProgressHandler(_event:ProgressEvent) {
			_soundLoadedPer = _event.bytesLoaded / _event.bytesTotal;
			seekBarProgress_mc.width = _seekBarProgressWidth * _soundLoadedPer;
		}
		private function soundCompleteHandler(_event:Event):void {
			_isPlaying = false;
			playPause_mc.removeEventListener(MouseEvent.CLICK, pauseClickHandler);
			getPlayButton();
			_pausePosition = 0;
			_progressTimer.stop();
			seekBarHandle_mc.x = seekBarProgress_mc.x + 0.5;
		}
		private function pauseClickHandler(_event:MouseEvent):void {
			_isPlaying = false;
			playPause_mc.removeEventListener(MouseEvent.CLICK, pauseClickHandler);
			getPlayButton();
			_pausePosition = _soundChannel.position;
			_soundChannel.stop();
			_progressTimer.stop();
		}
		private function playClickHandler(_event:MouseEvent = null):void {
			_isPlaying = true;
			playPause_mc.addEventListener(MouseEvent.CLICK, playClickHandler);
			getPauseButton();
			_soundChannel = _sound.play(_pausePosition);
			_soundChannel.addEventListener(Event.SOUND_COMPLETE, soundCompleteHandler);
			_progressTimer.start();
		}
		private function stopClickHandler(_event:MouseEvent):void {
			_isPlaying = false;
			playPause_mc.removeEventListener(MouseEvent.CLICK, pauseClickHandler);
			getPlayButton();
			_pausePosition = 0;
			_soundChannel.stop();
			_progressTimer.stop();
			seekBarHandle_mc.x = seekBarProgress_mc.x + 0.5;
		}
		private function progressTimerHandler(_event:TimerEvent):void {
			var _soundLength:Number = _sound.length / _soundLoadedPer;
			var _per:Number = _soundChannel.position / _soundLength;
			trace("sound length = " + _soundLength);
			trace("sound position = " + _soundChannel.position);
			seekBarHandle_mc.x = seekBarProgress_mc.x + 0.5 + (_seekBarProgressWidth - 1) * _per;
		}
		private function seekBarHandleStartDragHandler(_event:MouseEvent):void {		
			_isPlayingBeforeDrag = _isPlaying;
			if(_isPlayingBeforeDrag) {
				_soundChannel.stop();
				_progressTimer.stop();
				_isPlaying = false;
			}
			seekBarHandle_mc.stage.addEventListener(MouseEvent.MOUSE_UP,seekBarHandleStopDragHandler);
			seekBarHandle_mc.startDrag(false, new Rectangle(seekBarProgress_mc.x + 0.5, seekBarHandle_mc.y, _seekBarProgressWidth - 1, 0));
		}
		private function seekBarHandleStopDragHandler(_event:MouseEvent):void {			
			seekBarHandle_mc.stage.removeEventListener(MouseEvent.MOUSE_UP,seekBarHandleStopDragHandler);
			seekBarHandle_mc.stopDrag();
			if (seekBarHandle_mc.x > seekBarProgress_mc.x + seekBarProgress_mc.width - 0.5) {
				seekBarHandle_mc.x = seekBarProgress_mc.x + seekBarProgress_mc.width - 0.5;
			}
			var _per:Number = (seekBarHandle_mc.x - seekBarProgress_mc.x - 0.5) / (_seekBarProgressWidth - 1);
			var _soundLength:Number = _sound.length / _soundLoadedPer;
			_pausePosition = _soundLength * _per;
			if(_isPlayingBeforeDrag) {
				_soundChannel = _sound.play(_pausePosition);
				_soundChannel.addEventListener(Event.SOUND_COMPLETE, soundCompleteHandler);
				_progressTimer.start();
				_isPlaying = true;
			}
		}
		private function removeFromStageHandler(_event:Event):void {
			trace("removed from stage");
			if (_isDownloading) {
				_sound.close();
			}
			if (_isPlaying) {
				_soundChannel.stop();
			}
		}
	}
}