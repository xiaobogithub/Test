//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;

    public class _FormStyle {

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("Form");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("Form", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.paddingTop = 16;
                    this.paddingLeft = 16;
                    this.paddingRight = 16;
                    this.verticalGap = 6;
                    this.paddingBottom = 16;
                };
            };
        }

    }
}//package 
