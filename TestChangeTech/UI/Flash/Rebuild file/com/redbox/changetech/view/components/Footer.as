//Created by Action Script Viewer - http://www.buraks.com/asv
package com.redbox.changetech.view.components {
    import flash.geom.*;
    import mx.core.*;
    import flash.events.*;
    import mx.events.*;
    import mx.containers.*;

    public class Footer extends Canvas {

        private var _1994251252balanceRoomBadge:BalanceRoomBadge
        private var remTransform:ColorTransform
        private var _documentDescriptor_:UIComponentDescriptor

        public function Footer(){
            _documentDescriptor_ = new UIComponentDescriptor({type:Canvas, propertiesFactory:function ():Object{
                return ({childDescriptors:[new UIComponentDescriptor({type:BalanceRoomBadge, id:"balanceRoomBadge", events:{click:"__balanceRoomBadge_click"}, propertiesFactory:function ():Object{
                    return ({useHandCursor:true, buttonMode:true});
                }})]});
            }});
            super();
            mx_internal::_document = this;
            this.percentWidth = 100;
            this.verticalScrollPolicy = "off";
            this.horizontalScrollPolicy = "off";
            this.addEventListener("creationComplete", ___Footer_Canvas1_creationComplete);
        }
        private function mouseOutHandler(_arg1:Event):void{
            balanceRoomBadge.transform.colorTransform = remTransform;
        }
        public function ___Footer_Canvas1_creationComplete(_arg1:FlexEvent):void{
            init();
        }
        override public function initialize():void{
            mx_internal::setDocumentDescriptor(_documentDescriptor_);
            super.initialize();
        }
        private function mouseOverHandler(_arg1:Event):void{
            var _local2:Number = balanceRoomBadge.transform.colorTransform.redOffset;
            var _local3:Number = balanceRoomBadge.transform.colorTransform.blueOffset;
            var _local4:Number = balanceRoomBadge.transform.colorTransform.greenOffset;
            remTransform = new ColorTransform(1, 1, 1, 1, _local2, _local4, _local3, 0);
            balanceRoomBadge.transform.colorTransform = new ColorTransform(1, 1, 1, 1, (_local2 + 30), (_local4 + 30), (_local3 + 30), 0);
        }
        public function __balanceRoomBadge_click(_arg1:MouseEvent):void{
            dispatchEvent(new MouseEvent(MouseEvent.CLICK, true));
        }
        public function set balanceRoomBadge(_arg1:BalanceRoomBadge):void{
            var _local2:Object = this._1994251252balanceRoomBadge;
            if (_local2 !== _arg1){
                this._1994251252balanceRoomBadge = _arg1;
                this.dispatchEvent(PropertyChangeEvent.createUpdateEvent(this, "balanceRoomBadge", _local2, _arg1));
            };
        }
        public function get balanceRoomBadge():BalanceRoomBadge{
            return (this._1994251252balanceRoomBadge);
        }
        private function init():void{
            balanceRoomBadge.addEventListener(MouseEvent.MOUSE_OVER, mouseOverHandler);
            balanceRoomBadge.addEventListener(MouseEvent.MOUSE_OUT, mouseOutHandler);
        }

    }
}//package com.redbox.changetech.view.components 
