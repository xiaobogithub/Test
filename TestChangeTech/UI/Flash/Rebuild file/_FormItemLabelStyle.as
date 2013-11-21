//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;

    public class _FormItemLabelStyle {

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("FormItemLabel");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("FormItemLabel", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.textAlign = "right";
                };
            };
        }

    }
}//package 
