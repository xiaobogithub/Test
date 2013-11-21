//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.geometry.display {
    import flash.display.*;

    public interface IDisplayObjectProxy {

        function get transformBeforeRender():Boolean;
        function get displayObject():DisplayObject;
        function get layoutMode():String;

    }
}//package com.degrafa.geometry.display 
