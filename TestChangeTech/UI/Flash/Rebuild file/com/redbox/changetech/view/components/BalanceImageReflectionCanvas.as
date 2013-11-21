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
    import mx.effects.*;
    import mx.binding.*;
    import mx.containers.*;
    import flash.utils.*;
    import flash.system.*;
    import flash.accessibility.*;
    import flash.xml.*;
    import flash.net.*;
    import com.rictus.reflector.*;
    import flash.filters.*;
    import flash.ui.*;
    import mx.effects.easing.*;
    import flash.external.*;
    import flash.debugger.*;
    import flash.errors.*;
    import flash.printing.*;
    import flash.profiler.*;

    public class BalanceImageReflectionCanvas extends Canvas implements IBindingClient {

        private var _1282133823fadeIn:Fade
        private var _811296866contentImage:Image
        private var IMAGE_LOADED:String = "imageLoaded"
        mx_internal var _watchers:Array
        private var _1091436750fadeOut:Fade
        public var isImageLoaded:Boolean = false
        private var _source:String
        mx_internal var _bindingsBeginWithWord:Object
        private var lastSource:String
        mx_internal var _bindingsByDestination:Object
        private var _1646836710imageReflection:Reflector
        mx_internal var _bindings:Array
        private var _documentDescriptor_:UIComponentDescriptor

        private static var _watcherSetupUtil:IWatcherSetupUtil;

        public function BalanceImageReflectionCanvas(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:Image, id:"contentImage", events:{ioError:"__contentImage_ioError", complete:"__contentImage_complete", unload:"__contentImage_unload"}}), new UIComponentDescriptor({type:Reflector, id:"imageReflection", propertiesFactory:function ():Object{
                    return ({falloff:0.1, alpha:0.3});
                }})]});
            }});
            _bindings = [];
            _watchers = [];
            _bindingsByDestination = {};
            _bindingsBeginWithWord = {};
            super();
            mx_internal::_document = this;
            this.verticalScrollPolicy = "off";
            _BalanceImageReflectionCanvas_Fade1_i();
            _BalanceImageReflectionCanvas_Fade2_i();
            this.addEventListener("creationComplete", ___BalanceImageReflectionCanvas_Canvas1_creationComplete);
        }
        public function set fadeIn(_arg1:Fade):void{
            var _local2:Object = this._1282133823fadeIn;
            if (_local2 !== _arg1){
                this._1282133823fadeIn = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeIn", _local2, _arg1));
            };
        }
        public function __contentImage_ioError(_arg1:IOErrorEvent):void{
            dispatchEvent(new Event(IOErrorEvent.IO_ERROR, true));
        }
        override public function initialize():void{
            var target:* = null;
            var watcherSetupUtilClass:* = null;
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            var bindings:* = _BalanceImageReflectionCanvas_bindingsSetup();
            var watchers:* = [];
            target = this;
            if (_watcherSetupUtil == null){
                watcherSetupUtilClass = getDefinitionByName("_com_redbox_changetech_view_components_BalanceImageReflectionCanvasWatcherSetupUtil");
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
        public function __contentImage_complete(_arg1:Event):void{
            imageLoadComplete(_arg1);
        }
        public function get source():String{
            return (_source);
        }
        public function __contentImage_unload(_arg1:Event):void{
            removeReflection(_arg1);
            isImageLoaded = false;
        }
        private function init():void{
            trace(("source=" + source));
        }
        private function _BalanceImageReflectionCanvas_Fade2_i():Fade{
            var _local1:Fade = new Fade();
            fadeOut = _local1;
            _local1.alphaFrom = 1;
            _local1.alphaTo = 0;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeOut", fadeOut);
            return (_local1);
        }
        private function setReflection(_arg1:Event):void{
            imageReflection.visible = true;
            imageReflection.target = contentImage;
            imageReflection.y = ((contentImage.y + contentImage.contentHeight) - 2);
        }
        private function _BalanceImageReflectionCanvas_bindingsSetup():Array{
            var binding:* = null;
            var result:* = [];
            binding = new Binding(this, function ():Object{
                return (contentImage);
            }, function (_arg1:Object):void{
                fadeIn.target = _arg1;
            }, "fadeIn.target");
            result[0] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeOut);
            }, function (_arg1:Function):void{
                fadeIn.easingFunction = _arg1;
            }, "fadeIn.easingFunction");
            result[1] = binding;
            binding = new Binding(this, function ():Object{
                return (contentImage);
            }, function (_arg1:Object):void{
                fadeOut.target = _arg1;
            }, "fadeOut.target");
            result[2] = binding;
            binding = new Binding(this, function ():Function{
                return (Linear.easeOut);
            }, function (_arg1:Function):void{
                fadeOut.easingFunction = _arg1;
            }, "fadeOut.easingFunction");
            result[3] = binding;
            binding = new Binding(this, function ():Object{
                return (source);
            }, function (_arg1:Object):void{
                contentImage.source = _arg1;
            }, "contentImage.source");
            result[4] = binding;
            binding = new Binding(this, function ():Boolean{
                return (!((contentImage.source == null)));
            }, function (_arg1:Boolean):void{
                imageReflection.visible = _arg1;
            }, "imageReflection.visible");
            result[5] = binding;
            binding = new Binding(this, function ():Number{
                return (((contentImage.y + contentImage.height) - 10));
            }, function (_arg1:Number):void{
                imageReflection.y = _arg1;
            }, "imageReflection.y");
            result[6] = binding;
            return (result);
        }
        public function get fadeOut():Fade{
            return (this._1091436750fadeOut);
        }
        private function _BalanceImageReflectionCanvas_bindingExprs():void{
            var _local1:*;
            _local1 = contentImage;
            _local1 = Linear.easeOut;
            _local1 = contentImage;
            _local1 = Linear.easeOut;
            _local1 = source;
            _local1 = !((contentImage.source == null));
            _local1 = ((contentImage.y + contentImage.height) - 10);
        }
        public function set source(_arg1:String):void{
            var _local2:Object = this.source;
            if (_local2 !== _arg1){
                this._896505829source = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "source", _local2, _arg1));
            };
        }
        private function imageLoadComplete(_arg1:Event):void{
            trace("IMAGE REFLECTION CANVAS IMAGE LOAD COMPLETE");
            setReflection(_arg1);
            dispatchEvent(new Event(IMAGE_LOADED, true));
            isImageLoaded = true;
        }
        private function cleanUp(_arg1:Event):void{
        }
        public function get fadeIn():Fade{
            return (this._1282133823fadeIn);
        }
        public function set contentImage(_arg1:Image):void{
            var _local2:Object = this._811296866contentImage;
            if (_local2 !== _arg1){
                this._811296866contentImage = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "contentImage", _local2, _arg1));
            };
        }
        private function removeReflection(_arg1:Event):void{
            imageReflection.target = null;
            imageReflection.visible = false;
        }
        public function set imageReflection(_arg1:Reflector):void{
            var _local2:Object = this._1646836710imageReflection;
            if (_local2 !== _arg1){
                this._1646836710imageReflection = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "imageReflection", _local2, _arg1));
            };
        }
        private function _BalanceImageReflectionCanvas_Fade1_i():Fade{
            var _local1:Fade = new Fade();
            fadeIn = _local1;
            _local1.alphaFrom = 0;
            _local1.alphaTo = 1;
            _local1.duration = 500;
            BindingManager.executeBindings(this, "fadeIn", fadeIn);
            return (_local1);
        }
        public function get contentImage():Image{
            return (this._811296866contentImage);
        }
        public function set fadeOut(_arg1:Fade):void{
            var _local2:Object = this._1091436750fadeOut;
            if (_local2 !== _arg1){
                this._1091436750fadeOut = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "fadeOut", _local2, _arg1));
            };
        }
        public function get imageReflection():Reflector{
            return (this._1646836710imageReflection);
        }
        private function set _896505829source(_arg1:String):void{
            _source = _arg1;
        }
        public function ___BalanceImageReflectionCanvas_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }

        public static function set watcherSetupUtil(_arg1:IWatcherSetupUtil):void{
            BalanceImageReflectionCanvas._watcherSetupUtil = _arg1;
        }

    }
}//package com.redbox.changetech.view.components 
