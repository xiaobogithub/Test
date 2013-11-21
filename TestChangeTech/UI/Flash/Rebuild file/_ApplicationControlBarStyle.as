//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;

    public class _ApplicationControlBarStyle {

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("ApplicationControlBar");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("ApplicationControlBar", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.paddingTop = 5;
                    this.paddingLeft = 8;
                    this.fillAlphas = [0, 0];
                    this.cornerRadius = 5;
                    this.paddingRight = 8;
                    this.fillColors = [0xFFFFFF, 0xFFFFFF];
                    this.dropShadowEnabled = true;
                    this.docked = false;
                    this.paddingBottom = 4;
                    this.borderStyle = "applicationControlBar";
                    this.shadowDistance = 5;
                };
            };
        }

    }
}//package 
