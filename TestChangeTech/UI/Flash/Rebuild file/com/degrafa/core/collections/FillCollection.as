//Created by Action Script Viewer - http://www.buraks.com/asv
package com.degrafa.core.collections {
    import com.degrafa.core.*;

    public class FillCollection extends DegrafaCollection {

        public function FillCollection(_arg1:Array=null, _arg2:Boolean=false){
            super(IGraphicsFill, _arg1, _arg2);
        }
        public function addItem(_arg1:IGraphicsFill):IGraphicsFill{
            return (super._addItem(_arg1));
        }
        public function addItemAt(_arg1:IGraphicsFill, _arg2:Number):IGraphicsFill{
            return (super._addItemAt(_arg1, _arg2));
        }
        public function getItemIndex(_arg1:IGraphicsFill):int{
            return (super._getItemIndex(_arg1));
        }
        public function removeItemAt(_arg1:Number):IGraphicsFill{
            return (super._removeItemAt(_arg1));
        }
        public function getItemAt(_arg1:Number):IGraphicsFill{
            return (super._getItemAt(_arg1));
        }
        public function setItemIndex(_arg1:IGraphicsFill, _arg2:Number):Boolean{
            return (super._setItemIndex(_arg1, _arg2));
        }
        public function removeItem(_arg1:IGraphicsFill):IGraphicsFill{
            return (super._removeItem(_arg1));
        }
        public function addItems(_arg1:FillCollection):FillCollection{
            super.concat(_arg1.items);
            return (this);
        }

    }
}//package com.degrafa.core.collections 
