//Created by Action Script Viewer - http://www.buraks.com/asv
package {
    import mx.core.*;
    import mx.styles.*;
    import mx.skins.halo.*;

    public class _DragManagerStyle {

        private static var _embed_css_Assets_swf_mx_skins_cursor_DragCopy_1173942308:Class = _DragManagerStyle__embed_css_Assets_swf_mx_skins_cursor_DragCopy_1173942308;
        private static var _embed_css_Assets_swf_mx_skins_cursor_DragLink_1174204597:Class = _DragManagerStyle__embed_css_Assets_swf_mx_skins_cursor_DragLink_1174204597;
        private static var _embed_css_Assets_swf_mx_skins_cursor_DragReject_501850918:Class = _DragManagerStyle__embed_css_Assets_swf_mx_skins_cursor_DragReject_501850918;
        private static var _embed_css_Assets_swf_mx_skins_cursor_DragMove_1174170960:Class = _DragManagerStyle__embed_css_Assets_swf_mx_skins_cursor_DragMove_1174170960;

        public static function init(_arg1:IFlexModuleFactory):void{
            var fbs:* = _arg1;
            var style:* = StyleManager.getStyleDeclaration("DragManager");
            if (!style){
                style = new CSSStyleDeclaration();
                StyleManager.setStyleDeclaration("DragManager", style, false);
            };
            if (style.defaultFactory == null){
                style.defaultFactory = function ():void{
                    this.rejectCursor = _embed_css_Assets_swf_mx_skins_cursor_DragReject_501850918;
                    this.defaultDragImageSkin = DefaultDragImage;
                    this.moveCursor = _embed_css_Assets_swf_mx_skins_cursor_DragMove_1174170960;
                    this.copyCursor = _embed_css_Assets_swf_mx_skins_cursor_DragCopy_1173942308;
                    this.linkCursor = _embed_css_Assets_swf_mx_skins_cursor_DragLink_1174204597;
                };
            };
        }

    }
}//package 
