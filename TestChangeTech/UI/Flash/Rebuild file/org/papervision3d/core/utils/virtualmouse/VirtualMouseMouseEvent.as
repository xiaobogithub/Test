//Created by Action Script Viewer - http://www.buraks.com/asv
package org.papervision3d.core.utils.virtualmouse {
    import flash.display.*;
    import flash.events.*;

    public class VirtualMouseMouseEvent extends MouseEvent implements IVirtualMouseEvent {

        public function VirtualMouseMouseEvent(_arg1:String, _arg2:Boolean=false, _arg3:Boolean=false, _arg4:Number=NaN, _arg5:Number=NaN, _arg6:InteractiveObject=null, _arg7:Boolean=false, _arg8:Boolean=false, _arg9:Boolean=false, _arg10:Boolean=false, _arg11:int=0){
            super(_arg1, _arg2, _arg3, _arg4, _arg5, _arg6, _arg7, _arg8, _arg9, _arg10, _arg11);
        }
    }
}//package org.papervision3d.core.utils.virtualmouse 
