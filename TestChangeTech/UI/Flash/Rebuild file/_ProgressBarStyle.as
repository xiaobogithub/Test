//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;
    import mx.skins.halo.*;

    public class _ProgressBarStyle {

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("ProgressBar");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("ProgressBar", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.trackColors = [0xE7E7E7, 0xFFFFFF];
                    this.fontWeight = "bold";
                    this.maskSkin = ProgressMaskSkin;
                    this.leading = 0;
                    this.trackSkin = ProgressTrackSkin;
                    this.indeterminateMoveInterval = 28;
                    this.barSkin = ProgressBarSkin;
                    this.indeterminateSkin = ProgressIndeterminateSkin;
                };
            };
        }

    }
}//package 
