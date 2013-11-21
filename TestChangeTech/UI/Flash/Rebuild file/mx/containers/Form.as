//Created by Action Script Viewer - http://www.buraks.com/asv
package mx.containers {
    import flash.display.*;
    import mx.core.*;
    import mx.styles.*;
    import mx.controls.*;
    import mx.containers.utilityClasses.*;

    public class Form extends Container {

        mx_internal var layoutObject:BoxLayout
        private var measuredLabelWidth:Number

        mx_internal static const VERSION:String = "3.2.0.3958";

        private static var classInitialized:Boolean = false;

        public function Form(){
            layoutObject = new BoxLayout();
            super();
            if (!classInitialized){
                initializeClass();
                classInitialized = true;
            };
            showInAutomationHierarchy = true;
            mx_internal::layoutObject.target = this;
            mx_internal::layoutObject.direction = BoxDirection.VERTICAL;
        }
        override public function addChild(_arg1:DisplayObject):DisplayObject{
            invalidateLabelWidth();
            return (super.addChild(_arg1));
        }
        override public function styleChanged(_arg1:String):void{
            if (((((!(_arg1)) || ((_arg1 == "styleName")))) || (StyleManager.isSizeInvalidatingStyle(_arg1)))){
                invalidateLabelWidth();
            };
            super.styleChanged(_arg1);
        }
        override public function removeChildAt(_arg1:int):DisplayObject{
            invalidateLabelWidth();
            return (super.removeChildAt(_arg1));
        }
        function calculateLabelWidth():Number{
            var _local5:DisplayObject;
            if (!isNaN(measuredLabelWidth)){
                return (measuredLabelWidth);
            };
            var _local1:Number = 0;
            var _local2:Boolean;
            var _local3:int = numChildren;
            var _local4:int;
            while (_local4 < _local3) {
                _local5 = getChildAt(_local4);
                if ((_local5 is FormItem)){
                    _local1 = Math.max(_local1, FormItem(_local5).getPreferredLabelWidth());
                    _local2 = true;
                };
                _local4++;
            };
            if (_local2){
                measuredLabelWidth = _local1;
            };
            return (_local1);
        }
        function invalidateLabelWidth():void{
            var _local1:int;
            var _local2:int;
            var _local3:IUIComponent;
            if (((!(isNaN(measuredLabelWidth))) && (initialized))){
                measuredLabelWidth = NaN;
                _local1 = numChildren;
                _local2 = 0;
                while (_local2 < _local1) {
                    _local3 = IUIComponent(getChildAt(_local2));
                    if ((_local3 is IInvalidating)){
                        IInvalidating(_local3).invalidateSize();
                    };
                    _local2++;
                };
            };
        }
        public function get maxLabelWidth():Number{
            var _local3:DisplayObject;
            var _local4:Label;
            var _local1:int = numChildren;
            var _local2:int;
            while (_local2 < _local1) {
                _local3 = getChildAt(_local2);
                if ((_local3 is FormItem)){
                    _local4 = FormItem(_local3).itemLabel;
                    if (_local4){
                        return (_local4.width);
                    };
                };
                _local2++;
            };
            return (0);
        }
        override protected function updateDisplayList(_arg1:Number, _arg2:Number):void{
            super.updateDisplayList(_arg1, _arg2);
            mx_internal::layoutObject.updateDisplayList(_arg1, _arg2);
        }
        override public function addChildAt(_arg1:DisplayObject, _arg2:int):DisplayObject{
            invalidateLabelWidth();
            return (super.addChildAt(_arg1, _arg2));
        }
        override protected function measure():void{
            super.measure();
            mx_internal::layoutObject.measure();
            calculateLabelWidth();
        }
        override public function removeChild(_arg1:DisplayObject):DisplayObject{
            invalidateLabelWidth();
            return (super.removeChild(_arg1));
        }

        private static function initializeClass():void{
            StyleManager.registerInheritingStyle("labelWidth");
            StyleManager.registerSizeInvalidatingStyle("labelWidth");
            StyleManager.registerInheritingStyle("indicatorGap");
            StyleManager.registerSizeInvalidatingStyle("indicatorGap");
        }

    }
}//package mx.containers 
