//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.render.sort {

    public class BasicRenderSorter implements IRenderSorter {

        public function sort(_arg1:Array):void{
            _arg1.sortOn("screenDepth", Array.NUMERIC);
        }

    }
}//package org.papervision3d.core.render.sort 
