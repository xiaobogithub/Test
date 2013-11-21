//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core.collections {
    import com.degrafa.*;

    public class GraphicPointCollection extends DegrafaCollection {

        public function GraphicPointCollection(_arg1:Array=null, _arg2:Boolean=false){
            super(IGraphicPoint, _arg1, _arg2);
        }
        public function addItem(_arg1:IGraphicPoint):IGraphicPoint{
            return (super._addItem(_arg1));
        }
        public function addItemAt(_arg1:IGraphicPoint, _arg2:Number):IGraphicPoint{
            return (super._addItemAt(_arg1, _arg2));
        }
        public function getItemIndex(_arg1:IGraphicPoint):int{
            return (super._getItemIndex(_arg1));
        }
        public function removeItemAt(_arg1:Number):IGraphicPoint{
            return (super._removeItemAt(_arg1));
        }
        public function getItemAt(_arg1:Number):IGraphicPoint{
            return (super._getItemAt(_arg1));
        }
        public function setItemIndex(_arg1:IGraphicPoint, _arg2:Number):Boolean{
            return (super._setItemIndex(_arg1, _arg2));
        }
        public function removeItem(_arg1:IGraphicPoint):IGraphicPoint{
            return (super._removeItem(_arg1));
        }
        public function addItems(_arg1:GraphicPointCollection):GraphicPointCollection{
            super.concat(_arg1.items);
            return (this);
        }

    }
}//package com.degrafa.core.collections 
