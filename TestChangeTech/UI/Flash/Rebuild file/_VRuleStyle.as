﻿//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;

    public class _VRuleStyle {

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("VRule");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("VRule", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.strokeWidth = 2;
                    this.strokeColor = 12897484;
                };
            };
        }

    }
}//package 