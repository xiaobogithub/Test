﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;
    import mx.skins.halo.*;

    public class _SWFLoaderStyle {

        private static var _embed_css_Assets_swf___brokenImage_323519580:Class = _SWFLoaderStyle__embed_css_Assets_swf___brokenImage_323519580;

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("SWFLoader");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("SWFLoader", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.brokenImageSkin = _embed_css_Assets_swf___brokenImage_323519580;
                    this.borderStyle = "none";
                    this.brokenImageBorderSkin = BrokenImageBorderSkin;
                };
            };
        }

    }
}//package 
