//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.effects.effectClasses {
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.styles.*;
    import mx.effects.*;
    import mx.containers.*;

    public class ResizeInstance extends TweenEffectInstance {

        private var left
        private var origPercentHeight:Number
        private var parentOrigHorizontalScrollPolicy:String = ""
        private var explicitWidthSet:Boolean
        public var widthFrom:Number
        private var origExplicitHeight:Number
        private var _widthTo:Number
        private var parentOrigVerticalScrollPolicy:String = ""
        private var right
        private var origExplicitWidth:Number
        private var restoreAutoLayoutArray:Array
        private var restoreVisibleArray:Array
        private var bottom
        private var heightSet:Boolean
        private var _heightBy:Number
        private var widthSet:Boolean
        private var origHorizontalScrollPolicy:String = ""
        private var numHideEffectsPlaying:Number = 0
        private var top
        private var origVerticalScrollPolicy:String = ""
        private var _heightTo:Number
        private var explicitHeightSet:Boolean
        public var hideChildrenTargets:Array
        private var origPercentWidth:Number
        public var heightFrom:Number
        private var _widthBy:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        public function ResizeInstance(_arg1:Object){
            super(_arg1);
            needToLayout = true;
        }
        public function set widthBy(_arg1:Number):void{
            _widthBy = _arg1;
            widthSet = !(isNaN(_arg1));
        }
        public function get heightTo():Number{
            return (_heightTo);
        }
        public function set heightTo(_arg1:Number):void{
            _heightTo = _arg1;
            heightSet = !(isNaN(_arg1));
        }
        private function hidePanelChildren():Boolean{
            var _local3:Object;
            var _local4:Number;
            if (!hideChildrenTargets){
                return (false);
            };
            restoreVisibleArray = [];
            restoreAutoLayoutArray = [];
            var _local1:int = hideChildrenTargets.length;
            var _local2:int;
            while (_local2 < _local1) {
                _local3 = hideChildrenTargets[_local2];
                if ((_local3 is Panel)){
                    _local4 = numHideEffectsPlaying;
                    _local3.addEventListener(EffectEvent.EFFECT_START, eventHandler);
                    _local3.dispatchEvent(new Event("resizeStart"));
                    _local3.removeEventListener(EffectEvent.EFFECT_START, eventHandler);
                    if (numHideEffectsPlaying == _local4){
                        makePanelChildrenInvisible(Panel(_local3), _local2);
                    };
                };
                _local2++;
            };
            return ((numHideEffectsPlaying > 0));
        }
        override public function play():void{
            super.play();
            calculateExplicitDimensionChanges();
            var _local1:Boolean = hidePanelChildren();
            if ((target is IStyleClient)){
                left = target.getStyle("left");
                if (left != undefined){
                    target.setStyle("left", undefined);
                };
                right = target.getStyle("right");
                if (right != undefined){
                    target.setStyle("right", undefined);
                };
                top = target.getStyle("top");
                if (top != undefined){
                    target.setStyle("top", undefined);
                };
                bottom = target.getStyle("bottom");
                if (bottom != undefined){
                    target.setStyle("bottom", undefined);
                };
            };
            if (!_local1){
                startResizeTween();
            };
        }
        public function set heightBy(_arg1:Number):void{
            _heightBy = _arg1;
            heightSet = !(isNaN(_arg1));
        }
        override public function initEffect(_arg1:Event):void{
            super.initEffect(_arg1);
            if ((((_arg1 is ResizeEvent)) && ((_arg1.type == ResizeEvent.RESIZE)))){
                if (isNaN(widthBy)){
                    if (isNaN(widthFrom)){
                        widthFrom = ResizeEvent(_arg1).oldWidth;
                    };
                    if (isNaN(widthTo)){
                        _widthTo = target.width;
                    };
                };
                if (isNaN(heightBy)){
                    if (isNaN(heightFrom)){
                        heightFrom = ResizeEvent(_arg1).oldHeight;
                    };
                    if (isNaN(heightTo)){
                        _heightTo = target.height;
                    };
                };
            };
        }
        public function get widthBy():Number{
            return (_widthBy);
        }
        override public function onTweenUpdate(_arg1:Object):void{
            EffectManager.suspendEventHandling();
            target.width = Math.round(_arg1[0]);
            target.height = Math.round(_arg1[1]);
            if (tween){
                tween.needToLayout = true;
            };
            needToLayout = true;
            EffectManager.resumeEventHandling();
        }
        override mx_internal function eventHandler(_arg1:Event):void{
            var _local3:int;
            var _local4:int;
            var _local2:Container = (_arg1.target as Container);
            super.eventHandler(_arg1);
            if (_arg1.type == EffectEvent.EFFECT_START){
                _local2.addEventListener(EffectEvent.EFFECT_END, eventHandler);
                numHideEffectsPlaying++;
            } else {
                if (_arg1.type == EffectEvent.EFFECT_END){
                    _local2.removeEventListener(EffectEvent.EFFECT_END, eventHandler);
                    _local3 = hideChildrenTargets.length;
                    _local4 = 0;
                    while (_local4 < _local3) {
                        if (hideChildrenTargets[_local4] == _local2){
                            break;
                        };
                        _local4++;
                    };
                    makePanelChildrenInvisible(_local2, _local4);
                    if (--numHideEffectsPlaying == 0){
                        startResizeTween();
                    };
                };
            };
        }
        public function set widthTo(_arg1:Number):void{
            _widthTo = _arg1;
            widthSet = !(isNaN(_arg1));
        }
        private function calculateExplicitDimensionChanges():void{
            var _local5:Container;
            var _local6:Container;
            var _local1:* = (propertyChanges) ? propertyChanges.end["explicitWidth"] : undefined;
            var _local2:* = (propertyChanges) ? propertyChanges.end["explicitHeight"] : undefined;
            var _local3:* = (propertyChanges) ? propertyChanges.end["percentWidth"] : undefined;
            var _local4:* = (propertyChanges) ? propertyChanges.end["percentHeight"] : undefined;
            if (!heightSet){
                if (_local4 !== undefined){
                    origPercentHeight = _local4;
                } else {
                    origPercentHeight = target.percentHeight;
                };
                if (isNaN(origPercentHeight)){
                    if (_local2 !== undefined){
                        origExplicitHeight = _local2;
                    } else {
                        origExplicitHeight = target.explicitHeight;
                    };
                };
                _local5 = (target as Container);
                if (((_local5) && ((_local5.verticalScrollBar == null)))){
                    origVerticalScrollPolicy = _local5.verticalScrollPolicy;
                    _local5.verticalScrollPolicy = ScrollPolicy.OFF;
                };
                if (target.parent){
                    _local6 = (target.parent as Container);
                    if (((_local6) && ((_local6.verticalScrollBar == null)))){
                        parentOrigVerticalScrollPolicy = _local6.verticalScrollPolicy;
                        _local6.verticalScrollPolicy = ScrollPolicy.OFF;
                    };
                };
            };
            if (!widthSet){
                if (_local3 !== undefined){
                    origPercentWidth = _local3;
                } else {
                    origPercentWidth = target.percentWidth;
                };
                if (isNaN(origPercentWidth)){
                    if (_local1 !== undefined){
                        origExplicitWidth = _local1;
                    } else {
                        origExplicitWidth = target.explicitWidth;
                    };
                };
                _local5 = (target as Container);
                if (((_local5) && ((_local5.horizontalScrollBar == null)))){
                    origHorizontalScrollPolicy = _local5.horizontalScrollPolicy;
                    _local5.horizontalScrollPolicy = ScrollPolicy.OFF;
                };
                if (target.parent){
                    _local6 = (target.parent as Container);
                    if (((_local6) && ((_local6.horizontalScrollBar == null)))){
                        parentOrigHorizontalScrollPolicy = _local6.horizontalScrollPolicy;
                        _local6.horizontalScrollPolicy = ScrollPolicy.OFF;
                    };
                };
            };
            if (isNaN(widthFrom)){
                widthFrom = (((!(isNaN(widthTo))) && (!(isNaN(widthBy))))) ? (widthTo - widthBy) : target.width;
            };
            if (isNaN(widthTo)){
                if (((((isNaN(widthBy)) && (propertyChanges))) && (((!((propertyChanges.end["width"] === undefined))) || (!((_local1 === undefined))))))){
                    if (((!((_local1 === undefined))) && (!(isNaN(_local1))))){
                        explicitWidthSet = true;
                        _widthTo = _local1;
                    } else {
                        _widthTo = propertyChanges.end["width"];
                    };
                } else {
                    _widthTo = (isNaN(widthBy)) ? target.width : (widthFrom + widthBy);
                };
            };
            if (isNaN(heightFrom)){
                heightFrom = (((!(isNaN(heightTo))) && (!(isNaN(heightBy))))) ? (heightTo - heightBy) : target.height;
            };
            if (isNaN(heightTo)){
                if (((((isNaN(heightBy)) && (propertyChanges))) && (((!((propertyChanges.end["height"] === undefined))) || (!((_local2 === undefined))))))){
                    if (((!((_local2 === undefined))) && (!(isNaN(_local2))))){
                        explicitHeightSet = true;
                        _heightTo = _local2;
                    } else {
                        _heightTo = propertyChanges.end["height"];
                    };
                } else {
                    _heightTo = (isNaN(heightBy)) ? target.height : (heightFrom + heightBy);
                };
            };
        }
        private function makePanelChildrenInvisible(_arg1:Container, _arg2:Number):void{
            var _local4:IUIComponent;
            var _local3:Array = [];
            var _local5:int = _arg1.numChildren;
            var _local6:int;
            while (_local6 < _local5) {
                _local4 = IUIComponent(_arg1.getChildAt(_local6));
                if (_local4.visible){
                    _local3.push(_local4);
                    _local4.setVisible(false, true);
                };
                _local6++;
            };
            _local4 = _arg1.horizontalScrollBar;
            if (((_local4) && (_local4.visible))){
                _local3.push(_local4);
                _local4.setVisible(false, true);
            };
            _local4 = _arg1.verticalScrollBar;
            if (((_local4) && (_local4.visible))){
                _local3.push(_local4);
                _local4.setVisible(false, true);
            };
            restoreVisibleArray[_arg2] = _local3;
            if (_arg1.autoLayout){
                _arg1.autoLayout = false;
                restoreAutoLayoutArray[_arg2] = true;
            };
        }
        override public function end():void{
            if (!tween){
                calculateExplicitDimensionChanges();
                onTweenEnd((playReversed) ? [widthFrom, heightFrom] : [widthTo, heightTo]);
            };
            super.end();
        }
        private function startResizeTween():void{
            EffectManager.startVectorEffect(IUIComponent(target));
            tween = createTween(this, [widthFrom, heightFrom], [widthTo, heightTo], duration);
            applyTweenStartValues();
        }
        public function get heightBy():Number{
            return (_heightBy);
        }
        private function restorePanelChildren():void{
            var _local1:int;
            var _local2:int;
            var _local3:IUIComponent;
            var _local4:Array;
            var _local5:int;
            var _local6:int;
            if (hideChildrenTargets){
                _local1 = hideChildrenTargets.length;
                _local2 = 0;
                while (_local2 < _local1) {
                    _local3 = hideChildrenTargets[_local2];
                    _local4 = restoreVisibleArray[_local2];
                    if (_local4){
                        _local5 = _local4.length;
                        _local6 = 0;
                        while (_local6 < _local5) {
                            _local4[_local6].setVisible(true, true);
                            _local6++;
                        };
                    };
                    if (restoreAutoLayoutArray[_local2]){
                        Container(_local3).autoLayout = true;
                    };
                    _local3.dispatchEvent(new Event("resizeEnd"));
                    _local2++;
                };
            };
        }
        override public function onTweenEnd(_arg1:Object):void{
            var _local2:Container;
            var _local3:Container;
            EffectManager.endVectorEffect(IUIComponent(target));
            Application.application.callLater(restorePanelChildren);
            super.onTweenEnd(_arg1);
            EffectManager.suspendEventHandling();
            if (!heightSet){
                target.percentHeight = origPercentHeight;
                target.explicitHeight = origExplicitHeight;
                if (origVerticalScrollPolicy != ""){
                    _local2 = (target as Container);
                    if (_local2){
                        _local2.verticalScrollPolicy = origVerticalScrollPolicy;
                        origVerticalScrollPolicy = "";
                    };
                };
                if (((!((parentOrigVerticalScrollPolicy == ""))) && (target.parent))){
                    _local3 = (target.parent as Container);
                    if (_local3){
                        _local3.verticalScrollPolicy = parentOrigVerticalScrollPolicy;
                        parentOrigVerticalScrollPolicy = "";
                    };
                };
            };
            if (!widthSet){
                target.percentWidth = origPercentWidth;
                target.explicitWidth = origExplicitWidth;
                if (origHorizontalScrollPolicy != ""){
                    _local2 = (target as Container);
                    if (_local2){
                        _local2.horizontalScrollPolicy = origHorizontalScrollPolicy;
                        origHorizontalScrollPolicy = "";
                    };
                };
                if (((!((parentOrigHorizontalScrollPolicy == ""))) && (target.parent))){
                    _local3 = (target.parent as Container);
                    if (_local3){
                        _local3.horizontalScrollPolicy = parentOrigHorizontalScrollPolicy;
                        parentOrigHorizontalScrollPolicy = "";
                    };
                };
            };
            if (left != undefined){
                target.setStyle("left", left);
            };
            if (right != undefined){
                target.setStyle("right", right);
            };
            if (top != undefined){
                target.setStyle("top", top);
            };
            if (bottom != undefined){
                target.setStyle("bottom", bottom);
            };
            EffectManager.resumeEventHandling();
        }
        public function get widthTo():Number{
            return (_widthTo);
        }

    }
}//package mx.effects.effectClasses 
