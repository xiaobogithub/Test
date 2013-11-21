//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;

    public class _FormItemStyle {

        private static var _embed_css_Assets_swf_mx_containers_FormItem_Required_592249147:Class = _FormItemStyle__embed_css_Assets_swf_mx_containers_FormItem_Required_592249147;

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("FormItem");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("FormItem", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.indicatorSkin = _embed_css_Assets_swf_mx_containers_FormItem_Required_592249147;
                };
            };
        }

    }
}//package 
