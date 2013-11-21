//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.view {

    public interface IView {

        function stopRendering(_arg1:Boolean=false, _arg2:Boolean=false):void;
        function startRendering():void;
        function singleRender():void;

    }
}//package org.papervision3d.core.view 
