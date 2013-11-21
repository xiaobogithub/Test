//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import mx.controls.sliderClasses.*;

    public class CustomSliderThumb extends SliderThumb {

        override protected function measure():void{
            super.measure();
            measuredWidth = currentSkin.measuredWidth;
            measuredHeight = currentSkin.measuredHeight;
        }

    }
}//package com.redbox.changetech.view.components 
